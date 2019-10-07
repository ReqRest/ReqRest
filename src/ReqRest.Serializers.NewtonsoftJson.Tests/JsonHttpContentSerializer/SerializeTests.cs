namespace ReqRest.Serializers.NewtonsoftJson.Tests.JsonHttpContentSerializer
{
    using System.Threading.Tasks;
    using ReqRest.Serializers.NewtonsoftJson;
    using Xunit;
    using Newtonsoft.Json;
    using FluentAssertions;
    using ReqRest.Http;
    using System.Text;

    public class SerializeTests
    {

        [Theory]
        [MemberData(nameof(JsonTestData.Dtos), MemberType = typeof(JsonTestData))]
        public async Task Serializes_Object(MockDto dto)
        {
            var serializer = new JsonHttpContentSerializer();
            var serialized = serializer.Serialize(dto, encoding: null);
            var str = await serialized.ReadAsStringAsync();
            var expected = JsonConvert.SerializeObject(dto);
            str.Should().Be(expected);
        }

        [Fact]
        public void Resulting_HttpContent_Has_Json_Media_Type()
        {
            var serializer = new JsonHttpContentSerializer();
            var content = serializer.Serialize(new object(), null);
            content.Headers.ContentType.MediaType.Should().Be(MediaType.ApplicationJson);
        }

        [Theory]
        [InlineData("utf-8", "utf-8")]
        [InlineData("ascii", "us-ascii")]
        public void Uses_Specified_Encoding_In_Charset(string encodingStr, string expectedCharset)
        {
            var encoding = Encoding.GetEncoding(encodingStr);
            var serializer = new JsonHttpContentSerializer();
            var content = serializer.Serialize(new object(), encoding);
            content.Headers.ContentType.CharSet.Should().Be(expectedCharset);
        }

    }

}
