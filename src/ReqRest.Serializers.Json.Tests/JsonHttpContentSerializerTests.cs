namespace ReqRest.Serializers.Json.Tests
{
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using ReqRest.Http;
    using ReqRest.Serializers.Json.Tests.TestData;
    using ReqRest.Tests.Sdk.Models;
    using ReqRest.Tests.Sdk.TestBases;
    using Xunit;

    public class JsonHttpContentSerializerTests
    {

        public class SerializeTests : TestBase<JsonHttpContentSerializer>
        {

            [Theory]
            [MemberData(nameof(JsonTestData.Dtos), MemberType = typeof(JsonTestData))]
            public async Task Serializes_Object(SerializationDto dto)
            {
                var httpContent = Service.Serialize(dto, encoding: null)!;
                var str = await httpContent.ReadAsStringAsync();
                var expected = JsonSerializer.Serialize(dto);
                Assert.Equal(expected, str);
            }

            [Fact]
            public void Resulting_HttpContent_Has_Json_Media_Type()
            {
                var httpContent = Service.Serialize(new object(), null)!;
                Assert.Equal(MediaType.ApplicationJson, httpContent.Headers.ContentType.MediaType);
            }

            [Theory]
            [InlineData("utf-8", "utf-8")]
            [InlineData("ascii", "us-ascii")]
            public void Uses_Specified_Encoding_In_Charset(string encodingStr, string expectedCharset)
            {
                var encoding = Encoding.GetEncoding(encodingStr);
                var httpContent = Service.Serialize(new object(), encoding)!;
                Assert.Equal(expectedCharset, httpContent.Headers.ContentType.CharSet);
            }

        }

        public class DeserializeTests : TestBase<JsonHttpContentSerializer>
        {

            [Theory]
            [MemberData(nameof(JsonTestData.ValidJson), MemberType = typeof(JsonTestData))]
            public async Task Deserializes_Object(string json)
            {
                var httpContent = new StringContent(json);
                var deserialized = await Service.DeserializeAsync(httpContent, typeof(SerializationDto));
                var expected = JsonSerializer.Deserialize<SerializationDto>(json);
                Assert.Equal(expected, deserialized);
            }

            [Theory]
            [MemberData(nameof(JsonTestData.InvalidJson), MemberType = typeof(JsonTestData))]
            public async Task Throws_HttpContentSerializationException_For_Invalid_Json(string json)
            {
                var httpContent = new StringContent(json);
                await Assert.ThrowsAsync<HttpContentSerializationException>(
                    async () => await Service.DeserializeAsync(httpContent, typeof(SerializationDto))
                );
            }

        }

    }

}
