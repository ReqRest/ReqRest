namespace ReqRest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ReqRest.Http;
    using ReqRest.Resources;
    using ReqRest.Serializers;

    /// <summary>
    ///     Describes one possible result that a RESTful HTTP API may return when making a specific request.
    ///     
    ///     In essence, this class provides information about which .NET type may be returned after
    ///     making a request, for which status codes this type is returned and finally how this
    ///     type can be deserialized from an HTTP content into a .NET object.
    ///     
    ///     These information are used by ReqRest to automatically deserialize the HTTP content
    ///     of a response, depending on the response's HTTP status code.
    /// </summary>
    public sealed class ResponseTypeDescriptor
    {

        /// <summary>
        ///     Gets the .NET type which can be deserialized from a response's HTTP content, given
        ///     that its HTTP status code falls into one of the ranges defined by <see cref="StatusCodes"/>.
        /// </summary>
        public Type ResponseType { get; }

        /// <summary>
        ///     Gets a set of HTTP status code ranges for which the <see cref="ResponseType"/> can
        ///     be deserialized from a response's HTTP content.
        ///     
        ///     This is a <i>set</i> of distinct values, i.e. no range will appear more than
        ///     once when iterating through the values of this property.
        /// </summary>
        public IReadOnlyCollection<StatusCodeRange> StatusCodes { get; }

        /// <summary>
        ///     Gets a function which returns an <see cref="IHttpContentDeserializer"/> that should
        ///     be used to deserialize a response's HTTP content into the corresponding .NET object
        ///     of type <see cref="ResponseType"/>.
        /// </summary>
        public Func<IHttpContentDeserializer> HttpContentDeserializerProvider { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResponseTypeDescriptor"/> class
        ///     which describes that a request may return an HTTP content which can be deserialized
        ///     to an object of type <paramref name="responseType"/>, given that its HTTP status code
        ///     falls into one of the ranges defined by <paramref name="statusCodes"/>.
        /// </summary>
        /// <param name="responseType">
        ///     The .NET type which can be deserialized from a response's HTTP content, given
        ///     that its HTTP status code falls into one of the ranges defined by <paramref name="statusCodes"/>.
        /// </param>
        /// <param name="statusCodes">
        ///     A set of HTTP status code ranges for which the <paramref name="responseType"/> can
        ///     be deserialized from a response's HTTP content.
        /// </param>
        /// <param name="httpContentDeserializerProvider">
        ///     A function which returns an <see cref="IHttpContentDeserializer"/> that should
        ///     be used to deserialize a response's HTTP content into the corresponding .NET object
        ///     of type <paramref name="responseType"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="responseType"/>
        ///     * <paramref name="statusCodes"/>
        ///     * <paramref name="httpContentDeserializerProvider"/>
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="statusCodes"/> is empty.
        /// </exception>
        public ResponseTypeDescriptor(
            Type responseType,
            IEnumerable<StatusCodeRange> statusCodes,
            Func<IHttpContentDeserializer> httpContentDeserializerProvider)
        {
            _ = responseType ?? throw new ArgumentNullException(nameof(responseType));
            _ = statusCodes ?? throw new ArgumentNullException(nameof(statusCodes));
            _ = httpContentDeserializerProvider ?? throw new ArgumentNullException(nameof(httpContentDeserializerProvider));

            if (!statusCodes.Any())
            {
                throw new ArgumentException(
                    ExceptionStrings.ResponseTypeDescriptor_MustProvideAtLeastOneStatusCode(),
                    nameof(statusCodes)
                );
            }

            ResponseType = responseType;
            HttpContentDeserializerProvider = httpContentDeserializerProvider;
            StatusCodes = statusCodes.Distinct().ToList().AsReadOnly();
        }

        /// <summary>
        ///     Returns a string representation of this response type info.
        /// </summary>
        /// <returns>A string representing this instance.</returns>
        public override string ToString()
        {
            return $"{ResponseType.Name}: {string.Join(", ", StatusCodes)}";
        }

    }

}
