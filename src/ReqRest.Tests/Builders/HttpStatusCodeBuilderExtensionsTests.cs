namespace ReqRest.Tests.Builders
{
    using System;
    using System.Net;
    using ReqRest.Builders;
    using ReqRest.Tests.Sdk.Data;
    using Xunit;
    using static ReqRest.Tests.Sdk.Data.ParameterNullability;
    using ReqRest.Tests.Sdk.TestBases;

    public class HttpStatusCodeBuilderExtensionsTests
    {

        public class SetStatusCodeTests : BuilderTestBase
        {

            [Fact]
            public void Int32_Sets_Status_Code()
            {
                Service.SetStatusCode((int)HttpStatusCode.Accepted);
                Assert.Equal(HttpStatusCode.Accepted, Service.StatusCode);
            }

            [Fact]
            public void HttpStatusCode_Sets_Status_Code()
            {
                Service.SetStatusCode(HttpStatusCode.Accepted);
                Assert.Equal(HttpStatusCode.Accepted, Service.StatusCode);
            }

            [Theory, ArgumentNullExceptionData(NotNull, Null)]
            public void Int32_Throws_ArgumentNullException(IHttpStatusCodeBuilder builder, int statusCode = 200)
            {
                Assert.Throws<ArgumentNullException>(() => HttpStatusCodeBuilderExtensions.SetStatusCode(builder, statusCode));
            }
            
            [Theory, ArgumentNullExceptionData(NotNull, Null)]
            public void HttpStatusCode_Throws_ArgumentNullException(IHttpStatusCodeBuilder builder, HttpStatusCode statusCode = HttpStatusCode.OK)
            {
                Assert.Throws<ArgumentNullException>(() => HttpStatusCodeBuilderExtensions.SetStatusCode(builder, statusCode));
            }

        }

    }

}
