namespace ReqRest.Tests.Builders.BuilderExtensions
{
    using System;
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;

    public class IfTests : HttpRequestBuilderTestBase
    {

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void Executes_Action_Depending_On_Condition(bool condition, bool shouldRun)
        {
            var didRun = false;
            Builder.If(condition, _ => didRun = true);
            didRun.Should().Be(shouldRun);
        }

        [Fact]
        public void Throws_ArgumentNullException_For_Builder()
        {
            Action testCode = () => ((IBuilder)null).If(true, _ => { });
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Throws_ArgumentNullException_For_Action()
        {
            Action testCode = () => Builder.If(true, null);
            testCode.Should().Throw<ArgumentNullException>();
        }

    }

}
