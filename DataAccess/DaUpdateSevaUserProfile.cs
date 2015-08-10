using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace DataAccessLayer
{
  public class DaUpdateSevaUserProfile
    {


      public int AddContact(string Next_of_Kin_Name, string Next_of_Kin_Mobile_No, string Next_of_Kin_Country, string Next_of_Kin_Email,
                             string Local_Contact_Name, string Local_Contact_Mobile_No, string Local_Contact_Email, string Local_Contact_Pincode, string user_id)
      {

          MySqlConnection mysqlcon = null;
          int i = 0;

          DataTable dt = new DataTable();
          MySqlCommand mysqlcmd = new MySqlCommand();

          string get = @"Select user_id from sg_user_profile_detail_tbl  where user_id =  '" + user_id + "'";

          mysqlcon = DBUtils.CreateMySqlConnection();
          mysqlcmd = new MySqlCommand(get, mysqlcon);

          MySqlDataAdapter da = new MySqlDataAdapter(mysqlcmd);
          da.Fill(dt);

          if (dt.Rows.Count > 0)
          {
              string update = @"update sg_user_profile_detail_tbl 
                                set 
                                      Next_of_Kin_Name=@Next_of_Kin_Name,
                                      Next_of_Kin_Mobile_No=@Next_of_Kin_Mobile_No,
                                      Next_of_Kin_Country=@Next_of_Kin_Country,
                                      Next_of_Kin_Pincode=@Next_of_Kin_Email,
                                      
                                      Local_Contact_Name=@Local_Contact_Name,
                                      Local_Contact_Mobile_No=@Local_Contact_Mobile_No,
                                      Local_Contact_Email=@Local_Contact_Email,
                                      Local_Contact_Pincode=@Local_Contact_Pincode,                                      
                                      where user_id = '" + user_id + "'";

              mysqlcmd = new MySqlCommand(update, mysqlcon);

              mysqlcmd.Parameters.AddWithValue("@Next_of_Kin_Name", Next_of_Kin_Name);
              mysqlcmd.Parameters.AddWithValue("@Next_of_Kin_Mobile_No", Next_of_Kin_Mobile_No);
              mysqlcmd.Parameters.AddWithValue("@Next_of_Kin_Country", Next_of_Kin_Country);
              mysqlcmd.Parameters.AddWithValue("@Next_of_Kin_Email", Next_of_Kin_Email);
              mysqlcmd.Parameters.AddWithValue("@Local_Contact_Name", Local_Contact_Name);
              mysqlcmd.Parameters.AddWithValue("@Local_Contact_Mobile_No", Local_Contact_Mobile_No);
              mysqlcmd.Parameters.AddWithValue("@Local_Contact_Email", Local_Contact_Email);
              mysqlcmd.Parameters.AddWithValue("@Local_Contact_Pincode", Local_Contact_Pincode);
          }
          else
          {
              string add = @"Insert into sg_user_profile_detail_tbl
                             (
                                user_id,Next_of_Kin_Name,Next_of_Kin_Mobile_No,Next_of_Kin_Country,Next_of_Kin_Email,Local_Contact_Name,
                                Local_Contact_Mobile_No,Local_Contact_Email,Local_Contact_Pincode
                             )
                            Values
                            (
                                @user_id,@Next_of_Kin_Name,@Next_of_Kin_Mobile_No,@Next_of_Kin_Country,@Next_of_Kin_Email,@Local_Contact_Name,
                                @Local_Contact_Mobile_No,@Local_Contact_Email,@Local_Contact_Pincode

                             )";

              mysqlcmd = new MySqlCommand(add, mysqlcon);

              mysqlcmd.Parameters.Add("@user_id", MySqlDbType.Int32).Value = user_id;
              mysqlcmd.Parameters.Add("@Next_of_Kin_Name", MySqlDbType.VarChar).Value = Next_of_Kin_Name;
              mysqlcmd.Parameters.Add("@Next_of_Kin_Mobile_No", MySqlDbType.VarChar).Value = Next_of_Kin_Mobile_No;
              mysqlcmd.Parameters.Add("@Next_of_Kin_Address", MySqlDbType.VarChar).Value = Next_of_Kin_Country;
              mysqlcmd.Parameters.Add("@Next_of_Kin_Email", MySqlDbType.VarChar).Value = Next_of_Kin_Email;
              mysqlcmd.Parameters.Add("@Local_Contact_Name", MySqlDbType.VarChar).Value = Local_Contact_Name;
              mysqlcmd.Parameters.Add("@Local_Contact_Mobile_No", MySqlDbType.VarChar).Value = Local_Contact_Mobile_No;
              mysqlcmd.Parameters.Add("@Local_Contact_Email", MySqlDbType.VarChar).Value = Local_Contact_Email;
              mysqlcmd.Parameters.Add("@Local_Contact_Pincode", MySqlDbType.Int32).Value = Local_Contact_Pincode;

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
