<%@ Control Language="C#" EnableViewState="False" Inherits="BlogEngine.Core.Web.Controls.CommentViewBase" %>
<%@ Import Namespace="BlogEngine.Core" %>

<div id="id_<%=Comment.Id %>" class="comment byuser comment-author-feinifuh <%= Post.Author.Equals(Comment.Author, StringComparison.OrdinalIgnoreCase) ? " bypostauthor" : "" %> even thread-even">
		<div id="comment-<%=Comment.Id %>" class="iddiv">
		<div class="comment-gravatar"><%= Gravatar(65)%></div>
		
		<div class="comment-body">
		<div class="comment-meta commentmetadata"> 
		<cite class="fn"><%= Comment.Website != null ? "<a href=\"" + Comment.Website + "\" rel=\"nofollow\" class=\"url fn\">" + Comment.Author + "</a>" : "<span class=\"fn\">" +Comment.Author + "</span>" %></cite> <%= Flag %>
		<%= ReplyToLink %><%= AdminLinks %><br/>
		<a href="#id_<%=Comment.Id %>">	<%= Comment.DateCreated %></a>		
		</div>

		<p><%= Text %></p>
		
		
		<div class="reply"><%= ReplyToLink %></div><!-- .reply -->
		
		</div>
		<div class="clear" ></div>
		<!--comment Body-->
		
	</div><!-- #comment-##  -->
<%if (Comment.Comments.Count > 0)
  { %>
<div class="children" id="replies_<%=Comment.Id%>">
	<asp:PlaceHolder ID="phSubComments" runat="server" />
</div>
 <% } %>
</div>  

