namespace ReqRest.Tests.RestInterface
{
    using System;
    using FluentAssertions;
    using ReqRest;
    using Moq;
    using Xunit;
    using System.Reflection;

    public class ConstructorTests : RestInterfaceTestBase
    {

        [Fact]
        public void Throws_ArgumentNullException_For_RestClient()
        {
            Action testCode = () => CreateInterface(null, null);
            testCode.Should().Throw<TargetInvocationException>().WithInnerException<ArgumentNullException>();
        }

        [Fact]
        public void Sets_BaseUrlProvider_To_Specified_RestClient_If_Null()
        {
            var @interface = CreateInterface(RestClient, baseUrlProvider: null);
            @interface.BaseUrlProvider.Should().BeSameAs(@interface.Client);
        }

        [Fact]
        public void Sets_BaseUrlProvider_To_Specified_Value_If_Not_Null()
        {
            var baseUrlProvider = new Mock<IUrlProvider>().Object;
            var @interface = CreateInterface(RestClient, baseUrlProvider);
            @interface.BaseUrlProvider.Should().BeSameAs(baseUrlProvider);
        }

    }

}
