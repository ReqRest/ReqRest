namespace ReqRest.Serializers.NewtonsoftJson
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using ReqRest.Http;

    /// <summary>
    ///     An <see cref="HttpContent"/> (de-)serializer which uses the <c>Newtonsoft.Json</c>
    ///     library for the (de-)serialization process.
    ///     
    ///     Consider using the <see cref="Default"/> property for accessing a default 
    ///     <see cref="JsonHttpContentSerializer"/> instance if you don't require any specific JSON
    ///     serialization settings.
    /// </summary>
    public class JsonHttpContentSerializer : HttpContentSerializer
    {

        private static readonly JsonSerializer s_defaultJsonSerializer = new JsonSerializer();

        /// <summary>
        ///     Gets a default <see cref="JsonHttpContentSerializer"/> instance which internally
        ///     uses a default <see cref="Newtonsoft.Json.JsonSerializer"/> instance.
        ///     
        ///     This is an otherwise default <see cref="Newtonsoft.Json.JsonSerializer"/> whose
        ///     <see cref="JsonSerializer.NullValueHandling"/> is set to <see cref="NullValueHandling.Ignore"/>.
        /// </summary>
        public static JsonHttpContentSerializer Default { get; } = new JsonHttpContentSerializer();

        /// <summary>
        ///     Gets or sets the <see cref="Newtonsoft.Json.JsonSerializer"/> to be used for
        ///     (de-)serializing the resource objects.
        ///     This can be <see langword="null"/>. If so, a default <see cref="Newtonsoft.Json.JsonSerializer"/>
        ///     instance is used for (de-)serialization.
        ///     This is an otherwise default <see cref="Newtonsoft.Json.JsonSerializer"/> which
        ///     ignores <c>null</c> values during (de-)serialization.
        /// </summary>
        protected JsonSerializer? JsonSerializer { get; }

        private JsonSerializer ActualJsonSerializer => JsonSerializer ?? s_defaultJsonSerializer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonHttpContentSerializer"/> with
        ///     an optional <see cref="Newtonsoft.Json.JsonSerializer"/> to be used
        ///     for the (de-)serialization.
        /// </summary>
        /// <param name="jsonSerializer">
        ///     The <see cref="Newtonsoft.Json.JsonSerializer"/> to be used for
        ///     (de-)serializing the resource objects.
        ///     
        ///     If <see langword="null"/>, a default <see cref="Newtonsoft.Json.JsonSerializer"/>
        ///     instance is used for (de-)serialization.
        ///     This is an otherwise default <see cref="Newtonsoft.Json.JsonSerializer"/> which
        ///     ignores <c>null</c> values during (de-)serialization.
        /// </param>
        public JsonHttpContentSerializer(JsonSerializer? jsonSerializer = null)
        {
            JsonSerializer = jsonSerializer ?? s_defaultJsonSerializer;
        }

        /// <summary>
        ///     Returns either <see cref="Default"/> (if <paramref name="jsonSerializer"/> is <see langword="null"/>)
        ///     or a new <see cref="JsonHttpContentSerializer"/> instance.
        ///     Used internally so that no new instance has to be created all the time if no serializer is given.
        /// </summary>
        internal static JsonHttpContentSerializer FromJsonSerializer(JsonSerializer? jsonSerializer)
        {
            return jsonSerializer is null
                ? Default
                : new JsonHttpContentSerializer(jsonSerializer);
        }

        /// <inheritdoc/>
        protected override HttpContent? SerializeCore(object? content, Type? contentType, Encoding encoding)
        {
            using var stringWriter = new StringWriter();
            using var jsonWriter = new JsonTextWriter(stringWriter);
            ActualJsonSerializer.Serialize(jsonWriter, content, contentType);
            return new StringContent(stringWriter.ToString(), encoding, MediaType.ApplicationJson);
        }

        /// <inheritdoc/>
        protected override async Task<object?> DeserializeCore(
            HttpContent httpContent, Type contentType, CancellationToken cancellationToken)
        {
            var json = await httpContent.ReadAsStringAsync().ConfigureAwait(false);
            using var stringReader = new StringReader(json);
            using var jsonReader = new JsonTextReader(stringReader);
            return ActualJsonSerializer.Deserialize(jsonReader, contentType);
        }

    }

}
