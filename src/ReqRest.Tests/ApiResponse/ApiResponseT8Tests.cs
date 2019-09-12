namespace ReqRest.Tests.ApiResponse
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;
    using ReqRest;
    using ReqRest.Tests.Shared;

    public class ApiResponseT8Tests : ApiResponseT7Tests
    {
    
        protected override ApiResponseBase CreateResponse(
            HttpResponseMessage httpResponseMessage, IEnumerable<ResponseTypeInfo> possibleResponseTypes)
        {
            return new ApiResponse<Dto1, Dto2, Dto3, Dto4, Dto5, Dto6, Dto7, Dto8>(httpResponseMessage, possibleResponseTypes);
        }

        [Fact]
        public async Task DeserializeResourceAsync_Returns_Dto8_For_Associated_Status_Code()
        {
            dynamic response = CreateResponseForResourceDeserializationTests(Dto8StatusCode);
            var resource = await response.DeserializeResourceAsync();
            Assert.True(resource.IsEigth);
            Assert.IsType<Dto8>(resource.Value);
        }

    }

}
