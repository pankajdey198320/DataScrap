using MySql.Data.MySqlClient;
using System;
using System.Data;
using Model;
using System.Collections.Generic;
using System.Globalization;

namespace DataAccessLayer
{
   public class DaGetUserDetails
    {


       public clsUserDetails Get(string user_id)
        {
            MySqlConnection mysqlcon = null;
            DataTable dt = new DataTable();
            clsUserDetails UserDetails = new clsUserDetails();

            try
            {
                string query = @"select  u.user_id,u.mobileno,u.user_type,u.Email_Address,u.first_name,u.last_name,
                DATE_FORMAT(u.date_of_birth,'%d-%m-%Y') as date_of_birth,u.age,u.address_line1,u.address_line2,u.address_line3,u.pincode,u.Gender,
                c.country_id,s.state_id,ct.city_id		        

                from user_profile_tbl u
                left outer join country_tbl c
                on u.country_id=c.country_id
                left outer join state_tbl s
                on u.state_id = s.state_id
                left outer join city_tbl ct 
                on u.city_id = ct.city_id
                
                where u.user_id ='" + user_id + "'";

                mysqlcon = DBUtils.CreateMySqlConnection();
                MySqlCommand mysqlcmd = new MySqlCommand(query, mysqlcon);

                MySqlDataAdapter da = new MySqlDataAdapter(mysqlcmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    UserDetails.user_id = dt.Rows[0]["user_id"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["user_id"]);
                    UserDetails.Mobileno = dt.Rows[0]["mobileno"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["mobileno"]);
                    UserDetails.First_name = dt.Rows[0]["first_name"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["first_name"]);
                    UserDetails.Last_name = dt.Rows[0]["last_name"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["last_name"]);
                    UserDetails.Email_Address = dt.Rows[0]["Email_Address"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["Email_Address"]);                 
                  
                 
                    UserDetails.Age = dt.Rows[0]["age"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["age"]);
                    UserDetails.User_type = dt.Rows[0]["user_type"] == DBNull.Value ? "U" : Convert.ToString(dt.Rows[0]["user_type"]);

                    UserDetails.Date_of_birth = dt.Rows[0]["Date_of_birth"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["Date_of_birth"]);
                    //if (dt.Rows[0]["date_of_birth"] != DBNull.Value)
                    //{
                    //    UserDetails.Date_of_birth = DateTime.Parse(dt.Rows[0]["date_of_birth"].ToString()).ToString();
                    //}
                    //else
                    //{
                    //    UserDetails.Date_of_birth = DateTime.Parse(System.DateTime.Now.ToString());
                    //}

                    UserDetails.Address_line1 = dt.Rows[0]["address_line1"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["address_line1"]);
                    UserDetails.Address_line2 = dt.Rows[0]["address_line2"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["address_line2"]);
                    UserDetails.Address_line3 = dt.Rows[0]["address_line3"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["address_line3"]);
                    UserDetails.Gender=dt.Rows[0]["Gender"] == DBNull.Value?"":Convert.ToString(dt.Rows[0]["Gender"]);
                    UserDetails.Pincode = dt.Rows[0]["pincode"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["pincode"]);
                    UserDetails.Country_id = dt.Rows[0]["country_id"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["country_id"]);
                    UserDetails.State_id = dt.Rows[0]["state_id"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["state_id"]);
                    UserDetails.City_id = dt.Rows[0]["city_id"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["city_id"]);
                    
                    

                    return UserDetails;

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
            return UserDetails;
        }       
    }
}
