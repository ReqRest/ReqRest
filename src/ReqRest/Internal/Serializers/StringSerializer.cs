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
    internal sealed class StringSerializer : HttpContentSerializer
    {

        /// <summary>
        ///     Gets a factory for a default <see cref="StringSerializer"/> instance.
        /// </summary>
        public static Func<StringSerializer> DefaultFactory { get; } = () => Default;

        /// <summary>
        ///     Gets a default <see cref="StringSerializer"/> instance.
        /// </summary>
        public static StringSerializer Default { get; } = new StringSerializer();

        // This should never be called, because this class is internal.
        // If that ever happens, it's forced by the user.
        protected override HttpContent SerializeCore(object? content, Encoding encoding) =>
            throw new NotSupportedException();

        protected override async Task<object?> DeserializeCore(HttpContent httpContent, Type contentType)
        {
            if (contentType != typeof(string))
            {
                throw new NotSupportedException(ExceptionStrings.Serializer_CanOnlyDeserialize(typeof(string)));
            }
            return await httpContent.ReadAsStringAsync().ConfigureAwait(false);
        }

    }

}
