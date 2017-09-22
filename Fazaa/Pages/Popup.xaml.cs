using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Fazaa
{
	public partial class Popup : PopupPage
	{
        int _OrderId = 0;
		public Popup(int orderId)
		{
			InitializeComponent();
            _OrderId = orderId;
			listView.ItemsSource = Enumerable.Range(0,3);
		}
		public Popup()
		{
			InitializeComponent();
			
			listView.ItemsSource = Enumerable.Range(0, 3);
		}
		private async void OnClose(object sender, EventArgs e)
		{
			await Navigation.PopPopupAsync();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetOrderDetails(_OrderId).Wait();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
		private async Task GetOrderDetails(int order_id)
		{
            OrderDetailsModel.Orderdata orderModel = null;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{
						WebService ws = new WebService();
                orderModel = ws.GetOrderDetailsbyId(order_id);


					}).ContinueWith(async
					t =>
					{
						if (!ReferenceEquals(orderModel, null))
						{
							Device.BeginInvokeOnMainThread(() =>
							{

                                lblsource.Text = orderModel.source_address;
                                lblDestination.Text = orderModel.destination_address;
                        for (int i = 0; i < orderModel.product_detail.Count;i++)
                        {
                            orderModel.product_detail[i].quantity = "Quantity :"+orderModel.product_detail[i].quantity;
                            orderModel.product_detail[i].Category_Name = "Category :" + orderModel.product_detail[i].Category_Name;
                        }
                                listView.ItemsSource = orderModel.product_detail;
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
