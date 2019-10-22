namespace ReqRest.Serializers
{
    using System;
    using System.Net.Http;
    using System.Threading;
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
        /// <param name="deserializer">The serializer.</param>
        /// <param name="httpContent">
        ///     An <see cref="HttpContent"/> instance from which the content should be serialized.
        ///     This can be <see langword="null"/>.
        /// </param>
        /// <param name="cancellationToken">
        ///     A cancellation token which can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     An object of type <typeparamref name="T"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="deserializer"/>
        /// </exception>
        /// <exception cref="InvalidCastException">
        ///     The serializer returned an object which is not of type <typeparamref name="T"/>.
        /// </exception>
        /// <exception cref="HttpContentSerializationException">
        ///     Deserializing the content failed.
        /// </exception>
        /// <exception cref="TaskCanceledException">
        ///     The operation was canceled via the <paramref name="cancellationToken"/>.
        /// </exception>
        public static async Task<T> DeserializeAsync<T>(
            this IHttpContentDeserializer deserializer, 
            HttpContent? httpContent, 
            CancellationToken cancellationToken = default)
        {
#nullable disable
            _ = deserializer ?? throw new ArgumentNullException(nameof(deserializer));
            return (T)await deserializer.DeserializeAsync(httpContent, typeof(T), cancellationToken).ConfigureAwait(false);
#nullable restore
        }

    }

}
