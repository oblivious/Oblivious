using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
//--------------------------------------------------------------------------------------------------------------------------------------------------+
// TODO: A good enhancement would be to use URL Symantecs instead ofQueryString Paramenters.
//--------------------------------------------------------------------------------------------------------------------------------------------------+
namespace TailspinSpyworks
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (Session["UserName"] == null)
                {
                   Session["UserName"] = HttpContext.Current.User.Identity.Name;
                }
            }
        }
    }
}
