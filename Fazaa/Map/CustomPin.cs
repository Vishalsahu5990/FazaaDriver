using System;
using Xamarin.Forms.Maps;

namespace Fazaa
{
	public class CustomPin
	{

		public CustomPin()
		{

		}
		public Pin Pin { get; set; }

		public string Id { get; set; }

		public string Url { get; set; }
		public CustomPin(string name, string details, double latitude, double longitude)
		{

			Name = name;
			Details = details;

		}

		public string Name { get; set; }
		public int id { get; set; }
		public string Details { get; set; }
		public string ImageUrl { get; set; }





	}

	public class NewCustomPin
	{

		public NewCustomPin()
		{

		}
		public Pin Pin { get; set; }

		public string Id { get; set; }

		public string Url { get; set; }
		public NewCustomPin(string name, string details, double latitude, double longitude)
		{

			Name = name;
			Details = details;

		}

		public string Name { get; set; }
		public int id { get; set; }
		public string Details { get; set; }
		public string ImageUrl { get; set; }





	}
}
