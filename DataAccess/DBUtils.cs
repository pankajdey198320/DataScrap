using MySql.Data.MySqlClient;
using System;
using System.Data;


namespace DataAccessLayer
{
   class DBUtils
    {
        public static MySqlConnection CreateMySqlConnection()
        {
            MySqlConnection mysqlcon = null;
            try
            {
                string strCon = System.Configuration.ConfigurationManager.ConnectionStrings["sevagenie"].ConnectionString;
                mysqlcon = new MySqlConnection(strCon);
                mysqlcon.Open();
                if (mysqlcon.State == ConnectionState.Closed)
                {
                    mysqlcon.Open();
                }
            }
            catch (MySqlException exception)
            {
                Console.WriteLine(exception.Message);
            }
            return mysqlcon;
        }
    }
}
