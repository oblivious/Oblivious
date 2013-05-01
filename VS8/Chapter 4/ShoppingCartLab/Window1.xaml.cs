using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GenericList
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        List<ShoppingCartItem> shoppingCart;

        public Window1()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                shoppingCart.Add(new ShoppingCartItem(this.nameTextBox.Text, Double.Parse(this.priceTextBox.Text)));
                shoppingCartList.Items.Refresh();
                nameTextBox.Clear();
                priceTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter valid data: " + ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            shoppingCart = new List<ShoppingCartItem>();
            shoppingCartList.ItemsSource = shoppingCart;
            // TODO: Create a data binding from the ListBox to the List
        }

        private void sortNameButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Sort the shopping cart by name
            shoppingCart.Sort(ShoppingCartItem.SortByName);
            shoppingCartList.Items.Refresh();
        }

        private void sortPriceButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Sort the shopping cart by price
            shoppingCart.Sort(ShoppingCartItem.SortByPrice);
            shoppingCartList.Items.Refresh();
        }
    }

    public class ShoppingCartItem : IComparable
    {
        public string ItemName { get; set; }
        public double Price { get; set; }

        public ShoppingCartItem(string itemName, double price)
        {
            this.ItemName = itemName;
            this.Price = price;
        }

        public override string ToString()
        {
            return this.ItemName + ": " + this.Price.ToString("C");
        }

        #region IComparable Members
        int IComparable.CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
        #endregion

        public static int SortByName(ShoppingCartItem item1, ShoppingCartItem item2)
        {
            return item1.ItemName.CompareTo(item2.ItemName);
        }

        public static int SortByPrice(ShoppingCartItem item1, ShoppingCartItem item2)
        {
            return item1.Price.CompareTo(item2.Price);
        }
    }
}
