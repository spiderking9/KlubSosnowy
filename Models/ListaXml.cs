using KlubSosnowy.Models.ViewModel;
using NPOI.SS.UserModel;
using System;
using System.Text.RegularExpressions;
using Npoi.Mapper;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using MoreLinq;

namespace KlubSosnowy.Models
{
    public class ListaXml
    {

        public List<Bank> Execute(string localPath, int sheetIndex)
        {
            IWorkbook workbook;
            using (FileStream file = new FileStream(localPath, FileMode.Open, FileAccess.Read))
            {
                workbook = WorkbookFactory.Create(file);
            }

            var importer = new Mapper(workbook);
            var items = importer.Take<Bank>(sheetIndex);
            List<Bank> lista = new List<Bank>();
            foreach (var item in items)
            {
                if (item.Value.Tytulem == null) continue;
                if (!item.Value.Tytulem.Contains('(')) 
                    lista.Add(new Bank { T = item.Value.T, Data = item.Value.Data, Nazwa = item.Value.Nazwa, Tytulem = item.Value.Tytulem, Opis = item.Value.Opis, WyplataWPln = item.Value.WyplataWPln, Id = item.Value.Id });
                else
                {
                    var www = item.Value.Tytulem.Split('(', ')');
                    for (int i = 0; i < www.Length - 1; i += 2)
                    {
                        lista.Add(new Bank { T = item.Value.T, Data = item.Value.Data, Nazwa = item.Value.Nazwa, Tytulem = www[i], Opis = item.Value.Opis, WyplataWPln = www[i + 1],Id= item.Value.Id });
                    }
                }
            }
            return lista;
        }

        public List<Nip> Execute2(string localPath, int sheetIndex)
        {
            IWorkbook workbook;
            using (FileStream file = new FileStream(localPath, FileMode.Open, FileAccess.Read))
            {
                workbook = WorkbookFactory.Create(file);
            }

            var importer = new Mapper(workbook);
            var items = importer.Take<Nip>(sheetIndex);
            List<Nip> lista = new List<Nip>();
            foreach (var item in items)
            {
                if (item.Value.NIP == null) continue;
                    lista.Add(new Nip { NIP = item.Value.NIP, NrDokumentu = item.Value.NrDokumentu,Kontrahent=item.Value.Kontrahent});

            }
            return lista;
        }
        public List<BankWithNip> Execute3()
        {
            ListaXml lista = new ListaXml();
            List<Bank> listNip = lista.Execute(@"D:\faktury.xlsx", 0);
            List<Nip> listBank = lista.Execute2(@"D:\faktury.xlsx", 1);

            List<BankWithNip> listaBankNip = (from xx in listNip
                                              join zz in listBank 
                                              on xx.Nazwa equals 
                                              zz.Kontrahent
                                              into jts
                                              from jtResult in jts.DefaultIfEmpty()
                                              select new BankWithNip
                                              {
                                                  T = xx.T,
                                                  Data = xx.Data.Substring(0, 10),
                                                  Nazwa = xx.Nazwa,
                                                  Tytulem = xx.Tytulem,
                                                  Opis = xx.Opis,
                                                  WyplataWPln = xx.WyplataWPln.Replace('.', ','),
                                                  Nip = jtResult==null?"brak":jtResult.NIP ,
                                                  Id = xx.Id
                                              }).DistinctBy(w=>w.Tytulem).ToList();
            listaBankNip.Where(x => x.Nip == "brak").Select(w =>
            {
                if (listBank.FirstOrDefault(z => z.NrDokumentu == w.Tytulem || w.Opis.Contains(z.Kontrahent)) != null)
                    w.Nip = listBank.FirstOrDefault(z => z.NrDokumentu == w.Tytulem).NIP;
                else w.Nip = "brak";
                return w;
            }).ToList();


            return (from xx in listaBankNip
                   group xx by new BankWithNip
                   {
                       T = xx.T,
                       Data = xx.Data,
                       Nazwa = xx.Nazwa,
                       Tytulem = xx.Tytulem,
                       Opis = xx.Opis,
                       WyplataWPln = xx.WyplataWPln,
                       Nip = xx.Nip,
                       Id = xx.Id
                   } into grp
                   select grp.First()).ToList();
        }
    }
}