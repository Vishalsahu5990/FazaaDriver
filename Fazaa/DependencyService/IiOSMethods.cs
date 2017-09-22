using System;
namespace Fazaa
{
	public interface IiOSMethods
	{
		void ShowToast(string ToastMsg);
		void ShowLoader();
		void DismissLoader();
		UserModel RetriveLocalData();
		void SaveLocalData(UserModel um);
		void DeleteLocalData();
	}
}
