namespace ReqRest.Serializers
{
    using System;
    using System.Runtime.Serialization;
    using ReqRest.Serializers.Resources;

    /// <summary>
    ///     An exception that gets thrown by various members inside the library when an
    ///     <see cref="IHttpContentDeserializer"/> throws an exception during the deserialization
    ///     of a resource.
    /// </summary>
    [Serializable]
    public class HttpContentSerializationException : Exception
    {

        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpContentSerializationException"/> class
        ///     with a default error message.
        /// </summary>
        public HttpContentSerializationException()
            : this(message: null, innerException: null) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpContentSerializationException"/> class
        ///     with the specified error message.
        /// </summary>
        /// <param name="message">
        ///     The message that describes the error or <c>null</c> to use a default message.
        /// </param>
        public HttpContentSerializationException(string? message)
            : this(message, innerException: null) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpContentSerializationException"/> class
        ///     with the specified error message and and another exception which was the cause
        ///     of this exception.
        /// </summary>
        /// <param name="message">
        ///     The message that describes the error or <c>null</c> to use a default message.
        /// </param>
        /// <param name="innerException">
        ///     The exception that is the cause of this exception, or <c>null</c>, if no inner
        ///     exception is specified.
        /// </param>
        public HttpContentSerializationException(string? message, Exception? innerException)
            : base(message ?? GetDefaultMessage(), innerException) { }

        protected HttpContentSerializationException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        private static string GetDefaultMessage() =>
            ExceptionStrings.HttpContentSerializationException_Message;

    }

}
