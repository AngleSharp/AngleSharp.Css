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
        // the last substitution, which is usually the same for all elements (and
        // longhands, as the value of a shorthand is shared by them)
        private Substitution _substitution;

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
                var values = parent is CssReferenceValue reference ? reference.GetVariableValues() :
                    new[] { new CssVariableValue(parent.CssText) };
                String text = null;

                foreach (var candidate in values)
                {
                    text = candidate.Substitute(context.Resolve);

                    if (text is not null)
                    {
                        break;
                    }
                }

                if (text is null)
                {
                    return null;
                }

                // Parse the substituted shorthand once its complete token stream
                // is known, rather than feeding it to an individual longhand's
                // converter and discarding the remaining components.
                var substitution = _substitution;

                if (substitution is null || !substitution.Matches(context.Context, text))
                {
                    var parser = context.Context?.GetService<ICssParser>() ?? new CssParser(context.Context);
                    var declarations = parser.ParseDeclaration(shorthandName + ":" + text);
                    substitution = new Substitution(context.Context, text, declarations);
                    _substitution = substitution;
                }

                return substitution.Declarations.GetProperty(longhandName)?.RawValue?.Compute(context);
            }

            return ((ICssValue)this).Compute(context);
        }

        Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is CssChildValue value && Equals(value);

        #endregion

        #region Substitution

        /// <summary>
        /// The (uncomputed) declarations parsed from the substituted shorthand, which
        /// are only read. Immutable, such that it can be shared without synchronization.
        /// </summary>
        private sealed class Substitution
        {
            public Substitution(IBrowsingContext context, String text, ICssStyleDeclaration declarations)
            {
                Context = context;
                Text = text;
                Declarations = declarations;
            }

            // the parser (and hence the result) is determined by the context
            public IBrowsingContext Context { get; }

            public String Text { get; }

            public ICssStyleDeclaration Declarations { get; }

            public Boolean Matches(IBrowsingContext context, String text) =>
                Object.ReferenceEquals(Context, context) && Text.Equals(text, StringComparison.Ordinal);
        }

        #endregion
    }
}
