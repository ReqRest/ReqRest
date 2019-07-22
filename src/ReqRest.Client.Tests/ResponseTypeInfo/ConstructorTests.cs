namespace ReqRest.Client.Tests.ResponseTypeInfo
{
    using System;
    using FluentAssertions;
    using ReqRest.Client;
    using ReqRest.Http;
    using Xunit;

    public class ConstructorTests
    {

        [Fact]
        public void Throws_ArgumentNullException_For_ResponseType()
        {
            Action testCode = () => new ResponseTypeInfo(null, new[] { StatusCodeRange.All }, () => null);
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Throws_ArgumentNullException_For_StatusCodes()
        {
            Action testCode = () => new ResponseTypeInfo(typeof(object), null, () => null);
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Throws_ArgumentNullException_For_ResponseDeserializerFactory()
        {
            Action testCode = () => new ResponseTypeInfo(typeof(object), new[] { StatusCodeRange.All }, null);
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Throws_ArgumentException_For_Empty_StatusCodes()
        {
            Action testCode = () => new ResponseTypeInfo(typeof(object), new StatusCodeRange[0], () => null);
            testCode.Should().Throw<ArgumentException>();
        }

    }

}
