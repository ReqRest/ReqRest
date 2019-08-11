namespace ReqRest.Client.Tests.ApiInterface
{
    using System;
    using Moq;
    using ReqRest.Builders;
    using ReqRest.Client;

    public class ApiInterfaceTestBase
    {

        protected Uri DefaultBaseUrl { get; }
        protected ApiClient DefaultApiClient { get; }

        public ApiInterfaceTestBase()
        {
            DefaultBaseUrl = new Uri("https://test.com");
            DefaultApiClient = new Mock<ApiClient>(new ApiClientConfiguration { BaseUrl = DefaultBaseUrl }).Object;
        }

        protected ApiInterface CreateInterface(
            ApiClient apiClient, 
            IUrlProvider baseUrlProvider = null,
            Func<UrlBuilder, UrlBuilder> buildUrl = null)
        {
            buildUrl ??= (baseUrl) => baseUrl;

            var mock = new Mock<ApiInterface>(apiClient, baseUrlProvider) { CallBase = true };
            mock.Setup(x => x.BuildUrl(It.IsAny<UrlBuilder>())).Returns(buildUrl);
            return mock.Object;
        }

    }

}
