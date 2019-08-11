namespace ReqRest.Client.Tests.ApiResponse
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;
    using ReqRest.Client;
    using ReqRest.Tests;

    public class ApiResponseT6Tests : ApiResponseT5Tests
    {
    
        protected override ApiResponseBase CreateResponse(
            HttpResponseMessage httpResponseMessage, IEnumerable<ResponseTypeInfo> possibleResponseTypes)
        {
            return new ApiResponse<Dto1, Dto2, Dto3, Dto4, Dto5, Dto6>(httpResponseMessage, possibleResponseTypes);
        }

        [Fact]
        public async Task DeserializeResourceAsync_Returns_Dto6_For_Associated_Status_Code()
        {
            dynamic response = CreateResponseForResourceDeserializationTests(Dto6StatusCode);
            var resource = await response.DeserializeResourceAsync();
            Assert.True(resource.IsSixth);
            Assert.IsType<Dto6>(resource.Value);
        }

    }

}
