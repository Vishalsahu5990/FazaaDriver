using System;
namespace Fazaa
{
	public class OrderModel
	{
		public string destination_Name { get; set; }
		public string destination_Address { get; set; }
		public double destination_lat { get; set; }
		public double destination_long { get; set; }
		public int Status { get; set; }
		public string User_Email_Address { get; set; }
		public double User_Phone_Number { get; set; }
		public int responseCode { get; set; }

	}
}
