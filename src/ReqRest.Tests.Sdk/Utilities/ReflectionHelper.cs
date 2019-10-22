namespace ReqRest.Tests.Sdk.Utilities
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    ///     Provides helper methods dealing with reflection.
    /// </summary>
    public static class ReflectionHelper
    {

        private const string EqualityOperatorName = "op_Equality";
        private const string InequalityOperatorName = "op_Inequality";

        /// <summary>
        ///     Attempts to return an equality operator defined on the <paramref name="memberType"/>
        ///     which has the <paramref name="leftType"/> and <paramref name="rightType"/> as operands.
        /// </summary>
        /// <param name="memberType">The name of the operator method.</param>
        /// <param name="leftType">The type of the left operand.</param>
        /// <param name="rightType">The type of the right operand.</param>
        /// <returns>A <see cref="MethodInfo"/> for the operator or <see langword="null"/>.</returns>
        public static MethodInfo? TryGetEqualityOperator(Type memberType, Type leftType, Type rightType) =>
            TryGetBinaryOperator(EqualityOperatorName, memberType, leftType, rightType, typeof(bool));

        /// <summary>
        ///     Attempts to return an inequality operator defined on the <paramref name="memberType"/>
        ///     which has the <paramref name="leftType"/> and <paramref name="rightType"/> as operands.
        /// </summary>
        /// <param name="memberType">The name of the operator method.</param>
        /// <param name="leftType">The type of the left operand.</param>
        /// <param name="rightType">The type of the right operand.</param>
        /// <returns>A <see cref="MethodInfo"/> for the operator or <see langword="null"/>.</returns>
        public static MethodInfo? TryGetInequalityOperator(Type memberType, Type leftType, Type rightType) =>
            TryGetBinaryOperator(InequalityOperatorName, memberType, leftType, rightType, typeof(bool));

        /// <summary>
        ///     Attempts to return a binary operator with the specified name and signature for
        ///     either the left or the right type.
        /// </summary>
        /// <param name="operatorName">The name of the operator to be retrieved.</param>
        /// <param name="memberType">The name of the operator method.</param>
        /// <param name="leftType">The type of the left operand.</param>
        /// <param name="rightType">The type of the right operand.</param>
        /// <param name="returnType">The return type of the operator.</param>
        /// <returns>A <see cref="MethodInfo"/> for the operator or <see langword="null"/>.</returns>
        public static MethodInfo? TryGetBinaryOperator(
            string operatorName, Type memberType, Type leftType, Type rightType, Type returnType)
        {
            _ = operatorName ?? throw new ArgumentNullException(nameof(operatorName));
            _ = memberType ?? throw new ArgumentNullException(nameof(memberType));
            _ = leftType ?? throw new ArgumentNullException(nameof(leftType));
            _ = rightType ?? throw new ArgumentNullException(nameof(rightType));
            _ = returnType ?? throw new ArgumentNullException(nameof(returnType));

            var op = memberType.GetMethod(operatorName, BindingFlags.Public | BindingFlags.Static);
            var parameters = op?.GetParameters();

            if (   op is null
                || op.ReturnType != returnType
                || parameters?.Length != 2
                || parameters[0].ParameterType != leftType
                || parameters[1].ParameterType != rightType)
            {
                return null;
            }

            return op;
        }

        /// <summary>
        ///     Returns a value indicating whether the type is a <see cref="Nullable{T}"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A value indicating whether the type is a <see cref="Nullable{T}"/>.</returns>
        public static bool IsNullable(this Type type)
        {
            _ = type ?? throw new ArgumentNullException(nameof(type));
            return type.IsValueType && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        ///     Attempts to create an instance of the specified <paramref name="type"/> using
        ///     a parameter-less constructor (with potential optional parameters).
        /// </summary>
        /// <param name="type">The type of which to create an instance.</param>
        /// <returns>An instance of the <paramref name="type"/>.</returns>
        public static object CreateInstanceWithOptionalParameters(Type type)
        {
            _ = type ?? throw new ArgumentNullException(nameof(type));

            // This code can prob. be improved...

            // Try to create an instance with a default ctor first.
            try
            {
                return Activator.CreateInstance(type);
            }
            catch { }

            // If this fails, try to find a constructor with optional parameters only.
            var ctor = type.GetConstructors().First(ctor => ctor.GetParameters().All(param => param.IsOptional));
            var parameters = Enumerable.Repeat(Type.Missing, ctor.GetParameters().Length).ToArray();
            return CreateInstanceWithOptionalParameters(type, parameters);
        }

        /// <summary>
        ///     Attempts to create an instance of the specified <paramref name="type"/> using
        ///     the given <paramref name="parameters"/>.
        ///     In comparison to <see cref="Activator.CreateInstance(Type, object[])"/>, this method
        ///     supports optional parameters in the constructors.
        /// </summary>
        /// <param name="type">The type of which to create an instance.</param>
        /// <param name="parameters">Parameters to be passed to the constructor.</param>
        /// <returns>An instance of the <paramref name="type"/>.</returns>
        public static object CreateInstanceWithOptionalParameters(Type type, object?[] parameters)
        {
            _ = type ?? throw new ArgumentNullException(nameof(type));
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));

            return Activator.CreateInstance(
                type,
                BindingFlags.CreateInstance |
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.OptionalParamBinding,
                binder: null,
                parameters,
                CultureInfo.CurrentCulture
            );
        }

    }

}
