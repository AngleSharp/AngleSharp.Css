#nullable disable
namespace AngleSharp.Css
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Linq;

    /// <summary>
    /// A set of already constructed CSS value converters.
    /// </summary>
    static class ValueConverters
    {
        #region Misc

        /// <summary>
        /// Represents a CSS variable reference.
        /// </summary>
        public static readonly IValueConverter VarConverter = new ClassValueConverter<CssVarValue>(source =>
        {
            var ident = source.ParseIdent();

            if (ident.Isi(FunctionNames.Var) && source.Current == Symbols.RoundBracketOpen)
            {
                source.SkipCurrentAndSpaces();
                return source.ParseVar();
            }

            return null;
        });

        /// <summary>
        /// Creates an or converter for the given converters.
        /// </summary>
        public static IValueConverter Or(params IValueConverter[] converters) => new OrValueConverter(converters);

        public static IValueConverter SlashSeparated(IValueConverter converter) => new SeparatorConverter(converter, Symbols.Solidus);

        public static IValueConverter SpaceSeparated(IValueConverter converter) => new SeparatorConverter(converter, Symbols.Space);

        /// <summary>
        /// Creates a converter for the initial keyword with the given value.
        /// </summary>
        public static IValueConverter AssignInitial(ICssValue value) => new StandardValueConverter(value);

        /// <summary>
        /// Creates a converter for values containing (potentially multiple, at least one) var references.
        /// </summary>
        public static IValueConverter AssignReferences() => FromParser(FunctionParser.ParseVars);

        /// <summary>
        /// Creates a new converter by assigning the given identifier to a fixed result.
        /// </summary>
        public static IValueConverter Assign<T>(String identifier, T result) => new IdentifierValueConverter<T>(identifier, result);

        /// <summary>
        /// Creates a new boolean converter that toggles between the two given keywords.
        /// </summary>
        public static IValueConverter Toggle(String on, String off) => Or(Assign(on, true), Assign(off, false));

        #endregion

        #region Elementary

        /// <summary>
        /// Represents a converter for anything. Just copies the tokens.
        /// </summary>
        public static IValueConverter Any = new AnyValueConverter();

        /// <summary>
        /// Represents a converter for the none keyword with no value.
        /// </summary>
        public static IValueConverter None = new IdentifierValueConverter<Object>(CssKeywords.None, null);

        /// <summary>
        /// Represents a converter for the auto keyword with no value.
        /// </summary>
        public static IValueConverter Auto = new IdentifierValueConverter<CssLengthValue>(CssKeywords.Auto, CssLengthValue.Auto);

        /// <summary>
        /// Represents a converter for the content keyword with no value.
        /// </summary>
        public static IValueConverter Content = new IdentifierValueConverter<CssLengthValue>(CssKeywords.Content, CssLengthValue.Content);

        /// <summary>
        /// Represents a length object with line-width additions.
        /// http://dev.w3.org/csswg/css-backgrounds/#line-width
        /// </summary>
        public static readonly IValueConverter LineWidthConverter = FromParser(UnitParser.ParseLineWidth);

        /// <summary>
        /// Represents a calculated number.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/calc
        /// </summary>
        public static readonly IValueConverter CalcConverter = FromParser(CalcParser.ParseCalc);

        /// <summary>
        /// Represents a length object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/length
        /// </summary>
        public static readonly IValueConverter OnlyLengthConverter = new StructValueConverter<CssLengthValue>(UnitParser.ParseLength);

        /// <summary>
        /// Represents a resolution object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/resolution
        /// </summary>
        public static readonly IValueConverter OnlyResolutionConverter = new StructValueConverter<CssResolutionValue>(UnitParser.ParseResolution);

        /// <summary>
        /// Represents a time object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/time
        /// </summary>
        public static readonly IValueConverter OnlyTimeConverter = new StructValueConverter<CssTimeValue>(UnitParser.ParseTime);

        /// <summary>
        /// Represents a distance object (either Length or Percent).
        /// </summary>
        public static readonly IValueConverter OnlyLengthOrPercentConverter = new StructValueConverter<CssLengthValue>(UnitParser.ParseDistance);

        /// <summary>
        /// Represents a string object.
        /// </summary>
        public static readonly IValueConverter StringConverter = new StructValueConverter<CssStringValue>(FromString(StringParser.ParseString));

        /// <summary>
        /// Represents an URL object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/uri
        /// </summary>
        public static readonly IValueConverter UrlConverter = FromParser(CssUriParser.ParseUri);

        /// <summary>
        /// Represents many string objects, but always divisible by 2 (open-close quotes).
        /// </summary>
        public static readonly IValueConverter QuotesConverter = FromParser(CompoundParser.ParseQuotes);

        /// <summary>
        /// Represents an identifier object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/user-ident
        /// </summary>
        public static readonly IValueConverter IdentifierConverter = new IdentifierValueConverter(IdentParser.ParseNormalizedIdent);

        /// <summary>
        /// Represents an identifier object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/custom-ident
        /// </summary>
        public static readonly IValueConverter CustomIdentConverter = new IdentifierValueConverter(IdentParser.ParseCustomIdent);

        /// <summary>
        /// Represents an identifier object that matches the production rules of a single transition property.
        /// http://dev.w3.org/csswg/css-transitions/#single-transition-property
        /// </summary>
        public static readonly IValueConverter AnimatableConverter = new IdentifierValueConverter(IdentParser.ParseAnimatableIdent);

        /// <summary>
        /// Represents an integer object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/integer
        /// </summary>
        public static readonly IValueConverter OnlyIntegerConverter = new StructValueConverter<CssIntegerValue>(NumberParser.ParseInteger);

        /// <summary>
        /// Represents an integer object that is zero or greater.
        /// </summary>
        public static readonly IValueConverter NaturalIntegerConverter = new StructValueConverter<CssIntegerValue>(NumberParser.ParseNaturalInteger);

        /// <summary>
        /// Represents an integer object that only allows values \in { 100, 200, ..., 900 }.
        /// </summary>
        public static readonly IValueConverter WeightIntegerConverter = new StructValueConverter<CssIntegerValue>(NumberParser.ParseWeightInteger);

        /// <summary>
        /// Represents an integer object that is greater tha zero.
        /// </summary>
        public static readonly IValueConverter PositiveIntegerConverter = new StructValueConverter<CssIntegerValue>(NumberParser.ParsePositiveInteger);

        /// <summary>
        /// Represents an integer object with 0 or 1.
        /// </summary>
        public static readonly IValueConverter BinaryConverter = new StructValueConverter<CssIntegerValue>(NumberParser.ParseBinary);

        /// <summary>
        /// Represents a number object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/number
        /// </summary>
        public static readonly IValueConverter OnlyNumberConverter = new StructValueConverter<CssNumberValue>(NumberParser.ParseNumber);

        /// <summary>
        /// Represents a (calculated) number object.
        /// </summary>
        public static readonly IValueConverter NumberConverter = Or(OnlyNumberConverter, CalcConverter);

        /// <summary>
        /// Represents a (calculated) length object.
        /// </summary>
        public static readonly IValueConverter LengthConverter = Or(OnlyLengthConverter, CalcConverter);

        /// <summary>
        /// Represents a (calculated) resolution object.
        /// </summary>
        public static readonly IValueConverter ResolutionConverter = Or(OnlyResolutionConverter, CalcConverter);

        /// <summary>
        /// Represents a (calculated) time object.
        /// </summary>
        public static readonly IValueConverter TimeConverter = Or(OnlyTimeConverter, CalcConverter);

        /// <summary>
        /// Represents an (calculated) integer object.
        /// </summary>
        public static readonly IValueConverter IntegerConverter = Or(OnlyIntegerConverter, CalcConverter);

        /// <summary>
        /// Represents a (calculated) distance object (either Length or Percent).
        /// </summary>
        public static readonly IValueConverter LengthOrPercentConverter = Or(OnlyLengthOrPercentConverter, CalcConverter);

        /// <summary>
        /// Represents an number object that is zero or greater.
        /// </summary>
        public static readonly IValueConverter NaturalNumberConverter = new StructValueConverter<CssNumberValue>(NumberParser.ParseNaturalNumber);

        /// <summary>
        /// Represents an color object (usually hex or name).
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/color
        /// </summary>
        public static readonly IValueConverter ColorConverter = new StructValueConverter<CssColorValue>(ColorParser.ParseColor);

        /// <summary>
        /// Represents a converter for the accent-color property.
        /// </summary>
        public static readonly IValueConverter AccentColorConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            ColorConverter);

        /// <summary>
        /// Represents a converter for the caret-color property.
        /// </summary>
        public static readonly IValueConverter CaretColorConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            ColorConverter);

        /// <summary>
        /// Represents a converter for the color-scheme property.
        /// </summary>
        public static readonly IValueConverter ColorSchemeConverter = Or(
            Assign(CssKeywords.Normal, CssKeywords.Normal),
            WithAny(
                Or(
                    Assign(CssKeywords.Light, CssKeywords.Light),
                    Assign(CssKeywords.Dark, CssKeywords.Dark)
                )
            ).Many());

        /// <summary>
        /// Represents a converter for the forced-color-adjust property.
        /// </summary>
        public static readonly IValueConverter ForcedColorAdjustConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            Assign(CssKeywords.None, CssKeywords.None));

        /// <summary>
        /// Represents a converter for the print-color-adjust property.
        /// </summary>
        public static readonly IValueConverter PrintColorAdjustConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            Assign(CssKeywords.Economy, CssKeywords.Economy),
            Assign(CssKeywords.Exact, CssKeywords.Exact));

        /// <summary>
        /// Represents a converter for the backdrop-filter property.
        /// </summary>
        public static readonly IValueConverter BackdropFilterConverter = Assign(CssKeywords.None, CssKeywords.None);

        /// <summary>
        /// Represents a converter for blend mode values (mix-blend-mode, background-blend-mode).
        /// </summary>
        public static readonly IValueConverter BlendModeConverter = Or(
            Assign(CssKeywords.Normal, CssKeywords.Normal),
            Assign(CssKeywords.Multiply, CssKeywords.Multiply),
            Assign(CssKeywords.Screen, CssKeywords.Screen),
            Assign(CssKeywords.Overlay, CssKeywords.Overlay),
            Assign(CssKeywords.Darken, CssKeywords.Darken),
            Assign(CssKeywords.Lighten, CssKeywords.Lighten),
            Assign(CssKeywords.ColorDodge, CssKeywords.ColorDodge),
            Assign(CssKeywords.ColorBurn, CssKeywords.ColorBurn),
            Assign(CssKeywords.HardLight, CssKeywords.HardLight),
            Assign(CssKeywords.SoftLight, CssKeywords.SoftLight),
            Assign(CssKeywords.Difference, CssKeywords.Difference),
            Assign(CssKeywords.Exclusion, CssKeywords.Exclusion),
            Assign(CssKeywords.Hue, CssKeywords.Hue),
            Assign(CssKeywords.Saturation, CssKeywords.Saturation),
            Assign(CssKeywords.Color, CssKeywords.Color),
            Assign(CssKeywords.Luminosity, CssKeywords.Luminosity),
            Assign(CssKeywords.Add, CssKeywords.Add));

        /// <summary>
        /// Represents a converter for the mix-blend-mode property.
        /// </summary>
        public static readonly IValueConverter MixBlendModeConverter = BlendModeConverter;

        /// <summary>
        /// Represents a converter for the background-blend-mode property.
        /// </summary>
        public static readonly IValueConverter BackgroundBlendModeConverter = BlendModeConverter.FromList();

        /// <summary>
        /// Represents a converter for the isolation property.
        /// </summary>
        public static readonly IValueConverter IsolationConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            Assign(CssKeywords.Isolate, CssKeywords.Isolate));

        /// <summary>
        /// Represents a converter for the shape-outside property.
        /// </summary>
        public static readonly IValueConverter ShapeOutsideConverter = Assign(CssKeywords.None, CssKeywords.None);

        /// <summary>
        /// Represents a converter for the shape-margin property.
        /// </summary>
        public static readonly IValueConverter ShapeMarginConverter = LengthOrPercentConverter;

        /// <summary>
        /// Represents a converter for the shape-image-threshold property.
        /// </summary>
        public static readonly IValueConverter ShapeImageThresholdConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            NumberConverter);

        /// <summary>
        /// Represents a converter for the shape-rendering property.
        /// </summary>
        public static readonly IValueConverter ShapeRenderingConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            Assign(CssKeywords.OptimizeSpeed, CssKeywords.OptimizeSpeed),
            Assign(CssKeywords.CrispEdges, CssKeywords.CrispEdges),
            Assign(CssKeywords.GeometricPrecision, CssKeywords.GeometricPrecision));

        /// <summary>
        /// Represents a converter for the counter-set property.
        /// </summary>
        public static readonly IValueConverter CounterSetConverter = new CounterValueConverter(CssIntegerValue.Zero);

        /// <summary>
        /// Represents a converter for the image-rendering property.
        /// </summary>
        public static readonly IValueConverter ImageRenderingConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            Assign(CssKeywords.CrispEdges, CssKeywords.CrispEdges),
            Assign(CssKeywords.OptimizeQuality, CssKeywords.OptimizeQuality),
            Assign(CssKeywords.OptimizeSpeed, CssKeywords.OptimizeSpeed));

        /// <summary>
        /// Represents a converter for the image-orientation property.
        /// </summary>
        public static readonly IValueConverter ImageOrientationConverter = Or(
            Assign(CssKeywords.FromImage, CssKeywords.FromImage),
            FromParser(UnitParser.ParseAngle));

        /// <summary>
        /// Represents a converter for the view-transition-name property.
        /// </summary>
        public static readonly IValueConverter ViewTransitionNameConverter = Or(None, IdentifierConverter.FromList());

        /// <summary>
        /// Represents a converter for the view-transition-class property.
        /// </summary>
        public static readonly IValueConverter ViewTransitionClassConverter = Or(None, IdentifierConverter.FromList());

        /// <summary>
        /// Represents a converter for the anchor-name property.
        /// </summary>
        public static readonly IValueConverter AnchorNameConverter = Or(None, IdentifierConverter.FromList());

        /// <summary>
        /// Represents a converter for the anchor-scope property.
        /// </summary>
        public static readonly IValueConverter AnchorScopeConverter = Or(
            Assign(CssKeywords.All, CssKeywords.All),
            Assign(CssKeywords.Own, CssKeywords.Own),
            IdentifierConverter.FromList());

        /// <summary>
        /// Represents a converter for the position-anchor property.
        /// </summary>
        public static readonly IValueConverter PositionAnchorConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            IdentifierConverter);

        /// <summary>
        /// Represents a converter for the position-area property.
        /// </summary>
        public static readonly IValueConverter PositionAreaConverter = Or(
            Assign(CssKeywords.None, CssKeywords.None),
            Assign(CssKeywords.Top, CssKeywords.Top),
            Assign(CssKeywords.Bottom, CssKeywords.Bottom),
            Assign(CssKeywords.Left, CssKeywords.Left),
            Assign(CssKeywords.Right, CssKeywords.Right),
            Assign(CssKeywords.Center, CssKeywords.Center),
            Assign(CssKeywords.Start, CssKeywords.Start),
            Assign(CssKeywords.End, CssKeywords.End),
            Assign(CssKeywords.SelfStart, CssKeywords.SelfStart),
            Assign(CssKeywords.SelfEnd, CssKeywords.SelfEnd),
            Assign(CssKeywords.SpanAll, CssKeywords.SpanAll));

        /// <summary>
        /// Represents a converter for the position-try-fallbacks property.
        /// </summary>
        public static readonly IValueConverter PositionTryFallbacksConverter = None;

        /// <summary>
        /// Represents a converter for the position-try-order property.
        /// </summary>
        public static readonly IValueConverter PositionTryOrderConverter = Or(
            Assign(CssKeywords.Normal, CssKeywords.Normal),
            Assign(CssKeywords.FlipBlock, CssKeywords.FlipBlock),
            Assign(CssKeywords.FlipInline, CssKeywords.FlipInline),
            Assign(CssKeywords.FlipStart, CssKeywords.FlipStart),
            Assign(CssKeywords.FlipEnd, CssKeywords.FlipEnd));

        /// <summary>
        /// Represents a converter for the position-visibility property.
        /// </summary>
        public static readonly IValueConverter PositionVisibilityConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            Assign(CssKeywords.Always, CssKeywords.Always),
            Assign(CssKeywords.PreferHidden, CssKeywords.PreferHidden),
            Assign(CssKeywords.PreferNoOverflow, CssKeywords.PreferNoOverflow));

        /// <summary>
        /// Represents a converter for the text-underline-offset property.
        /// </summary>
        public static readonly IValueConverter TextUnderlineOffsetConverter = LengthOrPercentConverter;

        /// <summary>
        /// Represents a converter for the text-decoration-thickness property.
        /// </summary>
        public static readonly IValueConverter TextDecorationThicknessConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            Assign(CssKeywords.FromFont, CssKeywords.FromFont),
            LengthOrPercentConverter);

        /// <summary>
        /// Represents a converter for the text-decoration-skip-ink property.
        /// </summary>
        public static readonly IValueConverter TextDecorationSkipInkConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            Assign(CssKeywords.All, CssKeywords.All),
            Assign(CssKeywords.None, CssKeywords.None));

        /// <summary>
        /// Represents a converter for the text-wrap property.
        /// </summary>
        public static readonly IValueConverter TextWrapConverter = Or(
            Assign(CssKeywords.Wrap, CssKeywords.Wrap),
            Assign(CssKeywords.Nowrap, CssKeywords.Nowrap),
            Assign(CssKeywords.Balance, CssKeywords.Balance),
            Assign(CssKeywords.Stable, CssKeywords.Stable),
            Assign(CssKeywords.Pretty, CssKeywords.Pretty));

        /// <summary>
        /// Represents a converter for the text-wrap-mode property.
        /// </summary>
        public static readonly IValueConverter TextWrapModeConverter = Or(
            Assign(CssKeywords.Wrap, CssKeywords.Wrap),
            Assign(CssKeywords.Nowrap, CssKeywords.Nowrap));

        /// <summary>
        /// Represents a converter for the text-wrap-style property.
        /// </summary>
        public static readonly IValueConverter TextWrapStyleConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            Assign(CssKeywords.Stable, CssKeywords.Stable),
            Assign(CssKeywords.Balance, CssKeywords.Balance),
            Assign(CssKeywords.Pretty, CssKeywords.Pretty));

        /// <summary>
        /// Represents a converter for the white-space-collapse property.
        /// </summary>
        public static readonly IValueConverter WhiteSpaceCollapseConverter = Or(
            Assign(CssKeywords.Collapse, CssKeywords.Collapse),
            Assign(CssKeywords.Preserve, CssKeywords.Preserve),
            Assign(CssKeywords.PreserveBreaks, CssKeywords.PreserveBreaks),
            Assign(CssKeywords.PreserveSpaces, CssKeywords.PreserveSpaces));

        /// <summary>
        /// Represents a converter for the tab-size property.
        /// </summary>
        public static readonly IValueConverter TabSizeConverter = Or(NumberConverter, LengthConverter);

        /// <summary>
        /// Represents a converter for the hyphens property.
        /// </summary>
        public static readonly IValueConverter HyphensConverter = Or(
            Assign(CssKeywords.None, CssKeywords.None),
            Assign(CssKeywords.Manual, CssKeywords.Manual),
            Assign(CssKeywords.Auto, CssKeywords.Auto));

        /// <summary>
        /// Represents a converter for the hyphenate-character property.
        /// </summary>
        public static readonly IValueConverter HyphenateCharacterConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            StringConverter);

        /// <summary>
        /// Represents a converter for the hyphenate-limit-chars property.
        /// </summary>
        public static readonly IValueConverter HypenatateLimitCharsConverter = Assign(CssKeywords.Auto, CssKeywords.Auto);

        /// <summary>
        /// Represents a converter for the line-break property.
        /// </summary>
        public static readonly IValueConverter LineBreakConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            Assign(CssKeywords.Loose, CssKeywords.Loose),
            Assign(CssKeywords.Strict, CssKeywords.Strict),
            Assign(CssKeywords.Anywhere, CssKeywords.Anywhere));

        /// <summary>
        /// Represents a converter for the initial-letter property.
        /// </summary>
        public static readonly IValueConverter InitialLetterConverter = Or(
            Assign(CssKeywords.Normal, CssKeywords.Normal),
            NumberConverter);

        /// <summary>
        /// Represents a converter for the initial-letter-align property.
        /// </summary>
        public static readonly IValueConverter InitialLetterAlignConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            Assign(CssKeywords.Alphabetic, CssKeywords.Alphabetic),
            Assign(CssKeywords.Hanging, CssKeywords.Hanging),
            Assign(CssKeywords.Leading, CssKeywords.Leading));

        /// <summary>
        /// Represents a converter for the hanging-punctuation property.
        /// </summary>
        public static readonly IValueConverter HangingPunctuationConverter = Assign(CssKeywords.None, CssKeywords.None);

        /// <summary>
        /// Represents a converter for the mask-image property.
        /// </summary>
        public static readonly IValueConverter MaskImageConverter = MultipleImageSourceConverter;

        /// <summary>
        /// Represents a converter for the mask-mode property.
        /// </summary>
        public static readonly IValueConverter MaskModeConverter = Or(
            Assign(CssKeywords.AlphaKeyword, CssKeywords.AlphaKeyword),
            Assign(CssKeywords.Luminance, CssKeywords.Luminance));

        /// <summary>
        /// Represents a converter for the mask-repeat property.
        /// </summary>
        public static readonly IValueConverter MaskRepeatConverter = BackgroundRepeatsConverter;

        /// <summary>
        /// Represents a converter for the mask-position property.
        /// </summary>
        public static readonly IValueConverter MaskPositionConverter = PointConverter;

        /// <summary>
        /// Represents a converter for the mask-clip property.
        /// </summary>
        public static readonly IValueConverter MaskClipConverter = Or(
            Assign(CssKeywords.BorderBox, CssKeywords.BorderBox),
            Assign(CssKeywords.PaddingBox, CssKeywords.PaddingBox),
            Assign(CssKeywords.ContentBox, CssKeywords.ContentBox),
            Assign(CssKeywords.FillBox, CssKeywords.FillBox),
            Assign(CssKeywords.StrokeBox, CssKeywords.StrokeBox),
            Assign(CssKeywords.ViewBox, CssKeywords.ViewBox));

        /// <summary>
        /// Represents a converter for the mask-origin property.
        /// </summary>
        public static readonly IValueConverter MaskOriginConverter = Or(
            Assign(CssKeywords.BorderBox, CssKeywords.BorderBox),
            Assign(CssKeywords.PaddingBox, CssKeywords.PaddingBox),
            Assign(CssKeywords.ContentBox, CssKeywords.ContentBox),
            Assign(CssKeywords.FillBox, CssKeywords.FillBox),
            Assign(CssKeywords.StrokeBox, CssKeywords.StrokeBox),
            Assign(CssKeywords.ViewBox, CssKeywords.ViewBox));

        /// <summary>
        /// Represents a converter for the mask-size property.
        /// </summary>
        public static readonly IValueConverter MaskSizeConverter = BackgroundSizeConverter;

        /// <summary>
        /// Represents a converter for the mask-composite property.
        /// </summary>
        public static readonly IValueConverter MaskCompositeConverter = Or(
            Assign(CssKeywords.Add, CssKeywords.Add),
            Assign(CssKeywords.Subtract, CssKeywords.Subtract),
            Assign(CssKeywords.Intersect, CssKeywords.Intersect),
            Assign(CssKeywords.Exclude, CssKeywords.Exclude),
            Assign(CssKeywords.Multiply, CssKeywords.Multiply),
            Assign(CssKeywords.Screen, CssKeywords.Screen),
            Assign(CssKeywords.Overlay, CssKeywords.Overlay),
            Assign(CssKeywords.Darken, CssKeywords.Darken),
            Assign(CssKeywords.Lighten, CssKeywords.Lighten));

        /// <summary>
        /// Represents a converter for the mask-type property.
        /// </summary>
        public static readonly IValueConverter MaskTypeConverter = Or(
            Assign(CssKeywords.Luminance, CssKeywords.Luminance),
            Assign(CssKeywords.AlphaKeyword, CssKeywords.AlphaKeyword));

        /// <summary>
        /// Represents a converter for the mask-border property.
        /// </summary>
        public static readonly IValueConverter MaskBorderConverter = Assign(CssKeywords.None, CssKeywords.None);

        /// <summary>
        /// Represents a converter for the mask-border-source property.
        /// </summary>
        public static readonly IValueConverter MaskBorderSourceConverter = Assign(CssKeywords.None, CssKeywords.None);

        /// <summary>
        /// Represents a converter for the mask-border-slice property.
        /// </summary>
        public static readonly IValueConverter MaskBorderSliceConverter = NumberConverter;

        /// <summary>
        /// Represents a converter for the mask-border-width property.
        /// </summary>
        public static readonly IValueConverter MaskBorderWidthConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            LengthOrPercentConverter);

        /// <summary>
        /// Represents a converter for the mask-border-outset property.
        /// </summary>
        public static readonly IValueConverter MaskBorderOutsetConverter = LengthOrPercentConverter;

        /// <summary>
        /// Represents a converter for the mask-border-repeat property.
        /// </summary>
        public static readonly IValueConverter MaskBorderRepeatConverter = BackgroundRepeatsConverter;

        /// <summary>
        /// Represents a converter for the mask-border-mode property.
        /// </summary>
        public static readonly IValueConverter MaskBorderModeConverter = Or(
            Assign(CssKeywords.AlphaKeyword, CssKeywords.AlphaKeyword),
            Assign(CssKeywords.Luminance, CssKeywords.Luminance));

        /// <summary>
        /// Represents a position object.
        /// http://www.w3.org/TR/css3-background/#ltpositiongt
        /// </summary>
        public static readonly IValueConverter PointConverter = new StructValueConverter<CssPoint2D>(PointParser.ParsePoint);

        /// <summary>
        /// Represents an origin (Point3D) object.
        /// </summary>
        public static readonly IValueConverter OriginConverter = FromParser(PointParser.ParseOrigin);

        /// <summary>
        /// Represents a position object.
        /// http://www.w3.org/TR/css3-background/#ltpositiongt
        /// </summary>
        public static readonly IValueConverter PointXConverter = FromParser(PointParser.ParsePointX);

        /// <summary>
        /// Represents a position object.
        /// http://www.w3.org/TR/css3-background/#ltpositiongt
        /// </summary>
        public static readonly IValueConverter PointYConverter = FromParser(PointParser.ParsePointY);

        /// <summary>
        /// Represents an symbols object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/symbols
        /// </summary>
        public static readonly IValueConverter SymbolsConverter = FromParser(SymbolsParser.ParseSymbols);

        #endregion

        #region Functions

        /// <summary>
        /// Represents a shape object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/shape
        /// </summary>
        public static readonly IValueConverter ShapeConverter = FromParser(ShapeParser.ParseShape);

        /// <summary>
        /// Creates a converter for the content function.
        /// </summary>
        public static readonly IValueConverter ContentConverter = FromParser(FunctionParser.ParseContent);

        /// <summary>
        /// Creates a converter for the attr function.
        /// </summary>
        public static readonly IValueConverter AttrConverter = FromParser(FunctionParser.ParseAttr);

        /// <summary>
        /// Creates a converter for the counter(s) function.
        /// </summary>
        public static readonly IValueConverter CounterConverter = FromParser(FunctionParser.ParseCounter);

        /// <summary>
        /// Creates a converter for the running function.
        /// </summary>
        public static readonly IValueConverter RunningConverter = FromParser(FunctionParser.ParseRunning);

        #endregion

        #region Maps

        /// <summary>
        /// Represents a converter for the UpdateFrequency enumeration.
        /// </summary>
        public static readonly IValueConverter UpdateFrequencyConverter = Map.UpdateFrequencies.ToConverter();

        /// <summary>
        /// Represents a converter for the LineStyle enumeration.
        /// </summary>
        public static readonly IValueConverter LineStyleConverter = Map.LineStyles.ToConverter();

        /// <summary>
        /// Represents a converter for the BackgroundAttachment enumeration.
        /// </summary>
        public static readonly IValueConverter BackgroundAttachmentConverter = Map.BackgroundAttachments.ToConverter();

        /// <summary>
        /// Represents a converter for the BackgroundRepeat enumeration.
        /// </summary>
        public static readonly IValueConverter BackgroundRepeatConverter = Map.BackgroundRepeats.ToConverter();

        /// <summary>
        /// Represents a converter for the BoxModel enumeration.
        /// </summary>
        public static readonly IValueConverter BoxModelConverter = Map.BoxModels.ToConverter();

        /// <summary>
        /// Represents a converter for the AnimationDirection enumeration.
        /// </summary>
        public static readonly IValueConverter AnimationDirectionConverter = Map.AnimationDirections.ToConverter();

        /// <summary>
        /// Represents a converter for the AnimationFillStyle enumeration.
        /// </summary>
        public static readonly IValueConverter AnimationFillStyleConverter = Map.AnimationFillStyles.ToConverter();

        /// <summary>
        /// Represents a converter for animation-composition keyword values.
        /// </summary>
        public static readonly IValueConverter AnimationCompositionConverter = Or(
            Assign(CssKeywords.Replace, CssKeywords.Replace),
            Assign(CssKeywords.Add, CssKeywords.Add),
            Assign(CssKeywords.Accumulate, CssKeywords.Accumulate)).FromList();

        /// <summary>
        /// Represents a converter for animation-timeline values.
        /// </summary>
        public static readonly IValueConverter AnimationTimelineConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            IdentifierConverter).FromList();

        /// <summary>
        /// Represents a converter for a single animation-range-start/end value.
        /// </summary>
        public static readonly IValueConverter AnimationRangeConverter = FromParser(source =>
        {
            var name = Or(
                Assign(CssKeywords.Cover, CssKeywords.Cover),
                Assign(CssKeywords.Contain, CssKeywords.Contain)).Convert(source);

            if (name != null)
            {
                source.SkipSpacesAndComments();
                var pct = LengthOrPercentConverter.Convert(source);
                return pct != null ? new Values.CssTupleValue(new ICssValue[] { name, pct }) : name;
            }

            var normal = Assign<Object>(CssKeywords.Normal, null).Convert(source);
            if (normal != null) return normal;

            return LengthOrPercentConverter.Convert(source);
        });

        /// <summary>
        /// Represents a converter for the TextDecorationStyle enumeration.
        /// </summary>
        public static readonly IValueConverter TextDecorationStyleConverter = Map.TextDecorationStyles.ToConverter();

        /// <summary>
        /// Represents a converter for scroll-behavior (auto | smooth).
        /// </summary>
        public static readonly IValueConverter ScrollBehaviorConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            Assign(CssKeywords.Smooth, CssKeywords.Smooth));

        /// <summary>
        /// Represents a converter for scroll-snap-stop (normal | always).
        /// </summary>
        public static readonly IValueConverter ScrollSnapStopConverter = Or(
            Assign(CssKeywords.Normal, CssKeywords.Normal),
            Assign(CssKeywords.Always, CssKeywords.Always));

        /// <summary>
        /// Represents a converter for overscroll-behavior-x/y/block/inline (auto | contain | none).
        /// </summary>
        public static readonly IValueConverter OverscrollBehaviorConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            Assign(CssKeywords.Contain, CssKeywords.Contain),
            Assign(CssKeywords.None, CssKeywords.None));

        /// <summary>
        /// Represents a converter for the TextDecorationLine enumeration,
        /// taking many values or none.
        /// </summary>
        public static readonly IValueConverter TextDecorationLinesConverter = Or(Map.TextDecorationLines.ToConverter().Many(), None);

        /// <summary>
        /// Represents a converter for the ListPosition enumeration.
        /// </summary>
        public static readonly IValueConverter ListPositionConverter = Map.ListPositions.ToConverter();

        /// <summary>
        /// Represents a converter for the ListStyle enumeration.
        /// </summary>
        public static readonly IValueConverter ListStyleConverter = Or(Map.ListStyles.ToConverter(), StringConverter, SymbolsConverter, CustomIdentConverter);

        /// <summary>
        /// Represents a converter for the SymbolsType enumeration.
        /// </summary>
        public static readonly IValueConverter SymbolsTypeConverter = Map.SymbolsTypes.ToConverter();

        /// <summary>
        /// Represents a converter for the BreakMode enumeration.
        /// </summary>
        public static readonly IValueConverter BreakModeConverter = Map.BreakModes.ToConverter();

        /// <summary>
        /// Represents a converter for the BreakMode enumeration (constraint to the inside values).
        /// </summary>
        public static readonly IValueConverter BreakInsideModeConverter = Map.BreakInsideModes.ToConverter();

        /// <summary>
        /// Represents a converter for the BreakMode enumeration (constraint to the page values).
        /// </summary>
        public static readonly IValueConverter PageBreakModeConverter = Map.PageBreakModes.ToConverter();

        /// <summary>
        /// Represents a converter for the BreakMode enumeration (constraint to the page/inside values).
        /// </summary>
        public static readonly IValueConverter PageBreakInsideModeConverter = Map.PageBreakInsideModes.ToConverter();

        /// <summary>
        /// Represents a converter for the UnicodeMode enumeration.
        /// </summary>
        public static readonly IValueConverter UnicodeModeConverter = Map.UnicodeModes.ToConverter();

        /// <summary>
        /// Represents a converter for the Visibility enumeration.
        /// </summary>
        public static readonly IValueConverter VisibilityConverter = Map.Visibilities.ToConverter();

        /// <summary>
        /// Represents a converter for the PlayState enumeration.
        /// </summary>
        public static readonly IValueConverter PlayStateConverter = Map.PlayStates.ToConverter();

        /// <summary>
        /// Represents a converter for the FontVariant enumeration.
        /// </summary>
        public static readonly IValueConverter FontVariantConverter = Map.FontVariants.ToConverter();

        /// <summary>
        /// Represents a converter for the DirectionMode enumeration.
        /// </summary>
        public static readonly IValueConverter DirectionModeConverter = Map.DirectionModes.ToConverter();

        /// <summary>
        /// Represents a converter for the HorizontalAlignment enumeration.
        /// </summary>
        public static readonly IValueConverter HorizontalAlignmentConverter = Map.HorizontalAlignments.ToConverter();

        /// <summary>
        /// Represents a converter for the VerticalAlignment enumeration.
        /// </summary>
        public static readonly IValueConverter VerticalAlignmentConverter = Map.VerticalAlignments.ToConverter();

        /// <summary>
        /// Represents a converter for the Whitespace enumeration.
        /// </summary>
        public static readonly IValueConverter WhitespaceConverter = Map.Whitespaces.ToConverter();

        /// <summary>
        /// Represents a converter for the TextTransform enumeration.
        /// </summary>
        public static readonly IValueConverter TextTransformConverter = Map.TextTransforms.ToConverter();

		/// <summary>
		/// Represents a converter for the TextAlignLast enumeration.
		/// </summary>
		public static readonly IValueConverter TextAlignLastConverter = Map.TextAlignLasts.ToConverter();

		/// <summary>
		/// Represents a converter for the TextAnchor enumeration.
		/// </summary>
		public static readonly IValueConverter TextAnchorConverter = Map.TextAnchors.ToConverter();

		/// <summary>
		/// Represents a converter for the TextJustify enumeration.
		/// </summary>
		public static readonly IValueConverter TextJustifyConverter = Map.TextJustifies.ToConverter();

		/// <summary>
		/// Represents a converter for the ObjectFitting enumeration.
		/// </summary>
		public static readonly IValueConverter ObjectFittingConverter = Map.ObjectFittings.ToConverter();

        /// <summary>
        /// Represents a converter for the PositionMode enumeration.
        /// </summary>
        public static readonly IValueConverter PositionModeConverter = Or(Map.PositionModes.ToConverter(), RunningConverter);

        /// <summary>
        /// Represents a converter for the OverflowMode enumeration.
        /// </summary>
        public static readonly IValueConverter OverflowModeConverter = Map.OverflowModes.ToConverter();

        /// <summary>
        /// Represents a converter for the extended (directional) OverflowMode enumeration.
        /// </summary>
        public static readonly IValueConverter OverflowExtendedModeConverter = Map.OverflowExtendedModes.ToConverter();

        /// <summary>
        /// Represents a converter for the Floating enumeration.
        /// </summary>
        public static readonly IValueConverter FloatingConverter = Map.Floatings.ToConverter();

        /// <summary>
        /// Represents a converter for the DisplayMode enumeration.
        /// </summary>
        public static readonly IValueConverter DisplayModeConverter = Map.DisplayModes.ToConverter();

        /// <summary>
        /// Represents a converter for the ClearMode enumeration.
        /// </summary>
        public static readonly IValueConverter ClearModeConverter = Map.ClearModes.ToConverter();

        /// <summary>
        /// Represents a converter for the FontStretch enumeration.
        /// </summary>
        public static readonly IValueConverter FontStretchConverter = Map.FontStretches.ToConverter();

        /// <summary>
        /// Represents a converter for the FontStyle enumeration.
        /// </summary>
        public static readonly IValueConverter FontStyleConverter = Map.FontStyles.ToConverter();

        /// <summary>
        /// Represents a converter for the FontWeight enumeration.
        /// </summary>
        public static readonly IValueConverter FontWeightConverter = Map.FontWeights.ToConverter();

        /// <summary>
        /// Represents a converter for the font-display descriptor.
        /// </summary>
        public static readonly IValueConverter FontDisplayConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            Assign(CssKeywords.Block, CssKeywords.Block),
            Assign(CssKeywords.Swap, CssKeywords.Swap),
            Assign(CssKeywords.Fallback, CssKeywords.Fallback),
            Assign(CssKeywords.Optional, CssKeywords.Optional));

        /// <summary>
        /// Represents a converter for the font-kerning property.
        /// </summary>
        public static readonly IValueConverter FontKerningConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            Assign(CssKeywords.Normal, CssKeywords.Normal),
            Assign(CssKeywords.None, CssKeywords.None));

        /// <summary>
        /// Represents a converter for the font-language-override property.
        /// </summary>
        public static readonly IValueConverter FontLanguageOverrideConverter = Assign(CssKeywords.Normal, CssKeywords.Normal);

        /// <summary>
        /// Represents a converter for the font-optical-sizing property.
        /// </summary>
        public static readonly IValueConverter FontOpticalSizingConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            Assign(CssKeywords.None, CssKeywords.None));

        /// <summary>
        /// Represents a converter for the font-palette property.
        /// </summary>
        public static readonly IValueConverter FontPaletteConverter = Or(
            Assign(CssKeywords.Normal, CssKeywords.Normal),
            IdentifierConverter);

        /// <summary>
        /// Represents a converter for the font-synthesis property (shorthand).
        /// </summary>
        public static readonly IValueConverter FontSynthesisConverter = Or(
            Assign(CssKeywords.None, CssKeywords.None),
            WithAny(
                Or(
                    Assign(CssKeywords.Weight, CssKeywords.Weight),
                    Assign(CssKeywords.Style, CssKeywords.Style),
                    Assign(CssKeywords.SmallCaps, CssKeywords.SmallCaps)
                )
            ).Many());

        /// <summary>
        /// Represents a converter for the font-synthesis-weight property.
        /// </summary>
        public static readonly IValueConverter FontSynthesisWeightConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            Assign(CssKeywords.None, CssKeywords.None));

        /// <summary>
        /// Represents a converter for the font-synthesis-style property.
        /// </summary>
        public static readonly IValueConverter FontSynthesisStyleConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            Assign(CssKeywords.None, CssKeywords.None));

        /// <summary>
        /// Represents a converter for the font-synthesis-small-caps property.
        /// </summary>
        public static readonly IValueConverter FontSynthesisSmallCapsConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            Assign(CssKeywords.None, CssKeywords.None));

        /// <summary>
        /// Represents a converter for the font-variation-settings property.
        /// </summary>
        public static readonly IValueConverter FontVariationSettingsConverter = Assign(CssKeywords.Normal, CssKeywords.Normal);

        /// <summary>
        /// Represents a converter for the ResizeMode enumeration.
        /// </summary>
        public static readonly IValueConverter ResizeConverter = Map.ResizeModes.ToConverter();

        /// <summary>
        /// Represents a converter for the RubyAlignment enumeration.
        /// </summary>
        public static readonly IValueConverter RubyAlignmentConverter = Map.RubyAlignments.ToConverter();

        /// <summary>
        /// Represents a converter for the RubyOverhandMode enumeration.
        /// </summary>
        public static readonly IValueConverter RubyOverhangModeConverter = Map.RubyOverhangModes.ToConverter();

        /// <summary>
        /// Represents a converter for the RubyPosition enumeration.
        /// </summary>
        public static readonly IValueConverter RubyPositionConverter = Map.RubyPositions.ToConverter();

        /// <summary>
        /// Represents a converter for the PointerEvent enumeration.
        /// </summary>
        public static readonly IValueConverter PointerEventConverter = Map.PointerEvents.ToConverter();

        /// <summary>
        /// Represents a converter for the SystemFont enumeration.
        /// </summary>
        public static readonly IValueConverter SystemFontConverter = Map.SystemFonts.ToConverter();

		/// <summary>
		/// Represents a converter for the StrokeLinecap enumeration.
		/// </summary>
		public static readonly IValueConverter StrokeLinecapConverter = Map.StrokeLinecaps.ToConverter();

		/// <summary>
		/// Represents a converter for the StrokeLinejoin enumeration.
		/// </summary>
		public static readonly IValueConverter StrokeLinejoinConverter = Map.StrokeLinejoins.ToConverter();

		/// <summary>
		/// Represents a converter for the WordBreak enumeration.
		/// </summary>
        public static readonly IValueConverter WordBreakConverter = Or(
            Map.WordBreaks.ToConverter(),
            Assign(CssKeywords.BreakWord, CssKeywords.BreakWord));

		/// <summary>
		/// Represents a converter for the OverflowWrap enumeration.
		/// </summary>
		public static readonly IValueConverter OverflowWrapConverter = Map.OverflowWraps.ToConverter();

        /// <summary>
        /// Represents a converter for the BorderImageRepeat property.
        /// </summary>
        public static readonly IValueConverter BorderImageRepeatConverter = Map.BorderRepeats.ToConverter().Many(1, 2);

        /// <summary>
        /// Represents a converter for the ScriptingState enumeration.
        /// </summary>
        public static readonly IValueConverter ScriptingStateConverter = Map.ScriptingStates.ToConverter();

        /// <summary>
        /// Represents a converter for the PointerAccuracy enumeration.
        /// </summary>
        public static readonly IValueConverter PointerAccuracyConverter = Map.PointerAccuracies.ToConverter();

        /// <summary>
        /// Represents a converter for the HoverAbility enumeration.
        /// </summary>
        public static readonly IValueConverter HoverAbilityConverter = Map.HoverAbilities.ToConverter();

        /// <summary>
        /// Represents a converter for the JustifyContent enumeration.
        /// </summary>
        public static readonly IValueConverter JustifyContentConverter = Map.JustifyContentModes.ToConverter();

        /// <summary>
        /// Represents a converter for the AlignContent enumeration.
        /// </summary>
        public static readonly IValueConverter AlignContentConverter = Map.AlignContentModes.ToConverter();

        /// <summary>
        /// Represents a converter for the AlignSelf enumeration.
        /// </summary>
        public static readonly IValueConverter AlignSelfConverter = Map.AlignSelfModes.ToConverter();

        /// <summary>
        /// Represents a converter for the AlignItems enumeration.
        /// </summary>
        public static readonly IValueConverter AlignItemsConverter = Map.AlignItemsModes.ToConverter();

        /// <summary>
        /// Represents a converter for the JustifyItems enumeration.
        /// </summary>
        public static readonly IValueConverter JustifyItemsConverter = Map.JustifyItemsModes.ToConverter();

        /// <summary>
        /// Represents a converter for the JustifySelf enumeration.
        /// </summary>
        public static readonly IValueConverter JustifySelfConverter = Map.JustifySelfModes.ToConverter();

        /// <summary>
        /// Represents a converter for the FlexDirection enumeration.
        /// </summary>
        public static readonly IValueConverter FlexDirectionConverter = Map.FlexDirections.ToConverter();

        /// <summary>
        /// Represents a converter for the FlexWrap enumeration.
        /// </summary>
        public static readonly IValueConverter FlexWrapConverter = Map.FlexWrapModes.ToConverter();

        /// <summary>
        /// Represents a converter for the BookmarkState enumeration.
        /// </summary>
        public static readonly IValueConverter BookmarkStateConverter = Map.BookmarkStates.ToConverter();

        /// <summary>
        /// Represents a converter for the FootnoteDisplay enumeration.
        /// </summary>
        public static readonly IValueConverter FootnoteDisplayConverter = Map.FootnoteDisplays.ToConverter();

        /// <summary>
        /// Represents a converter for the FootnotePolicy enumeration.
        /// </summary>
        public static readonly IValueConverter FootnotePolicyConverter = Map.FootnotePolicies.ToConverter();

        /// <summary>
        /// Represents a converter for the Visibility enumeration.
        /// </summary>
        public static readonly IValueConverter ContentVisibilityConverter = Map.Visibilities.ToConverter();

        /// <summary>
        /// Represents a converter for the ScrollSnapAlignment enumeration.
        /// </summary>
        public static readonly IValueConverter ScrollSnapAlignmentConverter = Map.ScrollSnapAlignments.ToConverter();

        /// <summary>
        /// Represents a converter for the ScrollSnapAxis enumeration.
        /// </summary>
        public static readonly IValueConverter ScrollSnapAxisConverter = Map.ScrollSnapAxises.ToConverter();

        /// <summary>
        /// Represents a converter for the ScrollSnapStrictness enumeration.
        /// </summary>
        public static readonly IValueConverter ScrollSnapStrictnessConverter = Map.ScrollSnapStrictnesses.ToConverter();

        #endregion

        #region Specific

        /// <summary>
        /// Represents an optional integer object.
        /// </summary>
        public static readonly IValueConverter OptionalIntegerConverter = Or(
            IntegerConverter,
            Auto);

        /// <summary>
        /// Represents a positive or infinite number object.
        /// </summary>
        public static readonly IValueConverter PositiveOrInfiniteNumberConverter = Or(
            NaturalNumberConverter,
            Assign(CssKeywords.Infinite, Double.PositiveInfinity));

        /// <summary>
        /// Represents a positive or infinite number object.
        /// </summary>
        public static readonly IValueConverter OptionalNumberConverter = Or(
            NumberConverter,
            None);

        /// <summary>
        /// Represents a length object or null, when "normal" is given.
        /// </summary>
        public static readonly IValueConverter OptionalLengthConverter = Or(
            LengthConverter,
            Assign(CssKeywords.Normal, CssLengthValue.Normal));

        /// <summary>
        /// Represents a length (or default).
        /// </summary>
        public static readonly IValueConverter AutoLengthConverter = Or(
            LengthConverter,
            Auto);

        /// <summary>
        /// Represents a distance object (either Length or Percent) or none.
        /// </summary>
        public static readonly IValueConverter OptionalLengthOrPercentConverter = Or(
            LengthOrPercentConverter,
            None);

        /// <summary>
        /// Represents a distance object (or default).
        /// </summary>
        public static readonly IValueConverter AutoLengthOrPercentConverter = Or(
            LengthOrPercentConverter,
            Auto);

        /// <summary>
        /// Represents a converter for scroll-padding longhands (auto | length-percentage).
        /// </summary>
        public static readonly IValueConverter ScrollPaddingConverter = AutoLengthOrPercentConverter;
        
        /// <summary>
        /// Represents a value for a width.
        /// </summary>
        public static readonly IValueConverter WidthConverter = Or(
            LengthOrPercentConverter,
            Auto,
            Map.Sizings.ToConverter());

        /// <summary>
        /// Represents a length for a font size.
        /// </summary>
        public static readonly IValueConverter FontSizeConverter = Or(
            LengthOrPercentConverter,
            Map.FontSizes.ToConverter());

        #endregion

        #region Composed

        /// <summary>
        /// Represents a distance object with line-height additions.
        /// http://www.w3.org/TR/CSS2/visudet.html#propdef-line-height
        /// </summary>
        public static readonly IValueConverter LineHeightConverter = Or(
            LengthOrPercentConverter,
            NumberConverter,
            Assign(CssKeywords.Normal, CssLengthValue.Normal));
        
        /// <summary>
        /// Represents a distance object or normal length.
        /// </summary>
        public static readonly IValueConverter GapConverter = Or(
            LengthOrPercentConverter,
            Assign(CssKeywords.Normal, CssLengthValue.Normal));

        /// <summary>
        /// Represents a length object that is based on percentage or number.
        /// http://dev.w3.org/csswg/css-backgrounds/#border-image-slice
        /// </summary>
        public static readonly IValueConverter BorderImageSliceConverter = FromParser(CompoundParser.ParseBorderImageSlice);

        /// <summary>
        /// Represents a length object that is based on percentage, length or number.
        /// http://dev.w3.org/csswg/css-backgrounds/#border-image-width
        /// </summary>
        public static readonly IValueConverter ImageBorderWidthConverter = FromParser(UnitParser.ParseBorderWidth);

        /// <summary>
        /// Represents a length object derived from an image border-width.
        /// </summary>
        public static readonly IValueConverter BorderImageWidthConverter = ImageBorderWidthConverter.Periodic();

        /// <summary>
        /// Represents a timing-function object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/timing-function
        /// </summary>
        public static readonly IValueConverter TransitionConverter = FromParser(TimingFunctionParser.ParseTimingFunction);

        /// <summary>
        /// Represents a gradient object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/gradient
        /// </summary>
        public static readonly IValueConverter GradientConverter = FromParser(GradientParser.ParseGradient);

        /// <summary>
        /// Represents a transform function.
        /// http://www.w3.org/TR/css3-transforms/#typedef-transform-function
        /// </summary>
        public static readonly IValueConverter TransformConverter = FromParser(TransformParser.ParseTransform);

        /// <summary>
        /// Represents a color object or, alternatively, the current color.
        /// </summary>
        public static readonly IValueConverter CurrentColorConverter = new StructValueConverter<CssColorValue>(ColorParser.ParseCurrentColor);

        /// <summary>
        /// Represents a color object, the current color, or the inverted current color.
        /// </summary>
        public static readonly IValueConverter InvertedColorConverter = Or(
            CurrentColorConverter,
            Assign(CssKeywords.Invert, CssColorValue.InvertedColor));

		/// <summary>
		/// Represents a paint object.
		/// </summary>
		public static readonly IValueConverter PaintConverter = Or(
            UrlConverter,
            CurrentColorConverter,
            None);

		/// <summary>
		/// Represents a converter for Stroke Dasharray property
		/// taking many values or none.
		/// </summary>
		public static readonly IValueConverter StrokeDasharrayConverter = Or(
            Or(LengthOrPercentConverter, NumberConverter).Many(),
            None);

		/// <summary>
		/// Represents a converter for the StrokeMiterlimit enumeration.
		/// </summary>
		public static readonly IValueConverter StrokeMiterlimitConverter = new StructValueConverter<CssNumberValue>(NumberParser.ParseGreaterOrEqualOneNumber);

		/// <summary>
		/// Represents a ratio object.
		/// https://developer.mozilla.org/en-US/docs/Web/CSS/ratio
		/// </summary>
		public static readonly IValueConverter RatioConverter = new StructValueConverter<CssRatioValue>(NumberParser.ParseRatio);

        /// <summary>
        /// Represents a converter for the aspect-ratio property (auto | &lt;ratio&gt;).
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/aspect-ratio
        /// </summary>
        public static readonly IValueConverter AspectRatioConverter = Or(Auto, RatioConverter);

        /// <summary>
        /// Represents a converter for the overflow-anchor property (auto | none).
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/overflow-anchor
        /// </summary>
        public static readonly IValueConverter OverflowAnchorConverter = Or(Auto, None);

        /// <summary>
        /// Represents a converter for the overflow-clip-margin property (&lt;visual-box&gt; || &lt;length&gt;).
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/overflow-clip-margin
        /// </summary>
        public static readonly IValueConverter OverflowClipMarginConverter = Or(LengthOrPercentConverter, BoxModelConverter);

        /// <summary>
        /// Represents multiple shadow objects.
        /// http://dev.w3.org/csswg/css-backgrounds/#shadow
        /// </summary>
        public static readonly IValueConverter MultipleShadowConverter = Or(
            FromParser(ShadowParser.ParseShadow).FromList(),
            None);

        /// <summary>
        /// Represents an optional image source object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/image
        /// </summary>
        public static readonly IValueConverter OptionalImageSourceConverter = Or(
            UrlConverter,
            GradientConverter,
            None);

        /// <summary>
        /// Represents multiple image source object.
        /// </summary>
        public static readonly IValueConverter MultipleImageSourceConverter = OptionalImageSourceConverter.FromList();

        /// <summary>
        /// Represents the border-radius (horizontal / vertical; radius) converter.
        /// </summary>
        public static readonly IValueConverter BorderRadiusLonghandConverter = LengthOrPercentConverter.Radius();

        /// <summary>
        /// Represents a converter for font families.
        /// </summary>
        public static readonly IValueConverter FontFamiliesConverter = FromParser(IdentParser.ParseFontFamily).FromList();

        /// <summary>
        /// Represents a converter for background size.
        /// </summary>
        public static readonly IValueConverter BackgroundSizeConverter = FromParser(PointParser.ParseSize);

        /// <summary>
        /// Represents a converter for background repeat.
        /// </summary>
        public static readonly IValueConverter BackgroundRepeatsConverter = FromParser(CompoundParser.ParseBackgroundRepeat);

        /// <summary>
        /// Represents a converter for the content-list.
        /// </summary>
        public static readonly IValueConverter ContentListConverter = Or(StringConverter, CounterConverter, ContentConverter, AttrConverter).Many();

        #endregion

        #region Toggles

        /// <summary>
        /// Represents a converter for the orientation mode.
        /// </summary>
        public static readonly IValueConverter OrientationModeConverter = Toggle(CssKeywords.Portrait, CssKeywords.Landscape);

        /// <summary>
        /// Represents a converter for the table layout mode.
        /// </summary>
        public static readonly IValueConverter TableLayoutConverter = Toggle(CssKeywords.Fixed, CssKeywords.Auto);

        /// <summary>
        /// Represents a converter for the scan mode.
        /// </summary>
        public static readonly IValueConverter ScanModeConverter = Toggle(CssKeywords.Interlace, CssKeywords.Progressive);

        /// <summary>
        /// Represents a converter for the empty cells mode.
        /// </summary>
        public static readonly IValueConverter EmptyCellsConverter = Toggle(CssKeywords.Show, CssKeywords.Hide);

        /// <summary>
        /// Represents a converter for the caption side mode.
        /// </summary>
        public static readonly IValueConverter CaptionSideConverter = Toggle(CssKeywords.Top, CssKeywords.Bottom);

        /// <summary>
        /// Represents a converter for the backface visibility mode.
        /// </summary>
        public static readonly IValueConverter BackfaceVisibilityConverter = Toggle(CssKeywords.Visible, CssKeywords.Hidden);

        /// <summary>
        /// Represents a converter for the border collapse mode.
        /// </summary>
        public static readonly IValueConverter BorderCollapseConverter = Toggle(CssKeywords.Separate, CssKeywords.Collapse);

        /// <summary>
        /// Represents a converter for the box decoration break mode.
        /// </summary>
        public static readonly IValueConverter BoxDecorationConverter = Toggle(CssKeywords.Clone, CssKeywords.Slice);

        /// <summary>
        /// Represents a converter for the column span mode.
        /// </summary>
        public static readonly IValueConverter ColumnSpanConverter = Toggle(CssKeywords.All, CssKeywords.None);

        /// <summary>
        /// Represents a converter for the column fill mode.
        /// </summary>
        public static readonly IValueConverter ColumnFillConverter = Toggle(CssKeywords.Balance, CssKeywords.Auto);

        #endregion

        #region Order / Unordered

        /// <summary>
        /// Uses the provided converters successively in order.
        /// </summary>
        /// <param name="converters">The converters that are used.</param>
        /// <returns>The new converter.</returns>
        public static IValueConverter WithOrder(params IValueConverter[] converters) => new OrderedOptionsConverter(converters);

        /// <summary>
        /// Uses the converters in any order to convert provided values.
        /// </summary>
        /// <param name="converters">The converters that are used.</param>
        /// <returns>The new converter.</returns>
        public static IValueConverter WithAny(params IValueConverter[] converters) => new UnorderedOptionsConverter(converters);

        #endregion

        #region Grid

        /// <summary>
        /// Represents a converter for LineName values.
        /// </summary>
        public static readonly IValueConverter LineNamesConverter = new StructValueConverter<CssLineNamesValue>(GridParser.ParseLineNames);

        /// <summary>
        /// Represents a converter for TrackSize values.
        /// </summary>
        public static readonly IValueConverter TrackSizeConverter = FromParser(GridParser.ParseTrackSize);

        /// <summary>
        /// Represents a converter for FixedSize values.
        /// </summary>
        public static readonly IValueConverter FixedSizeConverter = FromParser(GridParser.ParseFixedSize);

        /// <summary>
        /// Represents a converter for TrackRepeat values.
        /// </summary>
        public static readonly IValueConverter TrackRepeatConverter = FromParser(GridParser.ParseTrackRepeat);

        /// <summary>
        /// Represents a converter for FixedRepeat values.
        /// </summary>
        public static readonly IValueConverter FixedRepeatConverter = FromParser(GridParser.ParseFixedRepeat);

        /// <summary>
        /// Represents a converter for AutoRepeat values.
        /// </summary>
        public static readonly IValueConverter AutoRepeatConverter = FromParser(GridParser.ParseAutoRepeat);

        /// <summary>
        /// Represents a converter for TrackList values.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/grid-template-columns#track-list
        /// </summary>
        public static readonly IValueConverter TrackListConverter = FromParser(GridParser.ParseTrackList);

        /// <summary>
        /// Represents a converter for AutoTrackList values.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/grid-template-columns#auto-track-list
        /// </summary>
        public static readonly IValueConverter AutoTrackListConverter = FromParser(GridParser.ParseAutoTrackList);

        #endregion

        #region Premade

        public static IValueConverter WithBorderSide(ICssValue lineWidth, ICssValue lineStyle, ICssValue lineColor) => AggregateTuple(
            WithAny(
                Or(LineWidthConverter, VarConverter).Option(lineWidth),
                Or(LineStyleConverter, VarConverter).Option(lineStyle),
                Or(CurrentColorConverter, VarConverter).Option(lineColor)));

        public static readonly IValueConverter GridTemplateConverter = Or(None, TrackListConverter.Exclusive(), AutoTrackListConverter.Exclusive());

        public static readonly IValueConverter GridAutoConverter = TrackSizeConverter.Many();

        public static readonly IValueConverter GridLineConverter = Or(
            Assign(CssKeywords.Auto, CssKeywords.Auto),
            WithAny(Assign(CssKeywords.Span, true), IntegerConverter, IdentifierConverter));

        public static readonly IValueConverter SrcListConverter =
            WithOrder(
                Or(UrlConverter, FromParser(ParseLocal)),
                Or(FromParser(ParseFormat), None))
            .FromList();

        #endregion

        #region Helpers

        private static IValueConverter FromParser<T>(Func<StringSource, T?> converter)
            where T : struct, ICssValue => new StructValueConverter<T>(converter);

        private static IValueConverter FromParser<T>(Func<StringSource, T> converter)
            where T : class, ICssValue => new ClassValueConverter<T>(converter);

        private static Func<StringSource, CssStringValue?> FromString(Func<StringSource, String> converter) => source =>
        {
            var result = converter.Invoke(source);

            if (result != null)
            {
                return new CssStringValue(result);
            }

            return null;
        };

        private static Func<StringSource, CssLengthValue?> FromInteger(Func<StringSource, Int32?> converter) => source =>
        {
            var result = converter.Invoke(source);

            if (result.HasValue)
            {
                return new CssLengthValue(result.Value, CssLengthValue.Unit.None);
            }

            return null;
        };

        private static Func<StringSource, CssLengthValue?> FromNumber(Func<StringSource, Double?> converter) => source =>
        {
            var result = converter.Invoke(source);

            if (result.HasValue)
            {
                return new CssLengthValue(result.Value, CssLengthValue.Unit.None);
            }

            return null;
        };

        private static ICssFunctionValue ParseLocal(this StringSource source)
        {
            if (source.IsFunction(CssKeywords.Local))
            {
                var content = source.ParseString() ?? source.ParseIdent();
                var f = source.SkipGetSkip();

                if (content != null && f == Symbols.RoundBracketClose)
                {
                    return new CssLocalFontValue(content);
                }
            }

            return null;
        }

        private static ICssFunctionValue ParseFormat(this StringSource source)
        {
            if (source.IsFunction(CssKeywords.Format))
            {
                var content = source.ParseString() ?? source.ParseIdent();
                var f = source.SkipGetSkip();

                if (content != null && f == Symbols.RoundBracketClose)
                {
                    return new CssFontFormatValue(content);
                }
            }

            return null;
        }

        #endregion

        #region Aggregators

        public static IValueConverter AggregatePeriodic(IValueConverter converter) => new PeriodicAggregator(converter);

        public static IValueConverter AggregateTuple(IValueConverter converter) => new TupleAggregator(converter);

        sealed class PeriodicAggregator : IValueAggregator, IValueConverter
        {
            private readonly IValueConverter _converter;

            public PeriodicAggregator(IValueConverter converter)
            {
                _converter = converter.Periodic();
            }

            public ICssValue Convert(StringSource source) => _converter.Convert(source);

            public ICssValue Merge(ICssValue[] values)
            {
                var first = values[0];

                if (first != null)
                {
                    var same = values.All(m => Object.Equals(m, first));
                    return same ? first : new CssPeriodicValue(values);
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                if (value is CssPeriodicValue periodic)
                {
                    return periodic.ToArray();
                }

                return new[]
                {
                    value,
                    value,
                    value,
                    value,
                };
            }
        }

        sealed class TupleAggregator : IValueAggregator, IValueConverter
        {
            private readonly IValueConverter _converter;

            public TupleAggregator(IValueConverter converter)
            {
                _converter = converter;
            }

            public ICssValue Convert(StringSource source) => _converter.Convert(source);

            public ICssValue Merge(ICssValue[] values)
            {
                if (values.Any(m => m != null))
                {
                    return new CssTupleValue(values);
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                if (value is CssTupleValue options)
                {
                    return options.ToArray();
                }

                return null;
            }
        }

        #endregion
    }
}
