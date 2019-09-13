namespace ReqRest.Tests.Internal.Serializers.NoContentSerializer
{
    using System;
    using FluentAssertions;
    using ReqRest.Http;
    using ReqRest.Internal.Serializers;
    using ReqRest.Serializers;
    using Xunit;

    public class SerializeTests
    {

        [Fact]
        public void Throws_NotSupportedException_When_Not_Serializing_NoContent()
        {
            var serializer = new NoContentSerializer();
            Action testCode = () => serializer.Serialize(123, null);
            testCode.Should().Throw<HttpContentSerializationException>().WithInnerException<NotSupportedException>();
        }

        [Fact]
        public void Serializes_NoContent()
        {
            var serializer = new NoContentSerializer();
            var content = serializer.Serialize(new NoContent(), null);
            content.Should().BeNull();
        }

    }

}
