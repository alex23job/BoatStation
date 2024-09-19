using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
