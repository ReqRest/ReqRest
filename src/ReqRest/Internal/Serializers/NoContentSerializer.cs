namespace ReqRest.Internal.Serializers
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using ReqRest.Serializers;

    /// <summary>
    ///     A serializer for the special <see cref="NoContent"/> type.
    ///     Fails to (de-)serialize any other type.
    /// </summary>
    internal sealed class NoContentSerializer : HttpContentSerializer
    {
        // HttpContentSerializer already handles NoContent. Nothing left to do.

        /// <summary>
        ///     Gets a factory for a default <see cref="NoContentSerializer"/> instance.
        /// </summary>
        public static Func<NoContentSerializer> DefaultFactory { get; } = () => Default;

        /// <summary>
        ///     Gets a default <see cref="NoContentSerializer"/> instance.
        /// </summary>
        public static NoContentSerializer Default { get; } = new NoContentSerializer();

        // These two should never be called, because the base class handles the NoContent serialization.
        // If we ever get here, it was forced by the user through some tricks.
        protected override Task<object?> DeserializeCore(HttpContent httpContent, Type contentType) =>
            throw new NotSupportedException();

        protected override HttpContent SerializeCore(object? content, Encoding encoding) =>
            throw new NotSupportedException();

    }

}
