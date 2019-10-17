namespace ReqRest.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using ReqRest.Tests.Sdk.TestBases;
    using Xunit;
    using ReqRest.Tests.Sdk.Data;
    using static ReqRest.Tests.Sdk.Data.ParameterNullability;
    using System.Reflection;
    using Moq;
    using ReqRest.Builders;

    public class RestInterfaceTests
    {

        public class ConstructorTests : RestInterfaceTestBase
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
                var baseUrlProvider = new Mock<IUrlProvider>().Object;
                var restClient = new Mock<RestClient>(new RestClientConfiguration());
                var service = CreateService(restClient.Object, baseUrlProvider, builder => builder);
                Assert.Same(baseUrlProvider, service.BaseUrlProvider);
            }

        }

        public class UrlTests : RestInterfaceTestBase
        {

            [Fact]
            public void Url_Returns_Uri_Built_Via_GetUrlBuilder()
            {
                var builtUrl = ((IUrlProvider)Service).GetUrlBuilder().Uri;
                Assert.Same(builtUrl, Service.Url);
            }

        }

        public class BuildRequestTests : RestInterfaceTestBase
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

        public class GetUrlBuilderTests : RestInterfaceTestBase
        {

            [Fact]
            public void Returns_UrlBuilder_Of_BaseUrlProvider()
            {
                var builder = new UrlBuilder();
                var baseUrlProviderMock = new Mock<IUrlProvider>();
                baseUrlProviderMock.Setup(x => x.GetUrlBuilder()).Returns(builder);
                var service = CreateService(DefaultRestClient, baseUrlProviderMock.Object, DefaultBuildUrl);

                Assert.Same(builder, ((IUrlProvider)service).GetUrlBuilder());
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

                ((IUrlProvider)service).GetUrlBuilder();
                Assert.True(wasCalled);
            }

            [Fact]
            public void Calls_BuildUrl_With_UrlBuilder_Of_BaseUrlProvider()
            {
                var builder = new UrlBuilder();
                var baseUrlProviderMock = new Mock<IUrlProvider>();
                var service = CreateService(
                    DefaultRestClient,
                    baseUrlProviderMock.Object,
                    baseUrl =>
                    {
                        Assert.Same(builder, baseUrl);
                        return baseUrl;
                    }
                ); ;
                
                baseUrlProviderMock.Setup(x => x.GetUrlBuilder()).Returns(builder);
                ((IUrlProvider)service).GetUrlBuilder();
            }

        }

        public class ToStringTests : RestInterfaceTestBase
        {

            [Fact]
            public void Returns_Url_ToString()
            {
                Assert.Equal(Service.Url.ToString(), Service.ToString());
            }

        }

    }

}
