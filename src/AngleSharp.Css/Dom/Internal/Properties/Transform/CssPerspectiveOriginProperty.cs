namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/perspective-origin
    /// Gets the position of the abscissa of the vanishing point.
    /// Gets the position of the ordinate of the vanishing point.
    /// </summary>
    sealed class CssPerspectiveOriginProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter PerspectiveConverter = Or(PointConverter, AssignInitial(Point.Center));

        #endregion

        #region ctor

        internal CssPerspectiveOriginProperty()
            : base(PropertyNames.PerspectiveOrigin, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return PerspectiveConverter; }
        }

        #endregion
    }
}
