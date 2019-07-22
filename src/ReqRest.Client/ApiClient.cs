namespace ReqRest.Client
{
    using System;
    using ReqRest.Builders;

    /// <summary>
    ///     An abstract base class for any client which consumes a RESTful HTTP API by statically
    ///     typing the available remote interfaces via <see cref="ApiInterface"/> instances.
    /// </summary>
    public abstract class ApiClient : IUrlProvider
    {

        private ApiClientConfiguration _configuration;

        /// <summary>
        ///     Gets or sets the configuration for this client instance.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public ApiClientConfiguration Configuration
        {
            get => _configuration;
            set => _configuration = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        ///     Initializes a new <see cref="ApiClient"/> instance which uses the specified
        ///     <paramref name="configuration"/>.
        /// </summary>
        /// <param name="configuration">
        ///     The configuration for this client instance.
        ///     
        ///     This can be <see langword="null"/>. In this case, a new 
        ///     <see cref="ApiClientConfiguration"/> is created and used instead.
        /// </param>
        public ApiClient(ApiClientConfiguration? configuration)
        {
            _configuration = configuration ?? new ApiClientConfiguration();
        }

        /// <summary>
        ///     Returns a new <see cref="UriBuilder"/> which starts building on the configured
        ///     <see cref="ApiClientConfiguration.BaseUrl"/>.
        /// </summary>
        UrlBuilder IUrlProvider.GetUrlBuilder()
        {
            return Configuration.BaseUrl is null
                ? new UrlBuilder()
                : new UrlBuilder(Configuration.BaseUrl);
        }

    }

    /// <summary>
    ///     An abstract base class for any client which consumes a RESTful HTTP API by statically
    ///     typing the available remote interfaces via <see cref="ApiInterface"/> instances.
    /// </summary>
    /// <typeparam name="TConfig">
    ///     A custom <see cref="ApiClientConfiguration"/> type which is used by the deriving client.
    /// </typeparam>
    public abstract class ApiClient<TConfig> : ApiClient
        where TConfig : ApiClientConfiguration, new()
    {

        /// <summary>
        ///     Gets or sets the configuration for this client instance.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public new TConfig Configuration
        {
            get => (TConfig)base.Configuration;
            set => base.Configuration = value; // Does ANE validation.
        }

        /// <summary>
        ///     Initializes a new <see cref="ApiClient"/> instance which uses the specified
        ///     <paramref name="configuration"/>.
        /// </summary>
        /// <param name="configuration">
        ///     The configuration for this client instance.
        ///     
        ///     This can be <see langword="null"/>. In this case, a new 
        ///     <typeparamref name="TConfig"/> is created and used instead.
        /// </param>
        public ApiClient(TConfig? configuration)
            : base(configuration ?? new TConfig()) { }

    }

}
