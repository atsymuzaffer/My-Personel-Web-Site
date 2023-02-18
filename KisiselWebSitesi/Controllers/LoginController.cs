using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KisiselWebSitesi.Models.Classes;

namespace KisiselWebSitesi.Controllers
{
    public class LoginController : Controller
    {
        Context context = new Context();
        // GET: Login
       
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Admin ad)
        {
            var info = context.Admins.FirstOrDefault(
                x => x.kullaniciAdi == ad.kullaniciAdi && x.sifre == ad.sifre);
            if (info != null)
            {
                FormsAuthentication.SetAuthCookie(info.kullaniciAdi,false);
                Session["kullaniciAdi"] = info.kullaniciAdi.ToString();
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View();
            }
        }

        public ActionResult logOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}