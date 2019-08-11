namespace ReqRest.Tests.ApiResponse
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ReqRest;
    using ReqRest.Http;
    using ReqRest.Serializers;
    using ReqRest.Tests;

    public abstract class ApiResponseTestBase
    {

        // The tests should cover that a certain DTO gets returned for a certain status code range.
        // It doesn't matter which status codes are used, but they should be different.
        protected const int UndefinedStatusCode = 404;
        protected const int Dto1StatusCode = 201;
        protected const int Dto2StatusCode = 202;
        protected const int Dto3StatusCode = 203;
        protected const int Dto4StatusCode = 204;
        protected const int Dto5StatusCode = 205;
        protected const int Dto6StatusCode = 206;
        protected const int Dto7StatusCode = 207;
        protected const int Dto8StatusCode = 208;

        /// <summary>
        ///     Creates an <see cref="ApiResponseBase"/> with the specified status code.
        ///     The response's content is an empty <see cref="ByteArrayContent"/> and its possible
        ///     response types are mapped to the <see cref="Dto1"/> to <see cref="Dto8"/> classes.
        /// 
        ///     The associated constants are defined as <see cref="Dto1StatusCode"/> and so on.
        /// 
        ///     The serializer simply uses <see cref="Activator.CreateInstance(System.Type)"/>
        ///     to initialize new Dto instances.
        /// </summary>
        /// <param name="statusCode">
        ///     The response's current status code.
        ///     Use one of the <see cref="Dto1StatusCode"/> or <see cref="UndefinedStatusCode"/> consts for this.
        /// </param>
        protected ApiResponseBase CreateResponseForResourceDeserializationTests(
            int statusCode = Dto1StatusCode,
            Func<IHttpContentDeserializer> responseDeserializerFactory = null)
        {
            responseDeserializerFactory ??= () => new MockedHttpContentSerializer(
                deserializeCoreImpl: (_, type) => Task.FromResult(Activator.CreateInstance(type)) 
            );
        
            var responseTypes = new List<ResponseTypeInfo>()
            {
                new ResponseTypeInfo(typeof(Dto1), new StatusCodeRange[] { Dto1StatusCode }, responseDeserializerFactory),
                new ResponseTypeInfo(typeof(Dto2), new StatusCodeRange[] { Dto2StatusCode }, responseDeserializerFactory),
                new ResponseTypeInfo(typeof(Dto3), new StatusCodeRange[] { Dto3StatusCode }, responseDeserializerFactory),
                new ResponseTypeInfo(typeof(Dto4), new StatusCodeRange[] { Dto4StatusCode }, responseDeserializerFactory),
                new ResponseTypeInfo(typeof(Dto5), new StatusCodeRange[] { Dto5StatusCode }, responseDeserializerFactory),
                new ResponseTypeInfo(typeof(Dto6), new StatusCodeRange[] { Dto6StatusCode }, responseDeserializerFactory),
                new ResponseTypeInfo(typeof(Dto7), new StatusCodeRange[] { Dto7StatusCode }, responseDeserializerFactory),
                new ResponseTypeInfo(typeof(Dto8), new StatusCodeRange[] { Dto8StatusCode }, responseDeserializerFactory),
            };
            var httpContent = new ByteArrayContent(Array.Empty<byte>());

            return CreateResponse(
                new HttpResponseMessage()
                {
                    StatusCode = (HttpStatusCode)statusCode,
                    Content = httpContent,
                }, 
                responseTypes
            );
        }

        protected abstract ApiResponseBase CreateResponse(
            HttpResponseMessage httpResponseMessage = null,
            IEnumerable<ResponseTypeInfo> possibleResponseTypes = null
        );

    }

}
