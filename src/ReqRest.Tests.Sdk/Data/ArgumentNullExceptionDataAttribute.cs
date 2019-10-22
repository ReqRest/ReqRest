namespace ReqRest.Tests.Sdk.Data
{
    using System;
    using System.Collections.Generic;
    using Xunit.Sdk;
    using System.Reflection;
    using ReqRest.Tests.Sdk.Mocks;

    /// <summary>
    ///     Provides information about whether a parameter or value is allowed to be null or not.
    /// </summary>
    public enum ParameterNullability
    {

        /// <summary>
        ///     The parameter is allowed to be <see langword="null"/> and should not throw an
        ///     <see cref="ArgumentNullException"/>.
        /// </summary>
        Null,

        /// <summary>
        ///     The parameter is not allowed to be <see langword="null"/> and should thus throw
        ///     an <see cref="ArgumentNullException"/>.
        /// </summary>
        NotNull,

    }

    /// <summary>
    ///     A data attribute which returns test data for ensuring that methods throw an
    ///     <see cref="ArgumentNullException"/>.
    ///     
    ///     The attribute generates data rows so that each parameter of a theory that is supposed
    ///     to throw an <see cref="ArgumentNullException"/> if <see langword="null"/> is <see langword="null"/>
    ///     exactly once.
    ///     The other parameters in such a data row are set to real instances.
    ///     
    ///     The real instances can be created via a variety of ways:
    ///     - By default, they are provided using <see cref="Dummy.For(Type)"/>.
    ///     - If the parameter is optional (i.e. has a default value) that value is used.
    /// </summary>
    public sealed class ArgumentNullExceptionDataAttribute : DataAttribute
    {

        /// <summary>
        ///     Gets information about which parameter of the test method is supposed to be
        ///     <see langword="null"/>.
        ///     This list's indices matches the test method's parameters in order.
        /// </summary>
        public IReadOnlyList<ParameterNullability> ParameterNullability { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ArgumentNullExceptionDataAttribute"/> attribute.
        /// </summary>
        /// <param name="useNullForParameter">
        ///     Information about which of the test method's parameters should be set to <see langword="null"/>.
        ///     This list must exactly match the test method's parameter count, otherwise, an exception gets thrown.
        ///     
        ///     For parameters which are set to <see cref="ParameterNullability.Null"/>, the test data will
        ///     not contain a <see langword="null"/> value, i.e. only real mocks will be created.
        ///     
        ///     For parameters which are set to <see cref="ParameterNullability.NotNull"/>, the test data will
        ///     contain one row with a <see langword="null"/> value. The other rows will contain
        ///     mock objects though.
        /// </param>
        public ArgumentNullExceptionDataAttribute(params ParameterNullability[] useNullForParameter)
        {
            ParameterNullability = useNullForParameter;
        }

        /// <summary>
        ///     Generates the data rows for testing for <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="testMethod">The test method.</param>
        /// <returns>The test data.</returns>
        public override IEnumerable<object?[]> GetData(MethodInfo testMethod)
        {
            var methodParameters = testMethod.GetParameters();
            if (methodParameters.Length != ParameterNullability.Count)
            {
                throw new ArgumentException(
                    $"The test method's number of parameters does not match the number defined in " +
                    $"the {nameof(ArgumentNullExceptionDataAttribute)} attribute."
                );
            }

            for (int i = 0; i < ParameterNullability.Count; i++)
            {
                // Only generate a row if the parameter is not supposed to be null.
                // Otherwise, we'd have a row without a single null value.
                // This may not throw an exception.
                if (ParameterNullability[i] == Data.ParameterNullability.NotNull)
                {
                    yield return CreateDataRow(methodParameters, methodParameters[i]);
                }
            }
        }

        private static object?[] CreateDataRow(ParameterInfo[] parameters, ParameterInfo nullParameter)
        {
            var parametersToGenerate = parameters.Length;
            var row = new object?[parametersToGenerate];

            for (int i = 0; i < parametersToGenerate; i++)
            {
                var currentParameter = parameters[i];

                if (currentParameter == nullParameter)
                {
                    row[i] = null;
                }
                else
                {
                    row[i] = Dummy.For(currentParameter);
                }
            }

            return row;
        }

    }

}
