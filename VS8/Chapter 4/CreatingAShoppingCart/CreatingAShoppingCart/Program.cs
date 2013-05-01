using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CreatingAShoppingCart
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList shoppingCart = new ArrayList();
            shoppingCart.Add(new ShoppingCartItem("Car", 5000));
            shoppingCart.Add(new ShoppingCartItem("Book", 30));
            shoppingCart.Add(new ShoppingCartItem("Phone", 80));
            shoppingCart.Add(new ShoppingCartItem("Computer", 1000));

            printCart(shoppingCart);

            Console.WriteLine("Sorting...");
            shoppingCart.Sort();
            shoppingCart.Reverse();
            printCart(shoppingCart);
        }

        private static void printCart(ArrayList cart)
        {
            foreach (ShoppingCartItem sci in cart)
            {
                Console.WriteLine(sci.itemName + ": $" + sci.price.ToString());
            }
        }
    }

    public class ShoppingCartItem : IComparable
    {
        public string itemName;
        public double price;

        public ShoppingCartItem(string itemName, double price)
        {
            this.itemName = itemName;
            this.price = price;
        }

        #region IComparable Members
        int IComparable.CompareTo(object obj)
        {
            if (!(obj is ShoppingCartItem))
                throw new ArgumentException("object not a ShoppingCartItem");
            return this.price.CompareTo(((ShoppingCartItem)obj).price);
        }
        #endregion
    }
}
