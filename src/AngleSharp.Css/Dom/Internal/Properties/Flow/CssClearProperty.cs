namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/clear
    /// Gets the value of the clear mode.
    /// </summary>
    sealed class CssClearProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(ClearModeConverter, AssignInitial(ClearMode.None));

        #endregion

        #region ctor

        internal CssClearProperty()
            : base(PropertyNames.Clear)
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
