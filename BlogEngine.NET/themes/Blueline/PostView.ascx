<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" Inherits="BlogEngine.Core.Web.Controls.PostViewBase" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        String url = Request.RawUrl;
        if (url.Contains("/post/"))
        {
            author.Visible = true;
            if (Post.AuthorProfile.PhotoUrl != "")
            {
                AuthorImg.InnerHtml = "<img src=\"" + Post.AuthorProfile.PhotoUrl.ToString() + "\" width=\"60px\"  height=\"60px\" alt=\"" + Post.AuthorProfile.FullName.ToString() + "\" ";
                AuthorImg.Visible = true;
            }
        }
        
    }
</script>
<div id="post<%=Index %>">
    <h1><a class="postheader" title="<%=Server.HtmlEncode(Post.Title) %>" href="<%=Post.RelativeLink %>"><%=Server.HtmlEncode(Post.Title) %></a></h1>
    <div class="postdescription"><img id="Img1" src="img/Clock.png" runat="server" alt="clock" /> <%=Post.DateCreated.ToString("MMMM d, yyyy HH:mm")%> by <img id="Img2" src="img/author.png" runat="server" alt="author" /> <a href="<%=VirtualPathUtility.ToAbsolute("~/") + "author/" + Post.Author %>.aspx"><%=Post.AuthorProfile != null ? Post.AuthorProfile.DisplayName : Post.Author%></a> | <%=AdminLinks %></div>
   
    <div class="postcontent"><asp:PlaceHolder ID="BodyContent" runat="server" /></div>

    <div id="author" runat="server" visible="false">

    <strong>About <%=Post.AuthorProfile != null ? Post.AuthorProfile.FullName : Post.Author%></strong><br />
    <div id="AuthorImg" runat="server" visible="false"></div>
    <br /><%=Post.AuthorProfile != null ? Post.AuthorProfile.AboutMe.ToString().Replace("<p>", "").Replace("</p>", "").Replace("\r\n", "").Replace("<br/>", "").Replace("<br>", "") : Post.Author%> View all posts by <a href="<%=VirtualPathUtility.ToAbsolute("~/") + "author/" + Post.Author %>.aspx"><%=Post.AuthorProfile != null ? Post.AuthorProfile.DisplayName : Post.Author%> →</a>
    </div><%=Rating %>
    <br />
    <div class="postfooter">
        Tags: <%=TagLinks(", ") %><br />
        Categories: <%=CategoryLinks(" | ") %><br />
        Actions: 
        <a rel="nofollow" href="mailto:?subject=<%=Server.UrlEncode(Post.Title) %>&amp;body=Thought you might like this: <%=Post.AbsoluteLink.ToString() %>">E-mail</a> | 
        
        <a href="<%=Post.PermaLink %>" rel="bookmark">Permalink</a> |
        <a rel="nofollow" href="<%=Post.RelativeLink %>#comment">
             <img id="Img4" runat="server" alt="comment" src="img/comment.png" /><%=Resources.labels.comments %> (<%=Post.ApprovedComments.Count %>)</a>
         |   
        <a rel="nofollow" href="<%=CommentFeed %>"><asp:Image ID="Image1" runat="Server" ImageUrl="~/pics/rssButton.gif" AlternateText="RSS comment feed" style="margin-right:3px" /><%=Server.HtmlEncode(Post.Title) %></a>
    </div>
    <br />
</div>