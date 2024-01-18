namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a flow relative CSS value.
    /// </summary>
    public class CssFlowRelativeValue<T> : ICssMultipleValue
        where T : ICssValue
    {
        #region Fields

        private readonly T[] _values;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS flow relative value container.
        /// </summary>
        /// <param name="values">The items to contain.</param>
        public CssFlowRelativeValue(T[] values = null)
        {
            _values = values ?? Array.Empty<T>();
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public ICssValue this[Int32 index]
        {
            get
            {
                return index switch
                {
                    0 => Start,
                    1 => (ICssValue)End,
                    _ => throw new ArgumentOutOfRangeException(nameof(index)),
                };
            }
        }

        /// <inheritdoc />
        public String CssText
        {
            get
            {
                var l = _values.Length;
                var parts = new String[l];

                for (var i = 0; i < l; i++)
                {
                    parts[i] = _values[i].CssText;
                }

                if (l == 2 && parts[1].Is(parts[0]))
                {
                    l = 1;
                    parts = new[] { parts[0] };
                }

                return String.Join(" ", parts);
            }
        }

        /// <inheritdoc />
        public T Start => _values.Length > 0 ? _values[0] : default;

        /// <inheritdoc />
        public T End => _values.Length > 1 ? _values[1] : Start;

        /// <inheritdoc />
        public Int32 Count => 2;

        #endregion

        #region Methods

        IEnumerator<ICssValue> IEnumerable<ICssValue>.GetEnumerator()
        {
            yield return Start;
            yield return End;
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<ICssValue>)this).GetEnumerator();

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var values = _values.Select(v => (T)v.Compute(context)).ToArray();
            return new CssFlowRelativeValue<T>(values);
        }

        #endregion
    }

    /// <summary>
    /// Represents a flow relative CSS value.
    /// </summary>
    sealed class CssFlowRelativeValue : CssFlowRelativeValue<ICssValue>
    {
        #region ctor

        public CssFlowRelativeValue(ICssValue[] values = null)
            : base(values)
        {
        }

        #endregion
    }
}
