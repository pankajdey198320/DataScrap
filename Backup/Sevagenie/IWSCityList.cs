using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Collections.Generic;
using Model;

namespace SevaGenieWS
{

    [ServiceContract]
    public interface IWSCityList
    {
        [OperationContract]
        [WebGet(UriTemplate = "/Get/{state_id}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        List<clsCityList> Get(string state_id);
    }
}
