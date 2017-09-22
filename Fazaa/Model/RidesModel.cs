using System;
using System.Collections.Generic;

namespace Fazaa
{
    public class RidesModel
    {
		public string responseMessage { get; set; }
		public List<Orderdata> orderdata { get; set; }
		public int responseCode { get; set; }
		public class Orderdata
		{
			public string Order_Id { get; set; }
			public string Order_Id_Show { get; set; }
			public string Order_DateTime { get; set; }
			public string Total_Price { get; set; }
			public string Order_Status { get; set; }
            public string Cell_Color { get; set; }
		}
    }
}
