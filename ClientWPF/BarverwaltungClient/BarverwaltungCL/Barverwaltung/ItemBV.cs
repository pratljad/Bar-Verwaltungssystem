using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarverwaltungCL.Barverwaltung
{
    public enum Typ
    {
        Getraenk,
        Essen,
        Shisha
    };

    public class ItemBV
    {
        //private int IDItem;
        public Typ TypItem { get; private set; }
        public string Bezeichnung { get; private set; }
        public double PreisItem { get; private set; }
        public List<Tabak> allTabak { get; private set; }

        public string allTabakLV
        {
            get
            {
                string returnValue = "";

                foreach (Tabak t in allTabak)
                {
                    if (returnValue == "")
                        returnValue = t.getBezeichnung();
                    else
                        returnValue += "," + t.getBezeichnung();
                }

                return returnValue;
            }

            private set { }
        }

        public ItemBV(Typ TypItem, string Bezeichnung, double PreisItem)
        {
            //this.IDItem = IDItem;
            this.TypItem = TypItem;
            this.Bezeichnung = Bezeichnung;
            this.PreisItem = PreisItem;

            allTabak = new List<Tabak>();
        }
        
        public void addTabak(Tabak t)
        {
            allTabak.Add(t);
        }
    }
}

