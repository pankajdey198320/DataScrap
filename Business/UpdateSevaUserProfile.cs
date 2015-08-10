using DataAccessLayer;
using System.Data;

namespace Business
{
   public class UpdateSevaUserProfile
    {
        int i = 0;



        public int AddContact(string Next_of_Kin_Name, string Next_of_Kin_Mobile_No, string Next_of_Kin_Country, string Next_of_Kin_Email,
                             string Local_Contact_Name, string Local_Contact_Mobile_No, string Local_Contact_Email, string Local_Contact_Pincode, string user_id)

        {
            DaUpdateSevaUserProfile DaUpdateSevaUserProfile = new DaUpdateSevaUserProfile();

            i = DaUpdateSevaUserProfile.AddContact(Next_of_Kin_Name, Next_of_Kin_Mobile_No, Next_of_Kin_Country, Next_of_Kin_Email,
                                                        Local_Contact_Name, Local_Contact_Mobile_No, Local_Contact_Email, Local_Contact_Pincode,
                                                        user_id);       
 
           
            return i;
        }
    }
}
