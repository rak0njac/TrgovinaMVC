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
    public class racunsController : Controller
    {
        private dbSTREntities db = new dbSTREntities();

        // GET: racuns
        public ActionResult Index()
        {
            return View(db.racuns.ToList().Where(x => x.tipracuna == "Gotovinski"));
        }

        public ActionResult Reload(string id)
        {
            List<racun> list = new List<racun>();
            foreach (racun r in db.racuns)
            {
                if (r.tipracuna == id)
                    list.Add(r);
            }
            return PartialView("_racuni", list);
        }

        // GET: racuns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            racun racun = db.racuns.Find(id);
            if (racun == null)
            {
                return HttpNotFound();
            }
            return View(racun);
        }

        // GET: racuns/Create
        public ActionResult Create()
        {
            ViewBag.pib = new SelectList(db.kupacs, "pib", "naziv");
            return View();
        }

        [HttpPost]
        public ActionResult addStavka([Bind(Include = "stavkaracunas")] racun racun)
        {
            racun.stavkaracunas.Add(new stavkaracuna());
            return PartialView("StavkeRacuna", racun);
        }

        // POST: racuns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tipracuna, kupac, datvalute, stavkaracunas")] racun racun)
        {
            racun.godina = 1234;
            racun.db_hidden = false;
            racun.datizdavanja = DateTime.Now;
            racun.brracuna = 0;

            foreach (stavkaracuna sr in racun.stavkaracunas)
            {
                sr.artikal = db.artikals.Where(o => o.naziv == sr.artikal.naziv).FirstOrDefault();
                //Debug.WriteLine(sr.artikal.naziv);
                //Debug.WriteLine(sr.idartikal);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.racuns.Add(racun);
                    //foreach (stavkaracuna s in vm.stavke)
                    //{
                    //    s.idracun = 1;
                    //    db.stavkaracunas.Add(s);
                    //}
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var errors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in errors.ValidationErrors)
                        {
                            // get the error message 
                            string errorMessage = validationError.ErrorMessage;
                            Debug.WriteLine(errorMessage);
                        }
                    }
                }
            }

            ViewBag.pib = new SelectList(db.kupacs, "pib", "naziv", racun.pib);
            return View(racun);
        }

        // GET: racuns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            racun racun = db.racuns.Find(id);
            if (racun == null)
            {
                return HttpNotFound();
            }
            ViewBag.pib = new SelectList(db.kupacs, "pib", "naziv", racun.pib);
            return View(racun);
        }

        // POST: racuns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idracun,brracuna,tipracuna,godina,datizdavanja,datvalute,ukupnacena,db_hidden,pib")] racun racun)
        {
            if (ModelState.IsValid)
            {
                db.Entry(racun).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pib = new SelectList(db.kupacs, "pib", "naziv", racun.pib);
            return View(racun);
        }

        // GET: racuns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            racun racun = db.racuns.Find(id);
            if (racun == null)
            {
                return HttpNotFound();
            }
            return View(racun);
        }

        // POST: racuns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            racun racun = db.racuns.Find(id);
            db.racuns.Remove(racun);
            db.SaveChanges();
            return RedirectToAction("Index");
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
