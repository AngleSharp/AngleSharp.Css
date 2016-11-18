namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/transform-style
    /// Gets if the children of the element are lying in the plane of the
    /// element itself. Otherwise the children of the element should be
    /// positioned in the 3D-space.
    /// </summary>
    sealed class CssTransformStyleProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(Toggle(CssKeywords.Flat, CssKeywords.Preserve3d), AssignInitial(true));

        #endregion

        #region ctor

        internal CssTransformStyleProperty()
            : base(PropertyNames.TransformStyle)
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
