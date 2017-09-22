using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Fazaa
{
	public class WebService
	{
		private const string BaseURl = "http://fazaasa.com/webservice/";
		static HttpClient client;
		public WebService()
		{
			client = new HttpClient();
			client.MaxResponseContentBufferSize = 256000;
		}
		public UserModel Login(string email, string password, string DeviceToken)
		{
			UserModel um = new UserModel();
			string deviceType = string.Empty;
			var uri = new Uri(string.Format(BaseURl + "login.php", string.Empty));

			try
			{
                Debug.WriteLine("****************************"+StaticDataModel.DeviceToken);
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("useremail", email);
				j.Add("password", password);
				j.Add("gcm_id", StaticDataModel.DeviceToken);
				if (Device.OS == TargetPlatform.iOS)
					deviceType = "ios";
				else
					deviceType = "android";
				j.Add("D_Device", deviceType);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(BaseURl + "login.php", content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						var DataValues = jObj["driverdetail"];
						StaticDataModel.UserId = Convert.ToInt32(DataValues["driver_id"].ToString());
						um.driver_id = Convert.ToInt32(DataValues["driver_id"].ToString());
						um.firstname = DataValues["firstname"].ToString();
						um.email = DataValues["email"].ToString();
						um.DOB = DataValues["DOB"].ToString();
						um.city = DataValues["city"].ToString();
						um.phonenumber = Convert.ToDouble(DataValues["phonenumber"].ToString());
						um.address = DataValues["address"].ToString();
						um.device = DataValues["device"].ToString();
						um.vehicle = DataValues["vehicle"].ToString();
						um.image = DataValues["image"].ToString();
						StaticDataModel.ProfilePic = um.image;
						StaticDataModel.UserName = um.firstname + " " + um.lastname;

						//Application.Current.MainPage = new RootPage(um);

					}

				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}

			return um;
		}
		public async void UpdateLocation()
		{
			try
			{
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("driver_id", StaticDataModel.UserId);
				j.Add("driver_address", "");
				j.Add("driver_lat", StaticDataModel.Lattitude);
				j.Add("driver_long", StaticDataModel.Longitude);
				j.Add("driver_device", StaticDataModel.DeviceType);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				response = await client.PostAsync(BaseURl + "update_driverlocation.php", content);
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						var DataValues = jObj["responseMessage"];


					}

				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}


		}
		public  int ForgotPassword(string email)
		{
            int ret = 0;
			try
			{
				HttpClient client = new HttpClient();
				JObject j = new JObject();
				j.Add("useremail", email);
				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				HttpResponseMessage response = client.PostAsync(BaseURl + "forgotpass_mail.php", content).Result;
				if (response.IsSuccessStatusCode)
				{
					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
                        var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);

						var DataValues = jObj["responseCode"].ToString();
						ret = Convert.ToInt32(DataValues);
						StaticMethods.ShowToast(DataValues);
						
					}
					
					

				}

			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);

			}
            finally
            {
              StaticMethods.DismissLoader();  
            }
            return ret;
		}
		public int AcceptOrder(int driver_id, int order_id)
		{
			int responseCode = 0;
			try
			{
				HttpResponseMessage response = null;
				JObject j = new JObject();
				//j.Add("driver_id", driver_id);
				//j.Add("order_id", order_id);
				j.Add("driver_id", StaticDataModel.UserId);
				j.Add("order_id", order_id);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				response = client.PostAsync(BaseURl + "order_accept.php", content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						var DataValues = jObj["responseCode"].ToString();
						responseCode = Convert.ToInt32(DataValues);
					}

				}
				return responseCode;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"ERROR {0}", ex.Message);
				return responseCode;
			}
			finally
			{
				StaticMethods.DismissLoader();
			}


		}
		public OrderModel GetOrderDetails(int order_id)
		{
			OrderModel um = new OrderModel();

			try
			{

				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("order_id", order_id);
				j.Add("driver_id", StaticDataModel.UserId);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				response = client.PostAsync(BaseURl + "ongoing_order.php", content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						var DataValues = jObj["orderdata"];
						um.responseCode = Convert.ToInt32(jObj["responseCode"].ToString());
						um.Status = Convert.ToInt32(DataValues["Status"].ToString());
						um.destination_Name = DataValues["destination_Name"].ToString();
						um.destination_Address = DataValues["destination_Address"].ToString();
						um.destination_long = Convert.ToDouble(DataValues["destination_long"].ToString());
						um.User_Email_Address = DataValues["User_Email_Address"].ToString();
						um.User_Phone_Number = Convert.ToDouble(DataValues["User_Phone_Number"].ToString());
					}

				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}

			return um;
		}
		public int UpdateOrderStatus(int order_status, int order_id)
		{
			int responseCode = 0;
			try
			{
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("order_id", order_id);
				j.Add("order_status", order_status);

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				response = client.PostAsync(BaseURl + "update_order_status.php", content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						var DataValues = jObj["responseCode"].ToString();
						responseCode = Convert.ToInt32(DataValues);
					}

				}
				return responseCode;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"ERROR {0}", ex.Message);
				return responseCode;
			}
		}
		public int SetJobFinish(int order_status, int order_id, int rating, string riview, string image)
		{
			int responseCode = 0;
			try
			{
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("order_id", order_id);
				j.Add("order_status", order_status);
				j.Add("rating", rating);
				j.Add("review", riview);
				j.Add("img1", image);
				j.Add("ext1", "png");

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				response = client.PostAsync(BaseURl + "job_finis.php", content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						var DataValues = jObj["responseCode"].ToString();
						responseCode = Convert.ToInt32(DataValues);
					}

				}
				return responseCode;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"ERROR {0}", ex.Message);
				return responseCode;
			}
			finally
			{

				StaticMethods.DismissLoader();
			}
		}
        public List<RidesModel.Orderdata> GetHistoryOfRides(int driver_id)
		{
			int responseCode = 0;
            List<RidesModel.Orderdata> ridesModel = null;
			try
			{
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("driver_id", driver_id); 
				

				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				response = client.PostAsync(BaseURl + "driver_rides.php", content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						var DataValues = jObj["responseCode"].ToString();
						responseCode = Convert.ToInt32(DataValues);
                        if(responseCode.Equals(200))
                        {
                            ridesModel = jObj["orderdata"].ToObject<List < RidesModel.Orderdata > >();
                        }
					}

				}
				
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"ERROR {0}", ex.Message);
				
			}
			finally
			{

				StaticMethods.DismissLoader();
			}
            return ridesModel;
		}
        public OrderDetailsModel.Orderdata GetOrderDetailsbyId(int order_id)
		{
			int responseCode = 0;
			OrderDetailsModel.Orderdata orderModel = null;
			try
			{
				HttpResponseMessage response = null;
				JObject j = new JObject();
				j.Add("Order_Id", order_id);


				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				response = client.PostAsync(BaseURl + "get_orderdetail.php", content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						var DataValues = jObj["responseCode"].ToString();
						responseCode = Convert.ToInt32(DataValues);
						if (responseCode.Equals(200))
						{
							orderModel = jObj["orderdata"].ToObject<OrderDetailsModel.Orderdata>();
						}
					}

				}

			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"ERROR {0}", ex.Message);

			}
			finally
			{

				StaticMethods.DismissLoader();
			}
			return orderModel;
		}
        public ProfileModel.Driverdetail GetDriverProfile()
		{
			int responseCode = 0;
			ProfileModel.Driverdetail profileModel = null;
			try
			{
				HttpResponseMessage response = null;
				JObject j = new JObject();
                j.Add("driver_id", StaticDataModel.UserId);


				var json = JsonConvert.SerializeObject(j);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				response = client.PostAsync(BaseURl + "driver_profile.php", content).Result;
				if (response.IsSuccessStatusCode)
				{

					using (StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
					{
						var contents = reader.ReadToEnd();
						JObject jObj = JObject.Parse(contents);
						var DataValues = jObj["responseCode"].ToString();
						responseCode = Convert.ToInt32(DataValues);
						if (responseCode.Equals(200))
						{
                            profileModel = jObj["driverdetail"].ToObject<ProfileModel.Driverdetail>();
						}
					}

				}

			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"ERROR {0}", ex.Message);

			}
			finally
			{

				StaticMethods.DismissLoader();
			}
			return profileModel;
		}
	}
}
