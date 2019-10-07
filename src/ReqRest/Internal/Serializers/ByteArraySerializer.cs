namespace ReqRest.Internal.Serializers
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     A serializer for raw bytes. Fails to (de-)serialize any other type.
    /// </summary>
    internal sealed class ByteArraySerializer : SpecificHttpContentDeserializer<byte[]>
    {

        public static Func<ByteArraySerializer> DefaultFactory { get; } = () => Default;

        public static ByteArraySerializer Default { get; } = new ByteArraySerializer();

        protected override byte[] DefaultValue => Array.Empty<byte>();

        protected override Task<byte[]> DeserializeCoreAsync(HttpContent httpContent, CancellationToken cancellationToken) =>
            httpContent.ReadAsByteArrayAsync();

    }

}
