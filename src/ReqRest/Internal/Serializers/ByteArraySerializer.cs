namespace ReqRest.Internal.Serializers
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using ReqRest.Resources;
    using ReqRest.Serializers;

    /// <summary>
    ///     A serializer for raw strings.
    ///     Fails to (de-)serialize any other type.
    /// </summary>
    internal sealed class ByteArraySerializer : HttpContentSerializer
    {

        /// <summary>
        ///     Gets a factory for a default <see cref="ByteArraySerializer"/> instance.
        /// </summary>
        public static Func<ByteArraySerializer> DefaultFactory { get; } = () => Default;

        /// <summary>
        ///     Gets a default <see cref="ByteArraySerializer"/> instance.
        /// </summary>
        public static ByteArraySerializer Default { get; } = new ByteArraySerializer();

        // This should never be called, because this class is internal.
        // If that ever happens, it's forced by the user.
        protected override HttpContent SerializeCore(object? content, Encoding encoding) =>
            throw new NotSupportedException();

        protected override async Task<object?> DeserializeCore(HttpContent httpContent, Type contentType)
        {
            if (contentType != typeof(byte[]))
            {
                throw new NotSupportedException(ExceptionStrings.Serializer_CanOnlyDeserialize(typeof(byte[])));
            }
            return await httpContent.ReadAsByteArrayAsync().ConfigureAwait(false);
        }
    }

}
