using DataAccessLayer;
using System.Collections.Generic;
using Model;
using System.Data;
 

namespace Business
{
   public class ValidateLogin
    {


       public clsvalidatelogin Get(clsUserDetails validatelogin)
                {
                    
                    clsvalidatelogin validate = new clsvalidatelogin();
                    DaLogin Dalogin = new DaLogin();

                    validate = Dalogin.Get(validatelogin);

                    return validate;
                }     
           
    }


}
