namespace ReqRest.Client.Tests.ApiResponse
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ReqRest.Client;
    using ReqRest.Serializers;
    using ReqRest.Tests;
    using Xunit;

    public class ApiResponseT1Tests : ApiResponseTests
    {

        protected override ApiResponseBase CreateResponse(
            HttpResponseMessage httpResponseMessage, IEnumerable<ResponseTypeInfo> possibleResponseTypes)
        {
            return new ApiResponse<Dto1>(httpResponseMessage, possibleResponseTypes);
        }

        [Fact]
        public void DeserializeResourceAsync_Throws_InvalidOperationException_If_SerializerFactory_Returns_Null()
        {
            dynamic response = CreateResponseForResourceDeserializationTests(
                responseDeserializerFactory: () => null
            );

            Assert.ThrowsAsync<InvalidOperationException>(async () => await response.DeserializeResourceAsync());
        }

        [Fact]
        public void DeserializeResourceAsync_Throws_HttpContentSerializationException_If_Serializer_Throws()
        {
            dynamic response = CreateResponseForResourceDeserializationTests(
                responseDeserializerFactory: () => new MockedHttpContentSerializer()
            );

            Assert.ThrowsAsync<HttpContentSerializationException>(async () => await response.DeserializeResourceAsync());
        }

        [Fact]
        public async Task DeserializeResourceAsync_Returns_Empty_Variant_For_Undefined_Status_Code()
        {
            dynamic response = CreateResponseForResourceDeserializationTests(UndefinedStatusCode);
            var resource = await response.DeserializeResourceAsync();
            Assert.True(resource.IsEmpty);
        }

        [Fact]
        public async Task DeserializeResourceAsync_Returns_Dto1_For_Associated_Status_Code()
        {
            dynamic response = CreateResponseForResourceDeserializationTests(Dto1StatusCode);
            var resource = await response.DeserializeResourceAsync();
            Assert.True(resource.IsFirst);
            Assert.IsType<Dto1>(resource.Value);
        }
        
    }

}
