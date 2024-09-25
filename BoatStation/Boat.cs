using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public override string ToString()
        {
            return $"BoatID {BoatID:0000} {BoatName:000000000000} {SerialNumber:00000000} {Description}";
        }

        public static List<Boat> GetBoatList()
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            List<Boat> list = new List<Boat>();
            try
            {
                string sql = "SELECT * FROM tbl_boat";
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                MySqlDataReader reader = cmd.ExecuteReader();
                int id;
                string name, sn, descr;

                while (reader.Read())
                {
                    int.TryParse(reader[0].ToString(), out id);
                    name = reader[1].ToString();
                    descr = reader[2].ToString();
                    sn = reader[3].ToString();
                    list.Add(new Boat(id, name, descr, sn));
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
    }
}
