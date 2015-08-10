using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using Business;

namespace SevaGenieWS
{
   
    public class WSUpdateProfile : IWSUpdateProfile
    {

        public string Update(string address_line1, string address_line2, string address_line3, string user_id)
        {

            UpdateUserProfile updateuserprofile = new UpdateUserProfile();
            int status;

            try
            {

                status = updateuserprofile.Update(address_line1, address_line2, address_line3, user_id);

                if (status == 1)
                {

                    return status.ToString();
                }
                else
                {
                    return status.ToString();
                   
                }

            }
            catch (Exception)
            {
                return string.Format("There is a problem in update");
            }

        }
    }
}
