<%@ Control Language="C#" EnableViewState="False" Inherits="BlogEngine.Core.Web.Controls.CommentViewBase" %>

<div id="id_<%=Comment.Id %>" class="vcard comment<%= Post.Author.Equals(Comment.Author, StringComparison.OrdinalIgnoreCase) ? " self" : "" %>">
  <div class="comment_header">
    <span class="gravatar"><%= Gravatar(64)%></span>
    <div class="visitor">
        <%= Comment.Website != null ? "<a href=\"" + Comment.Website + "\" rel=\"nofollow\" class=\"url fn\">" + Comment.Author + "</a>" : "<span class=\"fn\">" +Comment.Author + "</span>" %>
        <%= Flag %>
        <div class="postDate"><%= Comment.DateCreated %> <a href="#id_<%=Comment.Id %>">#</a></div>
    </div>  
  </div>
    
  <p class="comment-content"><%= Text %></p>
  <br style="clear:both;" />
  <p class="author">          
    <%= AdminLinks.Length > 2 ? AdminLinks.Substring(2) : AdminLinks %>
    <span style="float:right"><%=ReplyToLink%></span>
    <br />
  </p>
  
  <div class="comment-replies" id="replies_<%=Comment.Id %>" <%= (Comment.Comments.Count == 0 || Comment.Email == "pingback" || Comment.Email == "trackback") ? " style=\"display:none;\"" : "" %>>
	<asp:PlaceHolder ID="phSubComments" runat="server" />
  </div>
  
</div>