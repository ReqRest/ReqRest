namespace ReqRest.Tests.Builders
{
    using System;
    using System.Net.Http;
    using ReqRest.Builders;
    using ReqRest.Tests.Sdk.Data;
    using ReqRest.Tests.Sdk.Mocks;
    using ReqRest.Tests.Sdk.Mocks.Providers;
    using Xunit;
    using static ReqRest.Tests.Sdk.Data.ParameterNullability;
    using ReqRest.Tests.Sdk.TestBases;

    public class HttpMethodBuilderExtensionsTests
    {

        public class PredefinedHttpMethodsTests : BuilderTestBase
        {

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
                    { "PATCH", builder => builder.Patch() }
                };

            [Theory]
            [MemberData(nameof(MethodTestData))]
            public void Named_Method_Extensions_Set_Correct_Http_Method(
                string expectedMethod, Action<IHttpMethodBuilder> setup)
            {
                setup(Service);
                Assert.Equal(expectedMethod, Service.Method.Method);
            }

        }

        public class SetMethodTests : BuilderTestBase
        {

            [Fact]
            public void String_Sets_Method()
            {
                var name = "TEST";
                Service.SetMethod(name);
                Assert.Equal(name, Service.Method.Method);
            }

            [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
            public void String_Throws_ArgumentNullException(IHttpMethodBuilder builder, string method = "GET")
            {
                Assert.Throws<ArgumentNullException>(() => HttpMethodBuilderExtensions.SetMethod(builder, method));
            }

            [Fact]
            public void HttpMethod_Sets_Method()
            {
                var name = new HttpMethod("TEST");
                Service.SetMethod(name);
                Assert.Same(name, Service.Method);
            }

            [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
            public void HttpMethod_Throws_ArgumentNullException(
                IHttpMethodBuilder builder, 
                [MockUsing(typeof(HttpMethodMockProvider))] HttpMethod method)
            {
                Assert.Throws<ArgumentNullException>(() => HttpMethodBuilderExtensions.SetMethod(builder, method));
            }

        }

    }

}
