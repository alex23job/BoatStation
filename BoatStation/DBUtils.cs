using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BoatStation
{
    class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "vh338.timeweb.ru";
            int port = 3306;
            string database = "cs64182_expiredf";
            string username = "cs64182_expiredf";
            string password = "k9YNqxKZ";

            return DBMySqlUtils.GetDBConnection(host, port, database, username, password);
        }
        public static MySqlConnection GetDBConnectionLH()
        {
            string host = "localhost";
            int port = 3306;
            string database = "cs64182_expiredf";
            string username = "root";
            string password = "";

            return DBMySqlUtils.GetDBConnection(host, port, database, username, password);
        }

        public static void AddUser(MyUser mu)
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            try
            {
                string sql = "INSERT INTO tbl_user(user_name, user_username, user_password, user_role)" + "values(@name, @email, @password, @rule)";
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;

                MySqlParameter nm_param = new MySqlParameter("@name", MySqlDbType.String, 45);
                nm_param.Value = mu.Name;
                cmd.Parameters.Add(nm_param);

                MySqlParameter em_param = new MySqlParameter("@email", MySqlDbType.String, 45);
                em_param.Value = mu.Email;
                cmd.Parameters.Add(em_param);

                MySqlParameter psw_param = new MySqlParameter("@password", MySqlDbType.String, 45);
                psw_param.Value = mu.Password;
                cmd.Parameters.Add(psw_param);

                MySqlParameter rul_param = new MySqlParameter("@rule", MySqlDbType.Int32, 4);
                rul_param.Value = mu.Rule;
                cmd.Parameters.Add(rul_param);

                int rowCount = cmd.ExecuteNonQuery();
                MessageBox.Show("Row Count affected = " + rowCount.ToString());
                //listBox1.Items.Add("Row Count affected = " + rowCount.ToString());

            }
            catch (Exception ex)
            {
                //listBox1.Items.Add("Error : " + ex);
                MessageBox.Show("Error : " + ex);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }
        }
    }

    class DBMySqlUtils
    {
        public static MySqlConnection GetDBConnection(string host, int port, string database, string username, string password)
        {
            // Connection String
            String connString = "Server=" + host + ";Database=" + database + ";port=" + port + ";User Id=" + username + ";password=" + password;

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }
    }

}
