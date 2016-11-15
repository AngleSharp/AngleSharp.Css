namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/SVG/Attribute/stroke-linejoin
    /// Gets the value that should be used for the stroke-linejoin.
    /// </summary>
    sealed class CssStrokeLinejoinProperty : CssProperty
	{
		#region Fields

		private static readonly IValueConverter StyleConverter = StrokeLinejoinConverter;

		#endregion

		#region ctor

		public CssStrokeLinejoinProperty()
			: base(PropertyNames.StrokeLinejoin, PropertyFlags.Animatable)
		{
		}

		#endregion

		#region Properties

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
