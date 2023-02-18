using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KisiselWebSitesi.Models.Classes;

namespace KisiselWebSitesi.Controllers
{
    public class HomePageController : Controller
    {
        // GET: HomePage
        Context context = new Context();
        public ActionResult Index()
        {
            var deger = context.HomePages.ToList();
            return View(deger);
        }

        public PartialViewResult Icon()
        {
            var deger = context.Icons.ToList();
            return PartialView(deger);
        }
    }
}