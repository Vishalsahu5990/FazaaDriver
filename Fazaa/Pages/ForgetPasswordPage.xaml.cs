using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fazaa
{
	public partial class ForgetPasswordPage : ContentPage
	{
		public ForgetPasswordPage()
		{
			InitializeComponent();
		}

        void BtnSumit_Clicked(object sender, EventArgs e)
        {
			
            if (!string.IsNullOrEmpty(txtEmail.Text))
			{
				if (!Regex.Match(txtEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
				{
					StaticMethods.ShowToast("Incorrect Email.");
				}
				else
				{
                    ForgotPassword().Wait();

				}
			}
			else
			{
				StaticMethods.ShowToast("Email required.");
			}
        }

        protected override void OnAppearing()
		{
			base.OnAppearing();
            btnSumit.Clicked+= BtnSumit_Clicked;
			
		}
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            btnSumit.Clicked -= BtnSumit_Clicked;
        }
		private async Task ForgotPassword()
		{
            int ret = 0;
            StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{
						WebService wc = new WebService();
						ret= wc.ForgotPassword(txtEmail.Text);

					}).ContinueWith(
					t =>
					{
						Device.BeginInvokeOnMainThread(() =>
						{

							if (ret.Equals(200))
								Navigation.PopAsync();
							else
								StaticMethods.ShowToast("Something went wrong, please try again later!");

						});
                      

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
	}
}
