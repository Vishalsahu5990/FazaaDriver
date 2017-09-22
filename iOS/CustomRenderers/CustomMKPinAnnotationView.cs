using System;
using MapKit;
namespace Fazaa.iOS
{
	public class CustomMKPinAnnotationView : MKAnnotationView
	{
		public string Id { get; set; }

		public string Url { get; set; }
		public string Details { get; set; }
		public CustomMKPinAnnotationView(IMKAnnotation annotation, string id, string details)
	: base(annotation, id)
		{
		}
	}
}
	