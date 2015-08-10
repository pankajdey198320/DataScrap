using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using Business;
using Model;


namespace SevaGenieWS
{
    
    public class WSForgetPassword : IWSForgetPassword
    {

        public string Get(string mobileno)
        {
            int validate = 0;
            GetCredentials credentials = new GetCredentials();

            try
            {
                validate = credentials.Get(mobileno);

                //return string.Format(" " + mobileno + "  " + "is Already Exist");
                
                return validate.ToString();
            }
            catch (Exception ex)
            {
                throw new FaultException<string>(ex.Message);
            }
        }
    }
}
