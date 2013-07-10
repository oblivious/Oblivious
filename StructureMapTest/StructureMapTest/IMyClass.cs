using System;

namespace StructureMapTest
{
    public interface IMyClass
    {
        void DoSomething();
    }

    public class MyClass : IMyClass
    {
        private readonly ushort _someValue;
        private readonly int _someOtherValue;

        public MyClass(ushort someValue, int someOtherValue)
        {
            _someValue = someValue;
            _someOtherValue = someOtherValue;
        }

        public void DoSomething()
        {
            Console.WriteLine("Did something: {0}, {1}", _someValue, _someOtherValue);
        }
    }
}
