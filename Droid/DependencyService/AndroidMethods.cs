using System;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Widget;
using Fazaa.Droid;
using PerpetualEngine.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidMethods))]
namespace Fazaa.Droid
{
	public class AndroidMethods : IAndroidMethods
	{
		private string Key = "fazza_driver";
		Dialog d;

		public void ShowToast(string msg)
		{
			Toast.MakeText(Forms.Context, msg, ToastLength.Short).Show();
		}
		public void ShowLoader()
		{
			d = new Dialog(Forms.Context);
			d.SetContentView(Resource.Layout.CustomProgressDialog);
			d.SetCancelable(false);
			d.Window.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Transparent));
			d.Show();

		}
		public void DismissLoader()
		{
			d.Dismiss();
		}



		public  void SaveLocalData(UserModel um)
		{
			try
			{
				//store

				var prefs = Android.App.Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);
				var storage = prefs.Edit();
				storage.PutString("driver_id", um.driver_id.ToString());
				storage.PutString("firstname", um.firstname);
				storage.PutString("lastname", um.lastname);
				storage.PutString("email", um.email);
				storage.PutString("DOB", um.DOB);
				storage.PutString("city", um.city);
				storage.PutString("phonenumber", um.phonenumber.ToString());
				storage.PutString("address", um.address);
				storage.PutString("device", um.device);
				storage.PutString("vehicle", um.vehicle);
				storage.PutString("image", um.image);

				storage.Commit();
			}
			catch (Exception ex)
			{

			}
		}

		public  void DeleteLocalData()
		{
			try
			{
				//store
				var prefs = Android.App.Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);
				var storage = prefs.Edit();
				storage.PutString("driver_id", "");
				storage.PutString("firstname", "");
				storage.PutString("lastname", "");
				storage.PutString("email", "");
				storage.PutString("DOB", "");
				storage.PutString("city", "");
				storage.PutString("phonenumber", "");
				storage.PutString("address", "");
				storage.PutString("device", "");
				storage.PutString("vehicle", "");
				storage.PutString("image", "");
				storage.Commit();
			}
			catch (Exception ex)
			{

			}
		}

		public  UserModel RetriveLocalData()
		{
			UserModel um = new UserModel();
			try
			{
				//retreive 
				var storage = Android.App.Application.Context.GetSharedPreferences("MyApp", FileCreationMode.Private);

				um.driver_id = Convert.ToInt32(storage.GetString("driver_id", null));
				um.firstname = storage.GetString("firstname", null);
				um.lastname = storage.GetString("lastname", null);
				um.email = storage.GetString("email", null);
				um.DOB = storage.GetString("DOB", null);
				um.city = storage.GetString("city", null);
				um.phonenumber = Convert.ToInt32(storage.GetString("phonenumber", null));
				um.address = storage.GetString("address", null);
				um.device = storage.GetString("device", null);
				um.vehicle = storage.GetString("vehicle", null);
				um.image = storage.GetString("image",null);
				return um;
			}
			catch (Exception ex)
			{
				return um;
			}

		}

	}
}



