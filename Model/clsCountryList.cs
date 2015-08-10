using System;
using System.Runtime.Serialization;


namespace Model
{
    [DataContract]  
    public class clsCountryList
    {
        private int _Country_Id = 0;
        private string _country_code = String.Empty;
        private string _country_name = String.Empty;
        private string _country_isd_code=String.Empty;

        [DataMember]
        public int Country_Id
        {
            get { return _Country_Id; }
            set { _Country_Id = value; }
        }       
        [DataMember]
        public string Country_code
        {
            get { return _country_code; }
            set { _country_code = value; }
        }       
        [DataMember]
        public string Country_name
        {
            get { return _country_name; }
            set { _country_name = value; }
        }        
        [DataMember]
        public string Country_isd_code
        {
            get { return _country_isd_code; }
            set { _country_isd_code = value; }
        }

       

    }
}
