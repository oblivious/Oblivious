using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace InterfacesExercise
{
    class Car: IComparable
    {
        private string make;
        private int year;

        public string Make { get; private set; }
        public int Year { get; private set; }

        public Car(string make, int year)
        {
            this.make = make;
            this.year = year;
        }

        public int IComparable.CompareTo(object obj)
        {
            Car car = obj as Car;
            if (car == null)
                throw new ArgumentException("object is not of type Car");
            return String.Compare(this.Make, car.Make);
        }

        private class SortMakeDescending: IComparer
        {
            public int IComparer.Compare(object a, object b)
            {
                Car carA = a as Car;
                Car carB = b as Car;
                if (carA == null || carB == null)
                {
                    throw new ArgumentException("object is not of type Car");
                }
                return String.Compare(carB.Make, carA.Make);
            }
        }

        private class SortYearAscending : IComparer
        {
            public int IComparer.Compare(object a, object b)
            {
                Car carA = a as Car;
                Car carB = b as Car;
                if (carA == null || carB == null)
                {
                    throw new ArgumentException("object is not of type Car");
                }
                if (carA.Year > carB.Year)
                    return 1;
                if (carA.Year < carB.Year)
                    return -1;
                return 0;
            }
        }

        private class SortYearDescending : IComparer
        {
            public int IComparer.Compare(object a, object b)
            {
                Car carA = a as Car;
                Car carB = b as Car;
                if (carA == null || carB == null)
                {
                    throw new ArgumentException("object is not of type Car");
                }
                if (carA.Year < carB.Year)
                    return 1;
                if (carA.Year > carB.Year)
                    return -1;
                return 0;
            }
        }

        public IComparer GetMakeDescendingSorter()
        {
            return (IComparer)new SortMakeDescending();
        }

        public IComparer GetYearAscendingSorter()
        {
            return (IComparer)new SortYearAscending();
        }

        public IComparer GetYearDescendingSorter()
        {
            return (IComparer)new SortYearDescending();
        }
    }
}
