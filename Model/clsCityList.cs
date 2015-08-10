using System;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class clsCityList
    { 

        private int _City_Id = 0;
        private string _city_name = String.Empty;
        private string _city_code = String.Empty;
        private string _state_id = String.Empty;

        [DataMember]
        public int City_Id
        {
            get { return _City_Id; }
            set { _City_Id = value; }
        }       
         [DataMember]
        public string City_name
        {
            get { return _city_name; }
            set { _city_name = value; }
        }        
         [DataMember]
        public string City_code
        {
            get { return _city_code; }
            set { _city_code = value; }
        }     
         [DataMember]
        public string State_id
        {
            get { return _state_id; }
            set { _state_id = value; }
        }

    }
}
