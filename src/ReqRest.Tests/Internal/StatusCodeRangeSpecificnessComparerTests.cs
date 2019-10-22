namespace ReqRest.Tests.Internal
{
    using ReqRest.Http;
    using ReqRest.Internal;
    using ReqRest.Tests.Internal.TestData;
    using Xunit;

    public class StatusCodeRangeSpecificnessComparerTests
    {

        public class CompareTests
        {

            [Theory]
            [MemberData(nameof(StatusCodeRangeTestData.LessSpecificThanData), MemberType = typeof(StatusCodeRangeTestData))]
            public void Returns_LessThan_For_Less_Specific_Range(StatusCodeRange x, StatusCodeRange y)
            {
                var comparer = new StatusCodeRangeSpecificnessComparer();
                var result = comparer.Compare(x, y);
                Assert.True(result < 0);
            }

            [Theory]
            [MemberData(nameof(StatusCodeRangeTestData.LessSpecificThanData), MemberType = typeof(StatusCodeRangeTestData))]
            public void Returns_GreaterThan_For_More_Specific_Range(StatusCodeRange x, StatusCodeRange y)
            {
                var comparer = new StatusCodeRangeSpecificnessComparer();
                var result = comparer.Compare(y, x);
                Assert.True(result > 0);
            }

            [Theory]
            [MemberData(nameof(StatusCodeRangeTestData.EquallySpecificData), MemberType = typeof(StatusCodeRangeTestData))]
            public void Returns_Equal_For_Same_Range_Kind(StatusCodeRange x, StatusCodeRange y)
            {
                var comparer = new StatusCodeRangeSpecificnessComparer();
                var result = comparer.Compare(x, y);
                Assert.Equal(0, result);
            }

            [Theory]
            [MemberData(nameof(StatusCodeRangeTestData.ConflictingRanges), MemberType = typeof(StatusCodeRangeTestData))]
            public void Returns_Equal_For_Conflicting_Ranges(StatusCodeRange x, StatusCodeRange y)
            {
                var comparer = new StatusCodeRangeSpecificnessComparer();
                var result = comparer.Compare(x, y);
                Assert.Equal(0, result);
            }

        }

    }

}
