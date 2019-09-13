namespace ReqRest.Tests.Builders.BuilderExtensions
{
    using System;
    using FluentAssertions;
    using ReqRest.Builders;
    using Xunit;

    public class IfNotTests : HttpRequestBuilderTestBase
    {

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void Executes_Action_Depending_On_Condition(bool condition, bool shouldRun)
        {
            var didRun = false;
            Builder.IfNot(condition, _ => didRun = true);
            didRun.Should().Be(shouldRun);
        }

        [Fact]
        public void Throws_ArgumentNullException_For_Builder()
        {
            Action testCode = () => ((IBuilder)null).IfNot(true, _ => { });
            testCode.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Throws_ArgumentNullException_For_Action()
        {
            Action testCode = () => Builder.IfNot(true, null);
            testCode.Should().Throw<ArgumentNullException>();
        }

    }

}
