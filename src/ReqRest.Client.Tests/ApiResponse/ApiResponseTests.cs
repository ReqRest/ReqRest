namespace ReqRest.Client.Tests.ApiResponse
{
    using System.Collections.Generic;
    using System.Net.Http;
    using FluentAssertions;
    using ReqRest.Client;
    using ReqRest.Http;
    using Xunit;

    public class ApiResponseTests : ApiResponseTestBase
    {

        protected override ApiResponseBase CreateResponse(
            HttpResponseMessage httpResponseMessage = null, IEnumerable<ResponseTypeInfo> possibleResponseTypes = null)
        {
            return new ApiResponse(httpResponseMessage, possibleResponseTypes);
        }

        [Fact]
        public void Constructor_Uses_Specified_HttpResponseMessage()
        {
            var msg = new HttpResponseMessage();
            var res = CreateResponse(msg);
            res.HttpResponseMessage.Should().BeSameAs(msg);
        }

        [Fact]
        public void Constructor_Uses_Specified_PossibleResponseTypes()
        {
            var types = new[]
            {
                new ResponseTypeInfo(typeof(int), new StatusCodeRange[] { 200 }, () => null),
            };
            var res = CreateResponse(possibleResponseTypes: types);
            res.PossibleResponseTypes.Should().Equal(types);
        }

    }

}
