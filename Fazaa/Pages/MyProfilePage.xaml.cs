using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fazaa
{
    public partial class MyProfilePage : ContentPage
    {
        public MyProfilePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
		public void menu_Tapped(object sender, EventArgs e)
		{
			StaticDataModel.currentContext.MenuTapped.Execute(StaticDataModel.currentContext.MenuTapped);
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetProfile().Wait();
        }

		private async Task GetProfile()
		{
			ProfileModel.Driverdetail profileModel = null;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{
						WebService ws = new WebService();
                profileModel = ws.GetDriverProfile();


					}).ContinueWith(async
					t =>
					{
						if (!ReferenceEquals(profileModel, null))
						{
							Device.BeginInvokeOnMainThread(() =>
							{
                                //Setting data to textview

                                lblDriverName.Text = profileModel.firstname + " " + profileModel.lastname;
                                lblEmail.Text = profileModel.email;
                                if(!string.IsNullOrEmpty(profileModel.DOB))
                                lblDateofBirth.Text = profileModel.DOB;
								if (!string.IsNullOrEmpty(profileModel.address))
                                lblAddress.Text = profileModel.address;
								if (!string.IsNullOrEmpty(profileModel.phonenumber))
                                lblContactNumber.Text = profileModel.phonenumber;
							});

						}
						else
						{
							//StaticMethods.ShowToast(_listStores.responseMessage);
						}


						StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
								   );
		}
    }
}
