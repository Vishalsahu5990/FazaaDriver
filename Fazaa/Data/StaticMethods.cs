using System;
using Xamarin.Forms;

namespace Fazaa
{
	public static class StaticMethods
	{
		public static void ShowToast(string msg)
		{
			try
			{
				if (Device.OS == TargetPlatform.Android)
				{
					DependencyService.Get<IAndroidMethods>().ShowToast(msg);
				}
				else if (Device.OS == TargetPlatform.iOS)
				{
					DependencyService.Get<IiOSMethods>().ShowToast(msg);
				}
			}
			catch (Exception ex)
			{

			}
		}
		public static void ShowLoader()
		{
			try
			{
				if (Device.OS == TargetPlatform.Android)
				{
					DependencyService.Get<IAndroidMethods>().ShowLoader();
				}
				else if (Device.OS == TargetPlatform.iOS)
				{
					DependencyService.Get<IiOSMethods>().ShowLoader();
				}
			}
			catch (Exception ex)
			{

			}
		}
		public static void DismissLoader()
		{
			try
			{
				if (Device.OS == TargetPlatform.Android)
				{
					DependencyService.Get<IAndroidMethods>().DismissLoader();
				}
				else if (Device.OS == TargetPlatform.iOS)
				{
					DependencyService.Get<IiOSMethods>().DismissLoader();
				}
			}
			catch (Exception ex)
			{ 
				 
			} 
		}
		public static  UserModel GetLocalSavedData()
		{
			UserModel um = null;
			try
			{
				if (Device.OS == TargetPlatform.iOS)
				{
					um = DependencyService.Get<IiOSMethods>().RetriveLocalData();
					return um;
				}
				else
				{
					um = DependencyService.Get<IAndroidMethods>().RetriveLocalData();
					return um;
				}
			}
			catch (Exception ex)
			{
				return um;
			}


		}
		public static void DeleteLocalSavedData()
		{
			UserModel um = null;
			try
			{
				if (Device.OS == TargetPlatform.iOS)
				{
					 DependencyService.Get<IiOSMethods>().DeleteLocalData();

				}
				else
				{
					 DependencyService.Get<IAndroidMethods>().DeleteLocalData();

				}
			}
			catch (Exception ex)
			{
				
			}


		}
	}
}
