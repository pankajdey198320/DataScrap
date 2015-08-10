using System;
using System.ServiceModel;
using Business;
using Model;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace SevaGenieWS
{

    public class WSValidateLogin : IWSValidateLogin
    {

        public login Get(clsUserDetails validatelogin)
        {
            
            ValidateLogin ValidateLogin = new ValidateLogin();
           
            clsvalidatelogin validate = new clsvalidatelogin();
            login signup = new login();
             
            try
            {

                validate = ValidateLogin.Get(validatelogin);
                signup.Signup = new clsvalidatelogin[] {validate};
                return signup;
            }
            catch (Exception ex)
            {
                throw new FaultException<string>(ex.Message);
            }
        }
    }
}
