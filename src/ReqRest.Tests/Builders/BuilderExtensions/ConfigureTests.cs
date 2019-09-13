namespace ReqRest.Tests.Builders.BuilderExtensions
{
    using System;
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;

    public class ConfigureTests : HttpRequestBuilderTestBase
    {

        [Fact]
        public void Executes_Action()
        {
            var didRun = false;
            Builder.Configure(_ => didRun = true);
            didRun.Should().BeTrue();
        }

        [Fact]
        public void Throws_ArgumentNullException_For_Builder()
        {
            Action testCode = () => ((IBuilder)null).Configure(_ => { });
            testCode.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void Throws_ArgumentNullException_For_Action()
        {
            Action testCode = () => Builder.Configure(null);
            testCode.Should().Throw<ArgumentNullException>();
        }

    }

}
