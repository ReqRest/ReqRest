namespace ReqRest.Serializers.Json.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ReqRest.Http;
    using ReqRest.Serializers.Json;
    using ReqRest.Tests.Sdk.TestBases;
    using Xunit;
    using System.Text.Json;

    public class ApiRequestUpgraderExtensionsTests
    {

        public class AsJsonTests : ApiRequestUpgraderTestBase
        {

            public delegate ApiRequest AsJsonInvoker(
                ApiRequestUpgrader<ApiRequest> requestUpgrader,
                IEnumerable<StatusCodeRange> forStatusCodes
            );

            // All of the AsJson() methods should ultimately do the same, some simply with more configuration.
            // Testing every little detail would require a lot of tests, so let's be pragmatic and only
            // ensure that the base functionality works for all of them.
            // If a bug is found, add test cases for these bugs step-by-step.
            public static TheoryData<AsJsonInvoker> AsJsonInvokers { get; } = new TheoryData<AsJsonInvoker>()
            {
                (upgrader, codes) => upgrader.AsJson(codes),
                (upgrader, codes) => upgrader.AsJson(codes.ToArray()),
                (upgrader, codes) => upgrader.AsJson((JsonSerializerOptions?)null, codes),
                (upgrader, codes) => upgrader.AsJson((JsonSerializerOptions?)null, codes.ToArray()),
                (upgrader, codes) => upgrader.AsJson((Func<JsonHttpContentSerializer>?)null, codes),
                (upgrader, codes) => upgrader.AsJson((Func<JsonHttpContentSerializer>?)null, codes.ToArray()),
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

    }

}
