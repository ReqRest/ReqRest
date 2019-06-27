namespace ReqRest.Serializers.NewtonsoftJson
{
    using System;
    using System.Net.Http;
    using System.Text;
    using ReqRest.Builders;
    using ReqRest.Http;

    /// <summary>
    ///     Extends the <see cref="IHttpRequestMessageBuilder"/> interface with methods for dealing
    ///     with JSON data.
    /// </summary>
    public static class JsonHttpContentBuilderExtensions
    {

        /// <summary>
        ///     Serializes the specified <paramref name="content"/> to an <see cref="HttpContent"/>
        ///     using the JSON format and then sets the <see cref="IHttpContentBuilder.Content"/>
        ///     to the new value.
        ///     This method uses the <c>application/x-www-form-urlencoded</c> media type for the new content.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The request builder.</param>
        /// <param name="content">The content to be sent with the request.</param>
        /// <param name="encoding">
        ///     The encoding to be used for converting the serialized <paramref name="content"/> to bytes.
        ///     If <see langword="null"/>, <see cref="Encoding.UTF8"/> is used.
        /// </param>
        /// <param name="serializer">
        ///     The <see cref="JsonHttpContentSerializer"/> to be used for the content serialization.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        public static T SetJsonFormContent<T>(
            this T builder,
            object? content,
            Encoding? encoding = null,
            JsonHttpContentSerializer? serializer = null) where T : IHttpContentBuilder
        {
            return builder
                .SetJsonContent(content, encoding, serializer)
                .SetContentType(
                    MediaType.ApplicationWwwFormEncoded,
                    builder.Content?.Headers?.ContentType?.CharSet,
                    builder.Content?.Headers?.ContentType?.Parameters
                );
        }

        /// <summary>
        ///     Serializes the specified <paramref name="content"/> to an <see cref="HttpContent"/>
        ///     using the JSON format and then sets the <see cref="IHttpContentBuilder.Content"/>
        ///     to the new value.
        ///     This method uses the <c>application/json</c> media type for the new content.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The request builder.</param>
        /// <param name="content">The content to be sent with the request.</param>
        /// <param name="encoding">
        ///     The encoding to be used for converting the serialized <paramref name="content"/> to bytes.
        ///     If <see langword="null"/>, <see cref="Encoding.UTF8"/> is used.
        /// </param>
        /// <param name="serializer">
        ///     The <see cref="JsonHttpContentSerializer"/> to be used for the content serialization.
        /// </param>
        /// <returns>The specified <paramref name="builder"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="builder"/>
        /// </exception>
        public static T SetJsonContent<T>(
            this T builder,
            object? content,
            Encoding? encoding = null,
            JsonHttpContentSerializer? serializer = null) where T : IHttpContentBuilder
        {
            serializer ??= JsonHttpContentSerializer.Default;
            var httpContent = serializer.Serialize(content, encoding);
            return builder.SetContent(httpContent);
        }

    }

}
