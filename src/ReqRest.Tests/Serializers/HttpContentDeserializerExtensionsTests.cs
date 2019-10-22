namespace ReqRest.Tests.Serializers
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Moq;
    using ReqRest.Serializers;
    using ReqRest.Tests.Sdk.Data;
    using Xunit;
    using static ReqRest.Tests.Sdk.Data.ParameterNullability;

    public class HttpContentDeserializerExtensionsTests
    {

        public class DeserializeAsyncTests
        {

            private static readonly HttpContent EmptyContent = new StringContent("");

            [Theory, ArgumentNullExceptionData(NotNull)]
            public async Task Throws_ArgumentNullException(IHttpContentDeserializer deserializer)
            {
                var serializer = CreateSerializer<object>();
                await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                    await HttpContentDeserializerExtensions.DeserializeAsync<object>(deserializer, EmptyContent)
                );
            }

            [Fact]
            public async Task Throws_InvalidCastException_If_Content_Has_Different_Type()
            {
                var serializer = CreateSerializer<object>();
                await Assert.ThrowsAsync<InvalidCastException>(async () => await serializer.DeserializeAsync<int>(EmptyContent));
            }

            private IHttpContentDeserializer CreateSerializer<T>() where T : new()
            {
                var mock = new Mock<IHttpContentDeserializer>();

                mock.Setup(s => s.DeserializeAsync(null, typeof(T), CancellationToken.None)).Throws<ArgumentNullException>();

                mock.Setup(s => s.DeserializeAsync(
                    It.IsAny<HttpContent>(),
                    It.IsAny<Type>(),
                    It.IsAny<CancellationToken>()
                )).ReturnsAsync(new T());

                return mock.Object;
            }

        }

    }

}
