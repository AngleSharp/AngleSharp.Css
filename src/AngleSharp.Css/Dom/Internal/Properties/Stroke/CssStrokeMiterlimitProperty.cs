namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/SVG/Attribute/stroke-miterlimit
    /// Gets the value that should be used for the stroke-miterlimit.
    /// </summary>
    sealed class CssStrokeMiterlimitProperty : CssProperty
	{
		#region Fields

		private static readonly IValueConverter StyleConverter = StrokeMiterlimitConverter;

		#endregion

		#region ctor

		public CssStrokeMiterlimitProperty()
			: base(PropertyNames.StrokeMiterlimit, PropertyFlags.Animatable)
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
