﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demo.Controllers
{
    public class InsturmentController : Controller
    {
        // GET: Insturment
        public ActionResult Index()
        {
            return View();
        }
		public ActionResult Create()
		{
			return View();
		}

	}
}