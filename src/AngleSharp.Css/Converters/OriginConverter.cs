namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;

    sealed class OriginConverter : IValueConverter
    {
        public ICssValue Convert(StringSource source)
        {
            var pt = source.ParsePoint();
            source.SkipSpacesAndComments();
            var z = source.ParseLength();

            if (pt.HasValue)
            {
                return new Point3(pt.Value.X, pt.Value.Y, z ?? Length.Zero);
            }

            return null;
        }
    }
}
