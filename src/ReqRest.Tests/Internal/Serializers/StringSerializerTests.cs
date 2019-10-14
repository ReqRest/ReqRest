namespace ReqRest.Tests.Internal.Serializers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using ReqRest.Internal.Serializers;
    using Xunit;
    using System.Net.Http;

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
                );
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
