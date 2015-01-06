<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" Inherits="BlogEngine.Core.Web.Controls.PostViewBase" %>
<div class="post xfolkentry" id="post<%=Index %>">
    <div class="postTitle">
        <p class="date"><%=Post.DateCreated.ToString("MMMM") %> <%=Post.DateCreated.ToString("yy") %><b><%=Post.DateCreated.ToString("dd") %></b></p>
        <h1><a href="<%=Post.RelativeLink %>" class="taggedlink"><%=Server.HtmlEncode(Post.Title) %></a></h1>
    </div>
    <div class="text">
        <asp:PlaceHolder ID="BodyContent" runat="server" />
        <%=Rating %>

        <span class="tags">Tags: <%=TagLinks(", ") %></span> | <span class="categories">Categories: <%=CategoryLinks(" | ") %></span>
        <div class="postMeta">
            <a rel="nofollow" href="mailto:?subject=<%=Post.Title %>&amp;body=Thought you might like this: <%=Post.AbsoluteLink.ToString() %>"> E-mail</a> |
            <a rel="nofollow" href="<%=CommentFeed %>">Post RSS<img src="<%=VirtualPathUtility.ToAbsolute("~/pics/")%>rssButton.gif" alt="RSS comment feed" style="margin-left: 3px" /></a> 
            
            <span class="adminLinks"><%=AdminLinks %></span>

            <% if (BlogEngine.Core.BlogSettings.Instance.ModerationType == BlogEngine.Core.BlogSettings.Moderation.Disqus)
               { %>
            <a rel="nofollow" href="<%=Post.PermaLink %>#disqus_thread">
                <%=Resources.labels.comments %></a>
            <% }
               else
               { %>
            <a rel="bookmark" href="<%=Post.PermaLink %>" title="<%=Server.HtmlEncode(Post.Title) %>">Permalink</a> | 
            <a rel="nofollow" href="<%=Post.RelativeLink %>#comment"><%=Resources.labels.comments %>(<%=Post.ApprovedComments.Count %>)</a>
            <%} %>
        </div>
    </div>
</div>
