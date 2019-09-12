namespace ReqRest.Tests.Http.StatusCodeRange
{
    using FluentAssertions;
    using ReqRest.Http;
    using Xunit;

    public class HasWildcardComponentTests
    {

        [Theory]
        [InlineData(null, 123)]
        [InlineData(123, null)]
        [InlineData(null, null)]
        public void Returns_True_For_Wildcard_Ranges(int? from, int? to)
        {
            new StatusCodeRange(from, to).HasWildcardComponent.Should().BeTrue();
        }

        [Fact]
        public void Returns_False_For_Non_Wildcard_Range()
        {
            new StatusCodeRange(123).HasWildcardComponent.Should().BeFalse();
        }

    }

}
