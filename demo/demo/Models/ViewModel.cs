using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo.Models
{
	public class ViewModel
	{
		public int Agentid { get; set; }

		public string Agent_Name { get; set; }

		public string Gender { get; set; }

		public string Mobile_Number { get; set; }

		public string Address { get; set; }

		public double Basic_Salary { get; set; }

		public double Share_Charge { get; set; }

		public double Service_Charge { get; set; }

		public double Net_Total { get; set; }
	}
}