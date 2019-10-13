namespace ReqRest.Tests.Http
{
    using ReqRest.Http;
    using ReqRest.Tests.Sdk.TestRecipes;
    using Xunit;

    public class NoContentTests
    {

        public class EqualityTests : EqualityRecipe<NoContent>
        {

            protected override TheoryData<NoContent, NoContent> EqualObjects => new TheoryData<NoContent, NoContent>()
            {
                { new NoContent(), new NoContent() }
            };

            protected override TheoryData<NoContent, NoContent> UnequalObjects =>
                throw new SkipException("NoContent can never be unequal to another NoContent instance.");

        }

        public class GetHashCodeTests
        {

            [Fact]
            public void Returns_1()
            {
                var hashCode = new NoContent().GetHashCode();
                Assert.Equal(1, hashCode);
            }

        }

    }

}
