namespace ReqRest.Client.Tests.ApiResponse
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;
    using ReqRest.Client;
    using ReqRest.Tests;

    public class ApiResponseT4Tests : ApiResponseT3Tests
    {
    
        protected override ApiResponseBase CreateResponse(
            HttpResponseMessage httpResponseMessage, IEnumerable<ResponseTypeInfo> possibleResponseTypes)
        {
            return new ApiResponse<Dto1, Dto2, Dto3, Dto4>(httpResponseMessage, possibleResponseTypes);
        }

        [Fact]
        public async Task DeserializeResourceAsync_Returns_Dto4_For_Associated_Status_Code()
        {
            dynamic response = CreateResponseForResourceDeserializationTests(Dto4StatusCode);
            var resource = await response.DeserializeResourceAsync();
            Assert.True(resource.IsFourth);
            Assert.IsType<Dto4>(resource.Value);
        }

    }

}
