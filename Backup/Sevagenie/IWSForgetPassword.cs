using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using Model;

namespace SevaGenieWS
{
     
    [ServiceContract]
    public interface IWSForgetPassword
    {
        [OperationContract]
        [WebGet(UriTemplate = "/Get/{mobileno}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string Get(string mobileno);
    }
}
