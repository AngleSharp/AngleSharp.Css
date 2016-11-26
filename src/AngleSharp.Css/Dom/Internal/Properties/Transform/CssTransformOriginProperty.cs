namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/transform-origin
    /// Gets how far from the left edge of the box the origin of the
    /// transform is set.
    /// Gets how far from the top edge of the box the origin of the
    /// transform is set.
    /// Gets how far from the user eye the z = 0 origin is set.
    /// </summary>
    sealed class CssTransformOriginProperty : CssProperty
    {
        #region Fields

        private static IValueConverter StyleConverter = Or(new OriginConverter(), AssignInitial(Point.Center));

        #endregion

        #region ctor

        internal CssTransformOriginProperty()
            : base(PropertyNames.TransformOrigin, PropertyFlags.Animatable)
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
