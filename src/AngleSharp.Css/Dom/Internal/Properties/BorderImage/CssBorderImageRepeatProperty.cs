namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-repeat
    /// </summary>
    sealed class CssBorderImageRepeatProperty : CssProperty
    {
        #region Fields
        
        private static readonly IValueConverter StyleConverter = Or(BorderImageRepeatConverter, AssignInitial(BorderRepeat.Stretch));

        #endregion

        #region ctor

        internal CssBorderImageRepeatProperty()
            : base(PropertyNames.BorderImageRepeat)
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
