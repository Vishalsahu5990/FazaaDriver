using System;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using PushNotification.Plugin;
using Xamarin.Forms;

namespace Fazaa
{
	public partial class App : Application
	{
		public static double ScreenHeight;
		public static double ScreenWidth;
		public static RootPage rootpage { get; set; }
		public static IGeolocator locator;
		public App()
		{
			InitializeComponent();
			
                locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
          
			AutoLogin();

           // MainPage = new NavigationPage(new Fazaa.MyProfilePage()); 
			//MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#57B352"));
			//MainPage.SetValue(NavigationPage.BarTextColorProperty, Color.White); 
		}

		protected override void OnStart()
		{

			// Handle when your app starts 
			if(Device.OS==TargetPlatform.iOS)
			CrossPushNotification.Current.Register();
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps  
		}
		protected override void OnResume()
		{
			// Handle when your app resumes
			 //AutoLogin();
		}

		private  void AutoLogin()
		{
			if (Device.OS == TargetPlatform.iOS)
			{
				StaticDataModel.DeviceType = "ios";
				var data = DependencyService.Get<IiOSMethods>().RetriveLocalData();
				if (data.driver_id==0 )
				{
					
					MainPage = new NavigationPage(new Fazaa.Login());
				}
				else
				{
					StaticDataModel.UserId = data.driver_id;
					if (StaticDataModel.IsfromNotificationTap)
						Application.Current.MainPage = new NewOrderPage();
					else
						MainPage = new RootPage();
					//Application.Current.MainPage = new RootPage();
				}
			}
			else
			{ 
				StaticDataModel.DeviceType = "android";
				var data = DependencyService.Get<IAndroidMethods>().RetriveLocalData();


				if (data.driver_id == 0)
				{

					MainPage = new NavigationPage(new Fazaa.Login());
				}
				else
				{
					StaticDataModel.UserId = data.driver_id;
					if (StaticDataModel.IsfromNotificationTap)
						Application.Current.MainPage = new NewOrderPage();
					else
						MainPage = new RootPage();
					//Application.Current.MainPage = new RootPage();
				}
			}
			StaticDataModel.IsfromNotificationTap = false;
		}


	}
}
