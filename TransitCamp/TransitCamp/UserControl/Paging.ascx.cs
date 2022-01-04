using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TransitCamp.UserControl
{
    public partial class Paging : System.Web.UI.UserControl
    {
        #region Properties
        private Int32 pageSize = 0;

        private Int32 totalItems = 0;

        private Int32 currentPage = 0;

        public Int32 PageSize

        {

            get { return pageSize; }

            set { pageSize = value; }

        }

        public Int32 TotalItems

        {

            get { return totalItems; }

            set { totalItems = value; }

        }

        public Int32 CurrentPage

        {

            get { return currentPage; }

            set { currentPage = value; }

        }

        #endregion

        #region Events 

        public event EventHandler Paging_Click;

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var data = CreatePages(pageSize, totalItems, currentPage);
                repPagesBottom.DataSource = data;
                repPagesBottom.DataBind();
            }
        }

        public IEnumerable<Page> CreatePages(int pageSize, int totalItems, int currentPage)
        {
            List<Page> pages = new List<Page>();
            int totalPages = (totalItems / pageSize) + 1;
            int startIndex = 0;
            int endIndex = totalPages;

            if (totalPages > 10)
            {
                startIndex = currentPage - 5;
                endIndex = currentPage + 5;
                if (startIndex < 0)
                {
                    startIndex = 0;
                    endIndex = startIndex + 10;
                }
                if (endIndex > totalPages)
                {
                    endIndex = totalPages;
                    startIndex = totalPages - 10;
                }
            }
            pages.Add(new Page { Title = "««", PageNum = "1", CurrentPage = false });
            if (currentPage == 1)
                pages.Add(new Page { Title = "«", PageNum = (currentPage).ToString(), CurrentPage = false });
            else
                pages.Add(new Page { Title = "«", PageNum = (currentPage - 1).ToString(), CurrentPage = false });
            for (int i = startIndex; i < endIndex; i++)
            {
                Page page = new Page { Title = (i + 1).ToString(), PageNum = (i + 1).ToString(), CurrentPage = i == (currentPage - 1) };
                pages.Add(page);
            }
            if (currentPage == totalPages)
                pages.Add(new Page { Title = "»", PageNum = (currentPage).ToString(), CurrentPage = false });
            else
                pages.Add(new Page { Title = "»", PageNum = (currentPage + 1).ToString(), CurrentPage = false });
            pages.Add(new Page { Title = "»»", PageNum = totalPages.ToString(), CurrentPage = false });
            return pages;
        }

        protected void repPagesBottom_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            CurrentPage = Convert.ToInt32((string)e.CommandArgument);

            var data = CreatePages(pageSize, totalItems, currentPage);
            repPagesBottom.DataSource = data;
            repPagesBottom.DataBind();

            if (this.Paging_Click != null)
            {
                this.Paging_Click(this, e);
            }
        }

    }
    public class Page
    {
        public string Title { get; set; }
        public string PageNum { get; set; }
        public bool CurrentPage { get; set; }
    }
}