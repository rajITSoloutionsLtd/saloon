using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo.Models
{
	public class ModelTblService_Agent_Mapping
	{
		public int id { get; set; }

		public int Agent_ID { get; set; }

		public int Service_Type_ID { get; set; }


		

		public int Service_ID { get; set; }

		public String AgentName { get; set; }

		public String ServiceName { get; set; }

		public String ServiceTypeName { get; set; }
	}
}