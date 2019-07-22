namespace ReqRest.Serializers.Tests.HttpContentSerializer
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using ReqRest.Serializers;

    public class MockedHttpContentSerializer : HttpContentSerializer
    {

        public new Encoding DefaultEncoding => base.DefaultEncoding;

        public Func<object, Encoding, HttpContent> SerializeCoreImpl { get; set; }

        public Func<HttpContent, Type, Task<object>> DeserializeCoreImpl { get; set; }

        protected override HttpContent SerializeCore(object content, Encoding encoding)
        {
            return SerializeCoreImpl is null
                ? throw new NotImplementedException()
                : SerializeCoreImpl(content, encoding);
        }

        protected override Task<object> DeserializeCore(HttpContent httpContent, Type contentType)
        {
            return DeserializeCoreImpl is null
                ? throw new NotImplementedException()
                : DeserializeCoreImpl(httpContent, contentType);
        }

    }

}
