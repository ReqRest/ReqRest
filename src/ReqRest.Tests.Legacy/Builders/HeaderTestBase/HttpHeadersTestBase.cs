namespace ReqRest.Tests.Builders.HeaderTestBase
{
    using System.Net.Http.Headers;

    /// <summary>
    ///     An abstract base class for builder methods test that interact with the
    ///     <see cref="HttpHeaders"/> base class.
    /// </summary>
    /// <typeparam name="THeaders">The type of <see cref="HttpHeaders"/> to be tested.</typeparam>
    public abstract class HttpHeadersTestBase<THeaders> where THeaders : HttpHeaders
    {

        /// <summary>
        ///     Gets the headers which are being built.
        /// </summary>
        protected abstract THeaders Headers { get; }

    }

}
