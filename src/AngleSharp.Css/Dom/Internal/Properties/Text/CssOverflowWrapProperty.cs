﻿namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Converters;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/overflow-wrap
    /// Used to specify whether or not the browser may break lines within words 
    /// in order to prevent overflow when an otherwise unbreakable string 
    /// is too long to fit in its containing box.
    /// </summary>
    sealed class CssOverflowWrapProperty : CssProperty
	{
		#region Fields

		private static readonly IValueConverter StyleConverter = OverflowWrapConverter;

		#endregion

		public CssOverflowWrapProperty()
			: base(PropertyNames.OverflowWrap)
		{
		}

		internal override IValueConverter Converter
		{
			get
			{
				return StyleConverter;
			}
		}
	}
}
