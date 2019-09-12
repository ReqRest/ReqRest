namespace ReqRest.Tests.Builders.HttpResponseBuilder
{
    using System;
    using FluentAssertions;
    using ReqRest.Builders;
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
