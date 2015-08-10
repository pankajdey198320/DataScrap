using System.ServiceModel;
using System.ServiceModel.Web;
using System.Runtime.Serialization;
using Model;

namespace SevaGenieWS
{    
    [ServiceContract]
    public interface IWSValidateLogin
    {       
      [OperationContract]
      [WebInvoke(UriTemplate = "/Get/validate" ,Method="POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
       login Get(clsUserDetails validatelogin);  
    }

    [DataContract]
    public class login
    {
        clsvalidatelogin[] signup = new clsvalidatelogin[1];

        [DataMember]
        public clsvalidatelogin[] Signup
        {
            get { return signup; }
            set { signup = value; }
        }
    }
}
