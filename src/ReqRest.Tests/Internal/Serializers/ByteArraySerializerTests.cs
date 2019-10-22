namespace ReqRest.Tests.Internal.Serializers
{
    using System;
    using System.Threading.Tasks;
    using Xunit;
    using ReqRest.Internal.Serializers;
    using System.Net.Http;

    public class ByteArraySerializerTests
    {

        public class DeserializeTests
        {

            [Fact]
            public async Task Throws_NotSupportedException_When_Deserializing_Other_Type()
            {
                var serializer = new ByteArraySerializer();
                await Assert.ThrowsAsync<NotSupportedException>(
                    async () => await serializer.DeserializeAsync(new ByteArrayContent(Array.Empty<byte>()), typeof(int))
                ).ConfigureAwait(false);
            }

            [Fact]
            public async Task Deserializes_ByteArray()
            {
                var expected = new byte[] { 1, 2, 3 };
                var serializer = new ByteArraySerializer();
                var deserialized = await serializer.DeserializeAsync(new ByteArrayContent(expected), typeof(byte[]));
                Assert.IsType<byte[]>(deserialized);
                Assert.Equal(expected, deserialized);
            }

        }

    }

}
