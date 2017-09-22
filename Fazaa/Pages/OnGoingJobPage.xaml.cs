using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Fazaa
{
	public partial class OnGoingJobPage : ContentPage
	{
		int order_id = 0;
		private CustomMap _map;
		OrderModel ordermodel = null;
		Geocoder geoCoder;
		double lat = 0;
		double lng = 0;
		public static int CurrentOrderStatus = 0;
		List<StoreModel> storemodel = new List<StoreModel>();
		public  OnGoingJobPage(int _orderid)
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			btnViewOrderSummary.Clicked+= BtnViewOrderSummary_Clicked;
			geoCoder = new Geocoder();

			order_id = _orderid;

			PrepareUI();
			OnLoad();
			GetOrderDetails();
		}
		protected async override void OnAppearing()
		{
			base.OnAppearing();
			var OnStore = new TapGestureRecognizer
			{
				//First
				Command = new Command(() =>
				{
					
					Device.BeginInvokeOnMainThread(async () =>
					{
						
					});

				})
			};

			ViewOnStore.GestureRecognizers.Add(OnStore);
			var InStore = new TapGestureRecognizer
			{
				//Second
				Command = new Command(() =>
				{
					if (CurrentOrderStatus == 0)
					{
						Device.BeginInvokeOnMainThread(async () =>
						{
							var result = await this.DisplayAlert("Alert!", "Are you confirm to change status?", "Yes", "No");
							if (result)
							{

								UpdateOrderStatus(1);
							}
							else
							{

							}
						});
					}
					else
					{
						StaticMethods.ShowToast("You cannot change status.");
					}
				})
			};

			ViewInStore.GestureRecognizers.Add(InStore);


			var OnWay = new TapGestureRecognizer
			{
				//Third
				Command = new Command(() =>
				{
					if (CurrentOrderStatus>0)
					{
						Device.BeginInvokeOnMainThread(async () =>
						{
							var result = await this.DisplayAlert("Alert!", "Are you confirm to change status?", "Yes", "No");
							if (result)
							{
								UpdateOrderStatus(2);
							}
							else
							{

							}
						});
					}
					else
					{
						StaticMethods.ShowToast("You cannot change status.");
					}
				})
			};

			ViewOnWay.GestureRecognizers.Add(OnWay);
			btnFinish.Clicked += (sender, e) =>
			 {
				
				 Application.Current.MainPage = new JobFinishPage();
				 //SignaturePopupPage s = new SignaturePopupPage();
					//Navigation.PushPopupAsync(s);
				 //if (CurrentOrderStatus == 2)
					// UpdateOrderStatus(3);
				 //else
					// StaticMethods.ShowToast("Please change order status first.");


			};
		}
		private void PrepareUI()
		{ 
		try
			{
				var circleSize=App.ScreenWidth / 4;

				ViewOnStore.HeightRequest = circleSize;
				ViewOnStore.WidthRequest = circleSize;

				ViewInStore.HeightRequest = circleSize;
				ViewInStore.WidthRequest = circleSize;

				ViewOnWay.HeightRequest = circleSize;
				ViewOnWay.WidthRequest = circleSize;
			}
			catch (Exception ex)
			{

			}
		}
		private void CreateMap()
		{
			try
			{
				_map = new CustomMap(this)
				{
					MapType = MapType.Street,
					VerticalOptions = LayoutOptions.FillAndExpand
				};
				stlMap.Children.Add(_map);
				AddBarMarkerOnMap(25.023176,39.189978);
			}
			catch (Exception ex)
			{

			}
		}
		private async Task OnLoad()
		{

			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{
						CreateMap();

					}).ContinueWith(
					t =>
					{



					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
		private async Task GetOrderDetails()
		{
			//StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{
						WebService wc = new WebService();
				ordermodel=  wc.GetOrderDetails(order_id);

					}).ContinueWith(
					t =>
					{
						if (ordermodel != null)
						{
							lblName.Text = ordermodel.destination_Name;
					        lblDestinationLocation.Text = ordermodel.destination_Address;
							CurrentOrderStatus = ordermodel.Status;
							SetCurrentStatus(ordermodel.Status);
							GetLatLongFromAddress(ordermodel.destination_Address);
				        }

				//StaticMethods.DismissLoader();
					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}

		private async void GetLatLongFromAddress(string address)
		{ 
		try
			{
				var approximateLocations = await geoCoder.GetPositionsForAddressAsync(address);
				foreach (var position in approximateLocations)
				{
					lat = position.Latitude;
					lng = position.Longitude;
					Debug.WriteLine(position.Latitude + ", " + position.Longitude + "\n".ToString());
				}
				Device.BeginInvokeOnMainThread(async () =>
						{
					if (lat!=0)
							{
								var model = new StoreModel();
								model.lat = StaticDataModel.Lattitude;
						        model.lng = StaticDataModel.Longitude;
						        storemodel.Add(model);

						       var model1 = new StoreModel();
						        model1.lat = lat;
						        model1.lng = lng;
								storemodel.Add(model1);
								AddBarMarkerOnMap(storemodel);
								//AddBarMarkerOnMap(lat, lng);
								
							}
							else
							{
						StaticMethods.ShowToast("Unable to get store location.");
							}
						});

			}
			catch (Exception ex)
			{

			}
		}
		private void SetCurrentStatus(int status)
		{ 
		try
			{
				switch(status)
				{
					    case 0:
						ViewOnStore.Color = Color.FromHex("#56B352");
						CurrentOrderStatus = 0;
						break;
						
						case 1:
						ViewInStore.Color = Color.FromHex("#56B352");
						B1.BackgroundColor = Color.FromHex("#56B352");
						CurrentOrderStatus = 1;
						break;
						
						case 2:
						ViewInStore.Color = Color.FromHex("#56B352");
						B1.BackgroundColor = Color.FromHex("#56B352");
						ViewOnWay.Color = Color.FromHex("#56B352");
						B2.BackgroundColor = Color.FromHex("#56B352");
						CurrentOrderStatus = 2;
						break;
				}
			}
			catch (Exception ex)
			{

			}
		}
		private async Task UpdateOrderStatus(int order_status)
		{
			int responseCode = 0;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{
						WebService wc = new WebService();
				responseCode = wc.UpdateOrderStatus(order_status,order_id);

					}).ContinueWith(
					t =>
					{
				if (responseCode != 0)
						{
							SetCurrentStatus(order_status);
							if (order_status == 3)
							{ 
					         Application.Current.MainPage = new RootPage();
					        }
					        
						}

						StaticMethods.DismissLoader();
					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
		private void AddBarMarkerOnMap(double lat,double lng)
		{
			_map.Pins.Clear();
			try
			{
				var pin = new CustomPin
				{
					Pin = new Pin
					{
						Type = PinType.Place,
						Position = new Xamarin.Forms.Maps.Position(lat, lng),
						Label = "Path",
						Address = Guid.NewGuid().ToString(),
					},
					Id = "Xamarin",

					//Url = BarDetails.url

				};
				HomePage.staticcustomPins = 0;
				_map.CustomPins = new List<CustomPin> { pin };
				_map.Pins.Add(pin.Pin);
				_map.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(lat, lng), Distance.FromMiles(0.002)));

			}
			catch (Exception ex)
			{

			}

		}
		private void AddBarMarkerOnMap(List<StoreModel> _storemodel)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				if (_map.Pins != null)
					_map.Pins.Clear();
				CustomPin pin = null;
				List<CustomPin> customPins = new List<CustomPin>();
				try
				{
					for (int i = 0; i < _storemodel.Count; i++)
					{
						pin = new CustomPin
						{
							Pin = new Pin
							{
								Type = PinType.Place,
								Position = new Xamarin.Forms.Maps.Position(_storemodel[i].lat, _storemodel[i].lng),
								Label = "Path",
								Address = Guid.NewGuid().ToString(),
							},
							Id = "Xamarin",

						};
						customPins.Add(pin);
					}
					HomePage.staticcustomPins = 0;
					_map.CustomPins = new List<CustomPin> { pin };
					foreach (var Pin in customPins)
					{
						_map.Pins.Add(Pin.Pin);
					}
					_map.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(lat, lng), Distance.FromMiles(3)));

				}
				catch (Exception ex)
				{

				}
			});


		}

		void BtnViewOrderSummary_Clicked(object sender, EventArgs e)
		{
			Popup s = new Popup(order_id);
            Navigation.PushPopupAsync(s);
		}
	}
}
