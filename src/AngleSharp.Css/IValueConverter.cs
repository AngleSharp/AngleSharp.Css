namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;

    public interface IValueConverter
    {
        ICssValue Convert(StringSource source);
    }
}
