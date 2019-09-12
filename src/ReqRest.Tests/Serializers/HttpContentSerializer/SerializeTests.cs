namespace ReqRest.Tests.Serializers.HttpContentSerializer
{
    using System;
    using System.Text;
    using FluentAssertions;
    using ReqRest.Http;
    using ReqRest.Serializers;
    using ReqRest.Tests.Shared;
    using Xunit;

    public class SerializeTests
    {

        [Fact]
        public void Serializes_NoContent_To_Null()
        {
            var serializer = new MockedHttpContentSerializer();
            var httpContent = serializer.Serialize(new NoContent(), encoding: null);
            httpContent.Should().BeNull();
        }

        [Fact]
        public void Uses_DefaultEncoding_If_No_Encoding_Is_Specified()
        {
            Encoding encoding = null;
            var serializer = new MockedHttpContentSerializer()
            {
                SerializeCoreImpl = (c, e) => { encoding = e; return null; }
            };

            serializer.Serialize(null, encoding: null);
            encoding.Should().BeSameAs(serializer.DefaultEncoding);
        }

        [Fact]
        public void Wraps_Thrown_Exceptions_In_HttpContentSerializationException()
        {
            var ex = new Exception("To be thrown...");
            var serializer = new MockedHttpContentSerializer()
            {
                SerializeCoreImpl = (c, e) => throw ex,
            };

            Action testCode = () => serializer.Serialize(null, null);
            testCode.Should().Throw<HttpContentSerializationException>().And.InnerException.Should().BeSameAs(ex);
        }

        [Fact]
        public void Doesnt_Wrap_HttpContentSerializationException()
        {
            var ex = new HttpContentSerializationException("To be thrown...");
            var serializer = new MockedHttpContentSerializer()
            {
                SerializeCoreImpl = (c, e) => throw ex,
            };

            Action testCode = () => serializer.Serialize(null, null);
            testCode.Should().Throw<HttpContentSerializationException>().Which.Should().BeSameAs(ex);
        }

    }

}
