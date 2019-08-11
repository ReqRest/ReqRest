namespace ReqRest.Client.Tests.ApiResponseBase
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;
    using ReqRest.Client;
    using ReqRest.Http;

    public class GetCurrentResponseTypeInfoTests : ApiResponseBaseTestBase
    {

        [Theory]
        [MemberData(nameof(ResponseTypeData))]
        public void Holds_Correct_Value(
            IEnumerable<ResponseTypeInfo> possibleResponseTypes,
            int? expectedIndex,
            int forStatusCode)
        {
            // The property can be null, if the status code doesn't match any info.
            // -> Thus, the index can be null too.
            ResponseTypeInfo expected = null;
            if (expectedIndex is {})
            {
                expected = possibleResponseTypes.ElementAt(expectedIndex.Value);
            }
            
            var response = CreateResponse(possibleResponseTypes: possibleResponseTypes)
                .SetStatusCode(forStatusCode);
            
            response.GetCurrentResponseTypeInfo().Should().BeSameAs(expected);
        }

        public static TheoryData<IEnumerable<ResponseTypeInfo>, int?, int> ResponseTypeData =
            new TheoryData<IEnumerable<ResponseTypeInfo>, int?, int>()
            {
                // Empty collection.
                {
                    new ResponseTypeInfo[0],
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

        private static ResponseTypeInfo CreateInfo(params StatusCodeRange[] statusCodes) =>
            new ResponseTypeInfo(typeof(object), statusCodes, () => null);

    }

}
