using KlubSosnowy.Models;
using KlubSosnowy.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using System.Web;
using System.Web.Mvc;

namespace KlubSosnowy.Controllers
{

    public class ZamowieniaController : Controller
    {
        private ZamowieniaContext db = new ZamowieniaContext();
        private const string ZamowieniaSesjaKlucz = "zamowienieSesja";

        public ActionResult Index4()
        {
            ListaXml lista = new ListaXml();
            var www = lista.Execute3();
            ViewBag.Liczba = www.Count(w => w.Nip == "brak");

            ViewBag.Liczba2 = www.Count();
            ViewBag.Liczba3 = www.Count(w=>w.Opis.Contains("VB ELECTR"));
            
            return View(www);
        }
        // GET: Zamowienia

        public ActionResult Index()
        {
            List<PotrawyZamowieniaViewModel> potrawyZamowienia = new List<PotrawyZamowieniaViewModel>();


            List<PotrawyZamowieniaViewModel> potrawy = new List<PotrawyZamowieniaViewModel>();
            if (Session[ZamowieniaSesjaKlucz] != null)
                potrawyZamowienia = Session[ZamowieniaSesjaKlucz] as List<PotrawyZamowieniaViewModel>;
            //if (potrawyZamowienia.Count == 0)
            //{
            //    potrawyZamowienia.Add(new PotrawyZamowieniaViewModel { Ilosc = 2, KwotaRazem = 22, IdPotrawy=1, Cena = 22, Nazwa="dsfsd" });
            //    potrawyZamowienia.Add(new PotrawyZamowieniaViewModel { Ilosc = 4, KwotaRazem = 52, IdPotrawy=2, Cena = 11, Nazwa = "aaa" });
            //};
            potrawy = (from x in db.Potrawy
                   select new PotrawyZamowieniaViewModel
                   {
                       Ilosc = 0,
                       KwotaRazem = 0,
                       IdPotrawy = x.IdPotrawy,
                       Nazwa=x.Nazwa,
                       Cena=x.Cena
                   }).ToList();
            ViewBag.Razem = potrawyZamowienia.Sum(x => x.KwotaRazem);

            return View(potrawyZamowienia.Union(potrawy).DistinctBy(x => x.IdPotrawy).ToList());
        }
        // POST:  Potrawy/EdytujPotrawe/5
        [HttpPost]
        public ActionResult StatusZamowienia(List<PotrawyZamowieniaViewModel> listaZamowionychPotraw)
        {
            List<PotrawyZamowieniaViewModel> potrawyZamowienia = new List<PotrawyZamowieniaViewModel>();
            listaZamowionychPotraw.ForEach(x => potrawyZamowienia.Add(new PotrawyZamowieniaViewModel
            {
                IdPotrawy=x.IdPotrawy,
                Ilosc = x.Ilosc,
                KwotaRazem = x.Cena * x.Ilosc,
                Cena=x.Cena,
                Nazwa=x.Nazwa
            }));
            Session[ZamowieniaSesjaKlucz] = potrawyZamowienia;
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DodajZamowienie(int idPotrawy, int iloscPotrawy)
        {
            List<PotrawyZamowieniaViewModel> potrawyZamowienia = new List<PotrawyZamowieniaViewModel>();
            if (Session[ZamowieniaSesjaKlucz] != null)
            {
                potrawyZamowienia = Session[ZamowieniaSesjaKlucz] as List<PotrawyZamowieniaViewModel>;
            }
            //Potrawy potrawy = db.Potrawy.FirstOrDefault(x => x.IdPotrawy == idPotrawy);
            //if (potrawy == null) return HttpNotFound();

            PotrawyZamowieniaViewModel nowaPozycja = new PotrawyZamowieniaViewModel()
            {
                Ilosc = iloscPotrawy,
                IdPotrawy = idPotrawy,
                KwotaRazem = iloscPotrawy
            };
            potrawyZamowienia.Add(nowaPozycja);

            Session[ZamowieniaSesjaKlucz] = potrawyZamowienia;
            return RedirectToAction("Index");
        }
    }
}