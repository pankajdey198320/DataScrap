using System;
using Business;

namespace SevaGenieWS
{
     
    public class WSUpdateSevaUserProfile : IWSUpdateSevaUserProfile
    {


        public string AddContact(string Next_of_Kin_Name, string Next_of_Kin_Mobile_No, string Next_of_Kin_Country, string Next_of_Kin_Email,
                             string Local_Contact_Name, string Local_Contact_Mobile_No, string Local_Contact_Email, string Local_Contact_Pincode, string user_id)
        {
           
            try
            {

                UpdateSevaUserProfile UpdateSevaUserProfile = new UpdateSevaUserProfile();
                
                int status;

                status = UpdateSevaUserProfile.AddContact(Next_of_Kin_Name, Next_of_Kin_Mobile_No, Next_of_Kin_Country, Next_of_Kin_Email,
                                                        Local_Contact_Name, Local_Contact_Mobile_No, Local_Contact_Email, Local_Contact_Pincode,
                                                        user_id);

                if (status == 0)
                {
                  
                    return string.Format("Profile Updated Successfully");
                }
                else
                {

                    return string.Format("Error in Updated");
                }
            }
            catch (Exception)
            {
                return string.Format("Error in Updated");
            }

        }
    }
}
