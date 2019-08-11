namespace ReqRest.Client.Tests.ApiRequestBase
{
    using System;
    using System.Net.Http;
    using System.Reflection;
    using FluentAssertions;
    using Xunit;

    public class ConstructorTests : ApiRequestBaseTestBase
    {

        [Fact]
        public void Throws_ArgumentNullException_For_HttpClientProvider()
        {
            Action testCode = () => _ = CreateRequest(null);
            testCode.Should().Throw<TargetInvocationException>().WithInnerException<ArgumentNullException>();
        }

        [Fact]
        public void Uses_Specified_HttpClientProvider()
        {
            var request = CreateRequest(DefaultHttpClientProvider);
            request.HttpClientProvider.Should().BeSameAs(DefaultHttpClientProvider);
        }

        [Fact]
        public void Uses_Specified_HttpRequestMessage()
        {
            var msg = new HttpRequestMessage();
            var request = CreateRequest(DefaultHttpClientProvider, msg);
            request.HttpRequestMessage.Should().BeSameAs(msg);
        }

        [Fact]
        public void Uses_New_HttpRequestMessage_If_Null()
        {
            var request = CreateRequest(DefaultHttpClientProvider);
            request.HttpRequestMessage.Should().NotBeNull();
        }

    }

}
