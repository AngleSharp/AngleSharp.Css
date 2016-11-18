namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-right-color
    /// </summary>
    sealed class CssBorderRightColorProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(CurrentColorConverter, AssignInitial(Color.Transparent));

        #endregion

        #region ctor

        internal CssBorderRightColorProperty()
            : base(PropertyNames.BorderRightColor)
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
