using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Fazaa
{
	public partial class MyCartPage : ContentPage
	{
		public MyCartPage()
		{
			var items= GetOrderList();
			if(items!=null)
			Title = "My Cart("+items.Count.ToString()+")";
		}
		private List<string> GetOrderList()
		{
			List<string> source;
			try
			{
				InitializeComponent();
				 source = new List<string>();
				for (int i = 0; i < 5; i++)
				{
					source.Add("item");
				}
				listView.ItemsSource = source;
				return source;
			}
			catch (Exception ex)
			{
				return null;
				Debug.WriteLine(ex.Message);
			}
		}
	}
}
