using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Fazaa
{
	public partial class ProductsPage : ContentPage
	{
		public ProductsPage()
		{
			InitializeComponent();
			ToolbarItems.Add(new ToolbarItem("Filter", "search.png", async () =>
{
	var page = new ContentPage();
	var result = await page.DisplayAlert("Title", "Message", "Accept", "Cancel");
	
}));
			ToolbarItems.Add(new ToolbarItem("Filter", "search.png", async () =>
{
	var page = new ContentPage();
	var result = await page.DisplayAlert("Title", "Message", "Accept", "Cancel");

}));
			ToolbarItems.Add(new ToolbarItem("Filter", "search.png", async () =>
{
	var page = new ContentPage();
	var result = await page.DisplayAlert("Title", "Message", "Accept", "Cancel");

}));

			lblCoffebakeries.Text = "Coffee & Bakeries";

			var slTabGroceries = new TapGestureRecognizer();
			stGroceries.GestureRecognizers.Add(slTabGroceries);
			slTabGroceries.Tapped += async (object sender, EventArgs e) =>
			{
				await Navigation.PushAsync(new CategoryPage());
			};

			string[] collection = new string[] { "one", "two", "three" };
			string[] images = {
				"https://farm9.staticflickr.com/8625/15806486058_7005d77438.jpg",
				"https://farm5.staticflickr.com/4011/4308181244_5ac3f8239b.jpg",
				"https://farm8.staticflickr.com/7423/8729135907_79599de8d8.jpg",
				"https://farm3.staticflickr.com/2475/4058009019_ecf305f546.jpg",
				"https://farm6.staticflickr.com/5117/14045101350_113edbe20b.jpg",
				"https://farm2.staticflickr.com/1227/1116750115_b66dc3830e.jpg",
				"https://farm8.staticflickr.com/7351/16355627795_204bf423e9.jpg",
				"https://farm1.staticflickr.com/44/117598011_250aa8ffb1.jpg",
				"https://farm8.staticflickr.com/7524/15620725287_3357e9db03.jpg",
				"https://farm9.staticflickr.com/8351/8299022203_de0cb894b0.jpg",
			};
			List<StoreModel> list = new List<StoreModel>();
			int number = 0;
			for (int n = 0; n < 20; n++)
			{
				for (int i = 0; i < images.Length; i++)
				{
					number++;
					var item = new StoreModel()
					{
						ImageUrl = images[i],
						Price = "Price:"+" "+"150Rs",
						Description="Ingredients in Cosmetics and Beauty"
					};

					list.Add(item);
				}
			}

			flowlistview.FlowItemsSource = list;
			if (flowlistview != null)
			{
				flowlistview.FlowItemTapped += (object sender, ItemTappedEventArgs e) =>
				 {
					var item = (DLToolkit.Forms.Controls.FlowListView)sender;

					var data=	item.FindByName<Image>("image");
					 data.IsVisible = false;
				 };
			}

		}
	}
}
