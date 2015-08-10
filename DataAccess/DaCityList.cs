using MySql.Data.MySqlClient;
using System;
using System.Data;
using Model;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public class DaCityList
    {


        public List<clsCityList> Get(string State_id)
        {
        
            MySqlConnection mysqlcon = null;
            DataTable dt = new DataTable();

            List<clsCityList> LIcitylist = new List<clsCityList>();          
             

            try
            {

                string query = @"select city_id,city_name,city_code from city_tbl where state_id= '"
                               + State_id + "' order by city_name";

                mysqlcon = DBUtils.CreateMySqlConnection();
                MySqlCommand mysqlcmd = new MySqlCommand(query, mysqlcon);

                MySqlDataAdapter da = new MySqlDataAdapter(mysqlcmd);
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    clsCityList citylist = new clsCityList();
                    
                    citylist.City_Id = dt.Rows[i]["city_id"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[i]["city_id"]);
                    citylist.City_name = dt.Rows[i]["city_name"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[i]["city_name"]);   


                    LIcitylist.Add(citylist);
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
            return LIcitylist;
        }
    }
}
