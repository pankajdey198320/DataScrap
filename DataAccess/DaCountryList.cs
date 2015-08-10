using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace DataAccessLayer
{
   public class DaCountryList
    {



       public List<clsCountryList> Get()
        {
            MySqlConnection mysqlcon = null;
            DataTable dt = new DataTable();

            List<clsCountryList> LICountrylist = new List<clsCountryList>();            

            try
            {

                string query = @"select country_id,country_name,country_code,country_isd_code from country_tbl order by country_name";

                mysqlcon = DBUtils.CreateMySqlConnection();
                MySqlCommand mysqlcmd = new MySqlCommand(query, mysqlcon);

                MySqlDataAdapter da = new MySqlDataAdapter(mysqlcmd);
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    clsCountryList Countrylist = new clsCountryList(); 
                    Countrylist.Country_Id = dt.Rows[i]["country_id"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[i]["country_id"]);
                    Countrylist.Country_name = dt.Rows[i]["country_name"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[i]["country_name"]);
                    Countrylist.Country_code = dt.Rows[i]["country_code"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[i]["country_code"]);
                    Countrylist.Country_isd_code = dt.Rows[i]["country_isd_code"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[i]["country_isd_code"]);
                    LICountrylist.Add(Countrylist);
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
            return LICountrylist;
        }
   }
}
