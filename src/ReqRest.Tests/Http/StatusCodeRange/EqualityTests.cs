namespace ReqRest.Tests.Http.StatusCodeRange
{
    using FluentAssertions;
    using ReqRest.Http;
    using Xunit;

    public class EqualityTests
    {

        [Theory]
        [InlineData(null, null, null, null)]
        [InlineData(null, 200, null, 200)]
        [InlineData(200, null, 200, null)]
        [InlineData(200, 200, 200, 200)]
        [InlineData(100, 200, 100, 200)]
        public void True_For_Equal_Ranges(int? xFrom, int? xTo, int? yFrom, int? yTo)
        {
            var x = new StatusCodeRange(xFrom, xTo);
            var y = new StatusCodeRange(yFrom, yTo);

            x.Equals(y).Should().BeTrue();
            x.Equals((object)y).Should().BeTrue();
            (x == y).Should().BeTrue();
            (!(x != y)).Should().BeTrue();
            x.GetHashCode().Should().Be(y.GetHashCode());
        }
        
        [Theory]
        [InlineData(200, 200, 100, 200)]
        [InlineData(200, 200, 200, null)]
        [InlineData(200, 200, null, 200)]
        [InlineData(200, 200, null, null)]
        public void False_For_Unequal_Ranges(int? xFrom, int? xTo, int? yFrom, int? yTo)
        {
            var x = new StatusCodeRange(xFrom, xTo);
            var y = new StatusCodeRange(yFrom, yTo);

            x.Equals(y).Should().BeFalse();
            x.Equals((object)y).Should().BeFalse();
            (x == y).Should().BeFalse();
            (!(x != y)).Should().BeFalse();
            x.GetHashCode().Should().NotBe(y.GetHashCode());
        }

        [Fact]
        public void False_For_Other_Object()
        {
            var range = StatusCodeRange.All;
            range.Equals(new object()).Should().BeFalse();
        }
        
    }

}
