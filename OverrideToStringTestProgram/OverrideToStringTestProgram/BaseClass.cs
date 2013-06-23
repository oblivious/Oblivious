using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OverrideToStringTestProgram
{
    class BaseClass
    {
        public static int Length;

        private static LengthProvider lengthProvider;

        static BaseClass()
        {
            lengthProvider = new LengthProvider(0);
        }



        public override string ToString()
        {
            return "The Base Class";
        }

        public string ToOtherFormat()
        {
            return "(" + ToString() + ")";
        }

        public static int ValidLength
        {
            get { return 1; }
        }

        public static int ValidLengthIncludingHeaders
        {
            get { return ValidLength + 2; }
        }
    }
}
