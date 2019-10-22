namespace ReqRest.Serializers.NewtonsoftJson.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using ReqRest.Http;
    using ReqRest.Serializers.NewtonsoftJson;
    using ReqRest.Tests.Sdk.TestBases;
    using Xunit;
    using ReqRest.Tests.Sdk.Models;
    using ReqRest.Serializers.NewtonsoftJson.Tests.TestData;
    using Moq.Protected;

    public class JsonBuilderExtensionsTests
    {

        public class AsJsonTests : ResponseTypeInfoBuilderTestBase
        {

            public delegate ApiRequest AsJsonInvoker(
                ResponseTypeInfoBuilder<ApiRequest> builder,
                IEnumerable<StatusCodeRange> forStatusCodes
            );

            // All of the AsJson() methods should ultimately do the same, some simply with more configuration.
            // Testing every little detail would require a lot of tests, so let's be pragmatic and only
            // ensure that the base functionality works for all of them.
            // If a bug is found, add test cases for these bugs step-by-step.
            public static TheoryData<AsJsonInvoker> AsJsonInvokers { get; } = new TheoryData<AsJsonInvoker>()
            {
                (builder, codes) => builder.AsJson(codes),
                (builder, codes) => builder.AsJson(codes.ToArray()),
                (builder, codes) => builder.AsJson((JsonSerializer?)null, codes),
                (builder, codes) => builder.AsJson((JsonSerializer?)null, codes.ToArray()),
                (builder, codes) => builder.AsJson((JsonSerializerSettings?)null, forStatusCodes: codes),
                (builder, codes) => builder.AsJson((JsonSerializerSettings?)null, forStatusCodes: codes.ToArray()),
                (builder, codes) => builder.AsJson((JsonSerializerSettings?)null, useDefaultSettings: false, forStatusCodes: codes),
                (builder, codes) => builder.AsJson((JsonSerializerSettings?)null, useDefaultSettings: false, forStatusCodes: codes.ToArray()),
                (builder, codes) => builder.AsJson((Func<JsonHttpContentSerializer>?)null, codes),
                (builder, codes) => builder.AsJson((Func<JsonHttpContentSerializer>?)null, codes.ToArray()),
            };

            [Theory, MemberData(nameof(AsJsonInvokers))]
            public void Adds_Expected_ResponseTypeDescriptor(AsJsonInvoker asJson)
            {
                var upgraded = asJson(Service, new StatusCodeRange[] { StatusCodeRange.All });
                var descriptor = upgraded.PossibleResponseTypes.First();
                var serializer = descriptor.HttpContentDeserializerProvider();

                Assert.Equal(new[] { StatusCodeRange.All }, descriptor.StatusCodes);
                Assert.NotNull(serializer);
                Assert.IsType<JsonHttpContentSerializer>(serializer);
            }

            [Theory, MemberData(nameof(AsJsonInvokers))]
            public void Throws_ArgumentNullException_For_Builder(AsJsonInvoker asJson)
            {
                Assert.Throws<ArgumentNullException>(() => asJson(null!, new StatusCodeRange[] { StatusCodeRange.All }));
            }

            [Theory, MemberData(nameof(AsJsonInvokers))]
            public void Throws_ArgumentNullException_For_ForStatusCodes(AsJsonInvoker asJson)
            {
                Assert.Throws<ArgumentNullException>(() => asJson(Service, null!));
            }

            [Theory, MemberData(nameof(AsJsonInvokers))]
            public void Throws_ArgumentException_For_Empty_ForStatusCodes(AsJsonInvoker asJson)
            {
                Assert.Throws<ArgumentException>(() => asJson(Service, Array.Empty<StatusCodeRange>()));
            }

        }

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
