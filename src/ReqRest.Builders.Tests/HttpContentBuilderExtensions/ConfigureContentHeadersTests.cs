namespace ReqRest.Builders.Tests.HttpContentBuilderExtensions
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using FluentAssertions;
    using ReqRest.Builders;
    using ReqRest.Builders.Tests.HeaderTestBase;
    using Xunit;

    public class ConfigureContentHeadersTests : ConfigureHeadersTestBase<HttpContentHeaders>
    {

        private readonly IHttpContentBuilder _builder = new HttpRequestMessageBuilder()
        {
            HttpRequestMessage = new HttpRequestMessage()
            {
                Content = new ByteArrayContent(new byte[0]),
            },
        };

        protected override HttpContentHeaders Headers => _builder.Content.Headers;

        protected override void ConfigureHeaders(Action<HttpContentHeaders> configure) =>
            _builder.ConfigureContentHeaders(configure);

        [Fact]
        public void Throws_InvalidOperationException_If_Content_Is_Null()
        {
            _builder.Content = null;
            Action testCode = () => _builder.ConfigureContentHeaders(_ => { });
            testCode.Should().Throw<InvalidOperationException>();
        }
        
    }

}
