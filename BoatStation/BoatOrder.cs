using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatStation
{
    public class BoatOrder
    {
        DateTime date;
        int boat_id;
        public int BoatID { get { return boat_id; } }
        public string[] hourOrders = new string[12]{ "", "", "", "", "", "", "", "", "", "", "", ""};

        public DateTime Date { get { return date; } }
        public string strDate { get { return $"{date.Day:D02}.{date.Month:D02}.{date.Year:D04}"; } }

        public BoatOrder() { }
        public BoatOrder(DateTime dt, int id)
        {
            date = dt;
            boat_id = id;
        }

        public BoatOrder(string csv, char sim = ';')
        {
            string[] ar = csv.Split(sim);
            if (ar.Length == 14)
            {
                string[] sdt = ar[0].Split('.');
                if (sdt.Length == 3)
                {
                    int d, m, y;
                    if (int.TryParse(sdt[0], out d) && int.TryParse(sdt[1], out m) && int.TryParse(sdt[2], out y)) date = new DateTime(y, m, d);
                }
                int.TryParse(ar[1], out boat_id);
                for (int i = 0; i < 12; i++) hourOrders[i] = ar[i + 2];
            }
        }

        public string GetCSV(string sim = ";")
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(strDate + sim);
            sb.Append($"{BoatID:D04}{sim}");
            for (int i = 0; i < 12; i++) sb.Append(hourOrders[i] + sim);
            return sb.ToString();
        }
    }
}
