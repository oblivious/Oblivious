using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TailspinSpyworks.Data_Access;

namespace TailspinSpyworks.Account
{
    public partial class OrderDetails : System.Web.UI.Page
    {
        decimal _CartTotal = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Request.QueryString["OrderId"]))
            {
                Response.Redirect("~/Account/OrderList.aspx");
            }
        }

        protected void MyList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TailspinSpyworks.Data_Access.VewOrderDetail myCart = new Data_Access.VewOrderDetail();
                myCart = (TailspinSpyworks.Data_Access.VewOrderDetail)e.Row.DataItem;
                _CartTotal += Convert.ToDecimal(myCart.UnitCost * myCart.Quantity);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {

                e.Row.Cells[5].Text = "Total: " + _CartTotal.ToString("C");
            }
        }
    }
}