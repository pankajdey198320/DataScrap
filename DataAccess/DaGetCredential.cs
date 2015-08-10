using Model;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace DataAccessLayer
{
    public class DaGetCredential
    {

        public int Get(string mobileno)
        {
            MySqlConnection mysqlcon = null;
            DataTable dt = new DataTable();
            clsUserDetails usercredential = new clsUserDetails();
            
            try
            {
                string query = @"select password,first_name from user_profile_tbl
                                 where mobileno= '" + mobileno +  "'";

                mysqlcon = DBUtils.CreateMySqlConnection();
                MySqlCommand mysqlcmd = new MySqlCommand(query, mysqlcon);

                MySqlDataAdapter da = new MySqlDataAdapter(mysqlcmd);
                da.Fill(dt);
                if(dt.Rows.Count>0)
                {
                    return 1;
                }               
            }

            catch (Exception ex)
            {
                Exception oException = ex.GetBaseException();
                throw new ApplicationException(oException.ToString());
            }
            finally
            {
                if (mysqlcon != null)
                {
                    if (mysqlcon.State != ConnectionState.Closed)
                        mysqlcon.Close();
                    mysqlcon.Dispose();
                }
            }
            return 0;
        }
    }
}
