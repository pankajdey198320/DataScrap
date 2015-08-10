using System;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
   public class clsStateList
    {
        private int _state_id = 0;
        private string _state_name = String.Empty;
        private string _state_code = String.Empty;
        private string _country_id = String.Empty;     
        [DataMember]
        public int State_id
        {
            get { return _state_id; }
            set { _state_id = value; }
        }
         [DataMember]
        public string State_name
        {
            get { return _state_name; }
            set { _state_name = value; }
        }
         [DataMember]
        public string State_code
        {
            get { return _state_code; }
            set { _state_code = value; }
        }
         [DataMember]
        public string Country_id
        {
            get { return _country_id; }
            set { _country_id = value; }
        }
    }
}
