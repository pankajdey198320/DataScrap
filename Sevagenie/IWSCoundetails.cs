using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Model;

namespace WcfTest1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWSCoundetails" in both code and config file together.
    [ServiceContract]
    public interface IWSCoundetails
    {
        [OperationContract]
        [WebGet(UriTemplate = "/Get/{country_id}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        List<clsCountryList> Get(string country_id);
    }
}
