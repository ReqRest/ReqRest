namespace ReqRest.Client.Tests.ApiResponseBase
{
    using System.Collections.Generic;
    using System.Net.Http;
    using Moq;
    using ReqRest.Client;

    public abstract class ApiResponseBaseTestBase
    {

        protected ApiResponseBase CreateResponse(
            HttpResponseMessage httpResponseMessage = null,
            IEnumerable<ResponseTypeInfo> possibleResponseTypes = null)
        {
            return new Mock<ApiResponseBase>(httpResponseMessage, possibleResponseTypes) { CallBase = true }
                .Object;
        }

    }

}
