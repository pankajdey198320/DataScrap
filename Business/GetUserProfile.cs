using DataAccessLayer;
using Model;
using System;

namespace Business
{
   public class GetUserProfile
    {
        
        

        public clsUserDetails Get(string user_id)
        {
            clsUserDetails UserDetails = new clsUserDetails();
            DaGetUserDetails DaUserDetails = new DaGetUserDetails();

            UserDetails = DaUserDetails.Get(user_id);


            return UserDetails;
        }    
    }
}
