namespace ReqRest.Tests
{
    using System;
    using System.Collections.Generic;
    using ReqRest.Http;
    using ReqRest.Serializers;
    using ReqRest.Tests.Sdk.Data;
    using ReqRest.Tests.Sdk.Mocks;
    using ReqRest.Tests.Sdk.Mocks.Providers;
    using ReqRest.Tests.Sdk.TestRecipes;
    using Xunit;
    using static ReqRest.Tests.Sdk.Data.ParameterNullability;

    public class ResponseTypeInfoTests
    {

        public class ConstructorTests
        {

            [Theory, ArgumentNullExceptionData(NotNull, NotNull, NotNull)]
            public void Throws_ArgumentNullException_For_ResponseType(
                [MockUsing(typeof(TypeMockProvider), typeof(object))] Type responseType, 
                IEnumerable<StatusCodeRange> statusCodes, 
                Func<IHttpContentDeserializer> responseDeserializerFactory)
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ResponseTypeInfo(responseType, statusCodes, responseDeserializerFactory)
                );
            }

            [Fact]
            public void Throws_ArgumentException_For_Empty_StatusCodes()
            {
                Assert.Throws<ArgumentException>(
                    () => new ResponseTypeInfo(typeof(object), Array.Empty<StatusCodeRange>(), () => null)
                );
            }

        }

        public class ToStringTests : ToStringRecipe<ResponseTypeInfo>
        {

            protected override TheoryData<ResponseTypeInfo, Func<ResponseTypeInfo, string?>> Expectations => new TheoryData<ResponseTypeInfo, Func<ResponseTypeInfo, string?>>()
            {
                {
                    new ResponseTypeInfo(typeof(object), new[] { StatusCodeRange.All }, () => null!),
                    _ => $"Object: *"
                },
                {
                    new ResponseTypeInfo(typeof(int), new StatusCodeRange[] { (200, 300) }, () => null!),
                    _ => "Int32: [200, 300]" 
                },
                {
                    new ResponseTypeInfo(typeof(NoContent), new StatusCodeRange[] { (200, 300), 400, StatusCodeRange.All, (null, 100) }, () => null!),
                    _ => "NoContent: [200, 300], 400, *, [*, 100]"
                },
            };

        }

    }

}
