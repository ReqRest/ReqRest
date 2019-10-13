namespace ReqRest.Tests.Builders
{
    using System;
    using ReqRest.Builders;
    using System.Net.Http;
    using Xunit;
    using ReqRest.Tests.Sdk.Data;
    using static ReqRest.Tests.Sdk.Data.ParameterNullability;
    using ReqRest.Tests.Sdk.TestBases;

    public class HttpRequestMessageBuilderExtensionsTests
    {

        public class ConfigureRequestTests
        {

            public class ModifyTests : TestBase<HttpRequestMessageBuilder>
            {

                [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
                public void Throws_ArgumentNullException(HttpRequestMessageBuilder builder, Action<HttpRequestMessage> configure)
                {
                    Assert.Throws<ArgumentNullException>(() => HttpRequestMessageBuilderExtensions.ConfigureRequest(builder, configure));
                }

                [Fact]
                public void Invokes_Configure()
                {
                    var wasCalled = false;
                    Service.ConfigureRequest(_ => wasCalled = true);
                    Assert.True(wasCalled);
                }

                [Fact]
                public void Passes_Current_HttpRequestMessage()
                {
                    Service.ConfigureRequest(res => Assert.Same(Service.HttpRequestMessage, res));
                }

            }

            public class SetTests : TestBase<HttpRequestMessageBuilder>
            {

                [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
                public void Throws_ArgumentNullException(HttpRequestMessageBuilder builder, Func<HttpRequestMessage> configure)
                {
                    Assert.Throws<ArgumentNullException>(() => HttpRequestMessageBuilderExtensions.ConfigureRequest(builder, configure));
                }

                [Fact]
                public void Invokes_Configure()
                {
                    var wasCalled = false;
                    Service.ConfigureRequest(() => { wasCalled = true; return new HttpRequestMessage(); });
                    Assert.True(wasCalled);
                }

                [Fact]
                public void Changes_Builders_HttpRequestMessage()
                {
                    using var expected = new HttpRequestMessage();
                    Service.ConfigureRequest(() => expected);
                    Assert.Same(expected, Service.HttpRequestMessage);
                }

            }

            public class ModifyAndSetTests : TestBase<HttpRequestMessageBuilder>
            {

                [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
                public void Throws_ArgumentNullException(HttpRequestMessageBuilder builder, Func<HttpRequestMessage, HttpRequestMessage> configure)
                {
                    Assert.Throws<ArgumentNullException>(() => HttpRequestMessageBuilderExtensions.ConfigureRequest(builder, configure));
                }

                [Fact]
                public void Invokes_Configure()
                {
                    var wasCalled = false;
                    Service.ConfigureRequest(res => { wasCalled = true; return res; });
                    Assert.True(wasCalled);
                }

                [Fact]
                public void Passes_Current_HttpRequestMessage()
                {
                    Service.ConfigureRequest(res =>
                    {
                        Assert.Same(Service.HttpRequestMessage, res);
                        return res;
                    });
                }

                [Fact]
                public void Changes_Builders_HttpRequestMessage()
                {
                    using var expected = new HttpRequestMessage();
                    Service.ConfigureRequest(_ => expected);
                    Assert.Same(expected, Service.HttpRequestMessage);
                }

            }

            public class SetRequestTests : TestBase<HttpRequestMessageBuilder>
            {

                [Fact]
                public void Sets_HttpRequestMessage()
                {
                    using var message = new HttpRequestMessage();
                    Service.SetRequest(message);
                    Assert.Same(message, Service.HttpRequestMessage);
                }

                [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
                public void Throws_ArgumentNullException(HttpRequestMessageBuilder builder, HttpRequestMessage httpRequestMessage)
                {
                    Assert.Throws<ArgumentNullException>(() => HttpRequestMessageBuilderExtensions.SetRequest(builder, httpRequestMessage));
                }

            }

        }

    }

}
