using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fabianse70536
{
    class MyIFormattableImplementation : IFormattable
    {
        private int myInternalField = 0;
        #region IFormattable Members

        public string ToString(string format, IFormatProvider formatProvider)
        {
            //TODO: implement some custom format logic
            throw new NotImplementedException(); 
        }

        #endregion
    }
}
