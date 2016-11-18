namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-position
    /// Gets the list of all given positions.
    /// </summary>
    sealed class CssBackgroundPositionProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter ListConverter = Or(PointConverter.FromList(), AssignInitial(Point.Center));

        #endregion

        #region ctor

        internal CssBackgroundPositionProperty()
            : base(PropertyNames.BackgroundPosition, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return ListConverter; }
        }

        #endregion
    }
}
