namespace ReqRest
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
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
    /// <typeparam name="T6">A potential type which may be deserialized from this response.</typeparam>
    /// <typeparam name="T7">A potential type which may be deserialized from this response.</typeparam>
    /// <typeparam name="T8">A potential type which may be deserialized from this response.</typeparam>
    public sealed class ApiResponse<T1, T2, T3, T4, T5, T6, T7, T8> : ApiResponseBase
    {

        /// <summary>
        ///     Initializes a new <see cref="ApiResponse{T1, T2, T3, T4, T5, T6, T7, T8}"/> instance with the specified values.
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
        /// <returns>
        ///     The deserialized resource represented through a <see cref="Variant{T1, T2, T3, T4, T5, T6, T7, T8}"/>
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
        public async Task<Variant<T1, T2, T3, T4, T5, T6, T7, T8>> DeserializeResourceAsync()
        {
            if (CanDeserializeResource<T1>())
            {
                return await DeserializeResourceAsync<T1>().ConfigureAwait(false);
            }
            else if (CanDeserializeResource<T2>())
            {
                return await DeserializeResourceAsync<T2>().ConfigureAwait(false);
            }
            else if (CanDeserializeResource<T3>())
            {
                return await DeserializeResourceAsync<T3>().ConfigureAwait(false);
            }
            else if (CanDeserializeResource<T4>())
            {
                return await DeserializeResourceAsync<T4>().ConfigureAwait(false);
            }
            else if (CanDeserializeResource<T5>())
            {
                return await DeserializeResourceAsync<T5>().ConfigureAwait(false);
            }
            else if (CanDeserializeResource<T6>())
            {
                return await DeserializeResourceAsync<T6>().ConfigureAwait(false);
            }
            else if (CanDeserializeResource<T7>())
            {
                return await DeserializeResourceAsync<T7>().ConfigureAwait(false);
            }
            else if (CanDeserializeResource<T8>())
            {
                return await DeserializeResourceAsync<T8>().ConfigureAwait(false);
            }
            else
            {
                return new Variant<T1, T2, T3, T4, T5, T6, T7, T8>();
            }
        }

    }

}
