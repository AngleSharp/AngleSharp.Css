namespace AngleSharp.Css
{
    using AngleSharp.Css.Declarations;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The default implementation for CSS declaration factory.
    /// </summary>
    public class DefaultDeclarationFactory : IDeclarationFactory
    {
        private readonly DeclarationInfo defaultDeclaration = new DeclarationInfo(ValueConverters.Any);
        private readonly Dictionary<String, DeclarationInfo> _declarations = new Dictionary<String, DeclarationInfo>(StringComparer.OrdinalIgnoreCase)
        {
            {
                CaptionSideDeclaration.Name, new DeclarationInfo(
                    converter: CaptionSideDeclaration.Converter,
                    flags: CaptionSideDeclaration.Flags)
            },
            {
                CursorDeclaration.Name, new DeclarationInfo(
                    converter: CursorDeclaration.Converter,
                    flags: CursorDeclaration.Flags)
            },
            {
                EmptyCellsDeclaration.Name, new DeclarationInfo(
                    converter: EmptyCellsDeclaration.Converter,
                    flags: EmptyCellsDeclaration.Flags)
            },
            {
                OrphansDeclaration.Name, new DeclarationInfo(
                    converter: OrphansDeclaration.Converter,
                    flags: OrphansDeclaration.Flags)
            },
            {
                QuotesDeclaration.Name, new DeclarationInfo(
                    converter: QuotesDeclaration.Converter,
                    flags: QuotesDeclaration.Flags)
            },
            {
                TableLayoutDeclaration.Name, new DeclarationInfo(
                    converter: TableLayoutDeclaration.Converter,
                    flags: TableLayoutDeclaration.Flags)
            },
            {
                UnicodeBidiDeclaration.Name, new DeclarationInfo(
                    converter: UnicodeBidiDeclaration.Converter,
                    flags: UnicodeBidiDeclaration.Flags)
            },
            {
                WidowsDeclaration.Name, new DeclarationInfo(
                    converter: WidowsDeclaration.Converter,
                    flags: WidowsDeclaration.Flags)
            },
            {
                ContentDeclaration.Name, new DeclarationInfo(
                    converter: ContentDeclaration.Converter,
                    flags: ContentDeclaration.Flags)
            },
            {
                BoxDecorationBreakDeclaration.Name, new DeclarationInfo(
                    converter: BoxDecorationBreakDeclaration.Converter,
                    flags: BoxDecorationBreakDeclaration.Flags)
            },
            {
                BoxShadowDeclaration.Name, new DeclarationInfo(
                    converter: BoxShadowDeclaration.Converter,
                    flags: BoxShadowDeclaration.Flags)
            },
            {
                ClearDeclaration.Name, new DeclarationInfo(
                    converter: ClearDeclaration.Converter,
                    flags: ClearDeclaration.Flags)
            },
            {
                DisplayDeclaration.Name, new DeclarationInfo(
                    converter: DisplayDeclaration.Converter,
                    flags: DisplayDeclaration.Flags)
            },
            {
                FloatDeclaration.Name, new DeclarationInfo(
                    converter: FloatDeclaration.Converter,
                    flags: FloatDeclaration.Flags)
            },
            {
                OverflowDeclaration.Name, new DeclarationInfo(
                    converter: OverflowDeclaration.Converter,
                    flags: OverflowDeclaration.Flags)
            },
            {
                PositionDeclaration.Name, new DeclarationInfo(
                    converter: PositionDeclaration.Converter,
                    flags: PositionDeclaration.Flags)
            },
            {
                ZIndexDeclaration.Name, new DeclarationInfo(
                    converter: ZIndexDeclaration.Converter,
                    flags: ZIndexDeclaration.Flags)
            },
            {
                CounterResetDeclaration.Name, new DeclarationInfo(
                    converter: CounterResetDeclaration.Converter,
                    flags: CounterResetDeclaration.Flags)
            },
            {
                CounterIncrementDeclaration.Name, new DeclarationInfo(
                    converter: CounterIncrementDeclaration.Converter,
                    flags: CounterIncrementDeclaration.Flags)
            },
            {
                ObjectFitDeclaration.Name, new DeclarationInfo(
                    converter: ObjectFitDeclaration.Converter,
                    flags: ObjectFitDeclaration.Flags)
            },
            {
                ObjectPositionDeclaration.Name, new DeclarationInfo(
                    converter: ObjectPositionDeclaration.Converter,
                    flags: ObjectPositionDeclaration.Flags)
            },
            {
                StrokeDasharrayDeclaration.Name, new DeclarationInfo(
                    converter: StrokeDasharrayDeclaration.Converter,
                    flags: StrokeDasharrayDeclaration.Flags)
            },
            {
                StrokeDashoffsetDeclaration.Name, new DeclarationInfo(
                    converter: StrokeDashoffsetDeclaration.Converter,
                    flags: StrokeDashoffsetDeclaration.Flags)
            },
            {
                StrokeLinecapDeclaration.Name, new DeclarationInfo(
                    converter: StrokeLinecapDeclaration.Converter,
                    flags: StrokeLinecapDeclaration.Flags)
            },
            {
                StrokeLinejoinDeclaration.Name, new DeclarationInfo(
                    converter: StrokeLinejoinDeclaration.Converter,
                    flags: StrokeLinejoinDeclaration.Flags)
            },
            {
                StrokeMiterlimitDeclaration.Name, new DeclarationInfo(
                    converter: StrokeMiterlimitDeclaration.Converter,
                    flags: StrokeMiterlimitDeclaration.Flags)
            },
            {
                StrokeOpacityDeclaration.Name, new DeclarationInfo(
                    converter: StrokeOpacityDeclaration.Converter,
                    flags: StrokeOpacityDeclaration.Flags)
            },
            {
                StrokeDeclaration.Name, new DeclarationInfo(
                    converter: StrokeDeclaration.Converter,
                    flags: StrokeDeclaration.Flags)
            },
            {
                StrokeWidthDeclaration.Name, new DeclarationInfo(
                    converter: StrokeWidthDeclaration.Converter,
                    flags: StrokeWidthDeclaration.Flags)
            },
            {
                DirectionDeclaration.Name, new DeclarationInfo(
                    converter: DirectionDeclaration.Converter,
                    flags: DirectionDeclaration.Flags)
            },
            {
                OverflowWrapDeclaration.Name, new DeclarationInfo(
                    converter: OverflowWrapDeclaration.Converter,
                    flags: OverflowWrapDeclaration.Flags)
            },
            {
                WordWrapDeclaration.Name, new DeclarationInfo(
                    converter: WordWrapDeclaration.Converter,
                    flags: WordWrapDeclaration.Flags)
            },
            {
                PerspectiveOriginDeclaration.Name, new DeclarationInfo(
                    converter: PerspectiveOriginDeclaration.Converter,
                    flags: PerspectiveOriginDeclaration.Flags)
            },
            {
                PerspectiveDeclaration.Name, new DeclarationInfo(
                    converter: PerspectiveDeclaration.Converter,
                    flags: PerspectiveDeclaration.Flags)
            },
            {
                BackfaceVisibilityDeclaration.Name, new DeclarationInfo(
                    converter: BackfaceVisibilityDeclaration.Converter,
                    flags: BackfaceVisibilityDeclaration.Flags)
            },
            {
                ClipDeclaration.Name, new DeclarationInfo(
                    converter: ClipDeclaration.Converter,
                    flags: ClipDeclaration.Flags)
            },
            {
                OpacityDeclaration.Name, new DeclarationInfo(
                    converter: OpacityDeclaration.Converter,
                    flags: OpacityDeclaration.Flags)
            },
            {
                VisibilityDeclaration.Name, new DeclarationInfo(
                    converter: VisibilityDeclaration.Converter,
                    flags: VisibilityDeclaration.Flags)
            },
            {
                BottomDeclaration.Name, new DeclarationInfo(
                    converter: BottomDeclaration.Converter,
                    flags: BottomDeclaration.Flags)
            },
            {
                HeightDeclaration.Name, new DeclarationInfo(
                    converter: HeightDeclaration.Converter,
                    flags: HeightDeclaration.Flags)
            },
            {
                LeftDeclaration.Name, new DeclarationInfo(
                    converter: LeftDeclaration.Converter,
                    flags: LeftDeclaration.Flags)
            },
            {
                MaxHeightDeclaration.Name, new DeclarationInfo(
                    converter: MaxHeightDeclaration.Converter,
                    flags: MaxHeightDeclaration.Flags)
            },
            {
                MaxWidthDeclaration.Name, new DeclarationInfo(
                    converter: MaxWidthDeclaration.Converter,
                    flags: MaxWidthDeclaration.Flags)
            },
            {
                MinHeightDeclaration.Name, new DeclarationInfo(
                    converter: MinHeightDeclaration.Converter,
                    flags: MinHeightDeclaration.Flags)
            },
            {
                MinWidthDeclaration.Name, new DeclarationInfo(
                    converter: MinWidthDeclaration.Converter,
                    flags: MinWidthDeclaration.Flags)
            },
            {
                RightDeclaration.Name, new DeclarationInfo(
                    converter: RightDeclaration.Converter,
                    flags: RightDeclaration.Flags)
            },
            {
                TopDeclaration.Name, new DeclarationInfo(
                    converter: TopDeclaration.Converter,
                    flags: TopDeclaration.Flags)
            },
            {
                WidthDeclaration.Name, new DeclarationInfo(
                    converter: WidthDeclaration.Converter,
                    flags: WidthDeclaration.Flags)
            },
            {
                ColorDeclaration.Name, new DeclarationInfo(
                    converter: ColorDeclaration.Converter,
                    flags: ColorDeclaration.Flags)
            },
            {
                WordSpacingDeclaration.Name, new DeclarationInfo(
                    converter: WordSpacingDeclaration.Converter,
                    flags: WordSpacingDeclaration.Flags)
            },
            {
                LineHeightDeclaration.Name, new DeclarationInfo(
                    converter: LineHeightDeclaration.Converter,
                    flags: LineHeightDeclaration.Flags,
                    shorthands: LineHeightDeclaration.Shorthands)
            },
            {
                LetterSpacingDeclaration.Name, new DeclarationInfo(
                    converter: LetterSpacingDeclaration.Converter,
                    flags: LetterSpacingDeclaration.Flags)
            },
            {
                FontSizeAdjustDeclaration.Name, new DeclarationInfo(
                    converter: FontSizeAdjustDeclaration.Converter,
                    flags: FontSizeAdjustDeclaration.Flags)
            },
            {
                BreakAfterDeclaration.Name, new DeclarationInfo(
                    converter: BreakAfterDeclaration.Converter,
                    flags: BreakAfterDeclaration.Flags)
            },
            {
                BreakBeforeDeclaration.Name, new DeclarationInfo(
                    converter: BreakBeforeDeclaration.Converter,
                    flags: BreakBeforeDeclaration.Flags)
            },
            {
                BreakInsideDeclaration.Name, new DeclarationInfo(
                    converter: BreakInsideDeclaration.Converter,
                    flags: BreakInsideDeclaration.Flags)
            },
            {
                PageBreakAfterDeclaration.Name, new DeclarationInfo(
                    converter: PageBreakAfterDeclaration.Converter,
                    flags: PageBreakAfterDeclaration.Flags)
            },
            {
                PageBreakBeforeDeclaration.Name, new DeclarationInfo(
                    converter: PageBreakBeforeDeclaration.Converter,
                    flags: PageBreakBeforeDeclaration.Flags)
            },
            {
                PageBreakInsideDeclaration.Name, new DeclarationInfo(
                    converter: PageBreakInsideDeclaration.Converter,
                    flags: PageBreakInsideDeclaration.Flags)
            },
            {
                TransformOriginDeclaration.Name, new DeclarationInfo(
                    converter: TransformOriginDeclaration.Converter,
                    flags: TransformOriginDeclaration.Flags)
            },
            {
                TransformDeclaration.Name, new DeclarationInfo(
                    converter: TransformDeclaration.Converter,
                    flags: TransformDeclaration.Flags)
            },
            {
                TransformStyleDeclaration.Name, new DeclarationInfo(
                    converter: TransformStyleDeclaration.Converter,
                    flags: TransformStyleDeclaration.Flags)
            },
            {
                ColumnCountDeclaration.Name, new DeclarationInfo(
                    converter: ColumnCountDeclaration.Converter,
                    flags: ColumnCountDeclaration.Flags)
            },
            {
                ColumnFillDeclaration.Name, new DeclarationInfo(
                    converter: ColumnFillDeclaration.Converter,
                    flags: ColumnFillDeclaration.Flags)
            },
            {
                ColumnGapDeclaration.Name, new DeclarationInfo(
                    converter: ColumnGapDeclaration.Converter,
                    flags: ColumnGapDeclaration.Flags)
            },
            {
                ColumnSpanDeclaration.Name, new DeclarationInfo(
                    converter: ColumnSpanDeclaration.Converter,
                    flags: ColumnSpanDeclaration.Flags,
                    shorthands: ColumnSpanDeclaration.Shorthands)
            },
            {
                ColumnWidthDeclaration.Name, new DeclarationInfo(
                    converter: ColumnWidthDeclaration.Converter,
                    flags: ColumnWidthDeclaration.Flags,
                    shorthands: ColumnWidthDeclaration.Shorthands)
            },
            {
                BorderCollapseDeclaration.Name, new DeclarationInfo(
                    converter: BorderCollapseDeclaration.Converter,
                    flags: BorderCollapseDeclaration.Flags)
            },
            {
                BorderSpacingDeclaration.Name, new DeclarationInfo(
                    converter: BorderSpacingDeclaration.Converter,
                    flags: BorderSpacingDeclaration.Flags)
            },
            {
                WordBreakDeclaration.Name, new DeclarationInfo(
                    converter: WordBreakDeclaration.Converter,
                    flags: WordBreakDeclaration.Flags)
            },
            {
                WhiteSpaceDeclaration.Name, new DeclarationInfo(
                    converter: WhiteSpaceDeclaration.Converter,
                    flags: WhiteSpaceDeclaration.Flags)
            },
            {
                VerticalAlignDeclaration.Name, new DeclarationInfo(
                    converter: VerticalAlignDeclaration.Converter,
                    flags: VerticalAlignDeclaration.Flags)
            },
            {
                TextShadowDeclaration.Name, new DeclarationInfo(
                    converter: TextShadowDeclaration.Converter,
                    flags: TextShadowDeclaration.Flags)
            },
            {
                TextJustifyDeclaration.Name, new DeclarationInfo(
                    converter: TextJustifyDeclaration.Converter,
                    flags: TextJustifyDeclaration.Flags)
            },
            {
                TextIndentDeclaration.Name, new DeclarationInfo(
                    converter: TextIndentDeclaration.Converter,
                    flags: TextIndentDeclaration.Flags)
            },
            {
                TextAnchorDeclaration.Name, new DeclarationInfo(
                    converter: TextAnchorDeclaration.Converter,
                    flags: TextAnchorDeclaration.Flags)
            },
            {
                TextAlignDeclaration.Name, new DeclarationInfo(
                    converter: TextAlignDeclaration.Converter,
                    flags: TextAlignDeclaration.Flags)
            },
            {
                TextAlignLastDeclaration.Name, new DeclarationInfo(
                    converter: TextAlignLastDeclaration.Converter,
                    flags: TextAlignLastDeclaration.Flags)
            },
            {
                TextTransformDeclaration.Name, new DeclarationInfo(
                    converter: TextTransformDeclaration.Converter,
                    flags: TextTransformDeclaration.Flags)
            },
            {
                ListStyleImageDeclaration.Name, new DeclarationInfo(
                    converter: ListStyleImageDeclaration.Converter,
                    flags: ListStyleImageDeclaration.Flags,
                    shorthands: ListStyleImageDeclaration.Shorthands)
            },
            {
                ListStylePositionDeclaration.Name, new DeclarationInfo(
                    converter: ListStylePositionDeclaration.Converter,
                    flags: ListStylePositionDeclaration.Flags,
                    shorthands: ListStylePositionDeclaration.Shorthands)
            },
            {
                ListStyleTypeDeclaration.Name, new DeclarationInfo(
                    converter: ListStyleTypeDeclaration.Converter,
                    flags: ListStyleTypeDeclaration.Flags,
                    shorthands: ListStyleTypeDeclaration.Shorthands)
            },
            {
                FontFamilyDeclaration.Name, new DeclarationInfo(
                    converter: FontFamilyDeclaration.Converter,
                    flags: FontFamilyDeclaration.Flags,
                    shorthands: FontFamilyDeclaration.Shorthands)
            },
            {
                FontSizeDeclaration.Name, new DeclarationInfo(
                    converter: FontSizeDeclaration.Converter,
                    flags: FontSizeDeclaration.Flags,
                    shorthands: FontSizeDeclaration.Shorthands)
            },
            {
                FontStyleDeclaration.Name, new DeclarationInfo(
                    converter: FontStyleDeclaration.Converter,
                    flags: FontStyleDeclaration.Flags,
                    shorthands: FontStyleDeclaration.Shorthands)
            },
            {
                FontStretchDeclaration.Name, new DeclarationInfo(
                    converter: FontStretchDeclaration.Converter,
                    flags: FontStretchDeclaration.Flags)
            },
            {
                FontVariantDeclaration.Name, new DeclarationInfo(
                    converter: FontVariantDeclaration.Converter,
                    flags: FontVariantDeclaration.Flags,
                    shorthands: FontVariantDeclaration.Shorthands)
            },
            {
                FontWeightDeclaration.Name, new DeclarationInfo(
                    converter: FontWeightDeclaration.Converter,
                    flags: FontWeightDeclaration.Flags,
                    shorthands: FontWeightDeclaration.Shorthands)
            },
            {
                ColumnRuleWidthDeclaration.Name, new DeclarationInfo(
                    converter: ColumnRuleWidthDeclaration.Converter,
                    flags: ColumnRuleWidthDeclaration.Flags,
                    shorthands: ColumnRuleWidthDeclaration.Shorthands)
            },
            {
                ColumnRuleStyleDeclaration.Name, new DeclarationInfo(
                    converter: ColumnRuleStyleDeclaration.Converter,
                    flags: ColumnRuleStyleDeclaration.Flags,
                    shorthands: ColumnRuleStyleDeclaration.Shorthands)
            },
            {
                ColumnRuleColorDeclaration.Name, new DeclarationInfo(
                    converter: ColumnRuleColorDeclaration.Converter,
                    flags: ColumnRuleColorDeclaration.Flags,
                    shorthands: ColumnRuleColorDeclaration.Shorthands)
            },
            {
                PaddingTopDeclaration.Name, new DeclarationInfo(
                    converter: PaddingTopDeclaration.Converter,
                    flags: PaddingTopDeclaration.Flags,
                    shorthands: PaddingTopDeclaration.Shorthands)
            },
            {
                PaddingRightDeclaration.Name, new DeclarationInfo(
                    converter: PaddingRightDeclaration.Converter,
                    flags: PaddingRightDeclaration.Flags,
                    shorthands: PaddingRightDeclaration.Shorthands)
            },
            {
                PaddingLeftDeclaration.Name, new DeclarationInfo(
                    converter: PaddingLeftDeclaration.Converter,
                    flags: PaddingLeftDeclaration.Flags,
                    shorthands: PaddingLeftDeclaration.Shorthands)
            },
            {
                PaddingBottomDeclaration.Name, new DeclarationInfo(
                    converter: PaddingBottomDeclaration.Converter,
                    flags: PaddingBottomDeclaration.Flags,
                    shorthands: PaddingBottomDeclaration.Shorthands)
            },
            {
                MarginTopDeclaration.Name, new DeclarationInfo(
                    converter: MarginTopDeclaration.Converter,
                    flags: MarginTopDeclaration.Flags,
                    shorthands: MarginTopDeclaration.Shorthands)
            },
            {
                MarginRightDeclaration.Name, new DeclarationInfo(
                    converter: MarginRightDeclaration.Converter,
                    flags: MarginRightDeclaration.Flags,
                    shorthands: MarginRightDeclaration.Shorthands)
            },
            {
                MarginLeftDeclaration.Name, new DeclarationInfo(
                    converter: MarginLeftDeclaration.Converter,
                    flags: MarginLeftDeclaration.Flags,
                    shorthands: MarginLeftDeclaration.Shorthands)
            },
            {
                MarginBottomDeclaration.Name, new DeclarationInfo(
                    converter: MarginBottomDeclaration.Converter,
                    flags: MarginBottomDeclaration.Flags,
                    shorthands: MarginBottomDeclaration.Shorthands)
            },
            {
                BorderTopRightRadiusDeclaration.Name, new DeclarationInfo(
                    converter: BorderTopRightRadiusDeclaration.Converter,
                    flags: BorderTopRightRadiusDeclaration.Flags,
                    shorthands: BorderTopRightRadiusDeclaration.Shorthands)
            },
            {
                BorderTopLeftRadiusDeclaration.Name, new DeclarationInfo(
                    converter: BorderTopLeftRadiusDeclaration.Converter,
                    flags: BorderTopLeftRadiusDeclaration.Flags,
                    shorthands: BorderTopLeftRadiusDeclaration.Shorthands)
            },
            {
                BorderBottomRightRadiusDeclaration.Name, new DeclarationInfo(
                    converter: BorderBottomRightRadiusDeclaration.Converter,
                    flags: BorderBottomRightRadiusDeclaration.Flags,
                    shorthands: BorderBottomRightRadiusDeclaration.Shorthands)
            },
            {
                BorderBottomLeftRadiusDeclaration.Name, new DeclarationInfo(
                    converter: BorderBottomLeftRadiusDeclaration.Converter,
                    flags: BorderBottomLeftRadiusDeclaration.Flags,
                    shorthands: BorderBottomLeftRadiusDeclaration.Shorthands)
            },
            {
                OutlineWidthDeclaration.Name, new DeclarationInfo(
                    converter: OutlineWidthDeclaration.Converter,
                    flags: OutlineWidthDeclaration.Flags,
                    shorthands: OutlineWidthDeclaration.Shorthands)
            },
            {
                OutlineStyleDeclaration.Name, new DeclarationInfo(
                    converter: OutlineStyleDeclaration.Converter,
                    flags: OutlineStyleDeclaration.Flags,
                    shorthands: OutlineStyleDeclaration.Shorthands)
            },
            {
                OutlineColorDeclaration.Name, new DeclarationInfo(
                    converter: OutlineColorDeclaration.Converter,
                    flags: OutlineColorDeclaration.Flags,
                    shorthands: OutlineColorDeclaration.Shorthands)
            },
            {
                TextDecorationStyleDeclaration.Name, new DeclarationInfo(
                    converter: TextDecorationStyleDeclaration.Converter,
                    flags: TextDecorationStyleDeclaration.Flags,
                    shorthands: TextDecorationStyleDeclaration.Shorthands)
            },
            {
                TextDecorationLineDeclaration.Name, new DeclarationInfo(
                    converter: TextDecorationLineDeclaration.Converter,
                    flags: TextDecorationLineDeclaration.Flags,
                    shorthands: TextDecorationLineDeclaration.Shorthands)
            },
            {
                TextDecorationColorDeclaration.Name, new DeclarationInfo(
                    converter: TextDecorationColorDeclaration.Converter,
                    flags: TextDecorationColorDeclaration.Flags,
                    shorthands: TextDecorationColorDeclaration.Shorthands)
            },
            {
                TransitionTimingFunctionDeclaration.Name, new DeclarationInfo(
                    converter: TransitionTimingFunctionDeclaration.Converter,
                    flags: TransitionTimingFunctionDeclaration.Flags,
                    shorthands: TransitionTimingFunctionDeclaration.Shorthands)
            },
            {
                TransitionPropertyDeclaration.Name, new DeclarationInfo(
                    converter: TransitionPropertyDeclaration.Converter,
                    flags: TransitionPropertyDeclaration.Flags,
                    shorthands: TransitionPropertyDeclaration.Shorthands)
            },
            {
                TransitionDurationDeclaration.Name, new DeclarationInfo(
                    converter: TransitionDurationDeclaration.Converter,
                    flags: TransitionDurationDeclaration.Flags,
                    shorthands: TransitionDurationDeclaration.Shorthands)
            },
            {
                TransitionDelayDeclaration.Name, new DeclarationInfo(
                    converter: TransitionDelayDeclaration.Converter,
                    flags: TransitionDelayDeclaration.Flags,
                    shorthands: TransitionDelayDeclaration.Shorthands)
            },
            {
                BorderImageWidthDeclaration.Name, new DeclarationInfo(
                    converter: BorderImageWidthDeclaration.Converter,
                    flags: BorderImageWidthDeclaration.Flags,
                    shorthands: BorderImageWidthDeclaration.Shorthands)
            },
            {
                BorderImageSourceDeclaration.Name, new DeclarationInfo(
                    converter: BorderImageSourceDeclaration.Converter,
                    flags: BorderImageSourceDeclaration.Flags,
                    shorthands: BorderImageSourceDeclaration.Shorthands)
            },
            {
                BorderImageSliceDeclaration.Name, new DeclarationInfo(
                    converter: BorderImageSliceDeclaration.Converter,
                    flags: BorderImageSliceDeclaration.Flags,
                    shorthands: BorderImageSliceDeclaration.Shorthands)
            },
            {
                BorderImageRepeatDeclaration.Name, new DeclarationInfo(
                    converter: BorderImageRepeatDeclaration.Converter,
                    flags: BorderImageRepeatDeclaration.Flags,
                    shorthands: BorderImageRepeatDeclaration.Shorthands)
            },
            {
                BorderImageOutsetDeclaration.Name, new DeclarationInfo(
                    converter: BorderImageOutsetDeclaration.Converter,
                    flags: BorderImageOutsetDeclaration.Flags,
                    shorthands: BorderImageOutsetDeclaration.Shorthands)
            },
            {
                AnimationTimingFunctionDeclaration.Name, new DeclarationInfo(
                    converter: AnimationTimingFunctionDeclaration.Converter,
                    flags: AnimationTimingFunctionDeclaration.Flags,
                    shorthands: AnimationTimingFunctionDeclaration.Shorthands)
            },
            {
                AnimationPlayStateDeclaration.Name, new DeclarationInfo(
                    converter: AnimationPlayStateDeclaration.Converter,
                    flags: AnimationPlayStateDeclaration.Flags,
                    shorthands: AnimationPlayStateDeclaration.Shorthands)
            },
            {
                AnimationNameDeclaration.Name, new DeclarationInfo(
                    converter: AnimationNameDeclaration.Converter,
                    flags: AnimationNameDeclaration.Flags,
                    shorthands: AnimationNameDeclaration.Shorthands)
            },
            {
                AnimationIterationCountDeclaration.Name, new DeclarationInfo(
                    converter: AnimationIterationCountDeclaration.Converter,
                    flags: AnimationIterationCountDeclaration.Flags,
                    shorthands: AnimationIterationCountDeclaration.Shorthands)
            },
            {
                AnimationFillModeDeclaration.Name, new DeclarationInfo(
                    converter: AnimationFillModeDeclaration.Converter,
                    flags: AnimationFillModeDeclaration.Flags,
                    shorthands: AnimationFillModeDeclaration.Shorthands)
            },
            {
                AnimationDurationDeclaration.Name, new DeclarationInfo(
                    converter: AnimationDurationDeclaration.Converter,
                    flags: AnimationDurationDeclaration.Flags,
                    shorthands: AnimationDurationDeclaration.Shorthands)
            },
            {
                AnimationDirectionDeclaration.Name, new DeclarationInfo(
                    converter: AnimationDirectionDeclaration.Converter,
                    flags: AnimationDirectionDeclaration.Flags,
                    shorthands: AnimationDirectionDeclaration.Shorthands)
            },
            {
                AnimationDelayDeclaration.Name, new DeclarationInfo(
                    converter: AnimationDelayDeclaration.Converter,
                    flags: AnimationDelayDeclaration.Flags,
                    shorthands: AnimationDelayDeclaration.Shorthands)
            },
            {
                BackgroundSizeDeclaration.Name, new DeclarationInfo(
                    converter: BackgroundSizeDeclaration.Converter,
                    flags: BackgroundSizeDeclaration.Flags,
                    shorthands: BackgroundSizeDeclaration.Shorthands)
            },
            {
                BackgroundRepeatDeclaration.Name, new DeclarationInfo(
                    converter: BackgroundRepeatDeclaration.Converter,
                    flags: BackgroundRepeatDeclaration.Flags,
                    shorthands: BackgroundRepeatDeclaration.Shorthands)
            },
            {
                BackgroundPositionDeclaration.Name, new DeclarationInfo(
                    converter: BackgroundPositionDeclaration.Converter,
                    flags: BackgroundPositionDeclaration.Flags,
                    shorthands: BackgroundPositionDeclaration.Shorthands)
            },
            {
                BackgroundOriginDeclaration.Name, new DeclarationInfo(
                    converter: BackgroundOriginDeclaration.Converter,
                    flags: BackgroundOriginDeclaration.Flags,
                    shorthands: BackgroundOriginDeclaration.Shorthands)
            },
            {
                BackgroundImageDeclaration.Name, new DeclarationInfo(
                    converter: BackgroundImageDeclaration.Converter,
                    flags: BackgroundImageDeclaration.Flags,
                    shorthands: BackgroundImageDeclaration.Shorthands)
            },
            {
                BackgroundColorDeclaration.Name, new DeclarationInfo(
                    converter: BackgroundColorDeclaration.Converter,
                    flags: BackgroundColorDeclaration.Flags,
                    shorthands: BackgroundColorDeclaration.Shorthands)
            },
            {
                BackgroundClipDeclaration.Name, new DeclarationInfo(
                    converter: BackgroundClipDeclaration.Converter,
                    flags: BackgroundClipDeclaration.Flags,
                    shorthands: BackgroundClipDeclaration.Shorthands)
            },
            {
                BackgroundAttachmentDeclaration.Name, new DeclarationInfo(
                    converter: BackgroundAttachmentDeclaration.Converter,
                    flags: BackgroundAttachmentDeclaration.Flags,
                    shorthands: BackgroundAttachmentDeclaration.Shorthands)
            },
            {
                BorderTopWidthDeclaration.Name, new DeclarationInfo(
                    converter: BorderTopWidthDeclaration.Converter,
                    flags: BorderTopWidthDeclaration.Flags,
                    shorthands: BorderTopWidthDeclaration.Shorthands)
            },
            {
                BorderTopStyleDeclaration.Name, new DeclarationInfo(
                    converter: BorderTopStyleDeclaration.Converter,
                    flags: BorderTopStyleDeclaration.Flags,
                    shorthands: BorderTopStyleDeclaration.Shorthands)
            },
            {
                BorderTopColorDeclaration.Name, new DeclarationInfo(
                    converter: BorderTopColorDeclaration.Converter,
                    flags: BorderTopColorDeclaration.Flags,
                    shorthands: BorderTopColorDeclaration.Shorthands)
            },
            {
                BorderRightWidthDeclaration.Name, new DeclarationInfo(
                    converter: BorderRightWidthDeclaration.Converter,
                    flags: BorderRightWidthDeclaration.Flags,
                    shorthands: BorderRightWidthDeclaration.Shorthands)
            },
            {
                BorderRightStyleDeclaration.Name, new DeclarationInfo(
                    converter: BorderRightStyleDeclaration.Converter,
                    flags: BorderRightStyleDeclaration.Flags,
                    shorthands: BorderRightStyleDeclaration.Shorthands)
            },
            {
                BorderRightColorDeclaration.Name, new DeclarationInfo(
                    converter: BorderRightColorDeclaration.Converter,
                    flags: BorderRightColorDeclaration.Flags,
                    shorthands: BorderRightColorDeclaration.Shorthands)
            },
            {
                BorderLeftWidthDeclaration.Name, new DeclarationInfo(
                    converter: BorderLeftWidthDeclaration.Converter,
                    flags: BorderLeftWidthDeclaration.Flags,
                    shorthands: BorderLeftWidthDeclaration.Shorthands)
            },
            {
                BorderLeftStyleDeclaration.Name, new DeclarationInfo(
                    converter: BorderLeftStyleDeclaration.Converter,
                    flags: BorderLeftStyleDeclaration.Flags,
                    shorthands: BorderLeftStyleDeclaration.Shorthands)
            },
            {
                BorderLeftColorDeclaration.Name, new DeclarationInfo(
                    converter: BorderLeftColorDeclaration.Converter,
                    flags: BorderLeftColorDeclaration.Flags,
                    shorthands: BorderLeftColorDeclaration.Shorthands)
            },
            {
                BorderBottomWidthDeclaration.Name, new DeclarationInfo(
                    converter: BorderBottomWidthDeclaration.Converter,
                    flags: BorderBottomWidthDeclaration.Flags,
                    shorthands: BorderBottomWidthDeclaration.Shorthands)
            },
            {
                BorderBottomStyleDeclaration.Name, new DeclarationInfo(
                    converter: BorderBottomStyleDeclaration.Converter,
                    flags: BorderBottomStyleDeclaration.Flags,
                    shorthands: BorderBottomStyleDeclaration.Shorthands)
            },
            {
                BorderBottomColorDeclaration.Name, new DeclarationInfo(
                    converter: BorderBottomColorDeclaration.Converter,
                    flags: BorderBottomColorDeclaration.Flags,
                    shorthands: BorderBottomColorDeclaration.Shorthands)
            },
            {
                AnimationDeclaration.Name, new DeclarationInfo(
                    converter: AnimationDeclaration.Converter,
                    aggregator: AnimationDeclaration.Aggregator,
                    flags: AnimationDeclaration.Flags,
                    longhands: AnimationDeclaration.Longhands)
            },
            {
                BackgroundDeclaration.Name, new DeclarationInfo(
                    converter: BackgroundDeclaration.Converter,
                    aggregator: BackgroundDeclaration.Aggregator,
                    flags: BackgroundDeclaration.Flags,
                    longhands: BackgroundDeclaration.Longhands)
            },
            {
                TransitionDeclaration.Name, new DeclarationInfo(
                    converter: TransitionDeclaration.Converter,
                    aggregator: TransitionDeclaration.Aggregator,
                    flags: TransitionDeclaration.Flags,
                    longhands: TransitionDeclaration.Longhands)
            },
            {
                TextDecorationDeclaration.Name, new DeclarationInfo(
                    converter: TextDecorationDeclaration.Converter,
                    aggregator: TextDecorationDeclaration.Aggregator,
                    flags: TextDecorationDeclaration.Flags,
                    longhands: TextDecorationDeclaration.Longhands)
            },
            {
                OutlineDeclaration.Name, new DeclarationInfo(
                    converter: OutlineDeclaration.Converter,
                    aggregator: OutlineDeclaration.Aggregator,
                    flags: OutlineDeclaration.Flags,
                    longhands: OutlineDeclaration.Longhands)
            },
            {
                ListStyleDeclaration.Name, new DeclarationInfo(
                    converter: ListStyleDeclaration.Converter,
                    aggregator: ListStyleDeclaration.Aggregator,
                    flags: ListStyleDeclaration.Flags,
                    longhands: ListStyleDeclaration.Longhands)
            },
            {
                FontDeclaration.Name, new DeclarationInfo(
                    converter: FontDeclaration.Converter,
                    aggregator: FontDeclaration.Aggregator,
                    flags: FontDeclaration.Flags,
                    longhands: FontDeclaration.Longhands)
            },
            {
                ColumnsDeclaration.Name, new DeclarationInfo(
                    converter: ColumnsDeclaration.Converter,
                    aggregator: ColumnsDeclaration.Aggregator,
                    flags: ColumnsDeclaration.Flags,
                    longhands: ColumnsDeclaration.Longhands)
            },
            {
                ColumnRuleDeclaration.Name, new DeclarationInfo(
                    converter: ColumnRuleDeclaration.Converter,
                    aggregator: ColumnRuleDeclaration.Aggregator,
                    flags: ColumnRuleDeclaration.Flags,
                    longhands: ColumnRuleDeclaration.Longhands)
            },
            {
                PaddingDeclaration.Name, new DeclarationInfo(
                    converter: PaddingDeclaration.Converter,
                    aggregator: PaddingDeclaration.Aggregator,
                    flags: PaddingDeclaration.Flags,
                    longhands: PaddingDeclaration.Longhands)
            },
            {
                MarginDeclaration.Name, new DeclarationInfo(
                    converter: MarginDeclaration.Converter,
                    aggregator: MarginDeclaration.Aggregator,
                    flags: MarginDeclaration.Flags,
                    longhands: MarginDeclaration.Longhands)
            },
            {
                BorderRadiusDeclaration.Name, new DeclarationInfo(
                    converter: BorderRadiusDeclaration.Converter,
                    aggregator: BorderRadiusDeclaration.Aggregator,
                    flags: BorderRadiusDeclaration.Flags,
                    longhands: BorderRadiusDeclaration.Longhands)
            },
            {
                BorderImageDeclaration.Name, new DeclarationInfo(
                    converter: BorderImageDeclaration.Converter,
                    aggregator: BorderImageDeclaration.Aggregator,
                    flags: BorderImageDeclaration.Flags,
                    longhands: BorderImageDeclaration.Longhands)
            },
            {
                BorderWidthDeclaration.Name, new DeclarationInfo(
                    converter: BorderWidthDeclaration.Converter,
                    aggregator: BorderWidthDeclaration.Aggregator,
                    flags: BorderWidthDeclaration.Flags,
                    shorthands: BorderWidthDeclaration.Shorthands,
                    longhands: BorderWidthDeclaration.Longhands)
            },
            {
                BorderTopDeclaration.Name, new DeclarationInfo(
                    converter: BorderTopDeclaration.Converter,
                    aggregator: BorderTopDeclaration.Aggregator,
                    flags: BorderTopDeclaration.Flags,
                    shorthands: BorderTopDeclaration.Shorthands,
                    longhands: BorderTopDeclaration.Longhands)
            },
            {
                BorderStyleDeclaration.Name, new DeclarationInfo(
                    converter: BorderStyleDeclaration.Converter,
                    aggregator: BorderStyleDeclaration.Aggregator,
                    flags: BorderStyleDeclaration.Flags,
                    shorthands: BorderStyleDeclaration.Shorthands,
                    longhands: BorderStyleDeclaration.Longhands)
            },
            {
                BorderRightDeclaration.Name, new DeclarationInfo(
                    converter: BorderRightDeclaration.Converter,
                    aggregator: BorderRightDeclaration.Aggregator,
                    flags: BorderRightDeclaration.Flags,
                    shorthands: BorderRightDeclaration.Shorthands,
                    longhands: BorderRightDeclaration.Longhands)
            },
            {
                BorderLeftDeclaration.Name, new DeclarationInfo(
                    converter: BorderLeftDeclaration.Converter,
                    aggregator: BorderLeftDeclaration.Aggregator,
                    flags: BorderLeftDeclaration.Flags,
                    shorthands: BorderLeftDeclaration.Shorthands,
                    longhands: BorderLeftDeclaration.Longhands)
            },
            {
                BorderColorDeclaration.Name, new DeclarationInfo(
                    converter: BorderColorDeclaration.Converter,
                    aggregator: BorderColorDeclaration.Aggregator,
                    flags: BorderColorDeclaration.Flags,
                    shorthands: BorderColorDeclaration.Shorthands,
                    longhands: BorderColorDeclaration.Longhands)
            },
            {
                BorderBottomDeclaration.Name, new DeclarationInfo(
                    converter: BorderBottomDeclaration.Converter,
                    aggregator: BorderBottomDeclaration.Aggregator,
                    flags: BorderBottomDeclaration.Flags,
                    shorthands: BorderBottomDeclaration.Shorthands,
                    longhands: BorderBottomDeclaration.Longhands)
            },
            {
                BorderDeclaration.Name, new DeclarationInfo(
                    converter: BorderDeclaration.Converter,
                    aggregator: BorderDeclaration.Aggregator,
                    flags: BorderDeclaration.Flags,
                    longhands: BorderDeclaration.Longhands)
            },
        };

        /// <summary>
        /// Registers a new declaration for the given name. Throws an exception if another
        /// declaration for the given property name is already added.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="converter">The converter to use.</param>
        public void Register(String propertyName, DeclarationInfo converter)
        {
            _declarations.Add(propertyName, converter);
        }

        /// <summary>
        /// Unregisters an existing declaration.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The registered declaration, if any.</returns>
        public DeclarationInfo Unregister(String propertyName)
        {
            var info = default(DeclarationInfo);

            if (_declarations.TryGetValue(propertyName, out info))
            {
                _declarations.Remove(propertyName);
            }

            return info;
        }

        /// <summary>
        /// Creates a default declaration for the given property.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The default (any) declaration.</returns>
        protected virtual DeclarationInfo CreateDefault(String propertyName)
        {
            return defaultDeclaration;
        }

        /// <summary>
        /// Gets an existing declaration for the given property name.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The associated declaration.</returns>
        public DeclarationInfo Create(String propertyName)
        {
            var info = default(DeclarationInfo);

            if (!String.IsNullOrEmpty(propertyName) && _declarations.TryGetValue(propertyName, out info))
            {
                return info;
            }

            return CreateDefault(propertyName);
        }
    }
}