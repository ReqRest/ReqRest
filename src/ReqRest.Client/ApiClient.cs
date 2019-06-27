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

        /// <summary>
        ///     Gets the configuration for this client instance.
        /// </summary>
        public ApiClientConfiguration Configuration { get; }

        /// <summary>
        ///     Initializes a new <see cref="ApiClient"/> instance which uses the specified
        ///     <paramref name="configuration"/>.
        /// </summary>
        /// <param name="configuration">The configuration for this client instance.</param>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="configuration"/>
        /// </exception>
        public ApiClient(ApiClientConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        ///     Returns a new <see cref="UriBuilder"/> which starts building on the configured
        ///     <see cref="ApiClientConfiguration.BaseUrl"/>.
        /// </summary>
        UriBuilder IUrlProvider.GetUrlBuilder()
        {
            var builder = Configuration.BaseUrl is null
                ? new UriBuilder()
                : new UriBuilder(Configuration.BaseUrl);

            // UriBuilder may automagically set the port to the corresponding scheme default.
            // For example, https may lead to a port of 443.
            //
            // This is, imo, mostly unwanted. -> Reset the port manually, just to be safe.
            return builder.SetPort(null);
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
        where TConfig : ApiClientConfiguration
    {

        /// <summary>
        ///     Gets the configuration for this client instance.
        /// </summary>
        public new TConfig Configuration => (TConfig)base.Configuration;

        /// <summary>
        ///     Initializes a new <see cref="ApiClient"/> instance which uses the specified
        ///     <paramref name="configuration"/>.
        /// </summary>
        /// <param name="configuration">The configuration for this client instance.</param>
        /// <exception cref="ArgumentNullException">
        ///     * <paramref name="configuration"/>
        /// </exception>
        public ApiClient(TConfig configuration)
            : base(configuration) { }

    }

}
