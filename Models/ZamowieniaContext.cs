using System;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KlubSosnowy.Models
{
    public class ZamowieniaContext : IdentityDbContext<ApplicationUser>
    {
        public ZamowieniaContext() : base("name=ZamowieniaContext") { }
        public static ZamowieniaContext Create()
        {
            return new ZamowieniaContext();
        }
        public DbSet<Potrawy> Potrawy { get; set; }
        public DbSet<Potrawy_Skladniki> Potrawy_Skladniki { get; set; }
        public DbSet<PozycjeZamowienia> PozycjeZamowienia { get; set; }
        public DbSet<Skladniki> Skladniki { get; set; }
        public DbSet<Zamowienia> Zamowienia { get; set; }
    }
}
