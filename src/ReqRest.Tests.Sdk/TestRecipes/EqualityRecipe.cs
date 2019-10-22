namespace ReqRest.Tests.Sdk.TestRecipes
{
    using System;
    using Xunit;
    using ReqRest.Tests.Sdk.Data;
    using ReqRest.Tests.Sdk.Utilities;

    /// <summary>
    ///     A test recipe which tests various ways to compare two objects for equality using
    ///     methods like <see cref="object.Equals(object)"/>, <see cref="IEquatable{T2}.Equals(T2)"/>
    ///     or equality operators.
    /// </summary>
    /// <typeparam name="T1">The first type for equality comparison.</typeparam>
    /// <typeparam name="T2">The second type for equality comparison.</typeparam>
    public abstract class EqualityRecipe<T1, T2>
    {

        /// <summary>
        ///     Gets test data for objects which are considered to be equal.
        /// </summary>
        protected abstract TheoryData<T1, T2> EqualObjects { get; }

        /// <summary>
        ///     Gets test data for objects which are considered to be unequal.
        /// </summary>
        protected abstract TheoryData<T1, T2> UnequalObjects { get; }

        [SkippableTheory, InstanceMemberData(nameof(EqualObjects))]
        public virtual void Equals_Object_Returns_True_For_Equal_Objects(T1 a, T2 b)
        {
            Assert.True(a!.Equals(b));
        }

        [SkippableTheory, InstanceMemberData(nameof(UnequalObjects))]
        public virtual void Equals_Object_Returns_False_For_Unequal_Objects(T1 a, T2 b)
        {
            Assert.False(a!.Equals(b));
        }

        [SkippableTheory, InstanceMemberData(nameof(EqualObjects))]
        public virtual void Equals_IEquatable_Returns_True_For_Equal_Objects(T1 a, T2 b)
        {
            var equatable = a as IEquatable<T2>;
            Skip.If(equatable is null, reason: "The type does not implement the IEquatable interface.");
            Assert.True(equatable!.Equals(b));
        }

        [SkippableTheory, InstanceMemberData(nameof(UnequalObjects))]
        public virtual void Equals_IEquatable_Returns_False_For_Unequal_Objects(T1 a, T2 b)
        {
            var equatable = a as IEquatable<T2>;
            Skip.If(equatable is null, reason: "The type does not implement the IEquatable interface.");
            Assert.False(equatable!.Equals(b));
        }

        [SkippableTheory, InstanceMemberData(nameof(EqualObjects))]
        public virtual void Equality_Operator_Returns_True_For_Equal_Objects(T1 a, T2 b)
        {
            var op = TryGetEqualityOperator();
            Skip.If(op is null, reason: $"No equality operator exists for the types {typeof(T1)} and {typeof(T2)}.");
            Assert.True(op!(a, b));
        }

        [SkippableTheory, InstanceMemberData(nameof(UnequalObjects))]
        public virtual void Equality_Operator_Returns_False_For_Unequal_Objects(T1 a, T2 b)
        {
            var op = TryGetEqualityOperator();
            Skip.If(op is null, reason: $"No equality operator exists for the types {typeof(T1)} and {typeof(T2)}.");
            Assert.False(op!(a, b));
        }

        [SkippableTheory, InstanceMemberData(nameof(EqualObjects))]
        public virtual void Inequality_Operator_Returns_False_For_Equal_Objects(T1 a, T2 b)
        {
            var op = TryGetInequalityOperator();
            Skip.If(op is null, reason: $"No inequality operator exists for the types {typeof(T1)} and {typeof(T2)}.");
            Assert.False(op!(a, b));
        }

        [SkippableTheory, InstanceMemberData(nameof(UnequalObjects))]
        public virtual void Inequality_Operator_Returns_True_For_Unequal_Objects(T1 a, T2 b)
        {
            var op = TryGetInequalityOperator();
            Skip.If(op is null, reason: $"No inequality operator exists for the types {typeof(T1)} and {typeof(T2)}.");
            Assert.True(op!(a, b));
        }

        private static Func<T1, T2, bool>? TryGetEqualityOperator()
        {
            var op = ReflectionHelper.TryGetEqualityOperator(typeof(T1), typeof(T1), typeof(T2));
            if (op is null)
            {
                return null;
            }
            return (a, b) => (bool)op.Invoke(null, new object?[] { a, b });
        }

        private static Func<T1, T2, bool>? TryGetInequalityOperator()
        {
            var op = ReflectionHelper.TryGetInequalityOperator(typeof(T1), typeof(T1), typeof(T2));
            if (op is null)
            {
                return null;
            }
            return (a, b) => (bool)op.Invoke(null, new object?[] { a, b });
        }

    }

    /// <summary>
    ///     A test recipe which tests objects of the same type for equality by using
    ///     methods like <see cref="object.Equals(object)"/>, <see cref="IEquatable{T2}.Equals(T2)"/>
    ///     or equality operators.
    ///     This enhances the <see cref="EqualityRecipe{T1, T2}"/> with additional test cases.
    /// </summary>
    /// <typeparam name="T">The type to be used for equality comparison.</typeparam>
    public abstract class EqualityRecipe<T> : EqualityRecipe<T, T>
    {

        [SkippableTheory, InstanceMemberData(nameof(EqualObjects))]
        public virtual void GetHashCode_Returns_Same_Value_For_Equal_Objects(T a, T b)
        {
            Skip.If(a is null || b is null, reason: "Cannot compare hash codes if one side is null.");
            Assert.Equal(a!.GetHashCode(), b!.GetHashCode());
        }

        [SkippableTheory, InstanceMemberData(nameof(UnequalObjects))]
        public virtual void GetHashCode_Returns_Different_Value_For_Unequal_Objects(T a, T b)
        {
            Skip.If(a is null || b is null, reason: "Cannot compare hash codes if one side is null.");
            Assert.NotEqual(a!.GetHashCode(), b!.GetHashCode());
        }

    }

}
