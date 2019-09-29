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
        /// <typeparam name="TBuilder">The type of the builder.</typeparam>
        /// <typeparam name="TContent">The type of the content to be serialized.</typeparam>
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
        /// <exception cref="ArgumentException">
        ///     <paramref name="content"/> is not a subclass of <paramref name="contentType"/>, i.e.
        ///     the two types do not match.
        /// </exception>
        /// <exception cref="HttpContentSerializationException">
        ///     Serializing the <paramref name="content"/> failed.
        /// </exception>
        public static TBuilder SetJsonContent<TBuilder, TContent>(
            this TBuilder builder,
            TContent content,
            Encoding? encoding = null,
            JsonHttpContentSerializer? serializer = null) where TBuilder : IHttpContentBuilder
        {
            return builder.SetJsonContent(content, typeof(TContent), encoding, serializer);
        }

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
        /// <param name="contentType">
        ///     The type of the specified <paramref name="content"/>.
        ///     This can be declared to give the serializer additional information about how
        ///     <paramref name="content"/> should be serialized.
        ///     
        ///     This can be <see langword="null"/>. If so, the serializer will try to determine the
        ///     type on its own. If <paramref name="content"/> is also <see langword="null"/>, the
        ///     serializer will use default <see langword="null"/> value handling.
        /// </param>
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
        /// <exception cref="ArgumentException">
        ///     <paramref name="content"/> is not a subclass of <paramref name="contentType"/>, i.e.
        ///     the two types do not match.
        /// </exception>
        /// <exception cref="HttpContentSerializationException">
        ///     Serializing the <paramref name="content"/> failed.
        /// </exception>
        public static T SetJsonContent<T>(
            this T builder,
            object? content,
            Type? contentType = null,
            Encoding? encoding = null,
            JsonHttpContentSerializer? serializer = null) where T : IHttpContentBuilder
        {
            // Validate builder here to stop a potential serialization.
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            serializer ??= JsonHttpContentSerializer.Default;
            
            var httpContent = serializer.Serialize(content, contentType, encoding);
            return builder.SetContent(httpContent);
        }

    }

}
