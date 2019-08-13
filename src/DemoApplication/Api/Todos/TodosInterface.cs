namespace DemoApplication.Api.Todos
{
    using System.Collections.Generic;
    using ReqRest;
    using ReqRest.Builders;
    using ReqRest.Serializers.NewtonsoftJson;
    using System;
    using ReqRest.Http;

    // This file contains a standard RestInterface for the /todos interface of the API.
    //
    // Some terms:
    // A RestInterface is just that - an interface in the REST API.
    // Some examples:
    // In the URL http://api.com/todos there is one interface: "todos".
    // In the URL http://ap.com/todos/123 there is also one interface: "todos/123".
    // In the URL http://api.com/users/1/todos there are two entirely different interfaces.
    //                          ^^^^^^^^ ^^^^^
    //                          1        2
    // The interesting part is that the second one builds up on the first one.
    // This is beyond the scope of this file, but it's important to know how to differentiate between
    // the different interfaces.
    //
    //
    // This file shows how to wrap the /todos interface of the REST API.
    // It supports two methods:
    //   GET  /todos
    //   POST /todos
    //
    // There are also methods like GET /todos/123, but as you can see above, that is considered
    // a different interface within the context of ReqRest.
    // This can easily be seen by the supported methods: GET /todos/123 supports GET, PUT, POST, DELETE.
    // This is not the case for /todos.


    // When wrapping an interface, you simply need to create a class which derives from RestInterface.
    // Ideally, you give the class the name of the API interface.
    public class TodosInterface : RestInterface
    {

        // Every RestInterface requires a RestClient. That's because when making real HTTP requests,
        // an interface requires an HttpClient (which comes from the RestClient).
        // 
        // If you hover over the base(..) part, you will see that RestInterface has an optional
        // second parameter.
        // This second parameter is a so-called base URL provider.
        // It is required for building the final URL to which the request will be made.
        // If nothing is passed, the restClient is used.
        // The RestClient has a BaseUrl in its config - this URL is then used when building the URL.
        public TodosInterface(RestClient restClient) 
            : base(restClient) { }

        // Again, when making a request, the library must know which URL should be called.
        // This URL is built here.
        // Ideally, every interface only knows about its own name.
        // The rest of the URL info should come from the interface's parent (the base URL provider).
        //
        // For example, this interface uses the RestClient as the base URL provider.
        // This means that the base URL (https://jsonplaceholder.typicode.com) is already set in the
        // builder below.
        // If we simply append the /todos path, we get the full request URL without having
        // to make any assumption about other interfaces.
        protected override UrlBuilder BuildUrl(UrlBuilder baseUrl) =>
            baseUrl / "todos";

        // Now comes the interesting part - how to create requests.
        // I believe that the code is going to be clearer than any comments here.
        // Feel free to hover over the methods or pop up IntelliSense. I spend a lot of time
        // writing the XML documentation.
        public ApiRequest<IEnumerable<TodoItem>> Get() =>
            BuildRequest()
                .Get()                                         // Optional. GET is the default method.
                .Receive<IEnumerable<TodoItem>>().AsJson(200); // (1)

        public ApiRequest<TodoItem> Post(TodoItem item) =>
            BuildRequest()
                .Post()
                .SetJsonContent(item)
                .Receive<TodoItem>().AsJson(200);              // (1)


        // (1) This is a part that probably requires some further explanation.
        //     By default, BuildRequest() starts with a non-generic ApiRequest instance.
        //     
        //     The whole idea of ReqRest is to add type-information to your REST Client.
        //     This is implemented via "request upgrades".
        //     
        //     As you can see, the Receive<T>() method is used to add compile-time information
        //     about possible API resources that can be received for specific status codes.
        //
        //     You declare which type gets returned for which status code and for which format
        //     and the library will handle the rest for you.
        //     (See Program.cs for basic usage).
        //
        //     The examples below show you some nice features of the whole upgrading API.

        // The default ApiRequest - it has no type information about the result.
        // If this is requested, the user will only have a status code and the HttpResponseMessage.
        // No utility methods for deserializing the HttpContent will be available.
        private ApiRequest DefaultApiRequest() =>
            BuildRequest();

        // The first upgrade - one type parameter was added.
        // A TodoItem can be received for the status codes below.
        private ApiRequest<TodoItem> Upgrade1() =>
            BuildRequest()
                .Receive<TodoItem>().AsJson(200, 300, 400);

        // A second upgrade - this is where it gets interesting.
        // Depending on the status code, the API would now select the appropriate type to deserialize
        // once the request was made.
        // Now, the types here don't really make any sense (no API would return that), but consider
        // a fictional API which returns your resource for 200, but an Error DTO otherwise.
        // Normal behavior which is easily implemented with this library.
        //
        // Upgrading continues like this. At the moment, ReqRest supports up to 8 type parameters,
        // i.e. 8 Receive<T>() calls.
        private ApiRequest<List<TodoItem>, TodoItem> Upgrade2() =>
            BuildRequest()
                .Receive<List<TodoItem>>().AsJson(200)
                .Receive<TodoItem>().AsJson(300);

        // From now on, treat Exception as an Error DTO please.
        //
        // This example shows how you declare that a type gets received on a status code range.
        // With the setup below, we declare that a TodoItem gets received for status codes 200-299
        // and an Exception otherwise.
        // If there are resolvable range collisions (like below - the second line/range lies in the first one)
        // ReqRest will always pick the more specific range.
        private ApiRequest<Exception, TodoItem> Ranges1() =>
            BuildRequest()
                .Receive<Exception>().AsJson(StatusCodeRange.All)
                .Receive<TodoItem>().AsJson(new StatusCodeRange(from: 200, to: 299));

        // This is an alternative way to write the above.
        private ApiRequest<Exception, TodoItem> Ranges2() =>
            BuildRequest()
                .Receive<Exception>().AsJson((null, null)) // == StatusCodeRange.All
                .Receive<TodoItem>().AsJson((200, 299));   // == new StatusCodeRange(200, 299)

        // You can also combine and mix the ranges. You can also declare multiple ranges at once.
        private ApiRequest<Exception, TodoItem, object> Ranges3() =>
            BuildRequest()
                .Receive<Exception>().AsJson(StatusCodeRange.All)
                .Receive<TodoItem>().AsJson((100, 200), 300)
                .Receive<object>().AsJson((201, 500), 50, 999);

        // This here would lead to an exception.
        // It is possible to define ranges which ReqRest cannot resolve.
        // Think about it yourself - for the status code 250: which range would you choose?
        // Hard to tell.
        // ReqRest will throw an exception in cases like these.
        private ApiRequest<Exception, Exception> RangesCollisions() =>
            BuildRequest()
                .Receive<Exception>().AsJson((100, 300))
                .Receive<Exception>().AsJson((200, 400));

    }

}
