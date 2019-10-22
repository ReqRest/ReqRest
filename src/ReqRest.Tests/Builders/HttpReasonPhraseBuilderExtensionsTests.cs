namespace ReqRest.Tests.Builders
{
    using System;
    using ReqRest.Builders;
    using ReqRest.Tests.Sdk.Data;
    using Xunit;
    using static ReqRest.Tests.Sdk.Data.ParameterNullability;
    using ReqRest.Tests.Sdk.TestBases;

    public class HttpReasonPhraseBuilderExtensionsTests
    {

        public class SetReasonPhraseTests : BuilderTestBase
        {

            [Theory]
            [InlineData("")]
            [InlineData("Reason phrase")]
            [InlineData(null)]
            public void Sets_ReasonPhrase(string? reasonPhrase)
            {
                Service.SetReasonPhrase(reasonPhrase);
                Assert.Equal(reasonPhrase, Service.ReasonPhrase);
            }

            [Theory, ArgumentNullExceptionData(NotNull, Null)]
            public void Throws_ArgumentNullException(IHttpResponseReasonPhraseBuilder builder, string? reasonPhrase = "")
            {
                Assert.Throws<ArgumentNullException>(() => HttpResponseReasonPhraseBuilderExtensions.SetReasonPhrase(builder, reasonPhrase));
            }

        }

    }

}
