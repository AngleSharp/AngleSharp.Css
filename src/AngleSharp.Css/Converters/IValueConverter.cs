namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;

    interface IValueConverter
    {
        ICssValue Convert(StringSource source);
    }
}
