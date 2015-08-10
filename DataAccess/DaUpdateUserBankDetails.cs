using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace DataAccessLayer
{
   public class DaUpdateUserBankDetails
    {
        

      
        public int Update(string bank_name, string address_1, string address_2, string address_3, string country_id, string state_id,
                          string city_id, string pincode, string contact_person_name, string contact_person_mobile,
                          string contact_person_email, string user_id)
        {

            MySqlConnection mysqlcon = null;
            int i = 0;

            DataTable dt = new DataTable();
            MySqlCommand mysqlcmd = new MySqlCommand();
            
            string get = @"Select user_id from user_bank_tbl  where user_id =  '" + user_id + "'";

            mysqlcon = DBUtils.CreateMySqlConnection();
            mysqlcmd = new MySqlCommand(get, mysqlcon);

            MySqlDataAdapter da = new MySqlDataAdapter(mysqlcmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                string update = @"update user_bank_tbl set bank_name=@bank_name, address_1=@address_1,address_2=@address_2,
			                address_3=@address_3,country_id=@country_id,state_id=@state_id,city_id=@city_id,
			                pincode=@pincode,contact_person_name=@contact_person_name,
			                contact_person_mobile=@contact_person_mobile,contact_person_email=@contact_person_email
                            where user_id = '" + user_id + "'";

                mysqlcmd = new MySqlCommand(update, mysqlcon);

                mysqlcmd.Parameters.AddWithValue("@bank_name", bank_name);
                mysqlcmd.Parameters.AddWithValue("@address_1", address_1);
                mysqlcmd.Parameters.AddWithValue("@address_2", address_2);
                mysqlcmd.Parameters.AddWithValue("@address_3", address_3);
                mysqlcmd.Parameters.AddWithValue("@country_id", country_id);
                mysqlcmd.Parameters.AddWithValue("@state_id", state_id);
                mysqlcmd.Parameters.AddWithValue("@city_id", city_id);
                mysqlcmd.Parameters.AddWithValue("@pincode", pincode);
                mysqlcmd.Parameters.AddWithValue("@contact_person_name", contact_person_name);
                mysqlcmd.Parameters.AddWithValue("@contact_person_mobile", contact_person_mobile);
                mysqlcmd.Parameters.AddWithValue("@contact_person_email", contact_person_email);

            }
            else
            {
                string add = @"Insert into user_bank_tbl(bank_name,address_1,address_2,address_3,country_id,state_id,city_id,pincode,
                               contact_person_name,contact_person_mobile,contact_person_email,user_id)
                               Values
                               (@bank_name,@address_1,@address_2,@address_3,@country_id,@state_id,@city_id,@pincode,@contact_person_name,
                               @contact_person_mobile,@contact_person_email,@user_id)";
                               
                mysqlcmd = new MySqlCommand(add, mysqlcon);

                mysqlcmd.Parameters.Add("@bank_name", MySqlDbType.VarChar).Value = bank_name;
                mysqlcmd.Parameters.Add("@address_1", MySqlDbType.VarChar).Value = address_1;
                mysqlcmd.Parameters.Add("@address_2", MySqlDbType.VarChar).Value = address_2;
                mysqlcmd.Parameters.Add("@address_3", MySqlDbType.VarChar).Value = address_3;
                mysqlcmd.Parameters.Add("@country_id", MySqlDbType.Int32).Value = country_id;
                mysqlcmd.Parameters.Add("@state_id", MySqlDbType.Int32).Value = state_id;
                mysqlcmd.Parameters.Add("@city_id", MySqlDbType.Int32).Value = city_id;
                mysqlcmd.Parameters.Add("@pincode", MySqlDbType.Int32).Value = pincode;
                mysqlcmd.Parameters.Add("@contact_person_name", MySqlDbType.VarChar).Value = contact_person_name;
                mysqlcmd.Parameters.Add("@contact_person_mobile", MySqlDbType.VarChar).Value = contact_person_mobile;
                mysqlcmd.Parameters.Add("@contact_person_email", MySqlDbType.VarChar).Value = contact_person_email;
                mysqlcmd.Parameters.Add("@user_id", MySqlDbType.Int32).Value = user_id;

            }     

            try
            {
                mysqlcmd.ExecuteNonQuery();
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
