namespace ReqRest
{
    using System;
    using ReqRest.Builders;

    // Note:
    // One goal of this class is to be as slim as possible, i.e. to have as few public (!)
    // properties and methods as possible.
    // The reason is that a user of a client should be able to focus on the interfaces exposed
    // by the client and not other properties/members.
    // This is also the reason why the config values are stored in another class.
    // This allows the config to grow in the future without the client receiving additional
    // members.

    /// <summary>
    ///     An abstract base class for implementing a client class which consumes a
    ///     RESTful HTTP API by statically typing the available interfaces via
    ///     <see cref="RestInterface"/> instances that allow the user to create and make requests
    ///     to the API.
    /// </summary>
    public abstract class RestClient : IUrlProvider
    {

        private RestClientConfiguration _configuration;

        /// <summary>
        ///     Gets or sets the configuration for this client instance.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public RestClientConfiguration Configuration
        {
            get => _configuration;
            set => _configuration = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        ///     Initializes a new <see cref="RestClient"/> instance which uses the specified
        ///     <paramref name="configuration"/>.
        /// </summary>
        /// <param name="configuration">
        ///     The configuration for this client instance.
        ///     
        ///     This can be <see langword="null"/>. In this case, a new 
        ///     <see cref="RestClientConfiguration"/> is created and used instead.
        /// </param>
        public RestClient(RestClientConfiguration? configuration)
        {
            _configuration = configuration ?? new RestClientConfiguration();
        }

        /// <summary>
        ///     Returns a new <see cref="UriBuilder"/> which starts building on the configured
        ///     <see cref="RestClientConfiguration.BaseUrl"/>.
        /// </summary>
        UrlBuilder IUrlProvider.GetUrlBuilder()
        {
            return Configuration.BaseUrl is null
                ? new UrlBuilder()
                : new UrlBuilder(Configuration.BaseUrl);
        }
        
    }

    /// <summary>
    ///     An abstract base class for implementing a client class which consumes a
    ///     RESTful HTTP API by statically typing the available interfaces via
    ///     <see cref="RestInterface"/> instances that allow the user to create and make requests
    ///     to the API.
    /// </summary>
    /// <typeparam name="TConfig">
    ///     A custom <see cref="RestClientConfiguration"/> type which is used by the deriving client.
    /// </typeparam>
    public abstract class RestClient<TConfig> : RestClient
        where TConfig : RestClientConfiguration, new()
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
        ///     Initializes a new <see cref="RestClient"/> instance which uses the specified
        ///     <paramref name="configuration"/>.
        /// </summary>
        /// <param name="configuration">
        ///     The configuration for this client instance.
        ///     
        ///     This can be <see langword="null"/>. In this case, a new 
        ///     <typeparamref name="TConfig"/> is created and used instead.
        /// </param>
        public RestClient(TConfig? configuration)
            : base(configuration ?? new TConfig()) { }

    }

}
