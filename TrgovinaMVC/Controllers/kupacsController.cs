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
            return PartialView("Form");
        }

        // POST: kupacs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pib,naziv,adresa,brtel")] kupac kupac)
        {
            kupac.db_hidden = false;
            if (ModelState.IsValid)
            {
                try
                {
                    db.kupacs.Add(kupac);
                    db.SaveChanges();
                    TempData["status"] = "added";

                    return RedirectToAction("Index");

                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var errors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in errors.ValidationErrors)
                        {
                            string errorMessage = validationError.ErrorMessage;
                            Debug.WriteLine(errorMessage);
                        }
                    }
                }
            }


            return PartialView("Form", kupac);
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
            return PartialView("Form", kupac);
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
                TempData["status"] = "edited";
                return RedirectToAction("Index");
            }
            return PartialView("Form", kupac);
        }

        public ActionResult Delete(string idKupac)
        {
            kupac kupac = db.kupacs.Find(idKupac);
            kupac.db_hidden = true;
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
