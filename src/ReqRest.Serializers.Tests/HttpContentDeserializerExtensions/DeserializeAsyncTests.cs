namespace ReqRest.Serializers.Tests.HttpContentSerializerExtensions
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Moq;
    using ReqRest.Serializers;
    using Xunit;

    public class DeserializeAsyncTests
    {

        private static readonly HttpContent EmptyContent = new ByteArrayContent(new byte[0]);

        [Fact]
        public async Task Throws_ArgumentNullException_For_Serializer()
        {
            var serializer = CreateSerializer<object>();
            Func<Task> testCode = async () => 
                await HttpContentDeserializerExtensions.DeserializeAsync<object>(null, EmptyContent);
            await testCode.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task Throws_InvalidCastException_If_Content_Has_Different_Type()
        {
            var serializer = CreateSerializer<object>();
            Func<Task> testCode = async () => await serializer.DeserializeAsync<int>(EmptyContent);
            await testCode.Should().ThrowAsync<InvalidCastException>();
        }

        private IHttpContentDeserializer CreateSerializer<T>() where T : new()
        {
            var mock = new Mock<IHttpContentDeserializer>();

            mock.Setup(s => s.DeserializeAsync(null, typeof(T))).Throws<ArgumentNullException>();

            mock.Setup(s => s.DeserializeAsync(
                It.IsAny<HttpContent>(),
                It.IsAny<Type>()
            )).ReturnsAsync(new T());

            return mock.Object;
        }

    }

}
