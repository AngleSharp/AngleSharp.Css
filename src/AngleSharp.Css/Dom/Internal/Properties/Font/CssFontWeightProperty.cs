namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-weight
    /// </summary>
    sealed class CssFontWeightProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = FontWeightConverter.Or(
            WeightIntegerConverter).OrDefault(FontWeight.Normal);

        #endregion

        #region ctor
        
        internal CssFontWeightProperty()
            : base(PropertyNames.FontWeight, PropertyFlags.Inherited | PropertyFlags.Animatable)
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
