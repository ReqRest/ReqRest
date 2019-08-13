namespace ReqRest.Tests.ApiRequest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using ReqRest;
    using ReqRest.Http;
    using ReqRest.Serializers;
    using Xunit;

    public abstract partial class ApiRequestTestBase<TRequest>
    {

        private static readonly StatusCodeRange[] DefaultStatusCodes = { 204, (100, 200), (null, 123) };
        
        [Fact]
        public void ReceiveNoContent_Adds_Expected_ResponseTypeInfo() =>
            TestExpectedResponseTypeInfo(
                req => req.ReceiveNoContent(), 
                StatusCode.NoContent
            );

        [Fact]
        public void ReceiveNoContent_Adds_Expected_ResponseTypeInfo_StatusCodeRangeArray() =>
            TestExpectedResponseTypeInfo(
                req => req.ReceiveNoContent((StatusCodeRange[])DefaultStatusCodes),
                DefaultStatusCodes
            );
        
        [Fact]
        public void ReceiveNoContent_Adds_Expected_ResponseTypeInfo_StatusCodeRangeEnumerable() =>
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

}
