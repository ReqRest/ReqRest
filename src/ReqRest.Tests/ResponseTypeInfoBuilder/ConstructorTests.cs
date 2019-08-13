namespace ReqRest.Tests.ResponseTypeInfoBuilder
{
    using System;
    using FluentAssertions;
    using ReqRest;
    using Xunit;

    public class ConstructorTests
    {

        [Fact]
        public void Throws_ArgumentNullException_For_Request()
        {
            Action testCode = () => new ResponseTypeInfoBuilder<ApiRequest>(null, typeof(object));
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Throws_ArgumentNullException_For_ResponseType()
        {
            var request = new ApiRequest(() => null);
            Action testCode = () => new ResponseTypeInfoBuilder<ApiRequest>(request, null);
            testCode.Should().Throw<ArgumentNullException>();
        }

    }

}
