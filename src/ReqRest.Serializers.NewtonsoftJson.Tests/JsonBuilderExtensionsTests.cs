namespace ReqRest.Serializers.NewtonsoftJson.Tests
{
    using Moq;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using ReqRest.Serializers.NewtonsoftJson;
    using ReqRest.Tests.Sdk.TestBases;
    using Xunit;
    using ReqRest.Tests.Sdk.Models;
    using ReqRest.Serializers.NewtonsoftJson.Tests.TestData;
    using Moq.Protected;

    public class JsonBuilderExtensionsTests
    {

        public class SetJsonContentTests : BuilderTestBase
        {

            [Theory]
            [MemberData(nameof(JsonTestData.Dtos), MemberType = typeof(JsonTestData))]
            public async Task Serializes_Object_And_Sets_Content(SerializationDto dto)
            {
                var serializer = new JsonHttpContentSerializer();
                var expectedContent = serializer.Serialize(dto, Encoding.UTF8)!;
                var actualContent = Service.SetJsonContent(dto).Content!;

                var expectedString = await expectedContent.ReadAsStringAsync();
                var actualString = await actualContent.ReadAsStringAsync();
                Assert.Equal(expectedString, actualString);
            }

            [Fact]
            public void Passes_Parameters_To_User_Defined_Serializer()
            {
                var serializerMock = new Mock<JsonHttpContentSerializer>(new JsonSerializer()) { CallBase = true };
                var dto = new SerializationDto();

                Service.SetJsonContent(dto, Encoding.ASCII, serializerMock.Object);
                serializerMock.Protected().Verify("SerializeCore", Times.Once(), dto, typeof(SerializationDto), Encoding.ASCII);
            }

        }

    }

}
