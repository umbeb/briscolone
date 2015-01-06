using System;

public partial class themes_Discovery_PostView : BlogEngine.Core.Web.Controls.PostViewBase
{
    protected new void Page_Load(object sender, EventArgs e)
	{
		//base.Page_Load(sender, e);
	}

	public string GetLinksWithLabel(string label, string linkText)
	{
		if (string.IsNullOrEmpty(linkText))
		{
			return string.Empty;
		}
		else
		{
			return label + linkText + "<br />";
		}
	}
}
