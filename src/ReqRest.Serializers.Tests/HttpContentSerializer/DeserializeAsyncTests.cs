namespace ReqRest.Serializers.Tests.HttpContentSerializer
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using FluentAssertions;
    using ReqRest.Serializers;
    using Xunit;

    public class DeserializeAsyncTests
    {

        [Fact]
        public async Task Throws_ArgumentNullException_For_HttpContent()
        {
            var serializer = new MockedHttpContentSerializer();

            Func<Task> testCode = async () => await serializer.DeserializeAsync(null, typeof(object));
            await testCode.Should().ThrowAsync<ArgumentNullException>();
        }
        
        [Fact]
        public async Task Throws_ArgumentNullException_For_ContentType()
        {
            var serializer = new MockedHttpContentSerializer();
            var content = new ByteArrayContent(new byte[0]);

            Func<Task> testCode = async () => await serializer.DeserializeAsync(content, null);
            await testCode.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task Deserializes_NoContent()
        {
            var serializer = new MockedHttpContentSerializer();
            var content = new ByteArrayContent(new byte[0]);
            var result = await serializer.DeserializeAsync(content, typeof(NoContent));

            result.Should().BeOfType<NoContent>();
        }

        [Fact]
        public async Task Wraps_Thrown_Exceptions_In_HttpContentSerializationException()
        {
            var ex = new Exception("To be thrown...");
            var content = new ByteArrayContent(new byte[0]);
            var serializer = new MockedHttpContentSerializer()
            {
                DeserializeCoreImpl = (c, e) => throw ex,
            };

            Func<Task> testCode = async () => await serializer.DeserializeAsync(content, typeof(object));
            (await testCode.Should().ThrowAsync<HttpContentSerializationException>())
                .And.InnerException.Should().BeSameAs(ex);
        }

        [Fact]
        public async Task Doesnt_Wrap_HttpContentSerializationException()
        {
            var ex = new HttpContentSerializationException("To be thrown...");
            var content = new ByteArrayContent(new byte[0]);
            var serializer = new MockedHttpContentSerializer()
            {
                DeserializeCoreImpl = (c, e) => throw ex,
            };

            Func<Task> testCode = async () => await serializer.DeserializeAsync(content, typeof(object));
            (await testCode.Should().ThrowAsync<HttpContentSerializationException>())
                .Which.Should().BeSameAs(ex);
        }

    }

}
