﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TailspinSpyworks.Data_Access;

namespace TailspinSpyworks.Controls
{
    public partial class PopularItem : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
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
                                } into orderdgrp where orderdgrp.Quantity > 0 orderby orderdgrp.Quantity descending select orderdgrp).Take(5);

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