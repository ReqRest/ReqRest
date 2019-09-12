namespace ReqRest.Tests.Builders.HttpRequestMessageBuilder
{
    using System;
    using System.Net.Http;
    using FluentAssertions;
    using Xunit;
    using ReqRest.Builders;

    public class WrappedPropertiesTests
    {

        [Fact]
        public void Headers_Wraps_HttpRequestMessage_Property()
        {
            var builder = new HttpRequestMessageBuilder();
            var headers = ((IHttpHeadersBuilder)builder).Headers;
            headers.Should().BeSameAs(builder.HttpRequestMessage.Headers);
        }

        [Fact]
        public void Properties_Wraps_HttpRequestMessage_Property()
        {
            var builder = new HttpRequestMessageBuilder();
            var properties = ((IHttpRequestPropertiesBuilder)builder).Properties;
            properties.Should().BeSameAs(builder.HttpRequestMessage.Properties);
        }

        [Fact]
        public void Content_Wraps_HttpRequestMessage_Property()
        {
            var builder = new HttpRequestMessageBuilder().SetContent(new StringContent(""));
            var content = ((IHttpContentBuilder)builder).Content;
            content.Should().BeSameAs(builder.HttpRequestMessage.Content);
        }

        [Fact]
        public void Version_Wraps_HttpRequestMessage_Property()
        {
            var builder = new HttpRequestMessageBuilder().SetVersion(Version.Parse("1.2.3.4"));
            var version = ((IHttpProtocolVersionBuilder)builder).Version;
            version.Should().BeSameAs(builder.HttpRequestMessage.Version);
        }

        [Fact]
        public void RequestUri_Wraps_HttpRequestMessage_Property()
        {
            var builder = new HttpRequestMessageBuilder().SetRequestUri("http://test.com");
            var uri = ((IRequestUriBuilder)builder).RequestUri;
            uri.Should().BeSameAs(builder.HttpRequestMessage.RequestUri);
        }

        [Fact]
        public void Method_Wraps_HttpRequestMessage_Property()
        {
            var builder = new HttpRequestMessageBuilder().Patch();
            var method = ((IHttpMethodBuilder)builder).Method;
            method.Should().BeSameAs(builder.HttpRequestMessage.Method);
        }
        
    }

}
