using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BoatStation
{
    public class MyClient
    {
        MyUser user = null;
        public int ClientID { get; }
        public string Tlf { get; }
        public string Pasport { get; }
        public string FirstName { get; }
        public string SecondName { get; }
        public MyUser ClientUser { get { return user; } }

        public string TblClient { get { return $"{ClientID:D04}"; } }

        public MyClient() { }
        public MyClient(int id, string fnm, string snm, string tn, string pasp, MyUser mu)
        {
            ClientID = id;
            FirstName = fnm;
            SecondName = snm;
            Tlf = tn;
            Pasport = pasp;
            if (mu != null) user = new MyUser(mu.ID, mu.Rule, mu.Name, mu.Email, mu.Password);
        }

        public void SetUser(MyUser mu)
        {
            if (mu != null) user = new MyUser(mu.ID, mu.Rule, mu.Name, mu.Email, mu.Password);
        }

        public bool Compare(MyClient mc)
        {
            if (mc.FirstName != FirstName) return false;
            if (mc.SecondName != SecondName) return false;
            if (mc.Tlf != Tlf) return false;
            if (mc.Pasport != Pasport) return false;
            return mc.ClientUser.Compare(user);
        }

        public static MyClient GetClient(int userID)
        {
            MyUser user = MyUser.GetUser(userID);
            MyClient client = null;

            MySqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();

            try
            {
                string sql = "SELECT * FROM tbl_client";
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                MySqlDataReader reader = cmd.ExecuteReader();
                int id, user_id;
                string fname, sname, tlf, pasport;

                while (reader.Read())
                {
                    int.TryParse(reader[0].ToString(), out id); 
                    int.TryParse(reader[1].ToString(), out user_id);
                    fname = reader[2].ToString();
                    sname = reader[3].ToString();
                    tlf = reader[4].ToString();
                    pasport = reader[5].ToString();
                   
                    //string s = string.Format("id:{0} name:{1} email:{2} password:{3} rule:{4}", id, name, eml, password, rule);
                    //string s = string.Format("id:{0} name:{1} email:{2} password:{3} rule:{4}", reader[0], reader[1], reader[2], reader[3]);
                    //listBox1.Items.Add(s);
                    //MessageBox.Show(s);
                    if (user_id == userID)
                    {
                        //MessageBox.Show("yes => " + s);
                        client = new MyClient(id, fname, sname, tlf, pasport, user);
                        break;
                    }
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
            return client;
        }
    }
}
