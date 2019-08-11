namespace ReqRest.Client.Tests.ApiRequest
{
    using System;
    using System.Net.Http;
    using ReqRest.Builders;
    using ReqRest.Client;
    using ReqRest.Serializers.NewtonsoftJson;
    using ReqRest.Tests;

    /// <summary>
    ///     Provides helper methods for dealing with the ApiRequest{...} classes, e.g. methods
    ///     for creating instances of them.
    /// </summary>
    public static class ApiRequestHelper
    {

        #region Mocked Dto

        public static ApiRequest<Dto1> CreateT1(
            Func<HttpClient> httpClientProvider, HttpRequestMessage httpRequestMessage = null) =>
                Create<Dto1>(httpClientProvider, httpRequestMessage);
        
        public static ApiRequest<Dto1, Dto2> CreateT2(
            Func<HttpClient> httpClientProvider, HttpRequestMessage httpRequestMessage = null) =>
                Create<Dto1, Dto2>(httpClientProvider, httpRequestMessage);
        
        public static ApiRequest<Dto1, Dto2, Dto3> CreateT3(
            Func<HttpClient> httpClientProvider, HttpRequestMessage httpRequestMessage = null) =>
                Create<Dto1, Dto2, Dto3>(httpClientProvider, httpRequestMessage);
        
        public static ApiRequest<Dto1, Dto2, Dto3, Dto4> CreateT4(
            Func<HttpClient> httpClientProvider, HttpRequestMessage httpRequestMessage = null) =>
                Create<Dto1, Dto2, Dto3, Dto4>(httpClientProvider, httpRequestMessage);
        
        public static ApiRequest<Dto1, Dto2, Dto3, Dto4, Dto5> CreateT5(
            Func<HttpClient> httpClientProvider, HttpRequestMessage httpRequestMessage = null) =>
                Create<Dto1, Dto2, Dto3, Dto4, Dto5>(httpClientProvider, httpRequestMessage);
        
        public static ApiRequest<Dto1, Dto2, Dto3, Dto4, Dto5, Dto6> CreateT6(
            Func<HttpClient> httpClientProvider, HttpRequestMessage httpRequestMessage = null) =>
                Create<Dto1, Dto2, Dto3, Dto4, Dto5, Dto6>(httpClientProvider, httpRequestMessage);
        
        public static ApiRequest<Dto1, Dto2, Dto3, Dto4, Dto5, Dto6, Dto7> CreateT7(
            Func<HttpClient> httpClientProvider, HttpRequestMessage httpRequestMessage = null) =>
                Create<Dto1, Dto2, Dto3, Dto4, Dto5, Dto6, Dto7>(httpClientProvider, httpRequestMessage);
        
        public static ApiRequest<Dto1, Dto2, Dto3, Dto4, Dto5, Dto6, Dto7, Dto8> CreateT8(
            Func<HttpClient> httpClientProvider, HttpRequestMessage httpRequestMessage = null) =>
                Create<Dto1, Dto2, Dto3, Dto4, Dto5, Dto6, Dto7, Dto8>(httpClientProvider, httpRequestMessage);

        #endregion

        #region Generic

        public static ApiRequest Create(
            Func<HttpClient> httpClientProvider, HttpRequestMessage httpRequestMessage = null) =>
                new ApiRequest(httpClientProvider, httpRequestMessage)
                    .SetRequestUri("https://www.ReqRest-unit-tests.com");

        public static ApiRequest<T1> Create<T1>(
            Func<HttpClient> httpClientProvider, HttpRequestMessage httpRequestMessage = null) =>
                Create(httpClientProvider, httpRequestMessage)
                    .Receive<T1>().AsJson(1);
        
        public static ApiRequest<T1, T2> Create<T1, T2>(
            Func<HttpClient> httpClientProvider, HttpRequestMessage httpRequestMessage = null) =>
                Create<T1>(httpClientProvider, httpRequestMessage)
                    .Receive<T2>().AsJson(2);
        
        public static ApiRequest<T1, T2, T3> Create<T1, T2, T3>(
            Func<HttpClient> httpClientProvider, HttpRequestMessage httpRequestMessage = null) =>
                Create<T1, T2>(httpClientProvider, httpRequestMessage)
                    .Receive<T3>().AsJson(3);
        
        public static ApiRequest<T1, T2, T3, T4> Create<T1, T2, T3, T4>(
            Func<HttpClient> httpClientProvider, HttpRequestMessage httpRequestMessage = null) =>
                Create<T1, T2, T3>(httpClientProvider, httpRequestMessage)
                    .Receive<T4>().AsJson(4);
        
        public static ApiRequest<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(
            Func<HttpClient> httpClientProvider, HttpRequestMessage httpRequestMessage = null) =>
                Create<T1, T2, T3, T4>(httpClientProvider, httpRequestMessage)
                    .Receive<T5>().AsJson(5);
        
        public static ApiRequest<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(
            Func<HttpClient> httpClientProvider, HttpRequestMessage httpRequestMessage = null) =>
                Create<T1, T2, T3, T4, T5>(httpClientProvider, httpRequestMessage)
                    .Receive<T6>().AsJson(6);
        
        public static ApiRequest<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(
            Func<HttpClient> httpClientProvider, HttpRequestMessage httpRequestMessage = null) =>
                Create<T1, T2, T3, T4, T5, T6>(httpClientProvider, httpRequestMessage)
                    .Receive<T7>().AsJson(7);
        
        public static ApiRequest<T1, T2, T3, T4, T5, T6, T7, T8> Create<T1, T2, T3, T4, T5, T6, T7, T8>(
            Func<HttpClient> httpClientProvider, HttpRequestMessage httpRequestMessage = null) =>
                Create<T1, T2, T3, T4, T5, T6, T7>(httpClientProvider, httpRequestMessage)
                    .Receive<T8>().AsJson(8);
        #endregion

    }

}
