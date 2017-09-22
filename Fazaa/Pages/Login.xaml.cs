using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fazaa;
using Plugin.Geolocator;
using Plugin.Toasts;
using Xamarin.Forms;
using Plugin.Geolocator.Abstractions;
using System.Diagnostics;

namespace Fazaa
{
	public partial class Login : ContentPage
	{
		UserModel usermodel = null;
		IGeolocator locator;
		public Login()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			txtEmail.Text = "developer.farkya@gmail.com";
			txtPassword.Text = "123456";
			var ForgotPassword = new TapGestureRecognizer();
			lblForgotPassword.GestureRecognizers.Add(ForgotPassword);
			ForgotPassword.Tapped += (sender, e) =>
			 {
				 Navigation.PushAsync(new ForgetPasswordPage());
			 };

			btnSignIn.Clicked += (object sender, EventArgs e) =>
			{


				if (IsValidate())
				{
					if (!Regex.Match(txtEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
					{
						StaticMethods.ShowToast("Incorrect Email.");
					}
					else
					{
						//WebService wc = new WebService();
						//wc.Login(txtEmail.Text, txtPassword.Text, StaticDataModel.DeviceToken);
						LoginProcess();
					}

				}
				else
				{
					StaticMethods.ShowToast("All fields are required.");
				}


			};
		}

	
		private async Task LoginProcess()
		{ 
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{
						WebService wc = new WebService();
					usermodel=	 wc.Login(txtEmail.Text, txtPassword.Text, StaticDataModel.DeviceToken);

					}).ContinueWith(
					t =>
					{

				if (usermodel.driver_id > 0)
						{
                    DisplayAlert("alert!",StaticDataModel.DeviceToken,"Ok");
							SaveDataForAutoLogin(usermodel);
					        Application.Current.MainPage = new RootPage();
					        StaticMethods.DismissLoader();
					    }
						else

							StaticMethods.ShowToast("Login Failed.");

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}

		private void SaveDataForAutoLogin(UserModel um)
		{
			if (Device.OS == TargetPlatform.iOS)
			{
				DependencyService.Get<IiOSMethods>().SaveLocalData(um);

			}
			else
			{
				 DependencyService.Get<IAndroidMethods>().SaveLocalData(um);

			}

		}

		private bool IsValidate()
		{
			if (txtEmail.Text == string.Empty || txtEmail.Text == null)
			{

				return false;

			}
			else if (txtPassword.Text == string.Empty || txtPassword.Text == null)
			{


				return false;

			}

			else
			{
				return true;
			}
		}
		public  void Nav()
		{
			Navigation.PushAsync(new NewOrderPage());
		}
	}
}
