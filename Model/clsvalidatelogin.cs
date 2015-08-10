namespace Model
{
    using System;
    using System.Runtime.Serialization;
    using System.Web.Script.Serialization;

    [DataContract]
    public class clsvalidatelogin
    {
        private int _user_id = 0;

        [DataMember]
        public int User_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }

        
    }
}
