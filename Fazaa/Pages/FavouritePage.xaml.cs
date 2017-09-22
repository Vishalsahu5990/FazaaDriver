using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Fazaa
{
	public partial class FavouritePage : ContentPage
	{
		public FavouritePage()
		{
			ToolbarItems.Add(new ToolbarItem("Filter", "right_logo.png", async () =>
		 {
			 //	var page = new ContentPage();
			 //var result = await page.DisplayAlert("Title", "Message", "Accept", "Cancel");

		 }));
			GetOrderList();

		}

		public void btnRemove(object sender,EventArgs e)
		{ 
			// var item = (Xamarin.Forms.Button)sender;
			//var listitem = (from itm in allItems
			//				 where itm.ItemName == item.CommandParameter.ToString()
			//				 select itm)
			//				.FirstOrDefault<Item>();
			//allItems.Remove(listitem);
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
