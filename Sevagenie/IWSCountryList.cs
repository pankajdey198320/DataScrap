using System.ServiceModel;
using System.ServiceModel.Web;
using System.Collections.Generic;
using Model;

namespace SevaGenieWS
{

    [ServiceContract]
    public interface IWSCountryList
    {
        [OperationContract]
        [WebGet(UriTemplate = "/Get", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        List<clsCountryList> Get();
    }   
}
    
