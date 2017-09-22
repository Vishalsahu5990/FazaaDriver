using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Fazaa
{
	public partial class MenuPage : ContentPage
	{
		public StackLayout Home { get; set; }
		public MenuPage()
		{
			InitializeComponent();
			Icon = "menu.png";
			Title = "menu"; // The Title property must be set.

			SetData();

		}
		private void SetData()
		{
			try
			{
				Device.BeginInvokeOnMainThread(async () =>
						{
							var model = StaticMethods.GetLocalSavedData();
							if (model.image != null)
								imgProfile.Source = ImageSource.FromUri(new System.Uri(model.image));
							lblUsername.Text = model.firstname + " " + model.lastname;
							lblEmail.Text = model.email;
							
						});

			}
			catch (Exception ex)
			{
				StaticMethods.ShowToast("Something went wrong!");
			}
		}


	}
}
