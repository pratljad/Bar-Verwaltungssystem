using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarverwaltungCL.Barverwaltung
{
    public class Tabak
    {
        // Überlegen wenn keine ID benötigt wird statt Tabak einfach string verwenden

        //private int IDTabak;
        private string Bezeichnung;

        public Tabak(string Bezeichnung)
        {
            //this.IDTabak = IDTabak;
            this.Bezeichnung = Bezeichnung;
        }

        public string getBezeichnung()
        {
            return Bezeichnung;
        }
    }
}
