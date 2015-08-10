using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;
using Model;




namespace SevaGenieWS
{
    
    [ServiceContract]
    public interface IWSNewRegister
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "Add/New", Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        clsUserDetails Add(clsUserDetails newuser);        
    }   
}
