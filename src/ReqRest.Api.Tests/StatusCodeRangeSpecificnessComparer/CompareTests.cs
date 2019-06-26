namespace ReqRest.Api.Tests.StatusCodeRangeSpecificnessComparer
{
    using FluentAssertions;
    using ReqRest;
    using Xunit;

    public class CompareTests
    {

        [Theory]
        [MemberData(nameof(StatusCodeRangeData.LessSpecificThanData), MemberType = typeof(StatusCodeRangeData))]
        public void LessThan_Returned_For_Less_Specific_Range(StatusCodeRange x, StatusCodeRange y)
        {
            var comparer = new StatusCodeRangeSpecificnessComparer();
            var result = comparer.Compare(x, y);
            result.Should().BeLessThan(0);
        }

        [Theory]
        [MemberData(nameof(StatusCodeRangeData.LessSpecificThanData), MemberType = typeof(StatusCodeRangeData))]
        public void GreaterThan_Returned_For_More_Specific_Range(StatusCodeRange x, StatusCodeRange y)
        {
            var comparer = new StatusCodeRangeSpecificnessComparer();
            var result = comparer.Compare(y, x);
            result.Should().BeGreaterThan(0);
        }
        
        [Theory]
        [MemberData(nameof(StatusCodeRangeData.EquallySpecificData), MemberType = typeof(StatusCodeRangeData))]
        public void Equal_Returned_For_Same_Range_Kind(StatusCodeRange x, StatusCodeRange y)
        {
            var comparer = new StatusCodeRangeSpecificnessComparer();
            var result = comparer.Compare(x, y);
            result.Should().Be(0);
        }

        [Theory]
        [MemberData(nameof(StatusCodeRangeData.ConflictingRanges), MemberType = typeof(StatusCodeRangeData))]
        public void Equal_Returned_For_Conflicting_Ranges(StatusCodeRange x, StatusCodeRange y)
        {
            var comparer = new StatusCodeRangeSpecificnessComparer();
            var result = comparer.Compare(x, y);
            result.Should().Be(0);
        }

    }

}
