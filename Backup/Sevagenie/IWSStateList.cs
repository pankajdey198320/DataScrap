using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Collections.Generic;
using Model;


namespace SevaGenieWS
{
    
    [ServiceContract]
    public interface IWSStateList
    {
        [OperationContract]
        [WebGet(UriTemplate = "/Get/{country_id}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        List<clsStateList> Get(string country_id);       
    }
}
