namespace AngleSharp.Css
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;
    using static ValueConverters;

    public class DefaultConverterFactory : IConverterFactory
    {
        private readonly Dictionary<String, IValueConverter> _converters = new Dictionary<String, IValueConverter>(StringComparer.OrdinalIgnoreCase)
        {
            { PropertyNames.CaptionSide, Or(CaptionSideConverter, AssignInitial(true)) },
            { PropertyNames.Cursor, Or(new CursorValueConverter(), AssignInitial(SystemCursor.Auto)) },
            { PropertyNames.EmptyCells, Or(EmptyCellsConverter, AssignInitial(true)) },
            { PropertyNames.Orphans, Or(NaturalIntegerConverter, AssignInitial(2)) },
            { PropertyNames.Quotes, Or(QuotesConverter, None, AssignInitial(new[] { "«", "»" })) },
            { PropertyNames.TableLayout, Or(TableLayoutConverter, AssignInitial(false)) },
            { PropertyNames.UnicodeBidi, Or(UnicodeModeConverter, AssignInitial(UnicodeMode.Normal)) },
            { PropertyNames.Widows, Or(IntegerConverter, AssignInitial(2)) },
            { PropertyNames.Content, Or(new ContentValueConverter(), AssignInitial()) },
            { PropertyNames.BoxDecorationBreak, Or(BoxDecorationConverter, AssignInitial(false)) },
            { PropertyNames.BoxShadow, Or(MultipleShadowConverter, AssignInitial()) },
            { PropertyNames.Clear, Or(ClearModeConverter, AssignInitial(ClearMode.None)) },
            { PropertyNames.Display, Or(DisplayModeConverter, AssignInitial(DisplayMode.Inline)) },
            { PropertyNames.Float, Or(FloatingConverter, AssignInitial(Floating.None)) },
            { PropertyNames.Overflow, Or(OverflowModeConverter, AssignInitial(OverflowMode.Visible)) },
            { PropertyNames.Position, Or(PositionModeConverter, AssignInitial(PositionMode.Static)) },
            { PropertyNames.ZIndex, Or(OptionalIntegerConverter, AssignInitial()) },
            { PropertyNames.CounterReset, Or(new CounterValueConverter(0), AssignInitial()) },
            { PropertyNames.CounterIncrement, Or(new CounterValueConverter(1), AssignInitial()) },
            { PropertyNames.ObjectFit, Or(ObjectFittingConverter, AssignInitial(ObjectFitting.Fill)) },
            { PropertyNames.ObjectPosition, Or(PointConverter, AssignInitial(Point.Center)) },
            { PropertyNames.StrokeDasharray, StrokeDasharrayConverter },
            { PropertyNames.StrokeDashoffset, LengthOrPercentConverter },
            { PropertyNames.StrokeLinecap, Or(StrokeLinecapConverter, AssignInitial(StrokeLinecap.Butt)) },
            { PropertyNames.StrokeLinejoin, StrokeLinejoinConverter },
            { PropertyNames.StrokeMiterlimit, StrokeMiterlimitConverter },
            { PropertyNames.StrokeOpacity, Or(NumberConverter, AssignInitial(1f)) },
            { PropertyNames.Stroke, PaintConverter },
            { PropertyNames.StrokeWidth, LengthOrPercentConverter },
            { PropertyNames.Direction, Or(DirectionModeConverter, AssignInitial(DirectionMode.Ltr)) },
            { PropertyNames.OverflowWrap, OverflowWrapConverter },
            { PropertyNames.WordWrap, OverflowWrapConverter },
            { PropertyNames.PerspectiveOrigin, Or(PointConverter, AssignInitial(Point.Center)) },
            { PropertyNames.Perspective, Or(LengthConverter, None, AssignInitial(Length.Zero)) },
            { PropertyNames.BackfaceVisibility, Or(BackfaceVisibilityConverter, AssignInitial(true)) },
            { PropertyNames.Clip, Or(ShapeConverter, Auto, AssignInitial()) },
            { PropertyNames.Opacity, Or(NumberConverter, AssignInitial(1f)) },
            { PropertyNames.Visibility, Or(VisibilityConverter, AssignInitial(Visibility.Visible)) },
            { PropertyNames.Bottom, Or(AutoLengthOrPercentConverter, AssignInitial(Length.Auto)) },
            { PropertyNames.Height, Or(AutoLengthOrPercentConverter, AssignInitial(Length.Auto)) },
            { PropertyNames.Left, Or(AutoLengthOrPercentConverter, AssignInitial(Length.Auto)) },
            { PropertyNames.MaxHeight, Or(OptionalLengthOrPercentConverter, AssignInitial()) },
            { PropertyNames.MaxWidth, Or(OptionalLengthOrPercentConverter, AssignInitial()) },
            { PropertyNames.MinHeight, Or(LengthOrPercentConverter, AssignInitial(Length.Zero)) },
            { PropertyNames.MinWidth, Or(LengthOrPercentConverter, AssignInitial(Length.Zero)) },
            { PropertyNames.Right, Or(AutoLengthOrPercentConverter, AssignInitial(Length.Auto)) },
            { PropertyNames.Top, Or(AutoLengthOrPercentConverter, AssignInitial(Length.Auto)) },
            { PropertyNames.Width, Or(AutoLengthOrPercentConverter, AssignInitial(Length.Auto)) },
            { PropertyNames.Color, Or(ColorConverter, AssignInitial(Color.Black)) },
            { PropertyNames.WordSpacing, Or(OptionalLengthConverter, AssignInitial()) },
            { PropertyNames.LineHeight, Or(LineHeightConverter, AssignInitial(new Length(120f, Length.Unit.Percent))) },
            { PropertyNames.LetterSpacing, Or(OptionalLengthConverter, AssignInitial()) },
            { PropertyNames.FontSizeAdjust, Or(OptionalNumberConverter, AssignInitial()) },
            { PropertyNames.BreakAfter, Or(BreakModeConverter, AssignInitial(BreakMode.Auto)) },
            { PropertyNames.BreakBefore, Or(BreakModeConverter, AssignInitial(BreakMode.Auto)) },
            { PropertyNames.BreakInside, Or(BreakInsideModeConverter, AssignInitial(BreakMode.Auto)) },
            { PropertyNames.PageBreakAfter, Or(PageBreakModeConverter, AssignInitial(BreakMode.Auto)) },
            { PropertyNames.PageBreakBefore, Or(PageBreakModeConverter, AssignInitial(BreakMode.Auto)) },
            { PropertyNames.PageBreakInside, Or(PageBreakInsideModeConverter, AssignInitial(BreakMode.Auto)) },
            { PropertyNames.TransformOrigin, Or(new OriginConverter(), AssignInitial(Point.Center)) },
            { PropertyNames.Transform, Or(TransformConverter.Many(), None, AssignInitial()) },
            { PropertyNames.TransformStyle, Or(Toggle(CssKeywords.Flat, CssKeywords.Preserve3d), AssignInitial(true)) },
            { PropertyNames.ColumnCount, Or(OptionalIntegerConverter, AssignInitial()) },
            { PropertyNames.ColumnFill, Or(ColumnFillConverter, AssignInitial(true)) },
            { PropertyNames.ColumnGap, Or(LengthOrNormalConverter, AssignInitial(new Length(1f, Length.Unit.Em))) },
            { PropertyNames.ColumnSpan, Or(ColumnSpanConverter, AssignInitial(false)) },
            { PropertyNames.ColumnWidth, Or(AutoLengthConverter, AssignInitial(Length.Auto)) },
            { PropertyNames.BorderCollapse, Or(BorderCollapseConverter, AssignInitial(true)) },
            { PropertyNames.BorderSpacing, Or(LengthConverter.Many(1, 2), AssignInitial(Length.Zero)) },
            { PropertyNames.WordBreak, WordBreakConverter },
            { PropertyNames.WhiteSpace, Or(WhitespaceConverter, AssignInitial(Whitespace.Normal)) },
            { PropertyNames.VerticalAlign, Or(LengthOrPercentConverter, VerticalAlignmentConverter, AssignInitial(VerticalAlignment.Baseline)) },
            { PropertyNames.TextShadow, Or(MultipleShadowConverter, AssignInitial()) },
            { PropertyNames.TextJustify, TextJustifyConverter },
            { PropertyNames.TextIndent, Or(LengthOrPercentConverter, AssignInitial(Length.Zero)) },
            { PropertyNames.TextAnchor, TextAnchorConverter },
            { PropertyNames.TextAlign, Or(HorizontalAlignmentConverter, AssignInitial(HorizontalAlignment.Left)) },
            { PropertyNames.TextAlignLast, TextAlignLastConverter },
            { PropertyNames.TextTransform, Or(TextTransformConverter, AssignInitial(TextTransform.None)) },
            { PropertyNames.ListStyleImage, Or(OptionalImageSourceConverter, AssignInitial()) },
            { PropertyNames.ListStylePosition, Or(ListPositionConverter, AssignInitial(ListPosition.Outside)) },
            { PropertyNames.ListStyleType, Or(ListStyleConverter, AssignInitial(ListStyle.Disc)) },
            { PropertyNames.FontFamily, Or(FontFamiliesConverter, AssignInitial("Times New Roman")) },
            { PropertyNames.FontSize, Or(FontSizeConverter, AssignInitial(new Length(1f, Length.Unit.Em))) },
            { PropertyNames.FontStyle, Or(FontStyleConverter, AssignInitial(FontStyle.Normal)) },
            { PropertyNames.FontStretch, Or(FontStretchConverter, AssignInitial(FontStretch.Normal)) },
            { PropertyNames.FontVariant, Or(FontVariantConverter, AssignInitial(FontVariant.Normal)) },
            { PropertyNames.FontWeight, Or(FontWeightConverter, WeightIntegerConverter, AssignInitial(FontWeight.Normal)) },
            { PropertyNames.ColumnRuleWidth, Or(LineWidthConverter, AssignInitial(Length.Medium)) },
            { PropertyNames.ColumnRuleStyle, Or(LineStyleConverter, AssignInitial(LineStyle.None)) },
            { PropertyNames.ColumnRuleColor, Or(ColorConverter, AssignInitial(Color.Transparent)) },
            { PropertyNames.PaddingTop, Or(LengthOrPercentConverter, AssignInitial(Length.Zero)) },
            { PropertyNames.PaddingRight, Or(LengthOrPercentConverter, AssignInitial(Length.Zero)) },
            { PropertyNames.PaddingLeft, Or(LengthOrPercentConverter, AssignInitial(Length.Zero)) },
            { PropertyNames.PaddingBottom, Or(LengthOrPercentConverter, AssignInitial(Length.Zero)) },
            { PropertyNames.MarginTop, Or(AutoLengthOrPercentConverter, AssignInitial(Length.Zero)) },
            { PropertyNames.MarginRight, Or(AutoLengthOrPercentConverter, AssignInitial(Length.Zero)) },
            { PropertyNames.MarginLeft, Or(AutoLengthOrPercentConverter, AssignInitial(Length.Zero)) },
            { PropertyNames.MarginBottom, Or(AutoLengthOrPercentConverter, AssignInitial(Length.Zero)) },
            { PropertyNames.BorderTopRightRadius, Or(BorderRadiusLonghandConverter, AssignInitial(Length.Zero)) },
            { PropertyNames.BorderTopLeftRadius, Or(BorderRadiusLonghandConverter, AssignInitial(Length.Zero)) },
            { PropertyNames.BorderBottomRightRadius, Or(BorderRadiusLonghandConverter, AssignInitial(Length.Zero)) },
            { PropertyNames.BorderBottomLeftRadius, Or(BorderRadiusLonghandConverter, AssignInitial(Length.Zero)) },
            { PropertyNames.OutlineWidth, Or(LineWidthConverter, AssignInitial(Length.Medium)) },
            { PropertyNames.OutlineStyle, Or(LineStyleConverter, AssignInitial(LineStyle.None)) },
            { PropertyNames.OutlineColor, Or(InvertedColorConverter, AssignInitial(Color.Transparent)) },
            { PropertyNames.TextDecorationStyle, Or(TextDecorationStyleConverter, AssignInitial(TextDecorationStyle.Solid)) },
            { PropertyNames.TextDecorationLine, Or(TextDecorationLinesConverter, AssignInitial()) },
            { PropertyNames.TextDecorationColor, Or(ColorConverter, AssignInitial(Color.Black)) },
            { PropertyNames.TransitionTimingFunction, Or(TransitionConverter.FromList(), AssignInitial(CubicBezierTimingFunction.Ease)) },
            { PropertyNames.TransitionProperty, Or(AnimatableConverter.FromList(), None, AssignInitial(CssKeywords.All)) },
            { PropertyNames.TransitionDuration, Or(TimeConverter.FromList(), AssignInitial(Time.Zero)) },
            { PropertyNames.TransitionDelay, Or(TimeConverter.FromList(), AssignInitial(Time.Zero)) },
            { PropertyNames.BorderImageWidth, Or(BorderImageWidthConverter, AssignInitial(Length.Full)) },
            { PropertyNames.BorderImageSource, Or(OptionalImageSourceConverter, AssignInitial()) },
            { PropertyNames.BorderImageSlice, Or(BorderImageSliceConverter, AssignInitial(Length.Full)) },
            { PropertyNames.BorderImageRepeat, Or(BorderImageRepeatConverter, AssignInitial(BorderRepeat.Stretch)) },
            { PropertyNames.BorderImageOutset, Or(LengthOrPercentConverter.Periodic(), AssignInitial(Length.Zero)) },
            { PropertyNames.AnimationTimingFunction, Or(TransitionConverter.FromList(), AssignInitial(CubicBezierTimingFunction.Ease)) },
            { PropertyNames.AnimationPlayState, Or(PlayStateConverter.FromList(), AssignInitial(PlayState.Running)) },
            { PropertyNames.AnimationName, Or(IdentifierConverter.FromList(), None, AssignInitial()) },
            { PropertyNames.AnimationIterationCount, Or(PositiveOrInfiniteNumberConverter.FromList(), AssignInitial(1f)) },
            { PropertyNames.AnimationFillMode, Or(AnimationFillStyleConverter.FromList(), AssignInitial(AnimationFillStyle.None)) },
            { PropertyNames.AnimationDuration, Or(TimeConverter.FromList(), AssignInitial(Time.Zero)) },
            { PropertyNames.AnimationDirection, Or(AnimationDirectionConverter.FromList(), AssignInitial(AnimationDirection.Normal)) },
            { PropertyNames.AnimationDelay, Or(TimeConverter.FromList(), AssignInitial(Time.Zero)) },
            { PropertyNames.BackgroundSize, Or(BackgroundSizeConverter.FromList(), AssignInitial()) },
            { PropertyNames.BackgroundRepeat, Or(BackgroundRepeatsConverter.FromList(), AssignInitial(BackgroundRepeat.Repeat)) },
            { PropertyNames.BackgroundPosition, Or(PointConverter.FromList(), AssignInitial(Point.Center)) },
            { PropertyNames.BackgroundOrigin, Or(BoxModelConverter.FromList(), AssignInitial(BoxModel.PaddingBox)) },
            { PropertyNames.BackgroundImage, Or(MultipleImageSourceConverter, AssignInitial()) },
            { PropertyNames.BackgroundColor, Or(CurrentColorConverter, AssignInitial()) },
            { PropertyNames.BackgroundClip, Or(BoxModelConverter.FromList(), AssignInitial(BoxModel.BorderBox)) },
            { PropertyNames.BackgroundAttachment, Or(BackgroundAttachmentConverter.FromList(), AssignInitial(BackgroundAttachment.Scroll)) },
            { PropertyNames.BorderTopWidth, Or(LineWidthConverter, AssignInitial(Length.Medium)) },
            { PropertyNames.BorderTopStyle, Or(LineStyleConverter, AssignInitial(LineStyle.None)) },
            { PropertyNames.BorderTopColor, Or(CurrentColorConverter, AssignInitial(Color.Transparent)) },
            { PropertyNames.BorderRightWidth, Or(LineWidthConverter, AssignInitial(Length.Medium)) },
            { PropertyNames.BorderRightStyle, Or(LineStyleConverter, AssignInitial(LineStyle.None)) },
            { PropertyNames.BorderRightColor, Or(CurrentColorConverter, AssignInitial(Color.Transparent)) },
            { PropertyNames.BorderLeftWidth, Or(LineWidthConverter, AssignInitial(Length.Medium)) },
            { PropertyNames.BorderLeftStyle, Or(LineStyleConverter, AssignInitial(LineStyle.None)) },
            { PropertyNames.BorderLeftColor, Or(CurrentColorConverter, AssignInitial(Color.Transparent)) },
            { PropertyNames.BorderBottomWidth, Or(LineWidthConverter, AssignInitial(Length.Medium)) },
            { PropertyNames.BorderBottomStyle, Or(LineStyleConverter, AssignInitial(LineStyle.None)) },
            { PropertyNames.BorderBottomColor, Or(CurrentColorConverter, AssignInitial(Color.Transparent)) },
            { PropertyNames.Animation, Or(new AnimationValueConverter(), AssignInitial()) },
            { PropertyNames.Background, Or(new BackgroundValueConverter(), AssignInitial()) },
            { PropertyNames.Transition, Or(new TransitionValueConverter(), AssignInitial()) },
            { PropertyNames.TextDecoration, Or(new TextDecorationValueConverter(), AssignInitial()) },
            { PropertyNames.Outline, Or(new OutlineValueConverter(), AssignInitial()) },
            { PropertyNames.ListStyle, Or(new ListStyleValueConverter(), AssignInitial()) },
            { PropertyNames.Font, Or(new FontValueConverter(), SystemFontConverter, AssignInitial()) },
            { PropertyNames.Columns, Or(new ColumnsValueConverter(), AssignInitial()) },
            { PropertyNames.ColumnRule, Or(new ColumnRuleValueConverter(), AssignInitial()) },
            { PropertyNames.Padding, Or(LengthOrPercentConverter.Periodic(), AssignInitial(Length.Zero)) },
            { PropertyNames.Margin, Or(AutoLengthOrPercentConverter.Periodic(), AssignInitial(Length.Zero)) },
            { PropertyNames.BorderRadius, Or(new BorderRadiusValueConverter(), AssignInitial()) },
            { PropertyNames.BorderImage, Or(None, new BorderImageValueConverter(), AssignInitial()) },
            { PropertyNames.BorderWidth, Or(LineWidthConverter.Periodic(), AssignInitial()) },
            { PropertyNames.BorderTop, Or(WithAny(LineWidthConverter.Option(), LineStyleConverter.Option(), CurrentColorConverter.Option()), AssignInitial()) },
            { PropertyNames.BorderStyle, Or(LineStyleConverter.Periodic(), AssignInitial()) },
            { PropertyNames.BorderRight, Or(WithAny(LineWidthConverter.Option(), LineStyleConverter.Option(), CurrentColorConverter.Option()), AssignInitial()) },
            { PropertyNames.BorderLeft, Or(WithAny(LineWidthConverter.Option(), LineStyleConverter.Option(), CurrentColorConverter.Option()), AssignInitial()) },
            { PropertyNames.BorderColor, Or(CurrentColorConverter.Periodic(), AssignInitial()) },
            { PropertyNames.BorderBottom, Or(WithAny(LineWidthConverter.Option(), LineStyleConverter.Option(), CurrentColorConverter.Option()), AssignInitial()) },
            { PropertyNames.Border, Or(new BorderValueConverter(), AssignInitial()) },
        };

        /// <summary>
        /// Registers a new value converter for the given name.
        /// Throws an exception if another converter for the given
        /// property name is already added.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="converter">The converter to use.</param>
        public void Register(String propertyName, IValueConverter converter)
        {
            _converters.Add(propertyName, converter);
        }

        /// <summary>
        /// Unregisters an existing value converter.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The registered converter, if any.</returns>
        public IValueConverter Unregister(String propertyName)
        {
            var converter = default(IValueConverter);

            if (_converters.TryGetValue(propertyName, out converter))
            {
                _converters.Remove(propertyName);
            }

            return converter;
        }

        /// <summary>
        /// Creates a value converter for the given property.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The default (any) value converter.</returns>
        protected virtual IValueConverter CreateDefault(String propertyName)
        {
            return ValueConverters.Any;
        }

        /// <summary>
        /// Gets an existing value converter for the given property name.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The associated value converter.</returns>
        public IValueConverter Create(String propertyName)
        {
            var converter = default(IValueConverter);

            if (!String.IsNullOrEmpty(propertyName) && _converters.TryGetValue(propertyName, out converter))
            {
                return converter;
            }

            return CreateDefault(propertyName);
        }
    }
}
