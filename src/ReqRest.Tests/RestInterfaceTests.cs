namespace ReqRest.Tests
{
    using System;
    using Moq;
    using ReqRest.Builders;
    using ReqRest.Tests.Sdk.TestBases;
    using Xunit;

    public abstract class RestInterfaceTests : TestBase<RestInterface>
    {

        protected virtual RestClient DefaultRestClient => new Mock<RestClient>(new RestClientConfiguration()).Object;

        protected virtual IBaseUrlProvider DefaultBaseUrlProvider
        {
            get
            {
                var mock = new Mock<IBaseUrlProvider>();
                mock.Setup(x => x.BuildBaseUrl()).Returns(new UrlBuilder());
                return mock.Object;
            }
        }

        protected virtual Func<UrlBuilder, UrlBuilder> DefaultBuildUrl => builder => builder;

        protected override RestInterface CreateService()
        {
            return CreateService(DefaultRestClient, DefaultBaseUrlProvider, DefaultBuildUrl);
        }

        protected virtual RestInterface CreateService(
            RestClient restClient,
            IBaseUrlProvider? baseUrlProvider,
            Func<UrlBuilder, UrlBuilder> buildUrl)
        {
            var mock = new Mock<RestInterface>(restClient, baseUrlProvider) { CallBase = true };
            mock.Setup(x => x.BuildUrl(It.IsAny<UrlBuilder>())).Returns(buildUrl);
            return mock.Object;
        }

        public class ConstructorTests : RestInterfaceTests
        {

            [Fact]
            public void Throws_ArgumentNullException()
            {
                var ex = Record.Exception(() => CreateService(null!, DefaultBaseUrlProvider, DefaultBuildUrl)).InnerException;
                Assert.IsType<ArgumentNullException>(ex);
            }

            [Fact]
            public void Sets_BaseUrlProvider_To_Specified_RestClient_If_Null()
            {
                var service = CreateService(DefaultRestClient, null, DefaultBuildUrl);
                Assert.Same(service.Client, service.BaseUrlProvider);
            }

            [Fact]
            public void Sets_BaseUrlProvider_To_Specified_Value_If_Not_Null()
            {
                var baseUrlProvider = new Mock<IBaseUrlProvider>().Object;
                var restClient = new Mock<RestClient>(new RestClientConfiguration());
                var service = CreateService(restClient.Object, baseUrlProvider, builder => builder);
                Assert.Same(baseUrlProvider, service.BaseUrlProvider);
            }

        }

        public class UrlTests : RestInterfaceTests
        {

            [Fact]
            public void Url_Returns_Uri_Built_Via_GetUrlBuilder()
            {
                var builtUrl = ((IBaseUrlProvider)Service).BuildBaseUrl().Uri;
                Assert.Same(builtUrl, Service.Url);
            }

        }

        public class BuildRequestTests : RestInterfaceTests
        {

            [Fact]
            public void Returns_New_ApiRequest_Every_Time()
            {
                var req1 = Service.BuildRequest();
                var req2 = Service.BuildRequest();
                Assert.NotSame(req1, req2);
            }

            [Fact]
            public void Request_Uses_HttpClientProvider_From_RestClient_Config()
            {
                var req = Service.BuildRequest();
                Assert.Same(Service.Client.Configuration.HttpClientProvider, req.HttpClientProvider);
            }

            [Fact]
            public void Request_Uses_Url_Of_Interface()
            {
                var req = Service.BuildRequest();
                Assert.Equal(Service.Url, req.RequestUri);
            }

        }

        public class BuildBaseUrlTests : RestInterfaceTests
        {

            [Fact]
            public void Returns_UrlBuilder_Of_BaseUrlProvider()
            {
                var builder = new UrlBuilder();
                var baseUrlProviderMock = new Mock<IBaseUrlProvider>();
                baseUrlProviderMock.Setup(x => x.BuildBaseUrl()).Returns(builder);
                var service = CreateService(DefaultRestClient, baseUrlProviderMock.Object, DefaultBuildUrl);

                Assert.Same(builder, ((IBaseUrlProvider)service).BuildBaseUrl());
            }

            [Fact]
            public void Calls_BuildUrl()
            {
                var wasCalled = false;
                var service = CreateService(
                    DefaultRestClient, 
                    DefaultBaseUrlProvider, 
                    builder => { wasCalled = true; return builder; }
                );

                ((IBaseUrlProvider)service).BuildBaseUrl();
                Assert.True(wasCalled);
            }

            [Fact]
            public void Calls_BuildUrl_With_UrlBuilder_Of_BaseUrlProvider()
            {
                var builder = new UrlBuilder();
                var baseUrlProviderMock = new Mock<IBaseUrlProvider>();
                var service = CreateService(
                    DefaultRestClient,
                    baseUrlProviderMock.Object,
                    baseUrl =>
                    {
                        Assert.Same(builder, baseUrl);
                        return baseUrl;
                    }
                ); ;
                
                baseUrlProviderMock.Setup(x => x.BuildBaseUrl()).Returns(builder);
                ((IBaseUrlProvider)service).BuildBaseUrl();
            }

        }

        public class ToStringTests : RestInterfaceTests
        {

            [Fact]
            public void Returns_Url_ToString()
            {
                Assert.Equal(Service.Url.ToString(), Service.ToString());
            }

        }

    }

}
