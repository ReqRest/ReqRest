namespace ReqRest
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using ReqRest.Http;
    using ReqRest.Internal.Serializers;

    /// <summary>
    ///     Encapsulates information for building and making a request to a RESTful HTTP API.
    /// </summary>
    public sealed class ApiRequest : ApiRequestBase
    {

        /// <summary>
        ///     Initializes a new <see cref="ApiRequestBase"/> instance with the specified
        ///     initial property values.
        /// </summary>
        /// <param name="httpClientProvider">
        ///     A function which returns an <see cref="HttpClient"/> instance
        ///     which will ultimately be used to send the <see cref="HttpRequestMessage"/> for
        ///     executing the API request.
        /// </param>
        /// <param name="httpRequestMessage">
        ///     The request from which the builder starts building.
        ///     If <see langword="null"/>, a new instance is created instead.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="httpClientProvider"/>
        /// </exception>
        public ApiRequest(Func<HttpClient> httpClientProvider, HttpRequestMessage? httpRequestMessage = null)
            : base(httpClientProvider, httpRequestMessage) { }

        /// <summary>
        ///     Declares that the response to this request may have an empty HTTP content when
        ///     receiving a response with status code <c>204 No Content</c>.
        /// </summary>
        /// <returns>
        ///     An <see cref="ApiRequestBase"/>, upgraded with the <see cref="NoContent"/> type.
        /// </returns>
        public ApiRequest<NoContent> ReceiveNoContent() =>
            ReceiveNoContent(StatusCode.NoContent);

        /// <inheritdoc cref="ReceiveNoContent(IEnumerable{StatusCodeRange})"/>
        public ApiRequest<NoContent> ReceiveNoContent(params StatusCodeRange[] forStatusCodes) =>
            ReceiveNoContent((IEnumerable<StatusCodeRange>)forStatusCodes);

        /// <summary>
        ///     Declares that the response to this request may have an empty HTTP content, represented
        ///     through the <see cref="NoContent"/> type.
        /// </summary>
        /// <param name="forStatusCodes">
        ///     A set of status codes for which no content is received.
        /// </param>
        /// <returns>
        ///     An <see cref="ApiRequestBase"/>, upgraded with the <see cref="NoContent"/> type.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="forStatusCodes"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="forStatusCodes"/> is empty.
        /// </exception>
        public ApiRequest<NoContent> ReceiveNoContent(IEnumerable<StatusCodeRange> forStatusCodes) =>
            Receive<NoContent>().Build(NoContentSerializer.DefaultFactory, forStatusCodes);

        /// <inheritdoc cref="ReceiveByteArray(IEnumerable{StatusCodeRange})"/>
        public ApiRequest<byte[]> ReceiveByteArray(params StatusCodeRange[] forStatusCodes) =>
            ReceiveByteArray((IEnumerable<StatusCodeRange>)forStatusCodes);

        /// <summary>
        ///     Declares that the response to this request may have an HTTP content which can be
        ///     read as a <see cref="byte"/> array.
        /// </summary>
        /// <param name="forStatusCodes">
        ///     A set of status codes for which the HTTP content may be read as a <see cref="byte"/> array.
        /// </param>
        /// <returns>
        ///     An <see cref="ApiRequestBase"/>, upgraded with the <see cref="byte"/> array type.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="forStatusCodes"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="forStatusCodes"/> is empty.
        /// </exception>
        public ApiRequest<byte[]> ReceiveByteArray(IEnumerable<StatusCodeRange> forStatusCodes) =>
            Receive<byte[]>().Build(ByteArraySerializer.DefaultFactory, forStatusCodes);

        /// <inheritdoc cref="ReceiveString(IEnumerable{StatusCodeRange})"/>
        public ApiRequest<string> ReceiveString(params StatusCodeRange[] forStatusCodes) =>
            ReceiveString((IEnumerable<StatusCodeRange>)forStatusCodes);

        /// <summary>
        ///     Declares that the response to this request may have an HTTP content which can be
        ///     read as a raw string.
        /// </summary>
        /// <param name="forStatusCodes">
        ///     A set of status codes for which the HTTP content may be read as a raw string.
        /// </param>
        /// <returns>
        ///     An <see cref="ApiRequestBase"/>, upgraded with the <see cref="string"/> type.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="forStatusCodes"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="forStatusCodes"/> is empty.
        /// </exception>
        public ApiRequest<string> ReceiveString(IEnumerable<StatusCodeRange> forStatusCodes) =>
            Receive<string>().Build(StringSerializer.DefaultFactory, forStatusCodes);

        /// <summary>
        ///     Declares that the response to this request may have a content of type <typeparamref name="T"/>.
        ///     This returns a builder instance which requires you to specify additional information about
        ///     the possible response (for example, for which status codes the type is a possible result).
        /// </summary>
        /// <typeparam name="T">
        ///     The .NET type which may be returned by the response following this request.
        /// </typeparam>
        /// <returns>
        ///     An <see cref="ResponseTypeInfoBuilder{TRequest}"/> instance which must be used
        ///     to specify additional information about the possible response.
        /// </returns>
        public ResponseTypeInfoBuilder<ApiRequest<T>> Receive<T>() =>
            new ResponseTypeInfoBuilder<ApiRequest<T>>(
                new ApiRequest<T>(this), typeof(T));

        /// <summary>
        ///     Sends the request and returns the resulting HTTP response.
        /// </summary>
        /// <param name="completionOption">
        ///     Defines when the operation should complete (as soon as a response is
        ///     available or after reading the whole response content).
        /// </param>
        /// <param name="cancellationToken">
        ///     A cancellation token which can be used to cancel the operation.
        /// </param>
        /// <returns>The response to the request.</returns>
        /// <exception cref="TaskCanceledException">
        ///     The operation was canceled via the <paramref name="cancellationToken"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The <see cref="ApiRequestBase.HttpClientProvider"/> returned <see langword="null"/>.
        /// </exception>
        /// <remarks>
        ///     This method uses the <see cref="ApiRequestBase.HttpClientProvider"/> function for retrieving an
        ///     <see cref="HttpClient"/> with which the request will be sent.
        /// </remarks>
        /// <example>
        ///     <code>
        ///     var response = await request.FetchResponseAsync();
        ///     Console.WriteLine($"Received status {response.StatusCode}");
        ///     </code>
        /// </example>
        [DebuggerStepThrough]
        public async Task<ApiResponse> FetchResponseAsync(
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default)
        {
            var res = await FetchHttpResponseAsync(completionOption, cancellationToken).ConfigureAwait(false);
            return new ApiResponse(res, PossibleResponseTypes);
        }

    }

}
