using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KlubSosnowy.Models.ViewModel
{
    public class PotrawySkladnikiViewModel
    {
        public int IdPotrawy { get; set; }
        public string NazwaPotrawy { get; set; }
        public int IdSkladnika { get; set; }
        public string NazwaSkladnika { get; set; }
        public int IdPotrawySkladnik { get; set; }
        public double IloscSkladnika { get; set; }
        public string JednostkaMiary { get; set; }

    }
}