using DataAccessLayer;
using Model;
using System.Collections.Generic;

namespace Business
{
   public class CountryList
    {      
       

        public List<clsCountryList> Get()
        {
            List<clsCountryList> LICountrylist = new List<clsCountryList>();

            DaCountryList DaCountryList = new DaCountryList();
            LICountrylist = DaCountryList.Get();
 
            return LICountrylist; 
        } 
    }
}
