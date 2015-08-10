using MySql.Data.MySqlClient;
using System;
using System.Data;
using Model;

namespace DataAccessLayer
{
    public class DaLogin
    {


        public clsvalidatelogin Get(clsUserDetails validatelogin)
        {
            MySqlConnection mysqlcon = null;
            DataTable dt = new DataTable();

            clsvalidatelogin validate = new clsvalidatelogin();

            try
            {

                string query = @"Select user_id from user_profile_tbl where mobileno ='"
                               + validatelogin.Mobileno + "' and password= '" + validatelogin.Password + "'";

                mysqlcon = DBUtils.CreateMySqlConnection();
                MySqlCommand mysqlcmd = new MySqlCommand(query, mysqlcon);

                MySqlDataAdapter da = new MySqlDataAdapter(mysqlcmd);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    validate.User_id = dt.Rows[0]["user_id"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["user_id"]);

                    return validate;   
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
            return validate;  
        }
    }
}
