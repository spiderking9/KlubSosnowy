using KlubSosnowy.Models;
using KlubSosnowy.Models.ViewModel;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using MoreLinq;
using System.Web;
using System.Web.Mvc;

namespace KlubSosnowy.Controllers
{
    public class ZamowieniaController : Controller
    {
        private readonly ZamowieniaContext db = new ZamowieniaContext();
        private const string ZamowieniaSesjaKlucz = "zamowienieSesja";

        public ZamowieniaController()
        {
            //var roleStore = new RoleStore<IdentityRole>(db);
            //var roleMenager = new RoleManager<IdentityRole>(roleStore);
            //var rolaAdmin = new IdentityRole { Name = "Kucharz" };
            //roleMenager.Create(rolaAdmin);
            //var rolaAdmin2 = new IdentityRole { Name = "Zakupowiec" };
            //roleMenager.Create(rolaAdmin2);
            //db.SaveChanges();



            //var userStore = new UserStore<ApplicationUser>(db);
            //var userManager = new UserManager<ApplicationUser>(userStore);
            //string haslo = "Sh!ft3891";
            //string email = "admin@klubsosnowy.com";
            //ApplicationUser uzytkownik = new ApplicationUser()
            //{
            //    UserName = email,
            //    Email = email,
            //    EmailConfirmed = true,
            //};
            //if (userManager.Create(uzytkownik, haslo).Succeeded)
            //{
            //    userManager.AddToRole(uzytkownik.Id, "Admin");
            //}
            //db.SaveChanges();
        }

        public ActionResult TestLinqWithViewFromSql()
        {
            int id = 2;
            var xx = db.Database.SqlQuery<string>("select * from PotrawyISkladniki",id).ToList();

            return View(xx);
        }
        public ActionResult Index4()
        {
            ListaXml lista = new ListaXml();
            var www = lista.Execute3();
            ViewBag.Liczba = www.Count(w => w.Nip == "brak");

            ViewBag.Liczba2 = www.Count();
            ViewBag.Liczba3 = www.Count(w => w.Opis.Contains("VB ELECTR"));

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
                           Nazwa = x.Nazwa,
                           Cena = x.Cena
                       }).ToList();
            ViewBag.Razem = potrawyZamowienia.Sum(x => x.KwotaRazem);

            return View(potrawyZamowienia.Union(potrawy).DistinctBy(x => x.IdPotrawy).ToList());
        }
        // POST:  Potrawy/EdytujPotrawe/5
        [HttpPost]
        public ActionResult StatusZamowienia(List<PotrawyZamowieniaViewModel> listaZamowionychPotraw)
        {
            List<PotrawyZamowieniaViewModel> potrawyZamowienia = new List<PotrawyZamowieniaViewModel>();
            listaZamowionychPotraw.ForEach(x =>
            {
                if (x.Ilosc > 0) potrawyZamowienia.Add(new PotrawyZamowieniaViewModel
                {
                    IdPotrawy = x.IdPotrawy,
                    Ilosc = x.Ilosc,
                    KwotaRazem = x.Cena * x.Ilosc,
                    Cena = x.Cena,
                    Nazwa = x.Nazwa
                });
            }
            );
            Session[ZamowieniaSesjaKlucz] = potrawyZamowienia;
            return View(potrawyZamowienia);
        }

        [HttpPost]
        public ActionResult Wyslij(List<PotrawyZamowieniaViewModel> potrawyZamowienia, DateTime date)
        {
            Guid userId = new Guid(User.Identity.GetUserId());
            try
            {
                var Zamowienie = db.Zamowienia.Add(new Zamowienia { IdKlienta = userId, DataDodania = DateTime.Now, DataRealizacji = date });
                potrawyZamowienia.ForEach(w =>
                {
                    if (w.Ilosc > 0)
                    {
                        db.PozycjeZamowienia.Add(new PozycjeZamowienia { IdPotrawy = w.IdPotrawy, IdZamowienia = Zamowienie.IdZamowienia, Ilosc = w.Ilosc });
                    }
                });
                db.SaveChanges();
                ViewBag.CzyPoszlo = Zamowienie.IdZamowienia;
            }
            catch (Exception ex)
            {

                throw ex.InnerException.InnerException;
            }


            return View();
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