namespace ReqRest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     Defines the shared members of a request builder abstraction for a RESTful HTTP API.
    /// </summary>
    public abstract class ApiRequestBase : HttpRequestMessageBuilder
    {
        
        private HttpClient _httpClient;

        /// <summary>
        ///     Gets or sets an <see cref="System.Net.Http.HttpClient"/> instance which will
        ///     ultimately be used to send the <see cref="HttpRequestMessage"/> for executing
        ///     the API request.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public HttpClient HttpClient
        {
            get => _httpClient;
            set => _httpClient = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        ///     Gets a list of elements which declare what possible .NET types the API may
        ///     return for this request, depending on the result's status code.
        /// </summary>
        public IEnumerable<ResponseTypeInfo> PossibleResponseTypes => PossibleResponseTypesInternal;
        
        /// <summary>
        ///     Gets a modifiable list of elements which declare what possible .NET types the API may
        ///     return for this request, depending on the result's status code.
        /// </summary>
        internal IList<ResponseTypeInfo> PossibleResponseTypesInternal { get; }

        /// <summary>
        ///     Initializes a new <see cref="ApiRequestBase"/> instance with the specified
        ///     initial property values.
        /// </summary>
        /// <param name="httpClient">
        ///     An <see cref="System.Net.Http.HttpClient"/> instance which will
        ///     ultimately be used to send the <see cref="HttpRequestMessage"/> for executing
        ///     the API request.
        /// </param>
        /// <param name="httpRequestMessage">
        ///     The request from which the builder starts building.
        ///     If <see langword="null"/>, a new instance is created instead.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="httpClient"/>
        /// </exception>
        public ApiRequestBase(HttpClient httpClient, HttpRequestMessage? httpRequestMessage = null)
            : base(httpRequestMessage)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            PossibleResponseTypesInternal = new ResponseTypeInfoCollection();
        }

        /// <summary>
        ///     Initializes a new <see cref="ApiRequestBase"/> instance which re-uses the properties
        ///     from the specified request.
        ///     Used internally to wrap an upgraded request,
        /// </summary>
        private protected ApiRequestBase(ApiRequestBase request)
            : this(request.HttpClient, request.HttpRequestMessage)
        {
            var responseTypes = request.PossibleResponseTypes.ToList();
            PossibleResponseTypesInternal = new ResponseTypeInfoCollection(responseTypes);
        }

        /// <summary>
        ///     Uses the request's HttpClient to make the request and fetch the HTTP response.
        /// </summary>
        private protected Task<HttpResponseMessage> FetchHttpResponseAsync(
            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
            CancellationToken cancellationToken = default)
        {
            return HttpClient.SendAsync(HttpRequestMessage, completionOption, cancellationToken);
        }

    }

}
