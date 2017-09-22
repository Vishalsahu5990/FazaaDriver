using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Fazaa
{
	public partial class PaymentPage : ContentPage
	{
		public PaymentPage()
		{
			InitializeComponent();
			ToolbarItems.Add(new ToolbarItem("Filter", "right_logo.png", async () =>
	 {
			 //	var page = new ContentPage();
			 //var result = await page.DisplayAlert("Title", "Message", "Accept", "Cancel");

		 }));
		}
	}
}
