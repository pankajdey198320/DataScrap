using DataAccessLayer;
using System.IO;
using Model;


namespace Business
{
   public class NewUserRegister
    {


       //public int Add(string mobileno, string password, string Email_Address, string first_name, string last_name, string Gender, string date_of_birth, string pincode)
       // {
       //     int i = 0;      
       //     DaNewUserRegister DaNewUserRegister = new DaNewUserRegister();

       //     i = DaNewUserRegister.Add(mobileno,password,Email_Address,first_name,last_name,Gender,date_of_birth,pincode);
       //     return i;
       // }


       public clsUserDetails Add(clsUserDetails newuser)
       {
           clsUserDetails addnew1 = new clsUserDetails();
           DaNewUserRegister newadd = new DaNewUserRegister();

           addnew1 = newadd.Add(newuser);
           return addnew1;
       }
    }
}
