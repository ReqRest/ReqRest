namespace ReqRest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using ReqRest.Http;
    using ReqRest.Resources;
    using ReqRest.Serializers;

    /// <summary>
    ///     Contains information about which .NET type a RESTful HTTP API returns for a given set of
    ///     status codes and how to deserialize that type from an <see cref="HttpContent"/>.
    /// </summary>
    public sealed class ResponseTypeInfo
    {

        /// <summary>
        ///     Gets the .NET type representation of the result that the API returns for the status
        ///     codes defined by <see cref="StatusCodes"/>.
        /// </summary>
        public Type ResponseType { get; }

        /// <summary>
        ///     Gets a set of status codes for which the <see cref="ResponseType"/> is the result.
        /// </summary>
        public ISet<StatusCodeRange> StatusCodes { get; }

        /// <summary>
        ///     Gets a function which returns an <see cref="IHttpContentDeserializer"/> that must
        ///     be used to deserialize the .NET object from an HTTP response.
        /// </summary>
        public Func<IHttpContentDeserializer> ResponseDeserializerFactory { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResponseTypeInfo"/> class
        ///     which specifies that an API returns an object of type <paramref name="responseType"/>
        ///     for the given <paramref name="statusCodes"/>.
        /// </summary>
        /// <param name="responseType">
        ///     The .NET type representation of the result that the API returns for the status
        ///     codes defined by <see cref="StatusCodes"/>.
        /// </param>
        /// <param name="statusCodes">
        ///     A set of status codes for which the <see cref="ResponseType"/> is the result.
        /// </param>
        /// <param name="responseDeserializerFactory">
        ///     A function which returns an <see cref="IHttpContentDeserializer"/> that must
        ///     be used to deserialize the .NET object from an HTTP response.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="responseType"/>
        ///     * <paramref name="statusCodes"/>
        ///     * <paramref name="responseDeserializerFactory"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="statusCodes"/> is empty.
        /// </exception>
        public ResponseTypeInfo(
            Type responseType,
            IEnumerable<StatusCodeRange> statusCodes,
            Func<IHttpContentDeserializer> responseDeserializerFactory)
        {
            _ = responseType ?? throw new ArgumentNullException(nameof(responseType));
            _ = statusCodes ?? throw new ArgumentNullException(nameof(statusCodes));
            _ = responseDeserializerFactory ?? throw new ArgumentNullException(nameof(responseDeserializerFactory));

            if (!statusCodes.Any())
            {
                throw new ArgumentException(
                    ExceptionStrings.ResponseTypeInfo_MustProvideAtLeastOneStatusCode(),
                    nameof(statusCodes)
                );
            }

            ResponseType = responseType;
            ResponseDeserializerFactory = responseDeserializerFactory;
            StatusCodes = new HashSet<StatusCodeRange>(statusCodes);
        }

        /// <summary>
        ///     Returns a string representation of this response type info.
        /// </summary>
        /// <returns>A string representing this instance.</returns>
        public override string ToString()
        {
            var ranges = StatusCodes.Select(r => $"({r})");
            return $"{ResponseType.Name}: {string.Join(", ", ranges)}";
        }

    }

}
