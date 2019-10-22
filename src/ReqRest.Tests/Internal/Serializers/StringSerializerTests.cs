namespace ReqRest.Tests.Internal.Serializers
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ReqRest.Internal.Serializers;
    using Xunit;

    public class StringSerializerTests
    {

        public class DeserializeTests
        {

            [Fact]
            public async Task Throws_NotSupportedException_When_Deserializing_Other_Type()
            {
                var serializer = new StringSerializer();
                await Assert.ThrowsAsync<NotSupportedException>(
                    async () => await serializer.DeserializeAsync(new ByteArrayContent(Array.Empty<byte>()), typeof(int))
                ).ConfigureAwait(false);
            }

            [Fact]
            public async Task Deserializes_String()
            {
                var expected = "Hello World";
                var serializer = new StringSerializer();
                var deserialized = await serializer.DeserializeAsync(new StringContent(expected), typeof(string));
                Assert.IsType<string>(deserialized);
                Assert.Equal(expected, deserialized);
            }

        }

    }

}
