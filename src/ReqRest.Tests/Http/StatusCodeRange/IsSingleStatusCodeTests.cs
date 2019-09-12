namespace ReqRest.Tests.Http.StatusCodeRange
{
    using FluentAssertions;
    using ReqRest.Http;
    using Xunit;

    public class IsSingleStatusCodeTests
    {

        [Fact]
        public void Returns_True_For_Single_Status_Codes()
        {
            new StatusCodeRange(123, 123).IsSingleStatusCode.Should().BeTrue();
        }

        [Theory]
        [InlineData(123, 456)]
        [InlineData(123, 124)]
        [InlineData(123, null)]
        [InlineData(null, 123)]
        [InlineData(null, null)] // Covers all ranges, even though From == To.
        public void Returns_False_For_Actual_Range(int? from, int? to)
        {
            new StatusCodeRange(from, to).IsSingleStatusCode.Should().BeFalse();
        }

    }

}
