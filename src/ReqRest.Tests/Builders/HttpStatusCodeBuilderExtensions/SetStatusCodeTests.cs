namespace ReqRest.Tests.Builders.HttpStatusCodeBuilderExtensions
{
    using System.Net;
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;

    public class SetStatusCodeTests : HttpResponseBuilderTestBase
    {

        [Theory]
        [InlineData(200)]
        [InlineData(400)]
        public void Sets_Status_Code(int statusCode)
        {
            Builder.SetStatusCode(statusCode);
            ((IHttpStatusCodeBuilder)Builder).StatusCode.Should().Be(statusCode);

            Builder.SetStatusCode((HttpStatusCode)statusCode);
            ((IHttpStatusCodeBuilder)Builder).StatusCode.Should().Be(statusCode);
        }

    }

}
