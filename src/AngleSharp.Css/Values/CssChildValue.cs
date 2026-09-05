#nullable disable
namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a CSS value that was born from a shorthand.
    /// </summary>
    sealed class CssChildValue : ICssValue, IEquatable<CssChildValue>
    {
        #region Fields

        private readonly ICssValue _parent;
        private readonly ICssValue _value;
        private readonly String _shorthandName;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a CSS child-parent container.
        /// </summary>
        /// <param name="parent">The reference to the shorthand value.</param>
        /// <param name="value">The value of the child, if any.</param>
        /// <param name="shorthandName">The shorthand that supplied the pending value.</param>
        public CssChildValue(ICssValue parent, ICssValue value = null, String shorthandName = null)
        {
            _parent = parent;
            _value = value;
            _shorthandName = shorthandName;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the shorthand.
        /// </summary>
        public ICssValue Parent => _parent;

        /// <summary>
        /// Gets the value of the longhand, if any.
        /// </summary>
        public ICssValue Value => _value;

        /// <summary>
        /// Gets the text representation of the longhand.
        /// </summary>
        public String CssText => _value?.CssText ?? String.Empty;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current value is equal to the provided one.
        /// </summary>
        /// <param name="other">The value to check against.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(CssChildValue other)
        {
            if (other is not null)
            {
                var comparer = EqualityComparer<ICssValue>.Default;
                return comparer.Equals(_parent, other._parent) && comparer.Equals(_value, other._value);
            }

            return false;
        }

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            var parent = _parent.Compute(context);
            var value = _value?.Compute(context);
            return new CssChildValue(parent, value, _shorthandName);
        }

        internal ICssValue Compute(ICssComputeContext context, String longhandName)
        {
            var parent = _parent;
            var shorthandName = _shorthandName;

            while (parent is CssChildValue child)
            {
                shorthandName = child._shorthandName;
                parent = child.Parent;
            }

            if (shorthandName is not null && parent is ICssRawValue)
            {
                var text = new CssVariableValue(parent.CssText).Substitute(context.Resolve);

                if (text is null)
                {
                    return null;
                }

                // Parse the substituted shorthand once its complete token stream
                // is known, rather than feeding it to an individual longhand's
                // converter and discarding the remaining components.
                var parser = context.Context?.GetService<ICssParser>() ?? new CssParser(context.Context);
                var declarations = parser.ParseDeclaration(shorthandName + ":" + text);
                return declarations.GetProperty(longhandName)?.RawValue?.Compute(context);
            }

            return ((ICssValue)this).Compute(context);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssChildValue value && Equals(value);

        #endregion
    }
}
