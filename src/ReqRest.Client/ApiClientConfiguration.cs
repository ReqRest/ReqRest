namespace ReqRest
{
    using System;
    using System.Net.Http;

    /// <summary>
    ///     Defines the basic configuration values for an <see cref="ApiClient{TConfig}"/>.
    ///     This configuration can be extended by specific client implementations.
    /// </summary>
    public class ApiClientConfiguration
    {

        private HttpClientProvider? _httpClientProvider;

        /// <summary>
        ///     Gets or sets an <see cref="Uri"/> which forms the base URL for any request
        ///     created by the client.
        ///     
        ///     Any <see cref="ApiInterface"/> which is exposed by the client will start building
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
        ///     This returns a default <see cref="HttpClient"/> factory that uses a single
        ///     static <see cref="HttpClient"/> instance by default.
        /// </summary>
        public HttpClientProvider HttpClientProvider
        {
            get => _httpClientProvider ?? DefaultHttpClientProvider.GetHttpClient;
            set => _httpClientProvider = value;
        }

        /// <summary>
        ///     Initializes a new <see cref="ApiClientConfiguration"/> with default values.
        /// </summary>
        public ApiClientConfiguration() { }

    }

}
