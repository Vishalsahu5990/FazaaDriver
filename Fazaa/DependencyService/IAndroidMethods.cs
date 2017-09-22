using System;
namespace Fazaa
{
	public interface IAndroidMethods
	{
		void ShowToast(string ToastMsg);
		void ShowLoader();
		void DismissLoader();
		UserModel RetriveLocalData();
		void SaveLocalData(UserModel um);
		void DeleteLocalData();
	}
}
