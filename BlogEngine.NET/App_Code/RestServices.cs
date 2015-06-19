using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BlogEngine.Core.Json;
using System.ServiceModel.Activation;
// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RestServices" in code, svc and config file together.
namespace App_Code { 

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RestServices : IRestServices{
        public List<JsonPost> GetPosts()
	    {
            var obj = JsonPosts.GetPosts(1, 1, "", "", "");
            return obj;
	    }

        public List<JsonComment> GetLastComments(string numComments)
        {
            int value;
            List<JsonComment> ret=null;
            numComments = numComments ?? "0";
            bool result = int.TryParse(numComments, out value);
            if (result)
            {
                ret = JsonPosts.GetCommentsList(value);
            }
            else
            {
                ret = new List<JsonComment>(){
                    new JsonComment(){ Content="numComments non valido" }
                };
            }
            return ret;
        }

        public JsonComment GetComment(string idPost,string idComment)
        {
            JsonComment ret = null;
            if (idPost == null || idComment == null)
            {
                ret = new JsonComment() { Content = "numComments non valido" };
            }
            else
            {
                ret = JsonPosts.GetComment(idPost, idComment);
            }
            return ret;
        }
    }
}
