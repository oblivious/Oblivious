using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fabianse70536
{
    class MyIComparableImplementation: IComparable
    {
        public int myFieldToCompare = 0;
        #region IComparable Members

        public int CompareTo(object obj)
        {
            MyIComparableImplementation objToCompareTo = obj as MyIComparableImplementation;
            if (objToCompareTo.myFieldToCompare == this.myFieldToCompare)
            {
                return 0; 
            }
            else if (objToCompareTo.myFieldToCompare > this.myFieldToCompare)
            {
                return 1;
            }
            else 
            {
                return -1;
            }
        }

        #endregion
    }
}
