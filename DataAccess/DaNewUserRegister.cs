using MySql.Data.MySqlClient;
using System.Data;
using Model;
using System;
 
namespace DataAccessLayer
{
    public class DaNewUserRegister
    {

//        public int Add(string mobileno, string password, string Email_Address, string first_name, string last_name,string Gender,string date_of_birth, string pincode)
//        {

//                MySqlConnection mysqlcon = null;
//                int i=0;
//                DataTable dt = new DataTable();
//                clsUserDetails UserDetails = new clsUserDetails();

//                string query = @"Insert into user_profile_tbl(mobileno,password,Email_Address,first_name,last_name,Gender,date_of_birth,pincode)
//                                 Values
//                                 (@mobileno,@password,@Email_Address,@first_name,@last_name,@Gender,@date_of_birth,@pincode)";

//                mysqlcon = DBUtils.CreateMySqlConnection();
//                MySqlCommand mysqlcmd = new MySqlCommand(query, mysqlcon);

                
//                mysqlcmd.Parameters.AddWithValue("@mobileno", mobileno);
//                mysqlcmd.Parameters.AddWithValue("@password", password);
//                mysqlcmd.Parameters.AddWithValue("@Email_Address", Email_Address);
//                mysqlcmd.Parameters.AddWithValue("@first_name", first_name);
//                mysqlcmd.Parameters.AddWithValue("@last_name", last_name);
//                mysqlcmd.Parameters.AddWithValue("@Gender", Gender);
//                mysqlcmd.Parameters.AddWithValue("@date_of_birth", date_of_birth);
//                mysqlcmd.Parameters.AddWithValue("@pincode", pincode);
//                i = mysqlcmd.ExecuteNonQuery();
//                if (i >= 0)
//                {
//                    string query1 = @"select max(user_id) as user_id from user_profile_tbl";
//                    mysqlcon = DBUtils.CreateMySqlConnection();
//                    MySqlCommand mysqlcmd1 = new MySqlCommand(query1, mysqlcon);
//                    MySqlDataAdapter da = new MySqlDataAdapter(mysqlcmd1);
//                    da.Fill(dt);
//                    if (dt.Rows.Count > 0)
//                    {
//                        i = UserDetails.user_id = dt.Rows[0]["user_id"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["user_id"]);
//                        return i;
//                    }    
//                }

//                  mysqlcon.Close();

//                  return i;
//        }         



        public clsUserDetails Add(clsUserDetails newuser)
        {

            MySqlConnection mysqlcon = null;
            DataTable dt = new DataTable();

            clsUserDetails newuser2 = new clsUserDetails();

            string query = @"Insert into user_profile_tbl(mobileno,password,Email_Address,first_name,last_name,date_of_birth)
                                 Values
                                 (@mobileno,@password,@Email_Address,@first_name,@last_name,@date_of_birth)";


            mysqlcon = DBUtils.CreateMySqlConnection();
            MySqlCommand mysqlcmd = new MySqlCommand(query, mysqlcon);
            mysqlcmd.Parameters.AddWithValue("@mobileno", newuser.Mobileno);
            mysqlcmd.Parameters.AddWithValue("@password", newuser.Password);
            mysqlcmd.Parameters.AddWithValue("@Email_Address", newuser.Email_Address);
            mysqlcmd.Parameters.AddWithValue("@first_name", newuser.First_name);
            mysqlcmd.Parameters.AddWithValue("@last_name", newuser.Last_name);
            mysqlcmd.Parameters.AddWithValue("@date_of_birth", newuser.Date_of_birth);

            int j = mysqlcmd.ExecuteNonQuery();
            if (j >= 0)
            {
                string query1 = @"select max(user_id) as user_id from user_profile_tbl";
                mysqlcon = DBUtils.CreateMySqlConnection();
                MySqlCommand mysqlcmd1 = new MySqlCommand(query1, mysqlcon);
                MySqlDataAdapter da = new MySqlDataAdapter(mysqlcmd1);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    newuser.user_id = dt.Rows[0]["user_id"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["user_id"]);
                    return newuser2;
                }
            }

            mysqlcon.Close();

            return newuser2;
        }

    }
}
