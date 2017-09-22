using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using Newtonsoft.Json.Linq;
using Plugin.Toasts;
using UIKit;
using Xamarin;
using Xamarin.Forms;
using Newtonsoft.Json;
using XFShapeView.iOS;
using PushNotification.Plugin;

namespace Fazaa.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public static NotificationModel model = null;
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{

			//Consider inizializing before application initialization, if using any CrossPushNotification method during application initialization.
			CrossPushNotification.Initialize<CrossPushNotificationListener>();


			global::Xamarin.Forms.Forms.Init();
			ImageCircleRenderer.Init();
			FormsMaps.Init();
			FFImageLoading.Forms.Touch.CachedImageRenderer.Init();
			XFShapeView.iOS.Renderers.Init();
			App.ScreenHeight = (double)UIScreen.MainScreen.Bounds.Height;
			App.ScreenWidth = (double)UIScreen.MainScreen.Bounds.Width;
			SetupTitleBar();
			LoadApplication(new App());
			return base.FinishedLaunching(app, options);
		}
		private void SetupTitleBar()
		{
			UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(0.341f, 0.702f, 0.322f); //bar background
			UINavigationBar.Appearance.TintColor = UIColor.Black; //Tint color of button items
			UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes()
			{
				TextColor = UIColor.White
			});
		}
		private void PrepareRemoteNotification()
		{
			try
			{
				if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
				{
					var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
						UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
						new NSSet());

					UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
					UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
					UIApplication.SharedApplication.RegisterForRemoteNotifications();
				}
				else
				{
					UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
					UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);

				}
			}
			catch (Exception ex)
			{

			}
		}
		//public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		//{
		//	// Get current device token
		//	var DeviceToken = deviceToken.Description;
		//	if (!string.IsNullOrWhiteSpace(DeviceToken))
		//	{
		//		DeviceToken = DeviceToken.Trim('<').Trim('>');
		//		DeviceToken = DeviceToken.Replace(" ", "");
		//		StaticDataModel.DeviceToken = DeviceToken.ToString();
		//		Console.WriteLine(DeviceToken);
		//	}

		//	// Get previous device token
		//	var oldDeviceToken = NSUserDefaults.StandardUserDefaults.StringForKey("PushDeviceToken");

		//	// Has the token changed?
		//	if (string.IsNullOrEmpty(oldDeviceToken) || !oldDeviceToken.Equals(DeviceToken))
		//	{
		//		//TODO: Put your own logic here to notify your server that the device token has changed/been created!
		//	}

		//	// Save new device token 
		//	NSUserDefaults.StandardUserDefaults.SetString(DeviceToken, "PushDeviceToken");
		//}

		public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
		{
			if (CrossPushNotification.Current is IPushNotificationHandler)
			{
				((IPushNotificationHandler)CrossPushNotification.Current).OnErrorReceived(error);
			}
		}

		public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary options, Action<UIBackgroundFetchResult> completionHandler)
		{
			if (CrossPushNotification.Current is IPushNotificationHandler)
			{
				processNotification(options, false);
				((IPushNotificationHandler)CrossPushNotification.Current).OnMessageReceived(options);
			}
			//StaticDataModel.IsfromNotificationTap = true;
			//processNotification(options, false);

		}
		
		private void processNotification(NSDictionary options, bool fromFinishedLaunching)
		{
			try
			{
				if (null != options && options.ContainsKey(new NSString("aps")))
				{

					NSDictionary aps = options.ObjectForKey(new NSString("aps")) as NSDictionary;

					string alert = string.Empty;
					string sound = string.Empty;
					int badge = -1;

					if (aps.ContainsKey(new NSString("moredata")))
					{
						StaticDataModel.model = new NotificationModel();
						NSDictionary json = (aps["moredata"] as NSDictionary);

						StaticDataModel.model.customer_name = json["customer_name"].ToString();
						StaticDataModel.model.destination = json["destination"].ToString();
						StaticDataModel.model.driver_id = json["driver_id"].ToString();
						StaticDataModel.model.notificationtype = json["notificationtype"].ToString();
						StaticDataModel.model.order_id = json["order_id"].ToString();
						StaticDataModel.model.order_price = json["order_price"].ToString();
						StaticDataModel.model.store_location = json["store_location"].ToString();
						StaticDataModel.model.storename = json["storename"].ToString();

					}

				}
			}
			catch (Exception ex)
			{

			}
		}
		public override void DidEnterBackground(UIApplication application)
		{

			WebService wc = new WebService();
			wc.UpdateLocation();

			var taskID = UIApplication.SharedApplication.BeginBackgroundTask(null);
			Task.Factory.StartNew(() => FinishLongRunningTask(taskID));
		}
		void FinishLongRunningTask(nint taskID)
		{

			Thread.Sleep(25000);
			DidEnterBackground(null);

		}
		public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		{
			if (CrossPushNotification.Current is IPushNotificationHandler)
			{
				((IPushNotificationHandler)CrossPushNotification.Current).OnRegisteredSuccess(deviceToken);
			}
		}

		public override void DidRegisterUserNotificationSettings(UIApplication application, UIUserNotificationSettings notificationSettings)
		{
			application.RegisterForRemoteNotifications();
		}



		public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
		{
			if (CrossPushNotification.Current is IPushNotificationHandler)
			{
				((IPushNotificationHandler)CrossPushNotification.Current).OnMessageReceived(userInfo);
			}
		}

	}
}
