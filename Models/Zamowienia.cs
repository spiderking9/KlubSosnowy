using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KlubSosnowy.Models
{
    public class Zamowienia
    {
        [Key]
        public int IdZamowienia { get; set; }
        public Guid IdKlienta { get; set; }
        public DateTime DataDodania { get; set; }
        public DateTime DataRealizacji { get; set; }
        public bool CzyZrealizowane { get; set; }
        public virtual ICollection<PozycjeZamowienia> PozycjeZamowienia { get; set; }
    }
}
