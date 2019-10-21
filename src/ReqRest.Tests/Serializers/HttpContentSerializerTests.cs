namespace ReqRest.Tests.Serializers
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Moq;
    using ReqRest.Http;
    using ReqRest.Serializers;
    using ReqRest.Tests.Sdk.Data;
    using ReqRest.Tests.Sdk.TestBases;
    using static ReqRest.Tests.Sdk.Data.ParameterNullability;
    using Xunit;

    public abstract class HttpContentSerializerTests : TestBase<HttpContentSerializer>
    {

        protected override HttpContentSerializer CreateService()
        {
            return CreateService(null, null);
        }

        protected HttpContentSerializer CreateService(
            Func<object?, Type?, Encoding?, HttpContent?>? serializeCore = null,
            Func<HttpContent, Type, CancellationToken, Task<object?>>? deserializeAsyncCore = null)
        {
            var mock = new Mock<HttpContentSerializer>() { CallBase = true };

            if (!(serializeCore is null))
            {
                mock.Setup(x => x.SerializeCore(It.IsAny<object?>(), It.IsAny<Type?>(), It.IsAny<Encoding>()))
                    .Returns(serializeCore);
            }

            if (!(deserializeAsyncCore is null))
            {
                mock.Setup(x => x.DeserializeAsyncCore(It.IsAny<HttpContent>(), It.IsAny<Type>(), It.IsAny<CancellationToken>()))
                    .Returns(deserializeAsyncCore);
            }

            return mock.Object;
        }

        public class DefaultEncodingTests : HttpContentSerializerTests
        {

            [Fact]
            public void Uses_UTF8_As_Default_Encoding()
            {
                Assert.Same(Encoding.UTF8, Service.DefaultEncoding);
            }

        }

        public class SerializeTests : HttpContentSerializerTests
        {

            [Fact]
            public void Serializes_NoContent_To_Null()
            {
                var httpContent = Service.Serialize(new NoContent(), encoding: null);
                Assert.Null(httpContent);
            }

            [Fact]
            public void Passes_DefaultEncoding_To_SerializeCore_If_Encoding_Is_Null()
            {
                Encoding? passedEncoding = null;
                var service = CreateService(
                    serializeCore: (content, type, encoding) => { passedEncoding = encoding; return null; }
                );

                service.Serialize(null, contentType: null, encoding: null);
                Assert.Same(service.DefaultEncoding, passedEncoding);
            }

            [Fact]
            public void Wraps_Thrown_Exceptions_In_HttpContentSerializationException()
            {
                var ex = new Exception("To be thrown...");
                var service = CreateService(
                    serializeCore: (content, type, encoding) => throw ex
                );
                
                var thrown = Record.Exception(() => service.Serialize(null, contentType: null, encoding: null));

                Assert.IsType<HttpContentSerializationException>(thrown);
                Assert.Same(ex, thrown.InnerException);
            }

            [Fact]
            public void Doesnt_Wrap_HttpContentSerializationException()
            {
                var ex = new HttpContentSerializationException("To be thrown...");
                var service = CreateService(
                    serializeCore: (content, type, encoding) => throw ex
                );

                var thrown = Record.Exception(() => service.Serialize(null, contentType: null, encoding: null));
                Assert.Same(ex, thrown);
            }

        }

        public class DeserializeAsyncTests : HttpContentSerializerTests
        {

            [Theory, ArgumentNullExceptionData(NotNull)]
            public async Task Throws_ArgumentNullException_For_ContentType(Type contentType)
            {
                var content = new ByteArrayContent(Array.Empty<byte>());
                await Assert.ThrowsAsync<ArgumentNullException>(
                    async () => await Service.DeserializeAsync(content, contentType)
                );
            }

            [Fact]
            public async Task Deserializes_NoContent_From_Empty_Content()
            {
                var content = new ByteArrayContent(Array.Empty<byte>());
                var result = await Service.DeserializeAsync(content, typeof(NoContent));
                Assert.IsType<NoContent>(result);
            }

            [Fact]
            public async Task Deserializes_NoContent_From_Null()
            {
                var result = await Service.DeserializeAsync(null, typeof(NoContent));
                Assert.IsType<NoContent>(result);
            }

            [Fact]
            public async Task Wraps_Thrown_Exceptions_In_HttpContentSerializationException()
            {
                var ex = new Exception("To be thrown...");
                var content = new ByteArrayContent(Array.Empty<byte>());
                var serializer = CreateService(
                    deserializeAsyncCore: (httpContent, contentType, encoding) => throw ex
                );

                var thrown = await Record.ExceptionAsync(async () => await serializer.DeserializeAsync(content, typeof(object)));
                Assert.IsType<HttpContentSerializationException>(thrown);
                Assert.Same(ex, thrown.InnerException);
            }

            [Fact]
            public async Task Doesnt_Wrap_HttpContentSerializationException()
            {
                var ex = new HttpContentSerializationException("To be thrown...");
                var content = new ByteArrayContent(Array.Empty<byte>());
                var serializer = CreateService(
                    deserializeAsyncCore: (httpContent, contentType, encoding) => throw ex
                );

                var thrown = await Record.ExceptionAsync(async () => await serializer.DeserializeAsync(content, typeof(object)));
                Assert.Same(ex, thrown);
            }

        }

    }

}
