using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Fazaa
{
	public partial class MyOrderPage : ContentPage
	{
		public MyOrderPage()
		{
			ToolbarItems.Add(new ToolbarItem("Filter", "right_logo.png", async () =>
         {
         //	var page = new ContentPage();
	        //var result = await page.DisplayAlert("Title", "Message", "Accept", "Cancel");

         }));
			GetOrderList();
		}
		private void GetOrderList()
		{ 
		try
			{
				InitializeComponent();
				List<string> source = new List<string>();
				for (int i = 0; i < 20; i++)
				{
					source.Add("item");
				}
				listView.ItemsSource = source;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}
	}
}
