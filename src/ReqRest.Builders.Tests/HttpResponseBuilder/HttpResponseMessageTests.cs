namespace ReqRest.Builders.Tests.HttpResponseBuilder
{
    using System;
    using FluentAssertions;
    using ReqRest;
    using Xunit;

    public class HttpResponseMessageTests
    {

        [Fact]
        public void Throws_ArgumentNullException()
        {
            var builder = new HttpResponseMessageBuilder();
            Action testCode = () => builder.HttpResponseMessage = null;
            testCode.Should().Throw<ArgumentNullException>();
        }

    }

}
