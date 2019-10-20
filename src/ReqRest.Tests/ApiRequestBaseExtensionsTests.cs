namespace ReqRest.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using Xunit;
    using System.Net.Http;
    using ReqRest.Tests.Sdk.TestBases;

    public class ApiRequestBaseExtensionsTests
    {

        public class SetHttpClientProviderTests : TestBase<ApiRequest>
        {

            [Fact]
            public void Set_Provider_Sets_Custom_Func()
            {
                Func<HttpClient> provider = () => null!;
                Service.SetHttpClientProvider(provider);
                Assert.Same(provider, Service.HttpClientProvider);
            }

            [Fact]
            public void Set_Provider_Throws_ArgumentNullException_For_Provider()
            {
                Assert.Throws<ArgumentNullException>(() => Service.SetHttpClientProvider((Func<HttpClient>)null!));
            }

            [Fact]
            public void Set_HttpClient_Sets_Creates_Func_Which_Returns_Specified_HttpClient()
            {
                using var client = new HttpClient();
                Service.SetHttpClientProvider(client);
                Assert.Same(client, Service.HttpClientProvider());
            }

            [Fact]
            public void Set_HttpClient_Throws_ArgumentNullException_For_Client()
            {
                Assert.Throws<ArgumentNullException>(() => Service.SetHttpClientProvider((HttpClient)null!));
            }

        }

    }

}
