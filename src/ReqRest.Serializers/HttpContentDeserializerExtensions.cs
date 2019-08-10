namespace ReqRest.Serializers
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    ///     Extends the <see cref="IHttpContentDeserializer"/> with default extension methods.
    /// </summary>
    public static class HttpContentDeserializerExtensions
    {

        /// <summary>
        ///     Deserializes an object of the specified type <typeparamref name="T"/> from
        ///     the <paramref name="httpContent"/>.
        /// </summary>
        /// <typeparam name="T">
        ///     The target type of the object which is supposed to be deserialized.
        /// </typeparam>
        /// <param name="serializer">The serializer.</param>
        /// <param name="httpContent">
        ///     An <see cref="HttpContent"/> instance from which the content should be serialized.
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <returns>
        ///     An object of type <typeparamref name="T"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="serializer"/>
        ///     * <paramref name="httpContent"/>
        /// </exception>
        /// <exception cref="InvalidCastException">
        ///     The serializer returned an object which is not of type <typeparamref name="T"/>.
        /// </exception>
        /// <exception cref="HttpContentSerializationException">
        ///     Deserializing the content failed.
        /// </exception>
        public static async Task<T> DeserializeAsync<T>(this IHttpContentDeserializer serializer, HttpContent? httpContent)
        {
#nullable disable
            _ = serializer ?? throw new ArgumentNullException(nameof(serializer));
            return (T)await serializer.DeserializeAsync(httpContent, typeof(T)).ConfigureAwait(false);
#nullable restore
        }

    }

}
