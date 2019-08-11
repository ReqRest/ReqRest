namespace ReqRest.Client
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using ReqRest.Builders;

    /// <summary>
    ///     Defines a base class for wrapping an interface of a RESTful HTTP API.
    /// </summary>
    public abstract class ApiInterface : IUrlProvider
    {

        private Uri? _url;

        /// <summary>
        ///     Gets the <see cref="ApiClient"/> which ultimately manages (or rather "contains")
        ///     this interface.
        ///     This client's configuration is supposed to be used when building requests.
        /// </summary>
        protected internal ApiClient Client { get; }

        /// <summary>
        ///     Gets an <see cref="IUrlProvider"/> which is the logical parent of this
        ///     interface.
        ///     The URL which is returned by this <see cref="IUrlProvider"/> is used as this
        ///     interface's base url.
        /// </summary>
        protected internal IUrlProvider BaseUrlProvider { get; }

        /// <summary>
        ///     Gets the URL which was built for the <see cref="ApiInterface"/>.
        /// </summary>
        protected internal Uri Url => _url ??= this.GetUrl();

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
        }

        /// <inheritdoc/>
        UrlBuilder IUrlProvider.GetUrlBuilder()
        {
            var baseUrlBuilder = BaseUrlProvider.GetUrlBuilder();
            return BuildUrl(baseUrlBuilder);
        }

        /// <summary>
        ///     Builds the full URL of this interface by using the specified <see cref="UrlBuilder"/>.
        /// </summary>
        /// <param name="baseUrl">
        ///     An <see cref="UrlBuilder"/> which was created by this interface's parent.
        ///     This builder most likely holds some kind of base URL which can be extended
        ///     with this interface's information.
        ///     
        ///     For example, the parent may already have configured the builder with a base URL
        ///     like <c>http://www.test.com/user/1</c>.
        ///     This interface could then extend this URL with additional information like
        ///     <c>/items/2</c> or <c>?limit=10</c>.
        /// </param>
        /// <returns>
        ///     The final <see cref="UrlBuilder"/> which holds the parts of this interface's URL.
        ///     This should usually be the incoming <paramref name="baseUrl"/> builder instance,
        ///     but can, for special cases, also be an entirely different instance.
        /// </returns>
        protected internal abstract UrlBuilder BuildUrl(UrlBuilder baseUrl);

        /// <summary>
        ///     Returns a new <see cref="ApiRequest"/> instance which can be used
        ///     to build a specific request against this interface.
        ///     
        ///     The request is preconfigured with the default configuration of the <see cref="Client"/>
        ///     and the URL of this interface which was created via <see cref="BuildUrl(UrlBuilder)"/>.
        /// </summary>
        /// <returns>
        ///     A new <see cref="ApiRequestBase"/> instance.
        /// </returns>
        protected internal virtual ApiRequest BuildRequest()
        {
            return new ApiRequest(Client.Configuration.HttpClientProvider)
                .SetRequestUri(Url);
        }

        // The following methods are overridden/shadowed, so that the EditorBrowsable Attribute
        // can be applied.
        // While I myself consider hiding members bad practice, I will do it in this case,
        // because consumers of an API built via ApiInterfaces should, in my opinion,
        // only see an IntelliSense window with the API's actual members, i.e. something like this:
        //  ________                        ______________
        // | Get    |                      | Get          |
        // | Post   |   instead of this:   | GetHashCode  |
        // | Items  |                      | GetType      |
        // |________|                      | Post         |
        //                                 | ...          |
        //                                 |______________|
        //
        // It appears that this doesn't work for GetType(), but we will still keep it here,
        // for consistency. Furthermore, it may work as intended in SOME editors. Who knows.
        // Let's try to get the most out of it.

        /// <summary>
        ///     Gets the <see cref="Type"/> of the current instance.
        ///     This method calls the <see cref="object.GetType"/> method and returns its result.
        /// </summary>
        /// <returns>The exact runtime type of the current instance.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [ExcludeFromCodeCoverage]
        public new Type GetType() =>
            base.GetType();

        /// <summary>
        ///     Determines whether the specified object is equal to the current object.
        ///     This method calls the <see cref="object.Equals(object)"/> method and returns its result.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        ///     <see langword="true"/> if the specified object is equal to the current object;
        ///     otherwise, <see langword="false"/>.
        /// </returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [ExcludeFromCodeCoverage]
        public override bool Equals(object obj) =>
            base.Equals(obj);

        /// <summary>
        ///     Serves as the default hash function.
        ///     This method calls the <see cref="object.GetHashCode"/> method and returns its result.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [ExcludeFromCodeCoverage]
        public override int GetHashCode() =>
            base.GetHashCode();

        /// <summary>
        ///     Returns a string representing the interface's URL.
        /// </summary>
        /// <returns>A string representing the interface's URL.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString() =>
            Url.ToString();

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
        protected internal new TClient Client => (TClient)base.Client;

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
