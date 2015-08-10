using System;
using System.Runtime.Serialization;
 
namespace Model
{
    [DataContract]
   public class clsUserInsuranceDetails
    {
        private int user_id = 0;        
        private string insurance_company_name = String.Empty;
        private string policy_no = String.Empty;
        private DateTime? policy_commencement_date = null;
        private int policy_term = 0;
        private string policy_mode =String.Empty;
        private double premium_amount = 0.0;
        private string agent_name = String.Empty;
        private string agent_mobileno = String.Empty;

        [DataMember]
        public int User_id
        {
            get { return user_id; }
            set { user_id = value; }
        }

        [DataMember]
        public string Insurance_company_name
        {
            get { return insurance_company_name; }
            set { insurance_company_name = value; }
        }
        [DataMember]
        public string Policy_no
        {
            get { return policy_no; }
            set { policy_no = value; }
        }
        [DataMember]
        public DateTime? Policy_commencement_date
        {
            get { return policy_commencement_date; }
            set { policy_commencement_date = value; }
        }
        [DataMember]
        public int Policy_term
        {
            get { return policy_term; }
            set { policy_term = value; }
        }
        [DataMember]
        public string Policy_mode
        {
            get { return policy_mode; }
            set { policy_mode = value; }
        }
        [DataMember]
        public double Premium_amount
        {
            get { return premium_amount; }
            set { premium_amount = value; }
        }
        [DataMember]
        public string Agent_name
        {
            get { return agent_name; }
            set { agent_name = value; }
        }
        [DataMember]
        public string Agent_mobileno
        {
            get { return agent_mobileno; }
            set { agent_mobileno = value; }
        }
    }
}
