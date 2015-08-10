using System;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
   public class clsUserBankingDetails
    {
        private int user_id = 0;
        private string bank_name = String.Empty;
        private string address_1 = String.Empty;
        private string address_2 = String.Empty;
        private string address_3 = String.Empty;
        private int country_id = 0;
        private int state_id = 0;
        private int city_id = 0;
        private int pincode = 0;
        private string contact_person_name = String.Empty;
        private string contact_person_mobile = String.Empty;
        private string contact_person_email = String.Empty;

        [DataMember]
        public int User_id
        {
            get { return user_id; }
            set { user_id = value; }
        }
        [DataMember]
        public string Bank_name
        {
            get { return bank_name; }
            set { bank_name = value; }
        }
        [DataMember]
        public string Address_1
        {
            get { return address_1; }
            set { address_1 = value; }
        }
        [DataMember]
        public string Address_2
        {
            get { return address_2; }
            set { address_2 = value; }
        }
        [DataMember]
        public string Address_3
        {
            get { return address_3; }
            set { address_3 = value; }
        }
        [DataMember]
        public int Country_id
        {
            get { return country_id; }
            set { country_id = value; }
        }
        [DataMember]
        public int State_id
        {
            get { return state_id; }
            set { state_id = value; }
        }
        [DataMember]
        public int City_id
        {
            get { return city_id; }
            set { city_id = value; }
        }
        [DataMember]
        public int Pincode
        {
            get { return pincode; }
            set { pincode = value; }
        }
        [DataMember]
        public string Contact_person_name
        {
            get { return contact_person_name; }
            set { contact_person_name = value; }
        }
        [DataMember]
        public string Contact_person_mobile
        {
            get { return contact_person_mobile; }
            set { contact_person_mobile = value; }
        }
        [DataMember]
        public string Contact_person_email
        {
            get { return contact_person_email; }
            set { contact_person_email = value; }
        }
    }
}
