using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TechTest
{
    public partial class PriceListManager : Component
    {
        public List<Price> Prices { get; set; }
        private int _priceListID;

        public PriceListManager(int distributorID)
        {
            InitializeComponent();
            Prices = DataAccess.GetPriceList(distributorID);
        }

        public PriceListManager(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public int CreatePriceList()
        {
            return DataAccess.CreatePriceList();
        }
    }
}
