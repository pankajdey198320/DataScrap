using DataAccessLayer;
using Model;

namespace Business
{
    public class GetSevaUserProfile
    {
        public ClsSevaUserProfile Get(string user_id)
        {
            ClsSevaUserProfile SevaUserProfile = new ClsSevaUserProfile();
            DaGetSevaUserProfile DaSevaUserProfile = new DaGetSevaUserProfile();

            SevaUserProfile = DaSevaUserProfile.Get(user_id);

            return SevaUserProfile;
        } 
    }
}
