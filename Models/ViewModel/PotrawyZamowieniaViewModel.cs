using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KlubSosnowy.Models.ViewModel
{
    public class PotrawyZamowieniaViewModel
    {
        public int IdPotrawy { get; set; }
        public int Ilosc { get; set; }
        public decimal KwotaRazem { get; set; }
        public string Nazwa { get; set; }
        public decimal Cena { get; set; }
    }
}
