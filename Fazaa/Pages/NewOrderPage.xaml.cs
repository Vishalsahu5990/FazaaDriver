using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFShapeView;

namespace Fazaa
{
	public partial class NewOrderPage : ContentPage
	{
		public Label lblCounter1;
		public int order_id = 0;
		public int driver_id = 0;
		public int number = 0;
		private bool FromAcceept = false;

		public NewOrderPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			//CreateCustomCircle();
			SetData();
			StartTimer();

		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			btnAccept.Clicked += (sender, e) =>
			 {
				 FromAcceept = true;
				 if (number > 0)
				 {
					 StopTimer();
					 AcceptOrderProcess();
				 }
				 else
				 { 
				StaticMethods.ShowToast(" Your time out to accept this order.");
				 }

			 };
			btnReject.Clicked += (sender, e) =>
			 {
				 StaticDataModel.IsfromRejectOrder = true;
				 StopTimer();
				  Application.Current.MainPage = new RootPage();

			 };

		}
		private void SetData()
		{ 
			StaticDataModel.IsfromNotificationTap = false;
		try
			{
				if (Device.OS == TargetPlatform.iOS)
				{
					if (StaticDataModel.model != null)
					{
						lblCustomerName.Text = StaticDataModel.model.customer_name;
						lblDestination.Text = StaticDataModel.model.destination;
						lblStoreName.Text = StaticDataModel.model.storename;
						lblStoreLocation.Text = StaticDataModel.model.store_location;
						lblPrice.Text ="$"+ StaticDataModel.model.order_price;
						order_id =Convert.ToInt32(StaticDataModel.model.order_id);
						driver_id = Convert.ToInt32(StaticDataModel.model.driver_id);

					}
				}
			}
			catch (Exception ex)
			{

			}
		}
		private void StartTimer()
		{ 
              number = 45;
			Device.StartTimer(TimeSpan.FromSeconds(1), () =>
			{

				if (number > 0)
				{
					number = number - 1; //continue
					lblCounter.Text = number.ToString();
					Debug.WriteLine(number.ToString());
					return true;
				}
				else
				{
					if (!FromAcceept)
					{
						Application.Current.MainPage = new RootPage();
					}
				return false;
				}
				return false; //not continue

			});
		}
		private void StopTimer()
		{
			    number = 0;
				//lblCounter.Text = "0";
				
		}
		private void CreateCustomCircle()
		{ 
		try
			{
				 lblCounter1 = new Label
				{
					Text = "45",
					//FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
					FontSize = 55,
					TextColor = Color.White,
					HorizontalOptions = LayoutOptions.Fill,
					VerticalOptions = LayoutOptions.Fill,
					VerticalTextAlignment = TextAlignment.Center,
					HorizontalTextAlignment = TextAlignment.Center,
					FontAttributes = FontAttributes.Bold
				};
				var lblSeconds = new Label
				{
					Text = "Second",
					FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
					TextColor = Color.White,
					HorizontalOptions = LayoutOptions.Fill,
					VerticalOptions = LayoutOptions.Fill,
					VerticalTextAlignment = TextAlignment.Center,
					HorizontalTextAlignment = TextAlignment.Center,
					FontAttributes = FontAttributes.Bold,
                    Margin=new Thickness(0,0,0,10)
                                                   
				};
				var layout = new StackLayout
				{
					Orientation = StackOrientation.Vertical,
					HorizontalOptions = LayoutOptions.Center,
					VerticalOptions = LayoutOptions.Center,

				};
				layout.Children.Add(lblCounter);
				layout.Children.Add(lblSeconds);
				var box = new ShapeView
				{
					ShapeType = ShapeType.Circle,
					HeightRequest = App.ScreenWidth / 3-20,
					WidthRequest = App.ScreenWidth / 3-20,
					Color = Color.FromHex("#494848"),
					HorizontalOptions = LayoutOptions.Center,
					Content = layout,

				};

				stlCircle.Children.Add(box);

			}
			catch (Exception ex)
			{

			}
		}
		private async Task AcceptOrderProcess()
		{
			int responseCode = 0;
			StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{
						WebService wc = new WebService();
				        responseCode = wc.AcceptOrder(driver_id,order_id);;

					}).ContinueWith(
					t =>
					{

					if (responseCode == 200)
					{
					Application.Current.MainPage = new OnGoingJobPage(order_id);
						//Navigation.PushAsync(new NavigationPage((Page)Activator.CreateInstance(typeof(OnGoingJobPage))));
						StaticMethods.ShowToast("Order accepted successfully.");
					}
					else if (responseCode == 401)
					{
						
					Application.Current.MainPage = new RootPage();
							StaticMethods.ShowToast("Order already accepted by other driver.");
						}
						else
						{

							StaticMethods.ShowToast("Unable to accept order.");

						}
				StaticMethods.DismissLoader();

					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
	}
}
