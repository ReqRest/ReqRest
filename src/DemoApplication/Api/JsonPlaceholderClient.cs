namespace DemoApplication.Api
{
    using System;
    using DemoApplication.Api.Todos;
    using ReqRest.Client;

    /// <summary>
    ///     The REST client for the Json Placeholder API (https://jsonplaceholder.typicode.com).
    ///     This is the main entry point to the ReqRest API.
    /// </summary>
    public class JsonPlaceholderClient : RestClient
    {

        // The config can come from anywhere - just make it static for simplicity.
        // While not 100% required, you should always set the BaseUrl - otherwise, a standard
        // value will be used which is most likely going to be incorrect.
        private static readonly RestClientConfiguration DefaultConfig = new RestClientConfiguration
        {
            BaseUrl = new Uri("https://jsonplaceholder.typicode.com"),
        };

        public JsonPlaceholderClient()
            : base(DefaultConfig) { }

        // Interfaces are ideally all defined as functions.
        // This allows for readable methods sentence chains like
        //    client.Users(123).Todos().Get().FetchResponseAsync();
        //
        // When creating an interface, you must always pass a RestClient instance.
        // The interfaces require it, because this way, they get access to the config (and things
        // like the HttpClient).
        // 
        // It is recommended to always create new Instances here. A RestInterface is lightweight
        // (as long as you don't do anything heavy in your own interface class) and the transient
        // behavior is usually going to be the safest.


        // Checkout the TodosInterface for a fully commented RestInterface implementation
        // with additional tips and tricks at the bottom of the file.
        public TodosInterface Todos() =>
            new TodosInterface(this);

        // Checkout the TodoInterface for a commented parametrized RestInterface implementation.
        public TodoInterface Todos(int id) =>
            new TodoInterface(this, id);

    }

}
