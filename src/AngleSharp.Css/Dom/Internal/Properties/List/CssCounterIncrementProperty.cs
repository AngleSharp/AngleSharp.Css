namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/counter-increment
    /// </summary>
    sealed class CssCounterIncrementProperty : CssProperty
    {
        #region Fields

        // Former way of computing for IElement element:
        /*
            var pairs = CounterConverter.Convert(Value);

            if (pairs.Length == 0)
                return null;

            return pairs[0];
        */
        private static readonly IValueConverter StyleConverter = Or(new CounterValueConverter(1), Initial);

        #endregion

        #region ctor

        internal CssCounterIncrementProperty()
            : base(PropertyNames.CounterIncrement)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion
    }
}
