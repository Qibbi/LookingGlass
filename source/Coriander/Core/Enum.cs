using System;

namespace Coriander.Core
{
    public readonly struct Enum<T> : IEquatable<Enum<T>>, IEquatable<T> where T : unmanaged, Enum
    {
        private readonly int _value;

        public T Value => (T)Enum.ToObject(typeof(T), _value);

        public unsafe Enum(T source)
        {
            _value = *(int*)&source;
        }

        public Enum(int source)
        {
            _value = source;
        }

        public Enum(string source)
        {
            _value = (Enum.Parse(typeof(T), source) as IConvertible).ToInt32(null);
        }

        public static bool operator ==(Enum<T> x, Enum<T> y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(Enum<T> x, Enum<T> y)
        {
            return !x.Equals(y);
        }

        public static bool operator ==(Enum<T> x, T y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(Enum<T> x, T y)
        {
            return !x.Equals(y);
        }

        public static bool operator ==(T x, Enum<T> y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(T x, Enum<T> y)
        {
            return !x.Equals(y);
        }

        public static implicit operator Enum<T>(T x)
        {
            return new Enum<T>(x);
        }

        public static implicit operator T(Enum<T> x)
        {
            return x.Value;
        }

        public static implicit operator Enum<T>(int x)
        {
            return new Enum<T>(x);
        }

        public static implicit operator int(Enum<T> x)
        {
            return x._value;
        }

        public static implicit operator Enum<T>(string x)
        {
            return new Enum<T>(x);
        }

        public static implicit operator string(Enum<T> x)
        {
            return x.Value.ToString();
        }

        public bool Equals(Enum<T> other)
        {
            return _value == other._value;
        }

        public unsafe bool Equals(T other)
        {
            return _value == *(int*)&other;
        }

        public override bool Equals(object obj)
        {
            return obj is Enum<T> objT && Equals(objT);
        }

        public override int GetHashCode()
        {
            return _value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
