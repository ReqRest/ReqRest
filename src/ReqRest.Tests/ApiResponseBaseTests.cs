namespace ReqRest.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ReqRest.Tests.Sdk.TestBases;
    using System.Net.Http;
    using Moq;
    using Xunit;
    using ReqRest.Builders;
    using ReqRest.Http;

    public class ApiResponseBaseTests : TestBase<ApiResponseBase>
    {

        protected override ApiResponseBase CreateService()
        {
            return CreateService(null, null);
        }

        protected ApiResponseBase CreateService(HttpResponseMessage? httpResponseMessage, IEnumerable<ResponseTypeDescriptor>? possibleResponseTypes)
        {
            return new Mock<ApiResponseBase>(httpResponseMessage, possibleResponseTypes) { CallBase = true }.Object;
        }

        public class ConstructorTests : ApiResponseBaseTests
        {

            [Fact]
            public void Uses_Specified_HttpResponseMessage()
            {
                var msg = new HttpResponseMessage();
                var service = CreateService(msg, null);
                Assert.Same(msg, service.HttpResponseMessage);
            }

            [Fact]
            public void Uses_New_HttpRequestMessage_If_Null()
            {
                var service = CreateService(null, null);
                Assert.NotNull(service.HttpResponseMessage);
            }

            [Fact]
            public void Uses_Copy_Of_Specified_PossibleResponseTypes()
            {
                var types = new[]
                {
                    new ResponseTypeDescriptor(typeof(int), new StatusCodeRange[] { 200 }, () => null!),
                };

                var service = CreateService(null, types);
                Assert.Equal(types, service.PossibleResponseTypes);
                Assert.NotSame(types, service.PossibleResponseTypes);
            }

            [Fact]
            public void Uses_Empty_PossibleResponseTypesCollection_If_Null()
            {
                var service = CreateService(null, null);
                Assert.NotNull(service.PossibleResponseTypes);
                Assert.Empty(service.PossibleResponseTypes);
            }

        }

        public class GetCurrentResponseTypeDescriptorTests : ApiResponseBaseTests
        {

            [Theory]
            [MemberData(nameof(ResponseTypeData))]
            public void Holds_Correct_Value(
                IEnumerable<ResponseTypeDescriptor> possibleResponseTypes,
                int? expectedIndex,
                int forStatusCode)
            {
                // The property can be null, if the status code doesn't match any info.
                // -> Thus, the index can be null too.
                ResponseTypeDescriptor? expected = null;
                if (!(expectedIndex is null))
                {
                    expected = possibleResponseTypes.ElementAt(expectedIndex.Value);
                }

                var response = CreateService(null, possibleResponseTypes)
                    .SetStatusCode(forStatusCode);

                Assert.Same(expected, response.GetCurrentResponseTypeDescriptor());
            }

            public static TheoryData<IEnumerable<ResponseTypeDescriptor>, int?, int> ResponseTypeData()
            {
                return new TheoryData<IEnumerable<ResponseTypeDescriptor>, int?, int>()
                {
                    // Empty collection.
                    {
                        Array.Empty<ResponseTypeDescriptor>(),
                        /* expectedIndex: */ null,
                        /* forStatusCode: */ 200
                    },
                
                    // Basic status codes, including ranges.
                    {
                        new []
                        {
                            CreateInfo(200),
                        },
                        /* expectedIndex: */ 0,
                        /* forStatusCode: */ 200
                    },
                    {
                        new []
                        {
                            CreateInfo((200, 300)),
                        },
                        /* expectedIndex: */ 0,
                        /* forStatusCode: */ 200
                    },
                    {
                        new []
                        {
                            CreateInfo((null, 300)),
                        },
                        /* expectedIndex: */ 0,
                        /* forStatusCode: */ 200
                    },
                    {
                        new []
                        {
                            CreateInfo((100, null)),
                        },
                        /* expectedIndex: */ 0,
                        /* forStatusCode: */ 200
                    },
                    {
                        new []
                        {
                            CreateInfo(StatusCodeRange.All),
                        },
                        /* expectedIndex: */ 0,
                        /* forStatusCode: */ 200
                    },
                
                    // Multiple status codes at once (correct one included).
                    {
                        new []
                        {
                            CreateInfo(200),
                            CreateInfo(201),
                            CreateInfo(202),
                        },
                        /* expectedIndex: */ 0,
                        /* forStatusCode: */ 200
                    },
                    {
                        new []
                        {
                            CreateInfo(200),
                            CreateInfo(201),
                            CreateInfo(202),
                        },
                        /* expectedIndex: */ 2,
                        /* forStatusCode: */ 202
                    },

                    {
                        new []
                        {
                            CreateInfo((200, 299)),
                            CreateInfo((300, 399)),
                            CreateInfo((400, 499)),
                        },
                        /* expectedIndex: */ 0,
                        /* forStatusCode: */ 200
                    },
                    {
                        new []
                        {
                            CreateInfo((200, 299)),
                            CreateInfo((300, 399)),
                            CreateInfo((400, 499)),
                        },
                        /* expectedIndex: */ 1,
                        /* forStatusCode: */ 300
                    },
                    {
                        new []
                        {
                            CreateInfo((200, 299)),
                            CreateInfo((300, 399)),
                            CreateInfo((400, 499)),
                        },
                        /* expectedIndex: */ 2,
                        /* forStatusCode: */ 400
                    },
                
                    // Multiple ranges covering the same status, but some being more specific than others.
                    {
                        new []
                        {
                            CreateInfo((200, 299)),
                            CreateInfo((300, 399)),
                            CreateInfo((400, 499)),
                            CreateInfo(205),
                        },
                        /* expectedIndex: */ 0,
                        /* forStatusCode: */ 200
                    },
                    {
                        new []
                        {
                            CreateInfo((200, 299)),
                            CreateInfo((300, 399)),
                            CreateInfo((400, 499)),
                            CreateInfo(205),
                        },
                        /* expectedIndex: */ 3,
                        /* forStatusCode: */ 205
                    },
                    {
                        new []
                        {
                            CreateInfo((200, 299)),
                            CreateInfo((300, 399)),
                            CreateInfo((400, 499)),
                            CreateInfo((200, 250)),
                        },
                        /* expectedIndex: */ 3,
                        /* forStatusCode: */ 205
                    },
                
                    // Multiple status codes covering the same ranges (i.e. equal ones).
                    {
                        new []
                        {
                            CreateInfo((200, 300)),
                            CreateInfo((200, 300)),
                        },
                        /* expectedIndex: */ 0,
                        /* forStatusCode: */ 200
                    },
                    {
                        new []
                        {
                            CreateInfo((100, 200)),
                            CreateInfo((200, 300)),
                        },
                        /* expectedIndex: */ 0,
                        /* forStatusCode: */ 200
                    },
                };

                static ResponseTypeDescriptor CreateInfo(params StatusCodeRange[] statusCodes) =>
                    new ResponseTypeDescriptor(typeof(object), statusCodes, () => null!);
            }

        }

    }

}
