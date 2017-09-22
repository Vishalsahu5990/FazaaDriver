using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Fazaa
{
	public partial class ProductDetailsPage : ContentPage
	{
		public ProductDetailsPage()
		{
			InitializeComponent();
			ToolbarItems.Add(new ToolbarItem("Filter", "search.png", async () =>
	 {
		 //	var page = new ContentPage();
		 //var result = await page.DisplayAlert("Title", "Message", "Accept", "Cancel");

	 }));
			ToolbarItems.Add(new ToolbarItem("Filter", "search.png", async () =>
	 {
		 //	var page = new ContentPage();
		 //var result = await page.DisplayAlert("Title", "Message", "Accept", "Cancel");

	 }));
		}
	}
}
