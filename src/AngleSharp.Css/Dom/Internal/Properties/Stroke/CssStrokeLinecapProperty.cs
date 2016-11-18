namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/SVG/Attribute/stroke-linecap
    /// Gets the value that should be used for the stroke-linecap.
    /// </summary>
    sealed class CssStrokeLinecapProperty : CssProperty
	{
		#region Fields

		private static readonly IValueConverter StyleConverter = Or(StrokeLinecapConverter, AssignInitial(StrokeLinecap.Butt));

		#endregion

		#region ctor

		public CssStrokeLinecapProperty()
			: base(PropertyNames.StrokeLinecap, PropertyFlags.Animatable)
		{
		}

		#endregion

		#region Properties;

		internal override IValueConverter Converter
		{
			get
			{
				return StyleConverter;
			}
		}

		#endregion
	}
}
