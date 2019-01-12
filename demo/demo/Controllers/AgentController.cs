using demo;
using demo.Demo.Entity;
using demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demo.Controllers
{
	public class AgentController : Controller
	{

		// GET: Agent
		public ActionResult Index(String id)
		{
			List<Agent_Table> LstAgentTable = new List<Agent_Table>();
			List<ModelAgent> Lstmodelagent = new List<ModelAgent>();
			using (var context = new SMSEntities())

			{
				LstAgentTable = context.Agent_Table.ToList();

			}

			Lstmodelagent = new Utility().ConvertList<Agent_Table, ModelAgent>(LstAgentTable);

			ViewBag.lst = Lstmodelagent;
			return View(Lstmodelagent);	
		}
	
		public ActionResult Create(String id)
		{
			if (id == null)
			{
				ModelAgent mdlagnt = new ModelAgent();
				return View(mdlagnt);
			}

			else

			{
				int idno = Convert.ToInt32(id);
				List<Agent_Table> LstAgentTable = new List<Agent_Table>();
				List<ModelAgent> LstModelAgent = new List<ModelAgent>();
				using (var context = new SMSEntities())
				{
					LstAgentTable = context.Agent_Table.ToList().FindAll(x => x.id == idno);
				}

				LstModelAgent = new Utility().ConvertList<Agent_Table, ModelAgent>(LstAgentTable);
				return View(LstModelAgent[0]);

			}

		}

		[HttpPost]
		public ActionResult CreateAgent(ModelAgent mdlAgent)
		{
			if (mdlAgent.id == 0)
			{
				List<ModelAgent> LstmdlAgent = new List<demo.Models.ModelAgent>();
				LstmdlAgent.Add(mdlAgent);

				List<Agent_Table> lstAgentTable = new Utility().ConvertList<ModelAgent, Agent_Table>(LstmdlAgent);

				using (var context = new SMSEntities())
				{
					foreach (var item in lstAgentTable)
					{
						context.Agent_Table.Add(item);
						context.SaveChanges();
					}
				}
			}

			else
			{
				List<ModelAgent> LstmdlAgent = new List<demo.Models.ModelAgent>();
				LstmdlAgent.Add(mdlAgent);

				//Convert Our Model to Entity Model


				List<Agent_Table> lstAgentTable = new Utility().ConvertList<ModelAgent, Agent_Table>(LstmdlAgent);
				using (var context = new SMSEntities())
				{
					foreach (var entity in lstAgentTable)
					{
						context.Agent_Table.Attach(entity);
						context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
						context.SaveChanges();
					}
				}
			}



			return RedirectToAction("Index");

		}
	}
}


	




