namespace ReqRest.Builders.Tests.HttpRequestMessageBuilder
{
    using System.Net.Http;
    using FluentAssertions;
    using ReqRest;
    using Xunit;

    public class ConstructorTests
    {

        [Fact]
        public void Builder_Creates_New_Request()
        {
            var builder = new HttpRequestMessageBuilder();
            builder.HttpRequestMessage.Should().NotBeNull();
        }

        [Fact]
        public void Builder_Uses_Specified_Request()
        {
            var req = new HttpRequestMessage();
            var builder = new HttpRequestMessageBuilder(req);
            builder.HttpRequestMessage.Should().BeSameAs(req);
        }

    }

}
