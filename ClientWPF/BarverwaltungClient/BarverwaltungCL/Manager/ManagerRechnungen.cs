using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarverwaltungCL.Barverwaltung;
using System.IO;
using System.Windows;
using Invoicer.Services;
using Invoicer.Models;

namespace BarverwaltungCL.Manager
{
    public static class ManagerRechnungen
    {
        private static List<Rechnung> allRechnungen;

        public static IList<Rechnung> getRechnungen()
        {
            return allRechnungen;
        }

        public static void init()
        {
            allRechnungen = new List<Rechnung>();
        }

        public static void addRechnung(Rechnung r)
        {
            allRechnungen.Add(r);
        }

        public static void removeRechnung(Rechnung r)
        {
            allRechnungen.Remove(r);
        }

        public static void removeRechnung(int ID)
        {
            foreach (Rechnung r in allRechnungen)
            {
                if (r.IDRechnung == ID)
                {
                    allRechnungen.Remove(r);
                }
            }
        }

        public static void saveToFiles()
        {
            try
            {
                if (Directory.Exists(@"..\..\..\Bills"))
                {
                    Directory.Delete(@"..\..\..\Bills", true);
                }

                if (!Directory.Exists(@"..\..\..\Bills"))
                {
                    Directory.CreateDirectory(@"..\..\..\Bills");
                }

                foreach (Rechnung r in allRechnungen)
                {
                    List<ItemRow> listRow = new List<ItemRow>();
                    List<TotalRow> listTotalRow = new List<TotalRow>();

                    foreach (Bestellung b in r.allBestellungen)
                    {
                        foreach (ItemBV i in b.allItems())
                        {
                            listRow.Add(ItemRow.Make(i.Bezeichnung, Convert.ToString(i.TypItem), 1, Convert.ToDecimal(i.PreisItem * 0.2), Convert.ToDecimal(i.PreisItem * 0.8), Convert.ToDecimal(i.PreisItem)));
                        }
                    }

                    listTotalRow.Add(TotalRow.Make("Sub Total", Convert.ToDecimal(r.PreisRechnung * 0.8)));
                    listTotalRow.Add(TotalRow.Make("Mwst. 20%", Convert.ToDecimal(r.PreisRechnung * 0.2)));
                    listTotalRow.Add(TotalRow.Make("Total", Convert.ToDecimal(r.PreisRechnung)));

                    new InvoicerApi(SizeOption.A4, OrientationOption.Landscape, "€")
                        .TextColor("#294C73")
                        .BackColor("#7EFFFF")
                        .Image(@"..\..\images\hookah.png", 125, 27)
                        .Company(Address.Make("FROM", new string[] { "Team A", "Afritz am See", "Philipp Klaudrat", "Dragan Pratljacic", "Tician Hauswirth" }))
                        .Client(Address.Make("DETAILS", new string[] { "ID: " + r.IDRechnung, r.TimestampR.ToString(), "", "", "" }))
                        .Items(listRow)
                        .Totals(listTotalRow)
                        .Details(new List<DetailRow> {
                                    DetailRow.Make("PAYMENT INFORMATION", "No refund.", "", "If you have any questions concerning this invoice, contact our sales specialist Dragan Pratljacic.", "", "Thank you for your business.")
                        })
                        .Footer("http://htl-villach.at")
                        .Save(@"..\..\..\Bills\" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + r.IDRechnung, "password");
                }

                MessageBox.Show("All bills got saved.");
            }
            catch (Exception ex)
            {
                saveToFiles();
            }
        }
    }
}
