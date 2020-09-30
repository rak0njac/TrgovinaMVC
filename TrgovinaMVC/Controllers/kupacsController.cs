using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
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
            return PartialView("FormCreate");
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
                List<kupac> kupci = db.kupacs.Where(x => x.pib == kupac.naziv && x.aktivan == true).ToList();
                if (kupci.Count > 0)
                {
                    TempData["status"] = "nameConflict";
                }

                else
                {
                    kupac.aktivan = true;
                    db.kupacs.Add(kupac);
                    TempData["status"] = "added";
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return PartialView("FormCreate", kupac);
        }

        // GET: kupacs/Edit/5
        public ActionResult Edit(int? id)
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
            return PartialView("FormEdit", kupac);
        }

        // POST: kupacs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idkupac,pib,naziv,adresa,brtel")] kupac kupac)
        {
            if (ModelState.IsValid)
            {
                kupac.aktivan = true;
                db.Entry(kupac).State = EntityState.Modified;
                db.SaveChanges();
                TempData["status"] = "edited";
                return RedirectToAction("Index");
            }
            return PartialView("FormEdit", kupac);
        }

        [HttpPost]
        public ActionResult Delete(int idKupac)
        {
            kupac kupac = db.kupacs.Find(idKupac);
            kupac.aktivan = false;
            db.Entry(kupac).State = EntityState.Modified;
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
