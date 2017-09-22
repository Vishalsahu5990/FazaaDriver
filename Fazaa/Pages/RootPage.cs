using System;
using System.Diagnostics;
using System.Windows.Input;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace Fazaa
{
	public class RootPage:MasterDetailPage
	{
		UserModel usermodel = null;
		MenuPage menuPage;
		public static readonly BindableProperty MenuTappedCommandProperty = BindableProperty.Create<HomePage, ICommand>(x => x.OpenMenuCommand, null);

		public Command MenuTapped
		{
			get { return (Command)GetValue(MenuTappedCommandProperty); }
			set
			{
				SetValue(MenuTappedCommandProperty, value);
			}
		}
		public RootPage( )
		{
            StaticDataModel.currentContext = this;
			NavigationPage.SetHasNavigationBar(this, false);
			 menuPage = new MenuPage();
			Master = menuPage;
			Detail = new NavigationPage(new HomePage());
			Onload();
			MenuTapped = new Command(async (x) =>
			{
				Device.BeginInvokeOnMainThread(async () =>
				{

					this.IsPresented = true;
				});
			});


		}
		private void Onload()
		{ 
		try
			{
				
				this.Padding = new Thickness(0, 0, 0, 0);
				Master.Icon = "menu.png";

				var slhome = new TapGestureRecognizer();
				menuPage.FindByName<StackLayout>("stHome").GestureRecognizers.Add(slhome);
				slhome.Tapped += (object sender, EventArgs e) =>
				{
					StaticDataModel.IsfromNavigationMenu = true;
					Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(HomePage)));
				    IsPresented = false;

				};
				var slAboutUs = new TapGestureRecognizer();
				menuPage.FindByName<StackLayout>("stAboutus").GestureRecognizers.Add(slAboutUs);
				slAboutUs.Tapped += (object sender, EventArgs e) =>
				{
					
					Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(AboutPage)));
					IsPresented = false;
				};
				var slcategories = new TapGestureRecognizer();
				menuPage.FindByName<StackLayout>("stCategories").GestureRecognizers.Add(slcategories);
				slcategories.Tapped += (object sender, EventArgs e) =>
				{
					Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(HistoryofRidesPage)));
					IsPresented = false;

				};
				//var slMyAccount = new TapGestureRecognizer();
				//menuPage.FindByName<StackLayout>("stAccount").GestureRecognizers.Add(slMyAccount);
				//slMyAccount.Tapped += (object sender, EventArgs e) =>
				//{

				//	Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(NewOrderPage)));
				//	IsPresented = false;
				//};
				var stWishlist = new TapGestureRecognizer();
				menuPage.FindByName<StackLayout>("stWishlist").GestureRecognizers.Add(stWishlist);
				stWishlist.Tapped += (object sender, EventArgs e) =>
				{
                    Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(MyProfilePage)));
					IsPresented = false;

				};
				var stFavourite = new TapGestureRecognizer();
				menuPage.FindByName<StackLayout>("stFavourite").GestureRecognizers.Add(stFavourite);
				stFavourite.Tapped +=async (object sender, EventArgs e) =>
				{
					IsPresented = false;
					var answer = await DisplayAlert(null, "Would you like to Logout?", "Yes", "No");
					if (answer)
					{
                        if (Device.OS == TargetPlatform.iOS)
                        {
                         DependencyService.Get<IiOSMethods>().DeleteLocalData();
                        }
                        else
                        {
                         DependencyService.Get<IAndroidMethods>().DeleteLocalData();
                        }
						
						App.Current.MainPage = new NavigationPage(new Login());
					}

				};

			
				

				var layout = new StackLayout
				{
					HorizontalOptions = LayoutOptions.FillAndExpand
												   ,
					Orientation = StackOrientation.Horizontal,
					VerticalOptions = LayoutOptions.End,
					HeightRequest = 60
				};
				var content = new ContentPage()
				{

				};
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

	}
}
