namespace ReqRest.Tests.Builders.HeaderTestBase
{
    using System;
    using System.Net.Http.Headers;
    using FluentAssertions;
    using Xunit;

    public abstract class ConfigureHeadersTestBase<THeaders> : HttpHeadersTestBase<THeaders> where THeaders : HttpHeaders
    {

        protected abstract void ConfigureHeaders(Action<THeaders> configure);

        [Fact]
        public void ConfigureHeaders_Throws_ArgumentNullException_For_Action()
        {
            Action testCode = () => ConfigureHeaders(null);
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ConfigureHeaders_Invokes_Action()
        {
            var wasCalled = false;
            ConfigureHeaders(_ => wasCalled = true);
            wasCalled.Should().BeTrue();
        }

        [Fact]
        public void ConfigureHeaders_Passes_Requests_Headers()
        {
            ConfigureHeaders(headers =>
            {
                headers.Should().BeSameAs(Headers);
            });
        }

    }

}
