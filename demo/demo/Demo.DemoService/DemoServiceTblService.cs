using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using demo.Models;
using demo.Demo.Repository;
using demo.Demo.Entity;

namespace demo.Demo.DemoService
{
	public class DemoServiceTblService
	{
		/// <summary>
		/// Get All Service from Database
		/// </summary>
		/// <returns></returns>
		public List<Service> getserviceALL()
		{
			Utility util = new Utility();
			IRepository<Service_Table> repo = new ServiceRepo();
			List<Service> lst = new List<Models.Service>();
			lst = util.ConvertList<Service_Table, Service>(repo.GetAll().ToList());
			return lst;
			
		}

		/// <summary>
		/// Insert service to database
		/// </summary>
		/// <param name="srvclst"></param>
		public void addservice(List<Service> srvclst)
		{
			Utility util = new Utility();
			List<Service_Table> lst = new List<Service_Table>();
			lst = util.ConvertList<Service, Service_Table>(srvclst);
			IRepository<Service_Table> repo = new ServiceRepo();
			foreach(var itm in lst)
			{
				repo.Add(itm);
				repo.SaveChanges();
			}			

		}


		/// <summary>
		/// Retrive records through Service ID
		/// </summary>
		/// <param name="id">Service ID</param>
		/// <returns></returns>
		public Service getServiceByID(String id)
		{		
			IRepository<Service_Table> repo = new ServiceRepo();			
			Utility util = new Utility();
			List<Service> lst = new List<Models.Service>();
			int ids = Convert.ToInt32(id);
			lst = util.ConvertList<Service_Table, Service>(repo.Find(itm => itm.id == ids).ToList());
			return lst.Single();
		}

		public void UpdateService(List<Service> mdl)
		{
			List<Service_Table> lst = new List<Service_Table>();
			IRepository<Service_Table> repo = new ServiceRepo();
			Utility util = new Utility();


			lst = util.ConvertList<Service, Service_Table>(mdl);
			//var context = new SMSEntities();
			//context.Service_Table.Attach(entity);
			//context.Entry(entity).State = EntityState.Modified;
			//entity.update_at = DateTime.Now;

			repo.Attach(lst.Single());
			repo.SaveChanges();
		}
	}
}