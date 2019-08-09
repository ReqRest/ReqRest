namespace ReqRest.Client.Tests.ApiRequest
{
    using System;
    using System.Net.Http;
    using ReqRest.Client;

    public abstract class ApiRequestTestBase<TRequest> where TRequest : ApiRequestBase
    {

        protected dynamic CreateDynamicRequest() =>
            CreateRequest();
        
        protected dynamic CreateDynamicRequest(Func<HttpClient> httpClientProvider) =>
            CreateRequest(httpClientProvider);
        
        protected TRequest CreateRequest() =>
            CreateRequest(() => null);
        
        protected abstract TRequest CreateRequest(Func<HttpClient> httpClientProvider);

    }

}
