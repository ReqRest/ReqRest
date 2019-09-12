namespace ReqRest.Tests.Builders.HttpRequestMessageBuilderExtensions
{
    using System;
    using System.Net.Http;
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;

    public class SetRequestTests : HttpRequestBuilderTestBase
    {

        [Fact]
        public void Throws_ArgumentNullException_For_Builder()
        {
            Action testCode = () => HttpRequestMessageBuilderExtensions
                .SetRequest<HttpRequestMessageBuilder>(
                    null,
                    new HttpRequestMessage()
                );

            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Throws_ArgumentNullException_For_Message()
        {
            Action testCode = () => Builder.SetRequest(null);
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Sets_HttpRequestMessage()
        {
            var message = new HttpRequestMessage();
            Builder.SetRequest(message);
            Builder.HttpRequestMessage.Should().BeSameAs(message);
        }

    }

}
