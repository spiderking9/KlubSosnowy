using KlubSosnowy.Models;
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
        }
    }
}