using System;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
  public  class clsNewRegister
  {
      #region 
        private int _user_id = 0;
        private string _Mobileno = String.Empty;
        private string _Password = String.Empty;
        private string _Email = String.Empty;
        private string _Fname = String.Empty;
        private string _Lname = String.Empty;
        private string _Gender = String.Empty;
        private DateTime? _date_of_birth = null;
        private int _pincode = 0;
      #endregion
        [DataMember]
        public int User_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }

        [DataMember]
        public string Mobileno
        {
            get { return _Mobileno; }
            set { _Mobileno = value; }
        }
          [DataMember]
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
          [DataMember]
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
          [DataMember]
        public string Fname
        {
            get { return _Fname; }
            set { _Fname = value; }
        }
          [DataMember]
        public string Lname
        {
            get { return _Lname; }
            set { _Lname = value; }
        }
          [DataMember]
        public string Gender
          {
              get { return _Gender;}
              set { _Gender = value; }
          }
          [DataMember]
          public DateTime? date_of_birth
          {
              get { return _date_of_birth; }
              set { _date_of_birth = value; }
          }
          [DataMember]
          public int pincode
          {
              get { return _pincode; }
              set { _pincode = value; }
          }
    }
}
