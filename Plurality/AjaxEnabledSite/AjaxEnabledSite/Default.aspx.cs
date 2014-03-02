using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxEnabledSite
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
            }
            if (sm.IsInAsyncPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            //Datagrid
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            TimeLabel.Text = DateTime.Now.ToShortTimeString();
        }
    }
}