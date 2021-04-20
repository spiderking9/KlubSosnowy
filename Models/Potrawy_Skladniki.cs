using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KlubSosnowy.Models
{
    public class Potrawy_Skladniki
    {
        [Key]
        public int IdPotrawy_Skladnika { get; set; }
        public int IdPotrawy { get; set; }
        public virtual Potrawy Potrawy { get; set; }
        public int IdSkladnika { get; set; }
        public virtual Skladniki Skladniki { get; set; }

        public double Ilosc { get; set; }
    }
}
