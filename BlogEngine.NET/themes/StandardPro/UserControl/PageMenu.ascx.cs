using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using BlogEngine.Core;

namespace Raisr.BE.Themes.ChannelPro.Controls
{
    public partial class PageMenu : UserControl
    {
        #region Declarations / Fields

        private static string HTML_CONTENT;
        private static bool SHOW_HOME;
        private static bool SHOW_ARCHIVE;
        private static bool SHOW_LOGON_LOGOFF;
        private static bool SHOW_CONTACT;
        private static bool SHOW_PAGES;
        private static string MENU_CLASS;
        private static string MENU_ID;
        private static string PARENT_ITEM_CLASS;
        private static bool IS_DIRTY;

        #endregion

        #region Constructors

        static PageMenu()
        {
            BlogEngine.Core.Page.Saved += (sender, args) => IS_DIRTY = true;
        }

        #endregion

        public bool ShowHome
        {
            get { return SHOW_HOME; }
            set
            {
                if (value != SHOW_HOME)
                {
                    SHOW_HOME = value;
                    IS_DIRTY = true;
                }
            }
        }

        public bool ShowArchive
        {
            get { return SHOW_ARCHIVE; }
            set
            {
                if (value != SHOW_ARCHIVE)
                {
                    SHOW_ARCHIVE = value;
                    IS_DIRTY = true;
                }
            }
        }

        public bool ShowContact
        {
            get { return SHOW_CONTACT; }
            set
            {
                if (value != SHOW_CONTACT)
                {
                    SHOW_CONTACT = value;
                    IS_DIRTY = true;
                }
            }
        }

        public bool ShowLogonLogoff
        {
            get { return SHOW_LOGON_LOGOFF; }
            set
            {
                if (value != SHOW_LOGON_LOGOFF)
                {
                    SHOW_LOGON_LOGOFF = value;
                    IS_DIRTY = true;
                }
            }
        }

        public bool ShowPages
        {
            get { return SHOW_PAGES; }
            set
            {
                if (value != SHOW_PAGES)
                {
                    SHOW_PAGES = value;
                    IS_DIRTY = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets the css class that will be applied to the root ul tag of the menu.
        /// </summary>
        /// <value>
        /// The menu class.
        /// </value>
        public string MenuClass
        {
            get { return MENU_CLASS; }
            set
            {
                if (value != MENU_CLASS)
                {
                    MENU_CLASS = value;
                    IS_DIRTY = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets the id attribute that will be applied to the root ul tag of the menu.
        /// </summary>
        /// <value>
        /// The menu id.
        /// </value>
        public string MenuId
        {
            get { return MENU_ID; }
            set
            {
                if (value != MENU_ID)
                {
                    MENU_ID = value;
                    IS_DIRTY = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets the css class that will be applyed to each li tag that has child items.
        /// </summary>
        /// <value>
        /// The parent item class.
        /// </value>
        public string ParentItemClass
        {
            get { return PARENT_ITEM_CLASS; }
            set
            {
                if (value != PARENT_ITEM_CLASS)
                {
                    PARENT_ITEM_CLASS = value;
                    IS_DIRTY = true;
                }
            }
        }

        #region Menu creation

        protected override void Render(HtmlTextWriter writer)
        {
            RefreshCachedHtml();
            writer.Write(ShowLogonLogoff ? ReplaceLogonLogoffPlaceholders() : HTML_CONTENT);
        }

        private string ReplaceLogonLogoffPlaceholders()
        {
            if (Security.IsAuthenticated)
                return string.Format(HTML_CONTENT, Utils.RelativeWebRoot + "Account/login.aspx?logoff", Resources.labels.logoff);
            return string.Format(HTML_CONTENT, Utils.RelativeWebRoot + "Account/login.aspx", Resources.labels.login);
        }

        private static void RefreshCachedHtml()
        {
            if (IS_DIRTY)
            {
                var navigationList = BindPages(null, -1);
                HTML_CONTENT = Utils.RenderControl(navigationList);

                IS_DIRTY = false;
            }
        }

        /// <summary>
        /// Loops through all pages and builds the HTML
        /// presentation.
        /// </summary>
        /// <returns>A list item.</returns>
        private static HtmlGenericControl BindPages(BlogEngine.Core.Page parentPage, int level)
        {
            var ul = new HtmlGenericControl("ul");
            level++;

            if (parentPage == null)
            {
                if (!string.IsNullOrEmpty(MENU_CLASS))
                    ul.Attributes.Add("class", MENU_CLASS);

                if (!string.IsNullOrEmpty(MENU_ID))
                    ul.Attributes.Add("id", MENU_ID);

                if (SHOW_HOME)
                    ul.Controls.Add(CreatePageItem(Utils.AbsoluteWebRoot.ToString(), Resources.labels.home, "", false, level));

                if (SHOW_PAGES)
                {
                    if (BlogEngine.Core.Page.Pages != null)
                    {
                        var rootPages = BlogEngine.Core.Page.Pages.Where(
                                        page => !page.HasParentPage && page.ShowInList && page.IsVisibleToPublic);

                        foreach (var rootPage in rootPages.OrderBy(page => page.Title))
                        {
                            var li = CreatePageItem(rootPage.RelativeLink, rootPage.Title, rootPage.Description, rootPage.HasChildPages, level);
                            if (rootPage.HasChildPages)
                            {
                                li.Controls.Add(BindPages(rootPage, level));
                            }
                            ul.Controls.Add(li);
                        }
                    } 
                }

                if (SHOW_ARCHIVE)
                    ul.Controls.Add(CreatePageItem(Utils.AbsoluteWebRoot + "archive.aspx", Resources.labels.archive, "", false, level));

                if (SHOW_CONTACT)
                    ul.Controls.Add(CreatePageItem(Utils.AbsoluteWebRoot + "contact.aspx", Resources.labels.contact, "", false, level));

                if (SHOW_LOGON_LOGOFF)
                    ul.Controls.Add(CreatePageItem("{0}", "{1}", "", false, level));

            }
            else
            {
                var childPages =
                    BlogEngine.Core.Page.Pages.Where(
                        page => page.HasParentPage && page.ShowInList && page.IsVisibleToPublic && page.Parent.Equals(parentPage.Id));


                foreach (var childPage in childPages)
                {
                    var li = CreatePageItem(childPage.RelativeLink, childPage.Title, childPage.Description, childPage.HasChildPages, level);
                    if (childPage.HasChildPages)
                    {
                        li.Controls.Add(BindPages(childPage, level));
                    }
                    ul.Controls.Add(li);
                }
            }

            return ul;
        }

        #endregion

        private static HtmlGenericControl CreatePageItem(string url, string text, string description, bool hasChildren, int level)
        {
            const string itemClassTemplate = "level{0}{1}";
            
            var li = new HtmlGenericControl("li");
            var anc = new HtmlAnchor { HRef = url, InnerHtml = text, Title = description };
            li.Controls.Add(anc);
            
            var cssClass = string.Format(itemClassTemplate, level, "{0}");
            cssClass = string.Format(cssClass, hasChildren ? " " + PARENT_ITEM_CLASS : "");
            
            li.Attributes.Add("class", cssClass);
            return li;
        }
    }
}