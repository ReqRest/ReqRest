namespace ReqRest.Tests.ApiResponse
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;
    using ReqRest;
    using ReqRest.Tests.Shared;

    public class ApiResponseT5Tests : ApiResponseT4Tests
    {
    
        protected override ApiResponseBase CreateResponse(
            HttpResponseMessage httpResponseMessage, IEnumerable<ResponseTypeInfo> possibleResponseTypes)
        {
            return new ApiResponse<Dto1, Dto2, Dto3, Dto4, Dto5>(httpResponseMessage, possibleResponseTypes);
        }

        [Fact]
        public async Task DeserializeResourceAsync_Returns_Dto5_For_Associated_Status_Code()
        {
            dynamic response = CreateResponseForResourceDeserializationTests(Dto5StatusCode);
            var resource = await response.DeserializeResourceAsync();
            Assert.True(resource.IsFifth);
            Assert.IsType<Dto5>(resource.Value);
        }

    }

}
