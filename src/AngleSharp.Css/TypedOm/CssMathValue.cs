namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The base class of math expressions, e.g., calc(1px + 2em) or min(1px, 2em).
    /// https://www.w3.org/TR/css-typed-om-1/#cssmathvalue
    /// </summary>
    [DomName("CSSMathValue")]
    public abstract class CssMathValue : CssNumericValue
    {
        #region ctor

        internal CssMathValue(CssNumericType type)
            : base(type)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the operator of the expression.
        /// </summary>
        [DomName("operator")]
        public abstract CssMathOperator Operator { get; }

        #endregion

        #region Helpers

        internal static CssNumericValue[] CheckArguments(CssNumericValue[] values)
        {
            if (Check(values).Length == 0)
            {
                throw new DomException(DomError.Syntax);
            }

            return (CssNumericValue[])values.Clone();
        }

        /// <summary>
        /// Gets the type of the sum of the given values, e.g., for calc(), min(), or clamp().
        /// </summary>
        internal static CssNumericType AddTypes(params CssNumericValue[] values) =>
            CombineTypes(values, CssNumericType.Add);

        /// <summary>
        /// Gets the type of the product of the given values.
        /// </summary>
        internal static CssNumericType MultiplyTypes(params CssNumericValue[] values) =>
            CombineTypes(values, CssNumericType.Multiply);

        private static CssNumericType CombineTypes(CssNumericValue[] values, Func<CssNumericType, CssNumericType, CssNumericType?> combine)
        {
            var type = CheckArguments(values)[0].NumericType;

            for (var i = 1; i < values.Length; i++)
            {
                type = combine(type, values[i].NumericType) ??
                    throw new ArgumentException("The types of the values are not compatible.", nameof(values));
            }

            return type;
        }

        internal static Boolean AreEqual(CssNumericArray a, CssNumericArray b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }

            for (var i = 0; i < a.Length; i++)
            {
                if (!a[i].IsEqualTo(b[i]))
                {
                    return false;
                }
            }

            return true;
        }

        internal static void SerializeFunction(StringBuilder sb, String name, IEnumerable<CssNumericValue> values)
        {
            var first = true;
            sb.Append(name).Append('(');

            foreach (var value in values)
            {
                if (!first)
                {
                    sb.Append(", ");
                }

                value.Serialize(sb, true, true);
                first = false;
            }

            sb.Append(')');
        }

        internal static void Open(StringBuilder sb, Boolean nested, Boolean parenless)
        {
            if (!parenless)
            {
                sb.Append(nested ? "(" : "calc(");
            }
        }

        internal static void Close(StringBuilder sb, Boolean parenless)
        {
            if (!parenless)
            {
                sb.Append(')');
            }
        }

        #endregion
    }
}
