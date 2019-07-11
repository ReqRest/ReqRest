namespace ReqRest.Client.Tests.ResponseTypeInfoBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using ReqRest.Http;
    using ReqRest.Serializers;
    using Xunit;

    public class BuildTests
    {

        // Not static, because the request values might mutate in the tests. Others for consistency.
        private readonly ApiRequest DefaultRequest = new ApiRequest(() => null);
        private readonly Type DefaultResponseType = typeof(object);
        private readonly Func<IHttpContentDeserializer> DefaultResponseDeserializer = () => null;
        private readonly IEnumerable<StatusCodeRange> DefaultStatusCodes = new[] { StatusCodeRange.All };

        [Fact]
        public void Throws_ArgumentNullException_For_ResponseDeserializerFactory()
        {
            Action testCode = () => Build(DefaultRequest, DefaultResponseType, null, DefaultStatusCodes);
            testCode.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void Throws_ArgumentNullException_For_ForStatusCodes()
        {
            Action testCode = () => Build(DefaultRequest, DefaultResponseType, DefaultResponseDeserializer, null);
            testCode.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void Throws_ArgumentException_For_ForStatusCodes()
        {
            var statusCodes = new StatusCodeRange[0];
            Action testCode = () => Build(DefaultRequest, DefaultResponseType, DefaultResponseDeserializer, statusCodes);
            testCode.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Returns_Same_Request_As_Specified_In_Constructor()
        {
            Build(
                DefaultRequest,
                DefaultResponseType,
                DefaultResponseDeserializer,
                DefaultStatusCodes
            )
            .Should().BeSameAs(DefaultRequest);
        }

        [Fact]
        public void Adds_Expected_ResponseTypeInfo_To_Request()
        {
            var req = Build(
                DefaultRequest,
                DefaultResponseType,
                DefaultResponseDeserializer,
                DefaultStatusCodes
            );
            var info = req.PossibleResponseTypes.First();

            info.ResponseType.Should().Be(DefaultResponseType);
            info.ResponseDeserializerFactory.Should().BeSameAs(DefaultResponseDeserializer);
            info.StatusCodes.Should().Equal(DefaultStatusCodes);
        }

        private static T Build<T>(
            T request, 
            Type responseType, 
            Func<IHttpContentDeserializer> responseDeserializerFactory, 
            IEnumerable<StatusCodeRange> forStatusCodes)
            where T : ApiRequestBase
        {
            var builder = new ResponseTypeInfoBuilder<T>(request, responseType);
            return builder.Build(responseDeserializerFactory, forStatusCodes);
        }

    }

}
