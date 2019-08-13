namespace ReqRest.Tests.ApiResponse
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;
    using ReqRest;
    using ReqRest.Tests;

    public class ApiResponseT3Tests : ApiResponseT2Tests
    {
    
        protected override ApiResponseBase CreateResponse(
            HttpResponseMessage httpResponseMessage, IEnumerable<ResponseTypeInfo> possibleResponseTypes)
        {
            return new ApiResponse<Dto1, Dto2, Dto3>(httpResponseMessage, possibleResponseTypes);
        }

        [Fact]
        public async Task DeserializeResourceAsync_Returns_Dto3_For_Associated_Status_Code()
        {
            dynamic response = CreateResponseForResourceDeserializationTests(Dto3StatusCode);
            var resource = await response.DeserializeResourceAsync();
            Assert.True(resource.IsThird);
            Assert.IsType<Dto3>(resource.Value);
        }

    }

}
