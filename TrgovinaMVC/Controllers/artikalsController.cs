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

        // GET: artikals
        public ActionResult Index()
        {
            //TempData["status"] = "none";
            return View(db.artikals.ToList());
        }

        // GET: artikals/Create
        public ActionResult Create()
        {
            return PartialView("Form");
        }

        // POST: artikals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "naziv,cena,jm,kolnastanju")] artikal artikal)
        {

            artikal.db_hidden = false;
            if (ModelState.IsValid)
            {
                db.artikals.Add(artikal);
                db.SaveChanges();
                TempData["status"] = "added";

                return RedirectToAction("Index");
            }

            return PartialView("Form", artikal);
        }

        // GET: artikals/Edit/5
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
            return PartialView("Form", artikal);
        }

        // POST: artikals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idartikal,naziv,cena,jm,kolnastanju,db_hidden")] artikal artikal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artikal).State = EntityState.Modified;
                db.SaveChanges();
                TempData["status"] = "edited";
                return RedirectToAction("Index");
            }
            return PartialView("Form", artikal);
        }

        // GET: artikals/Delete/5
        public ActionResult Delete(int? idArt)
        {
            artikal artikal = db.artikals.Find(idArt);
            artikal.db_hidden = true;
            db.SaveChanges();
            return View(artikal);
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
