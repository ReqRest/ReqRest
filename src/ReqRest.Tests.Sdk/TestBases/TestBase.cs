namespace ReqRest.Tests.Sdk.TestBases
{
    using System;
    using ReqRest.Tests.Sdk.Mocks;
    using ReqRest.Tests.Sdk.Utilities;

    /// <summary>
    ///     Defines an abstract base class for test suites which need to test a service of a
    ///     specific type.
    ///     Inheriting from this class will provide default setup functionality.
    /// </summary>
    /// <typeparam name="T">The type of the service to be tested.</typeparam>
    public abstract class TestBase<T>
    {

        private readonly Lazy<T> _service;

        /// <summary>
        ///     Gets a single instance of the service to be tested (the SUT).
        ///     This instance is created using <see cref="CreateService"/> and will not change
        ///     for the remainder of the test case. 
        /// </summary>
        protected T Service => _service.Value;

        /// <summary>
        ///     Gets the value of the <see cref="Service"/> property, but as a <see langword="dynamic"/>.
        ///     This allows duck-typed tests.
        /// </summary>
        protected dynamic DynamicService => Service!;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TestBase{T}"/> class.
        /// </summary>
        public TestBase()
        {
            _service = new Lazy<T>(CreateService);
        }

        /// <summary>
        ///     Creates a new instance of the service to be tested (the SUT).
        ///     By default, this creates a new instance using <see cref="Activator.CreateInstance{T}"/>.
        /// </summary>
        /// <returns>
        ///     A new instance of the service to be tested (the SUT).
        /// </returns>
        protected virtual T CreateService()
        {
            // Dummy creates real instances of classes using mocks, so this is fine.
            // For interfaces, it creates mocks, so that's perfect too.
            return Dummy.For<T>();
        }

    }

}
