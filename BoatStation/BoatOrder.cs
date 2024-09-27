using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                date = DateTime.Parse(ar[0]);
                /*string[] sdt = ar[0].Split('.');
                if (sdt.Length == 3)
                {
                    int d, m, y;
                    if (int.TryParse(sdt[0], out d) && int.TryParse(sdt[1], out m) && int.TryParse(sdt[2], out y)) date = new DateTime(y, m, d);
                }*/
                int.TryParse(ar[1], out boat_id);
                for (int i = 0; i < 12; i++) hourOrders[i] = ar[i + 2];
            }
        }

        public string GetCSV(string sim = ";")
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(date.ToString() + sim);
            sb.Append($"{BoatID:D04}{sim}");
            for (int i = 0; i < 12; i++) sb.Append(hourOrders[i] + sim);
            return sb.ToString();
        }

        public bool SetOrder(int h, int uid)
        {
            bool res = false;
            if (h >= 0 && h < 12)
            {
                if (hourOrders[h] == "")
                {
                    hourOrders[h] = $"{uid:D04}";
                    return true;
                }
            }
            return res;
        }

        public static List<BoatOrder> GetOrdersList()
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            List<BoatOrder> list = new List<BoatOrder>();
            try
            {
                string sql = "SELECT * FROM tbl_orders";
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                MySqlDataReader reader = cmd.ExecuteReader();
                int id;
                DateTime dt;
                string orders;

                while (reader.Read())
                {
                    int.TryParse(reader[0].ToString(), out id);
                    dt = DateTime.Parse(reader[1].ToString());
                    orders = reader[2].ToString();
                    list.Add(new BoatOrder(orders));
                    //string s = string.Format("id:{0} name:{1} email:{2} password:{3} rule:{4}", id, name, eml, password, rule);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex);
                //listBox1.Items.Add("Error : " + ex);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }

            return list;
        }

        public static List<OrderView> GetOrdersViewList(List<BoatOrder> lbo, DateTime date)
        {
            List<OrderView> list = new List<OrderView>();
            foreach (BoatOrder bo in lbo)
            {
                if (bo.Date.Day == date.Day && bo.Date.Month == date.Month && bo.Date.Year == date.Year) list.Add(new OrderView(bo));
            }
            return list;
        }
    }

    public class OrderView
    {
        public DateTime BoatDate { get; set; }
        public string BoatName { get; set; } = "";
        public string BoatNumber { get; set; }
        public string Hour09 { get; set; }
        public string Hour10 { get; set; }
        public string Hour11 { get; set; }
        public string Hour12 { get; set; }
        public string Hour13 { get; set; }
        public string Hour14 { get; set; }
        public string Hour15 { get; set; }
        public string Hour16 { get; set; }
        public string Hour17 { get; set; }
        public string Hour18 { get; set; }
        public string Hour19 { get; set; }
        public string Hour20 { get; set; }
        public OrderView(BoatOrder bo)
        {
            BoatDate = bo.Date;
            BoatNumber = $"{bo.BoatID:D04}";
            BoatName = Boat.GetBoat(bo.BoatID).BoatName;
            Hour09 = bo.hourOrders[0];
            Hour10 = bo.hourOrders[1];
            Hour11 = bo.hourOrders[2];
            Hour12 = bo.hourOrders[3];
            Hour13 = bo.hourOrders[4];
            Hour14 = bo.hourOrders[5];
            Hour15 = bo.hourOrders[6];
            Hour16 = bo.hourOrders[7];
            Hour17 = bo.hourOrders[8];
            Hour18 = bo.hourOrders[9];
            Hour19 = bo.hourOrders[10];
            Hour20 = bo.hourOrders[11];
        }
        public OrderView(string csvStr, string sep = ";")
        {
            string[] s = csvStr.Split(sep[0]);
            if (s.Length >= 14)
            {
                BoatDate = DateTime.Parse(s[0]);
                BoatNumber = s[1];
                Hour09 = s[2];
                Hour10 = s[3];
                Hour11 = s[4];
                Hour12 = s[5];
                Hour13 = s[6];
                Hour14 = s[7];
                Hour15 = s[8];
                Hour16 = s[9];
                Hour17 = s[10];
                Hour18 = s[11];
                Hour19 = s[12];
                Hour20 = s[13];
                if (int.TryParse(BoatNumber, out int id))
                {
                    BoatName = Boat.GetBoat(id).BoatName;
                }
            }
        }
    }
}
