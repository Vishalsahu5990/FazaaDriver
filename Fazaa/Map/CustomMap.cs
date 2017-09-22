using System.Collections.Generic;
using Xamarin.Forms.Maps;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace Fazaa
{
	public class CustomMap: Map
	{
       
		public static readonly BindableProperty SelectedPinProperty = BindableProperty.Create<CustomMap, CustomPin>(x => x.SelectedPin, null);
		public List<CustomPin> CustomPins { get; set; }
		public ICommand ShowDetailCommand { get; set; }
		public CustomPin SelectedPin
		{
			get { return (CustomPin)base.GetValue(SelectedPinProperty); }
			set { base.SetValue(SelectedPinProperty, value); }
		}
		public static readonly BindableProperty PinTappedCommandProperty = BindableProperty.Create<CustomMap, Command>(x => x.PinTapped, null);

		public Command PinTapped
		{
			get { return (Command)GetValue(PinTappedCommandProperty); }
			set { SetValue(PinTappedCommandProperty, value); }
		}
		public CustomMap(ContentPage context)
		{
			PinTapped = new Command(async (x) =>
				{
					if (Device.OS == TargetPlatform.Android)
					{
						
					}
					else if (Device.OS == TargetPlatform.iOS)
					{
						
					}


					


				});

		}
		
	}
}
