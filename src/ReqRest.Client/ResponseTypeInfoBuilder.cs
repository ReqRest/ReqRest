namespace ReqRest.Client
{
    using System;
    using System.Collections.Generic;
    using ReqRest.Http;
    using ReqRest.Serializers;

    /// <summary>
    ///     A builder which gets returned by an <see cref="ApiRequest"/> (or one of its generic variants)
    ///     instance when upgrading it via the <see cref="ApiRequest.Receive{T}"/> method(s).
    ///     This builder allows enhancing the possible response type with information like which
    ///     status codes are required for receiving that response content type.
    /// </summary>
    /// <typeparam name="TRequest">The request type which gets returned after upgrading.</typeparam>
    public sealed class ResponseTypeInfoBuilder<TRequest> where TRequest : ApiRequestBase
    {

        private readonly TRequest _request;
        private readonly Type _responseType;

        /// <summary>
        ///     Initializes a new builder instance which enhances the upgrade information of the
        ///     specified <paramref name="request"/>.
        /// </summary>
        /// <param name="request">The upgraded request.</param>
        /// <param name="responseType">The type with which the request was just being upgraded.</param>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="request"/>
        ///     * <paramref name="responseType"/>
        /// </exception>
        public ResponseTypeInfoBuilder(TRequest request, Type responseType)
        {
            _request = request ?? throw new ArgumentNullException(nameof(request));
            _responseType = responseType ?? throw new ArgumentNullException(nameof(responseType));
        }

        /// <summary>
        ///     Creates a new <see cref="ResponseTypeInfo"/> instance with the specified values
        ///     and adds it to the request's <see cref="ApiRequestBase.PossibleResponseTypes"/>
        ///     list.
        ///     Afterwards, the request is returned to allow continuous request info building.
        /// </summary>
        /// <param name="responseDeserializerFactory">
        ///     A function which returns an <see cref="IHttpContentDeserializer"/> that must
        ///     be used to deserialize the .NET object from an HTTP response.
        /// </param>
        /// <param name="forStatusCodes">
        ///     A set of status codes for which the response type is the result.
        /// </param>
        /// <returns>
        ///     The api request instance with which this class was initialized.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="responseDeserializerFactory"/>
        ///     * <paramref name="forStatusCodes"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="forStatusCodes"/> is empty.
        /// </exception>
        public TRequest Build(
            Func<IHttpContentDeserializer> responseDeserializerFactory, 
            IEnumerable<StatusCodeRange> forStatusCodes)
        {
            _ = responseDeserializerFactory ?? throw new ArgumentNullException(nameof(responseDeserializerFactory));
            _ = forStatusCodes ?? throw new ArgumentNullException(nameof(forStatusCodes));

            var responseTypeInfo = new ResponseTypeInfo(_responseType, forStatusCodes, responseDeserializerFactory);
            _request.PossibleResponseTypesInternal.Add(responseTypeInfo);
            return _request;
        }

    }

}
