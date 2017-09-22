using System;
namespace Fazaa
{
    public class ProfileModel
    {
		public class Driverdetail
		{
			public string driver_id { get; set; }
			public string firstname { get; set; }
			public string lastname { get; set; }
			public string email { get; set; }
			public string DOB { get; set; }
			public string city { get; set; }
			public string phonenumber { get; set; }
			public string address { get; set; }
			public string device { get; set; }
			public string vehicle { get; set; }
			public string image { get; set; }
		}

		public string responseMessage { get; set; }
		public Driverdetail driverdetail { get; set; }
		public int responseCode { get; set; }
    }
}
