using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TailspinSpyworks.Data_Access;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

//--------------------------------------------------------------------------------------------------------------------------------------------------+
// TODO: In a future version of this application we will seperate this logic into a seperate Business Objets Layer
//--------------------------------------------------------------------------------------------------------------------------------------------------+
namespace TailspinSpyworks
{
    public struct ShoppingCartUpdates
    {
        public int ProductId;
        public int PurchaseQuantity;
        public bool RemoveItem;
    }


    public partial class MyShoppingCart
    {
        public const string CartId = "TailSpinSpyWorks_CartID";

        //------------------------------------------------------------------------------------------------------------------------------------------+
        public String GetShoppingCartId()
        {
            if (Session[CartId] == null)
            {
                Session[CartId] = System.Web.HttpContext.Current.Request.IsAuthenticated ? User.Identity.Name : Guid.NewGuid().ToString();
            }
            return Session[CartId].ToString();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------+
        public decimal GetTotal(string cartID)
        {

            using (CommerceEntities db = new CommerceEntities())
            {
                decimal cartTotal = 0;
                try 
                {
                  var myCart = (from c in db.ViewCarts where c.CartID == cartID select c);
                  if (myCart.Count() > 0)
                  {
                      cartTotal = myCart.Sum(od => (decimal)od.Quantity * (decimal)od.UnitCost);
                  }
                }
                catch(Exception exp)
                {
                    throw new Exception("ERROR: Unable to Calculate Order Total - " + exp.Message.ToString(), exp);
                }
            return (cartTotal);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------+
        public void RemoveItem(string cartID, int  productID)
        {
            using (CommerceEntities db = new CommerceEntities())
            {
                try
                {
                    var myItem = (from c in db.ShoppingCarts where c.CartID == cartID && c.ProductID == productID select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        db.DeleteObject(myItem);
                        db.SaveChanges();
                    }

                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Remove Cart Item - " + exp.Message.ToString(), exp);
                }
            }

        }

        //------------------------------------------------------------------------------------------------------------------------------------------+
        public void UpdateItem(string cartID, int productID, int quantity)
        {
            using (CommerceEntities db = new CommerceEntities())
            {
                try
                {
                    var myItem = (from c in db.ShoppingCarts where c.CartID == cartID && c.ProductID == productID select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        myItem.Quantity = quantity;
                        db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Update Cart Item - " + exp.Message.ToString(), exp);
                }
            }
    
        }

        //------------------------------------------------------------------------------------------------------------------------------------------+
        public void UpdateShoppingCartDatabase(String cartId, ShoppingCartUpdates[] CartItemUpdates)
        {
            using (CommerceEntities db = new CommerceEntities())
            {
                try
                {
                   int CartItemCOunt = CartItemUpdates.Count();
                   var myCart = (from c in db.ViewCarts where c.CartID == cartId select c);
                   foreach (var cartItem in myCart)
                   {
                       // Iterate through all rows within shopping cart list
                       for (int i = 0; i < CartItemCOunt; i++)
                       {
                           if (cartItem.ProductID == CartItemUpdates[i].ProductId)
                           {
                               if (CartItemUpdates[i].PurchaseQuantity < 1 || CartItemUpdates[i].RemoveItem == true)
                              {
                                  RemoveItem(cartId, cartItem.ProductID);
                              }
                           else 
                              {
                                  UpdateItem(cartId, cartItem.ProductID, CartItemUpdates[i].PurchaseQuantity);
                              }
                           }
                        }
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Update Cart Database - " + exp.Message.ToString(), exp);
                }            
            }           
        }

        //------------------------------------------------------------------------------------------------------------------------------------------+
        public void AddItem(string cartID, int productID, int quantity)
        {
            using (CommerceEntities db = new CommerceEntities())
            {
                try 
                {
                    var myItem = (from c in db.ShoppingCarts where c.CartID == cartID && c.ProductID == productID select c).FirstOrDefault();
                    if (myItem == null)
                    {
                        ShoppingCart cartadd = new ShoppingCart();
                        cartadd.CartID = cartID;
                        cartadd.Quantity = quantity;
                        cartadd.ProductID = productID;
                        cartadd.DateCreated = DateTime.Now;
                        db.ShoppingCarts.AddObject(cartadd);
                    }
                    else
                    {
                        myItem.Quantity += quantity;
                    }

                    db.SaveChanges();
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Add Item to Cart - " + exp.Message.ToString(), exp);
                }
            }

        }

        //------------------------------------------------------------------------------------------------------------------------------------------+
        public bool SubmitOrder(string UserName)
        {
            using (CommerceEntities db = new CommerceEntities())
            {
                try
                {
                    //------------------------------------------------------------------------+
                    //  Add New Order Record                                                  |
                    //------------------------------------------------------------------------+
                    Order newOrder = new Order();
                    newOrder.CustomerName = UserName;
                    newOrder.OrderDate = DateTime.Now;
                    newOrder.ShipDate = CalculateShipDate();
                    db.Orders.AddObject(newOrder);
                    db.SaveChanges();

                    //------------------------------------------------------------------------+
                    //  Create a new OderDetail Record for each item in the Shopping Cart     |
                    //------------------------------------------------------------------------+
                    String cartId = GetShoppingCartId();
                    var myCart = (from c in db.ViewCarts where c.CartID == cartId select c);
                    foreach (ViewCart item in myCart)
                    {
                        int i = 0;
                        if (i < 1)
                        {
                            OrderDetail od = new OrderDetail();
                            od.OrderID = newOrder.OrderID;
                            od.ProductID = item.ProductID;
                            od.Quantity = item.Quantity;
                            od.UnitCost = item.UnitCost;
                            db.OrderDetails.AddObject(od);
                            i++;
                        }

                        var myItem = (from c in db.ShoppingCarts where c.CartID == item.CartID && c.ProductID == item.ProductID select c).FirstOrDefault();
                        if (myItem != null)
                        {
                            db.DeleteObject(myItem);
                        }
                    }
                    db.SaveChanges();                    
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Submit Order - " + exp.Message.ToString(), exp);
                }
            } 

            return(true);
        }


        //------------------------------------------------------------------------------------------------------------------------------------------+
        DateTime CalculateShipDate()
        {
            DateTime shipDate = DateTime.Now.AddDays(2);
            return (shipDate);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------+
        public void MigrateCart(String oldCartId, String UserName)
        {
            using (CommerceEntities db = new CommerceEntities())
            {
                try
                {
                    var myShoppingCart = from cart in db.ShoppingCarts
                         where cart.CartID == oldCartId
                         select cart;

                    foreach (ShoppingCart item in myShoppingCart)
                    {
                        item.CartID = UserName;                 
                    }
                   db.SaveChanges();
                   Session[CartId] = UserName;
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Migrate Shopping Cart - " + exp.Message.ToString(), exp);
                }
            }           
        }
    }
}