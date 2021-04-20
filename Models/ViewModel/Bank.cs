using Npoi.Mapper.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KlubSosnowy.Models.ViewModel
{
    public class Bank
    {
        [Column("T")]
        public string T { get; set; }
        [Column("Data")]
        public string Data { get; set; }
        [Column("Nazwa")]
        public string Nazwa { get; set; }
        [Column("Tytulem")]
        public string Tytulem { get; set; }
        [Column("Opis")]
        public string Opis { get; set; }
        [Column("Wyplata")]
        public string WyplataWPln { get; set; }
        [Column("id")]
        public string Id { get; set; }
    }
}