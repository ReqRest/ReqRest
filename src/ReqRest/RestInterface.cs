namespace ReqRest
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using ReqRest.Builders;

    /// <summary>
    ///     Defines a base class for wrapping an interface of a RESTful HTTP API and exposing
    ///     the HTTP requests which can be made against that interface.
    ///     See remarks for details on what an interface means in this context.
    /// </summary>
    /// <remarks>
    ///     An interface in the context of this library means a certain part of an URL which
    ///     identifies a resource in a RESTful API.
    ///
    ///     For example, given the <c>http://api.com/todos</c> URL, the <c>todos</c> part is
    ///     an interface.
    ///     If a <see cref="RestClient"/> is configured with the base URL of that API (i.e.
    ///     (<c>http://api.com</c>), this base URL can be combined with the interface part
    ///     <c>todos</c> to form a full URL.
    ///
    ///     It is important to understand that ReqRest maps each possible interface to one class.
    ///     For example, while the two URLS <c>http://api.com/todos</c> and <c>http://api.com/todos/123</c>
    ///     might look like one interface, they are actually treated as two <see cref="RestInterface"/>
    ///     members by ReqRest.
    ///     This is because the two URLs support different methods. While the first one usually
    ///     supports methods like <c>GET</c> and <c>POST</c>, the second one usually supports
    ///     <c>GET</c>, <c>PUT</c>, <c>PATCH</c> and <c>DELETE</c>.
    ///     Thus, it makes sense to separate them into different classes that make different
    ///     requests available.
    /// </remarks>
    public abstract class RestInterface : IBaseUrlProvider
    {

        private Uri? _url;

        /// <summary>
        ///     Gets the <see cref="RestClient"/> which ultimately manages (or rather "contains")
        ///     this interface.
        ///     This client's configuration is supposed to be used when building requests.
        /// </summary>
        protected internal RestClient Client { get; }

        /// <summary>
        ///     Gets an <see cref="IBaseUrlProvider"/> which is the logical parent of this
        ///     interface.
        ///     The URL which is returned by this <see cref="IBaseUrlProvider"/> is used as this
        ///     interface's base url.
        /// </summary>
        protected internal IBaseUrlProvider BaseUrlProvider { get; }

        /// <summary>
        ///     Gets the final URL which was built for the <see cref="RestInterface"/>.
        /// </summary>
        /// <remarks>
        ///     This property is evaluated once and from then on cached.
        ///     Cast this class to an <see cref="IBaseUrlProvider"/> and use its
        ///     <see cref="IBaseUrlProvider.BuildBaseUrl"/> to force the creation of a new
        ///     <see cref="Uri"/> instance.
        /// </remarks>
        protected internal Uri Url => _url ??= ((IBaseUrlProvider)this).BuildBaseUrl().Uri;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RestInterface"/> class whose full URL
        ///     depends on another <see cref="IBaseUrlProvider"/>.
        /// </summary>
        /// <param name="restClient">
        ///     The <see cref="RestClient"/> which ultimately manages (or rather "contains")
        ///     this interface.
        /// </param>
        /// <param name="baseUrlProvider">
        ///     An <see cref="IBaseUrlProvider"/> which is the logical parent of this
        ///     interface.
        ///     The URL which is provided by this <see cref="IBaseUrlProvider"/> is used as this
        ///     interface's base url.
        ///     
        ///     If <see langword="null"/>, the <paramref name="restClient"/> is used instead.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="restClient"/>
        /// </exception>
        public RestInterface(RestClient restClient, IBaseUrlProvider? baseUrlProvider = null)
        {
            Client = restClient ?? throw new ArgumentNullException(nameof(restClient));
            BaseUrlProvider = baseUrlProvider ?? restClient;
        }

        /// <inheritdoc/>
        UrlBuilder IBaseUrlProvider.BuildBaseUrl()
        {
            var baseUrlBuilder = BaseUrlProvider.BuildBaseUrl();
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
        // because consumers of an API built via RestInterfaces should, in my opinion,
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
        public override bool Equals(object? obj) =>
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
    ///     Defines a base class for wrapping an interface of a RESTful HTTP API and exposing
    ///     the HTTP requests which can be made against that interface.
    ///     See remarks for details on what an interface means in this context.
    /// </summary>
    /// <typeparam name="TClient">
    ///     A specific <see cref="RestClient"/> type which is associated with this interface.
    ///     Specify this type parameter if you need to access specific properties from your custom
    ///     client implementation via this class' <see cref="RestInterface{TClient}.Client"/>
    ///     property.
    /// </typeparam>
    /// <remarks>
    ///     An interface in the context of this library means a certain part of an URL which
    ///     identifies a resource in a RESTful API.
    ///
    ///     For example, given the <c>http://api.com/todos</c> URL, the <c>todos</c> part is
    ///     an interface.
    ///     If a <see cref="RestClient"/> is configured with the base URL of that API (i.e.
    ///     (<c>http://api.com</c>), this base URL can be combined with the interface part
    ///     <c>todos</c> to form a full URL.
    ///
    ///     It is important to understand that ReqRest maps each possible interface to one class.
    ///     For example, while the two URLS <c>http://api.com/todos</c> and <c>http://api.com/todos/123</c>
    ///     might look like one interface, they are actually treated as two <see cref="RestInterface"/>
    ///     members by ReqRest.
    ///     This is because the two URLs support different methods. While the first one usually
    ///     supports methods like <c>GET</c> and <c>POST</c>, the second one usually supports
    ///     <c>GET</c>, <c>POST</c>, <c>PUT</c> and <c>DELETE</c>.
    ///     Thus, it makes sense to separate them into different classes that make different
    ///     requests available.
    /// </remarks>
    public abstract class RestInterface<TClient> : RestInterface
        where TClient : RestClient
    {

        /// <summary>
        ///     Gets the <typeparamref name="TClient"/> which ultimately manages (or rather "contains")
        ///     this interface.
        ///     This client's configuration is supposed to be used when building requests.
        /// </summary>
        protected internal new TClient Client => (TClient)base.Client;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RestInterface"/> class whose full URL
        ///     depends on another <see cref="IBaseUrlProvider"/>.
        /// </summary>
        /// <param name="client">
        ///     The <typeparamref name="TClient"/> which ultimately manages (or rather "contains")
        ///     this interface.
        /// </param>
        /// <param name="baseUrlProvider">
        ///     An <see cref="IBaseUrlProvider"/> which is the logical parent of this
        ///     interface.
        ///     The URL which is returned by this <see cref="IBaseUrlProvider"/> is used as this
        ///     interface's base url.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="client"/>
        /// </exception>
        public RestInterface(TClient client, IBaseUrlProvider? baseUrlProvider = null)
            : base(client, baseUrlProvider) { }

    }

}
