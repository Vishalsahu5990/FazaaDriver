using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamvvm;

namespace Fazaa
{
	public partial class HistoryofRidesPage : ContentPage
	{
		public HistoryofRidesPage()
		{
			InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            flowlistview.FlowColumnMinWidth = App.ScreenWidth;
		}
		public void menu_Tapped(object sender, EventArgs e)
		{
			StaticDataModel.currentContext.MenuTapped.Execute(StaticDataModel.currentContext.MenuTapped);
		}

        void Flowlistview_FlowItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as RidesModel.Orderdata;
            Navigation.PushPopupAsync(new Popup(Convert.ToInt32(item.Order_Id)));
        }

        protected async override  void OnAppearing()
		{
			base.OnAppearing();

            flowlistview.FlowItemTapped+= Flowlistview_FlowItemTapped;

            GetAllRides(StaticDataModel.UserId).Wait();
		}
		private async Task GetAllRides(int driver_id)
		{
            List<RidesModel.Orderdata> ridesModel = null;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{
                        WebService ws = new WebService();
                       ridesModel = ws.GetHistoryOfRides(driver_id);


					}).ContinueWith(async
                    t =>
                    {
                if (!ReferenceEquals(ridesModel,null))
						{
                    Device.BeginInvokeOnMainThread(() => 
                    {
                        for (int i = 0; i < ridesModel.Count;i++)
                        {
                            if(i%2==0)
                            {
                                ridesModel[i].Cell_Color = "#FFFFFF";
                            }
                            else
                            {
								ridesModel[i].Cell_Color = "#F0EFF0";
                            }
                        }
                        
                        flowlistview.FlowItemsSource = ridesModel;
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
