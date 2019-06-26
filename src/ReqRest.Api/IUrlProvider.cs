namespace ReqRest
{
    using System;

    /// <summary>
    ///     Represents a member which is able to provide a full URL.
    /// </summary>
    public interface IUrlProvider
    {

        /// <summary>
        ///     Creates and returns a new <see cref="UriBuilder"/> instance which is configured
        ///     with this member's current URL information.
        /// </summary>
        /// <returns>
        ///     A new <see cref="UriBuilder"/> instance which is already configured with the
        ///     URL information that this member has.
        /// </returns>
        UriBuilder GetUrlBuilder();

    }

    internal static class UrlProviderExtensions
    {

        public static Uri GetUrl(this IUrlProvider urlProvider)
        {
            _ = urlProvider ?? throw new ArgumentNullException(nameof(urlProvider));
            return urlProvider.GetUrlBuilder().Uri;
        }

    }

}
