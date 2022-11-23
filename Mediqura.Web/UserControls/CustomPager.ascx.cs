using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.UserControls
{
    public partial class CustomPager : System.Web.UI.UserControl
    {
        public delegate void PageCommandDelegate(object sender, PagerEventArgs e);
        public event PageCommandDelegate PageCommand;
        private int _pageIndex = 1;
        private int _pageSize = 10;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                BindPage();
                PreviousEnabled = false;

            }
        }
        public void BindPage()
        {
            rptPage.DataSource = new int[this.TotalPages];

            rptPage.DataBind();
        }
        public int PageSize
        {
            get
            {
                int size;
                int.TryParse(pageSize.Value, out size);
                return size == 0 ? _pageSize : size;
            }
            set
            {
                this.pageSize.Value = value.ToString();
                // TODO Does this belong here????
                NextEnabled = PageIndex < TotalPages;
            }
        }
        public int TotalRecords
        {
            get
            {
                int total = 0;
                int.TryParse(totalItems.Value, out total);
                return total;
            }
            set
            {
                totalItems.Value = value.ToString();
                NextEnabled = PageIndex < TotalPages - 1;
                // pageNumber.MaxLength = TotalPages.ToString().Length;
                // BindPage need here to automatically bind pages whenever TotalRecords available
                // if this is removed, it has to call BindPage method wherever it is using
                BindPage();
            }
        }
        public int TotalPages
        {
            get { return (PageSize == 0 ? 0 : (int)Math.Ceiling((decimal)TotalRecords / (decimal)PageSize)); }
        }
        public bool PreviousEnabled
        {
            get { return btnPrev.Enabled; }
            set
            {
                btnPrev.Enabled = value;
                //SetDisablePreviousCss();
            }
        }
        public bool NextEnabled
        {
            get { return btnNext.Enabled; }
            set
            {
                btnNext.Enabled = value;
                //SetDisableNextCss();
            }
        }
        public int PageIndex
        {
            get
            {
                int page;
                int.TryParse(origPageNumber.Value, out page);
                return Math.Max(0, page);
            }
            set
            {
                int pageIndex = Math.Max(0, value);

                if (TotalPages > 0)
                    pageIndex = Math.Min(pageIndex, TotalPages);

                origPageNumber.Value = pageIndex.ToString();

                PreviousEnabled = pageIndex > 0;
                NextEnabled = TotalPages > 0 && pageIndex < TotalPages - 1;
            }
        }
        public string PagedControlID
        {
            get;
            set;
        }
        protected void rptPage_ItemdataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (PageIndex == e.Item.ItemIndex)
                {
                    LinkButton lbtnPage = (LinkButton)e.Item.FindControl("lbtnPage");
                    lbtnPage.Enabled = false;
                }

            }
        }
        protected void Paging_Click(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "PageNumber")
            {
                PageIndex = Convert.ToInt32(e.CommandArgument);
            }
            else
            {
                PageIndex = Convert.ToInt32(e.CommandArgument) + int.Parse(origPageNumber.Value);
            }
            BindPage();

            if (PageCommand != null)
            {
                PageCommand(sender, new PagerEventArgs(PageIndex));
            }

        }
    }
}