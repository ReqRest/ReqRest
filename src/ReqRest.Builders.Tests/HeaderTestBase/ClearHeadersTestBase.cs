namespace ReqRest.Builders.Tests.HeaderTestBase
{
    using System.Net.Http.Headers;
    using FluentAssertions;
    using Xunit;

    public abstract class ClearHeadersTestBase<THeaders> : HttpHeadersTestBase<THeaders> where THeaders : HttpHeaders
    {

        protected abstract void ClearHeaders();

        [Fact]
        public void ClearHeaders_Clears_Headers()
        {
            Headers.Add("Test", "Header");
            ClearHeaders();
            Headers.Should().BeEmpty();
        }

    }

}
