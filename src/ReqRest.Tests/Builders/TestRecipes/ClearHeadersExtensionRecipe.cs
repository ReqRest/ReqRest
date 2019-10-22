namespace ReqRest.Tests.Builders.TestRecipes
{
    using System;
    using System.Net.Http.Headers;
    using ReqRest.Builders;
    using ReqRest.Tests.Sdk.Data;
    using ReqRest.Tests.Sdk.TestBases;
    using Xunit;
    using static ReqRest.Tests.Sdk.Data.ParameterNullability;

    public abstract class ClearHeadersExtensionRecipe<TBuilder, THeaders> : HttpHeadersBuilderTestBase<TBuilder, THeaders>
        where TBuilder : IHttpHeadersBuilder<THeaders>
        where THeaders : HttpHeaders
    {

        protected abstract void ClearHeaders(TBuilder builder);

        [Fact]
        public void ClearHeaders_Clears_Headers()
        {
            Builder.Headers.Add("Test", "Header");
            ClearHeaders(Builder);
            Assert.Empty(Builder.Headers);
        }

        [Theory, ArgumentNullExceptionData(NotNull)]
        public void Throws_ArgumentNullException(TBuilder builder)
        {
            Assert.Throws<ArgumentNullException>(() => ClearHeaders(builder));
        }

    }

}
