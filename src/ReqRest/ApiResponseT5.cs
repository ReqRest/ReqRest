namespace ReqRest
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using NCommons.Monads;
    using ReqRest.Serializers;

    /// <summary>
    ///     Encapsulates information which were returned by a RESTful HTTP API after making
    ///     a request to it.
    ///     This response class declares that it may hold a resource of one of the generic type parameters.
    /// </summary>
    /// <typeparam name="T1">A potential type which may be deserialized from this response.</typeparam>
    /// <typeparam name="T2">A potential type which may be deserialized from this response.</typeparam>
    /// <typeparam name="T3">A potential type which may be deserialized from this response.</typeparam>
    /// <typeparam name="T4">A potential type which may be deserialized from this response.</typeparam>
    /// <typeparam name="T5">A potential type which may be deserialized from this response.</typeparam>
    public sealed class ApiResponse<T1, T2, T3, T4, T5> : ApiResponseBase
    {

        /// <summary>
        ///     Initializes a new <see cref="ApiResponse{T1, T2, T3, T4, T5}"/> instance with the specified values.
        /// </summary>
        /// <param name="httpResponseMessage">
        ///     The <see cref="HttpResponseMessage"/> which was the underlying
        ///     result returned by an <see cref="HttpClient"/> after making the associated request.
        ///     If <see langword="null"/>, a new instance is created instead.
        /// </param>
        /// <param name="possibleResponseTypes">
        ///     A set of elements that declare which .NET types may have been returned by the
        ///     HTTP API in this response.
        ///     If <see langword="null"/>, an empty set is used instead.
        /// </param>
        internal ApiResponse(
            HttpResponseMessage? httpResponseMessage,
            IEnumerable<ResponseTypeInfo>? possibleResponseTypes)
            : base(httpResponseMessage, possibleResponseTypes) { }

        /// <summary>
        ///     Deserializes the HTTP content and returns the deserialized resource.
        /// </summary>
        /// <param name="cancellationToken">
        ///     A cancellation token which can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     The deserialized resource represented through a <see cref="Variant{T1, T2, T3, T4, T5}"/>
        ///     which holds a value that matches the response type declared for the response's HTTP status code.
        ///     This variant is empty if the response's HTTP status code doesn't match any declared one.
        /// </returns>
        /// <exception cref="HttpContentSerializationException">
        ///     There was a resource to deserialize, but the underlying <see cref="IHttpContentDeserializer"/>
        ///     threw an exception while deserializing the resource.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     <see cref="ApiResponseBase.GetCurrentResponseTypeInfo"/> returned <see langword="null"/>.
        ///     
        ///     --or--
        ///     
        ///     The <see cref="ResponseTypeInfo.ResponseDeserializerFactory"/> of the 
        ///     <see cref="ResponseTypeInfo"/> returned by 
        ///     <see cref="ApiResponseBase.GetCurrentResponseTypeInfo"/> returned <see langword="null"/>.
        /// </exception>
        /// <exception cref="TaskCanceledException">
        ///     The operation was canceled via the <paramref name="cancellationToken"/>.
        /// </exception>
        public async Task<Variant<T1, T2, T3, T4, T5>> DeserializeResourceAsync(CancellationToken cancellationToken = default)
        {
            if (CanDeserializeResource<T1>())
            {
                return await DeserializeResourceAsync<T1>(cancellationToken).ConfigureAwait(false);
            }
            else if (CanDeserializeResource<T2>())
            {
                return await DeserializeResourceAsync<T2>(cancellationToken).ConfigureAwait(false);
            }
            else if (CanDeserializeResource<T3>())
            {
                return await DeserializeResourceAsync<T3>(cancellationToken).ConfigureAwait(false);
            }
            else if (CanDeserializeResource<T4>())
            {
                return await DeserializeResourceAsync<T4>(cancellationToken).ConfigureAwait(false);
            }
            else if (CanDeserializeResource<T5>())
            {
                return await DeserializeResourceAsync<T5>(cancellationToken).ConfigureAwait(false);
            }
            else
            {
                return new Variant<T1, T2, T3, T4, T5>();
            }
        }

    }

}
