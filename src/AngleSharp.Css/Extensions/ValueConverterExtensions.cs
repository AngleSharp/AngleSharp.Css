#nullable enable
namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using static ValueConverters;

    /// <summary>
    /// Essential extensions for using the value converters.
    /// </summary>
    static class ValueConverterExtensions
    {
        /// <summary>
        /// Converts the value of the given media feature, reusing the conversion of an earlier
        /// evaluation where possible.
        /// </summary>
        internal static ICssValue? Convert(this IValueConverter converter, IMediaFeature feature) =>
            feature is MediaFeature known ? known.ConvertValue(converter) : converter.Convert(feature.Value);

        public static ICssValue? Convert(this IValueConverter converter, String value)
        {
            var source = new StringSource(value);
            source.SkipSpacesAndComments();
            var varRefs = source.ParseVars();

            if (varRefs == null)
            {
                var result = converter.Convert(source);
                source.SkipSpacesAndComments();
                return source.IsDone ? result : null;
            }

            return varRefs;
        }

        public static IValueConverter Many(this IValueConverter converter, Int32 min = 1, Int32 max = UInt16.MaxValue, String? separator = null) =>
            new OneOrMoreValueConverter(converter, min, max, separator);

        public static IValueConverter FromList(this IValueConverter converter) =>
            new ListValueConverter(converter);

        public static IValueConverter ToConverter<T>(this Dictionary<String, T> values) =>
            new DictionaryValueConverter<T>(values);

        public static IValueConverter BoxRelative(this IValueConverter converter) =>
            new BoxRelativeValueConverter(converter);

        public static IValueConverter FlowRelative(this IValueConverter converter) =>
            new FlowRelativeValueConverter(converter);

        public static IValueConverter Periodic(this IValueConverter converter) =>
            new PeriodicValueConverter(converter);

        public static IValueConverter Radius(this IValueConverter converter) =>
            new RadiusValueConverter(converter);

        public static IValueConverter Exclusive(this IValueConverter converter)
        {
            return new ClassValueConverter<ICssValue>(source =>
            {
                var pos = source.Index;
                var result = converter.Convert(source);
                source.SkipSpacesAndComments();

                if (result != null && !source.IsDone)
                {
                    source.BackTo(pos);
                    return null!;
                }

                return result!;
            });
        }

        public static IValueConverter Option(this IValueConverter converter, ICssValue defaultValue) =>
            Or(new OptionValueConverter(converter, defaultValue), AssignInitial(defaultValue));

        public static String Join<T>(this IEnumerable<T> values, String separator)
            where T : ICssValue
        {
            // Serialized for every property read of a computed style, and nearly all lists hold a
            // single entry or two (e.g. one background layer, a tuple of two lengths), which need
            // no buffer at all.
            var first = default(String);
            var buffer = default(StringBuilder);

            foreach (var value in values)
            {
                var str = value?.CssText;

                if (String.IsNullOrEmpty(str))
                {
                    continue;
                }

                if (first is null)
                {
                    first = str;
                }
                else
                {
                    buffer ??= StringBuilderPool.Obtain().Append(first);
                    buffer.Append(separator).Append(str);
                }
            }

            return buffer?.ToPool() ?? first ?? String.Empty;
        }
    }
}
