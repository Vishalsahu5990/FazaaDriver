using System;
using System.Collections.Generic;
using System.ComponentModel;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using System.Collections.ObjectModel;
using System.Net;
using Android.Graphics;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.Android;
using Java.IO;
using Java.Net;
using Android.Provider;
using Fazaa;
using Fazaa.Droid;

[assembly:ExportRenderer (typeof(CustomMap), typeof(CustomMapRenderer))]
namespace Fazaa.Droid
{
	public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
	{
		List<CustomPin> customPins;
		bool isDrawn;

		protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				NativeMap.InfoWindowClick -= OnInfoWindowClick;
			}

			if (e.NewElement != null)
			{
				var formsMap = (CustomMap)e.NewElement;
				customPins = formsMap.CustomPins;
				Control.GetMapAsync(this);
			}
			MessagingCenter.Subscribe<object, Xamarin.Forms.Maps.Position>(this, "ChangePositionAndroid", (sender, msg) =>

			{
				FormsMap_PinsUpdated(sender, msg);
			});
		}

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals("VisibleRegion") && !isDrawn)
            {
                NativeMap.Clear();
                NativeMap.InfoWindowClick += OnInfoWindowClick;
                NativeMap.SetInfoWindowAdapter(this);
                if (!ReferenceEquals(customPins, null))
                {
                   

                    foreach (var pin in customPins)
                    {
                        var marker = new MarkerOptions();
                        marker.SetPosition(new LatLng(pin.Pin.Position.Latitude, pin.Pin.Position.Longitude));
                        marker.SetTitle(pin.Pin.Label);
                        marker.SetSnippet(pin.Pin.Address);
                        marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.marker));

                        NativeMap.AddMarker(marker);
                    }
                }
                isDrawn = true;
            }
        }

		protected override void OnLayout(bool changed, int l, int t, int r, int b)
		{
			base.OnLayout(changed, l, t, r, b);

			if (changed)
			{
				isDrawn = false;
			}
		}

		void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
		{
			var customPin = GetCustomPin(e.Marker);
			if (customPin == null)
			{
				throw new Exception("Custom pin not found");
			}

			if (!string.IsNullOrWhiteSpace(customPin.Url))
			{
				var url = Android.Net.Uri.Parse(customPin.Url);
				var intent = new Intent(Intent.ActionView, url);
				intent.AddFlags(ActivityFlags.NewTask);
				Android.App.Application.Context.StartActivity(intent);
			}
		}

		public Android.Views.View GetInfoContents(Marker marker)
		{
			var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
			if (inflater != null)
			{
				Android.Views.View view;

				var customPin = GetCustomPin(marker);
				if (customPin == null)
				{
					throw new Exception("Custom pin not found");
				}

				if (customPin.Id == "Xamarin")
				{
					view = inflater.Inflate(Resource.Layout.design_navigation_menu, null);
				}
				else
				{
					view = inflater.Inflate(Resource.Layout.design_navigation_menu, null);
				}

				var infoTitle = view.FindViewById<TextView>(Resource.Id.design_bottom_sheet);
				var infoSubtitle = view.FindViewById<TextView>(Resource.Id.design_bottom_sheet);

				if (infoTitle != null)
				{
					infoTitle.Text = marker.Title;
				}
				if (infoSubtitle != null)
				{
					infoSubtitle.Text = marker.Snippet;
				}

				return view;
			}
			return null;
		}

		public Android.Views.View GetInfoWindow(Marker marker)
		{
			return null;
		}

		CustomPin GetCustomPin(Marker annotation)
		{
			var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);
			foreach (var pin in customPins)
			{
				if (pin.Pin.Position == position)
				{
					return pin;
				}
			}
			return null;
		}
		private void FormsMap_PinsUpdated(object sender, Xamarin.Forms.Maps.Position e)
		{
			NativeMap.Clear();
			NativeMap.InfoWindowClick += OnInfoWindowClick;
			NativeMap.SetInfoWindowAdapter(this);
			if (!ReferenceEquals(customPins, null))
			{


				foreach (var pin in customPins)
				{
					var marker = new MarkerOptions();
                    marker.SetPosition(new LatLng(e.Latitude, e.Longitude));
					marker.SetTitle(pin.Pin.Label);
					marker.SetSnippet(pin.Pin.Address);
					marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.marker));

					NativeMap.AddMarker(marker);
				}
			}
			isDrawn = true;

		}
		
	}
}
