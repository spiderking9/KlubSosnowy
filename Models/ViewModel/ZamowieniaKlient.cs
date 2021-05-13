using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KlubSosnowy.Models.ViewModel
{
    public class ZamowieniaKlient
    {
        public int IdZamowienia { get; set; }
        public Guid IdKlienta { get; set; }
        public DateTime DataDodania { get; set; }
        public DateTime DataRealizacji { get; set; }
        public bool CzyZrealizowane { get; set; }
        public string NazwaKlienta { get; set; }
    }
}