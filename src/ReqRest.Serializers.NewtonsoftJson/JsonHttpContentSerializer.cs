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
    /// </summary>
    public class JsonHttpContentSerializer : HttpContentSerializer
    {

        private static readonly JsonSerializer DefaultJsonSerializer = new JsonSerializer();

        /// <summary>
        ///     Gets a factory function returning the <see cref="Default"/> value.
        /// </summary>
        internal static Func<JsonHttpContentSerializer> DefaultFactory { get; } = () => Default;

        /// <summary>
        ///     Gets a default <see cref="JsonHttpContentSerializer"/> instance which is used
        ///     internally if no specific <see cref="Newtonsoft.Json.JsonSerializer"/> is required
        ///     for a serialization task.
        /// </summary>
        internal static JsonHttpContentSerializer Default { get; } = new JsonHttpContentSerializer();

        /// <summary>
        ///     Gets or sets the <see cref="Newtonsoft.Json.JsonSerializer"/> to be used for
        ///     (de-)serializing the resource objects.
        ///     
        ///     This can be <see langword="null"/>. If so, a default <see cref="Newtonsoft.Json.JsonSerializer"/>
        ///     instance is used for (de-)serialization.
        /// </summary>
        public JsonSerializer? JsonSerializer { get; set; }

        private JsonSerializer ActualJsonSerializer => JsonSerializer ?? DefaultJsonSerializer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonHttpContentSerializer"/> class
        ///     which uses a default <see cref="JsonSerializer"/>.
        /// </summary>
        public JsonHttpContentSerializer()
            : this(null) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonHttpContentSerializer"/> class
        ///     whose <see cref="JsonSerializer"/> is set to a new instance created from the specified
        ///     <paramref name="settings"/>.
        /// </summary>
        /// <param name="settings">
        ///     The settings from which a new <see cref="JsonSerializer"/> should be created.
        ///     
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <param name="useDefaultSettings">
        ///     If <see langword="true"/>, the serializer created from the settings will use default
        ///     settings from <see cref="JsonConvert.DefaultSettings"/>.
        ///     If <see langword="false"/>, that is not the case. 
        /// </param>
        public JsonHttpContentSerializer(JsonSerializerSettings? settings, bool useDefaultSettings = false)
            : this(useDefaultSettings ? JsonSerializer.CreateDefault(settings) : JsonSerializer.Create(settings)) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonHttpContentSerializer"/> class with
        ///     an optional <see cref="Newtonsoft.Json.JsonSerializer"/> to be used
        ///     for the (de-)serialization.
        /// </summary>
        /// <param name="jsonSerializer">
        ///     The <see cref="Newtonsoft.Json.JsonSerializer"/> to be used for
        ///     (de-)serializing the resource objects.
        ///     
        ///     If <see langword="null"/>, a default <see cref="Newtonsoft.Json.JsonSerializer"/>
        ///     instance is used for (de-)serialization.
        /// </param>
        public JsonHttpContentSerializer(JsonSerializer? jsonSerializer)
        {
            JsonSerializer = jsonSerializer ?? DefaultJsonSerializer;
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
        protected override async Task<object?> DeserializeAsyncCore(
            HttpContent httpContent, Type contentType, CancellationToken cancellationToken)
        {
            var json = await httpContent.ReadAsStringAsync().ConfigureAwait(false);
            using var stringReader = new StringReader(json);
            using var jsonReader = new JsonTextReader(stringReader);
            return ActualJsonSerializer.Deserialize(jsonReader, contentType);
        }

    }

}
