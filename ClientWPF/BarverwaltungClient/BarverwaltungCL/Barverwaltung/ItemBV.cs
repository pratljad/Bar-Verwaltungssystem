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
        private int IDItem;
        private Typ TypItem;
        private string Bezeichnung;
        private float PreisItem;
        private List<Tabak> allTabak;

        public ItemBV(int IDItem, Typ TypItem, string Bezeichnung, float PreisItem)
        {
            this.IDItem = IDItem;
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

