namespace ReqRest.Http.Tests.StatusCodeRange
{
    using FluentAssertions;
    using ReqRest.Http;
    using Xunit;

    public class DeconstructsTests
    {

        [Theory]
        [InlineData(null, null)]
        [InlineData(null, 200)]
        [InlineData(200, null)]
        [InlineData(200, 200)]
        [InlineData(100, 200)]
        public void Deconstructs_Into_From_And_To(int? from, int? to)
        {
            var range = new StatusCodeRange(from, to);
            range.Deconstruct(out var newFrom, out var newTo);
            newFrom.Should().Be(from);
            newTo.Should().Be(to);
        }

    }

}
