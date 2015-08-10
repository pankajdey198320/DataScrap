using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using Business;
using System.Collections.Generic;
using Model;

namespace SevaGenieWS
{

    public class WSCityList : IWSCityList
    {
        

        public List<clsCityList> Get(string state_id)
        {
            CityList citylist = new CityList();

            try
            {
                List<clsCityList> LiCityList = new List<clsCityList>();

                LiCityList = citylist.Get(state_id);

                return LiCityList;
            }
            catch (Exception ex)
            {
                throw new FaultException<string>(ex.Message);
            }
        }
    }
}
