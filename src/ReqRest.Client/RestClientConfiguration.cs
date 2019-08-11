namespace ReqRest.Client
{
    using System;
    using System.Net.Http;

    /// <summary>
    ///     Defines the basic configuration values for a <see cref="RestClient"/>.
    ///     This configuration can be extended by specific client implementations.
    /// </summary>
    public class RestClientConfiguration
    {

        private Func<HttpClient>? _httpClientProvider;

        /// <summary>
        ///     Gets or sets an <see cref="Uri"/> which forms the base URL for any request
        ///     created by the client.
        ///     
        ///     Any <see cref="RestInterface"/> which is exposed by the client will start building
        ///     its request URL based on this value.
        ///     
        ///     This can be <see langword="null"/>. If so, a default initial <see cref="Uri"/> will
        ///     be used by most components instead.
        /// </summary>
        public Uri? BaseUrl { get; set; }
        
        /// <summary>
        ///     Gets or sets a function which returns an <see cref="HttpClient"/> instance that
        ///     should be used for making requests created by the client.
        ///     
        ///     By default, this returns a default <see cref="HttpClient"/> factory that uses a single
        ///     static <see cref="HttpClient"/> instance.
        /// </summary>
        public Func<HttpClient> HttpClientProvider
        {
            get => _httpClientProvider ?? DefaultValues.HttpClientProvider;
            set => _httpClientProvider = value;
        }

        /// <summary>
        ///     Initializes a new <see cref="RestClientConfiguration"/> with default values.
        /// </summary>
        public RestClientConfiguration() { }

        private static class DefaultValues
        {

            private static readonly Lazy<HttpClient> s_httpClientLazy = new Lazy<HttpClient>(() => new HttpClient());

            public static Func<HttpClient> HttpClientProvider { get; } = () => s_httpClientLazy.Value;

        }

    }

}
