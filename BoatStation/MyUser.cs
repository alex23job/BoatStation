using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BoatStation
{
    public class MyUser
    {
        public static string[] Rules = { "Клиент", "Контролёр", "Менеджер", "Администратор"};
        public int ID { get; }
        public int Rule { get; }
        public string Name { get; }

        public string Email { get; }
        public string Password { get; }

        public MyUser() { }
        public MyUser(int id, int rul, string nm)
        {
            ID = id;
            Rule = (rul >= 0 && rul < 4) ? rul : 0;
            Name = nm;
            Email = "";
            Password = "";
        }

        public MyUser(int id, int rul, string nm, string eml, string pass)
        {
            ID = id;
            Rule = (rul >= 0 && rul < 4) ? rul : 0;
            Name = nm;
            Email = eml;
            Password = pass;
        }

        public override string ToString()
        {
            return $"user_{ID} {Rules[Rule]} {Name}";
        }

        public static MyUser CheckUser(string email, string pass)
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            MyUser mu = null;
            try
            {
                string sql = "SELECT * FROM tbl_user";
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                MySqlDataReader reader = cmd.ExecuteReader();
                int id, rule;
                string name, eml, password;

                while (reader.Read())
                {
                    int.TryParse(reader[0].ToString(), out id);
                    //id = (int)reader[0];
                    name = reader[1].ToString();
                    eml = reader[2].ToString();
                    password = reader[3].ToString();
                    //rule = (int)reader[4];
                    int.TryParse(reader[4].ToString(), out rule);
                    string s = string.Format("id:{0} name:{1} email:{2} password:{3} rule:{4}", id, name, eml, password, rule);
                    //string s = string.Format("id:{0} name:{1} email:{2} password:{3} rule:{4}", reader[0], reader[1], reader[2], reader[3]);
                    //listBox1.Items.Add(s);
                    //MessageBox.Show(s);
                    if (eml == email && pass == password)
                    {
                        MessageBox.Show("yes => " + s);
                        mu = new MyUser(id, rule, name);
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

            return mu;
        }

        public static List<MyUser> GetUserList(string email, string pass)
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            List<MyUser> list = new List<MyUser>();
            try
            {
                string sql = "SELECT * FROM tbl_user";
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                MySqlDataReader reader = cmd.ExecuteReader();
                int id, rule;
                string name, eml, password;

                while (reader.Read())
                {
                    int.TryParse(reader[0].ToString(), out id);
                    name = reader[1].ToString();
                    eml = reader[2].ToString();
                    password = reader[3].ToString();
                    int.TryParse(reader[4].ToString(), out rule);
                    list.Add(new MyUser(id, rule, name, eml, password));
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
