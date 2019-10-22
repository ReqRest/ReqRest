using System;

namespace ReqRest.Tests.Sdk.Models
{

    /// <summary>
    ///     A DTO class with some actual properties that can be used to test the functionality
    ///     of serializers.
    /// </summary>
    public class SerializationDto : IEquatable<SerializationDto?>
    {

        public string? StringValue { get; set; }

        public int IntValue { get; set; }

        public double DoubleValue { get; set; }

        public int? NullableInt { get; set; }

        public override bool Equals(object obj)
        {
            return obj is SerializationDto other ? Equals(other) : false;
        }

        public bool Equals(SerializationDto? other)
        {
            return !(other is null)
                && other.StringValue == StringValue
                && other.IntValue == IntValue
                && other.DoubleValue == DoubleValue
                && other.NullableInt == NullableInt;
        }

        public override int GetHashCode()
        {
            return (StringValue, IntValue, DoubleValue, NullableInt).GetHashCode();
        }

    }

}
