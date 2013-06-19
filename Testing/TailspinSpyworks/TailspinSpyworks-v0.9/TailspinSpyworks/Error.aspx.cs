using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TailspinSpyworks
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label_ErrorFrom.Text = Request["Err"].ToString();
            Label_ErrorMessage.Text = Request["InnerErr"].ToString();
        }
    }
}