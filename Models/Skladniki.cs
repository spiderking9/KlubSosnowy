using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace KlubSosnowy.Models
{
    public class Skladniki
    {
        [Key]
        public int IdSkladnika { get; set; }
        public string Nazwa { get; set; }
        public string JednostkaMiary { get; set; }
        public virtual ICollection<Potrawy_Skladniki> Potrawy_Skladniki { get; set; }
    }
}
