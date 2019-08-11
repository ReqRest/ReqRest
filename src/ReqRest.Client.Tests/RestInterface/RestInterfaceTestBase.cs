namespace ReqRest.Client.Tests.RestInterface
{
    using System;
    using Moq;
    using ReqRest.Builders;
    using ReqRest.Client;

    public class RestInterfaceTestBase
    {

        protected Uri DefaultBaseUrl { get; }
        protected RestClient RestClient { get; }

        public RestInterfaceTestBase()
        {
            DefaultBaseUrl = new Uri("https://test.com");
            RestClient = new Mock<RestClient>(new RestClientConfiguration { BaseUrl = DefaultBaseUrl }).Object;
        }

        protected RestInterface CreateInterface(
            RestClient restClient, 
            IUrlProvider baseUrlProvider = null,
            Func<UrlBuilder, UrlBuilder> buildUrl = null)
        {
            buildUrl ??= (baseUrl) => baseUrl;

            var mock = new Mock<RestInterface>(restClient, baseUrlProvider) { CallBase = true };
            mock.Setup(x => x.BuildUrl(It.IsAny<UrlBuilder>())).Returns(buildUrl);
            return mock.Object;
        }

    }

}
