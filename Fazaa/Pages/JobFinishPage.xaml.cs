using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fazaa
{
	public partial class JobFinishPage : ContentPage
	{
		private static int rating = 0;
		private string imageData = string.Empty;
		public JobFinishPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			btnFinish.Clicked+= BtnFinish_Clicked;
		try
			{
				lblCustomerName.Text = StaticDataModel.model.customer_name;
			}
			catch (Exception ex)
			{

			}

		}

async void One_Tapped(object sender, System.EventArgs e)
		{
			rating = 1;
				imgOne.Source = "green";

			imgTwo.Source = "black";
			imgThree.Source = "black";
                imgFour.Source = "black";
                imgFive.Source = "black";
			
}
async void Two_Tapped(object sender, System.EventArgs e)
{
			rating = 2;
				imgOne.Source = "green";
				imgTwo.Source = "green";

			 imgThree.Source = "black";
                imgFour.Source = "black";
                imgFive.Source = "black";

}
async void Three_Tapped(object sender, System.EventArgs e)
{
			rating = 3;
				imgOne.Source = "green";
				imgTwo.Source = "green";
				imgThree.Source = "green";

				 imgFour.Source = "black";
                imgFive.Source = "black";

	}
async void Four_Tapped(object sender, System.EventArgs e)
{
			rating = 4;
				imgOne.Source = "green";
				imgTwo.Source = "green";
				imgThree.Source = "green";
				imgFour.Source = "green";

			 imgFive.Source = "black";

	}
async void Five_Tapped(object sender, System.EventArgs e)
{
			rating = 5;
			
				imgOne.Source = "green";
				imgTwo.Source = "green";
				imgThree.Source = "green";
				imgFour.Source = "green";
				imgFive.Source = "green";
		
	}

		void BtnFinish_Clicked(object sender, EventArgs e)
		{
			try
			{
var data = padView.GetImage(Acr.XamForms.SignaturePad.ImageFormatType.Png);
Byte[] inArray = new Byte[(int)data.Length];
imageData = Convert.ToBase64String(inArray);

				FinishJobProcess();
			}
			catch (Exception ex)
			{

			}
		}
private async Task FinishJobProcess()
{
			StaticMethods.ShowLoader();
			try
			{
int responseCode = 0;
//StaticMethods.ShowLoader();
Task.Factory.StartNew(
			// tasks allow you to use the lambda syntax to pass wor
			() =>
			{
				WebService wc = new WebService();
responseCode = wc.SetJobFinish(3,Convert.ToInt32(StaticDataModel.model.order_id),rating,
				                               txtReview.Text,imageData);

			}).ContinueWith(
			t =>
			{
	if (responseCode == 200)
	{
					StaticDataModel.IsfromRejectOrder = true;
		Application.Current.MainPage = new RootPage();
	}
	else
	{
		StaticMethods.ShowToast("Unable to Finish Job, Please try again later!");
	}

	StaticMethods.DismissLoader();
}, TaskScheduler.FromCurrentSynchronizationContext()
		);
			}
			catch (Exception ex)
			{

			}
		}
	}
}
