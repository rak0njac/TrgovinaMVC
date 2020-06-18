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
            return PartialView("Racuni", list);
        }

        // GET: racuns/Details/5
        public ActionResult Show(int? id)
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

        public ActionResult getNaziviKupaca()
        {
            List<string> nazivi = db.kupacs.Select(x => x.naziv).ToList();
            return Json(nazivi, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getNaziviArtikla()
        {
            List<string> nazivi = db.artikals.Select(x => x.naziv).ToList();
            return Json(nazivi, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult addStavka([Bind] racun racun)
        {
            racun.stavkaracunas.Add(new stavkaracuna());
            return new EmptyResult();
            //return PartialView("StavkeRacuna", racun);
        }

        // POST: racuns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tipracuna, kupac, datvalute, stavkaracunas")] racun racun)
        {
            racun.godina = DateTime.Now.Year;
            racun.db_hidden = false;
            racun.datizdavanja = DateTime.Now;
            racun.brracuna = db.racuns.Where(o => o.tipracuna == racun.tipracuna).OrderBy(o => o.brracuna).Select(o => o.brracuna).FirstOrDefault() + 1;
            racun.kupac = db.kupacs.Where(o => o.naziv == racun.kupac.naziv).FirstOrDefault();

            foreach (stavkaracuna sr in racun.stavkaracunas)
            {
                sr.artikal = db.artikals.Where(o => o.naziv == sr.artikal.naziv).FirstOrDefault();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.racuns.Add(racun);
                    db.SaveChanges();
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

            ViewBag.pib = new SelectList(db.kupacs, "pib", "naziv", racun.pib);
            return View(racun);
        }


        public ActionResult Delete(int? idRacun)
        {
            racun racun = db.racuns.Find(idRacun);
            racun.db_hidden = true;
            db.SaveChanges();
            return View(racun);
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
