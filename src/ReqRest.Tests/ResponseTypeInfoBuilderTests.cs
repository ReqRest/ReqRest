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

    public class ResponseTypeInfoBuilderTests
    {

        public class ConstructorTests
        {

            [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
            public void Throws_ArgumentNullException(
                ApiRequest request, 
                [MockUsing(typeof(TypeMockProvider))] Type responseType)
            {
                Assert.Throws<ArgumentNullException>(() => new ResponseTypeInfoBuilder<ApiRequest>(request, responseType));
            }

        }

        public class BuildTests : TestBase<ResponseTypeInfoBuilder<ApiRequest>>
        {

            protected override ResponseTypeInfoBuilder<ApiRequest> CreateService() =>
                new ResponseTypeInfoBuilder<ApiRequest>(new ApiRequest(() => null!), typeof(object));

            [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
            public void Throws_ArgumentNullException(
                Func<IHttpContentDeserializer> responseDeserializerFactory,
                IEnumerable<StatusCodeRange> forStatusCodes)
            {
                Assert.Throws<ArgumentNullException>(() => Service.Build(responseDeserializerFactory, forStatusCodes));
            }

            [Fact]
            public void Throws_ArgumentException_For_Empty_forStatusCodes()
            {
                var statusCodes = Array.Empty<StatusCodeRange>();
                Assert.Throws<ArgumentException>(() => Service.Build(() => null!, statusCodes));
            }

            [Fact]
            public void Returns_Same_Request_As_Specified_In_Constructor()
            {
                var request = new ApiRequest(() => null!);
                var builder = new ResponseTypeInfoBuilder<ApiRequest>(request, typeof(object));
                var upgraded = builder.Build(() => null!, new[] { StatusCodeRange.All });
                Assert.Same(request, upgraded);
            }

            [Fact]
            public void Adds_Expected_ResponseTypeInfo_To_Request()
            {
                Func<IHttpContentDeserializer> responseDeserializerFactory = () => null!;
                var responseType = typeof(ResponseTypeInfoBuilderTests); // Any type is fine.
                var statusCodes = new[] { StatusCodeRange.All, 13, 7 };
                var builder = new ResponseTypeInfoBuilder<ApiRequest>(new ApiRequest(() => null!), responseType);
                var upgraded = builder.Build(responseDeserializerFactory, statusCodes);
                var info = upgraded.PossibleResponseTypes.First();

                Assert.Equal(responseType, info.ResponseType);
                Assert.Equal(responseDeserializerFactory, info.ResponseDeserializerFactory);
                Assert.Equal(statusCodes, info.StatusCodes);
            }

        }

    }

}
