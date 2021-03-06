﻿namespace ReqRest.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    
#pragma warning disable CA1040 // Avoid empty interfaces

    /// <summary>
    ///     A marker interface which is used within this library to mark builder classes.
    ///     Implementing this interface opens a set of generic extension methods which are useful
    ///     for any kind of builder.
    ///
    ///     See the <see cref="BuilderExtensions"/> class for the available extension methods.
    /// </summary>
    /// <seealso cref="BuilderExtensions"/>
    public interface IBuilder { }

#pragma warning restore CA1040 // Avoid empty interfaces

    /// <summary>
    ///     Represents a builder which is able to build an <see cref="HttpContent"/>.
    /// </summary>
    /// <seealso cref="HttpRequestMessageBuilder"/>
    /// <seealso cref="HttpResponseMessageBuilder"/>
    /// <seealso cref="HttpContentBuilderExtensions"/>
    public interface IHttpContentBuilder : IBuilder
    {

        /// <summary>
        ///     Gets or sets the HTTP content which the builder builds.
        ///     This can be <see langword="null"/>.
        /// </summary>
        HttpContent? Content { get; set; }

    }

    /// <summary>
    ///     Represents a builder which is able to build a set of <see cref="HttpHeaders"/> of a specific type.
    /// </summary>
    /// <typeparam name="T">
    ///     The <see cref="HttpHeaders"/> type to be built.
    /// </typeparam>
    /// <seealso cref="HttpRequestMessageBuilder"/>
    /// <seealso cref="HttpResponseMessageBuilder"/>
    /// <seealso cref="HttpHeadersBuilderExtensions"/>
    public interface IHttpHeadersBuilder<out T> : IBuilder where T : HttpHeaders
    {

        /// <summary>
        ///     Gets the collection of HTTP headers which the builder builds.
        /// </summary>
        T Headers { get; }

    }

    /// <summary>
    ///     Represents a builder which is able to build the <see cref="HttpContentHeaders"/> of
    ///     an <see cref="HttpContent"/>.
    ///     
    ///     By implementing this interface, the implementer gains access to additional extension
    ///     methods defined in <see cref="HttpContentHeadersBuilderExtensions"/>.
    ///     See remarks for details.
    /// </summary>
    /// <remarks>
    ///     This interface is introduced so that classes which implement the <see cref="IHttpHeadersBuilder{T}"/>
    ///     interface multiple times (for example the <see cref="HttpRequestMessageBuilder"/>) gain
    ///     access to convenience extension methods that allow users to easily interact with the
    ///     headers of an <see cref="HttpContent"/>.
    ///     
    ///     If you don't want this behavior, implement the <see cref="IHttpContentBuilder"/>
    ///     and <see cref="IHttpHeadersBuilder{HttpContentHeaders}"/> separately.
    /// </remarks>
    /// <seealso cref="HttpContentHeadersBuilderExtensions"/>
    /// <seealso cref="HttpRequestMessageBuilder"/>
    /// <seealso cref="HttpResponseMessageBuilder"/>
    public interface IHttpContentHeadersBuilder : IHttpContentBuilder, IHttpHeadersBuilder<HttpContentHeaders> 
    {
        // This interface is basically a hack which allows the HttpRequestMessageBuilder and HttpResponseMessageBuilder
        // classes to have the AddContentHeader(), ... extension methods so that there can be no amiguity
        // regarding the "default" AddHeader(IHttpHeadersBuilder), ... methods.
        // 
        // This interface basically says:
        // The class already implements an IHttpHeadersBuilder of some kind, but it also has
        // an HttpContent which should have fluent builder methods.
    }

    /// <summary>
    ///     Represents a builder which is able to build an <see cref="Uri"/> for making an HTTP request.
    /// </summary>
    /// <seealso cref="HttpRequestMessageBuilder"/>
    /// <seealso cref="RequestUriBuilderExtensions"/>
    public interface IRequestUriBuilder : IBuilder
    {

        /// <summary>
        ///     Gets or sets the request <see cref="Uri"/> which the builder builds.
        ///     This can be <see langword="null"/>.
        /// </summary>
        Uri? RequestUri { get; set; }

    }

    /// <summary>
    ///     Represents a builder which is able to build an <see cref="HttpMethod"/>.
    /// </summary>
    /// <seealso cref="HttpRequestMessageBuilder"/>
    /// <seealso cref="HttpMethodBuilderExtensions"/>
    public interface IHttpMethodBuilder : IBuilder
    {

        /// <summary>
        ///     Gets or sets the HTTP method which the builder builds.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        HttpMethod Method { get; set; }

    }

    /// <summary>
    ///     Represents a builder which is able to build an HTTP protocol version.
    /// </summary>
    /// <seealso cref="HttpRequestMessageBuilder"/>
    /// <seealso cref="HttpResponseMessageBuilder"/>
    /// <seealso cref="HttpProtocolVersionBuilderExtensions"/>
    public interface IHttpProtocolVersionBuilder : IBuilder
    {

        /// <summary>
        ///     Gets or sets the HTTP protocol version which the builder builds.
        /// </summary>
        Version Version { get; set; }

    }

    /// <summary>
    ///     Represents a builder which is able to build properties of an HTTP request.
    /// </summary>
    /// <seealso cref="HttpRequestMessageBuilder"/>
    /// <seealso cref="HttpRequestPropertiesBuilderExtensions"/>
    public interface IHttpRequestPropertiesBuilder : IBuilder
    {

        /// <summary>
        ///     Gets the request's set of properties which are being built.
        /// </summary>
        IDictionary<string, object?> Properties { get; }

    }

    /// <summary>
    ///     Represents a builder which is able to build a reason phrase which typically gets sent by
    ///     a server within the context of an HTTP message.
    /// </summary>
    /// <seealso cref="HttpResponseMessageBuilder"/>
    /// <seealso cref="HttpResponseReasonPhraseBuilderExtensions"/>
    public interface IHttpResponseReasonPhraseBuilder : IBuilder
    {
        
        /// <summary>
        ///     Gets or sets the reason phrase which the builder builds.
        /// </summary>
        string? ReasonPhrase { get; set; }

    }

    /// <summary>
    ///     Represents a builder which is able to build an HTTP status code.
    /// </summary>
    /// <seealso cref="HttpResponseMessageBuilder"/>
    /// <seealso cref="HttpStatusCodeBuilderExtensions"/>
    public interface IHttpStatusCodeBuilder : IBuilder
    {

        /// <summary>
        ///     Gets or sets the HTTP status code which the builder builds.
        /// </summary>
        HttpStatusCode StatusCode { get; set; }

    }

    /// <summary>
    ///     Represents a builder which is able to build an <see cref="System.Net.Http.HttpRequestMessage"/>.
    /// </summary>
    /// <seealso cref="HttpRequestMessageBuilder"/>
    /// <seealso cref="HttpRequestMessageBuilderExtensions"/>
    public interface IHttpRequestMessageBuilder : IBuilder
    {

        /// <summary>
        ///     Gets or sets the <see cref="System.Net.Http.HttpRequestMessage"/> which is being built.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        HttpRequestMessage HttpRequestMessage { get; set; }

    }

    /// <summary>
    ///     Represents a builder which is able to build an <see cref="System.Net.Http.HttpResponseMessage"/>.
    /// </summary>
    /// <seealso cref="HttpResponseMessageBuilder"/>
    /// <seealso cref="HttpResponseMessageBuilderExtensions"/>
    public interface IHttpResponseMessageBuilder : IBuilder
    {

        /// <summary>
        ///     Gets or sets the <see cref="System.Net.Http.HttpResponseMessage"/> which is being built.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        HttpResponseMessage HttpResponseMessage { get; set; }

    }

}
