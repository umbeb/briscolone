<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" Inherits="BlogEngine.Core.Web.Controls.PostViewBase" %>

<article class="post" id="post<%=Index %>">
<div class="entry-details">
	<p><%=Post.DateCreated.ToString("d. MMMM yyyy HH:mm") %><br>
		by <a href="<%=VirtualPathUtility.ToAbsolute("~/") + "author/" + Post.Author %>.aspx"><%=Post.AuthorProfile != null ? Post.AuthorProfile.DisplayName : Post.Author %></a><br>
		<a rel="nofollow" href="<%=Post.RelativeLink %>#comment"><%=Post.ApprovedComments.Count %> <%=Resources.labels.comments %></a>
		</p>
</div>
<header class="entry-header">
	<h2 class="entry-title"><a href="<%=Post.RelativeLink %>" class="taggedlink"><%=Server.HtmlEncode(Post.Title) %></a></h2>
	<div class="single-entry-details">
	<p><%=Post.DateCreated.ToString("d. MMMM yyyy HH:mm") %> by <a href="<%=VirtualPathUtility.ToAbsolute("~/") + "author/" + Post.Author %>.aspx"><%=Post.AuthorProfile != null ? Post.AuthorProfile.DisplayName : Post.Author %></a> | <a rel="nofollow" href="<%=Post.RelativeLink %>#comment"><%=Post.ApprovedComments.Count %> <%=Resources.labels.comments %></a></p>
</div>
</header>
<div class="entry-content">
	<asp:PlaceHolder ID="BodyContent" runat="server" />
	<footer class="single-entry-meta">
	<p>	Categories: <%=CategoryLinks(" , ") %> | Tags: <%=TagLinks(", ") %> | <%=AdminLinks %><a rel="bookmark" href="<%=Post.PermaLink %>">Permalink</a></p>
</footer>
</div>

</article>
<div class="clear"></div>