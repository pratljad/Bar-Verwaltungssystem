using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarverwaltungCL.IDManager;

namespace BarverwaltungCL.Barverwaltung
{
    public class Tischreservierung
    {
        public DateTime TimestampReservierung { get; private set; }
        public int IDReservierung { get; private set; }
        public List<int> Tischnummern { get; private set; }
        public string TischnummernLV
        {
            get
            {
                string returnValue = "";

                foreach(int i in Tischnummern)
                {
                    if (returnValue == "")
                        returnValue = Convert.ToString(i);
                    else
                        returnValue += "," + i;
                }

                return returnValue;
            }

            private set { } 
        }

        public Tischreservierung (DateTime TimestampReservierung)
        {
            this.IDReservierung = IDGeneratorReservierungen.getID();
            this.TimestampReservierung = TimestampReservierung;

            Tischnummern = new List<int>();
        }

        public void addTischnummer(int Tischnummer)
        {
            Tischnummern.Add(Tischnummer);
        }
    }
}
