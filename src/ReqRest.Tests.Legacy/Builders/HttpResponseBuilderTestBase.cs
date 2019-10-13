namespace ReqRest.Tests.Builders
{
    /// <summary>
    ///     A base class for all tests which test the request builder extensions.
    /// </summary>
    public class HttpResponseBuilderTestBase
    {

        /// <summary>
        ///     Gets <see cref="HttpResponseMessageBuilder"/> instance which can be used
        ///     the various builder methods for that class.
        /// </summary>
        public ReqRest.Builders.HttpResponseMessageBuilder Builder { get; } =
            new ReqRest.Builders.HttpResponseMessageBuilder();

    }

}
