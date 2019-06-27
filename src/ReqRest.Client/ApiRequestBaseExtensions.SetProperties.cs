namespace ReqRest
{
    using System;
    using System.Diagnostics;
    using System.Net.Http;

    public static partial class ApiRequestBaseExtensions
    {

        /// <summary>
        ///     Sets the <see cref="ApiRequestBase.HttpClient"/> which will be used for executing the API request.
        /// </summary>
        /// <typeparam name="T">The type of the request.</typeparam>
        /// <param name="request">The request.</param>
        /// <param name="httpClient">The new <see cref="HttpClient"/> to be used by the request.</param>
        /// <returns>The specified <paramref name="request"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="request"/>
        ///     * <paramref name="httpClient"/>
        /// </exception>
        [DebuggerStepThrough]
        public static T SetHttpClient<T>(this T request, HttpClient httpClient)
            where T : ApiRequestBase
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));
            request.HttpClient = httpClient; // Does ANE validation.
            return request;
        }

    }

}
