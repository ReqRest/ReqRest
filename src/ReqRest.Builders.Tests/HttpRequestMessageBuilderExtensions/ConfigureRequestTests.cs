namespace ReqRest.Builders.Tests.HttpRequestMessageBuilderExtensions
{
    using System;
    using System.Net.Http;
    using FluentAssertions;
    using Xunit;

    public class ConfigureRequestTests : HttpRequestBuilderTestBase
    {

        [Fact]
        public void Modify_Throws_ArgumentNullException_For_Action()
        {
            Action testCode = () => Builder.ConfigureRequest((Action<HttpRequestMessage>)null);
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Set_Throws_Argument_Null_Exception_For_Func()
        {
            Action testCode = () => Builder.ConfigureRequest((Func<HttpRequestMessage>)null);
            testCode.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void ModifyAndSet_Throws_Argument_Null_Exception_For_Func()
        {
            Action testCode = () => Builder.ConfigureRequest((Func<HttpRequestMessage, HttpRequestMessage>)null);
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Modify_Throws_ArgumentNullException_For_Builder()
        {
            IHttpRequestMessageBuilder builder = null;
            Action testCode = () => builder.ConfigureRequest(_ => { });
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Set_Throws_Argument_Null_Exception_For_Builder()
        {
            IHttpRequestMessageBuilder builder = null;
            Action testCode = () => builder.ConfigureRequest(() => new HttpRequestMessage());
            testCode.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void ModifyAndSet_Throws_Argument_Null_Exception_For_Builder()
        {
            IHttpRequestMessageBuilder builder = null;
            Action testCode = () => builder.ConfigureRequest(req => req);
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Modify_Invokes_Action()
        {
            bool wasCalled = false;
            Builder.ConfigureRequest(req => wasCalled = true);
            wasCalled.Should().BeTrue();
        }
        
        [Fact]
        public void Set_Invokes_Func()
        {
            bool wasCalled = false;
            Builder.ConfigureRequest(() => 
            {
                wasCalled = true;
                return new HttpRequestMessage();
            });
            wasCalled.Should().BeTrue();
        }
        
        [Fact]
        public void ModifyAndSet_Invokes_Func()
        {
            bool wasCalled = false;
            Builder.ConfigureRequest(req => 
            {
                wasCalled = true;
                return req;
            });
            wasCalled.Should().BeTrue();
        }

        [Fact]
        public void Modify_Passes_Builders_Request()
        {
            Builder.ConfigureRequest(req =>
            {
                req.Should().BeSameAs(Builder.HttpRequestMessage);
            });
        }

        [Fact]
        public void ModifyAndSet_Passes_Builders_Request()
        {
            Builder.ConfigureRequest(req =>
            {
                req.Should().BeSameAs(Builder.HttpRequestMessage);
                return req;
            });
        }

        [Fact]
        public void Set_Changes_Builders_Request()
        {
            var expected = new HttpRequestMessage();
            Builder.ConfigureRequest(() => expected);
            Builder.HttpRequestMessage.Should().BeSameAs(expected);
        }
        
        [Fact]
        public void ModifyAndSet_Changes_Builders_Request()
        {
            var expected = new HttpRequestMessage();
            Builder.ConfigureRequest(() => expected);
            Builder.HttpRequestMessage.Should().BeSameAs(expected);
        }

    }

}
