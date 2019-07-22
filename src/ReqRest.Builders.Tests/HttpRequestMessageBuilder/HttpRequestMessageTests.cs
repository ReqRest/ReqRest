namespace ReqRest.Builders.Tests.HttpRequestMessageBuilder
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class HttpRequestMessageTests
    {

        [Fact]
        public void Throws_ArgumentNullException()
        {
            var builder = new ReqRest.Builders.HttpRequestMessageBuilder();
            Action testCode = () => builder.HttpRequestMessage = null;
            testCode.Should().Throw<ArgumentNullException>();
        }

    }

}
