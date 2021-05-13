using KlubSosnowy.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KlubSosnowy.Models
{
    public class SampleData : DropCreateDatabaseIfModelChanges<ZamowieniaContext>
    {
        protected override void Seed(ZamowieniaContext context)
        {
            context.Potrawy.AddRange(new List<Potrawy>
            {
                new Potrawy { Nazwa = "Sałatka jarzynowa", Cena = 45 },
                new Potrawy { Nazwa = "Jaja faszerowane musem z łososia", Cena = 8 },
                new Potrawy { Nazwa = "Domowy rosół z kury z lubczykiem i makaronem", Cena = 28 },
                new Potrawy { Nazwa = "Pierogi z kaczką", Cena = 29 },

            });

            context.Skladniki.AddRange(new List<Skladniki>
            {
                new Skladniki { Nazwa = "Jajka", JednostkaMiary="sztuki" },
                new Skladniki { Nazwa = "Marchewki", JednostkaMiary="kg" },
                new Skladniki { Nazwa = "Ziemniaki", JednostkaMiary="kg" },
                new Skladniki { Nazwa = "Łosoś", JednostkaMiary="kg" },
                new Skladniki { Nazwa = "Makaron", JednostkaMiary="kg" },
                new Skladniki { Nazwa = "Udko z kaczki", JednostkaMiary="kg" },
                new Skladniki { Nazwa = "Udko z Kurczaka", JednostkaMiary="kg" },
                new Skladniki { Nazwa = "Mąka", JednostkaMiary="kg" },
                new Skladniki { Nazwa = "Mleko", JednostkaMiary="litr" },
                new Skladniki { Nazwa = "Sól", JednostkaMiary="kg" }
            });
            context.SaveChanges();


            #region ROLE
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var rolaAdmin = new IdentityRole { Name = "Admin" };
            roleManager.Create(rolaAdmin);
            //var rolaZakupowiec = new IdentityRole { Name = "Zakupowiec" };
            //roleManager.Create(rolaZakupowiec);
            //var rolaKucharz = new IdentityRole { Name = "Kucharz" };
            //roleManager.Create(rolaKucharz);
            context.SaveChanges();
            #endregion

            #region Konto Administratora
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            string haslo = "Sh!ft3891";
            string email = "admin@klubsosnowy.com";
            ApplicationUser uzytkownik = new ApplicationUser()
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
            };
            if (userManager.Create(uzytkownik, haslo).Succeeded)
            {
                userManager.AddToRole(uzytkownik.Id, "Admin");
            }
            context.SaveChanges();
            #endregion

            //#region Konto Zakupowca
            //string hasloZakupowiec = "zakupowiec";
            //string emailZakupowiec = "zakupowiec@klubsosnowy.com";
            //ApplicationUser uzytkownikZakupowiec = new ApplicationUser()
            //{
            //    UserName = emailZakupowiec,
            //    Email = emailZakupowiec,
            //    EmailConfirmed = true,
            //};
            //if (userManager.Create(uzytkownikZakupowiec, hasloZakupowiec).Succeeded)
            //{
            //    userManager.AddToRole(uzytkownik.Id, "Zakupowiec");
            //}
            //context.SaveChanges();
            //#endregion


            //#region Konto Kucharza
            //string hasloKucharza = "kucharz";
            //string emailKucharz = "kucharz@klubsosnowy.com";
            //ApplicationUser uzytkownikKucharz = new ApplicationUser()
            //{
            //    UserName = emailKucharz,
            //    Email = emailKucharz,
            //    EmailConfirmed = true,
            //};
            //if (userManager.Create(uzytkownikKucharz, hasloKucharza).Succeeded)
            //{
            //    userManager.AddToRole(uzytkownik.Id, "Kucharz");
            //}
            //context.SaveChanges();
            //#endregion

        }
    }
}