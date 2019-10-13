namespace ReqRest.Tests.Sdk.Data
{
    public interface IMockDataProvider
    {

        object Create();
    
    }

    public interface IMockDataProvider<T> : IMockDataProvider where T : notnull
    {

        new T Create();

    }

    public abstract class MockDataProvider<T> : IMockDataProvider<T> where T : notnull
    {

        object IMockDataProvider.Create() =>
            Create();

        public abstract T Create();

    }

}
