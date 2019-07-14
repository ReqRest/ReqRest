namespace ReqRest.Serializers.NewtonsoftJson.Tests.JsonHttpContentSerializer
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Newtonsoft.Json;
    using ReqRest.Serializers.NewtonsoftJson;
    using Xunit;

    public class DeserializeTests
    {

        [Theory]
        [MemberData(nameof(JsonTestData.ValidJson), MemberType = typeof(JsonTestData))]
        public async Task Deserializes_Object(string json)
        {
            var content = new StringContent(json);
            var serializer = new JsonHttpContentSerializer();
            var deserialized = await serializer.DeserializeAsync(content, typeof(MockDto));
            var expected = JsonConvert.DeserializeObject<MockDto>(json);
            deserialized.Should().BeEquivalentTo(expected);
        }
        
        [Theory]
        [MemberData(nameof(JsonTestData.InvalidJson), MemberType = typeof(JsonTestData))]
        public async Task Throws_HttpContentSerializationException_For_Invalid_Json(string json)
        {
            var content = new StringContent(json);
            var serializer = new JsonHttpContentSerializer();

            Func<Task> testCode = async () => await serializer.DeserializeAsync(content, typeof(MockDto));
            await testCode.Should().ThrowAsync<HttpContentSerializationException>();
        }

    }

}
