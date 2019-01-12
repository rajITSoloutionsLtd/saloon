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
	public class CustomerController : Controller
	{
		// GET: Customer
		public ActionResult Index()
		{
			List<TblOfCustomer> LstTblCustomer = new List<TblOfCustomer>();
			List<ModelCustomer> Lstmodelcustomer = new List<ModelCustomer>();
			using (var context = new SMSEntities())
			{
				LstTblCustomer = context.TblOfCustomers.ToList();

			}

			Lstmodelcustomer = new Utility().ConvertList<TblOfCustomer, ModelCustomer>(LstTblCustomer);

			ViewBag.lst = Lstmodelcustomer;
			return View(Lstmodelcustomer);
		}

		public ActionResult Create(String id)
		{
			if (id == null)
			{
				ModelCustomer mdlcustomer = new ModelCustomer();
				return View(mdlcustomer);
			}

			else

			{
				int idno = Convert.ToInt32(id);
				List<TblOfCustomer> LstTblCustomer = new List<TblOfCustomer>();
				List<ModelCustomer> Lstmodelcustomer = new List<ModelCustomer>();
				using (var context = new SMSEntities())
				{
					LstTblCustomer = context.TblOfCustomers.ToList().FindAll(x => x.id == idno);
				}

				Lstmodelcustomer = new Utility().ConvertList<TblOfCustomer, ModelCustomer>(LstTblCustomer);
				return View(Lstmodelcustomer[0]);

			}

		}

		[HttpPost]
		public ActionResult CreateCustomer(ModelCustomer mdlCustomer)
		{
			if (mdlCustomer.id == 0)
			{
				List<ModelCustomer> Lstmodelcustomer = new List<demo.Models.ModelCustomer>();
				Lstmodelcustomer.Add(mdlCustomer);

				List<TblOfCustomer> LstTblCustomer = new Utility().ConvertList<ModelCustomer, TblOfCustomer>(Lstmodelcustomer);

				using (var context = new SMSEntities())
				{
					foreach (var item in LstTblCustomer)
					{
						context.TblOfCustomers.Add(item);
						context.SaveChanges();
					}
				}
			}

			else
			{
				List<ModelCustomer> Lstmodelcustomer = new List<demo.Models.ModelCustomer>();
				Lstmodelcustomer.Add(mdlCustomer);

				//Convert Our Model to Entity Model


				List<TblOfCustomer> LstTblCustomer = new Utility().ConvertList<ModelCustomer, TblOfCustomer>(Lstmodelcustomer);
				using (var context = new SMSEntities())
				{
					foreach (var entity in LstTblCustomer)
					{
						context.TblOfCustomers.Attach(entity);
						context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
						context.SaveChanges();
					}
				}
			}



			return RedirectToAction("Index");

		}


	}

	
}