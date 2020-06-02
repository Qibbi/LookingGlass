using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Coriander.Core
{
    public class BitFlags<T> : IEnumerable, IEquatable<BitFlags<T>> where T : unmanaged, Enum
    {
        private class Enumerator : IEnumerator
        {
            private int _index;
            private readonly BitFlags<T> _flags;

            public object Current => (T)Enum.ToObject(typeof(T), _index);

            public Enumerator(BitFlags<T> flags)
            {
                _index = -1;
                _flags = flags;
            }

            public bool MoveNext()
            {
                while (++_index < _valueCount && !_flags.HasFlag(_index))
                {
                }
                return _index < _valueCount;
            }

            public void Reset()
            {
                _index = 0;
            }
        }

        private const int _bitsInSpan = 32;
        private const int _bitShift = 5;
        private const int _bitMask = 0x0000001F;

        private static readonly int _valueCount;
        private static readonly int _numSpans;
        private static readonly bool _hasInvalidValue;
        private static readonly bool _hasAllValue;
        private static readonly int _allValue;
        private static readonly uint _lastSpanMask;

        private readonly uint[] _value = new uint[_numSpans];

        static BitFlags()
        {
            Type type = typeof(T);
            _hasInvalidValue = Enum.IsDefined(type, -1);
            _hasAllValue = Enum.IsDefined(type, "ALL");
            if (_hasAllValue)
            {
                _allValue = new Enum<T>((T)Enum.Parse(type, "ALL"));
            }
            _valueCount = Enum.GetValues(type).Length - (_hasInvalidValue ? 1 : 0) - (_hasAllValue ? 1 : 0);
            _numSpans = (int)(((uint)_valueCount + _bitsInSpan - 1) >> _bitShift);
            int numLastSpan = _valueCount % _numSpans;
            _lastSpanMask = 0u;
            for (int idx = 0; idx < numLastSpan; ++idx)
            {
                _lastSpanMask |= 1u << idx;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(BitFlags<T> x, BitFlags<T> y)
        {
            return x.Equals(y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(BitFlags<T> x, BitFlags<T> y)
        {
            return !x.Equals(y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint operator &(BitFlags<T> x, uint y)
        {
            return x._value[0] & y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint operator &(uint x, BitFlags<T> y)
        {
            return x & y._value[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator uint(BitFlags<T> x)
        {
            return x._value[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator uint[](BitFlags<T> x)
        {
            return x._value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Add(T flag)
        {
            Add(*(int*)&flag);
        }

        public void Add(int flag)
        {
            if (_hasInvalidValue && flag == -1)
            {
                return;
            }
            if (_hasAllValue && flag == _allValue)
            {
                for (int idx = 0; idx < _numSpans; ++idx)
                {
                    _value[idx] = 0xFFFFFFFFu;
                }
                return;
            }
            if (flag < _valueCount)
            {
                _value[flag >> _bitShift] |= 1u << (flag & _bitMask);
            }
        }

        public void Add(Enum<T> flag)
        {
            int flagI = flag;
            if (_hasInvalidValue && flagI == -1)
            {
                return;
            }
            if (_hasAllValue && flagI == _allValue)
            {
                for (int idx = 0; idx < _numSpans; ++idx)
                {
                    _value[idx] = 0xFFFFFFFFu;
                }
                return;
            }
            if (flagI < _valueCount)
            {
                _value[flagI >> _bitShift] |= 1u << (flagI & _bitMask);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Remove(T flag)
        {
            Remove(*(int*)&flag);
        }

        public void Remove(int flag)
        {
            if (_hasInvalidValue && flag == -1)
            {
                return;
            }
            if (_hasAllValue && flag == _allValue)
            {
                for (int idx = 0; idx < _numSpans; ++idx)
                {
                    _value[idx] = 0u;
                }
                return;
            }
            if (flag < _valueCount)
            {
                _value[flag >> _bitShift] ^= 1u << (flag & _bitMask);
            }
        }

        public void Remove(Enum<T> flag)
        {
            int flagI = flag;
            if (_hasInvalidValue && flagI == -1)
            {
                return;
            }
            if (_hasAllValue && flagI == _allValue)
            {
                for (int idx = 0; idx < _numSpans; ++idx)
                {
                    _value[idx] = 0u;
                }
                return;
            }
            if (flagI < _valueCount)
            {
                _value[flagI >> _bitShift] ^= 1u << (flagI & _bitMask);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe bool HasFlag(T flag)
        {
            return HasFlag(*(int*)&flag);
        }

        public bool HasFlag(int flag)
        {
            if (_hasInvalidValue && flag == -1)
            {
                for (int idx = 0; idx < _numSpans; ++idx)
                {
                    if (_value[idx] != 0u)
                    {
                        return false;
                    }
                }
                return true;
            }
            if (_hasAllValue && flag == _allValue)
            {
                int fullSpans = flag / _bitsInSpan;
                for (int idx = 0; idx < fullSpans; ++idx)
                {
                    if (_value[idx] != 0xFFFFFFFFu)
                    {
                        return false;
                    }
                }
                if (fullSpans == _numSpans)
                {
                    return true;
                }
                return (_value[fullSpans] & _lastSpanMask) == _lastSpanMask ? true : false;
            }
            if (flag < _valueCount)
            {
                return (_value[flag >> _bitShift] & (1u << (flag & _bitMask))) != 0u;
            }
            return false;
        }

        public bool HasFlag(Enum<T> flag)
        {
            int flagI = flag;
            if (_hasInvalidValue && flagI == -1)
            {
                for (int idx = 0; idx < _numSpans; ++idx)
                {
                    if (_value[idx] != 0u)
                    {
                        return false;
                    }
                }
                return true;
            }
            if (_hasAllValue && flagI == _allValue)
            {
                int fullSpans = flagI / _bitsInSpan;
                for (int idx = 0; idx < fullSpans; ++idx)
                {
                    if (_value[idx] != 0xFFFFFFFFu)
                    {
                        return false;
                    }
                }
                if (fullSpans == _numSpans)
                {
                    return true;
                }
                return (_value[fullSpans] & _lastSpanMask) == _lastSpanMask ? true : false;
            }
            if (flagI < _valueCount)
            {
                return (_value[flagI >> _bitShift] & (1u << (flagI & _bitMask))) != 0u;
            }
            return false;
        }

        public IEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        public bool Equals(BitFlags<T> other)
        {
            for (int idx = 0; idx < _numSpans; ++idx)
            {
                if (_value[idx] != other._value[idx])
                {
                    return false;
                }
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            return obj is BitFlags<T> objT && Equals(objT);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (int)_value[0];
                for (int idx = 1; idx < _numSpans; ++idx)
                {
                    result = (result * 397) ^ (int)_value[idx];
                }
                return result;
            }
        }

        public override string ToString()
        {
            List<string> values = new List<string>();
            for (int idx = 0; idx < _valueCount; ++idx)
            {
                if (HasFlag(idx))
                {
                    values.Add(new Enum<T>(idx).ToString());
                }
            }
            return string.Join(" ", values);
        }
    }
}
