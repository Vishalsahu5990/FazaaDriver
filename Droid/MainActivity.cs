using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin;
using Plugin.Toasts;
using Xamarin.Forms;
using Android.Gms.Common;
using Firebase;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;
using Android.Gms.Tasks;

namespace Fazaa.Droid
{
    [Activity(Label = "FazaaDriver.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;
          
			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
			FormsMaps.Init(this, bundle);
			GetDeviceSize();
            //Should specify android Sender Id as parameter 
            //CrossPushNotification.Initialize<CrossPushNotificationListener>("<ANDROID SENDER ID>");
          

           
            IsPlayServicesAvailable();
           
			LoadApplication(new App());
		}
		private void GetDeviceSize()
		{
			try
			{
				var pixels = Resources.DisplayMetrics.WidthPixels; // real pixels
				var scale = Resources.DisplayMetrics.Density;
				int dps = (int)((pixels - 0.5f) / scale);

				App.ScreenWidth = dps;

				pixels = Resources.DisplayMetrics.HeightPixels; // real pixels
				scale = Resources.DisplayMetrics.Density;
				dps = (int)((pixels - 0.5f) / scale);

				App.ScreenHeight = dps; // real pixel
			}
			catch (Exception ex)
			{

			}
		}
		public bool IsPlayServicesAvailable()
		{
			try
			{
				int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
				if (resultCode != ConnectionResult.Success)
				{

					if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
					{
						var code = GoogleApiAvailability.Instance.GetErrorString(resultCode);
					}
					else
					{
						Console.WriteLine("This device is not supported");

						Finish();
					}
					return false;
				}
				else
				{ 
                    //// FirebaseCode Send Single Device  with token
                    //System.Threading.Tasks.Task.Run(() =>
                    //{
                    //    var instanceid = FirebaseInstanceId.Instance;
                    //    instanceid.DeleteInstanceId();
                    //    Log.Debug("TAG", "{0} {1}", instanceid.Token, instanceid.GetToken("1087703758123",
                    //        Firebase.Messaging.FirebaseMessaging.InstanceIdScope));
                    //    StaticDataModel.DeviceToken = FirebaseInstanceId.Instance.Token;
                    //});




					System.Threading.Tasks.Task.Run(() =>
							   {
                                    // FirebaseApp.InitializeApp(this);
								   var instanceid = FirebaseInstanceId.Instance;
								   instanceid.DeleteInstanceId();
			   //Log.Debug("TAG", "{0} {1}", instanceid.Token, instanceid.GetToken(this.GetString(Resource.String.gcm_defaultSenderId), Firebase.Messaging.FirebaseMessaging.InstanceIdScope));
			   StaticDataModel.DeviceToken = FirebaseInstanceId.Instance.Token;
								   Console.WriteLine("Google Play Services is available.");
								  
							   });
					return true;
				}
			}
			catch (Exception ex)
			{
                StaticMethods.ShowToast(ex.Message);
				return false;
			}
		}
	}
}
