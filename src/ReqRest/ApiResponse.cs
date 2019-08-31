namespace ReqRest
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using NCommons.Monads;
    using ReqRest.Serializers;

    /// <summary>
    ///     Encapsulates information which were returned by a RESTful HTTP API after making
    ///     a request to it.
    /// </summary>
    public sealed class ApiResponse : ApiResponseBase
    {

        /// <summary>
        ///     Initializes a new <see cref="ApiResponse"/> instance with the specified values.
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

    }

}
