using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;

namespace StructureMapDimeCast
{
    public class Consumer
    {
        public Consumer(ISoda soda)
        {
            Soda = soda;
        }

        public ISoda Soda { get; set; }
    }
}
