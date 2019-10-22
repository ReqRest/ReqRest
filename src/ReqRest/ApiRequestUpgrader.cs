namespace ReqRest
{
    using System;
    using System.Collections.Generic;
    using ReqRest.Http;
    using ReqRest.Serializers;

    /// <summary>
    ///     A class which upgrades an arbitrary <see cref="ApiRequestBase"/> instance by adding
    ///     a new <see cref="ResponseTypeDescriptor"/> describing the next possible result which may
    ///     be returned by a RESTful HTTP API to the request's <see cref="ApiRequestBase.PossibleResponseTypes"/>.
    ///     
    ///     See remarks for more information on request upgrading.
    /// </summary>
    /// <typeparam name="TUpgradedRequest">The request type which gets returned after upgrading.</typeparam>
    /// <remarks>
    ///     This class has ultimately been added to allow fluent request upgrading.
    ///     Request upgrading refers to adding type information to an <see cref="ApiRequestBase"/>
    ///     instance.
    ///     
    ///     For example, assume that a user starts with a plain <see cref="ApiRequest"/> instance as
    ///     in the following code:
    ///     
    ///     <code>
    ///     using ReqRest.Serializers.Json; 
    /// 
    ///     ApiRequest request = new ApiRequest(() => new HttpClient());
    ///     ApiRequest&lt;MyDto&gt; upgraded = request.Receive&lt;MyDto&gt;().AsJson(200);
    ///     </code>
    ///     
    ///     The process of adding type information, namely the <c>MyDto</c> type, is refered to as
    ///     upgrading the request.
    ///     
    ///     Simply adding a generic type parameter is not enough for correctly being able to
    ///     correctly deserialize any HTTP response though - more information is required for this,
    ///     for example which status code maps to which type (the <see cref="ResponseTypeDescriptor"/>
    ///     lists all the information which are required).
    ///     Adding these information is the task of this <see cref="ApiRequestUpgrader{TUpgradedRequest}"/>
    ///     class. Once returned via a method like <see cref="ApiRequest.Receive{T1}"/>, it is able
    ///     to add a new <see cref="ResponseTypeDescriptor"/> to the <see cref="ApiRequestBase.PossibleResponseTypes"/>
    ///     list and thus upgrades the request with new type information.
    /// </remarks>
    /// <seealso cref="ResponseTypeDescriptor"/>
    public sealed class ApiRequestUpgrader<TUpgradedRequest> where TUpgradedRequest : ApiRequestBase
    {

        private readonly TUpgradedRequest _upgradedRequest;
        private readonly Type _newResponseType;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ApiRequestUpgrader{TUpgradedRequest}"/>
        ///     class which adds additional information about which HTTP status codes are required
        ///     for the specified type to be received and how it can be deserialized to the specified
        ///     request.
        /// </summary>
        /// <param name="upgradedRequest">
        ///     The request which is supposed to be enhanced with additional information about the
        ///     specified response type.
        ///     This request is returned to the user once the upgrading phase is done, i.e. it is
        ///     supposed to be the result of upgrading.
        /// </param>
        /// <param name="newResponseType">
        ///     The response type with which the request has been upgraded.
        ///     The request is enhanced with additional information about this response type.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="upgradedRequest"/>
        ///     * <paramref name="newResponseType"/>
        /// </exception>
        public ApiRequestUpgrader(TUpgradedRequest upgradedRequest, Type newResponseType)
        {
            _upgradedRequest = upgradedRequest ?? throw new ArgumentNullException(nameof(upgradedRequest));
            _newResponseType = newResponseType ?? throw new ArgumentNullException(nameof(newResponseType));
        }

        /// <summary>
        ///     <para>
        ///         Upgrades the request with which this class has been initialized with the given
        ///         parameters and returns that request.
        ///     </para>
        ///     <para>
        ///         If you are not creating a custom <see cref="IHttpContentDeserializer"/>, you should
        ///         most likely not call this method directly. Use one of the available extension
        ///         methods instead, for example the <c>AsJson</c> method if you are using one of
        ///         ReqRest's JSON serializers.
        ///     </para>
        /// </summary>
        /// <param name="httpContentDeserializerProvider">
        ///     A function which returns an <see cref="IHttpContentDeserializer"/> that should
        ///     be used to deserialize a response's HTTP content into the corresponding .NET object
        ///     of the newly added type.
        /// </param>
        /// <param name="forStatusCodes">
        ///     A set of HTTP status code ranges for which the newly added type can
        ///     be deserialized from a response's HTTP content.
        /// </param>
        /// <returns>
        ///     The same request instance that was specified in the constructor.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="httpContentDeserializerProvider"/>
        ///     * <paramref name="forStatusCodes"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="forStatusCodes"/> is empty.
        /// </exception>
        public TUpgradedRequest Upgrade(
            Func<IHttpContentDeserializer> httpContentDeserializerProvider, 
            IEnumerable<StatusCodeRange> forStatusCodes)
        {
            _ = httpContentDeserializerProvider ?? throw new ArgumentNullException(nameof(httpContentDeserializerProvider));
            _ = forStatusCodes ?? throw new ArgumentNullException(nameof(forStatusCodes));

            var responseTypeDescriptor = new ResponseTypeDescriptor(_newResponseType, forStatusCodes, httpContentDeserializerProvider);
            _upgradedRequest.PossibleResponseTypesInternal.Add(responseTypeDescriptor);
            return _upgradedRequest;
        }

    }

}
