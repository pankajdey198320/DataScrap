using System.ServiceModel;
using System.ServiceModel.Web;
using Model;
using System.Runtime.Serialization;


namespace SevaGenieWS
{
   
    [ServiceContract]
    public interface IWSGetUserProfile
    {

        [OperationContract]
        [WebGet(UriTemplate = "/Get/{user_id}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        GetUser Get(string user_id);
    }


    [DataContract]
    public class GetUser
    {
        clsUserDetails[] User_details = new clsUserDetails[1];

        [DataMember]
        public clsUserDetails[] UserDetails
        {
            get { return User_details; }
            set { User_details = value; }
        }
    }
}
