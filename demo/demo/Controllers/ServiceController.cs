using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demo.Demo.DemoService;
using demo.Models;
using demo.Demo.Entity;

namespace demo.Controllers
{
	public class ServiceController : Controller
	{
		// GET: Service
		public ActionResult Index(String id)
		{
			List<Service_Table> lstSrvctable = new List<Service_Table>();
			List<Service> lstSrvc = new List<Service>();
			using (var context = new SMSEntities())
			{
				lstSrvctable = context.Service_Table.ToList();
			}

			lstSrvc = new Utility().ConvertList<Service_Table, Service>(lstSrvctable);

			ViewBag.lst = lstSrvc;
			return View(lstSrvc);
		}

		public ActionResult Create(String id)
		{
			List<ServiceType> lstsrvctype = new List<ServiceType>();

			using (var context = new SMSEntities())
			{
				lstsrvctype = context.ServiceTypes.ToList();
			}

			List<modelservicetype> lstmodelservicetype = new List<modelservicetype>();
			lstmodelservicetype = new Utility().ConvertList<ServiceType, modelservicetype>(lstsrvctype);

			ViewBag.Srvctypelst = lstmodelservicetype;

			if (id == null)
			{
				Service srvc = new Service();
				return View(srvc);
			}
			else
			{
				int idn = Convert.ToInt32(id);
				List<Service_Table> lstSrvctable = new List<Service_Table>();
				List<Service> lstSrvc = new List<Service>();
				using (var context = new SMSEntities())
				{
					lstSrvctable = context.Service_Table.ToList().FindAll(x => x.id == idn);
				}
				lstSrvc = new Utility().ConvertList<Service_Table, Service>(lstSrvctable);

				return View(lstSrvc[0]);
			}
			
		}

		[HttpPost]
		public ActionResult CreateService(Service srvc)
		{


			//CREATE NEW ITEM
			if (srvc.id == 0)		
			{
				List<Service> srvcLst = new List<Models.Service>();
				srvcLst.Add(srvc);
				//Convert Our Model to Entity Model
				List<Service_Table> lstServiceTable = new Utility().ConvertList<Service, Service_Table>(srvcLst);
				using (var context = new SMSEntities())
				{
					foreach(var item in lstServiceTable)
					{
						context.Service_Table.Add(item);
						context.SaveChanges();
					}
				}

			}





			//UPDATE A ITEM
			else
			{
				List<Service> srvcLst = new List<Models.Service>();
				srvcLst.Add(srvc);

				//Convert Our Model to Entity Model

				List<Service_Table> lstServiceTable = new Utility().ConvertList<Service, Service_Table>(srvcLst);
				using (var context = new SMSEntities())
				{
					foreach (var entity in lstServiceTable)
					{
						context.Service_Table.Attach(entity);
						context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
						entity.update_at = DateTime.Now;
						context.SaveChanges();
					}
				}
			}
			return RedirectToAction("Index");
		}


	}
}