namespace ReqRest.Tests.Builders.HttpMessageResponseBuilderExtensions
{
    using System;
    using System.Net.Http;
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;

    public class ConfigureResponseTests : HttpResponseBuilderTestBase
    {

        [Fact]
        public void Modify_Throws_ArgumentNullException_For_Action()
        {
            Action testCode = () => Builder.ConfigureResponse((Action<HttpResponseMessage>)null);
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Set_Throws_Argument_Null_Exception_For_Func()
        {
            Action testCode = () => Builder.ConfigureResponse((Func<HttpResponseMessage>)null);
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ModifyAndSet_Throws_Argument_Null_Exception_For_Func()
        {
            Action testCode = () => Builder.ConfigureResponse((Func<HttpResponseMessage, HttpResponseMessage>)null);
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Modify_Throws_ArgumentNullException_For_Builder()
        {
            IHttpResponseMessageBuilder builder = null;
            Action testCode = () => builder.ConfigureResponse(_ => { });
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Set_Throws_Argument_Null_Exception_For_Builder()
        {
            IHttpResponseMessageBuilder builder = null;
            Action testCode = () => builder.ConfigureResponse(() => new HttpResponseMessage());
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ModifyAndSet_Throws_Argument_Null_Exception_For_Builder()
        {
            IHttpResponseMessageBuilder builder = null;
            Action testCode = () => builder.ConfigureResponse(req => req);
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Modify_Invokes_Action()
        {
            bool wasCalled = false;
            Builder.ConfigureResponse(req => wasCalled = true);
            wasCalled.Should().BeTrue();
        }

        [Fact]
        public void Set_Invokes_Func()
        {
            bool wasCalled = false;
            Builder.ConfigureResponse(() =>
            {
                wasCalled = true;
                return new HttpResponseMessage();
            });
            wasCalled.Should().BeTrue();
        }

        [Fact]
        public void ModifyAndSet_Invokes_Func()
        {
            bool wasCalled = false;
            Builder.ConfigureResponse(req =>
            {
                wasCalled = true;
                return req;
            });
            wasCalled.Should().BeTrue();
        }

        [Fact]
        public void Modify_Passes_Builders_Response()
        {
            Builder.ConfigureResponse(req =>
            {
                req.Should().BeSameAs(Builder.HttpResponseMessage);
            });
        }

        [Fact]
        public void ModifyAndSet_Passes_Builders_Response()
        {
            Builder.ConfigureResponse(req =>
            {
                req.Should().BeSameAs(Builder.HttpResponseMessage);
                return req;
            });
        }

        [Fact]
        public void Set_Changes_Builders_Response()
        {
            var expected = new HttpResponseMessage();
            Builder.ConfigureResponse(() => expected);
            Builder.HttpResponseMessage.Should().BeSameAs(expected);
        }

        [Fact]
        public void ModifyAndSet_Changes_Builders_Response()
        {
            var expected = new HttpResponseMessage();
            Builder.ConfigureResponse(() => expected);
            Builder.HttpResponseMessage.Should().BeSameAs(expected);
        }

    }

}
