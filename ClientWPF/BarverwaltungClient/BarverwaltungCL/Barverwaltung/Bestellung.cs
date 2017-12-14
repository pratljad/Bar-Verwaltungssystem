using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarverwaltungCL.IDManager;

namespace BarverwaltungCL.Barverwaltung
{
    public enum StateBestellung
    {
        Done,
        NotDone
    };

    public class Bestellung
    {
        public int IDBestellung { get; private set; }   // Funktionen getIDBestellung, etc. entfernen und alle auf Property umschreiben
        public DateTime TimestampB { get; private set; }
        public int Tischnummer { get; set; }
        public StateBestellung stateBestellung { get; private set; }

        private List<ItemBV> _allItems;

        public double PreisBestellung
        {
            get
            {
                double returnValue = 0;

                foreach(ItemBV item in _allItems)
                {
                    returnValue += item.PreisItem;
                }

                return returnValue;
            }

            private set { }
        }

        public IList<ItemBV> allItems()
        {
            return this._allItems;
        }
        
        public Bestellung(DateTime TimestampB)
        {
            this.IDBestellung = IDGeneratorBestellungen.getID();
            this.TimestampB = TimestampB;
            this.stateBestellung = StateBestellung.NotDone;

            _allItems = new List<ItemBV>();
        } 

        public void addItem(ItemBV I)
        {
            _allItems.Add(I);
        }

        public int getIDBestellung()
        {
            return IDBestellung;
        }

        public DateTime getTimestamp()
        {
            return TimestampB;
        }

        public StateBestellung getStateBestellung()
        {
            return stateBestellung;
        }
    }
}
