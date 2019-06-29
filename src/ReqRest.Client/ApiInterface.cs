namespace ReqRest.Client
{
    using System;
    using ReqRest.Builders;

    /// <summary>
    ///     Defines a base class for wrapping an interface of a RESTful HTTP API.
    /// </summary>
    public abstract class ApiInterface : IUrlProvider
    {

        private readonly Lazy<Uri> _urlLazy;

        /// <summary>
        ///     Gets the <see cref="ApiClient"/> which ultimately manages (or rather "contains")
        ///     this interface.
        ///     This client's configuration is supposed to be used when building requests.
        /// </summary>
        protected ApiClient Client { get; }

        /// <summary>
        ///     Gets an <see cref="IUrlProvider"/> which is the logical parent of this
        ///     interface.
        ///     The URL which is returned by this <see cref="IUrlProvider"/> is used as this
        ///     interface's base url.
        /// </summary>
        protected IUrlProvider BaseUrlProvider { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ApiInterface"/> class whose full URL
        ///     depends on another <see cref="IUrlProvider"/>.
        /// </summary>
        /// <param name="apiClient">
        ///     The <see cref="ApiClient"/> which ultimately manages (or rather "contains")
        ///     this interface.
        /// </param>
        /// <param name="baseUrlProvider">
        ///     An <see cref="IUrlProvider"/> which is the logical parent of this
        ///     interface.
        ///     The URL which is provided by this <see cref="IUrlProvider"/> is used as this
        ///     interface's base url.
        ///     
        ///     If <see langword="null"/>, the <paramref name="apiClient"/> is used instead.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="apiClient"/>
        /// </exception>
        public ApiInterface(ApiClient apiClient, IUrlProvider? baseUrlProvider = null)
        {
            Client = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            BaseUrlProvider = baseUrlProvider ?? apiClient;
            _urlLazy = new Lazy<Uri>(() => this.GetUrl(), isThreadSafe: false);
        }

        /// <inheritdoc/>
        UriBuilder IUrlProvider.GetUrlBuilder()
        {
            var baseUriBuilder = BaseUrlProvider.GetUrlBuilder();
            return BuildUrl(baseUriBuilder);
        }

        /// <summary>
        ///     Builds the full URL of this interface by using the specified <see cref="UriBuilder"/>.
        /// </summary>
        /// <param name="baseUrl">
        ///     An <see cref="UriBuilder"/> which was created by this interface's parent.
        ///     This builder most likely holds some kind of base URL which can be extended
        ///     with this interface's information.
        ///     
        ///     For example, the parent may already have configured the builder with a base URL
        ///     like <c>http://www.test.com/user/1</c>.
        ///     This interface could then extend this URL with additional information like
        ///     <c>/items/2</c> or <c>?limit=10</c>.
        /// </param>
        /// <returns>
        ///     The final <see cref="UriBuilder"/> which holds the parts of this interface's URL.
        ///     This should usually be the incoming <paramref name="baseUrl"/> builder instance,
        ///     but can, for special cases, also be an entirely different instance.
        /// </returns>
        protected abstract UriBuilder BuildUrl(UriBuilder baseUrl);

        /// <summary>
        ///     Returns a new <see cref="ApiRequest"/> instance which can be used
        ///     to build a specific request against this interface.
        ///     
        ///     The request is preconfigured with the default configuration of the <see cref="Client"/>
        ///     and the URL of this interface which was created via <see cref="BuildUrl(UriBuilder)"/>.
        /// </summary>
        /// <returns>
        ///     A new <see cref="ApiRequestBase"/> instance.
        /// </returns>
        protected virtual ApiRequest BuildRequest()
        {
            var httpClient = Client.Configuration.HttpClientProvider.Invoke();
            return new ApiRequest(httpClient)
                .SetRequestUri(_urlLazy.Value);
        }

    }

    /// <summary>
    ///     Defines a base class for wrapping an interface of a RESTful HTTP API.
    /// </summary>
    /// <typeparam name="TClient">
    ///     A specific <see cref="ApiClient"/> type which is associated with this interface.
    ///     Specify this type parameter if you need to access specific properties from your custom
    ///     client implementation via this class' <see cref="ApiInterface{TClient}.Client"/>
    ///     property.
    /// </typeparam>
    public abstract class ApiInterface<TClient> : ApiInterface
        where TClient : ApiClient
    {

        /// <summary>
        ///     Gets the <typeparamref name="TClient"/> which ultimately manages (or rather "contains")
        ///     this interface.
        ///     This client's configuration is supposed to be used when building requests.
        /// </summary>
        protected new TClient Client => (TClient)base.Client;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ApiInterface"/> class whose full URL
        ///     depends on another <see cref="IUrlProvider"/>.
        /// </summary>
        /// <param name="client">
        ///     The <typeparamref name="TClient"/> which ultimately manages (or rather "contains")
        ///     this interface.
        /// </param>
        /// <param name="baseUrlProvider">
        ///     An <see cref="IUrlProvider"/> which is the logical parent of this
        ///     interface.
        ///     The URL which is returned by this <see cref="IUrlProvider"/> is used as this
        ///     interface's base url.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="client"/>
        /// </exception>
        public ApiInterface(TClient client, IUrlProvider? baseUrlProvider = null)
            : base(client, baseUrlProvider) { }

    }

}
