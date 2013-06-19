using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TailspinSpyworks.Data_Access;
using System.Diagnostics;

namespace TailspinSpyworks.Controls
{
    public partial class AlsoPurchased : System.Web.UI.UserControl
    {
        private int _ProductId;

        public int ProductId
        {
            get { return _ProductId ; }
            set { _ProductId = Convert.ToInt32(value); }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------+
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (_ProductId < 1)
            {
                // This should never happen but we could expand the use of this control by reducing the 
                // dependency on the query string by selecting a few RANDOME products here. 
                Debug.Fail("ERROR : The Also Purchased Control Can not be used without setting the ProductId.");
                throw new Exception("ERROR : It is illegal to load the AlsoPurchased COntrol without setting a ProductId.");
            }
      
            int ProductCount = 0;
            using (CommerceEntities db = new CommerceEntities())
            {
                try
                {
                    var v = db.SelectPurchasedWithProducts(_ProductId);
                    ProductCount = v.Count();
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Retrieve Also Purchased Items - " + exp.Message.ToString(), exp);
                }
            }

            if (ProductCount > 0)
            {
                WriteAlsoPurchased(_ProductId);              
            }
            else
            {
                 WritePopularItems();
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------+
        private void WriteAlsoPurchased(int currentProduct)
        {
            using (CommerceEntities db = new CommerceEntities())
            {
                try
                {
                    var v = db.SelectPurchasedWithProducts(currentProduct);
                    RepeaterItemsList.DataSource = v;
                    RepeaterItemsList.DataBind();
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Write Also Purchased - " + exp.Message.ToString(), exp);
                }
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------+
        private void WritePopularItems()
        {
            using (CommerceEntities db = new CommerceEntities())
            {
                try
                {
                    var query = (from ProductOrders in db.OrderDetails
                                 join SelectedProducts in db.Products on ProductOrders.ProductID equals SelectedProducts.ProductID
                                 group ProductOrders by new
                                 {
                                     ProductId = SelectedProducts.ProductID,
                                     ModelName = SelectedProducts.ModelName

                                 } into grp
                                 select new
                                 {
                                     ModelName = grp.Key.ModelName,
                                     ProductId = grp.Key.ProductId,
                                     Quantity = grp.Sum(o => o.Quantity)
                                 } into orderdgrp
                                 where orderdgrp.Quantity > 0
                                 orderby orderdgrp.Quantity descending
                                 select orderdgrp).Take(5);
                    LabelTitle.Text = "Other items you might be interested in: ";

                    RepeaterItemsList.DataSource = query;
                    RepeaterItemsList.DataBind();
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Load Popular Items - " + exp.Message.ToString(), exp);
                }
            }
        }
    }
}