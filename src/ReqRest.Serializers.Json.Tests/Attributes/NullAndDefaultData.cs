namespace ReqRest.Serializers.Json.Tests.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit.Sdk;
    using System.Reflection;

    /// <summary>
    ///     A data attribute which yields two data rows for a test.
    ///     The first row returns null for every reference type parameter and the default value for a value type.
    ///     The second row returns a default instance created via <see cref="Activator.CreateInstance(Type)"/>
    ///     for every parameter.
    /// </summary>
    public class NullAndDefaultData : DataAttribute
    {

        public override IEnumerable<object?[]> GetData(MethodInfo testMethod)
        {
            var parameters = testMethod.GetParameters();
            yield return parameters.Select(p => DefaultValueOrNull(p.ParameterType)).ToArray();
            yield return parameters.Select(p => Activator.CreateInstance(p.ParameterType)).ToArray();
        }

        private object? DefaultValueOrNull(Type type) =>
            type.IsValueType ? Activator.CreateInstance(type) : null;

    }

}
