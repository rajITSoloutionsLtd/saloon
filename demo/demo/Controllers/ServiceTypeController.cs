using demo.Demo.Entity;
using demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demo.Controllers
{
	public class ServiceTypeController : Controller
	{
		//private List<modelservicetype> mdlservcLst;

		// GET: ServiceType
		public ActionResult Index(String id)
		{
			List<ServiceType> lstSrvcType = new List<ServiceType>();
			List<modelservicetype> lstmdlsrvctype = new List<modelservicetype>();
			using (var context = new SMSEntities())
			{
				lstSrvcType = context.ServiceTypes.ToList();

			}
			lstmdlsrvctype = new Utility().ConvertList<ServiceType, modelservicetype>(lstSrvcType);

			ViewBag.lst = lstmdlsrvctype;
			return View(lstmdlsrvctype);
		}
		public ActionResult Create(String id)
		{ 
			if (id == null)
			{
				modelservicetype mdl = new modelservicetype();
				return View(mdl);
			}

			else

			{
				int idno = Convert.ToInt32(id);
				List<ServiceType> lstservicetype = new List<ServiceType>();
				List<modelservicetype> mdlsrvcLst = new List<modelservicetype>();
				using (var context = new SMSEntities())
				{
					lstservicetype= context.ServiceTypes.ToList().FindAll(x => x.id == idno);
				}

				mdlsrvcLst = new Utility().ConvertList<ServiceType, modelservicetype>(lstservicetype);
				return View(mdlsrvcLst[0]);

			}

		}

		[HttpPost]
		public ActionResult CreateServiceType(modelservicetype mdl)
		{
			if (mdl.id == 0)
			{
				List<modelservicetype> mdlsrvcLst = new List<Models.modelservicetype>();
				mdlsrvcLst.Add(mdl);

				List<ServiceType> lstservicetype = new Utility().ConvertList<modelservicetype, ServiceType>(mdlsrvcLst);

				using (var context = new SMSEntities())
				{
					foreach (var item in lstservicetype)
					{
						context.ServiceTypes.Add(item);
						context.SaveChanges();
					}
				}
			}
			//UPDATE A ITEM
			else
			{
				List<modelservicetype> mdlsrvcLst = new List<Models.modelservicetype>();
				mdlsrvcLst.Add(mdl);

				//Convert Our Model to Entity Model


				List<ServiceType> lstservicetype = new Utility().ConvertList<modelservicetype, ServiceType>(mdlsrvcLst);
				using (var context = new SMSEntities())
				{
					foreach (var entity in lstservicetype)
					{
						context.ServiceTypes.Attach(entity);
						context.Entry(entity).State = System.Data.Entity.EntityState.Modified;						
						context.SaveChanges();
					}
				}
			}



			return RedirectToAction("Index");

		}
	}

}
