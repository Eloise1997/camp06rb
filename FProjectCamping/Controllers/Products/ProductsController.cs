using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FProjectCamping.Controllers
{
	public class ProductsController : Controller
	{
		// GET: Products
		public ActionResult Create()
		{
			return View();
		}
	}
}