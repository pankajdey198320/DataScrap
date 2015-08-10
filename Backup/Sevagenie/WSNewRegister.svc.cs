using System;
using Business;
using Model;


namespace SevaGenieWS
{

    public class WSNewRegister : IWSNewRegister
    {

        //public string Add(string mobileno, string password, string Email_Address, string first_name, string last_name, string Gender, string date_of_birth, string pincode)
        //{
        //    NewUserRegister NewUserRegister = new NewUserRegister();
        //    int status;

        //    try
        //    {


        //        status = NewUserRegister.Add(mobileno, password, Email_Address, first_name, last_name, Gender, date_of_birth, pincode);

        //        if (status!=0)
        //        {
        //            return status.ToString();

        //        }
        //        else
        //        {
        //            return string.Format("status");
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return string.Format("There is a problem to add new user");
        //    }

        //}


        public clsUserDetails Add(clsUserDetails newuser)
        {
            clsUserDetails add1 = new clsUserDetails();
            NewUserRegister NewUser = new NewUserRegister();
            add1 = NewUser.Add(newuser);
            return add1;
        }
    }
}
