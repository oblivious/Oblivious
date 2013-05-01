using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace SerializeShoppingCartItem
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    [Serializable]
    class ShoppingCartItem : ISerializable, IDeserializationCallback
    {
        public int productId;
        public decimal price;
        public int quantity;
        [NonSerialized]
        public decimal total;

        // The standard, non-serialization constructor
        public ShoppingCartItem(int productID, decimal price, int quantity)
        {
            this.productId = productID;
            this.price = price;
            this.quantity = quantity;
        }

        // The following constructor is for deserialization
        public ShoppingCartItem(SerializationInfo info, StreamingContext context)
        {
            this.productId = info.GetInt32("Product ID");
            this.price = info.GetDecimal("Price");
            this.quantity = info.GetInt32("Quantity");
        }

        // The following method is called during serialization
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Product ID", this.productId);
            info.AddValue("Price", this.price);
            info.AddValue("Quantity", this.quantity);
        }

        public override string ToString()
        {
            return productId + ": " + price + " x " + quantity + " = " + total;
        }

        #region IDeserializationCallback Members

        public void OnDeserialization(object sender)
        {
            this.total = this.quantity * this.price;
        }

        #endregion
    }
}
