namespace ReqRest.Tests.Sdk.TestBases
{
    using System;
    using Moq;
    using ReqRest.Builders;

    /// <summary>
    ///     An abstract base class for any test suite that needs to test the abstract
    ///     <see cref="RestInterface"/> class.
    /// </summary>
    public abstract class RestInterfaceTestBase : TestBase<RestInterface>
    {

        /// <summary>The default value for the <see cref="RestClient"/> parameter when creating a new instance.</summary>
        protected virtual RestClient DefaultRestClient => new Mock<RestClient>(new RestClientConfiguration()).Object;

        /// <summary>The default value for the <see cref="IUrlProvider"/> parameter when creating a new instance.</summary>
        protected virtual IUrlProvider DefaultBaseUrlProvider
        {
            get
            {
                var mock = new Mock<IUrlProvider>();
                mock.Setup(x => x.GetUrlBuilder()).Returns(new UrlBuilder());
                return mock.Object;
            }
        }

        /// <summary>The default behavior for <see cref="RestInterface.BuildUrl(UrlBuilder)"/>.</summary>
        protected virtual Func<UrlBuilder, UrlBuilder> DefaultBuildUrl => builder => builder;
        
        /// <summary>
        ///     Creates a new <see cref="RestInterface"/> instance using a <see cref="RestClient"/> mock.
        /// </summary>
        protected override RestInterface CreateService()
        {
            return CreateService(DefaultRestClient, DefaultBaseUrlProvider, DefaultBuildUrl);
        }

        /// <summary>
        ///     Creates a new <see cref="RestInterface"/> instance using the provided parameters.
        /// </summary>
        /// <param name="restClient">A <see cref="RestClient"/> to be passed to the constructor.</param>
        /// <param name="baseUrlProvider">An <see cref="IUrlProvider"/> to be passed to the constructor.</param>
        /// <param name="buildUrl">
        ///     A function which mocks the functionality of the interface's 
        ///     <see cref="RestInterface.BuildUrl(UrlBuilder)"/> method.
        /// </param>
        protected virtual RestInterface CreateService(
            RestClient restClient,
            IUrlProvider? baseUrlProvider,
            Func<UrlBuilder, UrlBuilder> buildUrl)
        {
            var mock = new Mock<RestInterface>(restClient, baseUrlProvider) { CallBase = true };
            mock.Setup(x => x.BuildUrl(It.IsAny<UrlBuilder>())).Returns(buildUrl);
            return mock.Object;
        }

    }

}
