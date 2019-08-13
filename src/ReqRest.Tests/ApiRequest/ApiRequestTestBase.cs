namespace ReqRest.Tests.ApiRequest
{
    using System;
    using System.Net.Http;
    using ReqRest;
    using ReqRest.Tests;

    public abstract partial class ApiRequestTestBase<TRequest> where TRequest : ApiRequestBase
    {

        protected dynamic CreateDynamicRequest() =>
            CreateRequest();
        
        protected dynamic CreateDynamicRequest(Func<HttpClient> httpClientProvider) =>
            CreateRequest(httpClientProvider);
        
        protected TRequest CreateRequest() =>
            CreateRequest(() => null);
        
        protected abstract TRequest CreateRequest(Func<HttpClient> httpClientProvider);

    }

    public class ApiRequestTests : ApiRequestTestBase<ApiRequest>
    {
        protected override ApiRequest CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.Create(httpClientProvider);
    }

    public class ApiRequestT1Tests : ApiRequestTestBase<ApiRequest<Dto1>>
    {
        protected override ApiRequest<Dto1> CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.CreateT1(httpClientProvider);
    }

    public class ApiRequestT2Tests : ApiRequestTestBase<ApiRequest<Dto1, Dto2>>
    {
        protected override ApiRequest<Dto1, Dto2> CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.CreateT2(httpClientProvider);
    }

    public class ApiRequestT3Tests : ApiRequestTestBase<ApiRequest<Dto1, Dto2, Dto3>>
    {
        protected override ApiRequest<Dto1, Dto2, Dto3> CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.CreateT3(httpClientProvider);
    }

    public class ApiRequestT4Tests : ApiRequestTestBase<ApiRequest<Dto1, Dto2, Dto3, Dto4>>
    {
        protected override ApiRequest<Dto1, Dto2, Dto3, Dto4> CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.CreateT4(httpClientProvider);
    }

    public class ApiRequestT5Tests : ApiRequestTestBase<ApiRequest<Dto1, Dto2, Dto3, Dto4, Dto5>>
    {
        protected override ApiRequest<Dto1, Dto2, Dto3, Dto4, Dto5> CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.CreateT5(httpClientProvider);
    }

    public class ApiRequestT6Tests : ApiRequestTestBase<ApiRequest<Dto1, Dto2, Dto3, Dto4, Dto5, Dto6>>
    {
        protected override ApiRequest<Dto1, Dto2, Dto3, Dto4, Dto5, Dto6> CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.CreateT6(httpClientProvider);
    }

    public class ApiRequestT7Tests : ApiRequestTestBase<ApiRequest<Dto1, Dto2, Dto3, Dto4, Dto5, Dto6, Dto7>>
    {
        protected override ApiRequest<Dto1, Dto2, Dto3, Dto4, Dto5, Dto6, Dto7> CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.CreateT7(httpClientProvider);
    }

}
