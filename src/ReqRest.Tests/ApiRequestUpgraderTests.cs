namespace ReqRest.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ReqRest.Http;
    using ReqRest.Serializers;
    using ReqRest.Tests.Sdk.Data;
    using ReqRest.Tests.Sdk.Mocks;
    using ReqRest.Tests.Sdk.Mocks.Providers;
    using ReqRest.Tests.Sdk.TestBases;
    using Xunit;
    using static ReqRest.Tests.Sdk.Data.ParameterNullability;

    public class ApiRequestUpgraderTests
    {

        public class ConstructorTests
        {

            [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
            public void Throws_ArgumentNullException(
                ApiRequest request, 
                [MockUsing(typeof(TypeMockProvider))] Type responseType)
            {
                Assert.Throws<ArgumentNullException>(() => new ApiRequestUpgrader<ApiRequest>(request, responseType));
            }

        }

        public class UpgradeTests : ApiRequestUpgraderTestBase
        {

            [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
            public void Throws_ArgumentNullException(
                Func<IHttpContentDeserializer> responseDeserializerFactory,
                IEnumerable<StatusCodeRange> forStatusCodes)
            {
                Assert.Throws<ArgumentNullException>(() => Service.Upgrade(responseDeserializerFactory, forStatusCodes));
            }

            [Fact]
            public void Throws_ArgumentException_For_Empty_forStatusCodes()
            {
                var statusCodes = Array.Empty<StatusCodeRange>();
                Assert.Throws<ArgumentException>(() => Service.Upgrade(() => null!, statusCodes));
            }

            [Fact]
            public void Returns_Same_Request_As_Specified_In_Constructor()
            {
                var request = new ApiRequest(() => null!);
                var builder = new ApiRequestUpgrader<ApiRequest>(request, typeof(object));
                var upgraded = builder.Upgrade(() => null!, new[] { StatusCodeRange.All });
                Assert.Same(request, upgraded);
            }

            [Fact]
            public void Adds_Expected_ResponseTypeDescriptor_To_Request()
            {
                Func<IHttpContentDeserializer> responseDeserializerFactory = () => null!;
                var responseType = typeof(ApiRequestUpgraderTests); // Any type is fine.
                var statusCodes = new[] { StatusCodeRange.All, 13, 7 };
                var builder = new ApiRequestUpgrader<ApiRequest>(new ApiRequest(() => null!), responseType);
                var upgraded = builder.Upgrade(responseDeserializerFactory, statusCodes);
                var descriptor = upgraded.PossibleResponseTypes.First();

                Assert.Equal(responseType, descriptor.ResponseType);
                Assert.Equal(responseDeserializerFactory, descriptor.HttpContentDeserializerProvider);
                Assert.Equal(statusCodes, descriptor.StatusCodes);
            }

        }

    }

}
