using DataAccessLayer;
using Model;
using System.Collections.Generic;



namespace Business
{
    public class CityList
    {      

        public List<clsCityList> Get(string state_id)
        {
            List<clsCityList> CityList = new List<clsCityList>();
            DaCityList DaCityList = new DaCityList();

            CityList = DaCityList.Get(state_id);
            
            return CityList;
        }
    }
}
