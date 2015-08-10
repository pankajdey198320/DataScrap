using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using Business;
using System.Collections.Generic;
using Model;

namespace SevaGenieWS
{

    public class WSCountryList : IWSCountryList
    {      
        

        public List<clsCountryList> Get()
        {
            CountryList countrylist = new CountryList();

            try
            {
                List<clsCountryList> LiCountryList = new List<clsCountryList>();

                LiCountryList = countrylist.Get();            

                return LiCountryList;
            }
            catch (Exception ex)
            {
                throw new FaultException<string>(ex.Message);
            }
        }
     
    }    
}