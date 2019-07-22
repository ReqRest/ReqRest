namespace ReqRest.Builders.Tests
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
        public Builders.HttpRequestMessageBuilder Builder { get; } = 
            new Builders.HttpRequestMessageBuilder();

    }

}
