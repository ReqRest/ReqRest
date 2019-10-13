namespace ReqRest.Tests.Internal.Serializers.ByteArraySerializer
{
    using System;
    using System.Threading.Tasks;
    using ReqRest.Internal.Serializers;
    using Xunit;
    using FluentAssertions;
    using System.Net.Http;

    public class DeserializeTests
    {

        [Fact]
        public async Task Throws_NotSupportedException_When_Deserializing_Other_Type()
        {
            var serializer = new ByteArraySerializer();
            Func<Task> testCode = async () => await serializer.DeserializeAsync(new ByteArrayContent(Array.Empty<byte>()), typeof(int));
            await testCode.Should().ThrowAsync<NotSupportedException>();
        }

        [Fact]
        public async Task Deserializes_ByteArray()
        {
            var expected = new byte[] { 1, 2, 3 };
            var serializer = new ByteArraySerializer();
            var deserialized = await serializer.DeserializeAsync(new ByteArrayContent(expected), typeof(byte[]));
            deserialized.Should().BeOfType<byte[]>();
            deserialized.Should().BeEquivalentTo(expected);
        }

    }

}
