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
	public class TblServiceAgentMappingController : Controller
	{
		// GET: TblServiceAgentMapping
		public ActionResult Index(string id)
		{
			List<tblServiceAgentMapping> LstTableServiceAgentMapping = new List<tblServiceAgentMapping>();
			List<ModelTblService_Agent_Mapping> LstModelTblServiceAgentMapping = new List<ModelTblService_Agent_Mapping>();
			using (var context = new SMSEntities())
			{

				  LstTableServiceAgentMapping = context.tblServiceAgentMappings.ToList();

			}



			//Joinning Agent,TblServiceAgnetMapping,ServiceType

			var cont = new SMSEntities();
			LstModelTblServiceAgentMapping = (from tblagentmapp in cont.tblServiceAgentMappings
					   join tblAgent in cont.Agent_Table
					   on tblagentmapp.Agent_ID equals tblAgent.id
					   join tblServcType in cont.ServiceTypes
					   on tblagentmapp.Service_Type_ID equals tblServcType.id
					   select new ModelTblService_Agent_Mapping {  AgentName = tblAgent.Agent_Name, ServiceTypeName = tblServcType.Name,id=tblagentmapp.id  } ).ToList();


			//LstModelTblServiceAgentMapping = new Utility().ConvertList<tblServiceAgentMapping, ModelTblService_Agent_Mapping>(LstTableServiceAgentMapping);




			ViewBag.lst = LstModelTblServiceAgentMapping;
			return View(LstModelTblServiceAgentMapping);
		}

		public ActionResult Create(String id)
		{


			List<Agent_Table> ListAgent = new List<Agent_Table>();

			using (var context = new SMSEntities())
			{
				ListAgent = context.Agent_Table.ToList();
			}

			List<ModelAgent> modeltblserviceagent = new List<ModelAgent>();
			modeltblserviceagent = new Utility().ConvertList<Agent_Table,ModelAgent>(ListAgent);

			ViewBag.agentlist = modeltblserviceagent;






			List<ServiceType> ListSrvcType = new List<ServiceType>();

			using (var context = new SMSEntities())
			{
				ListSrvcType = context.ServiceTypes.ToList();
			}

			List<modelservicetype> Lstmodelservice = new List<modelservicetype>();
			Lstmodelservice = new Utility().ConvertList<ServiceType, modelservicetype>(ListSrvcType);

			ViewBag.srvclist = Lstmodelservice;





			if (id == null)
			{
				ModelTblService_Agent_Mapping modeltablserviceagent = new ModelTblService_Agent_Mapping();
				return View(modeltablserviceagent);
			}

			else

			{
				int idnumber = Convert.ToInt32(id);
				List<tblServiceAgentMapping> ListTableServiceAgentMaping = new List<tblServiceAgentMapping>();
				List<ModelTblService_Agent_Mapping> modelserviceagentList = new List<ModelTblService_Agent_Mapping>();
				using (var context = new SMSEntities())
				{
					ListTableServiceAgentMaping = context.tblServiceAgentMappings.ToList().FindAll(x => x.id == idnumber);
				}

				modelserviceagentList = new Utility().ConvertList<tblServiceAgentMapping, ModelTblService_Agent_Mapping>(ListTableServiceAgentMaping);
				return View(modelserviceagentList[0]);

			}

		}


		 [HttpPost]
		public ActionResult CreateServiceAgent(ModelTblService_Agent_Mapping mdlsrvcagnt)
		{
			//Insert An Item(New Create)
			if (mdlsrvcagnt.id == 0)
			{
				#region Master Add Table -> tblServiceAgentMapping
				List<ModelTblService_Agent_Mapping> modelserviceagentList = new List<Models.ModelTblService_Agent_Mapping>();
				modelserviceagentList.Add(mdlsrvcagnt);

				int mId = 0;
				List<tblServiceAgentMapping> ListTableServiceAgentMapping = new Utility().ConvertList<ModelTblService_Agent_Mapping, tblServiceAgentMapping>(modelserviceagentList);

				using (var context = new SMSEntities())
				{
					foreach (var item in ListTableServiceAgentMapping)
					{
						context.tblServiceAgentMappings.Add(item);						
						context.SaveChanges();
						mId = item.id;
					}
				}
				#endregion

				#region Details Add -> TblSrvcAgntMapDettails

				List<Model_AgentMappingDettails> lstModelMappingDetails = new List<Model_AgentMappingDettails>();
				List<TblSrvcAgntMapDettail> lstTblSrvcAgentMapDetails = new List<TblSrvcAgntMapDettail>();
				String[] chkBxListService = null;
				if (Request.Form.GetValues("checkbox[]") !=null) //Check Box
				{
					chkBxListService = Request.Form.GetValues("checkbox[]");
					foreach(String itm in chkBxListService)
					{
						Model_AgentMappingDettails mdl = new Model_AgentMappingDettails();
						mdl.Service = Convert.ToInt32(itm);
						mdl.SrvcTypeID = mdlsrvcagnt.Service_Type_ID;
						mdl.TableSrvcMapID = mId;
						lstModelMappingDetails.Add(mdl);
					}
					lstTblSrvcAgentMapDetails = new Utility().ConvertList<Model_AgentMappingDettails, TblSrvcAgntMapDettail>(lstModelMappingDetails);
				}
				using (var context2 = new SMSEntities()) //Save or Add
				{
					foreach (var item2 in lstTblSrvcAgentMapDetails)
					{
						context2.TblSrvcAgntMapDettails.Add(item2);
						context2.SaveChanges();
					}
				}

				#endregion

			}	
			
			//UPDATE An ITEM(Editor)

			else
			{

				#region Master -> Update
				List<ModelTblService_Agent_Mapping> modelserviceagentList = new List<Models.ModelTblService_Agent_Mapping>();
				modelserviceagentList.Add(mdlsrvcagnt);

				//Convert Our Model to Entity Model


				List<tblServiceAgentMapping> ListTableServiceAgentMapping = new Utility().ConvertList<ModelTblService_Agent_Mapping, tblServiceAgentMapping>(modelserviceagentList);
				using (var context = new SMSEntities())
				{
					foreach (var entity in ListTableServiceAgentMapping)
					{
						context.tblServiceAgentMappings.Attach(entity);
						context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
						context.SaveChanges();
					}
				}
			}
			#endregion


				#region Details ->  Search -> Delete-> Add


			List<Model_AgentMappingDettails> listModelMappingDetails = new List<Model_AgentMappingDettails>();
			List<TblSrvcAgntMapDettail> listTblSrvcAgentMapDetails = new List<TblSrvcAgntMapDettail>();
			String[] chkBoxListService = null;

			//Check Box
			if (Request.Form.GetValues("checkbox[]") != null)
			{
				chkBoxListService = Request.Form.GetValues("checkbox[]");
				foreach (String itm in chkBoxListService)
				{
					Model_AgentMappingDettails modelD = new Model_AgentMappingDettails();
					modelD.Service = Convert.ToInt32(itm);
					modelD.SrvcTypeID = mdlsrvcagnt.Service_Type_ID;
					modelD.TableSrvcMapID = mdlsrvcagnt.id;
					listModelMappingDetails.Add(modelD);
				}
				listTblSrvcAgentMapDetails = new Utility().ConvertList<Model_AgentMappingDettails, TblSrvcAgntMapDettail>(listModelMappingDetails);
			}


			
			using (var context2 = new SMSEntities())
			{
				int mid = mdlsrvcagnt.id;
				//Search
				List<TblSrvcAgntMapDettail> lstMapDetail = context2.TblSrvcAgntMapDettails.ToList().FindAll(x => x.TableSrvcMapID == mid);

				//Delete or Remove
				context2.TblSrvcAgntMapDettails.RemoveRange(lstMapDetail);
				context2.SaveChanges();
				

			}



			//Save or Add
			using (var context2 = new SMSEntities()) 
			{
				foreach (var item2 in listTblSrvcAgentMapDetails)
				{
					context2.TblSrvcAgntMapDettails.Add(item2);
					context2.SaveChanges();
				}
			}


			#endregion

			return RedirectToAction("Index");

			}

		public PartialViewResult ServiceTypeCeckBox(String ServicetypeID,String tblServcMappID="0")//Service Type ID like (spa)
		{
			
				List<Service_Table> lstSrvc = new List<Service_Table>();  //Initiated Entity Service TypeModel
				List<Service> lstMdlSrvc = new List<Service>();  //Initiated Our Model Service
				int srvctypid = Convert.ToInt32(ServicetypeID);
				using (var context = new SMSEntities())
				{
					lstSrvc = context.Service_Table.ToList().FindAll(x => x.Service_Type_Id == srvctypid); //Find all Services under provided Servce type id(like spa) 

				}

				lstMdlSrvc = new Utility().ConvertList<Service_Table, Service>(lstSrvc); //Convert Entity Service to our Model service

				List<Model_AgentMappingDettails> lstModelAgentMappDetails = new List<Model_AgentMappingDettails>();
				List<TblSrvcAgntMapDettail> lstTblMappDetail = new List<TblSrvcAgntMapDettail>();
				if(tblServcMappID!="0")
				{
					int mid = Convert.ToInt32(tblServcMappID);
					using (var context = new SMSEntities())
					{
						lstTblMappDetail = context.TblSrvcAgntMapDettails.ToList().FindAll(x => x.TableSrvcMapID == mid);
					}
					lstModelAgentMappDetails = new Utility().ConvertList<TblSrvcAgntMapDettail, Model_AgentMappingDettails>(lstTblMappDetail);
				}

				ViewBag.DetailsLst = lstModelAgentMappDetails;

				return PartialView(lstMdlSrvc); // Return to partial view with our Service Model
				
		}

	}
}
