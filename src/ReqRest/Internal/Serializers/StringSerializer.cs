namespace ReqRest.Internal.Serializers
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     A serializer for raw strings. Fails to (de-)serialize any other type.
    /// </summary>
    internal sealed class StringSerializer : SpecificHttpContentDeserializer<string>
    {

        public static Func<StringSerializer> DefaultFactory { get; } = () => Default;

        public static StringSerializer Default { get; } = new StringSerializer();

        protected override string DefaultValue => string.Empty;

        protected override Task<string> DeserializeCoreAsync(HttpContent httpContent, CancellationToken cancellationToken) =>
            httpContent.ReadAsStringAsync();

    }

}
