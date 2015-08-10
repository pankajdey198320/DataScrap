using MySql.Data.MySqlClient;
using System;
using System.Data;
using Model;
using System.Collections.Generic;

namespace DataAccessLayer
{
  public class DaStateList
    {


      public List<clsStateList> Get(string country_id)
        {
            MySqlConnection mysqlcon = null;
            DataTable dt = new DataTable();
            List<clsStateList> LIStateList = new List<clsStateList>();

          try
          {

              string query = @"select state_id,state_name,state_code from state_tbl where country_id= '"
                             + country_id + "' order by state_name";

              mysqlcon = DBUtils.CreateMySqlConnection();
              MySqlCommand mysqlcmd = new MySqlCommand(query, mysqlcon);

              MySqlDataAdapter da = new MySqlDataAdapter(mysqlcmd);
              da.Fill(dt);
              for (int i = 0; i < dt.Rows.Count; i++)
              {
                  clsStateList Statelist = new clsStateList();                  
                  Statelist.State_id = dt.Rows[i]["state_id"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[i]["state_id"]);
                  Statelist.State_name = dt.Rows[i]["state_name"] == DBNull.Value ? "" : Convert.ToString(dt.Rows[i]["state_name"]); 
                  LIStateList.Add(Statelist);
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
          return LIStateList;
      }       
    }
}
