using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechTest
{
    public class DataAccess
    {
        internal static List<Price> GetPriceList(int distributorID)
        {
            TechTestDataContext db = new TechTestDataContext();

            var result = (from price in db.Prices
                          join priceList in db.PriceLists on price.ID equals priceList.PriceID
                          join distPriceList in db.DistributorPriceLists on priceList.ID equals distPriceList.PriceListID
                          join distributor in db.Distributors on distPriceList.ID equals distributor.DistributorPriceListID
                          where distributor.ID == distributorID
                          select price).ToList();

            return result;
        }

        internal static int CreatePriceList()
        {
            TechTestDataContext db = new TechTestDataContext();

            int maxID = (from distPriceList in db.DistributorPriceLists
                         select distPriceList.PriceListID).Max();
            maxID++;
            DistributorPriceList distributorPriceList = new DistributorPriceList() { PriceListID = maxID };

            db.DistributorPriceLists.InsertOnSubmit(distributorPriceList);
            db.SubmitChanges();
            return maxID;
        }
    }
}
