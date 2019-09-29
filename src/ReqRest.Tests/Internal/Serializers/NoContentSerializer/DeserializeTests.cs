namespace ReqRest.Tests.Internal.Serializers.NoContentSerializer
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using FluentAssertions;
    using ReqRest.Http;
    using ReqRest.Internal.Serializers;
    using ReqRest.Serializers;
    using Xunit;

    public class DeserializeTests
    {

        [Fact]
        public async Task Throws_NotSupportedException_When_Deserializing_Other_Type()
        {
            var serializer = new NoContentSerializer();
            Func<Task> testCode = async () => await serializer.DeserializeAsync(new ByteArrayContent(Array.Empty<byte>()), typeof(int));
            await testCode.Should().ThrowAsync<NotSupportedException>();
        }

        [Fact]
        public async Task Deserializes_NoContent()
        {
            var serializer = new NoContentSerializer();
            var deserialized = await serializer.DeserializeAsync(new ByteArrayContent(Array.Empty<byte>()), typeof(NoContent));
            deserialized.Should().BeOfType<NoContent>();
        }

    }

}
