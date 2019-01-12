using demo.Demo.Entity;
using demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace demo.Controllers
{
	public class TokenController : Controller
	{
		// GET: Token
		public ActionResult Index()
		{
			{
				List<Token_Table> lstTokenTbl = new List<Token_Table>();
				List<TokenModel> lstTokenMdl = new List<TokenModel>();
				using (var context = new SMSEntities())
				{
					lstTokenTbl = context.Token_Table.ToList();

				}

				//Joinning Customer,Token,CustomerName

				var cont = new SMSEntities();
				

			
					
								ViewBag.lst = lstTokenMdl;
								return View(lstTokenMdl);
			}

	
			
		}

		public ActionResult Create(string id)
		{	
			
			//1


			List<TblOfCustomer> ListCustomer = new List<TblOfCustomer>();

			using (var context = new SMSEntities())
			{
				ListCustomer = context.TblOfCustomers.ToList();
			}

			List<ModelCustomer> modelcustomer = new List<ModelCustomer>();
			modelcustomer = new Utility().ConvertList<TblOfCustomer, ModelCustomer>(ListCustomer);

			ViewBag.customerlist = modelcustomer;

			//2


			List<ServiceType> ListSrvcType = new List<ServiceType>();

			using (var context = new SMSEntities())
			{
				ListSrvcType = context.ServiceTypes.ToList();
			}

			List<modelservicetype> modelservice = new List<modelservicetype>();
			modelservice = new Utility().ConvertList<ServiceType, modelservicetype>(ListSrvcType);

			ViewBag.servicelist = modelservice;




			if (id == null)
			{
				TokenModel Token_Table = new TokenModel();
				return View(Token_Table);
			}

			else

			{
				int idno = Convert.ToInt32(id);
				List<Token_Table> listTokentbl = new List<Token_Table>();
				List<TokenModel> mdlTokenLst = new List<TokenModel>();
				using (var context = new SMSEntities())
				{
					listTokentbl = context.Token_Table.ToList().FindAll(x => x.id == idno);
				}

				mdlTokenLst = new Utility().ConvertList<Token_Table, TokenModel>(listTokentbl);
				return View(mdlTokenLst[0]);

			}

		}

		[HttpPost]
		public ActionResult CreateToken(TokenModel mdlToken)
		{
			String[] services = null;
			if (Request.Form.Get("seviceCheckBox[]")!=null)
			{
				services = Request.Form.GetValues("seviceCheckBox[]");
			}
			String[] agentds = null;
			if (Request.Form.Get("AgentID[]") != null)
			{
				agentds = Request.Form.GetValues("AgentID[]");
			}



			if (mdlToken.id == 0)
			{
				List<TokenModel> LstTokenmodel = new List<demo.Models.TokenModel>();
				LstTokenmodel.Add(mdlToken);

				List<Token_Table> LstTokenTbl = new Utility().ConvertList<TokenModel, Token_Table>(LstTokenmodel);

				using (var context = new SMSEntities())
				{
					foreach (var item in LstTokenTbl)
					{
						context.Token_Table.Add(item);
						context.SaveChanges();
					}
				}
			}

			else
			{
				List<TokenModel> LstTokenmodel = new List<demo.Models.TokenModel>();
				LstTokenmodel.Add(mdlToken);

				//Convert Our Model to Entity Model


				List<Token_Table> LstTokenTbl = new Utility().ConvertList<TokenModel, Token_Table>(LstTokenmodel);
				using (var context = new SMSEntities())
				{
					foreach (var entity in LstTokenTbl)
					{
						context.Token_Table.Attach(entity);
						context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
						context.SaveChanges();
					}
				}
			}



			return RedirectToAction("Index");

		}

		public PartialViewResult CustomerLoad(string id)
		{
			List<TblOfCustomer> lstEnCustomer = new List<TblOfCustomer>();
			List<ModelCustomer> lstMdlCustomer = new List<ModelCustomer>();
			using (var context = new SMSEntities())
			{
				int customerID = Convert.ToInt32(id);

				lstEnCustomer = context.TblOfCustomers.ToList().FindAll(x => x.Mobile_Number == id);
			}

			lstMdlCustomer = new Utility().ConvertList<TblOfCustomer, ModelCustomer>(lstEnCustomer);
			if (lstEnCustomer.Count > 0)
			{
				return PartialView(lstMdlCustomer[0]);
			}
			else
			{
				return PartialView(new ModelCustomer());
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ServicetypeID"></param>
		/// <param name="tblServcMappID"></param>
		/// <returns></returns>
		public PartialViewResult ServiceTypeCheckBox(String ServicetypeID, String tblServcMappID = "0")//Service Type ID like (spa)
		{

			List<Service_Table> listService = new List<Service_Table>();  //Initiated Entity Service TypeModel
			List<Service> listMdlService = new List<Service>();  //Initiated Our Model Service
			int srvctypid = Convert.ToInt32(ServicetypeID);
			using (var context = new SMSEntities())
			{
				listService = context.Service_Table.ToList().FindAll(x => x.Service_Type_Id == srvctypid); //Find all Services under provided Servce type id(like spa) 

			}

			listMdlService = new Utility().ConvertList<Service_Table, Service>(listService); //Convert Entity Service to our Model service

			List<Model_AgentMappingDettails> lstModelAgentMappDetails = new List<Model_AgentMappingDettails>();
			List<TblSrvcAgntMapDettail> lstTblMappDetail = new List<TblSrvcAgntMapDettail>();
			if (tblServcMappID != "0")
			{
				int mid = Convert.ToInt32(tblServcMappID);
				using (var context = new SMSEntities())
				{
					lstTblMappDetail = context.TblSrvcAgntMapDettails.ToList().FindAll(x => x.TableSrvcMapID == mid);
				}
				lstModelAgentMappDetails = new Utility().ConvertList<TblSrvcAgntMapDettail, Model_AgentMappingDettails>(lstTblMappDetail);
			}

			ViewBag.Service = lstModelAgentMappDetails;

			return PartialView(listMdlService); // Return to partial view with our Service Model

		}

		//netWork


		public PartialViewResult ServiceLoad(string id,String cdis)
		{

			String[] splS = id.Split('-');
			List<int?> lstSpt = new List<int?>();
			foreach(var str in splS)
			{
				lstSpt.Add(Convert.ToInt32(str));
			}

			List<ViewModel> lst = new List<ViewModel>();
			using (var context = new SMSEntities())
			{
				 lst = (from at in context.Agent_Table
									   join tsam in context.tblServiceAgentMappings
									   on at.id equals tsam.Agent_ID
									   join tsamd in context.TblSrvcAgntMapDettails
									   on tsam.id equals tsamd.TableSrvcMapID
									   where lstSpt.Contains(tsamd.Service)
									   select new ViewModel { Agentid = at.id, Agent_Name = at.Agent_Name }).Distinct().ToList();



				double totalSrvceCharge = (from st in context.Service_Table
										where lstSpt.Contains(st.id)
										select st.Service_Charge).Sum();


				double discount = (totalSrvceCharge * Convert.ToDouble(cdis)) / 100;

				double nettotal= totalSrvceCharge - discount;

			

				lst.ForEach(delegate (ViewModel vm)
				{
					vm.Service_Charge = totalSrvceCharge;
					vm.Net_Total = nettotal;
				});

			}			



			return PartialView(lst);
		}

	}
}