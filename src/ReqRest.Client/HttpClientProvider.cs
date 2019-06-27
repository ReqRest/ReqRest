namespace ReqRest.Client
{
    using System.Net.Http;

    /// <summary>
    ///     Represents a method which dynamically returns an <see cref="HttpClient"/> that can be
    ///     used for making HTTP requests.
    /// </summary>
    /// <returns>
    ///     An <see cref="HttpClient"/> which can be used for making requests.
    /// </returns>
    public delegate HttpClient HttpClientProvider();
    
}
