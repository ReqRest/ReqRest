namespace ReqRest.Client.Tests.NoContentSerializer
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using FluentAssertions;
    using ReqRest.Client;
    using ReqRest.Serializers;
    using Xunit;

    public class DeserializeTests
    {

        [Fact]
        public async Task Throws_InvalidOperationException_When_Deserializing_Other_Type()
        {
            var serializer = new NoContentSerializer();
            Func<Task> testCode = async () => await serializer.DeserializeAsync(new ByteArrayContent(Array.Empty<byte>()), typeof(int));
            (await testCode.Should().ThrowAsync<HttpContentSerializationException>()).WithInnerException<InvalidOperationException>();
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
