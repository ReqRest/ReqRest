namespace ReqRest.Serializers.Json
{
    using System;
    using System.Threading.Tasks;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Text.Json;
    using System.Net.Http.Headers;
    using ReqRest.Http;

    /// <summary>
    ///     An <see cref="HttpContent"/> (de-)serializer which uses .NET's <see cref="System.Text.Json"/>
    ///     API for (de-)serializing objects to and from JSON.
    /// </summary>
    public class JsonHttpContentSerializer : HttpContentSerializer
    {

        /// <summary>
        ///     Gets or sets the options which should be used for (de-)serializing objects
        ///     to and from JSON.
        ///     
        ///     This can be <see langword="null"/>. If so, default options will be used.
        /// </summary>
        public JsonSerializerOptions? JsonSerializerOptions { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonHttpContentSerializer"/> class.
        /// </summary>
        /// <param name="jsonSerializerOptions">
        ///     The options which should be used for (de-)serializing objects
        ///     to and from JSON.
        ///     
        ///     This can be <see langword="null"/>. If so, default options will be used.
        /// </param>
        public JsonHttpContentSerializer(JsonSerializerOptions? jsonSerializerOptions = null)
        {
            JsonSerializerOptions = jsonSerializerOptions;
        }

        /// <inheritdoc/>
        protected override HttpContent? SerializeCore(object? content, Type? contentType, Encoding encoding)
        {
            // Since .NET's Json members are optimized for UTF-8, it makes sense to use these
            // optimizations if that's the encoding as well.
            if (encoding == Encoding.UTF8)
            {
                return SerializeUtf8(content, contentType);
            }
            else
            {
                return SerializeOtherEncoding(content, contentType, encoding);
            }
        }

        private HttpContent? SerializeUtf8(object? content, Type? contentType)
        {
            var bytes = JsonSerializer.SerializeToUtf8Bytes(content, contentType, JsonSerializerOptions);
            var httpContent = new ByteArrayContent(bytes);
            var contentTypeHeader = new MediaTypeHeaderValue(MediaType.ApplicationJson)
            {
                CharSet = Encoding.UTF8.WebName,
            };

            httpContent.Headers.ContentType = contentTypeHeader;
            return httpContent;
        }

        private HttpContent? SerializeOtherEncoding(object? content, Type? contentType, Encoding encoding)
        {
            var str = JsonSerializer.Serialize(content, contentType, JsonSerializerOptions);
            return new StringContent(str, encoding, MediaType.ApplicationJson);
        }

        /// <inheritdoc/>
        protected override async Task<object?> DeserializeAsyncCore(
            HttpContent httpContent, Type contentType, CancellationToken cancellationToken)
        {
            // While we'd ideally read directly from the stream here (if UTF-8), this is a lot of messy work.
            // For one, we don't know if the encoding is UTF-8 and finding this out is hard to do
            // with tasks like checking the BOM and charset format.
            // As a reference, check out https://github.com/dotnet/corefx/blob/7e9a177824cbefaee8985a9b517ebb0ea2e17a81/src/System.Net.Http/src/System/Net/Http/HttpContent.cs#L604
            // For the moment, simply fall back to simple string deserialization and optimize this later,
            // if required.
            // If this is an absolute must, people can still derive from this class and override the method.
            var json = await httpContent.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize(json, contentType, JsonSerializerOptions);
        }

    }

}
