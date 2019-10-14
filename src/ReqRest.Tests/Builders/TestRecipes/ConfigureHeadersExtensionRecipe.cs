namespace ReqRest.Tests.Builders.TestRecipes
{
    using System;
    using System.Net.Http.Headers;
    using ReqRest.Builders;
    using ReqRest.Tests.Sdk.Data;
    using ReqRest.Tests.Sdk.TestBases;
    using Xunit;
    using static ReqRest.Tests.Sdk.Data.ParameterNullability;

    public abstract class ConfigureHeadersExtensionRecipe<TBuilder, THeaders> : HttpHeadersBuilderTestBase<TBuilder, THeaders>
        where TBuilder : IHttpHeadersBuilder<THeaders>
        where THeaders : HttpHeaders
    {

        protected abstract void ConfigureHeaders(TBuilder builder, Action<THeaders> configure);

        [Fact]
        public void Invokes_Action()
        {
            var wasCalled = false;
            ConfigureHeaders(Builder, _ => wasCalled = true);
            Assert.True(wasCalled);
        }

        [Fact]
        public void Passes_Requests_Headers()
        {
            ConfigureHeaders(Builder, headers =>
            {
                Assert.Same(Builder.Headers, headers);
            });
        }

        [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
        public void Throws_ArgumentNullException(TBuilder builder, Action<THeaders> configure)
        {
            Assert.Throws<ArgumentNullException>(() => ConfigureHeaders(builder, configure));
        }

    }

}
