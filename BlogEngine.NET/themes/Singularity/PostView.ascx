<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" Inherits="BlogEngine.Core.Web.Controls.PostViewBase" %>

<div class="post xfolkentry" id="post<%=Index %>">
<div class="post-top"></div>
  <h1><a href="<%=Post.RelativeLink %>" class="taggedlink"><%=Server.HtmlEncode(Post.Title) %></a></h1>
  <div class="byline">
  <span class="pubDate"><%=Post.DateCreated.ToString("MMMM d")%><br /><%=Post.DateCreated.ToString("yyyy") %></span>
  <span class="author">Written by <a href="<%=VirtualPathUtility.ToAbsolute("~/") + "author/" + BlogEngine.Core.Utils.RemoveIllegalCharacters(Post.Author) %>.aspx"><%=Post.AuthorProfile != null ? Post.AuthorProfile.DisplayName : Post.Author %></a> &middot; Posted <a rel="nofollow" href="<%=Post.RelativeLink %>#comment"><%=Resources.labels.comments %> (<%=Post.ApprovedComments.Count %>)</a></span>
  </div>
  <div class="dude">
  <div class="text"><asp:PlaceHolder ID="BodyContent" runat="server" /></div>
    <%=Rating %>
    <p class="tags">Keywords: <%=TagLinks(", ") %></p>
    <p class="categories">Filed Under: <%=CategoryLinks(" &middot; ") %></p>
  <div class="footer">
   <%=AdminLinks %>
    <% if (BlogEngine.Core.BlogSettings.Instance.ModerationType == BlogEngine.Core.BlogSettings.Moderation.Disqus)
       { %>
    <a rel="nofollow" href="<%=Post.PermaLink %>#disqus_thread"><%=Resources.labels.comments %></a>
    <%}
       else
       { %>
    <a rel="bookmark" href="<%=Post.PermaLink %>" title="<%=Server.HtmlEncode(Post.Title) %>">Permalink</a> 
       
    <%} %>
   </div> 
  </div>
<div class="post-bottom"></div>
</div>