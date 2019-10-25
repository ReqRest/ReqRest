namespace ReqRest.Tests.Builders
{
    using System;
    using ReqRest.Builders;
    using ReqRest.Tests.Sdk.Data;
    using Xunit;
    using static ReqRest.Tests.Sdk.Data.ParameterNullability;
    using ReqRest.Tests.Sdk.TestBases;

    public class HttpProtocolVersionBuilderExtensionsTests
    {

        public class SetVersionTests : BuilderTestBase
        {

            [Fact]
            public void SetVersion_Sets_Version()
            {
                var expected = new Version(13, 7);
                Service.SetVersion(expected);
                Assert.Equal(expected, Service.Version);
            }

            [Theory, ArgumentNullExceptionData(NotNull, Null)]
            public void Throws_ArgumentNullException(IHttpProtocolVersionBuilder builder, Version version)
            {
                Assert.Throws<ArgumentNullException>(() => HttpProtocolVersionBuilderExtensions.SetVersion(builder, version));
            }

        }

    }

}
