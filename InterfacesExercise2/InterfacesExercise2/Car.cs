using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace InterfacesExercise2
{
    class Car : IComparable
    {
        private string make;
        private int year;

        public string Make {get; private set;}
        public int Year {get; private set;}

        public Car(string make, int year)
        {
            this.Make = make;
            this.Year = year;
        }

        public int IComparable.CompareTo(object obj)
        {
            Car car = obj as Car;
            if (car == null)
                throw new ArgumentException("object is not of type Car");
            return String.Compare(this.Make, car.Make);
        }

        private class SortMakeDescending : IComparer
        {
            public int IComparer.Compare(object objA, object objB)
            {
                Car carA = objA as Car;
                Car carB = objB as Car;
                if (carA == null || carB == null)
                {
                    throw new ArgumentException("object is not of type Car");
                }
                return String.Compare(carB.Make, carA.Make);
            }
        }

        private class SortYearAscending : IComparer
        {
            public int IComparer.Compare(object objA, object objB)
            {
                Car carA = objA as Car;
                Car carB = objB as Car;
                if (carA == null || carB == null)
                {
                    throw new ArgumentException("object is not of type Car");
                }
                if (carA.Year > carB.Year)
                    return 1;
                else if (carA.Year < carB.Year)
                    return -1;
                else
                    return 0;
            }
        }

        private class SortYearDescending : IComparer
        {
            public int IComparer.Compare(object objA, object objB)
            {
                Car carA = objA as Car;
                Car carB = objB as Car;
                if (carA == null || carB == null)
                {
                    throw new ArgumentException("object is not of type Car");
                }
                if (carA.Year < carB.Year)
                    return 1;
                else if (carA.Year > carB.Year)
                    return -1;
                else
                    return 0;
            }
        }

        public static IComparer MakeDescendingSorter()
        {
            return (IComparer)new SortMakeDescending();
        }

        public static IComparer YearAscendingSorter()
        {
            return (IComparer)new SortYearAscending();
        }

        public static IComparer YearDescendingSorter()
        {
            return (IComparer)new SortYearDescending();
        }
    }
}
