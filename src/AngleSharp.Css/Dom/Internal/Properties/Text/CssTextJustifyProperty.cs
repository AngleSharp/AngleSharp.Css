namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    sealed class CssTextJustifyProperty : CssProperty
	{
		#region Fields

		private static readonly IValueConverter StyleConverter = TextJustifyConverter;

		#endregion

		#region ctor

		public CssTextJustifyProperty()
			: base(PropertyNames.TextJustify)
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
