using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamvvm;
namespace Fazaa
{
	public partial class HomePage : ContentPage
	{
   public ICommand OpenMenuCommand { get; private set; }
		private CustomMap _map;
		private int flag = 0;
		public static int staticcustomPins;

		public HomePage()
		{
			InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

			Title = "Driver";
			App.locator = Plugin.Geolocator.CrossGeolocator.Current;
            App.locator.PositionChanged += position_Changed;


           
            if(!App.locator.IsListening)
                App.locator.StartListeningAsync(TimeSpan.FromSeconds(10),1, true);
			//Task.Factory.StartNew(async () =>
			//{ 
				  OnLoad();
				
			//});

		}
		public void menu_Tapped(object sender, EventArgs e)
		{
			StaticDataModel.currentContext.MenuTapped.Execute(StaticDataModel.currentContext.MenuTapped);
		}
		protected override async void OnAppearing()
		{
			base.OnAppearing();
			StaticDataModel.IsfromRejectOrder = false;
          
			
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			App.locator.PositionChanged -= position_Changed;
		}
		private async Task SetData()
		{ 
		try
			{
				Device.BeginInvokeOnMainThread( () =>
						{
							var model = StaticMethods.GetLocalSavedData();
					        if(model.image!=null)
							imgProfile.Source = ImageSource.FromUri(new System.Uri(model.image));
							lblDriverName.Text = model.firstname + " " + model.lastname;
							lblEmail.Text = model.email;
							StaticDataModel.UserId = model.driver_id;
						});
				

			}
			catch (Exception ex)
			{
				StaticMethods.ShowToast("Something went wrong!");
			}
		}
        private async void CreateMap()
		{
            try
            {
                _map = new CustomMap(this)
                {
                    MapType = MapType.Street,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };
                stlMap.Children.Add(_map);

                var position = await App.locator.GetPositionAsync(TimeSpan.FromSeconds( 10000));
				AddBarMarkerOnMap(StaticDataModel.Lattitude, StaticDataModel.Longitude);
				
             
            }
            catch(Exception)
            {
                
            }
		}
		private void AddBarMarkerOnMap(double lat,double lng)
		{
			_map.Pins.Clear();
			try
			{
				var pin = new CustomPin
				{
					Pin = new Pin
					{
						Type = PinType.Place,
						Position = new Xamarin.Forms.Maps.Position(lat, lng),
						Label = "Path",
						Address = Guid.NewGuid().ToString(),
					},
					Id = "Xamarin",

					//Url = BarDetails.url

				};
				staticcustomPins = 0;
				_map.CustomPins = new List<CustomPin> { pin };
				_map.Pins.Add(pin.Pin);
				_map.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(lat, lng), Distance.FromMiles(3)));

			}
			catch (Exception ex)
			{

			}

		}

		private async Task OnLoad()
		{

			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
					() =>
					{
						CreateMap();
				        SetData();

					}).ContinueWith(
					t =>
					{
               


					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
		private async Task GetCurrentPostion()
		{
            Task< Plugin.Geolocator.Abstractions.Position> position = null;
            StaticMethods.ShowLoader();
			Task.Factory.StartNew(
					// tasks allow you to use the lambda syntax to pass wor
                async() =>
					{
                position = App.locator.GetPositionAsync(TimeSpan.FromSeconds(10000));	

					}).ContinueWith(
					t =>
					{
                AddBarMarkerOnMap(position.Result.Latitude,position.Result.Longitude);

                StaticMethods.DismissLoader();
					}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}
		private async void UpdateLocation()
		{
			   
				WebService wc = new WebService();
				wc.UpdateLocation();
		}
		// Callback function for when GPS location changes
		void position_Changed(object obj, PositionEventArgs e)
		{
			 
			updateGPSDataList(e.Position);
		}

		// Update GPS location displays and database
		private async void updateGPSDataList(Plugin.Geolocator.Abstractions.Position position)
		{
			Debug.WriteLine("Position changed: " + position.Latitude);
			Debug.WriteLine("Position changed: " + position.Longitude);
			

            if (Device.OS == TargetPlatform.iOS)
            {
                MessagingCenter.Send<object, Xamarin.Forms.Maps.Position>(this, "ChangePosition", new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude));
            }
            else
            {
                if (StaticDataModel.Lattitude <= 0)
                {
					App.Current.MainPage = new RootPage();
                   
                }
                else
                {
					
                    AddBarMarkerOnMap(position.Latitude, position.Longitude);
                  
				}
            }
			StaticDataModel.Lattitude = position.Latitude;
			StaticDataModel.Longitude = position.Longitude;
		}

	}

}
