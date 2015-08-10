using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace SevaGenieWS
{
    
    [ServiceContract]
    public interface IWSUpdateProfile
    { 

        [OperationContract]
        [WebInvoke(UriTemplate = "/Update/{address_line1}/{address_line2}/{address_line3}/{user_id}",
                               Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]

        string Update(string address_line1, string address_line2, string address_line3, string user_id);   
    }
}
