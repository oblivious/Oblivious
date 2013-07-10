using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;

namespace StructureMapTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper.Bootstrap();

            IMyClass myClass = ObjectFactory.GetInstance<IMyClass>();

            myClass.DoSomething();
        }
    }
}
