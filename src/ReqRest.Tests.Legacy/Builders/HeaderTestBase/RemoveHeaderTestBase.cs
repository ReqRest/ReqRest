namespace ReqRest.Tests.Builders.HeaderTestBase
{
    using System.Linq;
    using System.Net.Http.Headers;
    using FluentAssertions;
    using Xunit;

    public abstract class RemoveHeaderTestBase<THeaders> : HttpHeadersTestBase<THeaders> where THeaders : HttpHeaders
    {

        protected abstract void RemoveHeader(params string[] names);

        [Theory]
        [InlineData(new string[] { "Test1" }, new string[] { "Test1" })]
        [InlineData(new string[] { "Test1", "Test2" }, new string[] { "Test1" })]
        [InlineData(new string[] { "Test1", "Test2" }, new string[] { "NotFound" })]
        [InlineData(new string[] { "Test1", "Test2" }, new string[] { null })]
        [InlineData(new string[] { "Test1", "Test2" }, new string[] { })]
        [InlineData(new string[] { "Test1", "Test2", "Test3" }, null)]
        public void RemoveHeader_Removes_Expected_Headers(string[] initial, string[] toRemove)
        {
            var remainingHeaders = initial.ToHashSet();
            remainingHeaders.ExceptWith(toRemove ?? new string[0]);

            foreach (var header in initial)
            {
                Headers.Add(header, "");
            }

            RemoveHeader(toRemove);

            foreach (var remaining in remainingHeaders)
            {
                Headers.Should().Contain(header => header.Key == remaining);
            }
        }

    }

}
