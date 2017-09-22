using System;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using PushNotification.Plugin;
using PushNotification.Plugin.Abstractions;
using Xamarin.Forms;
namespace Fazaa
{
      public class CrossPushNotificationListener : IPushNotificationListener
	{
		//Here you will receive all push notification messages
		//Messages arrives as a dictionary, the device type is also sent in order to check specific keys correctly depending on the platform.
		void IPushNotificationListener.OnMessage(JObject parameters, DeviceType deviceType)
		{
			Debug.WriteLine("Message Arrived");
			var model = StaticDataModel.model;
			if (model != null)
			{
				StaticDataModel.IsfromNotificationTap = true;
				AutoLogin();
             //App.Current.MainPage = new NavigationPage(new NewOrderPage());
			}
				
		}
		//Gets the registration token after push registration
		void IPushNotificationListener.OnRegistered(string Token, DeviceType deviceType)
		{
			StaticDataModel.DeviceToken = Token;
			Debug.WriteLine(string.Format("Push Notification - Device Registered - Token : {0}", Token));
		}
		//Fires when device is unregistered
		void IPushNotificationListener.OnUnregistered(DeviceType deviceType)
		{
			Debug.WriteLine("Push Notification - Device Unnregistered");

		}

		//Fires when error
		void IPushNotificationListener.OnError(string message, DeviceType deviceType)
		{
			Debug.WriteLine(string.Format("Push notification error - {0}", message));
		}

		//Enable/Disable Showing the notification
		bool IPushNotificationListener.ShouldShowNotification()
		{
			return true;
		}
private void AutoLogin()
{
	if (Device.OS == TargetPlatform.iOS)
	{
		StaticDataModel.DeviceType = "ios";
		var data = DependencyService.Get<IiOSMethods>().RetriveLocalData();
		if (data.driver_id == 0)
		{

App.Current.MainPage = new NavigationPage(new Fazaa.Login());
		}
		else
		{
			StaticDataModel.UserId = data.driver_id;
			if (StaticDataModel.IsfromNotificationTap)
				Application.Current.MainPage = new NewOrderPage();
			else
App.Current.MainPage = new NavigationPage(new RootPage());
			//Application.Current.MainPage = new RootPage();
		}
	}
	else
	{
		StaticDataModel.DeviceType = "android";
		var data = DependencyService.Get<IAndroidMethods>().RetriveLocalData();


		if (data.driver_id == 0)
		{

App.Current.MainPage = new NavigationPage(new Fazaa.Login());
		}
		else
		{
			StaticDataModel.UserId = data.driver_id;
			if (StaticDataModel.IsfromNotificationTap)
				Application.Current.MainPage = new NewOrderPage();
			else
App.Current.MainPage = new NavigationPage(new RootPage());
			//Application.Current.MainPage = new RootPage();
		}
	}
	
		}

	}
}
