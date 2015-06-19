
namespace BlogEngine.Core.Json
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    /// <summary>
    /// Wrapper aroung Post
    /// used to show post list in the admin
    /// </summary>
    /// 
    [DataContract]
    public class JsonPost
    {
        /// <summary>
        /// Post ID
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }
        
        /// <summary>
        /// Post title
        /// </summary>
        /// 
        [DataMember]
        public string Title { get; set; }
        
        /// <summary>
        /// Post author
        /// </summary>
        [DataMember]
        public string Author { get; set; }

        /// <summary>
        ///     Gets or sets the date portion of published date
        /// </summary>
        [DataMember]
        public string Date { get; set; }

        /// <summary>
        ///     Gets or sets the time portion of published date
        /// </summary>
        [DataMember]
        public string Time { get; set; }

        /// <summary>
        /// Comma separated list of post categories
        /// </summary>
        [DataMember]
        public string Categories { get; set; }

        /// <summary>
        /// Comma separated list of post tags
        /// </summary>
        [DataMember]
        public string Tags { get; set; }

        /// <summary>
        /// Comment counts for the post
        /// </summary>
        [DataMember]
        public string Comments { get; set; }
        

        /// <summary>
        /// Gets or sets post status
        /// </summary>
        [DataMember]
        public bool IsPublished { get; set; }

        /// <summary>
        /// If the current user can delete this page.
        /// </summary>
        [DataMember]
        public bool CanUserDelete { get; set; }

        /// <summary>
        /// If the current user can edit this page.
        /// </summary>
        [DataMember]
        public bool CanUserEdit { get; set; }
    }
}
