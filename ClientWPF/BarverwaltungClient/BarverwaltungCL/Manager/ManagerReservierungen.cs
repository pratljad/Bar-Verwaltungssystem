using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarverwaltungCL.Barverwaltung;

namespace BarverwaltungCL.Manager
{
    public static class ManagerReservierungen 
    {
        private static List<Tischreservierung> Reservierungen;

        public static IList<Tischreservierung> getReservierungen()
        {
            return Reservierungen.AsReadOnly();
        }

        public static void init()
        {
            Reservierungen = new List<Tischreservierung>();
        }

        public static void addReservierung(Tischreservierung t)
        {
            Reservierungen.Add(t);
        }

        public static void removeReservierung(Tischreservierung t)
        {
            Reservierungen.Remove(t);
        }
    }
}
