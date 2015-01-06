using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public partial class BlueLine : System.Web.UI.MasterPage
{

  protected void Page_Load(object sender, EventArgs e)
  {
      //Check url for localhost or live
      string url = BlogEngine.Core.Utils.AbsoluteWebRoot.AbsoluteUri.ToString();
      //If live
      if (!url.ToLower().Contains("localhost"))
      {
          //Check for www. in the url SEO friendly
          if (!url.ToLower().Contains("www"))
          {
              string[] seourl = BlogEngine.Core.Utils.AbsoluteWebRoot.AbsoluteUri.ToString().Split(("/.").ToCharArray());
              HttpContext.Current.Response.Status = "301 Moved Permanently";
              HttpContext.Current.Response.AddHeader("Location", Request.Url.ToString().ToLower().Replace("http://", "http://www."));
          }

      }

      //This code is made by http://www.onesoft.dk
      if (Page.User.Identity.IsAuthenticated)
      {
          aLogin.InnerText = Resources.labels.logoff;
          aLogin.HRef = BlogEngine.Core.Utils.RelativeWebRoot + "Account/login.aspx?logoff";
      }
      else
      {
          aLogin.HRef = BlogEngine.Core.Utils.RelativeWebRoot + "Account/login.aspx";
          aLogin.InnerText = Resources.labels.login;
      }
  }

}
