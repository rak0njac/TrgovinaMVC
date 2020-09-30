using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrgovinaMVC.Models;

namespace TrgovinaMVC.Controllers
{
    public class artikalsController : Controller
    {
        private dbSTREntities db = new dbSTREntities();

        public ActionResult Index()
        {
            return View(db.artikals.ToList());
        }

        public ActionResult Create()
        {
            return PartialView("FormCreate");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "naziv,cena,jm,kolnastanju")] artikal artikal)
        {

            if (ModelState.IsValid)
            {
                List<artikal> artikli = db.artikals.Where(x => x.naziv == artikal.naziv && x.aktivan == true).ToList();
                if (artikli.Count > 0)
                {
                    TempData["status"] = "nameConflict";
                }

                else
                {
                    artikal.aktivan = true;
                    db.artikals.Add(artikal);
                    TempData["status"] = "added";
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return PartialView("FormCreate", artikal);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            artikal artikal = db.artikals.Find(id);
            if (artikal == null)
            {
                return HttpNotFound();
            }
            return PartialView("FormEdit", artikal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idartikal,naziv,cena,jm,kolnastanju")] artikal artikal)
        {
            if (ModelState.IsValid)
            {
                artikal.aktivan = true;
                db.Entry(artikal).State = EntityState.Modified;
                db.SaveChanges();
                TempData["status"] = "edited";
                return RedirectToAction("Index");
            }
            return PartialView("FormEdit", artikal);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            artikal artikal = db.artikals.Find(id);
            artikal.aktivan = false;
            db.Entry(artikal).State = EntityState.Modified;
            db.SaveChanges();
            return new EmptyResult();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
