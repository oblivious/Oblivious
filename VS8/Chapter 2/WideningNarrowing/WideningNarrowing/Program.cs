using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WideningNarrowing
{
    class Program
    {
        static void Main(string[] args)
        {
            TypeA a;
            int i;
            a = -42;
            i = (int)a;
            Console.WriteLine("a = {0}, i = {1}", a.ToString(), i.ToString());

            bool b;
            b = Convert.ToBoolean(a);
            Console.WriteLine("a = {0}, b = {1}", a.ToString(), b.ToString());
            a = 0;
            b = Convert.ToBoolean(a);
            Console.WriteLine("a = {0}, b = {1}", a.ToString(), b.ToString());
        }
    }

    struct TypeA : IConvertible
    {
        public int Value;

        // Allows implicit conversion from an integer.
        public static implicit operator TypeA(int arg)
        {
            TypeA res = new TypeA();
            res.Value = arg;
            return res;
        }

        // Allows explicit conversion to an integer.
        public static explicit operator int(TypeA arg)
        {
            return arg.Value;
        }

        // Provides string conversion (avoids boxing).
        public override string ToString()
        {
            return this.Value.ToString();
        }

        #region IConvertible Members

        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(this.Value);
        }

        public byte ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(this.Value);
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(this.Value);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(this.Value);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(this.Value);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return this.Value;
        }

        public long ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(this.Value);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(this.Value);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(this.Value);
        }

        public string ToString(IFormatProvider provider)
        {
            return this.Value.ToString(provider);
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(this.Value);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(this.Value);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(this.Value);
        }

        #endregion
    }
}
