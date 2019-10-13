namespace ReqRest.Tests.Sdk.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ReqRest.Tests.Sdk.Data;
    using ReqRest.Tests.Sdk.Utilities;

    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class MockUsingAttribute : Attribute
    {

        public Type MockDataProviderType { get; }

        public IReadOnlyList<object?> ProviderParameters { get; }

        public MockUsingAttribute(Type mockDataProviderType, params object?[] providerParameters)
        {
            MockDataProviderType = mockDataProviderType ?? throw new ArgumentNullException(nameof(mockDataProviderType));
            ProviderParameters = providerParameters ?? throw new ArgumentNullException(nameof(providerParameters));

            if (!typeof(IMockDataProvider).IsAssignableFrom(mockDataProviderType))
            {
                throw new ArgumentException(
                    $"The type must be an instance of \"{typeof(IMockDataProvider).FullName}\".",
                    nameof(mockDataProviderType)
                );
            }
        }

        public virtual IMockDataProvider GetMockDataProvider()
        {
            if (ProviderParameters.Count == 0)
            {
                return (IMockDataProvider)ReflectionHelper.CreateInstanceWithOptionalParameters(MockDataProviderType);
            }
            return (IMockDataProvider)ReflectionHelper.CreateInstanceWithOptionalParameters(MockDataProviderType, ProviderParameters.ToArray());
        }

    }

}
