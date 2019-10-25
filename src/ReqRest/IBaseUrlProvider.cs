namespace ReqRest
{
    using ReqRest.Builders;

    /// <summary>
    ///     Represents a member which can create and return a <see cref="UrlBuilder"/> instance
    ///     which is pre-configured with a base URL that can be further extended by a consumer.
    /// </summary>
    /// <remarks>
    ///     This interface is mainly used by the <see cref="RestInterface"/> to dynamically
    ///     build the URL for requests that can be made through such an interface.
    ///     Each <see cref="RestInterface"/> can append its "interface path", e.g. <c>"foo"</c>,
    ///     to a base URL, e.g. <c>"http://my-address.com"</c>.
    ///     These base URLs are made available though this interface.
    /// </remarks>
    public interface IBaseUrlProvider
    {

        /// <summary>
        ///     Creates and returns a new <see cref="UrlBuilder"/> instance which is pre-configured
        ///     with a base URL that can be further extended by a consumer.
        /// </summary>
        /// <returns>
        ///     A new <see cref="UrlBuilder"/> instance which is pre-configured
        ///     with a base URL that can be further extended by a consumer.
        /// </returns>
        UrlBuilder BuildBaseUrl();

    }

}
