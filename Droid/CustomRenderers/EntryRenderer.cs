using System;
using Fazaa;
using Fazaa.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]

namespace Fazaa.Droid
{
	class MyEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				Control.SetBackgroundColor(global::Android.Graphics.Color.White);
				Control.Gravity = Android.Views.GravityFlags.Center;
				Control.TextAlignment = Android.Views.TextAlignment.Center;


			}
		}
	}
}
