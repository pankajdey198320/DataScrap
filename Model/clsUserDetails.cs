

namespace Model
{
    using System;
    using System.Runtime.Serialization;

    [DataContract(Name = "UserDetails")]

    public class clsUserDetails
    {
        private int _user_id = 0;
        private string _mobileno = String.Empty;
        private string _Password = String.Empty;       
        private string _user_type = String.Empty;
        private string _Email_Address = String.Empty;
        private string _first_name = String.Empty;
        private string _last_name = String.Empty;
        private string _date_of_birth = String.Empty;

       
        private int _age = 0;
        private string _address_line1 = String.Empty;
        private string _address_line2 = String.Empty;
        private string _address_line3 = String.Empty;
        private int _country_id = 0;
        private int _state_id = 0;
        private int _city_id = 0; 
        private int _pincode = 0;
        private string _Gender = String.Empty;


        [DataMember]
        public int user_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }
        [DataMember]
        public string Mobileno
        {
            get { return _mobileno; }
            set { _mobileno = value; }
        }
        [DataMember]
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        [DataMember]
        public string User_type
        {
            get { return _user_type; }
            set { _user_type = value; }
        }
        [DataMember]
        public string Email_Address
        {
            get { return _Email_Address; }
            set { _Email_Address = value; }
        }
        [DataMember]
        public string First_name
        {
            get { return _first_name; }
            set { _first_name = value; }
        }
        [DataMember]
        public string Last_name
        {
            get { return _last_name; }
            set { _last_name = value; }
        }
        [DataMember]
        public string Date_of_birth
        {
            get { return _date_of_birth; }
            set { _date_of_birth = value; }
        }
        [DataMember]
        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }
        [DataMember]
        public string Address_line1
        {
            get { return _address_line1; }
            set { _address_line1 = value; }
        }
        [DataMember]
        public string Address_line2
        {
            get { return _address_line2; }
            set { _address_line2 = value; }
        }
        [DataMember]
        public string Address_line3
        {
            get { return _address_line3; }
            set { _address_line3 = value; }
        }
        [DataMember]
        public int Country_id
        {
            get { return _country_id; }
            set { _country_id = value; }
        }
        [DataMember]
        public int State_id
        {
            get { return _state_id; }
            set { _state_id = value; }
        }
        [DataMember]
        public int City_id
        {
            get { return _city_id; }
            set { _city_id = value; }
        }
        [DataMember]
        public int Pincode
        {
            get { return _pincode; }
            set { _pincode = value; }
        }  
         [DataMember]
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
       
    }

    
    
}