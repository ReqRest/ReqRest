namespace ReqRest.Tests.Builders.HttpMessageResponseBuilderExtensions
{
    using System;
    using System.Net.Http;
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;

    public class SetResponseTests : HttpResponseBuilderTestBase
    {

        [Fact]
        public void Throws_ArgumentNullException_For_Builder()
        {
            Action testCode = () => HttpResponseMessageBuilderExtensions
                .SetResponse<HttpResponseMessageBuilder>(
                    null, 
                    new HttpResponseMessage()
                );

            testCode.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void Throws_ArgumentNullException_For_Message()
        {
            Action testCode = () => Builder.SetResponse(null);
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Sets_HttpResponseMessage()
        {
            var message = new HttpResponseMessage();
            Builder.SetResponse(message);
            Builder.HttpResponseMessage.Should().BeSameAs(message);
        }

    }

}
