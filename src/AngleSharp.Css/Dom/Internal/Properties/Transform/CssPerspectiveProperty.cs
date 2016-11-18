﻿namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using static ValueConverters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/perspective
    /// Gets the distance from the user to the z=0 plane. It is used to
    /// apply a perspective transform to the element and its content. If it
    /// 0 or a negative value, no perspective transform is applied.
    /// </summary>
    sealed class CssPerspectiveProperty : CssProperty
    {
        #region Fields

        private static readonly IValueConverter StyleConverter = Or(LengthConverter, None, AssignInitial(Length.Zero));

        #endregion

        #region ctor

        internal CssPerspectiveProperty()
            : base(PropertyNames.Perspective, PropertyFlags.Animatable)
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
