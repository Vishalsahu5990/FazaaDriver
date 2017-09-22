using System;
namespace Fazaa
{
	public class StoreModel
	{
		public string ImageUrl
		{
			get; set;
		}


		public string FileName
		{
			get; set;
		}
		public string Description { get; set; }
		public string Price { get; set; }
		public string Qty { get; set; }
		public double lat{ get; set; }
		public double lng { get; set; }
	}
}
