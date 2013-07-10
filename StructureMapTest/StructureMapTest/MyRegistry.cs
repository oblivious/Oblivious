using System;
using StructureMap.Configuration.DSL;
using System.Configuration;

namespace StructureMapTest
{
    internal class MyRegistry : Registry
    {
        public MyRegistry()
        {
            For<IMyClass>().Use<MyClass>()
                .Ctor<ushort>("someValue").Is(UInt16.Parse(ConfigurationManager.AppSettings["someValue"]))
                .Ctor<int>("someOtherValue").Is(Int32.Parse(ConfigurationManager.AppSettings["someOtherValue"]));
        }
    }
}
