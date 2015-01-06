<%@ Control Language="C#" EnableViewState="False" Inherits="BlogEngine.Core.Web.Controls.CommentViewBase" %>


<div id="id_<%=Comment.Id %>" class="vcard alt comment<%= Post.Author.Equals(Comment.Author, StringComparison.OrdinalIgnoreCase) ? " self" : "" %>">
<%= Gravatar(32)%>
<cite>
    <%= Comment.Website != null ? "<a href=\"" + Comment.Website + "\" class=\"url fn\">" + Comment.Author + "</a>" : "<span class=\"fn\">" +Comment.Author + "</span>" %>
    <%= Flag %> 
</cite> 
says:
<br />
<div id="container-comment">
<div id="commentheader"></div>
<div id="commentmain"><%= Text %></div>
<div id="commentfooter">&nbsp;</div>
</div>
<div ><%= AdminLinks %> - <%= Comment.DateCreated.ToLongDateString() %> at <%= Comment.DateCreated.ToShortTimeString() %> - 
  <%= ReplyToLink %> </div>
  
    <div id="replies_<%=Comment.Id %>" <%= (Comment.Comments.Count == 0) ? " style=\"display:none;\"" : "" %>>
	<asp:PlaceHolder ID="phSubComments" runat="server" />
  </div>
</div>