namespace ReqRest.Tests.Sdk.TestRecipes
{
    using Xunit;
    using ReqRest.Tests.Sdk.Data;
    using System;

    /// <summary>
    ///     A test recipe which validates the result of an <see cref="object.ToString"/> call.
    /// </summary>
    /// <typeparam name="T">The type to be tested.</typeparam>
    public abstract class ToStringRecipe<T> where T : notnull
    {

        /// <summary>
        ///     Gets test rows of objects on which <see cref="object.ToString"/> should be called
        ///     and of the expected result.
        /// </summary>
        protected abstract TheoryData<T, Func<T, string?>> Expectations { get; }

        [SkippableTheory, InstanceMemberData(nameof(Expectations))]
        public virtual void Returns_Expected_String(T value, Func<T, string?> expectationProvider)
        {
            var actual = value.ToString();
            var expected = expectationProvider(value);
            Assert.Equal(expected, actual);
        }

    }

}
