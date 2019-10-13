namespace ReqRest.Tests.Sdk.TestBases
{
    using ReqRest.Builders;

    /// <summary>
    ///     Extends the <see cref="BuilderTestBase"/> with a <see cref="Builder"/> property that
    ///     returns an <see cref="IHttpHeadersBuilder"/> of a specific type.
    /// </summary>
    /// <typeparam name="TBuilder">The type of the <see cref="IHttpHeadersBuilder"/>.</typeparam>
    public class HttpHeadersBuilderTestBase<TBuilder> : BuilderTestBase where TBuilder : IHttpHeadersBuilder
    {

        /// <summary>
        ///     Gets the builder of type <typeparamref name="TBuilder"/>.
        /// </summary>
        protected TBuilder Builder => (TBuilder)(object)Service;

    }

}
