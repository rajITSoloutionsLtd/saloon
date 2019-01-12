using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace demo.Models
{
	public class Service
	{
		public int id { get; set; }

		[Required]
		public string Service_Name { get; set; }

		[Required]
		public string GenderGender { get; set; }

		[Required]
		public int Age_From { get; set; }

		[Required]
		public int Age_To { get; set; }

		[Required]
		public string Customer_Type { get; set; }

		[Required]
		public double Service_Charge { get; set; }

		public int Service_Type_Id { get; set; }



	}

	public class modelservicetype
	{
		public int id { get; set; }

		public string Name { get; set; }

		public int Service_Type_Id { get; set; }

	}

}