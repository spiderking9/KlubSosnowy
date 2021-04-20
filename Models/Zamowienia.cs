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
        public int IdKlienta { get; set; }
        public DateTime DataDodania { get; set; }
        public virtual ICollection<PozycjeZamowienia> PozycjeZamowienia { get; set; }
    }
}
