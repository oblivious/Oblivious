using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using TailspinSpyworks.Data_Access;

namespace TailspinSpyworks
{
    public partial class ReviewAdd : System.Web.UI.Page
    {
        //------------------------------------------------------------------------------------------------------------------------------------------+
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ProductID"]))
                {
                int  productID = 0;
                Int32.TryParse(Request["productID"].ToString(), out productID);
                using (CommerceEntities db = new CommerceEntities())
                    {
                        try
                        {
                            var thisProduct = (from p in db.Products where p.ProductID == productID select p).FirstOrDefault();
                            ModelName.Text = thisProduct.ModelName;
                        }
                        catch (Exception exp)
                        {
                            throw new Exception("ERROR: Unable to Add Product Review - " + exp.Message.ToString(), exp);
                        }
                    }
                }
            else
                {
                Debug.Fail("ERROR : We should never get to ReviewAdd.aspx without a ProductId.");
                throw new Exception("ERROR : It is illegal to load ReviewAdd.aspx without setting a ProductId.");
                }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------+
        protected void ReviewAddBtn_Click(object sender, ImageClickEventArgs e)
        {
            if (Page.IsValid == true)
            {
                // Obtain ProductID from Page State
                int productID = Int32.Parse(Request["productID"]);

                // Obtain Rating number of RadioButtonList
                int rating = Int32.Parse(Rating.SelectedItem.Value);

                // Add Review to ReviewsDB.  HtmlEncode before entry
                using (CommerceEntities db = new CommerceEntities())
                {
                    try
                    {
                        ShoppingCart cartadd = new ShoppingCart();
                        Review newReview = new Review()
                        {
                            ProductID = productID,
                            Rating = rating,
                            CustomerName = HttpUtility.HtmlEncode(Name.Text),
                            CustomerEmail = HttpUtility.HtmlEncode(Email.Text),
                            Comments = HttpUtility.HtmlEncode(UserComment.Content)
                        };
                        db.Reviews.AddObject(newReview);
                        db.SaveChanges();
                    }
                    catch (Exception exp)
                    {
                        throw new Exception("ERROR: Unable to Update Cart Item - " + exp.Message.ToString(), exp);
                    }
                }
            Response.Redirect("ProductDetails.aspx?ProductID=" + productID);
            }
        Response.Redirect("ProductList.aspx");
        }

        protected void LikeRating_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
        {

        }
    }
}