using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KlubSosnowy.Models;

namespace KlubSosnowy.Controllers
{
    [Authorize(Roles = "Admin,Kucharz")]
    public class SkladnikiController : Controller
    {
        private ZamowieniaContext db = new ZamowieniaContext();

        // GET: Skladniki
        public ActionResult Index()
        {
            return View(db.Skladniki.ToList());
        }

        // GET: Skladniki/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skladniki skladniki = db.Skladniki.Find(id);
            if (skladniki == null)
            {
                return HttpNotFound();
            }
            return View(skladniki);
        }

        // GET: Skladniki/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Skladniki/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSkladnika,Nazwa,JednostkaMiary")] Skladniki skladniki)
        {
            if (ModelState.IsValid)
            {
                db.Skladniki.Add(skladniki);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(skladniki);
        }

        // GET: Skladniki/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skladniki skladniki = db.Skladniki.Find(id);
            if (skladniki == null)
            {
                return HttpNotFound();
            }
            return View(skladniki);
        }

        // POST: Skladniki/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSkladnika,Nazwa,JednostkaMiary")] Skladniki skladniki)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skladniki).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(skladniki);
        }

        // GET: Skladniki/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skladniki skladniki = db.Skladniki.Find(id);
            if (skladniki == null)
            {
                return HttpNotFound();
            }
            return View(skladniki);
        }

        // POST: Skladniki/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Skladniki skladniki = db.Skladniki.Find(id);
            db.Skladniki.Remove(skladniki);
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
