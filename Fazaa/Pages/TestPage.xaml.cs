using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Fazaa
{
	public partial class TestPage : ContentPage
	{
		public TestPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
            ReverseNumber();
		}
		public void ReverseString()
		{ 
            string str = "Anmol";
            string reverseString = string.Empty;
            int length = 0;
            length= str.Length-1;
            for (int i = length; i>=0;i--)
            {
                reverseString = reverseString + str[i];
            }
            DisplayAlert("Alert",reverseString,"Ok");
		}
        public void ReverseNumber()
        {
            int num = 12345;
            int reverseNumber = 0;

            while(num>0)
            {
                int remainder = num % 10;
                reverseNumber = (reverseNumber * 10) + remainder;
                num = num / 10;
            }
            DisplayAlert("Alert", reverseNumber.ToString(), "Ok");
        }
	}
}
