namespace ReqRest.Tests.Sdk.TestBases
{
    using System;

    /// <summary>
    ///     An abstract base class for any test suite that needs to test methods on a
    ///     <see cref="ResponseTypeInfoBuilder{TRequest}"/> instance.
    /// </summary>
    public class ResponseTypeInfoBuilderTestBase : TestBase<ResponseTypeInfoBuilder<ApiRequest>>
    {

        protected override ResponseTypeInfoBuilder<ApiRequest> CreateService()
        {
            return CreateService(new ApiRequest(() => null!), typeof(object));
        }

        protected virtual ResponseTypeInfoBuilder<ApiRequest> CreateService(ApiRequest request, Type responseType)
        {
            return new ResponseTypeInfoBuilder<ApiRequest>(request, responseType);
        }

    }

}
