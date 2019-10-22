namespace ReqRest.Tests.Sdk.TestBases
{
    using System;

    /// <summary>
    ///     An abstract base class for any test suite that needs to test methods on a
    ///     <see cref="ApiRequestUpgrader{TRequest}"/> instance.
    /// </summary>
    public class ApiRequestUpgraderTestBase : TestBase<ApiRequestUpgrader<ApiRequest>>
    {

        protected override ApiRequestUpgrader<ApiRequest> CreateService()
        {
            return CreateService(new ApiRequest(() => null!), typeof(object));
        }

        protected virtual ApiRequestUpgrader<ApiRequest> CreateService(ApiRequest request, Type responseType)
        {
            return new ApiRequestUpgrader<ApiRequest>(request, responseType);
        }

    }

}
