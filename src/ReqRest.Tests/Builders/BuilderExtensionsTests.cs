namespace ReqRest.Tests.Builders
{
    using System;
    using ReqRest.Builders;
    using Xunit;
    using ReqRest.Tests.Sdk.Data;
    using static ReqRest.Tests.Sdk.Data.ParameterNullability;
    using ReqRest.Tests.Sdk.TestBases;

    public class BuilderExtensionsTests
    {

        public class ConfigureTests : BuilderTestBase
        {

            [Fact]
            public void Executes_Action()
            {
                var didRun = false;
                Service.Configure(_ => didRun = true);
                Assert.True(didRun);
            }

            [Theory, ArgumentNullExceptionData(NotNull, NotNull)]
            public void Throws_ArgumentNullException(IBuilder builder, Action<IBuilder> configure)
            {
                Assert.Throws<ArgumentNullException>(() => BuilderExtensions.Configure(builder, configure));
            }

        }

        public class IfTests : BuilderTestBase
        {

            [Theory]
            [InlineData(true, true)]
            [InlineData(false, false)]
            public void Executes_Action_Depending_On_Condition(bool condition, bool shouldRun)
            {
                var didRun = false;
                Service.If(condition, _ => didRun = true);
                Assert.Equal(shouldRun, didRun);
            }

            [Theory, ArgumentNullExceptionData(NotNull, Null, NotNull)]
            public void Throws_ArgumentNullException(IBuilder builder, bool condition, Action<IBuilder> action)
            {
                Assert.Throws<ArgumentNullException>(() => BuilderExtensions.If(builder, condition, action));
            }

        }

        public class IfNotTests : BuilderTestBase
        {

            [Theory]
            [InlineData(true, false)]
            [InlineData(false, true)]
            public void Executes_Action_Depending_On_Condition(bool condition, bool shouldRun)
            {
                var didRun = false;
                Service.IfNot(condition, _ => didRun = true);
                Assert.Equal(shouldRun, didRun);
            }

            [Theory, ArgumentNullExceptionData(NotNull, Null, NotNull)]
            public void Throws_ArgumentNullException(IBuilder builder, bool condition, Action<IBuilder> action)
            {
                Assert.Throws<ArgumentNullException>(() => BuilderExtensions.IfNot(builder, condition, action));
            }

        }

    }

}
