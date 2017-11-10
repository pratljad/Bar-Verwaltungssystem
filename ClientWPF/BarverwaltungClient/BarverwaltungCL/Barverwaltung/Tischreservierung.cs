using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarverwaltungCL.Barverwaltung
{
    class Tischreservierung
    {
        private DateTime TimestampReservierung;
        private List<int> Tischnummern;

        public Tischreservierung (DateTime TimestampReservierung)
        {
            this.TimestampReservierung = TimestampReservierung;
        }

        public void addTisch(int Tischnummer)
        {
            Tischnummern.Add(Tischnummer);
        }
    }
}
