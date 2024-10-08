﻿using MySql.Data.MySqlClient;
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
                //MessageBox.Show("Row Count affected = " + rowCount.ToString());
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

        public static void UpdateUser(MyUser mu)
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            try
            {
                //string sql = "INSERT INTO tbl_user(user_name, user_username, user_password, user_role)" + "values(@name, @email, @password, @rule)";
                string sql = "UPDATE tbl_user SET user_name = @name, user_username = @email, user_password = @password, user_role = @rule WHERE user_id = @boid";
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

                MySqlParameter id_param = new MySqlParameter("@boid", MySqlDbType.Int32, 4);
                id_param.Value = mu.ID;
                cmd.Parameters.Add(id_param);

                int rowCount = cmd.ExecuteNonQuery();
                //MessageBox.Show("Row Count affected = " + rowCount.ToString());
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

        public static void DelUser(MyUser mu)
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            try
            {
                string sql = "DELETE FROM tbl_user WHERE user_id = @boid";
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;

                MySqlParameter id_param = new MySqlParameter("@boid", MySqlDbType.Int32, 4);
                id_param.Value = mu.ID;
                cmd.Parameters.Add(id_param);

                int rowCount = cmd.ExecuteNonQuery();
                //MessageBox.Show("Row Count affected = " + rowCount.ToString());
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

        public static void AddClient(MyClient mc)
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            try
            {
                string sql = "INSERT INTO tbl_client(user_ID, first_name, second_name, tlf, passport)" + "values(@uid, @fname, @sname, @tel, @pasport)";
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;

                MySqlParameter id_param = new MySqlParameter("@uid", MySqlDbType.Int32, 4);
                id_param.Value = mc.ClientUser.ID;
                cmd.Parameters.Add(id_param);

                MySqlParameter fnm_param = new MySqlParameter("@fname", MySqlDbType.String, 20);
                fnm_param.Value = mc.FirstName;
                cmd.Parameters.Add(fnm_param);

                MySqlParameter snm_param = new MySqlParameter("@sname", MySqlDbType.String, 20);
                snm_param.Value = mc.SecondName;
                cmd.Parameters.Add(snm_param);

                MySqlParameter tlf_param = new MySqlParameter("@tel", MySqlDbType.String, 15);
                tlf_param.Value = mc.Tlf;
                cmd.Parameters.Add(tlf_param);

                MySqlParameter pasp_param = new MySqlParameter("@pasport", MySqlDbType.String, 12);
                pasp_param.Value = mc.Pasport;
                cmd.Parameters.Add(pasp_param);

                int rowCount = cmd.ExecuteNonQuery();
                //MessageBox.Show("Row Count affected = " + rowCount.ToString());
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
        public static void UpdateClient(MyClient mc)
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            try
            {
                //string sql = "INSERT INTO tbl_client(user_ID, first_name, second_name, tlf, passport)" + "values(@uid, @fname, @sname, @tel, @pasport)";
                string sql = "UPDATE tbl_client SET user_ID = @uid, first_name = @fname, second_name = @sname, tlf = @tel, passport = @pasport WHERE id = @boid";
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;

                MySqlParameter uid_param = new MySqlParameter("@uid", MySqlDbType.Int32, 4);
                uid_param.Value = mc.ClientUser.ID;
                cmd.Parameters.Add(uid_param);

                MySqlParameter fnm_param = new MySqlParameter("@fname", MySqlDbType.String, 20);
                fnm_param.Value = mc.FirstName;
                cmd.Parameters.Add(fnm_param);

                MySqlParameter snm_param = new MySqlParameter("@sname", MySqlDbType.String, 20);
                snm_param.Value = mc.SecondName;
                cmd.Parameters.Add(snm_param);

                MySqlParameter tlf_param = new MySqlParameter("@tel", MySqlDbType.String, 15);
                tlf_param.Value = mc.Tlf;
                cmd.Parameters.Add(tlf_param);

                MySqlParameter pasp_param = new MySqlParameter("@pasport", MySqlDbType.String, 12);
                pasp_param.Value = mc.Pasport;
                cmd.Parameters.Add(pasp_param);

                MySqlParameter id_param = new MySqlParameter("@boid", MySqlDbType.Int32, 4);
                id_param.Value = mc.ClientID;
                cmd.Parameters.Add(id_param);

                int rowCount = cmd.ExecuteNonQuery();
                //MessageBox.Show("Row Count affected = " + rowCount.ToString());
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

        public static void DelClient(MyClient mc)
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            try
            {
                string sql = "DELETE FROM tbl_client WHERE id = @boid";
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;

                MySqlParameter id_param = new MySqlParameter("@boid", MySqlDbType.Int32, 4);
                id_param.Value = mc.ClientID;
                cmd.Parameters.Add(id_param);

                int rowCount = cmd.ExecuteNonQuery();
                //MessageBox.Show("Row Count affected = " + rowCount.ToString());
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

        public static void AddBoat(Boat bot)
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            try
            {
                string sql = "INSERT INTO tbl_boat(boat_name, description, serial_number, status)" + "values(@name, @descr, @sn, @st)";
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;

                MySqlParameter nm_param = new MySqlParameter("@name", MySqlDbType.String, 30);
                nm_param.Value = bot.BoatName;
                cmd.Parameters.Add(nm_param);

                MySqlParameter des_param = new MySqlParameter("@descr", MySqlDbType.String, 150);
                des_param.Value = bot.Description;
                cmd.Parameters.Add(des_param);

                MySqlParameter sn_param = new MySqlParameter("@sn", MySqlDbType.String, 20);
                sn_param.Value = bot.SerialNumber;
                cmd.Parameters.Add(sn_param);

                MySqlParameter st_param = new MySqlParameter("@st", MySqlDbType.Int32, 4);
                st_param.Value = bot.BoatNumStatus;
                cmd.Parameters.Add(st_param);

                int rowCount = cmd.ExecuteNonQuery();
                //MessageBox.Show("Boat adding! Row Count affected = " + rowCount.ToString());
                //listBox1.Items.Add("Row Count affected = " + rowCount.ToString());

            }
            catch (Exception ex)
            {
                //listBox1.Items.Add("Error : " + ex);
                MessageBox.Show("Boat adding => Error : " + ex);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }
        }

        public static void UpdateBoat(Boat bot)
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            try
            {
                string sql = "UPDATE tbl_boat SET boat_name = @name, description = @descr, serial_number =  @sn, status = @st WHERE id = @boid";
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;

                MySqlParameter nm_param = new MySqlParameter("@name", MySqlDbType.String, 30);
                nm_param.Value = bot.BoatName;
                cmd.Parameters.Add(nm_param);

                MySqlParameter des_param = new MySqlParameter("@descr", MySqlDbType.String, 150);
                des_param.Value = bot.Description;
                cmd.Parameters.Add(des_param);

                MySqlParameter sn_param = new MySqlParameter("@sn", MySqlDbType.String, 20);
                sn_param.Value = bot.SerialNumber;
                cmd.Parameters.Add(sn_param);

                MySqlParameter st_param = new MySqlParameter("@st", MySqlDbType.Int32, 4);
                st_param.Value = bot.BoatNumStatus;
                cmd.Parameters.Add(st_param);

                MySqlParameter id_param = new MySqlParameter("@boid", MySqlDbType.Int32, 4);
                id_param.Value = bot.BoatID;
                cmd.Parameters.Add(id_param);

                int rowCount = cmd.ExecuteNonQuery();
                //MessageBox.Show("Boat adding! Row Count affected = " + rowCount.ToString());
                //listBox1.Items.Add("Row Count affected = " + rowCount.ToString());

            }
            catch (Exception ex)
            {
                //listBox1.Items.Add("Error : " + ex);
                MessageBox.Show("Boat updating => Error : " + ex);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }
        }

        public static void AddBoatOrder(BoatOrder bo)
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            try
            {
                string sql = "INSERT INTO tbl_orders(date, boat_orders)" + "values(@dt, @orders)";
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;

                MySqlParameter dt_param = new MySqlParameter("@dt", MySqlDbType.Date, 10);
                dt_param.Value = bo.Date;
                cmd.Parameters.Add(dt_param);

                MySqlParameter ord_param = new MySqlParameter("@orders", MySqlDbType.String, 100);
                ord_param.Value = bo.GetCSV();
                cmd.Parameters.Add(ord_param);

                int rowCount = cmd.ExecuteNonQuery();
                //MessageBox.Show("BoatOrders adding! Row Count affected = " + rowCount.ToString());
                //listBox1.Items.Add("Row Count affected = " + rowCount.ToString());

            }
            catch (Exception ex)
            {
                //listBox1.Items.Add("Error : " + ex);
                MessageBox.Show("BoatOrders adding => Error : " + ex);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }
        }

        public static void UpdateBoatOrder(BoatOrder bo)
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            try
            {
                string sql = "UPDATE tbl_orders SET date = @dt, boat_orders = @orders WHERE id = @boid";
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;

                MySqlParameter dt_param = new MySqlParameter("@dt", MySqlDbType.Date, 10);
                dt_param.Value = bo.Date;
                cmd.Parameters.Add(dt_param);

                MySqlParameter ord_param = new MySqlParameter("@orders", MySqlDbType.String, 100);
                ord_param.Value = bo.GetCSV();
                cmd.Parameters.Add(ord_param);

                MySqlParameter id_param = new MySqlParameter("@boid", MySqlDbType.Int32, 4);
                id_param.Value = bo.OrderID;
                cmd.Parameters.Add(id_param);

                int rowCount = cmd.ExecuteNonQuery();
                //MessageBox.Show("BoatOrders updating! Row Count affected = " + rowCount.ToString());
                //listBox1.Items.Add("Row Count affected = " + rowCount.ToString());

            }
            catch (Exception ex)
            {
                //listBox1.Items.Add("Error : " + ex);
                MessageBox.Show("BoatOrders adding => Error : " + ex);
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
