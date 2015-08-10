using System;
using Model;
using System.Collections.Generic;
using DataAccessLayer;
using DataAccess;


namespace Business
{
    public class getcountrybyid
    {
        public List<clsCountryList> Get(string country_id)
        {
            List<clsCountryList> listcountry = new List<clsCountryList>();
            DaGetCountryById getcountry = new DaGetCountryById();
            listcountry = getcountry.Get(country_id);
            return listcountry;
        }
    }
}
