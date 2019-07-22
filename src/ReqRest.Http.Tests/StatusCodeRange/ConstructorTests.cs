namespace ReqRest.Http.Tests.StatusCodeRange
{
    using System;
    using FluentAssertions;
    using ReqRest.Http;
    using Xunit;

    public class ConstructorTests
    {

        [Theory]
        [InlineData(null, null)]
        [InlineData(null, 123)]
        [InlineData(123, null)]
        [InlineData(123, 123)]
        [InlineData(123, 456)]
        public void Initializes_From_And_To(int? from, int? to)
        {
            var range = new StatusCodeRange(from, to);
            range.From.Should().Be(from);
            range.To.Should().Be(to);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(123)]
        public void Initializes_From_And_To_With_Single_Status_Code(int? statusCode)
        {
            var range = new StatusCodeRange(statusCode);
            range.From.Should().Be(statusCode);
            range.To.Should().Be(statusCode);
        }

        [Fact]
        public void Throws_ArgumentException_If_From_Is_Less_Than_To()
        {
            Action testCode = () => new StatusCodeRange(1, 0);
            testCode.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Throws_ArgumentOutOfRangeException_If_From_Is_Less_Than_Zero()
        {
            Action testCode = () => new StatusCodeRange(-1, 123);
            testCode.Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [Fact]
        public void Throws_ArgumentOutOfRangeException_If_To_Is_Less_Than_Zero()
        {
            Action testCode = () => new StatusCodeRange(123, -1);
            testCode.Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [Fact]
        public void Throws_ArgumentOutOfRangeException_If_Single_Status_Code_Is_Less_Than_Zero()
        {
            Action testCode = () => new StatusCodeRange(-1);
            testCode.Should().Throw<ArgumentOutOfRangeException>();
        }

    }

}
