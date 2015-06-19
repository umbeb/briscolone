namespace BlogEngine.Core.Json
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;

    /// <summary>
    /// Post type
    /// </summary>
    public enum PostType
    {
        /// <summary>
        /// All posts
        /// </summary>
        All,
        /// <summary>
        /// Drafts
        /// </summary>
        Draft,
        /// <summary>
        /// Published posts
        /// </summary>
        Published
    }

    /// <summary>
    /// List of posts
    /// </summary>
    public static class JsonPosts
    {
        /// <summary>
        /// The current page.
        /// </summary>
        private static int currentPage = 1;
        
        /// <summary>
        /// The comm cnt.
        /// </summary>
        private static int postCnt;

        /// <summary>
        /// Gets post list based on selection for current page
        /// </summary>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page Size</param>
        /// <param name="postType">Selected post type: draft, published or all</param>
        /// <param name="filter">Secondary filter: category, tag, author or all</param>
        /// <param name="title">Value selected in secondary filter</param>
        /// <returns>List of posts</returns>
        public static List<JsonPost> GetPosts(int page, int pageSize, string postType, string filter, string title)
        {
            pageSize = Math.Max(pageSize, 1);
            var cntTo = page * pageSize;
            var cntFrom = cntTo - pageSize;
            var cnt = 0;

            var allPosts = new List<Post>();
            var filteredPosts = new List<Post>();
            var pagePosts = new List<JsonPost>();

            // first filter on selected post type
            switch (postType)
            {
                case "Published":
                    allPosts = (from p in Post.Posts.ToList() where p.IsPublished == true select p).ToList();
                    break;
                case "Draft":
                    allPosts = (from p in Post.Posts where p.IsPublished == false select p).ToList();
                    break;
                default:
                    allPosts = (from p in Post.Posts select p).ToList();
                    break;
            }

            // now filter first results on secondary filter
            switch (filter)
            {
                case "Category":
                    filteredPosts = (from x in allPosts where x.Categories.Contains(Category.GetCategory(new Guid(title))) orderby x.DateCreated descending select x).ToList();
                    break;
                case "Tag":
                    filteredPosts = (from x in allPosts where x.Tags.Contains(title) orderby x.DateCreated descending select x).ToList();
                    break;
                case "Author":
                    filteredPosts = (from x in allPosts where x.Author.Equals(title) orderby x.DateCreated descending select x).ToList();
                    break;
                default:
                    filteredPosts = (from x in allPosts orderby x.DateCreated descending select x).ToList();
                    break;
            }

            // convert each post into smaller Json friendly object 
            foreach (var x in filteredPosts)
            {
                cnt++;
                if (cnt <= cntFrom || cnt > cntTo)
                {
                    continue;
                }

                string tags = x.Tags.Aggregate("", (current, tag) => current + (tag + ","));

                var jp = new JsonPost
                {
                    Id = x.Id,
                    Author = GetAuthor(x.Author),
                    Title = string.Format("<a href=\"{0}\">{1}</a>", x.RelativeLink, System.Web.HttpContext.Current.Server.HtmlEncode(x.Title)),
                    Date = x.DateCreated.ToString("dd MMM yyyy"),
                    Time = x.DateCreated.ToString("t"),
                    Categories = GetCategories(x.Categories),
                    Tags = GetTags(x.Tags),
                    Comments = GetComments(x.Comments, x.RelativeLink),
                    //CommentsList= x.Comments,
                    IsPublished = x.IsPublished,
                    CanUserEdit = x.CanUserEdit,
                    CanUserDelete = x.CanUserDelete
                };
                pagePosts.Add(jp);
            }

            currentPage = page;
            postCnt = cnt;

            return pagePosts;
        }

        /// <summary>
        /// Builds pager control for posts page
        /// </summary>
        /// <param name="page">Current Page Number</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns></returns>
        public static string GetPager(int page, int pageSize)
        {
            if (postCnt == 0)
            {
                return string.Empty;
            }
            if (page < 1) page = 1;

            var prvLnk = string.Empty;
            var nxtLnk = string.Empty;
            var firstLnk = string.Empty;
            var lastLnk = string.Empty;
            const string linkFormat = "<a href=\"#\" id=\"{0}\" onclick=\"return LoadPostsForPage('{1}');\" class=\"{0}\"></a>";

            pageSize = Math.Max(pageSize, 1);
            var pgs = Convert.ToDecimal(postCnt) / Convert.ToDecimal(pageSize);
            var p = pgs - (int)pgs;
            var lastPage = p > 0 ? (int)pgs + 1 : (int)pgs;

            var postTo = page * pageSize;
            if (postTo > postCnt) postTo = postCnt;

            var currentScope = ((page * pageSize) - (pageSize - 1)).ToString() + " - " + postTo.ToString();

            var pageLink = string.Format("Showing <span id=\"PagerCurrentPage\">{0}</span> of {1}", currentScope, postCnt);

            if (currentPage > 1)
            {
                prvLnk = string.Format(linkFormat, "prevLink", page - 1);
                firstLnk = string.Format(linkFormat, "firstLink", 1);
            }

            if (page < lastPage)
            {
                nxtLnk = string.Format(linkFormat, "nextLink", page + 1);
                lastLnk = string.Format(linkFormat, "lastLink", lastPage);
            }

            return "<div id=\"ListPager\">" + firstLnk + prvLnk + pageLink + nxtLnk + lastLnk + "</div>";
        }

        #region Private Methods

        static string GetCategories(ICollection<Category> categories)
        {
            if (categories == null || categories.Count == 0)
                return string.Empty;

            var html = categories.Aggregate("", (current, cat) => current + string.Format("<a href='#' onclick=\"ChangePostFilter('Category','{0}','{1}')\">{1}</a>, ", cat.Id, cat.Title));
            return html.Trim().Substring(0, html.Trim().Length - 1);
        }

        static string GetTags(ICollection<string> tags)
        {
            if (tags == null || tags.Count == 0)
                return string.Empty;

            var html = tags.Aggregate("", (current, tag) => current + string.Format("<a href='#' onclick=\"ChangePostFilter('Tag','{0}','')\">{0}</a>, ", tag));
            return html.Trim().Substring(0, html.Trim().Length - 1);
        }

        static string GetAuthor(string  author)
        {
            if (string.IsNullOrEmpty(author)) 
                return string.Empty;
            return string.Format("<a href='#' onclick=\"ChangePostFilter('Author','{0}','')\">{0}</a>", author);
        }

        static string GetComments(ICollection<Comment> comments, string postUrl)
        {
            int pending, approved;
            int spam = comments.Count(c => c.IsSpam == true && c.IsDeleted == false);

            string pLink = "<a href=\"{0}\" class=\"comCountPending tipsyhelp\" original-title=\"Pending comments\">({1})</a>";
            string aLink = "<a href=\"{0}\" class=\"comCountApproved tipsyhelp\" original-title=\"Approved comments, visible to public\">{1}</a>";
            string sLink = "<a href=\"{0}\" class=\"comCountSpam tipsyhelp\" original-title=\"Spam comments\">({1})</a>";

            sLink = spam > 0 ? string.Format(sLink, postUrl + "#comments", spam) : "";

            if (BlogSettings.Instance.EnableCommentsModeration)
            {
                pending = comments.Count(c => (c.IsApproved == false && c.IsSpam == false && c.IsDeleted == false));
                approved = comments.Count(c => c.IsApproved == true && c.IsDeleted == false);

                pLink = pending > 0 ? string.Format(pLink, postUrl + "#comment", pending) : "";
                aLink = approved > 0 ? string.Format(aLink, postUrl + "#comment", approved) : "<span class=\"comNone tipsyhelp\" original-title=\"Approved comments, visible to public\">0</span>";

                return aLink + pLink + sLink;
            }
            else
            {
                approved = comments.Count(c => c.IsSpam == false);
                aLink = approved > 0 ? string.Format(aLink, postUrl + "#comment", approved) : "<span class=\"comNone tipsyhelp\" original-title=\"Approved comments, visible to public\">0</span>";
                return aLink + sLink;
            }
        }

        public static List<JsonComment> GetCommentsList(int numComments)
        {

            Post lastPost = (from post in Post.Posts
                           orderby post.DateCreated descending
                           select post
                           ).FirstOrDefault();

                                     
            JsonComment jComm;
            List<JsonComment> lstComment = new List<JsonComment>();
            List<Comment> lstCommentBase = null;
            if (numComments > 0)
            {
                lstCommentBase = lastPost.AllComments.Take(numComments).ToList();
            }
            else
            {
                lstCommentBase = lastPost.AllComments;
            }
            foreach (var item in lstCommentBase)
            {
                jComm = new JsonComment();
                jComm.Author = item.Author;
                jComm.Content = WebUtility.HtmlDecode( item.Content).ToString().Substring(0,15);
                jComm.Date = item.DateCreated.ToString("dd/MM/yyyy") ;
                jComm.Email = item.Email;
                jComm.Id = item.Id;
                jComm.IdPost = lastPost.Id;
                lstComment.Add(jComm);
            }

            return lstComment;
        }

        public static JsonComment GetComment(string idPost, string idComment)
        {

            Post currentPost = Post.Posts.Where (p=> p.Id.Equals(new Guid(idPost))).FirstOrDefault();
            Comment comment = (from comm in currentPost.AllComments
                               where comm.Id.ToString()==idComment
                               select comm).FirstOrDefault();


            JsonComment jComm = new JsonComment();
            jComm.Author = comment.Author;
            jComm.Content = WebUtility.HtmlDecode(comment.Content).ToString();
            jComm.Date = comment.DateCreated.ToString("dd/MM/yyyy");
            jComm.Email = comment.Email;
            jComm.Id = comment.Id;
            jComm.IdPost = currentPost.Id;

            return jComm;

            
        }
        #endregion

    }
}
