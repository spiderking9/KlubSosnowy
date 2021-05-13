using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using MoreLinq;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KlubSosnowy.Models.ViewModel;
using KlubSosnowy.Models;

namespace KlubSosnowy.Controllers
{
    [Authorize(Roles = "Admin,Kucharz")]
    public class PotrawyController : Controller
    {
        private readonly ZamowieniaContext db = new ZamowieniaContext();
        [AllowAnonymous]
        // GET: Potrawy
        public ActionResult Index()
        {
            return View(db.Potrawy.ToList());
        }

        // GET: Potrawy/EdytujPotrawe/5
        public ActionResult EdytujPotrawe(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<PotrawySkladnikiViewModel> potrawySkladniki = (from ps in db.Potrawy_Skladniki
                                         join s in db.Skladniki
                                         on ps.IdSkladnika equals s.IdSkladnika
                                         join p in db.Potrawy
                                         on ps.IdPotrawy equals p.IdPotrawy
                                         where ps.IdPotrawy == id
                                         select new PotrawySkladnikiViewModel
                                         {
                                            IdPotrawy=ps.IdPotrawy,
                                            NazwaPotrawy=p.Nazwa,
                                            IdSkladnika=s.IdSkladnika,
                                            IdPotrawySkladnik=ps.IdPotrawy_Skladnika,
                                            NazwaSkladnika =s.Nazwa,
                                            IloscSkladnika=ps.Ilosc,
                                            JednostkaMiary=s.JednostkaMiary
                                         }).ToList();
            var potrawa = db.Potrawy.FirstOrDefault(x => x.IdPotrawy == id);
            ViewBag.NazwaPotrawy = potrawa.Nazwa;
            ViewBag.IdPotrawy = potrawa.IdPotrawy;

            if (potrawySkladniki == null)
            {
                return HttpNotFound();
            }
            return View(potrawySkladniki);
        }
        // POST:  Potrawy/EdytujPotrawe/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult EdytujPotrawe(List<Potrawy_Skladniki> potrawySkladniki)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in potrawySkladniki)
                {
                    db.Entry(item).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(potrawySkladniki);
        }

        // GET: Potrawy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Potrawy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPotrawy,Nazwa,Cena")] Potrawy potrawy)
        {
            if (ModelState.IsValid)
            {
                db.Potrawy.Add(potrawy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(potrawy);
        }

        // GET: Potrawy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Potrawy potrawy = db.Potrawy.Find(id);
            if (potrawy == null)
            {
                return HttpNotFound();
            }
            return View(potrawy);
        }

        // POST: Potrawy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPotrawy,Nazwa,Cena")] Potrawy potrawy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(potrawy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(potrawy);
        }


        // GET: Potrawy/AddSkladnik/5
        public ActionResult AddSkladnik(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<PotrawySkladnikiViewModel> skladniki = (from sk in db.Skladniki
                                                         where !(from sp in db.Potrawy_Skladniki
                                                                 where sp.IdPotrawy==id select sp.IdSkladnika).Contains(sk.IdSkladnika)
                                                          select new PotrawySkladnikiViewModel {
                                                              IdSkladnika = sk.IdSkladnika,
                                                              NazwaSkladnika = sk.Nazwa,
                                                              JednostkaMiary = sk.JednostkaMiary
                                                          }).ToList();

            if (skladniki == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPotrawy = id;
            return View(skladniki);
        }
        
        // POST: Potrawy/AddSkladnik/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult AddSkladnik(List<PotrawySkladnikiViewModel> potrawySkladniki)
        {
            List<Potrawy_Skladniki> potrSkladnikiConverted = (from post in potrawySkladniki
                                                              where post.IloscSkladnika !=0
                                                              select new Potrawy_Skladniki
                                           {
                                               IdPotrawy = post.IdPotrawy,
                                               IdSkladnika = post.IdSkladnika,
                                               Ilosc = post.IloscSkladnika,
                                           }).ToList();
            int idPotrawy = potrawySkladniki.FirstOrDefault().IdPotrawy;
            if (ModelState.IsValid)
            {
                foreach (var item in potrSkladnikiConverted)
                {
                    db.Entry(item).State = EntityState.Added;
                }
                db.SaveChanges();
                return RedirectToAction("EdytujPotrawe", new { id = idPotrawy });
            }
            return View(potrawySkladniki);
        }

        // GET: Potrawy/DeleteSkladnik/5
        public ActionResult DeleteSkladnik(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Potrawy_Skladniki potrawySkladniki = db.Potrawy_Skladniki.Find(id);
            if (potrawySkladniki == null)
            {
                return HttpNotFound();
            }
            return View(potrawySkladniki);
        }

        // POST: Potrawy/DeleteSkladnik/5
        [HttpPost, ActionName("DeleteSkladnik")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSkladnikConfirmed(int id)
        {
            Potrawy_Skladniki potrawySkladniki = db.Potrawy_Skladniki.Find(id);
            db.Potrawy_Skladniki.Remove(potrawySkladniki);
            db.SaveChanges();
            return RedirectToAction("EdytujPotrawe", new { id = potrawySkladniki.IdPotrawy });
        }

        // GET: Potrawy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Potrawy potrawy = db.Potrawy.Find(id);
            if (potrawy == null)
            {
                return HttpNotFound();
            }
            return View(potrawy);
        }

        // POST: Potrawy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Potrawy potrawy = db.Potrawy.Find(id);
            db.Potrawy.Remove(potrawy);
            IEnumerable<Potrawy_Skladniki> poSk = db.Potrawy_Skladniki.Where(x => x.IdPotrawy == id);
            foreach (var item in poSk)
            {
                db.Potrawy_Skladniki.Remove(item);
            }
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
