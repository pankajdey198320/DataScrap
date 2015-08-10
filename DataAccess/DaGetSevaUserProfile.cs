using System;
using System.Data;
using MySql.Data.MySqlClient;
using Model;

namespace DataAccessLayer
{
    public class DaGetSevaUserProfile
    {


        public ClsSevaUserProfile Get(string user_id)
        {
            MySqlConnection mysqlcon = null;
            DataTable dt = new DataTable();
            ClsSevaUserProfile SevaUser = new ClsSevaUserProfile();

            try
            {
                string qurey = @"select user_id,sathi_1_user_id,sathi_2_user_id,Next_of_Kin_Name,Next_of_Kin_Mobile_No,
                               Next_of_Kin_Country,Next_of_Kin_Pincode,Local_Contact_Name,Local_Contact_Mobile_No,
                               Local_Contact_Address,Local_Contact_Pincode,daily_seva_flag,medical_seva_flag,
                               financial_seva_flag,entertain_seva_flag,financial_banker_seva_flag,
                               financial_insurance_seva_flag from sg_user_profile_detail_tbl
                               where user_id='" + user_id + "'";
                
                mysqlcon = DBUtils.CreateMySqlConnection();
                MySqlCommand mysqlcmd = new MySqlCommand(qurey,mysqlcon);
                MySqlDataAdapter da = new MySqlDataAdapter(mysqlcmd);
                da.Fill(dt);

                if(dt.Rows.Count>0)
                {
                    SevaUser.User_id = dt.Rows[0]["user_id"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["user_id"]);
                    SevaUser.Sathi_1_user_id = dt.Rows[0]["sathi_1_user_id"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["sathi_1_user_id"]);
                    SevaUser.Sathi_2_user_id = dt.Rows[0]["sathi_2_user_id"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["sathi_2_user_id"]);
                    SevaUser.Next_of_Kin_Name1 = dt.Rows[0]["Next_of_Kin_Name"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["Next_of_Kin_Name"]);
                    SevaUser.Next_of_Kin_Mobile_No1 = dt.Rows[0]["Next_of_Kin_Mobile_No"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["Next_of_Kin_Mobile_No"]);
                    SevaUser.Next_of_Kin_Country1 = dt.Rows[0]["Next_of_Kin_Country"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["Next_of_Kin_Country"]);
                    SevaUser.Next_of_Kin_Pincode1 = dt.Rows[0]["Next_of_Kin_Pincode"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["Next_of_Kin_Pincode"]);
                    SevaUser.Local_Contact_Name1 = dt.Rows[0]["Local_Contact_Name"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["Local_Contact_Name"]);
                    SevaUser.Local_Contact_Mobile_No1 = dt.Rows[0]["Local_Contact_Mobile_No"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["Local_Contact_Mobile_No"]);
                    SevaUser.Local_Contact_Email1 = dt.Rows[0]["Local_Contact_Email"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[0]["Local_Contact_Email"]);
                    SevaUser.Local_Contact_Pincode1 = dt.Rows[0]["Local_Contact_Pincode"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["Local_Contact_Pincode"]);

                    if(dt.Rows[0]["daily_seva_flag"] != DBNull.Value )
                    {                        
                        SevaUser.Daily_seva_flag = Convert.ToByte(dt.Rows[0]["daily_seva_flag"]);
                    }
                    else
                    {
                        SevaUser.Daily_seva_flag=0;
                    }

                    if (dt.Rows[0]["medical_seva_flag"] != DBNull.Value)
                    {
                        SevaUser.Medical_seva_flag = Convert.ToByte(dt.Rows[0]["medical_seva_flag"]);
                    } 
                    else
                    {
                        SevaUser.Medical_seva_flag=0;
                    }
                  
                    if (dt.Rows[0]["financial_seva_flag"] != DBNull.Value)
                    {
                        SevaUser.Financial_seva_flag = Convert.ToByte(dt.Rows[0]["financial_seva_flag"]);
                    } 
                    else
                    {
                        SevaUser.Financial_seva_flag=0;
                    }
                 
                    if (dt.Rows[0]["entertain_seva_flag"] != DBNull.Value)
                    {
                        SevaUser.Entertain_seva_flag = Convert.ToByte(dt.Rows[0]["entertain_seva_flag"]);
                    } 
                    else
                    {
                        SevaUser.Entertain_seva_flag=0;
                    }

                    if (dt.Rows[0]["financial_banker_seva_flag"] != DBNull.Value)
                    {
                        SevaUser.Financial_banker_seva_flag = Convert.ToByte(dt.Rows[0]["financial_banker_seva_flag"]);
                    } 
                    else
                    {
                        SevaUser.Financial_banker_seva_flag=0;
                    }
                    if (dt.Rows[0]["financial_insurance_seva_flag"] != DBNull.Value)
                    {
                        SevaUser.Financial_insurance_seva_flag = Convert.ToByte(dt.Rows[0]["financial_insurance_seva_flag"]);
                    } 
                    else
                    {
                        SevaUser.Financial_insurance_seva_flag=0;
                    }
                    
                    return SevaUser;
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
            return SevaUser;
        }

    }
}
