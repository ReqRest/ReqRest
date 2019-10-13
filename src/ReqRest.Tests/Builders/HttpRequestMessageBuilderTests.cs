namespace ReqRest.Tests.Builders
{
    using System;
    using System.Net.Http;
    using ReqRest.Builders;
    using ReqRest.Tests.Sdk.TestRecipes;
    using Xunit;
    using ReqRest.Tests.Sdk.TestBases;

    public class HttpRequestMessageBuilderTests
    {

        public class ConstructorTests
        {

            [Fact]
            public void Creates_New_HttpRequestMessage()
            {
                var builder = new HttpRequestMessageBuilder();
                Assert.NotNull(builder.HttpRequestMessage);
            }

            [Fact]
            public void Uses_Specified_HttpRequestMessage()
            {
                using var msg = new HttpRequestMessage();
                var builder = new HttpRequestMessageBuilder(msg);
                Assert.Same(msg, builder.HttpRequestMessage);
            }

        }

        public class HttpRequestMessageTests : TestBase<HttpRequestMessageBuilder>
        {

            [Fact]
            public void Throws_ArgumentNullException()
            {
                Assert.Throws<ArgumentNullException>(() => Service.HttpRequestMessage = null!);
            }

        }

        public class WrappedPropertiesTests : TestBase<HttpRequestMessageBuilder>
        {

            [Fact]
            public void Headers_Wraps_HttpRequestMessage_Property()
            {
                var wrappedHeaders = ((IHttpHeadersBuilder)Service).Headers;
                Assert.Same(Service.HttpRequestMessage.Headers, wrappedHeaders);
            }

            [Fact]
            public void Properties_Wraps_HttpRequestMessage_Property()
            {
                var wrappedProperties = ((IHttpRequestPropertiesBuilder)Service).Properties;
                Assert.Same(Service.HttpRequestMessage.Properties, wrappedProperties);
            }

            [Fact]
            public void Content_Wraps_HttpRequestMessage_Property()
            {
                Service.Content = new StringContent("");
                var wrappedContent = ((IHttpContentBuilder)Service).Content;
                Assert.Same(Service.HttpRequestMessage.Content, wrappedContent);
            }

            [Fact]
            public void Version_Wraps_HttpRequestMessage_Property()
            {
                Service.Version = Version.Parse("1.2.3.4");
                var wrappedVersion = ((IHttpProtocolVersionBuilder)Service).Version;
                Assert.Equal(Service.HttpRequestMessage.Version, wrappedVersion);
            }

            [Fact]
            public void RequestUri_Wraps_HttpRequestMessage_Property()
            {
                Service.RequestUri = new Uri("http://test.com");
                var wrappedUri = ((IRequestUriBuilder)Service).RequestUri;
                Assert.Same(Service.HttpRequestMessage.RequestUri, wrappedUri);
            }

            [Fact]
            public void Method_Wraps_HttpRequestMessage_Property()
            {
                Service.Method = HttpMethod.Patch;
                var wrappedMtethod = ((IHttpMethodBuilder)Service).Method;
                Assert.Same(Service.HttpRequestMessage.Method, wrappedMtethod);
            }

        }

        public class ToStringTests : ToStringRecipe<HttpRequestMessageBuilder>
        {

            protected override TheoryData<HttpRequestMessageBuilder, string?> Expectations => new TheoryData<HttpRequestMessageBuilder, string?>()
            {
                { new HttpRequestMessageBuilder(), new HttpRequestMessage().ToString() },
            };

        }

    }

}
