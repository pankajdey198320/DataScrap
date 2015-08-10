using System.ServiceModel;
using System.ServiceModel.Web;
using Model;

namespace SevaGenieWS
{
    
    [ServiceContract]
    public interface IWSGetSevaUserProfile
    {
        [OperationContract]
        [WebGet(UriTemplate = "/Get/{user_id}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        ClsSevaUserProfile Get(string user_id);
    }
}