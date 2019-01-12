using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demo.Models;
using System.Web.Security;
using demo.Demo.Entity;
namespace demo.Controllers
{
    [Authorize]
    public class useraccountController : Controller
    {
        // GET: userAccount
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
			String logmessage = "";
			if(TempData["log"]!=null && TempData["log"].ToString()=="Failed")
			{
				logmessage = "UserName and Password is incorrect,Please try again";
			}
			ViewBag.LogMsg = logmessage;

            return View(new LoginUser());
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Signin(LoginUser logUsr, String returnUrl)
        {
			bool isLog = false;
			using (var contxt=new SMSEntities())
			{
				int count = contxt.users.ToList().FindAll(x => x.username == logUsr.UserName && x.password == logUsr.Password).Count();
				if(count>0)
				{
					isLog = true;
				}

			}

			if (isLog)
			{
				FormsAuthentication.SetAuthCookie(logUsr.UserName, false);
				return RedirectToAction("Index", "Dashboard");
			}
			else
			{
				TempData["log"] = "Failed";
				return RedirectToAction("Login");
				
			}
        }
        
        [AllowAnonymous]
        public ActionResult signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "user");
        }
    }
}