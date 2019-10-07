namespace ReqRest.Internal.Serializers
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using ReqRest.Http;

    /// <summary>
    ///     A serializer for the special <see cref="NoContent"/> type. Fails to (de-)serialize any other type.
    /// </summary>
    internal sealed class NoContentSerializer : SpecificHttpContentDeserializer<NoContent>
    {

        private static readonly Task<NoContent> CompletedNoContent = Task.FromResult(new NoContent());

        public static Func<NoContentSerializer> DefaultFactory { get; } = () => Default;

        public static NoContentSerializer Default { get; } = new NoContentSerializer();

        protected override NoContent DefaultValue => new NoContent();

        protected override Task<NoContent> DeserializeCoreAsync(HttpContent httpContent, CancellationToken cancellationToken) =>
            CompletedNoContent;
        // Analogous to the default HttpContentSerializer behavior, ignore cases where the
        // httpContent is actually NOT empty.

    }

}
