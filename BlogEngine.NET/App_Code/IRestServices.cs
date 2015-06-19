using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using BlogEngine.Core.Json;

namespace App_Code
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRestServices" in both code and config file together.
    [ServiceContract]
    public interface IRestServices
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "GetPosts")]
        List<JsonPost> GetPosts();

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "GetLastComments?numComments={numComments}")]
        List<JsonComment> GetLastComments(string numComments);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "GetComment?idPost={idPost}&idComment={idComment}")]
        JsonComment GetComment(string idPost, string idComment);
    }

}