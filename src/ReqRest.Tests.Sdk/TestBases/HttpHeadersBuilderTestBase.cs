namespace ReqRest.Tests.Sdk.TestBases
{
    using System.Net.Http.Headers;
    using ReqRest.Builders;

    /// <summary>
    ///     Extends the <see cref="BuilderTestBase"/> with a <see cref="Builder"/> property that
    ///     returns an <see cref="IHttpHeadersBuilder{THeaders}"/> of a specific type.
    /// </summary>
    /// <typeparam name="TBuilder">The type of the builder.</typeparam>
    /// <typeparam name="THeaders">The type of the <see cref="HttpHeaders"/>.</typeparam>
    public class HttpHeadersBuilderTestBase<TBuilder, THeaders> : BuilderTestBase
        where TBuilder : IHttpHeadersBuilder<THeaders>
        where THeaders : HttpHeaders
    {

        /// <summary>
        ///     Gets a headers builder for the <typeparamref name="THeaders"/> headers.
        /// </summary>
        protected TBuilder Builder => (TBuilder)(object)Service;

    }

}
