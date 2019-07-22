namespace ReqRest.Client.Tests.ApiRequestBaseExtensions
{
    using System;
    using System.Net.Http;
    using FluentAssertions;
    using ReqRest.Client;
    using Xunit;

    public class SetHttpClientProviderTests
    {

        protected ApiRequest Request { get; } = new ApiRequest(() => null);

        [Fact]
        public void Set_Provider_Sets_Custom_Func()
        {
            Func<HttpClient> provider = () => null;
            Request.SetHttpClientProvider(provider);
            Request.HttpClientProvider.Should().BeSameAs(provider);
        }

        [Fact]
        public void Set_Provider_Throws_ArgumentNullException_For_Provider()
        {
            Action testCode = () => Request.SetHttpClientProvider((Func<HttpClient>)null);
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Set_HttpClient_Sets_Creates_Func_Which_Returns_Specified_HttpClient()
        {
            using var client = new HttpClient();
            Request.SetHttpClientProvider(client);
            Request.HttpClientProvider().Should().BeSameAs(client);
        }

        [Fact]
        public void Set_HttpClient_Throws_ArgumentNullException_For_Client()
        {
            Action testCode = () => Request.SetHttpClientProvider((HttpClient)null);
            testCode.Should().Throw<ArgumentNullException>();
        }

    }

}
