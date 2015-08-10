using Model;
using Business;
using System;
using System.ServiceModel;
using System.Collections.Generic;


namespace WcfTest1
{
    public class WSCoundetails : IWSCoundetails
    {
        public List<clsCountryList> Get(string country_id)
        {
            getcountrybyid country = new getcountrybyid();
            try
            {
                List<clsCountryList> details = new List<clsCountryList>();
                details = country.Get(country_id);
                return details;
            }
            catch (Exception ex)
            {
                throw new FaultException<string>(ex.Message);
            }
 
        }
    }
}
