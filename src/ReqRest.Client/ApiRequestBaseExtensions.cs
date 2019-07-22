namespace ReqRest.Client
{
    using System;
    using System.Diagnostics;
    using System.Net.Http;

    /// <summary>
    ///     Extends the <see cref="ApiRequestBase"/> class with default extension methods provided
    ///     by the library.
    /// </summary>
    public static partial class ApiRequestBaseExtensions
    {

        /// <summary>
        ///     Sets the <see cref="ApiRequestBase.HttpClientProvider"/> function to a function
        ///     which returns the specified <paramref name="httpClient"/>.
        /// </summary>
        /// <typeparam name="T">The type of the request.</typeparam>
        /// <param name="request">The request.</param>
        /// <param name="httpClient">
        ///     An <see cref="HttpClient"/> instance which will ultimately be used to send the
        ///     <see cref="HttpRequestMessage"/> for executing the API request.
        /// </param>
        /// <returns>The specified <paramref name="request"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="request"/>
        ///     * <paramref name="httpClient"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetHttpClientProvider<T>(this T request, HttpClient httpClient)
            where T : ApiRequestBase
        {
            _ = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            return request.SetHttpClientProvider(() => httpClient);
        }

        /// <summary>
        ///     Sets the <see cref="ApiRequestBase.HttpClientProvider"/> function which returns an
        ///     <see cref="HttpClient"/> instance which will ultimately be used to send the
        ///     <see cref="HttpRequestMessage"/> for executing the API request.
        /// </summary>
        /// <typeparam name="T">The type of the request.</typeparam>
        /// <param name="request">The request.</param>
        /// <param name="httpClientProvider">
        ///     A function which returns an <see cref="HttpClient"/> instance
        ///     which will ultimately be used to send the <see cref="HttpRequestMessage"/> for
        ///     executing the API request.
        /// </param>
        /// <returns>The specified <paramref name="request"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="request"/>
        ///     * <paramref name="httpClientProvider"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetHttpClientProvider<T>(this T request, Func<HttpClient> httpClientProvider)
            where T : ApiRequestBase
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));
            request.HttpClientProvider = httpClientProvider; // Does ANE validation.
            return request;
        }

    }

}
