namespace ReqRest.Serializers
{
    using System;
    using System.Net.Http;
    using System.Text;

    /// <summary>
    ///     Extends the <see cref="IHttpContentSerializer"/> with default extension methods.
    /// </summary>
    public static class HttpContentSerializerExtensions
    {

        /// <summary>
        ///     Serializes the specified <paramref name="content"/> into a new
        ///     <see cref="HttpContent"/> instance.
        ///     
        ///     This method passes the type type of the generic type parameter <typeparamref name="T"/>
        ///     as the content type to the serializer.
        /// </summary>
        /// <typeparam name="T">
        ///     The target type of the object which is supposed to be serialized.
        /// </typeparam>
        /// <param name="serializer">The serializer.</param>
        /// <param name="content">
        ///     The object to be serialized into a new <see cref="HttpContent"/> instance.
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <param name="encoding">
        ///     An optional encoding to be used by the serializer if it serializes the <paramref name="content"/>
        ///     to an <see cref="HttpContent"/> which requires one.
        ///     If <see langword="null"/>, a default encoding is used.
        /// </param>
        /// <returns>
        ///     A new <see cref="HttpContent"/> instance which holds the serialized <paramref name="content"/>
        ///     or <see langword="null"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="serializer"/>
        /// </exception>
        /// <exception cref="HttpContentSerializationException">
        ///     Serializing the <paramref name="content"/> failed.
        /// </exception>
        public static HttpContent? Serialize<T>(this IHttpContentSerializer serializer, T content, Encoding? encoding)
        {
            _ = serializer ?? throw new ArgumentNullException(nameof(serializer));
            return serializer.Serialize(content, typeof(T), encoding);
        }

    }

}
