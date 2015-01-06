using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using BlogEngine.Core;

public partial class StandardSite : System.Web.UI.MasterPage
{
  protected void Page_Load(object sender, EventArgs e)
  {
		if (Security.IsAuthenticated)
		{
            aUser.InnerText = "Welcome " + Page.User.Identity.Name + "!";
			aLogin.InnerText = Resources.labels.logoff;
			aLogin.HRef = Utils.RelativeWebRoot + "Account/login.aspx?logoff";
		}
		else
		{
			aLogin.HRef = Utils.RelativeWebRoot + "Account/login.aspx";
			aLogin.InnerText = Resources.labels.login;
		}
        string pageCompletePath = HttpContext.Current.Request.RawUrl;

        if (pageCompletePath.ToLower().Contains("default.aspx"))
        {
            be_Top_SPro_WIDGET_ZONE.Visible = true;
        }
        else
        {
            be_Top_SPro_WIDGET_ZONE.Visible = false;
        }
            
            
  }

}
