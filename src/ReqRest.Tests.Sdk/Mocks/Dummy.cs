namespace ReqRest.Tests.Sdk.Mocks
{
    using System;
    using ReqRest.Tests.Sdk.Utilities;
    using Moq;
    using System.Reflection;
    using System.Linq;

    /// <summary>
    ///     Defines static helper methods for creating "dummy" objects, i.e. mocks that have no
    ///     real functionality.
    /// </summary>
    public static class Dummy
    {

        /// <summary>
        ///     Creates a dummy instance for a given method parameter.
        ///     If the parameter has a default value that is not <see langword="null"/>, this value
        ///     is used.
        ///     If the parameter is tagged with the <see cref="MockUsingAttribute"/>, the value
        ///     is retrieved using the associated mock data provider.
        ///     Otherwise, an instance is created using <see cref="For(Type)"/>.
        /// </summary>
        /// <param name="parameter">The method parameter for which an instance should be created.</param>
        /// <returns>An instance that can be passed to the specified <paramref name="parameter"/>.</returns>
        public static object For(ParameterInfo parameter)
        {
            _ = parameter ?? throw new ArgumentNullException(nameof(parameter));

            if (parameter.HasDefaultValue)
            {
                return parameter.RawDefaultValue;
            }

            var mockUsingAttribute = parameter.GetCustomAttribute<MockUsingAttribute>();
            if (!(mockUsingAttribute is null))
            {
                return mockUsingAttribute.GetMockDataProvider().Create();
            }

            return For(parameter.ParameterType);
        }

        /// <summary>
        ///     Creates a dummy instance of the specified type.
        ///     This instance may potentially be a mock without any functionality.
        /// </summary>
        /// <typeparam name="T">The type to be created.</typeparam>
        /// <returns>An instance of the type.</returns>
        public static T For<T>() =>
            (T)For(typeof(T));

        /// <summary>
        ///     Creates a dummy instance of the specified type.
        ///     This instance may potentially be a mock without any functionality.
        /// </summary>
        /// <param name="type">The type to be created.</param>
        /// <returns>An instance of the type.</returns>
        public static object For(Type type)
        {
            _ = type ?? throw new ArgumentNullException(nameof(type));

            if (type.IsNullable())
            {
                return ForNullable(type);
            }
            else if (type.IsValueType)
            {
                return ForValueType(type);
            }
            else if (type.IsArray)
            {
                return ForArray(type);
            }
            else if (type.IsClass && !typeof(Delegate).IsAssignableFrom(type))
            {
                return ForClass(type);
            }
            else
            {
                return UsingMocks(type);
            }
        }

        private static object ForNullable(Type nullableType)
        {
            var innerType = nullableType.GetGenericArguments()[0];
            return ForValueType(innerType);
        }

        private static object ForValueType(Type valueType)
        {
            // Since structs always have a parameter-less constructor, simply fall back to Activator.
            return Activator.CreateInstance(valueType);
        }
        
        private static object ForArray(Type type)
        {
            // An empty array should be enough.
            return Activator.CreateInstance(type, new object[] { 0 });
        }

        private static object ForClass(Type type)
        {
            // Classes are tricky because of constructor parameters.
            // In general, try to always use Activator to create an instance, because it supports
            // optional parameters with the right overload.
            // If no constructor without parameters is possible, try to use dummies for the creation.
            return TryCreateFromParameterlessConstructor(type)
                ?? TryCreateUsingCtorsRecursively(type)
                ?? throw new ArgumentException(
                       $"Cannot create an instance of the type {type.FullName}, " +
                       $"because no appropriate constructor can be called."
                   );
        }

        private static object? TryCreateFromParameterlessConstructor(Type type)
        {
            try
            {
                return ReflectionHelper.CreateInstanceWithOptionalParameters(type);
            }
            catch
            {
                return null;
            }
        }

        private static object? TryCreateUsingCtorsRecursively(Type type)
        {
            // Try to create an instance using each constructor, beginning with the one having the
            // least number of parameters.
            var ctors = type.GetConstructors().OrderBy(ctor => ctor.GetParameters().Length);

            foreach (var ctor in ctors)
            {
                try
                {
                    var parameters = ctor.GetParameters();
                    var parameterInstances = parameters.Select(param => For(param));
                    return ReflectionHelper.CreateInstanceWithOptionalParameters(type, parameterInstances.ToArray());
                }
                catch
                { 
                    // On errors, simply try the next constructor.
                }
            }

            // If we get here, no constructor could be used, i.e. this method failed.
            return null;
        }

        private static object UsingMocks(Type typeToMock)
        {
            // Use Moq to create a mock for the type.
            // Since Moq only provides a generic interface, we must fall back to some reflection hacks
            // that create the mock.
            var mockType = typeof(Mock<>).MakeGenericType(typeToMock);
            var property = mockType.GetProperty(nameof(Mock.Object), typeToMock);
            var mock = Activator.CreateInstance(mockType);
            return property.GetValue(mock);
        }

    }

}
