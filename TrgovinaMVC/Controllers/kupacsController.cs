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
    public class kupacsController : Controller
    {
        private dbSTREntities db = new dbSTREntities();

        // GET: kupacs
        public ActionResult Index()
        {
            return View(db.kupacs.ToList());
        }


        // GET: kupacs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: kupacs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pib,naziv,adresa,brtel")] kupac kupac)
        {
            if (ModelState.IsValid)
            {
                kupac.db_hidden = false;
                db.kupacs.Add(kupac);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kupac);
        }

        // GET: kupacs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kupac kupac = db.kupacs.Find(id);
            if (kupac == null)
            {
                return HttpNotFound();
            }
            return View(kupac);
        }

        // POST: kupacs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pib,naziv,adresa,brtel,db_hidden")] kupac kupac)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kupac).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kupac);
        }

        public ActionResult Delete(string idKupac)
        {
            kupac kupac = db.kupacs.Find(idKupac);
            kupac.db_hidden = true;
            db.SaveChanges();
            return View(kupac);
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
