namespace ReqRest.Tests.Sdk.TestBases
{
    using ReqRest.Tests.Sdk.Mocks;

    /// <summary>
    ///     An abstract base class for any test suite that needs to test one of ReqRest's builder
    ///     interfaces.
    /// </summary>
    public abstract class BuilderTestBase : TestBase<BuilderMock>
    {

        /// <summary>Creates a new <see cref="BuilderMock"/> instance.</summary>
        protected override BuilderMock CreateService() =>
            new BuilderMock();

    }

}
