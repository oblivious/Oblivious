using System;
using System.Collections.Generic;
using System.Text;

namespace ChainOfResponsibilityPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var sb = new StringBuilder();
            sb.AppendLine("This code is intended to overcome the issue of complex business logic");
            sb.AppendLine("when handling similar but related objects. For example: \n");
            sb.AppendLine("   if (order.IsInternational)");
            sb.AppendLine("   {");
            sb.AppendLine("      processInternationalOrder(order)");
            sb.AppendLine("   }");
            sb.AppendLine("   else if (order.LineItems.Count > 10");
            sb.AppendLine("   {");
            sb.AppendLine("      processLargeDomesticOrder(order)");
            sb.AppendLine("   }");
            sb.AppendLine("   else");
            sb.AppendLine("   {");
            sb.AppendLine("      processRegularDomesticOrder(order)");
            sb.AppendLine("   }\n");
            sb.AppendLine("Use a chain of responsibility instead...");
            Console.WriteLine(sb.ToString());


        }
    }

    public class Order
    {
        private List<string> _lineItems;

        public Order()
        {
            _lineItems = new List<string>();
        }

        public List<string> LineItems { get { return _lineItems; } }
    }

    public interface IOrderHandler
    {
        void ProcessOrder(Order order);
        bool CanProcess(Order order);
    }

    public class OrderStatusMessage
    {
        public string Message { get; set; }
    }

    public class OrderProcessingModule
    {
        private readonly IOrderHandler[] _handlers;

        public OrderProcessingModule()
        {
            _handlers = new IOrderHandler[]
                {
                    new InternationalOrderHandler(),
                    new SmallDomesticOrderHandler(),
                    new LargeDomesticOrderHandler(),
                };
        }

        public void Process(OrderStatusMessage orderStatusMessage, Order order)
        {
            // Apply the changes to the Order from the OrderStatusMessage
            UpdateTheOrder(order);

            // Find the first IOrderHander that "knows" how to process this Order
            var handler = Array.Find(_handlers, h => h.CanProcess(order));
        }

        private static void UpdateTheOrder(Order order)
        {
        }
    }
}
