using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarverwaltungCL.IDManager;

namespace BarverwaltungCL.Barverwaltung // ID vom Konstruktur raustun und IDManager machen
{
    public enum StateRechnung
    {
        Payed,
        NotPayed
    };

    public class Rechnung
    {
        public int IDRechnung { get; set; }
        public DateTime TimestampR { get; private set; }
        public StateRechnung stateRechnung { get; private set; }
        public List<Bestellung> allBestellungen { get; private set; }

        public double PreisRechnung
        {
            get
            {
                double returnValue = 0;

                foreach (Bestellung b in allBestellungen)
                {
                    returnValue += b.PreisBestellung;
                }

                return returnValue;
            }

            private set { }
        }

        public Rechnung (DateTime TimestampR)
        {
            this.TimestampR = TimestampR;
            this.stateRechnung = StateRechnung.NotPayed;

            this.allBestellungen = new List<Bestellung>();
        }

        public void addBestellung(Bestellung b)
        {
            this.allBestellungen.Add(b);
        }
    }
}
