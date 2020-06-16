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
            return View(db.artikals.ToList());
        }

        // GET: artikals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: artikals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idartikal,naziv,cena,jm,kolnastanju")] artikal artikal)
        {
            if (ModelState.IsValid)
            {
                artikal.db_hidden = false;
                db.artikals.Add(artikal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artikal);
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
            return View(artikal);
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
                return RedirectToAction("Index");
            }
            return View(artikal);
        }

        // GET: artikals/Delete/5
        public ActionResult Delete(int idArt)
        {
            artikal artikal = db.artikals.Find(idArt);
            artikal.db_hidden = true;
            db.SaveChanges();
            return View(artikal);
        }

        // POST: artikals/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    artikal artikal = db.artikals.Find(id);
        //    db.artikals.Remove(artikal);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
