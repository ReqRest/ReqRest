namespace ReqRest.Http.Resources
{
    internal static class ExceptionStrings
    {

        public static string StatusCodeRange_FromCannotBeGreaterThanTo() =>
            $"Cannot create a status code range where {nameof(StatusCodeRange.From)} is greater " +
            $"than {nameof(StatusCodeRange.To)}.";

    }

}
