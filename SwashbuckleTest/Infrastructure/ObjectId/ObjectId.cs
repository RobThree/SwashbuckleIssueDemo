using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace SwashbuckleTest.Infrastructure.ObjectId
{
    public struct ObjectId : IComparable<ObjectId>, IEquatable<ObjectId>, IConvertible
    {
        private readonly long _value;
        private static readonly ObjectId _nullid = new(0);
        private static readonly CultureInfo _cultureinfo = CultureInfo.InvariantCulture;

        public bool IsNew => _value <= 0;

        private ObjectId(long value) => _value = value;

        public static ObjectId Empty => _nullid;

        public static ObjectId Parse(string? objectId)
            => TryParse(objectId, out var result) ? result : throw new FormatException($"'{objectId}' is not a valid ObjectId.");

        public static bool TryParse(string? objectId, [NotNullWhen(true)] out ObjectId result)
        {
            if (string.IsNullOrEmpty(objectId))
            {
                result = _nullid;
                return true;
            }

            if (long.TryParse(objectId, out var longresult))
            {
                result = new(longresult);
                return true;
            }

            result = _nullid;
            return false;
        }

        public override string ToString() => _value.ToString(_cultureinfo);
        public bool Equals(ObjectId other) => _value == other._value;
        public override bool Equals(object? obj) => (obj != null && obj is ObjectId id) && Equals(id);
        public int CompareTo(ObjectId other) => _value.CompareTo(other._value);
        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator <(ObjectId lhs, ObjectId rhs) => lhs._value.CompareTo(rhs._value) < 0;
        public static bool operator >(ObjectId lhs, ObjectId rhs) => lhs._value.CompareTo(rhs._value) > 0;
        public static bool operator <=(ObjectId lhs, ObjectId rhs) => lhs._value.CompareTo(rhs._value) <= 0;
        public static bool operator >=(ObjectId lhs, ObjectId rhs) => lhs._value.CompareTo(rhs._value) >= 0;
        public static bool operator ==(ObjectId lhs, ObjectId rhs) => lhs._value.Equals(rhs._value);
        public static bool operator !=(ObjectId lhs, ObjectId rhs) => !(lhs == rhs);

        public static implicit operator long(ObjectId objectId) => objectId._value;
        public static implicit operator long?(ObjectId objectId) => objectId.IsNew ? null : objectId._value;
        public static implicit operator string(ObjectId objectId) => objectId._value.ToString(_cultureinfo);

        public static implicit operator ObjectId(long objectId) => new(objectId);
        public static implicit operator ObjectId(long? objectId) => objectId == null ? _nullid : new(objectId.Value);
        public static implicit operator ObjectId(string objectId) => string.IsNullOrEmpty(objectId) ? _nullid : new(long.Parse(objectId, _cultureinfo));

        #region IConvertible
        public TypeCode GetTypeCode() => TypeCode.Object;
        public bool ToBoolean(IFormatProvider? provider) => throw new InvalidCastException();
        public byte ToByte(IFormatProvider? provider) => throw new InvalidCastException();
        public char ToChar(IFormatProvider? provider) => throw new InvalidCastException();
        public DateTime ToDateTime(IFormatProvider? provider) => throw new InvalidCastException();
        public decimal ToDecimal(IFormatProvider? provider) => _value;
        public double ToDouble(IFormatProvider? provider) => throw new InvalidCastException();
        public short ToInt16(IFormatProvider? provider) => throw new InvalidCastException();
        public int ToInt32(IFormatProvider? provider) => throw new InvalidCastException();
        public long ToInt64(IFormatProvider? provider) => _value;
        public sbyte ToSByte(IFormatProvider? provider) => throw new InvalidCastException();
        public float ToSingle(IFormatProvider? provider) => throw new InvalidCastException();
        public string ToString(IFormatProvider? provider) => throw new NotImplementedException();
        public object ToType(Type conversionType, IFormatProvider? provider)
            => Type.GetTypeCode(conversionType) switch
            {
                TypeCode.String => ((IConvertible)this).ToString(provider),
                TypeCode.Object when (conversionType == typeof(object) || conversionType == typeof(ObjectId)) => this,
                _ => throw new InvalidCastException(),
            };
        public ushort ToUInt16(IFormatProvider? provider) => throw new InvalidCastException();
        public uint ToUInt32(IFormatProvider? provider) => throw new InvalidCastException();
        public ulong ToUInt64(IFormatProvider? provider) => (ulong)_value;
        #endregion
    }
}
