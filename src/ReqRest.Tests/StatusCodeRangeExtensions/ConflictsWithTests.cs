namespace ReqRest.Tests.StatusCodeRangeExtensions
{
    using FluentAssertions;
    using ReqRest.Http;
    using Xunit;

    public class ConflictsWithTests
    {

        [Theory]
        [MemberData(nameof(StatusCodeRangeData.ConflictingRanges), MemberType = typeof(StatusCodeRangeData))]
        public void Returns_True_For_Conflicting_Ranges(StatusCodeRange x, StatusCodeRange y)
        {
            x.ConflictsWith(y).Should().BeTrue();
            y.ConflictsWith(x).Should().BeTrue();
        }
        
        [Theory]
        [MemberData(nameof(StatusCodeRangeData.NonConflictingRanges), MemberType = typeof(StatusCodeRangeData))]
        public void Returns_False_For_Non_Conflicting_Ranges(StatusCodeRange x, StatusCodeRange y)
        {
            x.ConflictsWith(y).Should().BeFalse();
            y.ConflictsWith(x).Should().BeFalse();
        }

    }

}
