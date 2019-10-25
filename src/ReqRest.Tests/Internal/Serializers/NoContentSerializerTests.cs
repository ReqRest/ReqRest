namespace ReqRest.Tests.Internal.Serializers
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ReqRest.Http;
    using ReqRest.Internal.Serializers;
    using Xunit;

    public class NoContentSerializerTests
    {

        public class DeserializeTests
        {

            [Fact]
            public async Task Throws_NotSupportedException_When_Deserializing_Other_Type()
            {
                var serializer = new NoContentSerializer();
                await Assert.ThrowsAsync<NotSupportedException>(
                    async () => await serializer.DeserializeAsync(new ByteArrayContent(Array.Empty<byte>()), typeof(int))
                ).ConfigureAwait(false);
            }

            [Fact]
            public async Task Deserializes_NoContent()
            {
                var serializer = new NoContentSerializer();
                var deserialized = await serializer.DeserializeAsync(new ByteArrayContent(Array.Empty<byte>()), typeof(NoContent));
                Assert.IsType<NoContent>(deserialized);
            }

        }

    }

}
