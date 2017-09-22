using System;
using MapKit;
using UIKit;
using System.Windows.Input;

namespace Fazaa.iOS
{
	class MyMapDelegate : MKMapViewDelegate
	{
		public event EventHandler AnnotationTapped;
		private readonly ICommand _PinTapped;
		public MyMapDelegate(ICommand PinTapped = null)
		{
			_PinTapped = PinTapped;
		}

	}
}
