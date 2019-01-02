namespace AngleSharp.Css
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// A set of already constructed CSS value converters.
    /// </summary>
    static class ValueConverters
    {
        #region Misc

        /// <summary>
        /// Creates an or converter for the given converters.
        /// </summary>
        public static IValueConverter Or(params IValueConverter[] converters) => new OrValueConverter(converters);

        public static IValueConverter SlashSeparated(IValueConverter converter) => new SeparatorConverter(converter, Symbols.Solidus);

        /// <summary>
        /// Creates a converter for the initial keyword with the given value.
        /// </summary>
        public static IValueConverter AssignInitial<T>(T value) => new StandardValueConverter<T>(value);

        /// <summary>
        /// Creates a converter for the initial keyword with no value.
        /// </summary>
        public static IValueConverter AssignInitial() => AssignInitial<Object>(null);

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
        public static IValueConverter Auto = new IdentifierValueConverter<Length>(CssKeywords.Auto, Length.Auto);

        /// <summary>
        /// Represents a converter for the content keyword with no value.
        /// </summary>
        public static IValueConverter Content = new IdentifierValueConverter<Length>(CssKeywords.Content, Length.Content);

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
        public static readonly IValueConverter OnlyLengthConverter = new StructValueConverter<Length>(UnitParser.ParseLength);

        /// <summary>
        /// Represents a resolution object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/resolution
        /// </summary>
        public static readonly IValueConverter OnlyResolutionConverter = new StructValueConverter<Resolution>(UnitParser.ParseResolution);

        /// <summary>
        /// Represents a time object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/time
        /// </summary>
        public static readonly IValueConverter OnlyTimeConverter = new StructValueConverter<Time>(UnitParser.ParseTime);

        /// <summary>
        /// Represents a distance object (either Length or Percent).
        /// </summary>
        public static readonly IValueConverter OnlyLengthOrPercentConverter = new StructValueConverter<Length>(UnitParser.ParseDistance);

        /// <summary>
        /// Represents a string object.
        /// </summary>
        public static readonly IValueConverter StringConverter = FromParser(FromString(StringParser.ParseString));

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
        /// Represents an identifier object that matches the production rules of a single transition property.
        /// http://dev.w3.org/csswg/css-transitions/#single-transition-property
        /// </summary>
        public static readonly IValueConverter AnimatableConverter = new IdentifierValueConverter(IdentParser.ParseAnimatableIdent);

        /// <summary>
        /// Represents an integer object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/integer
        /// </summary>
        public static readonly IValueConverter OnlyIntegerConverter = new StructValueConverter<Length>(FromInteger(NumberParser.ParseInteger));

        /// <summary>
        /// Represents an integer object that is zero or greater.
        /// </summary>
        public static readonly IValueConverter NaturalIntegerConverter = new StructValueConverter<Length>(FromInteger(NumberParser.ParseNaturalInteger));

        /// <summary>
        /// Represents an integer object that only allows values \in { 100, 200, ..., 900 }.
        /// </summary>
        public static readonly IValueConverter WeightIntegerConverter = new StructValueConverter<Length>(FromInteger(NumberParser.ParseWeightInteger));

        /// <summary>
        /// Represents an integer object that is greater tha zero.
        /// </summary>
        public static readonly IValueConverter PositiveIntegerConverter = new StructValueConverter<Length>(FromInteger(NumberParser.ParsePositiveInteger));

        /// <summary>
        /// Represents an integer object with 0 or 1.
        /// </summary>
        public static readonly IValueConverter BinaryConverter = new StructValueConverter<Length>(FromInteger(NumberParser.ParseBinary));

        /// <summary>
        /// Represents a number object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/number
        /// </summary>
        public static readonly IValueConverter OnlyNumberConverter = new StructValueConverter<Length>(FromNumber(NumberParser.ParseNumber));

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
        public static readonly IValueConverter NaturalNumberConverter = new StructValueConverter<Length>(FromNumber(NumberParser.ParseNaturalNumber));

        /// <summary>
        /// Represents an color object (usually hex or name).
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/color
        /// </summary>
        public static readonly IValueConverter ColorConverter = new StructValueConverter<Color>(ColorParser.ParseColor);

        /// <summary>
        /// Represents a position object.
        /// http://www.w3.org/TR/css3-background/#ltpositiongt
        /// </summary>
        public static readonly IValueConverter PointConverter = new StructValueConverter<Point>(PointParser.ParsePoint);

        /// <summary>
        /// Represents a Point3 object.
        /// </summary>
        public static readonly IValueConverter Point3Converter = FromParser(PointParser.ParsePoint3);

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

        #endregion

        #region Functions

        /// <summary>
        /// Represents a shape object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/shape
        /// </summary>
        public static readonly IValueConverter ShapeConverter = FromParser(ShapeParser.ParseShape);

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
        /// Represents a converter for the TextDecorationStyle enumeration.
        /// </summary>
        public static readonly IValueConverter TextDecorationStyleConverter = Map.TextDecorationStyles.ToConverter();

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
        public static readonly IValueConverter ListStyleConverter = Map.ListStyles.ToConverter();

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
		/// Represents a converter for the TextTAligLast enumeration.
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
        public static readonly IValueConverter PositionModeConverter = Map.PositionModes.ToConverter();

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
		public static readonly IValueConverter WordBreakConverter = Map.WordBreaks.ToConverter();

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
        /// Represents a converter for the FlexDirection enumeration.
        /// </summary>
        public static readonly IValueConverter FlexDirectionConverter = Map.FlexDirections.ToConverter();

        /// <summary>
        /// Represents a converter for the FlexWrap enumeration.
        /// </summary>
        public static readonly IValueConverter FlexWrapConverter = Map.FlexWrapModes.ToConverter();

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
            Assign(CssKeywords.Normal, Length.Normal));

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
            Assign(CssKeywords.Normal, Length.Normal));
        
        /// <summary>
        /// Represents a distance object or normal length.
        /// </summary>
        public static readonly IValueConverter GapConverter = Or(
            LengthOrPercentConverter,
            Assign(CssKeywords.Normal, Length.Normal));

        /// <summary>
        /// Represents a length object that is based on percentage or number.
        /// http://dev.w3.org/csswg/css-backgrounds/#border-image-slice
        /// </summary>
        public static readonly IValueConverter BorderImageSliceConverter = new StructValueConverter<BorderImageSlice>(CompoundParser.ParseBorderImageSlice);

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
        public static readonly IValueConverter CurrentColorConverter = new StructValueConverter<Color>(ColorParser.ParseCurrentColor);

        /// <summary>
        /// Represents a color object, the current color, or the inverted current color.
        /// </summary>
        public static readonly IValueConverter InvertedColorConverter = Or(
            CurrentColorConverter,
            Assign(CssKeywords.Invert, Color.InvertedColor));

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
		public static readonly IValueConverter StrokeMiterlimitConverter = new StructValueConverter<Length>(FromNumber(NumberParser.ParseGreaterOrEqualOneNumber));

		/// <summary>
		/// Represents a ratio object.
		/// https://developer.mozilla.org/en-US/docs/Web/CSS/ratio
		/// </summary>
		public static readonly IValueConverter RatioConverter = new StructValueConverter<Length>(FromNumber(NumberParser.ParseRatio));

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
        public static readonly IValueConverter BorderRadiusLonghandConverter = WithOrder(
            LengthOrPercentConverter,
            LengthOrPercentConverter.Option());

        /// <summary>
        /// Represents a converter for font families.
        /// </summary>
        public static readonly IValueConverter FontFamiliesConverter = FromParser(IdentParser.ParseFontFamily).FromList();

        /// <summary>
        /// Represents a converter for background size.
        /// </summary>
        public static readonly IValueConverter BackgroundSizeConverter = new StructValueConverter<BackgroundSize>(PointParser.ParseSize);

        /// <summary>
        /// Represents a converter for background repeat.
        /// </summary>
        public static readonly IValueConverter BackgroundRepeatsConverter = new StructValueConverter<ImageRepeats>(CompoundParser.ParseBackgroundRepeat);

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
        public static readonly IValueConverter LineNamesConverter = FromParser(GridParser.ParseLineNames);

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

        public static readonly IValueConverter MarginConverter = Or(AutoLengthOrPercentConverter, AssignInitial(Length.Zero));

        public static readonly IValueConverter PaddingConverter = Or(LengthOrPercentConverter, AssignInitial(Length.Zero));

        public static readonly IValueConverter BorderSideConverter = Or(WithAny(LineWidthConverter.Option(), LineStyleConverter.Option(), CurrentColorConverter.Option()), AssignInitial());

        public static readonly IValueConverter GridTemplateConverter = Or(None, TrackListConverter.Exclusive(), AutoTrackListConverter.Exclusive(), AssignInitial());

        public static readonly IValueConverter GridAutoConverter = Or(TrackSizeConverter.Many(), AssignInitial());

        public static readonly IValueConverter GridLineConverter = Or(
            Assign(CssKeywords.Auto, "auto"),
            WithAny(Assign(CssKeywords.Span, true), IntegerConverter, IdentifierConverter),
            AssignInitial());

        #endregion

        #region Helpers

        private static IValueConverter FromParser<T>(Func<StringSource, T> converter)
            where T : class, ICssValue
        {
            return new ClassValueConverter<T>(converter);
        }

        private static Func<StringSource, StringValue> FromString(Func<StringSource, String> converter)
        {
            return source =>
            {
                var result = converter.Invoke(source);

                if (result != null)
                {
                    return new StringValue(result);
                }

                return null;
            };
        }

        private static Func<StringSource, Length?> FromInteger(Func<StringSource, Int32?> converter)
        {
            return source =>
            {
                var result = converter.Invoke(source);

                if (result.HasValue)
                {
                    return new Length(result.Value, Length.Unit.None);
                }

                return null;
            };
        }

        private static Func<StringSource, Length?> FromNumber(Func<StringSource, Double?> converter)
        {
            return source =>
            {
                var result = converter.Invoke(source);

                if (result.HasValue)
                {
                    return new Length(result.Value, Length.Unit.None);
                }

                return null;
            };
        }

        #endregion
    }
}
