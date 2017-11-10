using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarverwaltungCL.Barverwaltung
{
    public enum StateBestellung
    {
        Done,
        NotDone
    };

    class Bestellung
    {
        private int IDBestellung;
        private DateTime TimestampB;
        private StateBestellung stateBestellung;
        private List<ItemBV> allItems;
        
        public Bestellung(int IDBestellung, DateTime TimestampB, StateBestellung stateBestellung)
        {
            this.IDBestellung = IDBestellung;
            this.TimestampB = TimestampB;
            this.stateBestellung = stateBestellung;

            allItems = new List<ItemBV>();
        } 

        public void addItem(ItemBV I)
        {
            allItems.Add(I);
        }
    }
}
