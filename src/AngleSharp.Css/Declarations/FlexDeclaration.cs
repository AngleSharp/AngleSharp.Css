#nullable disable
namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class FlexDeclaration
    {
        public static String Name = PropertyNames.Flex;

        public static String[] Longhands = new[]
        {
            PropertyNames.FlexGrow,
            PropertyNames.FlexShrink,
            PropertyNames.FlexBasis,
        };

        public static IValueConverter Converter = new FlexAggregator();

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        sealed class FlexAggregator : IValueAggregator, IValueConverter
        {
            // Shared (boxed) instances, such that Merge can tell omitted components apart; computing
            // creates new instances, hence a computed shorthand is always serialized in full.
            private static readonly ICssValue OmittedFactor = new CssLengthValue(1, CssLengthValue.Unit.None);
            private static readonly ICssValue OmittedBasis = new CssLengthValue(0, CssLengthValue.Unit.Percent);

            private static readonly IValueConverter converter = Or(None, WithAny(
                Or(FlexGrowDeclaration.Converter, VarConverter),
                Or(FlexShrinkDeclaration.Converter, VarConverter),
                Or(FlexBasisDeclaration.Converter, VarConverter)));

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Merge(ICssValue[] values)
            {
                var grow = values[0];
                var shrink = values[1];
                var basis = values[2];

                if (grow != null || shrink != null || basis != null)
                {
                    // Components omitted in the shorthand are left out again, such that it serializes as
                    // written; missing longhands (e.g., a lone computed `flex-basis: 40px`) must still be
                    // serialized, otherwise `flex: 40px` would mean `1 1 40px`.
                    return new CssTupleValue(new[]
                    {
                        Restore(grow, InitialValues.FlexGrowDecl),
                        Restore(shrink, InitialValues.FlexShrinkDecl),
                        Restore(basis, InitialValues.FlexBasisDecl),
                    });
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                var options = value as CssTupleValue;

                if (options != null)
                {
                    // Omitted factors default to 1, an omitted basis to 0% (CSS Flexbox 1, 7.1.1)
                    return new[]
                    {
                        options.Items[0] ?? OmittedFactor,
                        options.Items[1] ?? OmittedFactor,
                        options.Items[2] ?? OmittedBasis,
                    };
                }
                else if (value != null && value.CssText.Isi(CssKeywords.None))
                {
                    // flex: none is equivalent to flex: 0 0 auto
                    return new[]
                    {
                        new CssLengthValue(0, CssLengthValue.Unit.None),
                        new CssLengthValue(0, CssLengthValue.Unit.None),
                        InitialValues.FlexBasisDecl,
                    };
                }

                return null;
            }

            private static ICssValue Restore(ICssValue value, ICssValue initialValue)
            {
                if (ReferenceEquals(value, OmittedFactor) || ReferenceEquals(value, OmittedBasis))
                {
                    return null;
                }
                else if (value is CssInitialValue initial)
                {
                    return initial.Value ?? initialValue;
                }

                return value ?? initialValue;
            }
        }
    }
}
