using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demo.Models;
namespace demo.Controllers
{
    public class UserController : Controller
    {

        // GET: User
        public ActionResult Index()
        {
            String currentuser = HttpContext.User.Identity.Name;

            ViewBag.UserLst = new UserInfo().getData();


            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if(ModelState.IsValid)
            {
                return View("Index");
            }
            else
            {
                return View(user);
            }
        }


    }
}