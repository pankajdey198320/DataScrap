using DataAccessLayer;
using Model;
using System.Collections.Generic;

namespace Business
{
   public class StateList
    {
       
     

        public List<clsStateList> Get(string country_id)
        {
            List<clsStateList> LIStateList = new List<clsStateList>();
            DaStateList DaStateList = new DaStateList();


            LIStateList = DaStateList.Get(country_id);

             
            return LIStateList;
        }    
    }
}
