namespace ReqRest.Client
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using ReqRest.Builders;
    using ReqRest.Client.Resources;

    /// <summary>
    ///     Defines the shared members of a request builder abstraction for a RESTful HTTP API.
    /// </summary>
    public abstract class ApiRequestBase : HttpRequestMessageBuilder
    {
        
        private Func<HttpClient> _httpClientProvider;

        /// <summary>
        ///     Gets or sets a function which returns an <see cref="HttpClient"/> instance
        ///     which will ultimately be used to send the <see cref="HttpRequestMessage"/> for
        ///     executing the API request.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public Func<HttpClient> HttpClientProvider
        {
            get => _httpClientProvider;
            set => _httpClientProvider = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        ///     Gets a list of elements which declare what possible .NET types the API may
        ///     return for this request, depending on the result's status code.
        /// </summary>
        public IReadOnlyCollection<ResponseTypeInfo> PossibleResponseTypes { get; }
        
        /// <summary>
        ///     Gets a modifiable list of elements which declare what possible .NET types the API may
        ///     return for this request, depending on the result's status code.
        /// </summary>
        internal IList<ResponseTypeInfo> PossibleResponseTypesInternal { get; }

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
        public ApiRequestBase(Func<HttpClient> httpClientProvider, HttpRequestMessage? httpRequestMessage = null)
            : base(httpRequestMessage)
        {
            _httpClientProvider = httpClientProvider ?? throw new ArgumentNullException(nameof(httpClientProvider));
            PossibleResponseTypesInternal = new ResponseTypeInfoCollection();
            PossibleResponseTypes = new ReadOnlyCollection<ResponseTypeInfo>(PossibleResponseTypesInternal);
        }

        /// <summary>
        ///     Initializes a new <see cref="ApiRequestBase"/> instance which re-uses the properties
        ///     from the specified request.
        ///     Used internally to wrap an upgraded request.
        /// </summary>
        private protected ApiRequestBase(ApiRequestBase request)
            : this(request.HttpClientProvider, request.HttpRequestMessage)
        {
            // Must use ToList() here. The response types infos should not be modifiable from the outside.
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
            var httpClient = HttpClientProvider() ?? throw new InvalidOperationException(ExceptionStrings.HttpClientProvider_Returned_Null);
            return httpClient.SendAsync(HttpRequestMessage, completionOption, cancellationToken);
        }

    }

}
