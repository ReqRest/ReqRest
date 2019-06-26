namespace ReqRest.Builders.Tests.HttpMethodBuilderExtensions
{
    using System;
    using System.Net.Http;
    using FluentAssertions;
    using Xunit;

    public class NamedMethodExtensionsTests : HttpRequestBuilderTestBase
    {

        [Theory]
        [MemberData(nameof(MethodTestData))]
        public void Named_Method_Extensions_Set_Correct_Http_Method(
            string expectedMethod, Action<IHttpMethodBuilder> setup)
        {
            setup(Builder);
            Builder.HttpRequestMessage.Method.Method.Should().Be(expectedMethod);
        }

        public static TheoryData<string, Action<IHttpMethodBuilder>> MethodTestData
            => new TheoryData<string, Action<IHttpMethodBuilder>>()
            {
                { HttpMethod.Get.Method, builder => builder.Get() },
                { HttpMethod.Put.Method, builder => builder.Put() },
                { HttpMethod.Post.Method, builder => builder.Post() },
                { HttpMethod.Delete.Method, builder => builder.Delete() },
                { HttpMethod.Options.Method, builder => builder.Options() },
                { HttpMethod.Trace.Method, builder => builder.Trace() },
                { HttpMethod.Head.Method, builder => builder.Head() },
            };

    }

}
