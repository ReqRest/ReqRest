namespace ReqRest
{
    using System;
    using System.ComponentModel;
    using ReqRest.Resources;

    /// <summary>
    ///     Represents a range between two HTTP status codes and provides methods for determining
    ///     whether status codes fall within this range.
    /// </summary>
    public readonly struct StatusCodeRange : IEquatable<StatusCodeRange>
    {
        
        /// <summary>
        ///     A <see cref="StatusCodeRange"/> which covers every single status code that is available.
        /// </summary>
        public static readonly StatusCodeRange All = StatusCode.Wildcard;

        /// <summary>
        ///     Gets or sets the status code where the range starts.
        ///     
        ///     If <see langword="null"/>, every single status code before
        ///     <see cref="To"/> is covered by this range.
        ///     
        ///     If <see cref="To"/> is also <see langword="null"/>, this range covers every
        ///     available status code.
        /// </summary>
        public int? From { get; }

        /// <summary>
        ///     Gets or sets the status code where the range ends.
        ///     
        ///     If <see langword="null"/>, every single status code after
        ///     <see cref="From"/> is covered by this range.
        ///     
        ///     If <see cref="From"/> is also <see langword="null"/>, this range covers every
        ///     available status code.
        /// </summary>
        public int? To { get; }

        private int FromOrMinInt => From ?? int.MinValue;

        private int ToOrMaxInt => To ?? int.MaxValue;

        /// <summary>
        ///     Gets a value indicating whether this range spans only a single status code,
        ///     i.e. if <see cref="From"/> equals <see cref="To"/>.
        /// </summary>
        public bool IsSingleStatusCode => From == To && From != StatusCode.Wildcard;

        /// <summary>
        ///     Gets a value indicating whether <see cref="From"/> or <see cref="To"/> are
        ///     <see langword="null"/>, thus indicating a wildcard.
        /// </summary>
        public bool HasWildcardComponent => From == StatusCode.Wildcard || To == StatusCode.Wildcard;

        /// <summary>
        ///     Initializes a new <see cref="StatusCodeRange"/> instance which only spans a
        ///     single status code.
        /// </summary>
        /// <param name="singleStatusCode">
        ///     The single status code that is covered by this range.
        ///     If <see langword="null"/>, this range is equal to <see cref="All"/> and will
        ///     thus cover every single available status code.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="singleStatusCode"/> is less than zero.
        /// </exception>
        public StatusCodeRange(int? singleStatusCode)
            : this(singleStatusCode, singleStatusCode) { }

        /// <summary>
        ///     Initializes a new <see cref="StatusCodeRange"/> instance which starts at
        ///     <paramref name="from"/> and ends at <paramref name="to"/>.
        /// </summary>
        /// <param name="from">
        ///     the status code where the range starts.
        ///     
        ///     If <see langword="null"/>, every single status code before
        ///     <paramref name="to"/> is covered by this range.
        ///     
        ///     If <paramref name="to"/> is also <see langword="null"/>, this range covers every
        ///     available status code.
        /// </param>
        /// <param name="to">
        ///     The status code where the range ends.
        ///     
        ///     If <see langword="null"/>, every single status code after
        ///     <paramref name="from"/> is covered by this range.
        ///     
        ///     If <paramref name="from"/> is also <see langword="null"/>, this range covers every
        ///     available status code.
        /// </param>
        /// <exception cref="ArgumentException">
        ///     <paramref name="from"/> is larger than <paramref name="to"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="from"/> or <paramref name="to"/> is less than zero.
        /// </exception>
        public StatusCodeRange(int? from, int? to)
        {
            if (from < 0 || to < 0)
            {
                throw new ArgumentOutOfRangeException(ExceptionStrings.StatusCodeRange_CodesCannotBeNegative);
            }

            if (from > to)
            {
                throw new ArgumentException(ExceptionStrings.StatusCodeRange_FromCannotBeGreaterThanTo);
            }

            From = from;
            To = to;
        }

        /// <summary>
        ///     Returns a value indicating whether the specified <paramref name="statusCode"/>
        ///     falls within this range.
        /// </summary>
        /// <param name="statusCode">
        ///     The status code to be checked.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if <paramref name="statusCode"/> falls within this range;
        ///     <see langword="false"/> if not.
        /// </returns>
        public bool IsInRange(int statusCode)
        {
            return FromOrMinInt <= statusCode && statusCode <= ToOrMaxInt;
        }

        /// <summary>
        ///     Returns a value indicating whether the <paramref name="other"/> specified status
        ///     code range falls within this range.
        ///     This will also return <see langword="true"/> if the ranges have equal values.
        ///     For example, this will return <see langword="true"/> for <c>(200-300), (250,300)</c>.
        /// </summary>
        /// <param name="other">
        ///     The other status code range to be checked.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if <paramref name="other"/> falls within this range;
        ///     <see langword="false"/> if not.
        /// </returns>
        public bool IsInRange(StatusCodeRange other)
        {
            return FromOrMinInt <= other.FromOrMinInt && other.ToOrMaxInt <= ToOrMaxInt;
        }

        /// <summary>
        ///     Returns a value indicating whether this status code range equals the specified object.
        /// </summary>
        /// <param name="obj">An object to be compared with this status code range.</param>
        /// <returns>
        ///     <see langword="true"/> the two objects are considered equal;
        ///     <see langword="false"/> if not.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is StatusCodeRange other)
            {
                return this == other;
            }
            return false;
        }

        /// <summary>
        ///     Returns a value indicating whether this status code range equals the
        ///     <paramref name="other"/> one.
        /// </summary>
        /// <param name="other">Another <see cref="StatusCodeRange"/> instance.</param>
        /// <returns>
        ///     <see langword="true"/> the two ranges are considered equal;
        ///     <see langword="false"/> if not.
        /// </returns>
        public bool Equals(StatusCodeRange other)
        {
            return this == other;
        }

        /// <summary>
        ///     Returns a unique hash code for this status code range.
        /// </summary>
        /// <returns>A unique hash code.</returns>
        public override int GetHashCode()
        {
            return (From, To).GetHashCode();
        }

        /// <summary>
        ///     Returns a string similar to <c>(200-404)</c>, <c>(200)</c>, <c>(200-*)</c>,
        ///     depending on <see cref="From"/> and <see cref="To"/>.
        /// </summary>
        /// <returns>A string representing the current status code range.</returns>
        public override string ToString()
        {
            // Goal:
            // Represent the range like this:      200-404
            // If the range only spans one code:   404
            // If From/To is null, replace with *: 200-*   or   *
            if (IsSingleStatusCode || this == All)
            {
                var code = StatusCodeToString(From);
                return $"{code}";
            }
            else
            {
                var from = StatusCodeToString(From);
                var to = StatusCodeToString(To);
                return $"{from}-{to}";
            }
            
            static string StatusCodeToString(int? statusCode) =>
                statusCode is null ? "*" : $"{statusCode}";
        }

        /// <summary>
        ///     Deconstructs the instance.
        /// </summary>
        /// <param name="from">The from value.</param>
        /// <param name="to">The to value.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out int? from, out int? to)
        {
            from = From;
            to = To;
        }

        /// <seealso cref="Equals(StatusCodeRange)"/>
        public static bool operator ==(StatusCodeRange left, StatusCodeRange right) =>
            left.From == right.From && left.To == right.To;

        /// <seealso cref="Equals(StatusCodeRange)"/>
        public static bool operator !=(StatusCodeRange left, StatusCodeRange right) =>
            !(left == right);

        /// <summary>
        ///     Creates a new <see cref="StatusCodeRange"/> instance which only spans the
        ///     specified <paramref name="singleStatusCode"/>.
        /// </summary>
        /// <param name="singleStatusCode">
        ///     The single status code that is covered by this range.
        ///     If <see langword="null"/>, this range is equal to <see cref="All"/> and will
        ///     thus cover every single available status code.
        /// </param>
        public static implicit operator StatusCodeRange(int? singleStatusCode) =>
            new StatusCodeRange(singleStatusCode);

        /// <summary>
        ///     Creates a new <see cref="StatusCodeRange"/> instance with the range values defined
        ///     by the specified tuple.
        /// </summary>
        /// <param name="range">
        ///     A tuple where the first value corresponds to <see cref="From"/> and the second
        ///     value to <see cref="To"/>.
        /// </param>
        public static implicit operator StatusCodeRange((int? From, int? To) range) =>
            new StatusCodeRange(range.From, range.To);

    }

}
