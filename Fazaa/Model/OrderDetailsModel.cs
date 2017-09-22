using System;
using System.Collections.Generic;

namespace Fazaa
{
	public class OrderDetailsModel
	{
		public class ProductDetail
		{
			public string Product_Name { get; set; }
			public string Product_description { get; set; }
			public string Product_Image { get; set; }
			public double Product_Price { get; set; }
			public string quantity { get; set; }
            public string Category_Name { get; set; }
           
		}

		public class Orderdata
		{
			public string Order_Id { get; set; }
			public string Order_Id_Show { get; set; }
			public string source_address { get; set; }
			public string destination_address { get; set; }
			public List<ProductDetail> product_detail { get; set; }
		}

		public string responseMessage { get; set; }
		public Orderdata orderdata { get; set; }
		public int responseCode { get; set; }
	}
}
