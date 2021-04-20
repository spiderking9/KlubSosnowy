using Npoi.Mapper.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KlubSosnowy.Models.ViewModel
{
    public class Nip
    {
        [Column("Nr dokumentu")]
        public string NrDokumentu { get; set; }
        [Column("NIP")]
        public string NIP { get; set; }
        [Column("Kontrahent")]
        public string Kontrahent { get; set; }
    }
}