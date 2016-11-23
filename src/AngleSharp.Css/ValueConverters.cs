namespace AngleSharp.Css
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Extensions;
    using AngleSharp.Css.Values;
    using System;
    using System.Linq;

    /// <summary>
    /// A set of already constructed CSS value converters.
    /// </summary>
    static class ValueConverters
    {
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
        /// Represents a converter for the initial keyword with no value.
        /// </summary>
        public static IValueConverter Initial = new IdentifierValueConverter<Object>(CssKeywords.Initial, null);

        /// <summary>
        /// Represents a converter for the inherit keyword with no value.
        /// </summary>
        public static IValueConverter Inherit = new IdentifierValueConverter<Object>(CssKeywords.Inherit, null);

        /// <summary>
        /// Represents a converter for the auto keyword with no value.
        /// </summary>
        public static IValueConverter Auto = new IdentifierValueConverter<Object>(CssKeywords.Auto, null);

        /// <summary>
        /// Represents a converter for the currentColor keyword with the transparent value.
        /// </summary>
        public static IValueConverter CurrentColor = new IdentifierValueConverter<Color>(CssKeywords.CurrentColor, Color.Transparent);

        /// <summary>
        /// Represents a length object with line-width additions.
        /// http://dev.w3.org/csswg/css-backgrounds/#line-width
        /// </summary>
        public static readonly IValueConverter LineWidthConverter = new StructValueConverter<Length>(ValueExtensions.ToBorderWidth);

        /// <summary>
        /// Represents a length object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/length
        /// </summary>
        public static readonly IValueConverter LengthConverter = new StructValueConverter<Length>(ValueExtensions.ToLength);

        /// <summary>
        /// Represents a resolution object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/resolution
        /// </summary>
        public static readonly IValueConverter ResolutionConverter = new StructValueConverter<Resolution>(ValueExtensions.ToResolution);

        /// <summary>
        /// Represents a frequency object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/frequency
        /// </summary>
        public static readonly IValueConverter FrequencyConverter = new StructValueConverter<Frequency>(ValueExtensions.ToFrequency);

        /// <summary>
        /// Represents a time object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/time
        /// </summary>
        public static readonly IValueConverter TimeConverter = new StructValueConverter<Time>(ValueExtensions.ToTime);

        /// <summary>
        /// Represents an URL object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/uri
        /// </summary>
        public static readonly IValueConverter UrlConverter = new UrlValueConverter();

        /// <summary>
        /// Represents a string object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/string
        /// </summary>
        public static readonly IValueConverter StringConverter = new StringValueConverter();

        /// <summary>
        /// Represents many string objects, but always divisible by 2 (open-close quotes).
        /// </summary>
        public static readonly IValueConverter QuotesConverter = new QuotesValueConverter();

        /// <summary>
        /// Represents a string object from many identifiers.
        /// </summary>
        public static readonly IValueConverter LiteralsConverter = new IdentifierValueConverter(ValueExtensions.IsLiteral);

        /// <summary>
        /// Represents an identifier object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/user-ident
        /// </summary>
        public static readonly IValueConverter IdentifierConverter = new IdentifierValueConverter(ValueExtensions.IsIdentifier);

        /// <summary>
        /// Represents an identifier object that matches the production rules of a single transition property.
        /// http://dev.w3.org/csswg/css-transitions/#single-transition-property
        /// </summary>
        public static readonly IValueConverter AnimatableConverter = new IdentifierValueConverter(ValueExtensions.IsAnimatableIdentifier);

        /// <summary>
        /// Represents an integer object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/integer
        /// </summary>
        public static readonly IValueConverter IntegerConverter = new StructValueConverter<Int32>(ValueExtensions.ToInteger);

        /// <summary>
        /// Represents an integer object that is zero or greater.
        /// </summary>
        public static readonly IValueConverter NaturalIntegerConverter = new StructValueConverter<Int32>(ValueExtensions.ToNaturalInteger);

        /// <summary>
        /// Represents an integer object that only allows values \in { 100, 200, ..., 900 }.
        /// </summary>
        public static readonly IValueConverter WeightIntegerConverter = new StructValueConverter<Int32>(ValueExtensions.ToWeightInteger);

        /// <summary>
        /// Represents an integer object that is greater tha zero.
        /// </summary>
        public static readonly IValueConverter PositiveIntegerConverter = new StructValueConverter<Int32>(ValueExtensions.ToPositiveInteger);

        /// <summary>
        /// Represents an integer object with 0 or 1.
        /// </summary>
        public static readonly IValueConverter BinaryConverter = new StructValueConverter<Int32>(ValueExtensions.ToBinary);

        /// <summary>
        /// Represents an angle object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/angle
        /// </summary>
        public static readonly IValueConverter AngleConverter = new StructValueConverter<Angle>(ValueExtensions.ToAngle);

        /// <summary>
        /// Represents a number object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/number
        /// </summary>
        public static readonly IValueConverter NumberConverter = new StructValueConverter<Single>(ValueExtensions.ToSingle);

        /// <summary>
        /// Represents an number object that is zero or greater.
        /// </summary>
        public static readonly IValueConverter NaturalNumberConverter = new StructValueConverter<Single>(ValueExtensions.ToNaturalSingle);

		/// <summary>
		/// Represents a percentage object.
		/// https://developer.mozilla.org/en-US/docs/Web/CSS/percentage
		/// </summary>
		public static readonly IValueConverter PercentConverter = new StructValueConverter<Percent>(ValueExtensions.ToPercent);

        /// <summary>
        /// Represents an color object (usually hex or name).
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/color
        /// </summary>
        public static readonly IValueConverter ColorConverter = new StructValueConverter<Color>(ValueExtensions.ToColor);

        /// <summary>
        /// Represents a distance object (either Length or Percent).
        /// </summary>
        public static readonly IValueConverter LengthOrPercentConverter = new StructValueConverter<Length>(ValueExtensions.ToDistance);

        /// <summary>
        /// Represents a converter for the BorderImageOutset property.
        /// </summary>
        public static readonly IValueConverter BorderImageOutsetConverter = Or(
            LengthOrPercentConverter, 
            AssignInitial(Length.Zero));

        #endregion

        #region Functions

        /// <summary>
        /// Represents a position object.
        /// http://www.w3.org/TR/css3-background/#ltpositiongt
        /// </summary>
        public static readonly IValueConverter PointConverter = Construct(() =>
        {
            var hi = Or(
                Assign(CssKeywords.Left, Length.Zero),
                Assign(CssKeywords.Right, new Length(100f, Length.Unit.Percent)),
                Assign(CssKeywords.Center, new Length(50f, Length.Unit.Percent)));
            var vi = Or(
                Assign(CssKeywords.Top, Length.Zero),
                Assign(CssKeywords.Bottom, new Length(100f, Length.Unit.Percent)),
                Assign(CssKeywords.Center, new Length(50f, Length.Unit.Percent)));
            var h = Or(hi, LengthOrPercentConverter);
            var v = Or(vi, LengthOrPercentConverter);

            return Or(
                WithOrder(hi, LengthOrPercentConverter, vi, LengthOrPercentConverter),
                WithOrder(hi, LengthOrPercentConverter, vi),
                WithOrder(hi, vi, LengthOrPercentConverter),
                WithOrder(h, v),
                WithOrder(v, h),
                LengthOrPercentConverter,
                Toggle(CssKeywords.Left, CssKeywords.Right),
                Toggle(CssKeywords.Top, CssKeywords.Bottom),
                Assign(CssKeywords.Center, Point.Center));
        });

        /// <summary>
        /// Represents an attribute retriever object.
        /// http://dev.w3.org/csswg/css-values/#funcdef-attr
        /// </summary>
        public static readonly IValueConverter AttrConverter = Func(FunctionNames.Attr, WithArgs(
            Or(StringConverter, IdentifierConverter)));

        /// <summary>
        /// Represents a steps timing-function object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/timing-function
        /// </summary>
        public static readonly IValueConverter StepsConverter = Func(FunctionNames.Steps, WithArgs(
            IntegerConverter, 
            Or(Assign(CssKeywords.Start, true), Assign(CssKeywords.End, false)).Option(false)));

        /// <summary>
        /// Represents a cubic-bezier timing-function object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/timing-function
        /// </summary>
        public static readonly IValueConverter CubicBezierConverter = Construct(() =>
        {
            var number = NumberConverter;
            return Func(FunctionNames.CubicBezier, WithArgs(number, number, number, number));
        });

        /// <summary>
        /// Represents a counter object.
        /// http://www.w3.org/TR/CSS2/syndata.html#value-def-counter
        /// </summary>
        public static readonly IValueConverter CounterConverter = Construct(() =>
        {
            var name = IdentifierConverter;
            var def = StringConverter;
            var kind = name.Option(CssKeywords.Decimal);
            return Or(
                Func(FunctionNames.Counter, WithArgs(name, kind)),
                Func(FunctionNames.Counters, WithArgs(name, def, kind)));
        });

        /// <summary>
        /// Represents a shape object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/shape
        /// </summary>
        public static readonly IValueConverter ShapeConverter = Construct(() =>
        {
            var length = LengthConverter;
            return Or(
                Func(FunctionNames.Rect, Or(
                    WithArgs(length, length, length, length),
                    WithArgs(LengthConverter.Many(4, 4)))),
                Auto);
        });

        /// <summary>
        /// Represents a linear-gradient object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/linear-gradient
        /// </summary>
        public static readonly IValueConverter LinearGradientConverter = Construct(() =>
        {
            return Or(
                new LinearGradientConverter(FunctionNames.LinearGradient, false),
                new LinearGradientConverter(FunctionNames.RepeatingLinearGradient, true));
        });

        /// <summary>
        /// Represents a radial-gradient object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/radial-gradient
        /// </summary>
        public static readonly IValueConverter RadialGradientConverter = Construct(() =>
        {            
            return Or(
                new RadialGradientConverter(FunctionNames.RadialGradient, false),
                new RadialGradientConverter(FunctionNames.RepeatingRadialGradient, true));
        });

        /// <summary>
        /// A perspective for 3D transformations.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-perspective
        /// </summary>
        public static readonly IValueConverter PerspectiveConverter = Construct(() =>
        {
            return Func(FunctionNames.Perspective, WithArgs(LengthConverter));
        });

        /// <summary>
        /// Sets the transformation matrix explicitly.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-matrix3d
        /// </summary>
        public static readonly IValueConverter MatrixTransformConverter = Construct(() =>
        {
            return Or(
                Func(FunctionNames.Matrix, WithArgs(NumberConverter, 6)),
                Func(FunctionNames.Matrix3d, WithArgs(NumberConverter, 16)));
        });

        /// <summary>
        /// A broad variety of translate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-translate3d
        /// </summary>
        public static readonly IValueConverter TranslateTransformConverter = Construct(() =>
        {
            var distance = LengthOrPercentConverter;
            var option = distance.Option(Length.Zero);
            return Or(
                Func(FunctionNames.Translate, WithArgs(distance, option)),
                Func(FunctionNames.Translate3d, WithArgs(distance, option, option)),
                Func(FunctionNames.TranslateX, WithArgs(distance)),
                Func(FunctionNames.TranslateY, WithArgs(distance)),
                Func(FunctionNames.TranslateZ, WithArgs(distance)));
        });

        /// <summary>
        /// A broad variety of scale transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-scale3d
        /// </summary>
        public static readonly IValueConverter ScaleTransformConverter = Construct(() =>
        {
            var number = NumberConverter;
            var option = number.Option(Single.NaN);
            return Or(
                Func(FunctionNames.Scale, WithArgs(number, option)),
                Func(FunctionNames.Scale3d, WithArgs(number, option, option)),
                Func(FunctionNames.ScaleX, WithArgs(number)),
                Func(FunctionNames.ScaleY, WithArgs(number)),
                Func(FunctionNames.ScaleZ, WithArgs(number)));
        });

        /// <summary>
        /// A broad variety of rotate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-rotate3d
        /// </summary>
        public static readonly IValueConverter RotateTransformConverter = Construct(() =>
        {
            var angle = AngleConverter;
            var number = NumberConverter;
            return Or(
                Func(FunctionNames.Rotate, WithArgs(angle)),
                Func(FunctionNames.Rotate3d, WithArgs(number, number, number, angle)),
                Func(FunctionNames.RotateX, WithArgs(angle)),
                Func(FunctionNames.RotateY, WithArgs(angle)),
                Func(FunctionNames.RotateZ, WithArgs(angle)));
        });

        /// <summary>
        /// A broad variety of skew transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-skew
        /// </summary>
        public static readonly IValueConverter SkewTransformConverter = Construct(() =>
        {
            var angle = AngleConverter;
            return Or(
                Func(FunctionNames.Skew, WithArgs(angle, angle)),
                Func(FunctionNames.SkewX, WithArgs(angle)),
                Func(FunctionNames.SkewY, WithArgs(angle)));
        });

        #endregion

        #region Maps

        /// <summary>
        /// Represents a converter for the default font families.
        /// </summary>
        public static readonly IValueConverter DefaultFontFamiliesConverter = Map.FontFamilies.ToConverter();

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
            Assign(CssKeywords.Infinite, Single.PositiveInfinity));

        /// <summary>
        /// Represents a positive or infinite number object.
        /// </summary>
        public static readonly IValueConverter OptionalNumberConverter = Or(
            NumberConverter, 
            None);

        /// <summary>
        /// Represents a length object or alternatively a fixed length when "normal" is given.
        /// </summary>
        public static readonly IValueConverter LengthOrNormalConverter = Or(
            LengthConverter, 
            Assign(CssKeywords.Normal, new Length(1f, Length.Unit.Em)));

        /// <summary>
        /// Represents a length object or null, when "normal" is given.
        /// </summary>
        public static readonly IValueConverter OptionalLengthConverter = Or(
            LengthConverter, 
            Assign(CssKeywords.Normal));

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
            Assign(CssKeywords.Normal));

        /// <summary>
        /// Represents a length object that is based on percentage or number.
        /// http://dev.w3.org/csswg/css-backgrounds/#border-image-slice
        /// </summary>
        public static readonly IValueConverter BorderSliceConverter = Or(
            PercentConverter, 
            NumberConverter);

        public static readonly IValueConverter BorderImageSliceConverter = WithAny(
            BorderSliceConverter.Option(new Length(100f, Length.Unit.Percent)),
            BorderSliceConverter.Option(),
            BorderSliceConverter.Option(),
            BorderSliceConverter.Option(),
            Assign(CssKeywords.Fill, true).Option(false));

        /// <summary>
        /// Represents a length object that is based on percentage, length or number.
        /// http://dev.w3.org/csswg/css-backgrounds/#border-image-width
        /// </summary>
        public static readonly IValueConverter ImageBorderWidthConverter = Or(
            LengthOrPercentConverter, 
            NumberConverter, 
            Assign(CssKeywords.Auto));

        public static readonly IValueConverter BorderImageWidthConverter = ImageBorderWidthConverter.Periodic();

        /// <summary>
        /// Represents a timing-function object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/timing-function
        /// </summary>
        public static readonly IValueConverter TransitionConverter = Or(
            Map.TimingFunctions.ToConverter(), 
            StepsConverter, 
            CubicBezierConverter);

        /// <summary>
        /// Represents a gradient object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/gradient
        /// </summary>
        public static readonly IValueConverter GradientConverter = Or(
            LinearGradientConverter, 
            RadialGradientConverter);

        /// <summary>
        /// Represents a transform function.
        /// http://www.w3.org/TR/css3-transforms/#typedef-transform-function
        /// </summary>
        public static readonly IValueConverter TransformConverter = Or(
            MatrixTransformConverter,
            ScaleTransformConverter,
            RotateTransformConverter,
            TranslateTransformConverter,
            SkewTransformConverter,
            PerspectiveConverter);

        /// <summary>
        /// Represents a color object or, alternatively, the current color.
        /// </summary>
        public static readonly IValueConverter CurrentColorConverter = Or(
            ColorConverter, 
            CurrentColor);

        /// <summary>
        /// Represents a color object, the current color, or the inverted current color.
        /// </summary>
        public static readonly IValueConverter InvertedColorConverter = Or(
            CurrentColorConverter, 
            Assign(CssKeywords.Invert));

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
		public static readonly IValueConverter StrokeMiterlimitConverter = new StructValueConverter<Single>(ValueExtensions.ToGreaterOrEqualOneSingle);

		/// <summary>
		/// Represents a ratio object.
		/// https://developer.mozilla.org/en-US/docs/Web/CSS/ratio
		/// </summary>
		public static readonly IValueConverter RatioConverter = WithOrder(
            IntegerConverter, IntegerConverter.StartsWithDelimiter());

        /// <summary>
        /// Represents multiple shadow objects.
        /// http://dev.w3.org/csswg/css-backgrounds/#shadow
        /// </summary>
        public static readonly IValueConverter MultipleShadowConverter = Or(
            new ShadowConverter().FromList(), 
            None);

        /// <summary>
        /// Represents an image source object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/image
        /// </summary>
        public static readonly IValueConverter ImageSourceConverter = Or(
            UrlConverter, 
            GradientConverter);

        /// <summary>
        /// Represents an optional image source object.
        /// </summary>
        public static readonly IValueConverter OptionalImageSourceConverter = Or(
            ImageSourceConverter, 
            None);

        /// <summary>
        /// Represents multiple image source object.
        /// </summary>
        public static readonly IValueConverter MultipleImageSourceConverter = OptionalImageSourceConverter.FromList();

        /// <summary>
        /// Represents the border-radius (h h h h / v v v v) converter.
        /// </summary>
        public static readonly IValueConverter BorderRadiusShorthandConverter = new BorderRadiusConverter();

        /// <summary>
        /// Represents the border-radius (horizontal / vertical; radius) converter.
        /// </summary>
        public static readonly IValueConverter BorderRadiusLonghandConverter = WithOrder(
            LengthOrPercentConverter, 
            LengthOrPercentConverter.Option());

        /// <summary>
        /// Represents a converter for font families.
        /// </summary>
        public static readonly IValueConverter FontFamiliesConverter = Or(
            DefaultFontFamiliesConverter, 
            StringConverter, 
            LiteralsConverter).FromList();

        /// <summary>
        /// Represents a converter for background size.
        /// </summary>
        public static readonly IValueConverter BackgroundSizeConverter = Or(
            WithOrder(AutoLengthOrPercentConverter, AutoLengthOrPercentConverter),
            AutoLengthOrPercentConverter,
            Assign(CssKeywords.Cover), 
            Assign(CssKeywords.Contain));

        /// <summary>
        /// Represents a converter for background repeat.
        /// </summary>
        public static readonly IValueConverter BackgroundRepeatsConverter = Or(
            WithOrder(BackgroundRepeatConverter, BackgroundRepeatConverter),
            BackgroundRepeatConverter,
            Assign(CssKeywords.RepeatX), 
            Assign(CssKeywords.RepeatY));

        #endregion

        #region Toggles

        /// <summary>
        /// Represents a converter for the table layout mode.
        /// </summary>
        public static readonly IValueConverter TableLayoutConverter = Toggle(CssKeywords.Fixed, CssKeywords.Auto);

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

        #region Misc

        /// <summary>
        /// Creates an or converter for the given converters.
        /// </summary>
        public static IValueConverter Or(params IValueConverter[] converters) => new OrValueConverter(converters);

        /// <summary>
        /// Creates a converter for the initial keyword with the given value.
        /// </summary>
        public static IValueConverter AssignInitial<T>(T value) => new IdentifierValueConverter<T>(CssKeywords.Initial, value);

        /// <summary>
        /// Creates a new function converter for the function name.
        /// </summary>
        public static IValueConverter Func(String name, IValueConverter args) => new FunctionValueConverter(name, args);

        /// <summary>
        /// Creates a new converter by assigning the given identifier to a fixed result.
        /// </summary>
        public static IValueConverter Assign<T>(String identifier, T result) => new IdentifierValueConverter<T>(identifier, result);

        /// <summary>
        /// Creates a new converter by assigning the given identifier to a fixed result.
        /// </summary>
        public static IValueConverter Assign(String identifier) => new IdentifierValueConverter<Object>(identifier, null);

        /// <summary>
        /// Creates a new boolean converter that toggles between the two given keywords.
        /// </summary>
        public static IValueConverter Toggle(String on, String off) => Or(Assign(on, true), Assign(off, false));

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

        /// <summary>
        /// Uses the provided converter for the whole value.
        /// </summary>
        /// <param name="converter">The converter that is used.</param>
        /// <returns>The new converter.</returns>
        public static IValueConverter Continuous(IValueConverter converter) => new ContinuousValueConverter(converter);

        #endregion

        #region Helper

        private static IValueConverter Construct(Func<IValueConverter> f)
        {
            return f();
        }

        private static IValueConverter WithArgs(IValueConverter converter, Int32 arguments)
        {
            var converters = Enumerable.Repeat(converter, arguments).ToArray();
            return WithArgs(converters);
        }

        private static IValueConverter WithArgs(IValueConverter converter)
        {
            return new ArgumentsValueConverter(converter);
        }

        private static IValueConverter WithArgs(params IValueConverter[] converters)
        {
            return new ArgumentsValueConverter(converters);
        }

        #endregion
    }
}
