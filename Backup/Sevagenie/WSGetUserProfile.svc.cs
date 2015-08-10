using System;
using System.ServiceModel;
using Business;
using Model;
using System.Web.Script.Serialization;

namespace SevaGenieWS
{
    
    public class WSGetUserProfile : IWSGetUserProfile
    {

        public GetUser Get(string user_id)
        
        {
            GetUserProfile GetUserProfile = new GetUserProfile();
            clsUserDetails objclsUserDetails = new clsUserDetails();

            GetUser getuser = new GetUser();

            
            try
            {

                objclsUserDetails = GetUserProfile.Get(user_id);

                getuser.UserDetails = new clsUserDetails[] { objclsUserDetails };
              
                return getuser;
            }
            catch (Exception ex)
            {
                throw new FaultException<string>(ex.Message);
            }
        }


    }
}
