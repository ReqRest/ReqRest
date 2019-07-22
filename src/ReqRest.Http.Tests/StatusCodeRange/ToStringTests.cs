namespace ReqRest.Http.Tests.StatusCodeRange
{
    using FluentAssertions;
    using ReqRest.Http;
    using Xunit;

    public class ToStringTests
    {

        [Theory]
        [InlineData(null, null, "*")]
        [InlineData(200, 200, "200")]
        [InlineData(null, 200, "*-200")]
        [InlineData(200, null, "200-*")]
        [InlineData(200, 300, "200-300")]
        public void Returns_Expected_String(int? from, int? to, string expected)
        {
            new StatusCodeRange(from, to).ToString().Should().Be(expected);
        }

    }

}
