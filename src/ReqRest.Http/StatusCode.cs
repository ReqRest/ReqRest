namespace ReqRest.Http
{

    /// <summary>
    ///     Defines constants for the standard HTTP status codes.
    /// </summary>
    public static class StatusCode
    {

        /// <summary>
        ///     A "wildcard" status code which is <see langword="null"/>.
        ///     Can, for example, be used by <see cref="StatusCodeRange"/> to indicate that
        ///     every available status code is meant.
        /// </summary>
        public static readonly int? Wildcard = null;

        public const int Continue = 100;
        public const int SwitchingProtocols = 101;
        public const int Processing = 102;
        public const int EarlyHints = 103;

        public const int Ok = 200;
        public const int Created = 201;
        public const int Accepted = 202;
        public const int NonAuthoritativeInformation = 203;
        public const int NoContent = 204;
        public const int ResetContent = 205;
        public const int PartialContent = 206;
        public const int MultiStatus = 207;
        public const int AlreadyReported = 208;
        public const int ImUsed = 226;

        public const int Ambiguous = 300;
        public const int MultipleChoices = 300;
        public const int Moved = 301;
        public const int MovedPermanently = 301;
        public const int Found = 302;
        public const int Redirect = 302;
        public const int RedirectMethod = 303;
        public const int SeeOther = 303;
        public const int NotModified = 304;
        public const int UseProxy = 305;
        public const int Unused = 306;
        public const int RedirectKeepVerb = 307;
        public const int TemporaryRedirect = 307;
        public const int PermanentRedirect = 308;

        public const int BadRequest = 400;
        public const int Unauthorized = 401;
        public const int PaymentRequired = 402;
        public const int Forbidden = 403;
        public const int NotFound = 404;
        public const int MethodNotAllowed = 405;
        public const int NotAcceptable = 406;
        public const int ProxyAuthenticationRequired = 407;
        public const int RequestTimeout = 408;
        public const int Conflict = 409;
        public const int Gone = 410;
        public const int LengthRequired = 411;
        public const int PreconditionFailed = 412;
        public const int RequestEntityTooLarge = 413;
        public const int PayloadTooLarge = 413;
        public const int RequestUriTooLong = 414;
        public const int UnsupportedMediaType = 415;
        public const int RequestedRangeNotSatisfiable = 416;
        public const int ExpectationFailed = 417;
        public const int ImATeapot = 418;
        public const int MisdirectedRequest = 421;
        public const int UnprocessableEntity = 422;
        public const int Locked = 423;
        public const int FailedDependency = 424;
        public const int TooEarly = 425;
        public const int UpgradeRequired = 426;
        public const int PreconditionRequired = 428;
        public const int TooManyRequests = 429;
        public const int RequestHeaderTooLarge = 431;
        public const int UnavailableForLegalReasons = 451;

        public const int InternalServerError = 500;
        public const int NotImplemented = 501;
        public const int BadGateway = 502;
        public const int ServiceUnavailable = 503;
        public const int GatewayTimeout = 504;
        public const int HttpVersionNotSupported = 505;
        public const int VariantAlsoNegotiates = 506;
        public const int InsufficientStorage = 507;
        public const int LoopDetected = 508;
        public const int NotExtended = 510;
        public const int NetworkAuthenticationRequired = 511;

    }

}
