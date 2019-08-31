namespace ReqRest.Tests.Internal.Serializers.StringSerializer
{
    using System;
    using ReqRest.Internal.Serializers;
    using Xunit;
    using FluentAssertions;
    using ReqRest.Serializers;

    public class SerializeTests
    {

        [Fact]
        public void Throws_NotSupportedException_When_Not_Serializing_NoContent()
        {
            var serializer = new StringSerializer();
            Action testCode = () => serializer.Serialize("str", null);
            testCode.Should().Throw<HttpContentSerializationException>().WithInnerException<NotSupportedException>();
        }

    }

}
