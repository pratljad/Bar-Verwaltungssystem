using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarverwaltungCL.Barverwaltung
{
    public class Tabak
    {
        private int IDTabak;
        private string Bezeichnung;

        public Tabak(int IDTabak, string Bezeichnung)
        {
            this.IDTabak = IDTabak;
            this.Bezeichnung = Bezeichnung;
        }
    }
}
