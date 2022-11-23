using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mediqura.Web.MedCommon
{
    public class PagerEventArgs
    {
        private int pageIndex = 0;
        public PagerEventArgs(PagerEventArgs e)
        {
            this.PageIndex = e.PageIndex;
        }
        public PagerEventArgs(int pageIndex)
        {
            this.PageIndex = pageIndex;
        }
        #region Event properties

        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }

        #endregion
    }
}