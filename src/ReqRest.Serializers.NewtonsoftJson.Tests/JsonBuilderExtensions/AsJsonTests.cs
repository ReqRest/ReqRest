namespace ReqRest.Serializers.NewtonsoftJson.Tests.JsonBuilderExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Newtonsoft.Json;
    using ReqRest;
    using ReqRest.Http;
    using ReqRest.Serializers.NewtonsoftJson;
    using Xunit;

    public class AsJsonTests
    {

        public delegate ApiRequest AsJsonInvoker(
            ResponseTypeInfoBuilder<ApiRequest> builder,
            IEnumerable<StatusCodeRange> forStatusCodes
        );

        public static TheoryData<AsJsonInvoker> AsJsonInvokers { get; } =
            new TheoryData<AsJsonInvoker>()
            {
                (builder, codes) => builder.AsJson(codes),
                (builder, codes) => builder.AsJson(codes?.ToArray()),
                (builder, codes) => builder.AsJson((JsonSerializer)null, codes),
                (builder, codes) => builder.AsJson((JsonSerializer)null, codes?.ToArray()),
                (builder, codes) => builder.AsJson((JsonSerializerSettings)null, forStatusCodes: codes),
                (builder, codes) => builder.AsJson((JsonSerializerSettings)null, forStatusCodes: codes?.ToArray()),
                (builder, codes) => builder.AsJson((JsonSerializerSettings)null, useDefaultSettings: false, forStatusCodes: codes),
                (builder, codes) => builder.AsJson((JsonSerializerSettings)null, useDefaultSettings: false, forStatusCodes: codes?.ToArray()),
                (builder, codes) => builder.AsJson((Func<JsonHttpContentSerializer>)null, codes),
                (builder, codes) => builder.AsJson((Func<JsonHttpContentSerializer>)null, codes?.ToArray()),
            };

        [Theory]
        [MemberData(nameof(AsJsonInvokers))]
        public void Adds_Expected_ResponseTypeInfo(AsJsonInvoker asJson)
        {
            var builder = CreateBuilder();
            var upgraded = asJson(builder, new StatusCodeRange[] { StatusCodeRange.All });
            var info = upgraded.PossibleResponseTypes.First();
            var serializer = info.ResponseDeserializerFactory();

            info.StatusCodes.Should().Equal(StatusCodeRange.All);
            serializer
                .Should().NotBeNull()
                .And.BeOfType<JsonHttpContentSerializer>();
        }

        [Theory]
        [MemberData(nameof(AsJsonInvokers))]
        public void Throws_ArgumentNullException_For_Builder(AsJsonInvoker asJson)
        {
            Action testCode = () => asJson(null, new StatusCodeRange[] { StatusCodeRange.All });
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(AsJsonInvokers))]
        public void Throws_ArgumentNullException_For_ForStatusCodes(AsJsonInvoker asJson)
        {
            var builder = CreateBuilder();
            Action testCode = () => asJson(builder, null);
            testCode.Should().Throw<ArgumentNullException>();
        }
        
        [Theory]
        [MemberData(nameof(AsJsonInvokers))]
        public void Throws_ArgumentException_For_Empty_ForStatusCodes(AsJsonInvoker asJson)
        {
            var builder = CreateBuilder();
            Action testCode = () => asJson(builder, new StatusCodeRange[0]);
            testCode.Should().Throw<ArgumentException>();
        }

        private static ResponseTypeInfoBuilder<ApiRequest> CreateBuilder()
        {
            var request = new ApiRequest(() => null);
            return new ResponseTypeInfoBuilder<ApiRequest>(request, typeof(object));
        }

    }

}
