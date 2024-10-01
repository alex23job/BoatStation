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
        public static string[] TypeStatus = { "OK", "TO", "Ремонт", "Del"}; 
        public int BoatID { get; }
        public string BoatName { get; }
        public string Description { get; }
        public string SerialNumber { get; }
        public int BoatNumStatus { get { return status; } }
        public string Status { get { return TypeStatus[status]; } }
        int status = 0;

        public Boat() { }
        public Boat(int id, string nm, string descr, string sn, int st = 0)
        {
            BoatID = id;
            BoatName = nm;
            Description = descr;
            SerialNumber = sn;
            status = st;
        }

        public override string ToString()
        {
            return $"BoatID {BoatID:D04} {BoatName, -20} {SerialNumber, -15} {Description} {Status}";
        }

        public void SetStatus(int st)
        {
            if (st >= 0 && st < 4) status = st;
        }

        public bool Compare(Boat bt)
        {
            if (bt.BoatName != BoatName) return false;
            if (bt.BoatNumStatus != status) return false;
            if (bt.Description != Description) return false;
            if (bt.SerialNumber != SerialNumber) return false;
            return true;
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
                int id, st;
                string name, sn, descr;

                while (reader.Read())
                {
                    int.TryParse(reader[0].ToString(), out id);
                    name = reader[1].ToString();
                    descr = reader[2].ToString();
                    sn = reader[3].ToString();
                    int.TryParse(reader[4].ToString(), out st);
                    list.Add(new Boat(id, name, descr, sn, st));
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

        public static Boat GetBoat(int boat_id)
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            Boat resBoat = null;
            try
            {
                string sql = "SELECT * FROM tbl_boat WHERE id = @id";
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id", boat_id);
                MySqlDataReader reader = cmd.ExecuteReader();
                int id, st;
                string name, sn, descr;

                while (reader.Read())
                {
                    int.TryParse(reader[0].ToString(), out id);
                    name = reader[1].ToString();
                    descr = reader[2].ToString();
                    sn = reader[3].ToString();
                    int.TryParse(reader[4].ToString(), out st);
                    resBoat = new Boat(id, name, descr, sn, st);
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

            return resBoat;
        }

        public static Boat CheckBoat(Boat bot)
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            Boat resBoat = null;
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
                    if (name == bot.BoatName && descr == bot.Description && sn == bot.SerialNumber)
                    {
                        resBoat = new Boat(id, name, descr, sn);
                    }
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

            return resBoat;
        }
    }
}
