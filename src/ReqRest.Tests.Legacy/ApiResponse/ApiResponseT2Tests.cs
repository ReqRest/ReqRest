namespace ReqRest.Tests.ApiResponse
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;
    using ReqRest;
    using ReqRest.Tests.Shared;

    public class ApiResponseT2Tests : ApiResponseT1Tests
    {
    
        protected override ApiResponseBase CreateResponse(
        HttpResponseMessage httpResponseMessage, IEnumerable<ResponseTypeInfo> possibleResponseTypes)
        {
            return new ApiResponse<Dto1, Dto2>(httpResponseMessage, possibleResponseTypes);
        }

        [Fact]
        public async Task DeserializeResourceAsync_Returns_Dto2_For_Associated_Status_Code()
        {
            dynamic response = CreateResponseForResourceDeserializationTests(Dto2StatusCode);
            var resource = await response.DeserializeResourceAsync();
            Assert.True(resource.IsSecond);
            Assert.IsType<Dto2>(resource.Value);
        }

    }

}
