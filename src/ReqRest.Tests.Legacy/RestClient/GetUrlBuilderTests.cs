namespace ReqRest.Tests.RestClient
{
    using System;
    using FluentAssertions;
    using Moq;
    using ReqRest.Builders;
    using ReqRest;
    using Xunit;

    public class GetUrlBuilderTests
    {

        [Fact]
        public void Always_Returns_New_Builder()
        {
            Builder().Should().NotBeSameAs(Builder());
        }

        [Fact]
        public void Returns_Builder_With_Default_Url_If_Configured_BaseUrl_Is_Null()
        {
            var url = Builder().Uri;
            url.Scheme.Should().Be("http");
            url.Host.Should().Be("localhost");
            url.Port.Should().Be(80);
        }

        [Fact]
        public void Returns_Builder_With_Configured_Url()
        {
            var config = new RestClientConfiguration() { BaseUrl = new Uri("https://test.com/foo") };
            var url = Builder(config).Uri;
            url.Should().Be(config.BaseUrl);
        }

        private static UrlBuilder Builder(RestClientConfiguration config = null)
        {
            var client = new Mock<RestClient>(config).Object;
            return ((IUrlProvider)client).GetUrlBuilder();
        }

    }

}
