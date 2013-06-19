using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTest;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            int listID = 1;

            PriceListManager priceList = new PriceListManager(listID);

            Console.WriteLine("The following Prices with PriceListID = " + listID + "  exist in the database:");

            foreach (Price price in priceList.Prices)
            {
                Console.WriteLine("    ProductID: " + price.ProductID + ", Price: " + (price.Price1.ToString() + ",").PadRight(8) + " Currency " + price.Currency);
            }

            listID = priceList.CreatePriceList();
            Console.WriteLine("New list created with ID: " + listID);
        }
    }
}
