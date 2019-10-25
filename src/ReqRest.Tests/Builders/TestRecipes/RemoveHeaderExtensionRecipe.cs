namespace ReqRest.Tests.Builders.TestRecipes
{
    using System;
    using System.Linq;
    using System.Net.Http.Headers;
    using ReqRest.Builders;
    using ReqRest.Tests.Sdk.Data;
    using ReqRest.Tests.Sdk.TestBases;
    using Xunit;
    using static ReqRest.Tests.Sdk.Data.ParameterNullability;

    public abstract class RemoveHeaderExtensionRecipe<TBuilder, THeaders> : HttpHeadersBuilderTestBase<TBuilder, THeaders>
        where TBuilder : IHttpHeadersBuilder<THeaders>
        where THeaders : HttpHeaders
    {

        protected abstract void RemoveHeader(TBuilder builder, params string?[]? names);

        [Theory]
        [InlineData(new string[] { "Test1" }, new string?[] { "Test1" })]
        [InlineData(new string[] { "Test1", "Test2" }, new string?[] { "Test1" })]
        [InlineData(new string[] { "Test1", "Test2" }, new string?[] { "NotFound" })]
        [InlineData(new string[] { "Test1", "Test2" }, new string?[] { null })]
        [InlineData(new string[] { "Test1", "Test2" }, new string?[] { })]
        [InlineData(new string[] { "Test1", "Test2", "Test3" }, null)]
        public void RemoveHeader_Removes_Expected_Headers(string[] initial, string?[]? toRemove)
        {
            var remainingHeaders = initial.ToHashSet();
            remainingHeaders.ExceptWith((toRemove ?? Array.Empty<string?>())!);

            foreach (var header in initial)
            {
                Builder.Headers.Add(header, "");
            }

            RemoveHeader(Builder, toRemove);

            foreach (var remaining in remainingHeaders)
            {
                Assert.Contains(Builder.Headers, header => header.Key == remaining);
            }
        }

        [Theory, ArgumentNullExceptionData(NotNull, Null)]
        public void Throws_ArgumentNullException(TBuilder builder, string?[]? names = null)
        {
            Assert.Throws<ArgumentNullException>(() => RemoveHeader(builder, names));
        }

    }

}
