namespace ReqRest.Tests.Sdk.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit.Sdk;
    using Xunit;
    using System.Reflection;
    using System.Collections;

    /// <summary>
    ///     A data attribute base class which works similarly to XUnit's <see cref="MemberDataAttributeBase"/>,
    ///     but with the twist that it doesn't require the members to be static.
    ///     Instead, instances of the declaring class are created and then used for retrieving the data.
    ///     This opens up the possibility to declare tests with abstract test data members.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    [DataDiscoverer("Xunit.Sdk.MemberDataDiscoverer", "xunit.core")]
    public class InstanceMemberDataAttribute : DataAttribute
    {

        private const BindingFlags InstanceBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        ///     Returns <see langword="true"/> if the data attribute wants to skip enumerating data during discovery.
        ///     This will cause the theory to yield a single test case for all data, and the data discovery
        ///     will be during test execution instead of discovery.
        /// </summary>
        public bool DisableDiscoveryEnumeration { get; set; }

        /// <summary>
        ///     Gets the member name.
        /// </summary>
        public string MemberName { get; }

        /// <summary>
        ///     Gets the parameters passed to the member. Only supported for static methods.
        /// </summary>
        public object?[] Parameters { get; }

        /// <summary>
        ///     Gets or sets the type to retrieve the member from. If not set, then the property
        ///     will be retrieved from the unit test class.
        /// </summary>
        public Type? MemberType { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="InstanceMemberDataAttribute"/> class.
        /// </summary>
        /// <param name="memberName">The member name.</param>
        /// <param name="parameters">
        ///     The type to retrieve the member from. If not set, then the property
        ///     will be retrieved from the unit test class.
        /// </param>
        public InstanceMemberDataAttribute(string memberName, params object?[]? parameters)
        {
            MemberName = memberName ?? throw new ArgumentNullException(nameof(memberName));
            Parameters = parameters ?? Array.Empty<object>();
        }

        /// <summary>
        ///     Returns the data rows returned by a member which is fetched from an instance
        ///     created by <see cref="CreateMemberInstance(Type)"/>.
        ///     This may return an empty set of data rows.
        /// </summary>
        /// <param name="testMethod">The test method for which the data should be returned.</param>
        /// <returns>A set of data rows.</returns>
        public override IEnumerable<object?[]> GetData(MethodInfo testMethod)
        {
            _ = testMethod ?? throw new ArgumentNullException(nameof(testMethod));
            var actualMemberType = MemberType ?? testMethod.ReflectedType;
            return GetDataRowsOrThrow(actualMemberType);
        }

        private IEnumerable<object?[]> GetDataRowsOrThrow(Type actualMemberType)
        {
            var instance = CreateMemberInstance(actualMemberType);
            var accessor = GetDataAccessor(instance, actualMemberType);
            if (accessor is null)
            {
                throw new ArgumentException($"Could not find the member {MemberName} on type {actualMemberType.FullName}.");
            }

            var data = accessor?.Invoke();
            return data?.Cast<object?[]>() ?? Enumerable.Empty<object?[]>();
        }

        /// <summary>
        ///     Creates a new instance of the class which defines the data source.
        ///     This instance is then used for retrieving the data member.
        /// </summary>
        /// <param name="actualMemberType">The type for which an instance should be created.</param>
        /// <returns>An instance of the <paramref name="actualMemberType"/> type.</returns>
        protected object CreateMemberInstance(Type actualMemberType)
        {
            return Activator.CreateInstance(actualMemberType);
        }

        /// <summary>
        ///     Returns an accessor function which allows to retrieve the data from the specified
        ///     <paramref name="instance"/>.
        ///     By default, this looks for properties, methods and fields with the <see cref="MemberName"/>
        ///     and an appropriate return type.
        /// </summary>
        /// <param name="instance">The instance for which an accessor should be returned.</param>
        /// <param name="actualMemberType">The type for which an instance should be created.</param>
        /// <returns>An accessor function which returns a set of data rows.</returns>
        protected virtual Func<IEnumerable?>? GetDataAccessor(object instance, Type actualMemberType)
        {
            return GetPropertyAccessor(instance, actualMemberType)
                ?? GetMethodAccessor(instance, actualMemberType)
                ?? GetFieldAccessor(instance, actualMemberType)
                ?? null;
        }

        private Func<IEnumerable?>? GetPropertyAccessor(object instance, Type actualMemberType)
        {
            var prop = actualMemberType
                .GetProperties(InstanceBindingFlags)
                .FirstOrDefault(prop => prop.Name == MemberName && typeof(IEnumerable).IsAssignableFrom(prop.PropertyType));

            if (prop is null)
            {
                return null;
            }
            return () => (IEnumerable?)prop.GetValue(instance);
        }

        private Func<IEnumerable?>? GetMethodAccessor(object instance, Type actualMemberType)
        {
            var method = actualMemberType
                .GetMethods(InstanceBindingFlags)
                .FirstOrDefault(method => method.Name == MemberName && typeof(IEnumerable).IsAssignableFrom(method.ReturnType));

            if (method is null)
            {
                return null;
            }
            return () => (IEnumerable?)method.Invoke(instance, Parameters);
        }

        private Func<IEnumerable?>? GetFieldAccessor(object instance, Type actualMemberType)
        {
            var field = actualMemberType
                .GetProperties(InstanceBindingFlags)
                .FirstOrDefault(field => field.Name == MemberName && typeof(IEnumerable).IsAssignableFrom(field.PropertyType));

            if (field is null)
            {
                return null;
            }
            return () => (IEnumerable?)field.GetValue(instance);
        }

    }

}
