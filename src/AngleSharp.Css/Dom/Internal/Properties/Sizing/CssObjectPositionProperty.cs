namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using static ValueConverters;

    /// <summary>
    /// More information available:
    /// http://dev.w3.org/csswg/css-images-3/#the-object-position
    /// </summary>
    sealed class CssObjectPositionProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(PointConverter, AssignInitial(Point.Center));

        #endregion

        #region ctor

        internal CssObjectPositionProperty()
            : base(PropertyNames.ObjectPosition, PropertyFlags.Animatable)
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
