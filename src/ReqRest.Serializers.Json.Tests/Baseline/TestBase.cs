namespace ReqRest.Serializers.Json.Tests.Baseline
{
    using System;

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
        ///     Initializes a new instance of the <see cref="TestBase{T}"/> class.
        /// </summary>
        public TestBase()
        {
            _service = new Lazy<T>(CreateService);
        }

        /// <summary>
        ///     When overridden, creates a new instance of the service to be tested (the SUT).
        ///     This will always be a new instance.
        /// </summary>
        /// <returns>
        ///     A new instance of the service to be tested (the SUT).
        /// </returns>
        protected abstract T CreateService();

    }

}
