namespace ReqRest.Tests.Builders
{
    using System;
    using ReqRest.Builders;
    using Xunit;
    using System.Net.Http;
    using System.Net;
    using ReqRest.Tests.Sdk.TestRecipes;
    using ReqRest.Tests.Sdk.TestBases;
    using System.Net.Http.Headers;

    public class HttpResponseMessageBuilderTests
    {

        public class ConstructorTests
        {

            [Fact]
            public void Creates_New_HttpResponseMessage()
            {
                var builder = new HttpResponseMessageBuilder();
                Assert.NotNull(builder.HttpResponseMessage);
            }

            [Fact]
            public void Uses_Specified_HttpResponseMessage()
            {
                using var msg = new HttpResponseMessage();
                var builder = new HttpResponseMessageBuilder(msg);
                Assert.Same(msg, builder.HttpResponseMessage);
            }

        }

        public class HttpResponseMessageTests : TestBase<HttpResponseMessageBuilder>
        {

            [Fact]
            public void Throws_ArgumentNullException()
            {
                Assert.Throws<ArgumentNullException>(() => Service.HttpResponseMessage = null!);
            }

        }

        public class WrappedPropertiesTests : TestBase<HttpResponseMessageBuilder>
        {

            [Fact]
            public void Headers_Wraps_HttpRequestMessage_Property()
            {
                var wrappedHeaders = ((IHttpHeadersBuilder<HttpResponseHeaders>)Service).Headers;
                Assert.Same(Service.HttpResponseMessage.Headers, wrappedHeaders);
            }

            [Fact]
            public void Content_Wraps_HttpRequestMessage_Property()
            {
                Service.Content = new StringContent("");
                var wrappedContent = ((IHttpContentBuilder)Service).Content;
                Assert.Same(Service.HttpResponseMessage.Content, wrappedContent);
            }

            [Fact]
            public void Version_Wraps_HttpRequestMessage_Property()
            {
                Service.Version = Version.Parse("1.2.3.4");
                var wrappedVersion = ((IHttpProtocolVersionBuilder)Service).Version;
                Assert.Equal(Service.HttpResponseMessage.Version, wrappedVersion);
            }

            [Fact]
            public void ReasonPhrase_Wraps_HttpResponseMessage_Property()
            {
                Service.ReasonPhrase = "Hello";
                var wrappedReasonPhrase = ((IHttpResponseReasonPhraseBuilder)Service).ReasonPhrase;
                Assert.Equal(Service.HttpResponseMessage.ReasonPhrase, wrappedReasonPhrase);
            }

            [Fact]
            public void StatusCode_Wraps_HttpResponseMessage_Property()
            {
                Service.StatusCode = HttpStatusCode.Accepted;
                var wrappedStatusCode = ((IHttpStatusCodeBuilder)Service).StatusCode;
                Assert.Equal(Service.HttpResponseMessage.StatusCode, wrappedStatusCode);
            }

        }

        public class ToStringTests : ToStringRecipe<HttpResponseMessageBuilder>
        {

            protected override TheoryData<HttpResponseMessageBuilder, string?> Expectations => new TheoryData<HttpResponseMessageBuilder, string?>()
            {
                { new HttpResponseMessageBuilder(), new HttpResponseMessage().ToString() },
            };

        }

    }

}
