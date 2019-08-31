namespace ReqRest.Tests.ApiRequest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using ReqRest;
    using ReqRest.Http;
    using ReqRest.Internal.Serializers;
    using ReqRest.Serializers;
    using Xunit;

    public abstract partial class ApiRequestTestBase<TRequest>
    {

        private static readonly StatusCodeRange[] DefaultStatusCodes = { 204, (100, 200), (null, 123) };
        
        [Fact]
        public void ReceiveNoContent_Adds_Expected_ResponseTypeInfo() =>
            TestExpectedResponseTypeInfo(
                typeof(NoContent),
                typeof(NoContentSerializer),
                req => req.ReceiveNoContent(), 
                StatusCode.NoContent
            );

        [Fact]
        public void ReceiveNoContent_Adds_Expected_ResponseTypeInfo_StatusCodeRangeArray() =>
            TestExpectedResponseTypeInfo(
                typeof(NoContent),
                typeof(NoContentSerializer),
                req => req.ReceiveNoContent((StatusCodeRange[])DefaultStatusCodes),
                DefaultStatusCodes
            );
        
        [Fact]
        public void ReceiveNoContent_Adds_Expected_ResponseTypeInfo_StatusCodeRangeEnumerable() =>
            TestExpectedResponseTypeInfo(
                typeof(NoContent),
                typeof(NoContentSerializer),
                req => req.ReceiveNoContent((IEnumerable<StatusCodeRange>)DefaultStatusCodes),
                DefaultStatusCodes
            );


        [Fact]
        public void ReceiveString_Adds_Expected_ResponseTypeInfo_StatusCodeRangeArray() =>
            TestExpectedResponseTypeInfo(
                typeof(string),
                typeof(StringSerializer),
                req => req.ReceiveString((StatusCodeRange[])DefaultStatusCodes),
                DefaultStatusCodes
            );

        [Fact]
        public void ReceiveString_Adds_Expected_ResponseTypeInfo_StatusCodeRangeEnumerable() =>
            TestExpectedResponseTypeInfo(
                typeof(string),
                typeof(StringSerializer),
                req => req.ReceiveString((IEnumerable<StatusCodeRange>)DefaultStatusCodes),
                DefaultStatusCodes
            );
        

        [Fact]
        public void ReceiveByteArray_Adds_Expected_ResponseTypeInfo_StatusCodeRangeArray() =>
            TestExpectedResponseTypeInfo(
                typeof(byte[]),
                typeof(ByteArraySerializer),
                req => req.ReceiveByteArray((StatusCodeRange[])DefaultStatusCodes),
                DefaultStatusCodes
            );

        [Fact]
        public void ReceiveByteArray_Adds_Expected_ResponseTypeInfo_StatusCodeRangeEnumerable() =>
            TestExpectedResponseTypeInfo(
                typeof(byte[]),
                typeof(ByteArraySerializer),
                req => req.ReceiveByteArray((IEnumerable<StatusCodeRange>)DefaultStatusCodes),
                DefaultStatusCodes
            );

        private void TestExpectedResponseTypeInfo(
            Type expectedType,
            Type expectedSerializerType,
            Func<dynamic, ApiRequestBase> receive,
            params StatusCodeRange[] expectedStatusCodes)
        {
            var req = CreateDynamicRequest();
            var upgraded = (ApiRequestBase)receive(req);
            var info = upgraded.PossibleResponseTypes.Last();

            info.ResponseType.Should().Be(expectedType);
            info.StatusCodes.Should().Equal(expectedStatusCodes);
            info.ResponseDeserializerFactory().Should().BeOfType(expectedSerializerType);
        }
        
    }

}
