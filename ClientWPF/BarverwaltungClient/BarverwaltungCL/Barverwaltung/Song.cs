using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarverwaltungCL.Barverwaltung
{
    public class Song
    {
        public int IDSong { get; private set; }
        public int Tischnummer { get; private set; }
        public string Titel { get; private set; }
        public string Interpret { get; private set; }

        public Song(int ID, int Tischnummer, string Titel, string Interpret)
        {
            this.IDSong = ID;
            this.Tischnummer = Tischnummer;
            this.Titel = Titel;
            this.Interpret = Interpret;
        }
    }
}
