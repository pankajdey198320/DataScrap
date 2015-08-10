using System;
using System.ServiceModel;
using Model;
using Business;

namespace SevaGenieWS
{
    
    public class WSGetSevaUserProfile : IWSGetSevaUserProfile
    {
        public ClsSevaUserProfile Get(string user_id)
        {
            GetSevaUserProfile GetSevaUserProfile = new GetSevaUserProfile();
            ClsSevaUserProfile CLSevaUserProfile = new ClsSevaUserProfile();

            try
            {
                CLSevaUserProfile = GetSevaUserProfile.Get(user_id);

                return CLSevaUserProfile;
            }
            catch (Exception ex)
            {
                throw new FaultException<string>(ex.Message);
            }
        }
    }
}
