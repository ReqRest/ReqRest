namespace ReqRest.Builders.Tests.HttpResponseMessageBuilder
{
    using System;
    using System.Net.Http;
    using FluentAssertions;
    using Xunit;
    using ReqRest.Builders;

    public class WrappedPropertiesTests
    {
        
        [Fact]
        public void Headers_Wraps_HttpResponseMessage_Property()
        {
            var builder = new HttpResponseMessageBuilder();
            var headers = ((IHttpHeadersBuilder)builder).Headers;
            headers.Should().BeSameAs(builder.HttpResponseMessage.Headers);
        }

        [Fact]
        public void Content_Wraps_HttpResponseMessage_Property()
        {
            var builder = new HttpResponseMessageBuilder().SetContent(new StringContent(""));
            var content = ((IHttpContentBuilder)builder).Content;
            content.Should().BeSameAs(builder.HttpResponseMessage.Content);
        }

        [Fact]
        public void Version_Wraps_HttpResponseMessage_Property()
        {
            var builder = new HttpResponseMessageBuilder().SetVersion(Version.Parse("1.2.3.4"));
            var version = ((IHttpProtocolVersionBuilder)builder).Version;
            version.Should().BeSameAs(builder.HttpResponseMessage.Version);
        }

        [Fact]
        public void ReasonPhrase_Wraps_HttpResponseMessage_Property()
        {
            var builder = new HttpResponseMessageBuilder().SetReasonPhrase("Test");
            var reasonPhrase = ((IHttpResponseReasonPhraseBuilder)builder).ReasonPhrase;
            reasonPhrase.Should().BeEquivalentTo(builder.HttpResponseMessage.ReasonPhrase);
        }

        [Fact]
        public void StatusCode_Wraps_HttpResponseMessage_Property()
        {
            var builder = new HttpResponseMessageBuilder().SetStatusCode(123);
            var statusCode = ((IHttpStatusCodeBuilder)builder).StatusCode;
            statusCode.Should().Be(builder.HttpResponseMessage.StatusCode);
        }
        
    }

}
