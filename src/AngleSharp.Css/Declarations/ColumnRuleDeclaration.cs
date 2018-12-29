namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static ValueConverters;

    static class ColumnRuleDeclaration
    {
        public static String Name = PropertyNames.ColumnRule;

        public static IValueConverter Converter = new ColumnRuleAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.ColumnRuleColor,
            PropertyNames.ColumnRuleStyle,
            PropertyNames.ColumnRuleWidth,
        };

        sealed class ColumnRuleValueConverter : IValueConverter
        {
            public ICssValue Convert(StringSource source)
            {
                var color = default(ICssValue);
                var width = default(ICssValue);
                var style = default(ICssValue);
                var pos = 0;

                do
                {
                    pos = source.Index;

                    if (color == null)
                    {
                        color = ColorParser.ParseColor(source);
                        source.SkipSpacesAndComments();
                    }

                    if (width == null)
                    {
                        width = source.ParseLineWidth();
                        source.SkipSpacesAndComments();
                    }

                    if (style == null)
                    {
                        style = source.ParseConstant(Map.LineStyles);
                        source.SkipSpacesAndComments();
                    }
                }
                while (pos != source.Index);

                return new CssTupleValue(new[] { color, width, style });
            }
        }

        sealed class ColumnRuleAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(new ColumnRuleValueConverter(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Collect(IEnumerable<ICssProperty> properties)
            {
                var color = properties.Where(m => m.Name == ColumnRuleColorDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var width = properties.Where(m => m.Name == ColumnRuleWidthDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var style = properties.Where(m => m.Name == ColumnRuleStyleDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

                if (color != null || width != null || style != null)
                {
                    return new CssTupleValue(new[] { color, width, style });
                }

                return null;
            }

            public IEnumerable<ICssProperty> Distribute(ICssValue value)
            {
                var options = value as CssTupleValue;

                if (options != null)
                {
                    return new[]
                    {
                        new CssProperty(ColumnRuleColorDeclaration.Name, ColumnRuleColorDeclaration.Converter, ColumnRuleColorDeclaration.Flags, options.Items[0]),
                        new CssProperty(ColumnRuleWidthDeclaration.Name, ColumnRuleWidthDeclaration.Converter, ColumnRuleWidthDeclaration.Flags, options.Items[1]),
                        new CssProperty(ColumnRuleStyleDeclaration.Name, ColumnRuleStyleDeclaration.Converter, ColumnRuleStyleDeclaration.Flags, options.Items[2]),
                    };
                }

                return null;
            }
        }
    }
}
