namespace ReqRest.Tests.Sdk.Mocks.Providers
{
    using System;
    using ReqRest.Tests.Sdk.Data;

    public sealed class TypeMockProvider : MockDataProvider<Type>
    {

        public Type Type { get; }

        public TypeMockProvider(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public override Type Create() =>
            Type;

    }

}
