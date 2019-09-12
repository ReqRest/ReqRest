namespace ReqRest.Serializers.Tests.NoContent
{
    using FluentAssertions;
    using ReqRest.Http;
    using ReqRest.Serializers;
    using Xunit;

    public class ToStringTests
    {

        [Fact]
        public void Equals_No_Content()
        {
            new NoContent().ToString().Should().Be("No Content");
        }

    }

}
