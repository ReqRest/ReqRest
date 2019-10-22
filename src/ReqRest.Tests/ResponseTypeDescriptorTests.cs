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

    public class ResponseTypeDescriptorTests
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
                    () => new ResponseTypeDescriptor(responseType, statusCodes, responseDeserializerFactory)
                );
            }

            [Fact]
            public void Throws_ArgumentException_For_Empty_StatusCodes()
            {
                Assert.Throws<ArgumentException>(
                    () => new ResponseTypeDescriptor(typeof(object), Array.Empty<StatusCodeRange>(), () => null!)
                );
            }

            [Fact]
            public void Uses_Distinct_List_Of_Provided_StatusCodeRange_Values()
            {
                var statusCodes = new StatusCodeRange[]
                {
                    (100, 200),
                    (null, 200),
                    (100, 200),
                    (null, 201),
                    (null, 200),
                };
                var expected = new StatusCodeRange[]
                {
                    (100, 200),
                    (null, 200),
                    (null, 201),
                };

                var descriptor = new ResponseTypeDescriptor(typeof(object), statusCodes, () => null!);
                Assert.Equal(expected, descriptor.StatusCodes);
            }

        }

        public class ToStringTests : ToStringRecipe<ResponseTypeDescriptor>
        {

            protected override TheoryData<ResponseTypeDescriptor, Func<ResponseTypeDescriptor, string?>> Expectations => new TheoryData<ResponseTypeDescriptor, Func<ResponseTypeDescriptor, string?>>()
            {
                {
                    new ResponseTypeDescriptor(typeof(object), new[] { StatusCodeRange.All }, () => null!),
                    _ => $"Object: *"
                },
                {
                    new ResponseTypeDescriptor(typeof(int), new StatusCodeRange[] { (200, 300) }, () => null!),
                    _ => "Int32: [200, 300]" 
                },
                {
                    new ResponseTypeDescriptor(typeof(NoContent), new StatusCodeRange[] { (200, 300), 400, StatusCodeRange.All, (null, 100) }, () => null!),
                    _ => "NoContent: [200, 300], 400, *, [*, 100]"
                },
            };

        }

    }

}
