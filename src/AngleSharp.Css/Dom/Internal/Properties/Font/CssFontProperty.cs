namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font
    /// </summary>
    sealed class CssFontProperty : CssShorthandProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(new FontConverter(), SystemFontConverter);

        #endregion

        #region ctor

        internal CssFontProperty()
            : base(PropertyNames.Font, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Instead of specifying individual longhand properties, a keyword
        /// can be used to represent a specific system font.
        /// </summary>
        /// <param name="font">The font to select.</param>
        static void SetSystemFont(SystemFont font)
        {
            switch (font)
            {
                case SystemFont.Caption:
                case SystemFont.Icon:
                case SystemFont.MessageBox:
                    //SetFont("Arial", "16px");
                    break;
                case SystemFont.StatusBar:
                case SystemFont.Menu:
                    //SetFont("Segoe UI", "12px");
                    break;
                case SystemFont.SmallCaption:
                    //SetFont("Segoe UI", "15px");
                    break;
            }
        }

        #endregion
    }
}
