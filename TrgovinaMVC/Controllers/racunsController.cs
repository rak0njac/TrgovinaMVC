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
            return View("Racun", racun);
        }

        public ActionResult Create()
        {
            ViewBag.kupci = db.kupacs.Where(x => x.aktivan).Select(x => new SelectListItem { Text = x.naziv, Value = x.idkupac.ToString() }).ToList();
            ViewBag.artikli = db.artikals.Where(x=>x.aktivan).Select(x => new SelectListItem { Text = x.naziv, Value = x.idartikal.ToString() }).ToList();
            return View();
        }

        public ActionResult getCenaPoJm(int? id)
        {
            artikal artikal = db.artikals.Where(x => x.idartikal == id && x.aktivan).FirstOrDefault();
            return Content(artikal.cena.ToString());
        }

        //[HttpPost]
        //public ActionResult addStavka([Bind] racun racun)
        //{
        //    racun.stavkaracunas.Add(new stavkaracuna());
        //    return new EmptyResult();
        //    //return PartialView("StavkeRacuna", racun);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tipracuna, idkupac, datvalute, stavkaracunas")] racun racun)
        {
            racun.datizdavanja = DateTime.Now;
            racun.brracuna = db.racuns.Where(o => o.tipracuna == racun.tipracuna).OrderByDescending(o => o.brracuna).Select(o => o.brracuna).FirstOrDefault() + 1;

            racun.ukupnacena = 0;

            foreach (stavkaracuna sr in racun.stavkaracunas)
            {
                sr.ukupnacena = sr.kolicina * sr.cenapojm;
                racun.ukupnacena += sr.ukupnacena;
            }

            if (ModelState.IsValid)
            {
                db.racuns.Add(racun);
                db.SaveChanges();
                TempData["status"] = "added";
                return RedirectToAction("Index");
            }

            return View(racun);
        }


        //public ActionResult Delete(int? id)
        //{
        //    racun racun = db.racuns.Find(id);
        //    db.stavkaracunas.RemoveRange(racun.stavkaracunas);
        //    db.racuns.Remove(racun);
        //    db.SaveChanges();
        //    return View(racun);
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
