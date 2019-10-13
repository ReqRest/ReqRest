namespace ReqRest.Tests.ApiResponseBase
{
    using System.Net.Http;
    using FluentAssertions;
    using Xunit;
    using ReqRest;
    using ReqRest.Http;

    public class ConstructorTests : ApiResponseBaseTestBase
    {
        
        [Fact]
        public void Uses_Specified_HttpResponseMessage()
        {
            var msg = new HttpResponseMessage();
            var response = CreateResponse(msg);
            response.HttpResponseMessage.Should().BeSameAs(msg);
        }

        [Fact]
        public void Uses_New_HttpRequestMessage_If_Null()
        {
            var response = CreateResponse(null);
            response.HttpResponseMessage.Should().NotBeNull();
        }

        [Fact]
        public void Uses_Copy_Of_Specified_PossibleResponseTypes()
        {
            var types = new[]
            {
                new ResponseTypeInfo(typeof(int), new StatusCodeRange[] { 200 }, () => null),
            }; 
            
            var response = CreateResponse(possibleResponseTypes: types);
            response.PossibleResponseTypes.Should().Equal(types);
            response.PossibleResponseTypes.Should().NotBeSameAs(types);
        }

        [Fact]
        public void Uses_Empty_PossibleResponseTypesCollection_If_Null()
        {
            var response = CreateResponse(possibleResponseTypes: null);
            response.PossibleResponseTypes.Should().NotBeNull();
        }
        
    }

}
