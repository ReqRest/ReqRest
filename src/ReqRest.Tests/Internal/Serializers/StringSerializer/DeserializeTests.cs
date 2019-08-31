namespace ReqRest.Tests.Internal.Serializers.StringSerializer
{
    using System;
    using System.Threading.Tasks;
    using ReqRest.Internal.Serializers;
    using Xunit;
    using FluentAssertions;
    using ReqRest.Serializers;
    using System.Net.Http;

    public class DeserializeTests
    {

        [Fact]
        public async Task Throws_NotSupportedException_When_Deserializing_Other_Type()
        {
            var serializer = new StringSerializer();
            Func<Task> testCode = async () => await serializer.DeserializeAsync(new ByteArrayContent(Array.Empty<byte>()), typeof(int));
            (await testCode.Should().ThrowAsync<HttpContentSerializationException>()).WithInnerException<NotSupportedException>();
        }

        [Fact]
        public async Task Deserializes_String()
        {
            var expected = "Hello World";
            var serializer = new StringSerializer();
            var deserialized = await serializer.DeserializeAsync(new StringContent(expected), typeof(string));
            deserialized.Should().BeOfType<string>();
            deserialized.Should().BeEquivalentTo(expected);
        }

    }

}
