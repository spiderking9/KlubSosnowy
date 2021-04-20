using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KlubSosnowy.Models
{
    public class PozycjeZamowienia
    {
        [Key]
        public int IdPozycjeZamowienia { get; set; }
        public int IdZamowienia { get; set; }
        public virtual Zamowienia Zamowienia { get; set; }
        public int IdPotrawy { get; set; }
        public virtual Potrawy Potrawy { get; set; }
        public int Ilosc { get; set; }
    }
}
