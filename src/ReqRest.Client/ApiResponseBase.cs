namespace ReqRest.Client
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ReqRest.Builders;
    using ReqRest.Client.Resources;
    using ReqRest.Serializers;

    /// <summary>
    ///     Defines the shared members of a response which was returned by a RESTful HTTP API
    ///     after making a request with this library.
    /// </summary>
    public abstract class ApiResponseBase : HttpResponseMessageBuilder
    {
        // Why does this class extend HttpResponseMessageBuilder?
        // The HttpResponseMessage (which should always be exposed by this class) is fully mutable.
        // This means that this response's content could be modified anyway. Since this is possible,
        // we might aswell give access to the full builder API.
        // There may also be situations where an ApiResponse is passed around by a user.
        // I can think of scenarios where the response gets modified before being passed to some methods.
        // For this, the builder APIs are quite helpful.

        /// <summary>
        ///     Gets or sets the HTTP status code of the underlying <see cref="HttpResponseMessage"/>
        ///     as an <see cref="int"/> value for convenient access.
        /// </summary>
        public int StatusCode
        {
            get => (int)HttpResponseMessage.StatusCode;
            set => HttpResponseMessage.StatusCode = (HttpStatusCode)value;
        }

        /// <summary>
        ///     Gets a set of elements that declare which .NET types may have been returned by the
        ///     HTTP API in this response.
        /// </summary>
        protected internal IReadOnlyCollection<ResponseTypeInfo> PossibleResponseTypes { get; }

        /// <summary>
        ///     Gets a <see cref="ResponseTypeInfo"/> from the <see cref="PossibleResponseTypes"/>
        ///     set which is the most appropriate one for the response's status code.
        ///     
        ///     This is <see langword="null"/> if the <see cref="PossibleResponseTypes"/>
        ///     property doesn't contain any information which matches the response's status code.
        /// </summary>
        protected internal ResponseTypeInfo? CurrentResponseTypeInfo { get; }

        /// <summary>
        ///     Initializes a new <see cref="ApiResponseBase"/> instance with the specified values.
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
        public ApiResponseBase(
            HttpResponseMessage? httpResponseMessage,
            IEnumerable<ResponseTypeInfo>? possibleResponseTypes)
            : base(httpResponseMessage)
        {
            PossibleResponseTypes = new ReadOnlyCollection<ResponseTypeInfo>(
                possibleResponseTypes?.ToArray() ?? Array.Empty<ResponseTypeInfo>()
            );
            CurrentResponseTypeInfo = FindMostAppropriateResponseTypeInfo();
        }

        private ResponseTypeInfo? FindMostAppropriateResponseTypeInfo()
        {
            var possibleTypes =
                from info in PossibleResponseTypes
                from statusCode in info.StatusCodes
                where statusCode.IsInRange(StatusCode)
                select new { StatusCode = statusCode, Info = info };

            return possibleTypes
                .OrderByDescending(x => x.StatusCode, StatusCodeRangeSpecificnessComparer.Default)
                .Select(x => x.Info)
                .FirstOrDefault();
        }

        /// <summary>
        ///     Returns a value indicating whether a resource of the specified type <typeparamref name="T"/>
        ///     can be deserialized from this response's content.
        ///     The value is determined based on <see cref="CurrentResponseTypeInfo"/>.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the resource to be deserialized.
        /// </typeparam>
        /// <returns>
        ///     <see langword="true"/> if a resource of the specified type can be deserialized;
        ///     <see langword="false"/> if not.
        /// </returns>
        private protected bool CanDeserializeResource<T>() =>
               CurrentResponseTypeInfo != null
            && typeof(T).IsAssignableFrom(CurrentResponseTypeInfo.ResponseType);

        /// <summary>
        ///     Attempts to deserialize the response to the specified type <typeparamref name="T"/>
        ///     and, depending on the success, returns either the deserialized resource or information
        ///     about an exception which occured during deserialization.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the resource to be deserialized.
        /// </typeparam>
        /// <returns>
        ///     The deserialized resource or information about a deserialization exception.
        /// </returns>
        private protected async Task<T> DeserializeResourceAsync<T>()
        {
            if (CurrentResponseTypeInfo is null)
            {
                throw new InvalidOperationException(ExceptionStrings.ApiResponse_NoResponseTypeInfoForResponse);
            }

            var deserializer = CurrentResponseTypeInfo.ResponseDeserializerFactory();
            if (deserializer is null)
            {
                throw new InvalidOperationException(ExceptionStrings.ApiResponse_InvalidResponseDeserializer);
            }

            try
            {
                return await deserializer.DeserializeAsync<T>(HttpResponseMessage.Content).ConfigureAwait(false);
            }
            catch (Exception ex) when (!(ex is HttpContentSerializationException))
            {
                // Ideally, the deserializer throws this exception by himself, but we cannot count on that.
                throw new HttpContentSerializationException(null, ex);
            }
        }

    }

}
