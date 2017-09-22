using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Fazaa
{
	public partial class Signup : PopupPage
	{
		Picker picker_city;
		public Signup()
		{
			InitializeComponent();

			var slDateofbirth = new TapGestureRecognizer();
			stDob.GestureRecognizers.Add(slDateofbirth);
			slDateofbirth.Tapped += (object sender, EventArgs e) =>
			{
				datepicker.Focus();
			};
			var slCity = new TapGestureRecognizer();
			stCity.GestureRecognizers.Add(slCity);
			slCity.Tapped += (object sender, EventArgs e) =>
			{
				CityPicker.Focus();
			};

			CityPicker.Items.Add("Delhi");
			CityPicker.Items.Add("Mumbai");
			CityPicker.Items.Add("Pune");
			CityPicker.Items.Add("Indore");
			CityPicker.Items.Add("Bhopal");
			CityPicker.Items.Add("Jabalpur");
			CityPicker.Items.Add("Delhi");
			CityPicker.Items.Add("Mumbai");
			CityPicker.Items.Add("Pune");
			CityPicker.Items.Add("Indore");
			CityPicker.Items.Add("Bhopal");
			CityPicker.Items.Add("Jabalpur");	
			CityPicker.Items.Add("Delhi");
			CityPicker.Items.Add("Mumbai");
			CityPicker.Items.Add("Pune");
			CityPicker.Items.Add("Indore");
			CityPicker.Items.Add("Bhopal");
			CityPicker.Items.Add("Jabalpur");


			CityPicker.SelectedIndexChanged += (object sender, EventArgs e) =>
			 { 
			 var item = CityPicker.Items[CityPicker.SelectedIndex];
				 lblCity.Text = item;
				 lblDob.Text = item;
			};
			btnSignup.Clicked += (object sender, EventArgs e) =>
			 { 

			};
		}
	}
}
