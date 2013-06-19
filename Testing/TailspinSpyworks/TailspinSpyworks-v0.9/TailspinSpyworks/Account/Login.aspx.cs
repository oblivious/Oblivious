using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace TailspinSpyworks.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Page.Request.UrlReferrer != null)
                {
                    Session["LoginReferrer"] = Page.Request.UrlReferrer.ToString();
                }
            }
           
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/");

            }

        }

        protected void LoginUser_LoggedIn(object sender, EventArgs e)
        {
            MyShoppingCart usersShoppingCart = new MyShoppingCart();
            String cartId = usersShoppingCart.GetShoppingCartId();
            usersShoppingCart.MigrateCart(cartId, LoginUser.UserName);
            
            if(Session["LoginReferrer"] != null)
            {
                Response.Redirect(Session["LoginReferrer"].ToString());
            }

            Session["UserName"] = LoginUser.UserName;
        }
    }
}
