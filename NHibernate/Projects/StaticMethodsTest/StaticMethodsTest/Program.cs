using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StaticMethodsTest
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    public class Product
    {
        public int Id { get; set; }
    }

    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Id(x => x.Id);
        }
    }

    public class ClassMap<T>
    {
        private Func<T, int> idMethod = null;

        public void Id(Func<T, int> getId)
        {
            idMethod = getId;
        }

        public int GetId(T instance)
        {
            return idMethod(instance);
        }
    }
}
