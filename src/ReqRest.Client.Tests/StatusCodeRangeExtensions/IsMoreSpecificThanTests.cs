namespace ReqRest.Client.Tests.StatusCodeRangeExtensions
{
    using FluentAssertions;
    using Xunit;

    public class IsMoreSpecificThanTests
    {

        [Theory]
        [MemberData(nameof(StatusCodeRangeData.LessSpecificThanData), MemberType = typeof(StatusCodeRangeData))]
        public void Returns_True_For_More_Specific_Range(StatusCodeRange x, StatusCodeRange y)
        {
            // By just reversing the less specific data, we should get a valid test result.
            y.IsMoreSpecificThan(x).Should().BeTrue();
        }
        
        [Theory]
        [MemberData(nameof(StatusCodeRangeData.LessSpecificThanData), MemberType = typeof(StatusCodeRangeData))]
        public void Returns_False_For_Less_Specific_Range(StatusCodeRange x, StatusCodeRange y)
        {
            x.IsMoreSpecificThan(y).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(StatusCodeRangeData.EquallySpecificData), MemberType = typeof(StatusCodeRangeData))]
        [MemberData(nameof(StatusCodeRangeData.ConflictingRanges), MemberType = typeof(StatusCodeRangeData))]
        public void Returns_False_For_Equal_And_Conflicting_Ranges(StatusCodeRange x, StatusCodeRange y)
        {
            x.IsMoreSpecificThan(y).Should().BeFalse();
        }

    }

}
