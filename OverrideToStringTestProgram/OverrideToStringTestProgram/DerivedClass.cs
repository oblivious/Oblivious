using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OverrideToStringTestProgram
{
    class DerivedClass : BaseClass
    {
        static DerivedClass()
        {
            Length = 1;
        }

        public override string ToString()
        {
            return "Derived Class of " + base.ToString();
        }

        public new static int ValidLength
        {
            get { return 2; }
        }

        public new static int ValidLengthIncludingHeaders
        {
            get { return ValidLength + 2; }
        }
    }
}
