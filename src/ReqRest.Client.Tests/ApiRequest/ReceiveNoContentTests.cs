namespace ReqRest.Client.Tests.ApiRequest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using FluentAssertions;
    using ReqRest.Client;
    using ReqRest.Http;
    using ReqRest.Serializers;
    using Xunit;

    public abstract class ReceiveNoContentTestBase<TRequest> : ApiRequestTestBase<TRequest>
        where TRequest : ApiRequestBase
    {

        private static readonly StatusCodeRange[] DefaultStatusCodes = { 204, (100, 200), (null, 123) };
        
        [Fact]
        public void Adds_Expected_ResponseTypeInfo() =>
            TestExpectedResponseTypeInfo(
                req => req.ReceiveNoContent(), 
                StatusCode.NoContent
            );

        [Fact]
        public void Adds_Expected_ResponseTypeInfo_StatusCodeRangeArray() =>
            TestExpectedResponseTypeInfo(
                req => req.ReceiveNoContent((StatusCodeRange[])DefaultStatusCodes),
                DefaultStatusCodes
            );
        
        [Fact]
        public void Adds_Expected_ResponseTypeInfo_StatusCodeRangeEnumerable() =>
            TestExpectedResponseTypeInfo(
                req => req.ReceiveNoContent((IEnumerable<StatusCodeRange>)DefaultStatusCodes),
                DefaultStatusCodes
            );

        private void TestExpectedResponseTypeInfo(
            Func<dynamic, ApiRequestBase> receiveNoContent,
            params StatusCodeRange[] expectedStatusCodes)
        {
            var req = CreateDynamicRequest();
            var upgraded = (ApiRequestBase)receiveNoContent(req);
            var info = upgraded.PossibleResponseTypes.Last();

            info.ResponseType.Should().Be(typeof(NoContent));
            info.StatusCodes.Should().Equal(expectedStatusCodes);
            info.ResponseDeserializerFactory().Should().BeOfType(typeof(NoContentSerializer));
        }
        
    }

    public class ApiRequestReceiveNoContentTests : ReceiveNoContentTestBase<ApiRequest>
    {
        protected override ApiRequest CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.Create(httpClientProvider);
    }

    public class ApiRequestT1ReceiveNoContentTests : ReceiveNoContentTestBase<ApiRequest<Dto1>>
    {
        protected override ApiRequest<Dto1> CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.CreateT1(httpClientProvider);
    }

    public class ApiRequestT2ReceiveNoContentTests : ReceiveNoContentTestBase<ApiRequest<Dto1, Dto2>>
    {
        protected override ApiRequest<Dto1, Dto2> CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.CreateT2(httpClientProvider);
    }

    public class ApiRequestT3ReceiveNoContentTests : ReceiveNoContentTestBase<ApiRequest<Dto1, Dto2, Dto3>>
    {
        protected override ApiRequest<Dto1, Dto2, Dto3> CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.CreateT3(httpClientProvider);
    }

    public class ApiRequestT4ReceiveNoContentTests : ReceiveNoContentTestBase<ApiRequest<Dto1, Dto2, Dto3, Dto4>>
    {
        protected override ApiRequest<Dto1, Dto2, Dto3, Dto4> CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.CreateT4(httpClientProvider);
    }

    public class ApiRequestT5ReceiveNoContentTests : ReceiveNoContentTestBase<ApiRequest<Dto1, Dto2, Dto3, Dto4, Dto5>>
    {
        protected override ApiRequest<Dto1, Dto2, Dto3, Dto4, Dto5> CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.CreateT5(httpClientProvider);
    }

    public class ApiRequestT6ReceiveNoContentTests : ReceiveNoContentTestBase<ApiRequest<Dto1, Dto2, Dto3, Dto4, Dto5, Dto6>>
    {
        protected override ApiRequest<Dto1, Dto2, Dto3, Dto4, Dto5, Dto6> CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.CreateT6(httpClientProvider);
    }

    public class ApiRequestT7ReceiveNoContentTests : ReceiveNoContentTestBase<ApiRequest<Dto1, Dto2, Dto3, Dto4, Dto5, Dto6, Dto7>>
    {
        protected override ApiRequest<Dto1, Dto2, Dto3, Dto4, Dto5, Dto6, Dto7> CreateRequest(Func<HttpClient> httpClientProvider) =>
            ApiRequestHelper.CreateT7(httpClientProvider);
    }

}
