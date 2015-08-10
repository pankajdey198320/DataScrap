using System;
using DataAccessLayer;


namespace Business
{
   public class UpdateUserProfile
    {


       public int Update(string address_line1, string address_line2, string address_line3, string user_id)
        {
            DaUpdateUserDetails DaUpdateUserProfile = new DaUpdateUserDetails();

            int i = 0;

            i = DaUpdateUserProfile.Update(address_line1, address_line2, address_line3, user_id);
            return i;
        }
    }
}
