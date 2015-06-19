namespace BlogEngine.Core.Json
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Wrapper for Json serializible comment list
    /// </summary>
    [DataContract]
    public class JsonComment
    {
        #region Constants and Fields

        /// <summary>
        ///     Gets or sets the Comment Id
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid IdPost { get; set; }

        /// <summary>
        ///     Gets or sets the Email
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the Author
        /// </summary>
        [DataMember]
        public string Author { get; set; }

        /// <summary>
        ///     Gets the avatar image
        /// </summary>
        public string Avatar
        {
            get
            {
                Uri uri = string.IsNullOrEmpty(Website) ? null : new Uri(Website);
                Avatar avatar = Core.Avatar.GetAvatar(32, Email, uri, null, Author);
                
                if (avatar.HasNoImage || string.IsNullOrEmpty(Email) || !Email.Contains("@"))
                {
                    // <img> tag pointing to noavatar.jpg, or no image if Avatar setting is "none".
                    return avatar.ImageTag ?? string.Empty;
                }

                const string linkWithImage = "<a href=\"mailto:{2}\" alt=\"{0}\" title=\"{0}\">{1}</a>";
                return string.Format(CultureInfo.InvariantCulture, linkWithImage, Author, avatar.ImageTag, Email);
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this comment has nested comments
        /// </summary>
        public bool HasChildren
        {
            get
            {
                return GetChildren(Id);
            }
        }

        /// <summary>
        ///     Gets or sets the comment title
        /// </summary>

        public string Title { get; set; }

        /// <summary>
        ///     Gets or sets the shorten comment content
        /// </summary>

        public string Teaser { get; set; }

        /// <summary>
        ///     Gets or sets the author's website
        /// </summary>

        public string Website { get; set; }

        /// <summary>
        /// Static avatar image
        /// </summary>

        public string AuthorAvatar { get; set; }
        /// <summary>
        ///     Gets or sets the comment body
        /// </summary>
        [DataMember]
        public string Content { get; set; }

        /// <summary>
        ///     Gets or sets the ip
        /// </summary>

        public string Ip { get; set; }

        /// <summary>
        ///     Gets or sets the date portion of published date
        /// </summary>
        [DataMember]
        public string Date { get; set; }

        /// <summary>
        ///     Gets or sets the time portion of published date
        /// </summary>

        public string Time { get; set; }

        #endregion

        /// <summary>
        /// If connent has nested comments
        /// </summary>
        /// <param name="comId">
        /// Comment Id
        /// </param>
        /// <returns>
        /// True if has child records
        /// </returns>
        private static bool GetChildren(Guid comId)
        {
            return Post.Posts.SelectMany(p => p.Comments).Any(c => c.ParentId == comId);
        }
    }
}