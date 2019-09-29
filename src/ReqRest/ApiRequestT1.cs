namespace ReqRest
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using NCommons.Monads;
    using ReqRest.Http;
    using ReqRest.Internal.Serializers;
    using ReqRest.Serializers;

    /// <summary>
    ///     Encapsulates information for building and making a request to a RESTful HTTP API.
    ///     A response to this request may lead to a response content of one of the generic type parameters.
    /// </summary>
    /// <typeparam name="T1">A type which may be returned by a response following this request.</typeparam>
    public sealed class ApiRequest<T1> : ApiRequestBase
    {

        /// <summary>
        ///     Initializes a new instance of this class which upgrades and wraps an existing request.
        /// </summary>
        /// <param name="request">The request to be upgraded and wrapped.</param>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="request"/>
        /// </exception>
        internal ApiRequest(ApiRequest request)
            : base(request ?? throw new ArgumentNullException(nameof(request))) { }

        /// <summary>
        ///     Declares that the response to this request may have an empty HTTP content when
        ///     receiving a response with status code <c>204 No Content</c>.
        /// </summary>
        /// <returns>
        ///     An <see cref="ApiRequestBase"/>, upgraded with the <see cref="NoContent"/> type.
        /// </returns>
        public ApiRequest<T1, NoContent> ReceiveNoContent() =>
            ReceiveNoContent(StatusCode.NoContent);

        /// <inheritdoc cref="ReceiveNoContent(IEnumerable{StatusCodeRange})"/>
        public ApiRequest<T1, NoContent> ReceiveNoContent(params StatusCodeRange[] forStatusCodes) =>
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
        public ApiRequest<T1, NoContent> ReceiveNoContent(IEnumerable<StatusCodeRange> forStatusCodes) =>
            Receive<NoContent>().Build(NoContentSerializer.DefaultFactory, forStatusCodes);

        /// <inheritdoc cref="ReceiveByteArray(IEnumerable{StatusCodeRange})"/>
        public ApiRequest<T1, byte[]> ReceiveByteArray(params StatusCodeRange[] forStatusCodes) =>
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
        public ApiRequest<T1, byte[]> ReceiveByteArray(IEnumerable<StatusCodeRange> forStatusCodes) =>
            Receive<byte[]>().Build(ByteArraySerializer.DefaultFactory, forStatusCodes);

        /// <inheritdoc cref="ReceiveString(IEnumerable{StatusCodeRange})"/>
        public ApiRequest<T1, string> ReceiveString(params StatusCodeRange[] forStatusCodes) =>
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
        public ApiRequest<T1, string> ReceiveString(IEnumerable<StatusCodeRange> forStatusCodes) =>
            Receive<string>().Build(StringSerializer.DefaultFactory, forStatusCodes);

        /// <summary>
        ///     Declares that the response to this request may have a content of type <typeparamref name="T2"/>.
        ///     This returns a builder instance which requires you to specify additional information about
        ///     the possible response (for example, for which status codes the type is a possible result).
        /// </summary>
        /// <typeparam name="T2">
        ///     The .NET type which may be returned by the response following this request.
        /// </typeparam>
        /// <returns>
        ///     An <see cref="ResponseTypeInfoBuilder{TRequest}"/> instance which must be used
        ///     to specify additional information about the possible response.
        /// </returns>
        public ResponseTypeInfoBuilder<ApiRequest<T1, T2>> Receive<T2>() =>
            new ResponseTypeInfoBuilder<ApiRequest<T1, T2>>(
                new ApiRequest<T1, T2>(this), typeof(T2));

        /// <summary>
        ///     Sends the request, deserializes the HTTP content of the resulting HTTP response and
        ///     returns both the response and the deserialized resource.
        /// </summary>
        /// <param name="completionOption">
        ///     Defines when the operation should complete (as soon as a response is
        ///     available or after reading the whole response content).
        /// </param>
        /// <param name="cancellationToken">
        ///     A cancellation token which can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     A tuple which stores the response to the request and the resource which was deserialized
        ///     from the response's HTTP content.
        ///     
        ///     The resource is represented through a <see cref="Variant{T1}"/>
        ///     which holds a value that matches the response type declared for the response's HTTP status code.
        ///     This variant is empty if the response's HTTP status code doesn't match any declared one.    
        /// </returns>
        /// <exception cref="TaskCanceledException">
        ///     The operation was canceled via the <paramref name="cancellationToken"/>.
        /// </exception>
        /// <exception cref="HttpContentSerializationException">
        ///     There was a resource to deserialize, but the underlying <see cref="IHttpContentDeserializer"/>
        ///     threw an exception while deserializing the resource.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The <see cref="ApiRequestBase.HttpClientProvider"/> returned <see langword="null"/>.
        /// </exception>
        /// <remarks>
        ///     This method is designed for developer convenience and simply combines calling
        ///     <see cref="FetchResponseAsync(HttpCompletionOption, CancellationToken)"/> and
        ///     deserializing the resource afterwards.
        ///     
        ///     Be aware that this method may throw an <see cref="HttpContentSerializationException"/>,
        ///     because it runs the code of an arbitrary <see cref="IHttpContentDeserializer"/>.
        ///     
        ///     This method uses the <see cref="ApiRequestBase.HttpClientProvider"/> function for retrieving an
        ///     <see cref="HttpClient"/> with which the request will be sent.
        ///     
        ///     See the example for how to ideally use this method.
        /// </remarks>
        /// <seealso cref="FetchResponseAsync(HttpCompletionOption, CancellationToken)"/>
        /// <seealso cref="FetchResourceAsync(HttpCompletionOption, CancellationToken)"/>
        /// <example>
        ///     <code>
        ///     var (response, resource) = await request.FetchAsync();
        ///     
        ///     // Note: The above is equivalent to calling these two lines manually:
        ///     var response = await request.FetchResponseAsync();
        ///     var resource = await response.DeserializeResourceAsync();
        ///     
        ///     Console.WriteLine($"Received status {response.StatusCode}");
        ///     Console.WriteLine($"Received a resource of type {resource.Value?.GetType()}");
        ///     </code>
        /// </example>
        [DebuggerStepThrough]
        public async Task<(ApiResponse<T1> Response, Variant<T1> Resource)> FetchAsync(
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default)
        {
            var response = await FetchResponseAsync(completionOption, cancellationToken).ConfigureAwait(false);
            var resource = await response.DeserializeResourceAsync(cancellationToken).ConfigureAwait(false);
            return (response, resource);
        }

        /// <summary>
        ///     Sends the request and returns the resulting HTTP response without serializing its HTTP content.
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
        ///     This method only returns the HTTP response itself and does not perform any deserialization logic.
        ///     To access the underlying resource, use the 
        ///     <see cref="ApiResponse{T1}.DeserializeResourceAsync"/> method
        ///     of the response.
        ///     
        ///     Consider using the <see cref="FetchAsync(HttpCompletionOption, CancellationToken)"/> method if you
        ///     are interested in both the response and the underlying resource.
        ///     
        ///     This method uses the <see cref="ApiRequestBase.HttpClientProvider"/> function for retrieving an
        ///     <see cref="HttpClient"/> with which the request will be sent.
        /// </remarks>
        /// <example>
        ///     <code>
        ///     var response = await request.FetchResponseAsync();
        ///     var resource = await response.DeserializeResourceAsync();
        ///     
        ///     Console.WriteLine($"Received status {response.StatusCode}");
        ///     Console.WriteLine($"Received a resource of type {resource.Value?.GetType()}");
        ///     </code>
        /// </example>
        /// <seealso cref="FetchAsync(HttpCompletionOption, CancellationToken)"/>
        /// <seealso cref="FetchResourceAsync(HttpCompletionOption, CancellationToken)"/>
        [DebuggerStepThrough]
        public async Task<ApiResponse<T1>> FetchResponseAsync(
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default)
        {
            var res = await FetchHttpResponseAsync(completionOption, cancellationToken).ConfigureAwait(false);
            return new ApiResponse<T1>(res, PossibleResponseTypes);
        }

        /// <summary>
        ///     Sends the request, deserializes the HTTP content of the resulting HTTP response and
        ///     then returns this deserialized resource.
        /// </summary>
        /// <param name="completionOption">
        ///     Defines when the operation should complete (as soon as a response is
        ///     available or after reading the whole response content).
        /// </param>
        /// <param name="cancellationToken">
        ///     A cancellation token which can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     The deserialized resource represented through a <see cref="Variant{T1}"/>
        ///     which holds a value that matches the response type declared for the response's HTTP status code.
        ///     This variant is empty if the response's HTTP status code doesn't match any declared one.
        /// </returns>
        /// <exception cref="TaskCanceledException">
        ///     The operation was canceled via the <paramref name="cancellationToken"/>.
        /// </exception>
        /// <exception cref="HttpContentSerializationException">
        ///     There was a resource to deserialize, but the underlying <see cref="IHttpContentDeserializer"/>
        ///     threw an exception while deserializing the resource.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The <see cref="ApiRequestBase.HttpClientProvider"/> returned <see langword="null"/>.
        /// </exception>
        /// <remarks>
        ///     This method only returns the deserialized resource. Any other information about the
        ///     HTTP response, for example the returned HTTP status code, is lost.
        ///     If you are interested in any kind of information about the HTTP response, consider
        ///     using the <see cref="FetchAsync(HttpCompletionOption, CancellationToken)"/> method.
        ///     
        ///     Be aware that this method may throw an <see cref="HttpContentSerializationException"/>,
        ///     because it runs the code of an arbitrary <see cref="IHttpContentDeserializer"/>.
        ///     
        ///     This method uses the <see cref="ApiRequestBase.HttpClientProvider"/> function for retrieving an
        ///     <see cref="HttpClient"/> with which the request will be sent.
        /// </remarks>
        /// <example>
        ///     <code>
        ///     var resource = await response.FetchResourceAsync();
        ///     Console.WriteLine($"Received a resource of type {resource.Value?.GetType()}");
        ///     </code>
        /// </example>
        /// <seealso cref="FetchAsync(HttpCompletionOption, CancellationToken)"/>
        /// <seealso cref="FetchResponseAsync(HttpCompletionOption, CancellationToken)"/>
        [DebuggerStepThrough]
        public async Task<Variant<T1>> FetchResourceAsync(
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default)
        {
            var response = await FetchResponseAsync(completionOption, cancellationToken).ConfigureAwait(false);
            return await response.DeserializeResourceAsync(cancellationToken).ConfigureAwait(false);
        }

    }

}
