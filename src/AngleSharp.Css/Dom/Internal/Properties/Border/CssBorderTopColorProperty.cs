namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top-color
    /// </summary>
    sealed class CssBorderTopColorProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(CurrentColorConverter, AssignInitial(Color.Transparent));

        #endregion

        #region ctor

        internal CssBorderTopColorProperty()
            : base(PropertyNames.BorderTopColor)
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
