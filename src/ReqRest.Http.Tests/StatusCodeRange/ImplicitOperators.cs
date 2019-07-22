namespace ReqRest.Http.Tests.StatusCodeRange
{
    using FluentAssertions;
    using ReqRest.Http;
    using Xunit;

    public class ImplicitOperators
    {

        [Theory]
        [InlineData(null, null)]
        [InlineData(null, 123)]
        [InlineData(123, null)]
        [InlineData(123, 123)]
        [InlineData(123, 456)]
        public void Can_Create_From_Tuple(int? from, int? to)
        {
            StatusCodeRange range = (from, to);
            range.From.Should().Be(from);
            range.To.Should().Be(to);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(123)]
        public void Can_Create_From_Integer(int? statusCode)
        {
            StatusCodeRange range = statusCode;
            range.From.Should().Be(statusCode);
            range.To.Should().Be(statusCode);
        }

    }

}
