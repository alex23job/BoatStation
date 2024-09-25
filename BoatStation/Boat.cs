using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatStation
{
    public class Boat
    {
        public int BoatID { get; }
        public string BoatName { get; }
        public string Description { get; }
        public string SerialNumber { get; }

        public Boat() { }
        public Boat(int id, string nm, string descr, string sn)
        {
            BoatID = id;
            BoatName = nm;
            Description = descr;
            SerialNumber = sn;
        }
    }
}
