using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarverwaltungCL.Barverwaltung;
using System.Windows.Shapes;
using System.Windows;

namespace BarverwaltungClient
{
    public class Table
    {
        private int tableNumber;
        private Rectangle tablePosition;
        private List<Bestellung> tableBestellungen;

        public Table(int TableNumber, Rectangle r)
        {
            this.tableNumber = TableNumber;
            tablePosition = r;
            tableBestellungen = new List<Bestellung>();
        }

        public int getTableNumber()
        {
            return tableNumber;
        }

        public Rectangle getTablePosition()
        {
            return tablePosition;
        }

        public List<Bestellung> getTableBestellungen()
        {
            return tableBestellungen;
        }

        public void removeBestellung(int row)
        {
            tableBestellungen.RemoveAt(row);
        }
    }
}
