namespace ReqRest.Client
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using NCommons.Monads;
    using ReqRest.Http;
    using ReqRest.Serializers;

#pragma warning disable CA2000 // Dispose objects before losing scope

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
        ///     Makes this request via the associated <see cref="ApiRequestBase.HttpClientProvider"/> and
        ///     returns the response returned by the server as an <see cref="ApiResponse"/>.
        /// </summary>
        /// <param name="completionOption">
        ///     Defines when the operation should complete (as soon as a response is
        ///     available or after reading the whole response content).
        /// </param>
        /// <param name="cancellationToken">
        ///     A cancellation token which can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     An <see cref="ApiResponse"/> instance which contains the response of the server.
        /// </returns>
        /// <exception cref="TaskCanceledException">
        ///     The operation was canceled via the <paramref name="cancellationToken"/>.
        /// </exception>
        [DebuggerStepThrough]
        public async Task<ApiResponse> FetchResponseAsync(
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default)
        {
            var res = await FetchHttpResponseAsync(completionOption, cancellationToken).ConfigureAwait(false);
            return new ApiResponse(res, PossibleResponseTypes);
        }

    }

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
        ///     Makes this request via the associated <see cref="ApiRequestBase.HttpClientProvider"/> and
        ///     returns the response returned by the server as an <see cref="ApiResponse{T1}"/>.
        /// </summary>
        /// <param name="completionOption">
        ///     Defines when the operation should complete (as soon as a response is
        ///     available or after reading the whole response content).
        /// </param>
        /// <param name="cancellationToken">
        ///     A cancellation token which can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     An <see cref="ApiResponse{T1}"/> instance which contains the response of the server.
        /// </returns>
        /// <exception cref="TaskCanceledException">
        ///     The operation was canceled via the <paramref name="cancellationToken"/>.
        /// </exception>
        [DebuggerStepThrough]
        public async Task<ApiResponse<T1>> FetchResponseAsync(
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default)
        {
            var res = await FetchHttpResponseAsync(completionOption, cancellationToken).ConfigureAwait(false);
            return new ApiResponse<T1>(res, PossibleResponseTypes);
        }

        /// <summary>
        ///     Makes this request via the associated <see cref="ApiRequestBase.HttpClientProvider"/>.
        ///     Afterwards, deserializes the content of the underlying HTTP content depending
        ///     on the response's HTTP status code.
        ///     This returns a variant which will hold one of the possible generic types or no value if
        ///     the response's status code could not be matched with one of them.
        /// </summary>
        /// <param name="completionOption">
        ///     Defines when the operation should complete (as soon as a response is
        ///     available or after reading the whole response content).
        /// </param>
        /// <param name="cancellationToken">
        ///     A cancellation token which can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     A <see cref="Variant{T1}"/> which either holds the deserialized value (if there
        ///     was a <see cref="ResponseTypeInfo"/> in the <see cref="ApiResponseBase.PossibleResponseTypes"/>
        ///     set which matches the response's status code) or no value (if no such info was found).
        /// </returns>
        /// <exception cref="TaskCanceledException">
        ///     The operation was canceled via the <paramref name="cancellationToken"/>.
        /// </exception>
        /// <exception cref="HttpContentSerializationException">
        ///     There was a resource to deserialize, but the underlying <see cref="IHttpContentDeserializer"/>
        ///     threw an exception while deserializing the resource.
        /// </exception>
        [DebuggerStepThrough]
        public async Task<Variant<T1>> FetchResourceAsync(
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default)
        {
            var response = await FetchResponseAsync(completionOption, cancellationToken).ConfigureAwait(false);
            return await response.DeserializeResourceAsync().ConfigureAwait(false);
        }

    }

    /// <summary>
    ///     Encapsulates information for building and making a request to a RESTful HTTP API.
    ///     A response to this request may lead to a response content of one of the generic type parameters.
    /// </summary>
    /// <typeparam name="T1">A type which may be returned by a response following this request.</typeparam>
    /// <typeparam name="T2">A type which may be returned by the response following this request.</typeparam>
    public sealed class ApiRequest<T1, T2> : ApiRequestBase
    {

        /// <summary>
        ///     Initializes a new instance of this class which upgrades and wraps an existing request.
        /// </summary>
        /// <param name="request">The request to be upgraded and wrapped.</param>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="request"/>
        /// </exception>
        internal ApiRequest(ApiRequest<T1> request)
            : base(request ?? throw new ArgumentNullException(nameof(request))) { }

        /// <summary>
        ///     Declares that the response to this request may have an empty HTTP content when
        ///     receiving a response with status code <c>204 No Content</c>.
        /// </summary>
        /// <returns>
        ///     An <see cref="ApiRequestBase"/>, upgraded with the <see cref="NoContent"/> type.
        /// </returns>
        public ApiRequest<T1, T2, NoContent> ReceiveNoContent() =>
            ReceiveNoContent(StatusCode.NoContent);

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
        public ApiRequest<T1, T2, NoContent> ReceiveNoContent(params StatusCodeRange[] forStatusCodes) =>
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
        public ApiRequest<T1, T2, NoContent> ReceiveNoContent(IEnumerable<StatusCodeRange> forStatusCodes) =>
            Receive<NoContent>().Build(NoContentSerializer.DefaultFactory, forStatusCodes);

        /// <summary>
        ///     Declares that the response to this request may have a content of type <typeparamref name="T3"/>.
        ///     This returns a builder instance which requires you to specify additional information about
        ///     the possible response (for example, for which status codes the type is a possible result).
        /// </summary>
        /// <typeparam name="T3">
        ///     The .NET type which may be returned by the response following this request.
        /// </typeparam>
        /// <returns>
        ///     An <see cref="ResponseTypeInfoBuilder{TRequest}"/> instance which must be used
        ///     to specify additional information about the possible response.
        /// </returns>
        public ResponseTypeInfoBuilder<ApiRequest<T1, T2, T3>> Receive<T3>() =>
            new ResponseTypeInfoBuilder<ApiRequest<T1, T2, T3>>(
                new ApiRequest<T1, T2, T3>(this), typeof(T3));

        /// <summary>
        ///     Makes this request via the associated <see cref="ApiRequestBase.HttpClientProvider"/> and
        ///     returns the response returned by the server as an <see cref="ApiResponse{T1, T2}"/>.
        /// </summary>
        /// <param name="completionOption">
        ///     Defines when the operation should complete (as soon as a response is
        ///     available or after reading the whole response content).
        /// </param>
        /// <param name="cancellationToken">
        ///     A cancellation token which can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     An <see cref="ApiResponse{T1, T2}"/> instance which contains the response of the server.
        /// </returns>
        /// <exception cref="TaskCanceledException">
        ///     The operation was canceled via the <paramref name="cancellationToken"/>.
        /// </exception>
        [DebuggerStepThrough]
        public async Task<ApiResponse<T1, T2>> FetchResponseAsync(
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default)
        {
            var res = await FetchHttpResponseAsync(completionOption, cancellationToken).ConfigureAwait(false);
            return new ApiResponse<T1, T2>(res, PossibleResponseTypes);
        }

        /// <summary>
        ///     Makes this request via the associated <see cref="ApiRequestBase.HttpClientProvider"/>.
        ///     Afterwards, deserializes the content of the underlying HTTP content depending
        ///     on the response's HTTP status code.
        ///     This returns a variant which will hold one of the possible generic types or no value if
        ///     the response's status code could not be matched with one of them.
        /// </summary>
        /// <param name="completionOption">
        ///     Defines when the operation should complete (as soon as a response is
        ///     available or after reading the whole response content).
        /// </param>
        /// <param name="cancellationToken">
        ///     A cancellation token which can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     A <see cref="Variant{T1, T2}"/> which either holds the deserialized value (if there
        ///     was a <see cref="ResponseTypeInfo"/> in the <see cref="ApiResponseBase.PossibleResponseTypes"/>
        ///     set which matches the response's status code) or no value (if no such info was found).
        /// </returns>
        /// <exception cref="TaskCanceledException">
        ///     The operation was canceled via the <paramref name="cancellationToken"/>.
        /// </exception>
        /// <exception cref="HttpContentSerializationException">
        ///     There was a resource to deserialize, but the underlying <see cref="IHttpContentDeserializer"/>
        ///     threw an exception while deserializing the resource.
        /// </exception>
        [DebuggerStepThrough]
        public async Task<Variant<T1, T2>> FetchResourceAsync(
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default)
        {
            var response = await FetchResponseAsync(completionOption, cancellationToken).ConfigureAwait(false);
            return await response.DeserializeResourceAsync().ConfigureAwait(false);
        }

    }

    /// <summary>
    ///     Encapsulates information for building and making a request to a RESTful HTTP API.
    ///     A response to this request may lead to a response content of one of the generic type parameters.
    /// </summary>
    /// <typeparam name="T1">A type which may be returned by a response following this request.</typeparam>
    /// <typeparam name="T2">A type which may be returned by the response following this request.</typeparam>
    /// <typeparam name="T3">A type which may be returned by the response following this request.</typeparam>
    public sealed class ApiRequest<T1, T2, T3> : ApiRequestBase
    {

        /// <summary>
        ///     Initializes a new instance of this class which upgrades and wraps an existing request.
        /// </summary>
        /// <param name="request">The request to be upgraded and wrapped.</param>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="request"/>
        /// </exception>
        internal ApiRequest(ApiRequest<T1, T2> request)
            : base(request ?? throw new ArgumentNullException(nameof(request))) { }

        /// <summary>
        ///     Declares that the response to this request may have an empty HTTP content when
        ///     receiving a response with status code <c>204 No Content</c>.
        /// </summary>
        /// <returns>
        ///     An <see cref="ApiRequestBase"/>, upgraded with the <see cref="NoContent"/> type.
        /// </returns>
        public ApiRequest<T1, T2, T3, NoContent> ReceiveNoContent() =>
            ReceiveNoContent(StatusCode.NoContent);

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
        public ApiRequest<T1, T2, T3, NoContent> ReceiveNoContent(params StatusCodeRange[] forStatusCodes) =>
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
        public ApiRequest<T1, T2, T3, NoContent> ReceiveNoContent(IEnumerable<StatusCodeRange> forStatusCodes) =>
            Receive<NoContent>().Build(NoContentSerializer.DefaultFactory, forStatusCodes);

        /// <summary>
        ///     Declares that the response to this request may have a content of type <typeparamref name="T4"/>.
        ///     This returns a builder instance which requires you to specify additional information about
        ///     the possible response (for example, for which status codes the type is a possible result).
        /// </summary>
        /// <typeparam name="T4">
        ///     The .NET type which may be returned by the response following this request.
        /// </typeparam>
        /// <returns>
        ///     An <see cref="ResponseTypeInfoBuilder{TRequest}"/> instance which must be used
        ///     to specify additional information about the possible response.
        /// </returns>
        public ResponseTypeInfoBuilder<ApiRequest<T1, T2, T3, T4>> Receive<T4>() =>
            new ResponseTypeInfoBuilder<ApiRequest<T1, T2, T3, T4>>(
                new ApiRequest<T1, T2, T3, T4>(this), typeof(T4));

        /// <summary>
        ///     Makes this request via the associated <see cref="ApiRequestBase.HttpClientProvider"/> and
        ///     returns the response returned by the server as an <see cref="ApiResponse{T1, T2, T3}"/>.
        /// </summary>
        /// <param name="completionOption">
        ///     Defines when the operation should complete (as soon as a response is
        ///     available or after reading the whole response content).
        /// </param>
        /// <param name="cancellationToken">
        ///     A cancellation token which can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     An <see cref="ApiResponse{T1, T2, T3}"/> instance which contains the response of the server.
        /// </returns>
        /// <exception cref="TaskCanceledException">
        ///     The operation was canceled via the <paramref name="cancellationToken"/>.
        /// </exception>
        [DebuggerStepThrough]
        public async Task<ApiResponse<T1, T2, T3>> FetchResponseAsync(
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default)
        {
            var res = await FetchHttpResponseAsync(completionOption, cancellationToken).ConfigureAwait(false);
            return new ApiResponse<T1, T2, T3>(res, PossibleResponseTypes);
        }

        /// <summary>
        ///     Makes this request via the associated <see cref="ApiRequestBase.HttpClientProvider"/>.
        ///     Afterwards, deserializes the content of the underlying HTTP content depending
        ///     on the response's HTTP status code.
        ///     This returns a variant which will hold one of the possible generic types or no value if
        ///     the response's status code could not be matched with one of them.
        /// </summary>
        /// <param name="completionOption">
        ///     Defines when the operation should complete (as soon as a response is
        ///     available or after reading the whole response content).
        /// </param>
        /// <param name="cancellationToken">
        ///     A cancellation token which can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     A <see cref="Variant{T1, T2, T3}"/> which either holds the deserialized value (if there
        ///     was a <see cref="ResponseTypeInfo"/> in the <see cref="ApiResponseBase.PossibleResponseTypes"/>
        ///     set which matches the response's status code) or no value (if no such info was found).
        /// </returns>
        /// <exception cref="TaskCanceledException">
        ///     The operation was canceled via the <paramref name="cancellationToken"/>.
        /// </exception>
        /// <exception cref="HttpContentSerializationException">
        ///     There was a resource to deserialize, but the underlying <see cref="IHttpContentDeserializer"/>
        ///     threw an exception while deserializing the resource.
        /// </exception>
        [DebuggerStepThrough]
        public async Task<Variant<T1, T2, T3>> FetchResourceAsync(
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default)
        {
            var response = await FetchResponseAsync(completionOption, cancellationToken).ConfigureAwait(false);
            return await response.DeserializeResourceAsync().ConfigureAwait(false);
        }

    }

    /// <summary>
    ///     Encapsulates information for building and making a request to a RESTful HTTP API.
    ///     A response to this request may lead to a response content of one of the generic type parameters.
    /// </summary>
    /// <typeparam name="T1">A type which may be returned by a response following this request.</typeparam>
    /// <typeparam name="T2">A type which may be returned by the response following this request.</typeparam>
    /// <typeparam name="T3">A type which may be returned by the response following this request.</typeparam>
    /// <typeparam name="T4">A type which may be returned by the response following this request.</typeparam>
    public sealed class ApiRequest<T1, T2, T3, T4> : ApiRequestBase
    {

        /// <summary>
        ///     Initializes a new instance of this class which upgrades and wraps an existing request.
        /// </summary>
        /// <param name="request">The request to be upgraded and wrapped.</param>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="request"/>
        /// </exception>
        internal ApiRequest(ApiRequest<T1, T2, T3> request)
            : base(request ?? throw new ArgumentNullException(nameof(request))) { }

        /// <summary>
        ///     Declares that the response to this request may have an empty HTTP content when
        ///     receiving a response with status code <c>204 No Content</c>.
        /// </summary>
        /// <returns>
        ///     An <see cref="ApiRequestBase"/>, upgraded with the <see cref="NoContent"/> type.
        /// </returns>
        public ApiRequest<T1, T2, T3, T4, NoContent> ReceiveNoContent() =>
            ReceiveNoContent(StatusCode.NoContent);

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
        public ApiRequest<T1, T2, T3, T4, NoContent> ReceiveNoContent(params StatusCodeRange[] forStatusCodes) =>
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
        public ApiRequest<T1, T2, T3, T4, NoContent> ReceiveNoContent(IEnumerable<StatusCodeRange> forStatusCodes) =>
            Receive<NoContent>().Build(NoContentSerializer.DefaultFactory, forStatusCodes);

        /// <summary>
        ///     Declares that the response to this request may have a content of type <typeparamref name="T5"/>.
        ///     This returns a builder instance which requires you to specify additional information about
        ///     the possible response (for example, for which status codes the type is a possible result).
        /// </summary>
        /// <typeparam name="T5">
        ///     The .NET type which may be returned by the response following this request.
        /// </typeparam>
        /// <returns>
        ///     An <see cref="ResponseTypeInfoBuilder{TRequest}"/> instance which must be used
        ///     to specify additional information about the possible response.
        /// </returns>
        public ResponseTypeInfoBuilder<ApiRequest<T1, T2, T3, T4, T5>> Receive<T5>() =>
            new ResponseTypeInfoBuilder<ApiRequest<T1, T2, T3, T4, T5>>(
                new ApiRequest<T1, T2, T3, T4, T5>(this), typeof(T5));

        /// <summary>
        ///     Makes this request via the associated <see cref="ApiRequestBase.HttpClientProvider"/> and
        ///     returns the response returned by the server as an <see cref="ApiResponse{T1, T2, T3, T4}"/>.
        /// </summary>
        /// <param name="completionOption">
        ///     Defines when the operation should complete (as soon as a response is
        ///     available or after reading the whole response content).
        /// </param>
        /// <param name="cancellationToken">
        ///     A cancellation token which can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     An <see cref="ApiResponse{T1, T2, T3, T4}"/> instance which contains the response of the server.
        /// </returns>
        /// <exception cref="TaskCanceledException">
        ///     The operation was canceled via the <paramref name="cancellationToken"/>.
        /// </exception>
        [DebuggerStepThrough]
        public async Task<ApiResponse<T1, T2, T3, T4>> FetchResponseAsync(
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default)
        {
            var res = await FetchHttpResponseAsync(completionOption, cancellationToken).ConfigureAwait(false);
            return new ApiResponse<T1, T2, T3, T4>(res, PossibleResponseTypes);
        }

        /// <summary>
        ///     Makes this request via the associated <see cref="ApiRequestBase.HttpClientProvider"/>.
        ///     Afterwards, deserializes the content of the underlying HTTP content depending
        ///     on the response's HTTP status code.
        ///     This returns a variant which will hold one of the possible generic types or no value if
        ///     the response's status code could not be matched with one of them.
        /// </summary>
        /// <param name="completionOption">
        ///     Defines when the operation should complete (as soon as a response is
        ///     available or after reading the whole response content).
        /// </param>
        /// <param name="cancellationToken">
        ///     A cancellation token which can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     A <see cref="Variant{T1, T2, T3, T4}"/> which either holds the deserialized value (if there
        ///     was a <see cref="ResponseTypeInfo"/> in the <see cref="ApiResponseBase.PossibleResponseTypes"/>
        ///     set which matches the response's status code) or no value (if no such info was found).
        /// </returns>
        /// <exception cref="TaskCanceledException">
        ///     The operation was canceled via the <paramref name="cancellationToken"/>.
        /// </exception>
        /// <exception cref="HttpContentSerializationException">
        ///     There was a resource to deserialize, but the underlying <see cref="IHttpContentDeserializer"/>
        ///     threw an exception while deserializing the resource.
        /// </exception>
        [DebuggerStepThrough]
        public async Task<Variant<T1, T2, T3, T4>> FetchResourceAsync(
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default)
        {
            var response = await FetchResponseAsync(completionOption, cancellationToken).ConfigureAwait(false);
            return await response.DeserializeResourceAsync().ConfigureAwait(false);
        }

    }

    /// <summary>
    ///     Encapsulates information for building and making a request to a RESTful HTTP API.
    ///     A response to this request may lead to a response content of one of the generic type parameters.
    /// </summary>
    /// <typeparam name="T1">A type which may be returned by a response following this request.</typeparam>
    /// <typeparam name="T2">A type which may be returned by the response following this request.</typeparam>
    /// <typeparam name="T3">A type which may be returned by the response following this request.</typeparam>
    /// <typeparam name="T4">A type which may be returned by the response following this request.</typeparam>
    /// <typeparam name="T5">A type which may be returned by the response following this request.</typeparam>
    public sealed class ApiRequest<T1, T2, T3, T4, T5> : ApiRequestBase
    {

        /// <summary>
        ///     Initializes a new instance of this class which upgrades and wraps an existing request.
        /// </summary>
        /// <param name="request">The request to be upgraded and wrapped.</param>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="request"/>
        /// </exception>
        internal ApiRequest(ApiRequest<T1, T2, T3, T4> request)
            : base(request ?? throw new ArgumentNullException(nameof(request))) { }

        /// <summary>
        ///     Declares that the response to this request may have an empty HTTP content when
        ///     receiving a response with status code <c>204 No Content</c>.
        /// </summary>
        /// <returns>
        ///     An <see cref="ApiRequestBase"/>, upgraded with the <see cref="NoContent"/> type.
        /// </returns>
        public ApiRequest<T1, T2, T3, T4, T5, NoContent> ReceiveNoContent() =>
            ReceiveNoContent(StatusCode.NoContent);

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
        public ApiRequest<T1, T2, T3, T4, T5, NoContent> ReceiveNoContent(params StatusCodeRange[] forStatusCodes) =>
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
        public ApiRequest<T1, T2, T3, T4, T5, NoContent> ReceiveNoContent(IEnumerable<StatusCodeRange> forStatusCodes) =>
            Receive<NoContent>().Build(NoContentSerializer.DefaultFactory, forStatusCodes);

        /// <summary>
        ///     Declares that the response to this request may have a content of type <typeparamref name="T6"/>.
        ///     This returns a builder instance which requires you to specify additional information about
        ///     the possible response (for example, for which status codes the type is a possible result).
        /// </summary>
        /// <typeparam name="T6">
        ///     The .NET type which may be returned by the response following this request.
        /// </typeparam>
        /// <returns>
        ///     An <see cref="ResponseTypeInfoBuilder{TRequest}"/> instance which must be used
        ///     to specify additional information about the possible response.
        /// </returns>
        public ResponseTypeInfoBuilder<ApiRequest<T1, T2, T3, T4, T5, T6>> Receive<T6>() =>
            new ResponseTypeInfoBuilder<ApiRequest<T1, T2, T3, T4, T5, T6>>(
                new ApiRequest<T1, T2, T3, T4, T5, T6>(this), typeof(T6));

        /// <summary>
        ///     Makes this request via the associated <see cref="ApiRequestBase.HttpClientProvider"/> and
        ///     returns the response returned by the server as an <see cref="ApiResponse{T1, T2, T3, T4, T5}"/>.
        /// </summary>
        /// <param name="completionOption">
        ///     Defines when the operation should complete (as soon as a response is
        ///     available or after reading the whole response content).
        /// </param>
        /// <param name="cancellationToken">
        ///     A cancellation token which can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     An <see cref="ApiResponse{T1, T2, T3, T4, T5}"/> instance which contains the response of the server.
        /// </returns>
        /// <exception cref="TaskCanceledException">
        ///     The operation was canceled via the <paramref name="cancellationToken"/>.
        /// </exception>
        [DebuggerStepThrough]
        public async Task<ApiResponse<T1, T2, T3, T4, T5>> FetchResponseAsync(
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default)
        {
            var res = await FetchHttpResponseAsync(completionOption, cancellationToken).ConfigureAwait(false);
            return new ApiResponse<T1, T2, T3, T4, T5>(res, PossibleResponseTypes);
        }

        /// <summary>
        ///     Makes this request via the associated <see cref="ApiRequestBase.HttpClientProvider"/>.
        ///     Afterwards, deserializes the content of the underlying HTTP content depending
        ///     on the response's HTTP status code.
        ///     This returns a variant which will hold one of the possible generic types or no value if
        ///     the response's status code could not be matched with one of them.
        /// </summary>
        /// <param name="completionOption">
        ///     Defines when the operation should complete (as soon as a response is
        ///     available or after reading the whole response content).
        /// </param>
        /// <param name="cancellationToken">
        ///     A cancellation token which can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     A <see cref="Variant{T1, T2, T3, T4, T5}"/> which either holds the deserialized value (if there
        ///     was a <see cref="ResponseTypeInfo"/> in the <see cref="ApiResponseBase.PossibleResponseTypes"/>
        ///     set which matches the response's status code) or no value (if no such info was found).
        /// </returns>
        /// <exception cref="TaskCanceledException">
        ///     The operation was canceled via the <paramref name="cancellationToken"/>.
        /// </exception>
        /// <exception cref="HttpContentSerializationException">
        ///     There was a resource to deserialize, but the underlying <see cref="IHttpContentDeserializer"/>
        ///     threw an exception while deserializing the resource.
        /// </exception>
        [DebuggerStepThrough]
        public async Task<Variant<T1, T2, T3, T4, T5>> FetchResourceAsync(
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default)
        {
            var response = await FetchResponseAsync(completionOption, cancellationToken).ConfigureAwait(false);
            return await response.DeserializeResourceAsync().ConfigureAwait(false);
        }

    }

    /// <summary>
    ///     Encapsulates information for building and making a request to a RESTful HTTP API.
    ///     A response to this request may lead to a response content of one of the generic type parameters.
    /// </summary>
    /// <typeparam name="T1">A type which may be returned by a response following this request.</typeparam>
    /// <typeparam name="T2">A type which may be returned by the response following this request.</typeparam>
    /// <typeparam name="T3">A type which may be returned by the response following this request.</typeparam>
    /// <typeparam name="T4">A type which may be returned by the response following this request.</typeparam>
    /// <typeparam name="T5">A type which may be returned by the response following this request.</typeparam>
    /// <typeparam name="T6">A type which may be returned by the response following this request.</typeparam>
    public sealed class ApiRequest<T1, T2, T3, T4, T5, T6> : ApiRequestBase
    {

        /// <summary>
        ///     Initializes a new instance of this class which upgrades and wraps an existing request.
        /// </summary>
        /// <param name="request">The request to be upgraded and wrapped.</param>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="request"/>
        /// </exception>
        internal ApiRequest(ApiRequest<T1, T2, T3, T4, T5> request)
            : base(request ?? throw new ArgumentNullException(nameof(request))) { }

        /// <summary>
        ///     Declares that the response to this request may have an empty HTTP content when
        ///     receiving a response with status code <c>204 No Content</c>.
        /// </summary>
        /// <returns>
        ///     An <see cref="ApiRequestBase"/>, upgraded with the <see cref="NoContent"/> type.
        /// </returns>
        public ApiRequest<T1, T2, T3, T4, T5, T6, NoContent> ReceiveNoContent() =>
            ReceiveNoContent(StatusCode.NoContent);

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
        public ApiRequest<T1, T2, T3, T4, T5, T6, NoContent> ReceiveNoContent(params StatusCodeRange[] forStatusCodes) =>
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
        public ApiRequest<T1, T2, T3, T4, T5, T6, NoContent> ReceiveNoContent(IEnumerable<StatusCodeRange> forStatusCodes) =>
            Receive<NoContent>().Build(NoContentSerializer.DefaultFactory, forStatusCodes);

        /// <summary>
        ///     Declares that the response to this request may have a content of type <typeparamref name="T7"/>.
        ///     This returns a builder instance which requires you to specify additional information about
        ///     the possible response (for example, for which status codes the type is a possible result).
        /// </summary>
        /// <typeparam name="T7">
        ///     The .NET type which may be returned by the response following this request.
        /// </typeparam>
        /// <returns>
        ///     An <see cref="ResponseTypeInfoBuilder{TRequest}"/> instance which must be used
        ///     to specify additional information about the possible response.
        /// </returns>
        public ResponseTypeInfoBuilder<ApiRequest<T1, T2, T3, T4, T5, T6, T7>> Receive<T7>() =>
            new ResponseTypeInfoBuilder<ApiRequest<T1, T2, T3, T4, T5, T6, T7>>(
                new ApiRequest<T1, T2, T3, T4, T5, T6, T7>(this), typeof(T7));

        /// <summary>
        ///     Makes this request via the associated <see cref="ApiRequestBase.HttpClientProvider"/> and
        ///     returns the response returned by the server as an <see cref="ApiResponse{T1, T2, T3, T4, T5, T6}"/>.
        /// </summary>
        /// <param name="completionOption">
        ///     Defines when the operation should complete (as soon as a response is
        ///     available or after reading the whole response content).
        /// </param>
        /// <param name="cancellationToken">
        ///     A cancellation token which can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     An <see cref="ApiResponse{T1, T2, T3, T4, T5, T6}"/> instance which contains the response of the server.
        /// </returns>
        /// <exception cref="TaskCanceledException">
        ///     The operation was canceled via the <paramref name="cancellationToken"/>.
        /// </exception>
        [DebuggerStepThrough]
        public async Task<ApiResponse<T1, T2, T3, T4, T5, T6>> FetchResponseAsync(
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default)
        {
            var res = await FetchHttpResponseAsync(completionOption, cancellationToken).ConfigureAwait(false);
            return new ApiResponse<T1, T2, T3, T4, T5, T6>(res, PossibleResponseTypes);
        }

        /// <summary>
        ///     Makes this request via the associated <see cref="ApiRequestBase.HttpClientProvider"/>.
        ///     Afterwards, deserializes the content of the underlying HTTP content depending
        ///     on the response's HTTP status code.
        ///     This returns a variant which will hold one of the possible generic types or no value if
        ///     the response's status code could not be matched with one of them.
        /// </summary>
        /// <param name="completionOption">
        ///     Defines when the operation should complete (as soon as a response is
        ///     available or after reading the whole response content).
        /// </param>
        /// <param name="cancellationToken">
        ///     A cancellation token which can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     A <see cref="Variant{T1, T2, T3, T4, T5, T6}"/> which either holds the deserialized value (if there
        ///     was a <see cref="ResponseTypeInfo"/> in the <see cref="ApiResponseBase.PossibleResponseTypes"/>
        ///     set which matches the response's status code) or no value (if no such info was found).
        /// </returns>
        /// <exception cref="TaskCanceledException">
        ///     The operation was canceled via the <paramref name="cancellationToken"/>.
        /// </exception>
        /// <exception cref="HttpContentSerializationException">
        ///     There was a resource to deserialize, but the underlying <see cref="IHttpContentDeserializer"/>
        ///     threw an exception while deserializing the resource.
        /// </exception>
        [DebuggerStepThrough]
        public async Task<Variant<T1, T2, T3, T4, T5, T6>> FetchResourceAsync(
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default)
        {
            var response = await FetchResponseAsync(completionOption, cancellationToken).ConfigureAwait(false);
            return await response.DeserializeResourceAsync().ConfigureAwait(false);
        }

    }

    /// <summary>
    ///     Encapsulates information for building and making a request to a RESTful HTTP API.
    ///     A response to this request may lead to a response content of one of the generic type parameters.
    /// </summary>
    /// <typeparam name="T1">A type which may be returned by a response following this request.</typeparam>
    /// <typeparam name="T2">A type which may be returned by the response following this request.</typeparam>
    /// <typeparam name="T3">A type which may be returned by the response following this request.</typeparam>
    /// <typeparam name="T4">A type which may be returned by the response following this request.</typeparam>
    /// <typeparam name="T5">A type which may be returned by the response following this request.</typeparam>
    /// <typeparam name="T6">A type which may be returned by the response following this request.</typeparam>
    /// <typeparam name="T7">A type which may be returned by the response following this request.</typeparam>
    public sealed class ApiRequest<T1, T2, T3, T4, T5, T6, T7> : ApiRequestBase
    {

        /// <summary>
        ///     Initializes a new instance of this class which upgrades and wraps an existing request.
        /// </summary>
        /// <param name="request">The request to be upgraded and wrapped.</param>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="request"/>
        /// </exception>
        internal ApiRequest(ApiRequest<T1, T2, T3, T4, T5, T6> request)
            : base(request ?? throw new ArgumentNullException(nameof(request))) { }

        /// <summary>
        ///     Declares that the response to this request may have an empty HTTP content when
        ///     receiving a response with status code <c>204 No Content</c>.
        /// </summary>
        /// <returns>
        ///     An <see cref="ApiRequestBase"/>, upgraded with the <see cref="NoContent"/> type.
        /// </returns>
        public ApiRequest<T1, T2, T3, T4, T5, T6, T7, NoContent> ReceiveNoContent() =>
            ReceiveNoContent(StatusCode.NoContent);

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
        public ApiRequest<T1, T2, T3, T4, T5, T6, T7, NoContent> ReceiveNoContent(params StatusCodeRange[] forStatusCodes) =>
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
        public ApiRequest<T1, T2, T3, T4, T5, T6, T7, NoContent> ReceiveNoContent(IEnumerable<StatusCodeRange> forStatusCodes) =>
            Receive<NoContent>().Build(NoContentSerializer.DefaultFactory, forStatusCodes);

        /// <summary>
        ///     Declares that the response to this request may have a content of type <typeparamref name="T8"/>.
        ///     This returns a builder instance which requires you to specify additional information about
        ///     the possible response (for example, for which status codes the type is a possible result).
        /// </summary>
        /// <typeparam name="T8">
        ///     The .NET type which may be returned by the response following this request.
        /// </typeparam>
        /// <returns>
        ///     An <see cref="ResponseTypeInfoBuilder{TRequest}"/> instance which must be used
        ///     to specify additional information about the possible response.
        /// </returns>
        public ResponseTypeInfoBuilder<ApiRequest<T1, T2, T3, T4, T5, T6, T7, T8>> Receive<T8>() =>
            new ResponseTypeInfoBuilder<ApiRequest<T1, T2, T3, T4, T5, T6, T7, T8>>(
                new ApiRequest<T1, T2, T3, T4, T5, T6, T7, T8>(this), typeof(T8));

        /// <summary>
        ///     Makes this request via the associated <see cref="ApiRequestBase.HttpClientProvider"/> and
        ///     returns the response returned by the server as an <see cref="ApiResponse{T1, T2, T3, T4, T5, T6, T7}"/>.
        /// </summary>
        /// <param name="completionOption">
        ///     Defines when the operation should complete (as soon as a response is
        ///     available or after reading the whole response content).
        /// </param>
        /// <param name="cancellationToken">
        ///     A cancellation token which can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     An <see cref="ApiResponse{T1, T2, T3, T4, T5, T6, T7}"/> instance which contains the response of the server.
        /// </returns>
        /// <exception cref="TaskCanceledException">
        ///     The operation was canceled via the <paramref name="cancellationToken"/>.
        /// </exception>
        [DebuggerStepThrough]
        public async Task<ApiResponse<T1, T2, T3, T4, T5, T6, T7>> FetchResponseAsync(
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default)
        {
            var res = await FetchHttpResponseAsync(completionOption, cancellationToken).ConfigureAwait(false);
            return new ApiResponse<T1, T2, T3, T4, T5, T6, T7>(res, PossibleResponseTypes);
        }

        /// <summary>
        ///     Makes this request via the associated <see cref="ApiRequestBase.HttpClientProvider"/>.
        ///     Afterwards, deserializes the content of the underlying HTTP content depending
        ///     on the response's HTTP status code.
        ///     This returns a variant which will hold one of the possible generic types or no value if
        ///     the response's status code could not be matched with one of them.
        /// </summary>
        /// <param name="completionOption">
        ///     Defines when the operation should complete (as soon as a response is
        ///     available or after reading the whole response content).
        /// </param>
        /// <param name="cancellationToken">
        ///     A cancellation token which can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     A <see cref="Variant{T1, T2, T3, T4, T5, T6, T7}"/> which either holds the deserialized value (if there
        ///     was a <see cref="ResponseTypeInfo"/> in the <see cref="ApiResponseBase.PossibleResponseTypes"/>
        ///     set which matches the response's status code) or no value (if no such info was found).
        /// </returns>
        /// <exception cref="TaskCanceledException">
        ///     The operation was canceled via the <paramref name="cancellationToken"/>.
        /// </exception>
        /// <exception cref="HttpContentSerializationException">
        ///     There was a resource to deserialize, but the underlying <see cref="IHttpContentDeserializer"/>
        ///     threw an exception while deserializing the resource.
        /// </exception>
        [DebuggerStepThrough]
        public async Task<Variant<T1, T2, T3, T4, T5, T6, T7>> FetchResourceAsync(
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default)
        {
            var response = await FetchResponseAsync(completionOption, cancellationToken).ConfigureAwait(false);
            return await response.DeserializeResourceAsync().ConfigureAwait(false);
        }

    }

    /// <summary>
    ///     Encapsulates information for building and making a request to a RESTful HTTP API.
    ///     A response to this request may lead to a response content of one of the generic type parameters.
    /// </summary>
    /// <typeparam name="T1">A type which may be returned by a response following this request.</typeparam>
    /// <typeparam name="T2">A type which may be returned by the response following this request.</typeparam>
    /// <typeparam name="T3">A type which may be returned by the response following this request.</typeparam>
    /// <typeparam name="T4">A type which may be returned by the response following this request.</typeparam>
    /// <typeparam name="T5">A type which may be returned by the response following this request.</typeparam>
    /// <typeparam name="T6">A type which may be returned by the response following this request.</typeparam>
    /// <typeparam name="T7">A type which may be returned by the response following this request.</typeparam>
    /// <typeparam name="T8">A type which may be returned by the response following this request.</typeparam>
    public sealed class ApiRequest<T1, T2, T3, T4, T5, T6, T7, T8> : ApiRequestBase
    {

        /// <summary>
        ///     Initializes a new instance of this class which upgrades and wraps an existing request.
        /// </summary>
        /// <param name="request">The request to be upgraded and wrapped.</param>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="request"/>
        /// </exception>
        internal ApiRequest(ApiRequest<T1, T2, T3, T4, T5, T6, T7> request)
            : base(request ?? throw new ArgumentNullException(nameof(request))) { }

        /// <summary>
        ///     Makes this request via the associated <see cref="ApiRequestBase.HttpClientProvider"/> and
        ///     returns the response returned by the server as an <see cref="ApiResponse{T1, T2, T3, T4, T5, T6, T7, T8}"/>.
        /// </summary>
        /// <param name="completionOption">
        ///     Defines when the operation should complete (as soon as a response is
        ///     available or after reading the whole response content).
        /// </param>
        /// <param name="cancellationToken">
        ///     A cancellation token which can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     An <see cref="ApiResponse{T1, T2, T3, T4, T5, T6, T7, T8}"/> instance which contains the response of the server.
        /// </returns>
        /// <exception cref="TaskCanceledException">
        ///     The operation was canceled via the <paramref name="cancellationToken"/>.
        /// </exception>
        [DebuggerStepThrough]
        public async Task<ApiResponse<T1, T2, T3, T4, T5, T6, T7, T8>> FetchResponseAsync(
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default)
        {
            var res = await FetchHttpResponseAsync(completionOption, cancellationToken).ConfigureAwait(false);
            return new ApiResponse<T1, T2, T3, T4, T5, T6, T7, T8>(res, PossibleResponseTypes);
        }

        /// <summary>
        ///     Makes this request via the associated <see cref="ApiRequestBase.HttpClientProvider"/>.
        ///     Afterwards, deserializes the content of the underlying HTTP content depending
        ///     on the response's HTTP status code.
        ///     This returns a variant which will hold one of the possible generic types or no value if
        ///     the response's status code could not be matched with one of them.
        /// </summary>
        /// <param name="completionOption">
        ///     Defines when the operation should complete (as soon as a response is
        ///     available or after reading the whole response content).
        /// </param>
        /// <param name="cancellationToken">
        ///     A cancellation token which can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     A <see cref="Variant{T1, T2, T3, T4, T5, T6, T7, T8}"/> which either holds the deserialized value (if there
        ///     was a <see cref="ResponseTypeInfo"/> in the <see cref="ApiResponseBase.PossibleResponseTypes"/>
        ///     set which matches the response's status code) or no value (if no such info was found).
        /// </returns>
        /// <exception cref="TaskCanceledException">
        ///     The operation was canceled via the <paramref name="cancellationToken"/>.
        /// </exception>
        /// <exception cref="HttpContentSerializationException">
        ///     There was a resource to deserialize, but the underlying <see cref="IHttpContentDeserializer"/>
        ///     threw an exception while deserializing the resource.
        /// </exception>
        [DebuggerStepThrough]
        public async Task<Variant<T1, T2, T3, T4, T5, T6, T7, T8>> FetchResourceAsync(
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default)
        {
            var response = await FetchResponseAsync(completionOption, cancellationToken).ConfigureAwait(false);
            return await response.DeserializeResourceAsync().ConfigureAwait(false);
        }

    }

#pragma warning restore CA2000 // Dispose objects before losing scope

}
