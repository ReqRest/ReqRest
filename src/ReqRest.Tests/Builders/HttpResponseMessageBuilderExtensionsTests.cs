namespace ReqRest.Tests.Builders
{
    using System;
    using ReqRest.Builders;
    using Xunit;
    using ReqRest.Tests.Sdk.Data;
    using static ReqRest.Tests.Sdk.Data.ParameterNullability;
    using System.Net.Http;
    using ReqRest.Tests.Sdk.TestBases;

    public class HttpResponseMessageBuilderExtensionsTests
    {

        public class ConfigureResponseTests
        {

            public class ModifyTests : TestBase<HttpResponseMessageBuilder>
            {

                [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
                public void Throws_ArgumentNullException(HttpResponseMessageBuilder builder, Action<HttpResponseMessage> configure)
                {
                    Assert.Throws<ArgumentNullException>(() => HttpResponseMessageBuilderExtensions.ConfigureResponse(builder, configure));
                }

                [Fact]
                public void Invokes_Configure()
                {
                    var wasCalled = false;
                    Service.ConfigureResponse(_ => wasCalled = true);
                    Assert.True(wasCalled);
                }

                [Fact]
                public void Passes_Current_HttpResponseMessage()
                {
                    Service.ConfigureResponse(res => Assert.Same(Service.HttpResponseMessage, res));
                }

            }

            public class SetTests : TestBase<HttpResponseMessageBuilder>
            {

                [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
                public void Throws_ArgumentNullException(HttpResponseMessageBuilder builder, Func<HttpResponseMessage> configure)
                {
                    Assert.Throws<ArgumentNullException>(() => HttpResponseMessageBuilderExtensions.ConfigureResponse(builder, configure));
                }

                [Fact]
                public void Invokes_Configure()
                {
                    var wasCalled = false;
                    Service.ConfigureResponse(() => { wasCalled = true; return new HttpResponseMessage(); });
                    Assert.True(wasCalled);
                }

                [Fact]
                public void Changes_Builders_HttpResponseMessage()
                {
                    using var expected = new HttpResponseMessage();
                    Service.ConfigureResponse(() => expected);
                    Assert.Same(expected, Service.HttpResponseMessage);
                }

            }

            public class ModifyAndSetTests : TestBase<HttpResponseMessageBuilder>
            {

                [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
                public void Throws_ArgumentNullException(HttpResponseMessageBuilder builder, Func<HttpResponseMessage, HttpResponseMessage> configure)
                {
                    Assert.Throws<ArgumentNullException>(() => HttpResponseMessageBuilderExtensions.ConfigureResponse(builder, configure));
                }

                [Fact]
                public void Invokes_Configure()
                {
                    var wasCalled = false;
                    Service.ConfigureResponse(res => { wasCalled = true; return res; });
                    Assert.True(wasCalled);
                }

                [Fact]
                public void Passes_Current_HttpResponseMessage()
                {
                    Service.ConfigureResponse(res =>
                    {
                        Assert.Same(Service.HttpResponseMessage, res);
                        return res;
                    });
                }

                [Fact]
                public void Changes_Builders_HttpResponseMessage()
                {
                    using var expected = new HttpResponseMessage();
                    Service.ConfigureResponse(_ => expected);
                    Assert.Same(expected, Service.HttpResponseMessage);
                }

            }

        }

        public class SetResponseTests : TestBase<HttpResponseMessageBuilder>
        {

            [Fact]
            public void Sets_HttpResponseMessage()
            {
                using var message = new HttpResponseMessage();
                Service.SetResponse(message);
                Assert.Same(message, Service.HttpResponseMessage);
            }

            [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
            public void Throws_ArgumentNullException(HttpResponseMessageBuilder builder, HttpResponseMessage httpResponseMessage)
            {
                Assert.Throws<ArgumentNullException>(() => HttpResponseMessageBuilderExtensions.SetResponse(builder, httpResponseMessage));
            }

        }

    }

}
