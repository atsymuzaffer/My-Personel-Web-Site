using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KisiselWebSitesi.Models.Classes;

namespace KisiselWebSitesi.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        Context context = new Context();

        public ActionResult Index()
        {
            var deger = context.HomePages.ToList();
            return View(deger);
        }

        public ActionResult HomePageList(int id)
        {
            var hlist = context.HomePages.Find(id);
            return View("HomePageList", hlist);
        }

        public ActionResult HomePageUpdate(HomePage x)
        {
            var hp = context.HomePages.Find(x.id);
            hp.isim = x.isim;
            hp.profil=x.profil;
            hp.unvan=x.unvan;
            hp.aciklama=x.aciklama;
            hp.iletisim=x.iletisim;
            context.SaveChanges();
            
            return RedirectToAction("Index");
        }

        public ActionResult iconList()
        {
            var deger = context.Icons.ToList();
            return View(deger);

        }
        [HttpGet]
        public ActionResult iconCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult iconCreate(Icon i)
        {
            context.Icons.Add(i);
            context.SaveChanges();
            return RedirectToAction("iconList");
        }

        public ActionResult iconFind(int id)
        {
            var il = context.Icons.Find(id);
            return View("iconFind", il);
        }

        public ActionResult iconUpdate(Icon x)
        {
            var iu = context.Icons.Find(x.id);
            iu.ikon = x.ikon;
            iu.link = x.link;
            context.SaveChanges();
            return RedirectToAction("iconList");
        }

        public ActionResult iconDelete(int id)
        {
            var delete = context.Icons.Find(id);
            context.Icons.Remove(delete);
            context.SaveChanges();
            return RedirectToAction("iconList");
        }
    }
}