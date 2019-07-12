namespace ReqRest.Client.Tests.ApiClientConfiguration
{
    using System;
    using System.Net.Http;
    using FluentAssertions;
    using ReqRest.Client;
    using Xunit;

    public class HttpClientProviderTests
    {

        [Fact]
        public void Provides_Default_Client_If_Not_Set()
        {
            var client1 = new ApiClientConfiguration().HttpClientProvider();
            var client2 = new ApiClientConfiguration().HttpClientProvider();
            client1.Should().BeSameAs(client2);
        }

        [Fact]
        public void Can_Be_Set_To_New_Provider()
        {
            Func<HttpClient> provider = () => null;
            var config = new ApiClientConfiguration();

            config.HttpClientProvider = provider;
            config.HttpClientProvider.Should().BeSameAs(provider);
        }

        [Fact]
        public void Resets_To_Default_Provider_If_Set_To_Null()
        {
            var config = new ApiClientConfiguration();
            var defaultProvider = config.HttpClientProvider;

            config.HttpClientProvider = () => null;
            config.HttpClientProvider = null;

            config.HttpClientProvider.Should().BeSameAs(defaultProvider);
        }

    }

}
