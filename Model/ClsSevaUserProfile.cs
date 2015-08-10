using System.Runtime.Serialization;
using System;

namespace Model
{
    [DataContract]
    public class ClsSevaUserProfile
    {
        private int user_id = 0;
        private int sathi_1_user_id = 0;
        private int sathi_2_user_id = 0;
        private string Next_of_Kin_Name = String.Empty;
        private string Next_of_Kin_Mobile_No = String.Empty;
        private string Next_of_Kin_Country = String.Empty;
        private int Next_of_Kin_Pincode = 0;
        private string Local_Contact_Name = String.Empty;
        private string Local_Contact_Mobile_No = String.Empty;
        private string Local_Contact_Email = String.Empty;
        private int Local_Contact_Pincode = 0;
        private byte daily_seva_flag = 0;
        private byte medical_seva_flag = 0;
        private byte financial_seva_flag = 0;
        private byte entertain_seva_flag = 0;
        private byte financial_banker_seva_flag = 0;
        private byte financial_insurance_seva_flag = 0;        


        [DataMember]
        public int User_id
        {
            get { return user_id; }
            set { user_id = value; }
        }
        [DataMember]
        public int Sathi_1_user_id
        {
            get { return sathi_1_user_id; }
            set { sathi_1_user_id = value; }
        }
        [DataMember]
        public int Sathi_2_user_id
        {
            get { return sathi_2_user_id; }
            set { sathi_2_user_id = value; }
        }
        [DataMember]
        public string Next_of_Kin_Name1
        {
            get { return Next_of_Kin_Name; }
            set { Next_of_Kin_Name = value; }
        }
        [DataMember]
        public string Next_of_Kin_Mobile_No1
        {
            get { return Next_of_Kin_Mobile_No; }
            set { Next_of_Kin_Mobile_No = value; }
        }
        [DataMember]
        public string Next_of_Kin_Country1
        {
            get { return Next_of_Kin_Country; }
            set { Next_of_Kin_Country = value; }
        }
        [DataMember]
        public int Next_of_Kin_Pincode1
        {
            get { return Next_of_Kin_Pincode; }
            set { Next_of_Kin_Pincode = value; }
        }
        [DataMember]
        public string Local_Contact_Name1
        {
            get { return Local_Contact_Name; }
            set { Local_Contact_Name = value; }
        }
        [DataMember]
        public string Local_Contact_Mobile_No1
        {
            get { return Local_Contact_Mobile_No; }
            set { Local_Contact_Mobile_No = value; }
        }
        [DataMember]
        public string Local_Contact_Email1
        {
            get { return Local_Contact_Email; }
            set { Local_Contact_Email = value; }
        }
        [DataMember]
        public int Local_Contact_Pincode1
        {
            get { return Local_Contact_Pincode; }
            set { Local_Contact_Pincode = value; }
        }
        [DataMember]
        public byte Daily_seva_flag
        {
            get { return daily_seva_flag; }
            set { daily_seva_flag = value; }
        }
        [DataMember]
        public byte Medical_seva_flag
        {
            get { return medical_seva_flag; }
            set { medical_seva_flag = value; }
        }
        [DataMember]
        public byte Financial_seva_flag
        {
            get { return financial_seva_flag; }
            set { financial_seva_flag = value; }
        }
        [DataMember]
        public byte Entertain_seva_flag
        {
            get { return entertain_seva_flag; }
            set { entertain_seva_flag = value; }
        }
        [DataMember]
        public byte Financial_banker_seva_flag
        {
            get { return financial_banker_seva_flag; }
            set { financial_banker_seva_flag = value; }
        }
        [DataMember]
        public byte Financial_insurance_seva_flag
        {
            get { return financial_insurance_seva_flag; }
            set { financial_insurance_seva_flag = value; }
        }
    }
}
