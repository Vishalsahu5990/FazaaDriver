using System;
namespace Fazaa
{
	public static class StaticDataModel
	{
        public static RootPage currentContext = null;
		public static NotificationModel model = null;
		public static int UserId = 0;
        public static bool IsfromRejectOrder = false;
		public static bool IsfromNavigationMenu = false;
		public static bool IsfromNotificationTap = false;
		public static string UserName = string.Empty;
		public static string ProfilePic = string.Empty;
		public static string DeviceToken = string.Empty;
		public static string DeviceType = string.Empty;
		public static double Lattitude = 0;
		public static double Longitude = 0;
	}
}
