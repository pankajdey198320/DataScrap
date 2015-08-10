using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace DataAccessLayer
{
   public class DaUpdateUserDetails
    {



       public int Update(string address_line1, string address_line2, string address_line3, string user_id)
        {

            MySqlConnection mysqlcon = null;        
            MySqlCommand mysqlcmd = new MySqlCommand();
            int i = 0;       

            mysqlcon = DBUtils.CreateMySqlConnection();
           
                string query = @"update user_profile_tbl set address_line1=@address_line1,address_line2=@address_line2,address_line3=@address_line3
                                where user_id = '" + user_id + "'";

              

                mysqlcmd = new MySqlCommand(query, mysqlcon);

                mysqlcmd.Parameters.AddWithValue("@address_line1", address_line1);
                mysqlcmd.Parameters.AddWithValue("@address_line2", address_line2);
                mysqlcmd.Parameters.AddWithValue("@address_line3", address_line3);
                        

           
            try
            {
                i = mysqlcmd.ExecuteNonQuery();
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

            return i;
        }        
       
    }
}
