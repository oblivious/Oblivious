using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OverrideToStringTestProgram
{
    internal class LengthProvider
    {
        public int Length { get; private set; }

        public LengthProvider(int length)
        {
            Length = length;
        }
    }
}
