namespace ReqRest.Tests
{
    using System;
    using System.Net.Http;
    using ReqRest.Tests.Sdk.TestBases;
    using Xunit;

    public class RestClientConfigurationTests
    {

        public class HttpClientProviderTests : TestBase<RestClientConfiguration>
        {

            [Fact]
            public void Provides_Default_Client_If_Not_Set()
            {
                var client1 = new RestClientConfiguration().HttpClientProvider();
                var client2 = new RestClientConfiguration().HttpClientProvider();
                Assert.Same(client1, client2);
            }

            [Fact]
            public void Can_Be_Set_To_New_Provider()
            {
                Func<HttpClient> provider = () => null!;
                Service.HttpClientProvider = provider;
                Assert.Same(provider, Service.HttpClientProvider);
            }

            [Fact]
            public void Resets_To_Default_Provider_If_Set_To_Null()
            {
                var defaultProvider = Service.HttpClientProvider;
                Service.HttpClientProvider = () => null!;
                Service.HttpClientProvider = null;
                Assert.Same(defaultProvider, Service.HttpClientProvider);
            }

        }

    }

}
