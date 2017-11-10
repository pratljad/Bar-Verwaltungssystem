using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarverwaltungCL.Barverwaltung // ID vom Konstruktur raustun und IDManager machen
{
    public enum StateRechnung
    {
        Payed,
        NotPayed
    };

    public class Rechnung
    {
        private int IDRechnung;
        private DateTime TimestampR;
        private StateRechnung stateRechnung;
        private List<Bestellung> allBestellungen;

        public Rechnung (int IDRechnung, DateTime TimestampR, StateRechnung stateRechnung)
        {
            this.IDRechnung = IDRechnung;
            this.TimestampR = TimestampR;
            this.stateRechnung = stateRechnung;

            this.allBestellungen = new List<Bestellung>();
        }

        public void addBestellung(Bestellung b)
        {
            this.allBestellungen.Add(b);
        }
    }
}
