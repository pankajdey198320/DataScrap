using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using Business;
using System.Collections.Generic;
using Model;

namespace SevaGenieWS
{

    public class WSStateList : IWSStateList
    {
        

        public List<clsStateList> Get(string country_id)
        {
            StateList statelist = new StateList();   
            try
            {
               
                List<clsStateList> LIStateList = new List<clsStateList>();
             
                LIStateList = statelist.Get(country_id);

                return LIStateList;
            }
            catch (Exception ex)
            {
                throw new FaultException<string>(ex.Message);
            }
        }
    }    
}
