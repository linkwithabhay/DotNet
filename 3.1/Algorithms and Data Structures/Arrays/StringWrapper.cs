using System;
using System.Collections.Generic;
using System.Text;

namespace Arrays
{
    // implements all interfaces that string does through the field content
    public sealed class StringWrapper : IEnumerable<char>, ICloneable, IComparable, IComparable<string>, IConvertible, IEquatable<string>
    {
        private readonly string content;

        public StringWrapper(string content)
        {
            this.content = content;
        }

        // implicit conversions
        public static implicit operator string(StringWrapper d) => d.content;
        public static implicit operator StringWrapper(string b) => new StringWrapper(b);

        public static bool operator <(StringWrapper lhs, StringWrapper rhs)
        {
            return lhs.content.CompareTo(rhs.content) < 0;
        }

        public static bool operator >(StringWrapper lhs, StringWrapper rhs)
        {
            return lhs.content.CompareTo(rhs.content) > 0;
        }

        // string supports it, why shouldnt we?
        public static StringWrapper operator +(StringWrapper lhs, StringWrapper rhs)
        {
            var sb = new StringBuilder();
            sb.Append(lhs.content);
            sb.Append(rhs.content);
            return sb.ToString();
        }

        // at request of @Alexey Khoroshikh
        public static StringWrapper operator *(StringWrapper lhs, int rhs)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < rhs; i++)
            {
                sb.Append(lhs.content);
            }
            return sb.ToString();
        }

        // other nice thing to have
        public static string[] operator /(StringWrapper lhs, char rhs)
        {
            return lhs.content.Split(rhs);
        }

        public override bool Equals(object obj)
        {
            return (obj is StringWrapper wrapper && content == wrapper.content)
                || (obj is string str && content == str);
        }

        #region auto-generated code through visual studio

        public override int GetHashCode()
        {
            return -1896430574 + EqualityComparer<string>.Default.GetHashCode(content);
        }

        public override string ToString()
        {
            return this.content;
        }

        public object Clone()
        {
            return content.Clone();
        }

        public int CompareTo(string other)
        {
            return content.CompareTo(other);
        }

        public bool Equals(string other)
        {
            return content.Equals(other);
        }

        public IEnumerator<char> GetEnumerator()
        {
            return ((IEnumerable<char>)content).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)content).GetEnumerator();
        }

        public int CompareTo(object obj)
        {
            return content.CompareTo(obj);
        }

        public TypeCode GetTypeCode()
        {
            return content.GetTypeCode();
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return ((IConvertible)content).ToBoolean(provider);
        }

        public byte ToByte(IFormatProvider provider)
        {
            return ((IConvertible)content).ToByte(provider);
        }

        public char ToChar(IFormatProvider provider)
        {
            return ((IConvertible)content).ToChar(provider);
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            return ((IConvertible)content).ToDateTime(provider);
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return ((IConvertible)content).ToDecimal(provider);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return ((IConvertible)content).ToDouble(provider);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return ((IConvertible)content).ToInt16(provider);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return ((IConvertible)content).ToInt32(provider);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return ((IConvertible)content).ToInt64(provider);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return ((IConvertible)content).ToSByte(provider);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return ((IConvertible)content).ToSingle(provider);
        }

        public string ToString(IFormatProvider provider)
        {
            return content.ToString(provider);
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return ((IConvertible)content).ToType(conversionType, provider);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return ((IConvertible)content).ToUInt16(provider);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return ((IConvertible)content).ToUInt32(provider);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return ((IConvertible)content).ToUInt64(provider);
        }

        #endregion auto-generated code through visual studio
    }
}
