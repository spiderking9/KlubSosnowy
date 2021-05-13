using KlubSosnowy.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using KlubSosnowy.Models.ViewModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KlubSosnowy.Controllers
{
    public class MenagerZamowienController : Controller
    {
        private ZamowieniaContext db = new ZamowieniaContext();
        public ActionResult Index(bool czyZrealizowane = false)
        {
            ViewBag.CzyZrealizowane = czyZrealizowane;
            return View((from xx in db.Zamowienia
                         join yy in db.Users
                         on xx.IdKlienta equals new Guid(yy.Id)
                         select new ZamowieniaKlient
                         {
                             IdZamowienia=xx.IdZamowienia,
                             IdKlienta=xx.IdKlienta,
                             NazwaKlienta=yy.UserName,
                             CzyZrealizowane=xx.CzyZrealizowane,
                             DataDodania=xx.DataDodania,
                             DataRealizacji=xx.DataRealizacji
                         }).ToList());
        }
        public ActionResult SzczegolyWszyscy(int id)
        {
            ViewBag.IdZamowienia = id;
            var lista = (from xx in db.PozycjeZamowienia
                         join yy in db.Potrawy
                         on xx.IdPotrawy equals yy.IdPotrawy
                         where xx.IdZamowienia == id
                         select new PotrawyZamowieniaViewModel
                         {
                             Cena = yy.Cena,
                             IdPotrawy = xx.IdPotrawy,
                             Ilosc = xx.Ilosc,
                             Nazwa = yy.Nazwa
                         }).ToList();
            return View(lista);
        }
        public ActionResult PokazMojeZamowienia(bool czyZrealizowane = false)
        {
            ViewBag.CzyZrealizowane = czyZrealizowane;
            if (User.Identity.GetUserId()==null)
            {
                return RedirectToAction("Login","Account");
            }
            Guid userId = new Guid(User.Identity.GetUserId());
            return View(db.Zamowienia.Where(x => x.IdKlienta == userId && x.CzyZrealizowane == czyZrealizowane).ToList());
        }
        public ActionResult Szczegoly(int id)
        {

            ViewBag.IdZamowienia = id;
            var lista = (from xx in db.PozycjeZamowienia
                         join yy in db.Potrawy
                         on xx.IdPotrawy equals yy.IdPotrawy
                         where xx.IdZamowienia == id 
                         select new PotrawyZamowieniaViewModel
                         {
                             Cena = yy.Cena,
                             IdPotrawy = xx.IdPotrawy,
                             Ilosc = xx.Ilosc,
                             Nazwa = yy.Nazwa
                         }).ToList();
            return View(lista);
        }


    }
}