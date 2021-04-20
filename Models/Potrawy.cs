using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace KlubSosnowy.Models
{
    public class Potrawy
    {
        [Key]
        public int IdPotrawy { get; set; }
        public string Nazwa { get; set; }
        public decimal Cena { get; set; }
        public virtual ICollection<Potrawy_Skladniki> Potrawy_Skladniki { get; set; }
        public virtual ICollection<PozycjeZamowienia> PozycjeZamowienia { get; set; }

    }
}
