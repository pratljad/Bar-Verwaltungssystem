using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarverwaltungCL.Barverwaltung
{
    public class Song
    {
        private int IDSong;
        private string Titel;
        private string Interpret;

        public Song(int ID, string Titel, string Interpret)
        {
            this.IDSong = ID;
            this.Titel = Titel;
            this.Interpret = Interpret;
        }
    }
}
