using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace IComparableIComparer
{
    public class Car : IComparable
    {
        private string make;
        private int year;

        public Car(string make, int year)
        {
            this.make = make;
            this.year = year;
        }

        public int IComparable.CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            Car otherCar = obj as Car;
            if (otherCar == null)
            {
                throw new ArgumentException("Object is not of type Car");
            }
            else
            {
                return this.make.CompareTo(otherCar.make);
            }
        }

        #region Nested classes
        private class sortByMakeAscending : IComparer
        {
            public int IComparer.Compare(object a, object b)
            {
                if (a == null && b == null)
                    return 0;
                if (b == null)
                    return 1;
                if (a == null)
                    return -1;
                Car c = a as Car;
                Car d = b as Car;
                if (c == null || d == null)
                {
                    throw new ArgumentException("Object is not of type Car");
                }
                else
                {
                    return c.make.CompareTo(d.make);
                }
            }
        }

        private class sortByMakeDescending : IComparer
        {
            public int IComparer.Compare(object a, object b)
            {
                if (a == null && b == null)
                    return 0;
                if (b == null)
                    return 1;
                if (a == null)
                    return -1;
                Car c = a as Car;
                Car d = b as Car;
                if (c == null || d == null)
                {
                    throw new ArgumentException("Object is not of type Car");
                }
                else
                {
                    return d.make.CompareTo(c.make);
                }
            }
        }

        private class sortByYearAscending : IComparer
        {
            public int IComparer.Compare(object a, object b)
            {
                if (a == null && b == null)
                    return 0;
                if (b == null)
                    return 1;
                if (a == null)
                    return -1;
                Car c = a as Car;
                Car d = b as Car;
                if (c == null || d == null)
                {
                    throw new ArgumentException("Object is not of type Car");
                }
                else
                {
                    return c.year.CompareTo(d.year);
                }
            }
        }

        private class sortByYearDescending : IComparer
        {
            public int IComparer.Compare(object a, object b)
            {
                if (a == null && b == null)
                    return 0;
                if (b == null)
                    return 1;
                if (a == null)
                    return -1;
                Car c = a as Car;
                Car d = b as Car;
                if (c == null || d == null)
                {
                    throw new ArgumentException("Object is not of type Car");
                }
                else
                {
                    return d.year.CompareTo(c.year);
                }
            }
        }
        #endregion

        public IComparer sortByMakeAscendingHelper()
        {
            return (IComparer) new sortByMakeAscending();
        }

        public IComparer sortByMakeDescendingHelper()
        {
            return (IComparer) new sortByMakeDescending();
        }

        public IComparer sortByYearAscendingHelper()
        {
            return (IComparer) new sortByYearAscending();
        }

        public IComparer sortByYearDescendingHelper()
        {
            return (IComparer) new sortByYearDescending();
        }
    }
}
