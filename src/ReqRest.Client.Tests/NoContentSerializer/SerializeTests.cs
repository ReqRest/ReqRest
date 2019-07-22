namespace ReqRest.Client.Tests.NoContentSerializer
{
    using System;
    using System.Threading.Tasks;
    using FluentAssertions;
    using ReqRest.Client;
    using ReqRest.Serializers;
    using Xunit;

    public class SerializeTests
    {

        [Fact]
        public void Throws_InvalidOperationException_When_Serializing_Other_Type()
        {
            var serializer = new NoContentSerializer();
            Action testCode = () => serializer.Serialize(123, null);
            testCode.Should().Throw<HttpContentSerializationException>().WithInnerException<InvalidOperationException>();
        }

        [Fact]
        public async Task Serializes_NoContent()
        {
            var serializer = new NoContentSerializer();
            var content = serializer.Serialize(new NoContent(), null);
            (await content.ReadAsByteArrayAsync()).Length.Should().Be(0);
        }

    }

}
