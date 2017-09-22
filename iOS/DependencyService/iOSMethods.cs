using System;
using Fazaa;
using Xamarin.Forms;
using BigTed;
using Fazaa.iOS;
using PerpetualEngine.Storage;

[assembly: Dependency(typeof(iOSMethods))]
namespace Fazaa.iOS
{
	public class iOSMethods : IiOSMethods
	{
		private string Key = "fazza_driver";

		public void ShowToast(string msg)
		{
			BTProgressHUD.ShowToast(msg, false, 1000);
		}
		public void ShowLoader()
		{
			BTProgressHUD.Show();
		}
		public void DismissLoader()
		{
			BTProgressHUD.Dismiss();
		}
		public void SaveLocalData(UserModel um)
		{
			try
			{
				var storage = SimpleStorage.EditGroup(Key);
				storage.Put("driver_id", um.driver_id.ToString());
				storage.Put("firstname", um.firstname);
				storage.Put("lastname", um.lastname);
				storage.Put("email", um.email);
				storage.Put("DOB", um.DOB);
				storage.Put("city", um.city);
				storage.Put("phonenumber", um.phonenumber.ToString());
				storage.Put("address", um.address);
				storage.Put("device", um.device);
				storage.Put("vehicle", um.vehicle);
				storage.Put("image", um.image);
			}
			catch (Exception ex)
			{

			}
		}
		public UserModel RetriveLocalData()
		{
			UserModel um = new UserModel();
			try
			{
				var storage = SimpleStorage.EditGroup(Key);
				string id=storage.Get("driver_id");
				um.driver_id = Convert.ToInt32(id);
				um.firstname = storage.Get("firstname");
				um.lastname = storage.Get("lastname");
				um.email = storage.Get("email");
				um.DOB = storage.Get("DOB");
				um.city = storage.Get("city");
				um.phonenumber = Convert.ToInt32(storage.Get("phonenumber"));
				um.address = storage.Get("address");
				um.device = storage.Get("device");
				um.vehicle = storage.Get("vehicle");
				um.image = storage.Get("image");
				return um;
			}
			catch (Exception ex)
			{
				return um;
			}
		}
		public void DeleteLocalData()
		{
			string values = string.Empty;
			try
			{

				var storage = SimpleStorage.EditGroup(Key);
				storage.Delete(Key);

			}
			catch (Exception ex)
			{

			}
		}
	}
}
