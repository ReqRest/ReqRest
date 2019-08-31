namespace ReqRest.Serializers.NewtonsoftJson
{
    using System;
    using System.Text;
    using ReqRest.Builders;

    /// <summary>
    ///     Extends the <see cref="IHttpRequestMessageBuilder"/> interface with methods for dealing
    ///     with JSON data.
    /// </summary>
    public static class JsonHttpContentBuilderExtensions
    {

        /// <summary>
        ///     Sets the HTTP content to a serialized JSON string representing <paramref name="content"/>.
        /// 
        ///     <paramref name="content"/> is serialized using the specified <paramref name="serializer"/>
        ///     and <paramref name="encoding"/>.
        ///     The serialized HTTP content has the <c>application/json</c> media type.
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
            // Validate builder here to stop a potential serialization.
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            serializer ??= JsonHttpContentSerializer.Default;
            
            var httpContent = serializer.Serialize(content, encoding);
            return builder.SetContent(httpContent);
        }

    }

}
