namespace ReqRest.Client.Tests.ApiRequestBase
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class HttpClientProviderTests : ApiRequestBaseTestBase
    {

        [Fact]
        public void Throws_ArgumentNullException()
        {
            var request = CreateRequest(DefaultHttpClientProvider);
            Action testCode = () => request.HttpClientProvider = null;
            testCode.Should().Throw<ArgumentNullException>();
        }

    }

}
