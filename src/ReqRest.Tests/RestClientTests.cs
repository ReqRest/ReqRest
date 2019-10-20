namespace ReqRest.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using Moq;
    using Xunit;
    using ReqRest.Tests.Sdk.TestBases;
    using ReqRest.Builders;

    public abstract class RestClientTests : TestBase<RestClient>
    {

        protected override RestClient CreateService()
        {
            return CreateService(null);
        }

        protected virtual RestClient CreateService(RestClientConfiguration? configuration)
        {
            var mock = new Mock<RestClient>(configuration) { CallBase = true };
            return mock.Object;
        }

        public class ConstructorTests : RestClientTests
        {

            [Fact]
            public void Uses_Specified_Configuration()
            {
                var config = new RestClientConfiguration();
                var service = CreateService(config);
                Assert.Same(config, service.Configuration);
            }

            [Fact]
            public void Creates_Default_Configuration_If_None_Is_Specified()
            {
                var service = CreateService(null);
                Assert.NotNull(service.Configuration);
                Assert.IsType<RestClientConfiguration>(service.Configuration);
            }

        }

        public class ConfigurationTests : RestClientTests
        {

            [Fact]
            public void Throws_ArgumentNullException()
            {
                Assert.Throws<ArgumentNullException>(() => Service.Configuration = null!);
            }

            [Fact]
            public void Sets_Configuration()
            {
                var config = new RestClientConfiguration();
                Service.Configuration = config;
                Assert.Same(config, Service.Configuration);
            }

        }

        public class GetUrlBuilderTests : RestClientTests
        {

            [Fact]
            public void Always_Returns_New_Builder()
            {
                var first = GetBuilder(Service);
                var second = GetBuilder(Service);
                Assert.NotSame(first, second);
            }

            [Fact]
            public void Returns_Builder_With_Default_Url_If_Configured_BaseUrl_Is_Null()
            {
                var url = GetBuilder(Service).Uri;
                Assert.Equal("http", url.Scheme);
                Assert.Equal("localhost", url.Host);
                Assert.Equal(80, url.Port);
            }

            [Fact]
            public void Returns_Builder_With_Configured_Url()
            {
                var config = new RestClientConfiguration() { BaseUrl = new Uri("https://test.com/foo") };
                var service = CreateService(config);
                var url = GetBuilder(service).Uri;
                Assert.Equal(config.BaseUrl, url);
            }

            private static UrlBuilder GetBuilder(RestClient client)
            {
                return ((IUrlProvider)client).GetUrlBuilder();
            }

        }

    }

}
