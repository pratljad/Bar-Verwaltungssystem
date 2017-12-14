using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarverwaltungCL.IDManager
{
    public static class IDGeneratorReservierungen
    {
        private static int ID = 0;

        public static int getID()
        {
            ID++;
            return ID;
        }
    }
}
