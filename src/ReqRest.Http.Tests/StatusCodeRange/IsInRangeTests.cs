namespace ReqRest.Http.Tests.StatusCodeRange
{
    using FluentAssertions;
    using ReqRest.Http;
    using Xunit;

    public class IsInRangeTests
    {

        [Theory]
        [InlineData(null, null, 0)]
        [InlineData(null, null, 50)]
        [InlineData(null, null, int.MaxValue)]
        [InlineData(0, 100, 0)]
        [InlineData(0, 100, 50)]
        [InlineData(0, 100, 100)]
        [InlineData(null, 100, 0)]
        [InlineData(null, 100, 50)]
        [InlineData(null, 100, 100)]
        [InlineData(100, null, 100)]
        [InlineData(100, null, 150)]
        [InlineData(100, null, int.MaxValue)]
        public void Returns_True_For_Single_Status_Codes(int? rangeFrom, int? rangeTo, int statusCode)
        {
            var range = new StatusCodeRange(rangeFrom, rangeTo);
            range.IsInRange(statusCode).Should().BeTrue();
        }
        
        [Theory]
        [InlineData(0, 100, 101)]
        [InlineData(0, 100, 200)]
        [InlineData(null, 100, 101)]
        [InlineData(null, 100, 200)]
        [InlineData(100, null, 99)]
        [InlineData(100, null, 0)]
        public void Returns_False_For_Single_Status_Codes(int? rangeFrom, int? rangeTo, int statusCode)
        {
            var range = new StatusCodeRange(rangeFrom, rangeTo);
            range.IsInRange(statusCode).Should().BeFalse();
        }

        [Theory]
        [InlineData(0, 100,  0, 100)]
        [InlineData(0, 100,  0,  50)]
        [InlineData(0, 100, 50, 100)]
        [InlineData(0, 100,  0,   0)]
        [InlineData(0, 100, 100, 100)]
        [InlineData(null, 100, null, 100)]
        [InlineData(null, 100, null,  50)]
        [InlineData(null, 100,    0, 100)]
        [InlineData(null, 100,    0,   0)]
        [InlineData(null, 100, 100,  100)]
        [InlineData(100, null, 100, null)]
        [InlineData(100, null, 150, null)]
        [InlineData(100, null, 100, 150)]
        [InlineData(100, null, 100, 100)]
        [InlineData(100, null, 150, 150)]
        [InlineData(null, null, null, null)]
        [InlineData(null, null,    0, null)]
        [InlineData(null, null, null, int.MaxValue)]
        public void Returns_True_For_Other_Range(int? outerFrom, int? outerTo, int? innerFrom, int? innerTo)
        {
            var outer = new StatusCodeRange(outerFrom, outerTo);
            var inner = new StatusCodeRange(innerFrom, innerTo);
            outer.IsInRange(inner).Should().BeTrue();
        }
        
        [Theory]
        [InlineData(100, 200,  99, 200)]
        [InlineData(100, 200, 100, 201)]
        [InlineData(100, 200, 200, 201)]
        [InlineData(100, 200,  99, 100)]
        [InlineData(100, 200, 300, 400)]
        [InlineData(100, 200,  99,  99)]
        [InlineData(100, 200, 201, 201)]
        [InlineData(null, 100, null, 101)]
        [InlineData(null, 100,   99, 101)]
        [InlineData(null, 100,  200, 200)]
        [InlineData(null, 100,  101, 101)]
        [InlineData(100, null,  99, null)]
        [InlineData(100, null,  99,  101)]
        [InlineData(100, null,   50,  50)]
        [InlineData(100, null,   99,  99)]
        public void Returns_False_For_Other_Range(int? outerFrom, int? outerTo, int? innerFrom, int? innerTo)
        {
            var outer = new StatusCodeRange(outerFrom, outerTo);
            var inner = new StatusCodeRange(innerFrom, innerTo);
            outer.IsInRange(inner).Should().BeFalse();
        }

    }

}
