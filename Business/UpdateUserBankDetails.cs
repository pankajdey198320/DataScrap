using System;
using DataAccessLayer;

namespace Business
{
    public class UpdateUserBankDetails
    {


        public int Update(string bank_name, string address_1, string address_2, string address_3, string country_id,
                          string state_id, string city_id, string pincode, string contact_person_name,
                          string contact_person_mobile, string contact_person_email, string user_id)
        {
            DaUpdateUserBankDetails DaUpdateUserBankDetails = new DaUpdateUserBankDetails();
            int i = 0;


            i = DaUpdateUserBankDetails.Update(bank_name, address_1, address_2, address_3, country_id, state_id, city_id, pincode,
                           contact_person_name, contact_person_mobile, contact_person_email, user_id);


            return i;
        }
    }
}
