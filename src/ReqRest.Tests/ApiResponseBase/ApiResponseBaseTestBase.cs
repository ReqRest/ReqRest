namespace ReqRest.Tests.ApiResponseBase
{
    using System.Collections.Generic;
    using System.Net.Http;
    using Moq;
    using ReqRest;

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
