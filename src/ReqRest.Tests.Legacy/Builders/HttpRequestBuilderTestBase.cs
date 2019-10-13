namespace ReqRest.Tests.Builders
{
    using ReqRest.Builders;

    /// <summary>
    ///     A base class for all tests which test the request builder extensions.
    /// </summary>
    public class HttpRequestBuilderTestBase
    {
        
        /// <summary>
        ///     Gets a default <see cref="IHttpRequestMessageBuilder"/> instance which can be used
        ///     for testing the extension methods.
        /// </summary>
        public ReqRest.Builders.HttpRequestMessageBuilder Builder { get; } = 
            new ReqRest.Builders.HttpRequestMessageBuilder();

    }

}
