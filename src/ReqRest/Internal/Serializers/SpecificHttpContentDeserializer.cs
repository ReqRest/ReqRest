namespace ReqRest.Internal.Serializers
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using ReqRest.Resources;
    using ReqRest.Serializers;

    /// <summary>
    ///     Internally used for deserializers that are able to serialize only one specific type,
    ///     e.g. the <see cref="StringSerializer"/>.
    /// </summary>
    /// <typeparam name="T">The type to be deserialized.</typeparam>
    internal abstract class SpecificHttpContentDeserializer<T> : IHttpContentDeserializer
    {

        /// <summary>
        ///     Gets the value to be returned if the provided <see cref="HttpContent"/> is
        ///     <see langword="null"/>.
        /// </summary>
        protected abstract T DefaultValue { get; }

        /// <inheritdoc/>
        public async Task<object?> DeserializeAsync(
            HttpContent? httpContent, Type contentType, CancellationToken cancellationToken = default)
        {
            _ = contentType ?? throw new ArgumentNullException(nameof(contentType));

            if (contentType != typeof(T))
            {
                throw new NotSupportedException(ExceptionStrings.Serializer_CanOnlyDeserialize(typeof(string)));
            }

            if (httpContent is null)
            {
                return DefaultValue;
            }

            return await DeserializeCoreAsync(httpContent, cancellationToken).ConfigureAwait(false);
        }

        protected abstract Task<T> DeserializeCoreAsync(HttpContent httpContent, CancellationToken cancellationToken);

    }

}
