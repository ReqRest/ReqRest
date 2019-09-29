namespace ReqRest.Tests.Shared
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using ReqRest.Serializers;

    public class MockedHttpContentSerializer : HttpContentSerializer
    {

        public new Encoding DefaultEncoding => base.DefaultEncoding;

        public Func<object, Encoding, HttpContent> SerializeCoreImpl { get; set; }

        public Func<HttpContent, Type, Task<object>> DeserializeCoreImpl { get; set; }

        public MockedHttpContentSerializer(
            Func<object, Encoding, HttpContent> serializeCoreImpl = null,
            Func<HttpContent, Type, Task<object>> deserializeCoreImpl = null)
        {
            SerializeCoreImpl = serializeCoreImpl;
            DeserializeCoreImpl = deserializeCoreImpl;
        }

        protected override HttpContent SerializeCore(object content, Type type, Encoding encoding)
        {
            return SerializeCoreImpl is null
                ? throw new NotImplementedException()
                : SerializeCoreImpl(content, encoding);
        }

        protected override Task<object> DeserializeAsyncCore(
            HttpContent httpContent, Type contentType, CancellationToken cancellationToken)
        {
            return DeserializeCoreImpl is null
                ? throw new NotImplementedException()
                : DeserializeCoreImpl(httpContent, contentType);
        }

    }

}
