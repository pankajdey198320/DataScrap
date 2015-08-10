using DataAccessLayer;
using Model;
using System.Data;
using System;

namespace Business
{
   public class GetCredentials
    {
       
        public int Get(string mobileno)
        {            
            DaGetCredential GetCredential  = new DaGetCredential();
            int i = 0;


            i = GetCredential.Get(mobileno);

            return i;
        }    
    }
}
