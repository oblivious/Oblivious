using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHibernateDemo.Code;
using NHibernate.Linq;

namespace NHibernateDemo
{
    public partial class _Default : System.Web.UI.Page
    {
        private static readonly Customers Customers = new Customers();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ReloadCustomersList();
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(FirstNameTextBox.Text) || String.IsNullOrWhiteSpace(LastNameTextBox.Text))
                return;

            using (var session = Global.SessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var customer = new Customer { FirstName = FirstNameTextBox.Text, LastName = LastNameTextBox.Text };
                session.Save(customer);
                tx.Commit();
            }

            ReloadCustomersList();
        }

        private void ReloadCustomersList()
        {
            CustomersList.Items.Clear();

            using (var session = Global.SessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var customers = session.Query<Customer>().OrderBy(x => x.LastName).ToList();

                foreach (var customer in customers)
                {
                    CustomersList.Items.Add(String.Format("{0} {1}", customer.FirstName, customer.LastName));
                }
                tx.Commit();
            }
        }
    }
}
