namespace ReqRest.Client
{
    using System;
    using System.Net.Http;
    using System.Threading;

    /// <summary>
    ///     Defines a static way for obtaining a default <see cref="HttpClient"/> for
    ///     plug-and-play of API clients.
    /// </summary>
    internal static class DefaultHttpClientProvider
    {

        private static Lazy<HttpClient> s_httpClientLazy = new Lazy<HttpClient>(
            () => new HttpClient(),
            LazyThreadSafetyMode.ExecutionAndPublication
        );

        /// <summary>Gets a default <see cref="HttpClient"/> instance which can be used by API clients.</summary>
        internal static HttpClient GetHttpClient() =>
            s_httpClientLazy.Value;

    }

}
