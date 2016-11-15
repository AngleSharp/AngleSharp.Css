namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/word-beak
    /// Specify whether to break lines within words.
    /// </summary>
    sealed class CssWordBreakProperty : CssProperty
	{
		#region Fields

		private static readonly IValueConverter StyleConverter = WordBreakConverter;

		#endregion

		#region ctor

		public CssWordBreakProperty() 
			: base(PropertyNames.WordBreak)
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
