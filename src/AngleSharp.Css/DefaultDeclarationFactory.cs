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
        private readonly Dictionary<String, DeclarationInfo> _declarations = new Dictionary<String, DeclarationInfo>(StringComparer.OrdinalIgnoreCase)
        {
            {
                CaptionSideDeclaration.Name, new DeclarationInfo(
                    name: CaptionSideDeclaration.Name,
                    converter: CaptionSideDeclaration.Converter,
                    flags: CaptionSideDeclaration.Flags)
            },
            {
                CursorDeclaration.Name, new DeclarationInfo(
                    name: CursorDeclaration.Name,
                    converter: CursorDeclaration.Converter,
                    flags: CursorDeclaration.Flags)
            },
            {
                EmptyCellsDeclaration.Name, new DeclarationInfo(
                    name: EmptyCellsDeclaration.Name,
                    converter: EmptyCellsDeclaration.Converter,
                    flags: EmptyCellsDeclaration.Flags)
            },
            {
                OrphansDeclaration.Name, new DeclarationInfo(
                    name: OrphansDeclaration.Name,
                    converter: OrphansDeclaration.Converter,
                    flags: OrphansDeclaration.Flags)
            },
            {
                QuotesDeclaration.Name, new DeclarationInfo(
                    name: QuotesDeclaration.Name,
                    converter: QuotesDeclaration.Converter,
                    flags: QuotesDeclaration.Flags)
            },
            {
                TableLayoutDeclaration.Name, new DeclarationInfo(
                    name: TableLayoutDeclaration.Name,
                    converter: TableLayoutDeclaration.Converter,
                    flags: TableLayoutDeclaration.Flags)
            },
            {
                UnicodeBidiDeclaration.Name, new DeclarationInfo(
                    name: UnicodeBidiDeclaration.Name,
                    converter: UnicodeBidiDeclaration.Converter,
                    flags: UnicodeBidiDeclaration.Flags)
            },
            {
                WidowsDeclaration.Name, new DeclarationInfo(
                    name: WidowsDeclaration.Name,
                    converter: WidowsDeclaration.Converter,
                    flags: WidowsDeclaration.Flags)
            },
            {
                ContentDeclaration.Name, new DeclarationInfo(
                    name: ContentDeclaration.Name,
                    converter: ContentDeclaration.Converter,
                    flags: ContentDeclaration.Flags)
            },
            {
                BoxDecorationBreakDeclaration.Name, new DeclarationInfo(
                    name: BoxDecorationBreakDeclaration.Name,
                    converter: BoxDecorationBreakDeclaration.Converter,
                    flags: BoxDecorationBreakDeclaration.Flags)
            },
            {
                BoxShadowDeclaration.Name, new DeclarationInfo(
                    name: BoxShadowDeclaration.Name,
                    converter: BoxShadowDeclaration.Converter,
                    flags: BoxShadowDeclaration.Flags)
            },
            {
                ClearDeclaration.Name, new DeclarationInfo(
                    name: ClearDeclaration.Name,
                    converter: ClearDeclaration.Converter,
                    flags: ClearDeclaration.Flags)
            },
            {
                DisplayDeclaration.Name, new DeclarationInfo(
                    name: DisplayDeclaration.Name,
                    converter: DisplayDeclaration.Converter,
                    flags: DisplayDeclaration.Flags)
            },
            {
                FloatDeclaration.Name, new DeclarationInfo(
                    name: FloatDeclaration.Name,
                    converter: FloatDeclaration.Converter,
                    flags: FloatDeclaration.Flags)
            },
            {
                OverflowDeclaration.Name, new DeclarationInfo(
                    name: OverflowDeclaration.Name,
                    converter: OverflowDeclaration.Converter,
                    flags: OverflowDeclaration.Flags)
            },
            {
                OverflowXDeclaration.Name, new DeclarationInfo(
                    name: OverflowXDeclaration.Name,
                    converter: OverflowXDeclaration.Converter,
                    flags: OverflowXDeclaration.Flags)
            },
            {
                OverflowYDeclaration.Name, new DeclarationInfo(
                    name: OverflowYDeclaration.Name,
                    converter: OverflowYDeclaration.Converter,
                    flags: OverflowYDeclaration.Flags)
            },
            {
                PositionDeclaration.Name, new DeclarationInfo(
                    name: PositionDeclaration.Name,
                    converter: PositionDeclaration.Converter,
                    flags: PositionDeclaration.Flags)
            },
            {
                ZIndexDeclaration.Name, new DeclarationInfo(
                    name: ZIndexDeclaration.Name,
                    converter: ZIndexDeclaration.Converter,
                    flags: ZIndexDeclaration.Flags)
            },
            {
                CounterResetDeclaration.Name, new DeclarationInfo(
                    name: CounterResetDeclaration.Name,
                    converter: CounterResetDeclaration.Converter,
                    flags: CounterResetDeclaration.Flags)
            },
            {
                CounterIncrementDeclaration.Name, new DeclarationInfo(
                    name: CounterIncrementDeclaration.Name,
                    converter: CounterIncrementDeclaration.Converter,
                    flags: CounterIncrementDeclaration.Flags)
            },
            {
                ObjectFitDeclaration.Name, new DeclarationInfo(
                    name: ObjectFitDeclaration.Name,
                    converter: ObjectFitDeclaration.Converter,
                    flags: ObjectFitDeclaration.Flags)
            },
            {
                ObjectPositionDeclaration.Name, new DeclarationInfo(
                    name: ObjectPositionDeclaration.Name,
                    converter: ObjectPositionDeclaration.Converter,
                    flags: ObjectPositionDeclaration.Flags)
            },
            {
                StrokeDasharrayDeclaration.Name, new DeclarationInfo(
                    name: StrokeDasharrayDeclaration.Name,
                    converter: StrokeDasharrayDeclaration.Converter,
                    flags: StrokeDasharrayDeclaration.Flags)
            },
            {
                StrokeDashoffsetDeclaration.Name, new DeclarationInfo(
                    name: StrokeDashoffsetDeclaration.Name,
                    converter: StrokeDashoffsetDeclaration.Converter,
                    flags: StrokeDashoffsetDeclaration.Flags)
            },
            {
                StrokeLinecapDeclaration.Name, new DeclarationInfo(
                    name: StrokeLinecapDeclaration.Name,
                    converter: StrokeLinecapDeclaration.Converter,
                    flags: StrokeLinecapDeclaration.Flags)
            },
            {
                StrokeLinejoinDeclaration.Name, new DeclarationInfo(
                    name: StrokeLinejoinDeclaration.Name,
                    converter: StrokeLinejoinDeclaration.Converter,
                    flags: StrokeLinejoinDeclaration.Flags)
            },
            {
                StrokeMiterlimitDeclaration.Name, new DeclarationInfo(
                    name: StrokeMiterlimitDeclaration.Name,
                    converter: StrokeMiterlimitDeclaration.Converter,
                    flags: StrokeMiterlimitDeclaration.Flags)
            },
            {
                StrokeOpacityDeclaration.Name, new DeclarationInfo(
                    name: StrokeOpacityDeclaration.Name,
                    converter: StrokeOpacityDeclaration.Converter,
                    flags: StrokeOpacityDeclaration.Flags)
            },
            {
                StrokeDeclaration.Name, new DeclarationInfo(
                    name: StrokeDeclaration.Name,
                    converter: StrokeDeclaration.Converter,
                    flags: StrokeDeclaration.Flags)
            },
            {
                StrokeWidthDeclaration.Name, new DeclarationInfo(
                    name: StrokeWidthDeclaration.Name,
                    converter: StrokeWidthDeclaration.Converter,
                    flags: StrokeWidthDeclaration.Flags)
            },
            {
                DirectionDeclaration.Name, new DeclarationInfo(
                    name: DirectionDeclaration.Name,
                    converter: DirectionDeclaration.Converter,
                    flags: DirectionDeclaration.Flags)
            },
            {
                OverflowWrapDeclaration.Name, new DeclarationInfo(
                    name: OverflowWrapDeclaration.Name,
                    converter: OverflowWrapDeclaration.Converter,
                    flags: OverflowWrapDeclaration.Flags)
            },
            {
                WordWrapDeclaration.Name, new DeclarationInfo(
                    name: WordWrapDeclaration.Name,
                    converter: WordWrapDeclaration.Converter,
                    flags: WordWrapDeclaration.Flags)
            },
            {
                PerspectiveOriginDeclaration.Name, new DeclarationInfo(
                    name: PerspectiveOriginDeclaration.Name,
                    converter: PerspectiveOriginDeclaration.Converter,
                    flags: PerspectiveOriginDeclaration.Flags)
            },
            {
                PerspectiveDeclaration.Name, new DeclarationInfo(
                    name: PerspectiveDeclaration.Name,
                    converter: PerspectiveDeclaration.Converter,
                    flags: PerspectiveDeclaration.Flags)
            },
            {
                BackfaceVisibilityDeclaration.Name, new DeclarationInfo(
                    name: BackfaceVisibilityDeclaration.Name,
                    converter: BackfaceVisibilityDeclaration.Converter,
                    flags: BackfaceVisibilityDeclaration.Flags)
            },
            {
                ClipDeclaration.Name, new DeclarationInfo(
                    name: ClipDeclaration.Name,
                    converter: ClipDeclaration.Converter,
                    flags: ClipDeclaration.Flags)
            },
            {
                OpacityDeclaration.Name, new DeclarationInfo(
                    name: OpacityDeclaration.Name,
                    converter: OpacityDeclaration.Converter,
                    flags: OpacityDeclaration.Flags)
            },
            {
                VisibilityDeclaration.Name, new DeclarationInfo(
                    name: VisibilityDeclaration.Name,
                    converter: VisibilityDeclaration.Converter,
                    flags: VisibilityDeclaration.Flags)
            },
            {
                BottomDeclaration.Name, new DeclarationInfo(
                    name: BottomDeclaration.Name,
                    converter: BottomDeclaration.Converter,
                    flags: BottomDeclaration.Flags)
            },
            {
                HeightDeclaration.Name, new DeclarationInfo(
                    name: HeightDeclaration.Name,
                    converter: HeightDeclaration.Converter,
                    flags: HeightDeclaration.Flags)
            },
            {
                LeftDeclaration.Name, new DeclarationInfo(
                    name: LeftDeclaration.Name,
                    converter: LeftDeclaration.Converter,
                    flags: LeftDeclaration.Flags)
            },
            {
                MaxHeightDeclaration.Name, new DeclarationInfo(
                    name: MaxHeightDeclaration.Name,
                    converter: MaxHeightDeclaration.Converter,
                    flags: MaxHeightDeclaration.Flags)
            },
            {
                MaxWidthDeclaration.Name, new DeclarationInfo(
                    name: MaxWidthDeclaration.Name,
                    converter: MaxWidthDeclaration.Converter,
                    flags: MaxWidthDeclaration.Flags)
            },
            {
                MinHeightDeclaration.Name, new DeclarationInfo(
                    name: MinHeightDeclaration.Name,
                    converter: MinHeightDeclaration.Converter,
                    flags: MinHeightDeclaration.Flags)
            },
            {
                MinWidthDeclaration.Name, new DeclarationInfo(
                    name: MinWidthDeclaration.Name,
                    converter: MinWidthDeclaration.Converter,
                    flags: MinWidthDeclaration.Flags)
            },
            {
                RightDeclaration.Name, new DeclarationInfo(
                    name: RightDeclaration.Name,
                    converter: RightDeclaration.Converter,
                    flags: RightDeclaration.Flags)
            },
            {
                TopDeclaration.Name, new DeclarationInfo(
                    name: TopDeclaration.Name,
                    converter: TopDeclaration.Converter,
                    flags: TopDeclaration.Flags)
            },
            {
                WidthDeclaration.Name, new DeclarationInfo(
                    name: WidthDeclaration.Name,
                    converter: WidthDeclaration.Converter,
                    flags: WidthDeclaration.Flags)
            },
            {
                ColorDeclaration.Name, new DeclarationInfo(
                    name: ColorDeclaration.Name,
                    converter: ColorDeclaration.Converter,
                    flags: ColorDeclaration.Flags)
            },
            {
                WordSpacingDeclaration.Name, new DeclarationInfo(
                    name: WordSpacingDeclaration.Name,
                    converter: WordSpacingDeclaration.Converter,
                    flags: WordSpacingDeclaration.Flags)
            },
            {
                LineHeightDeclaration.Name, new DeclarationInfo(
                    name: LineHeightDeclaration.Name,
                    converter: LineHeightDeclaration.Converter,
                    flags: LineHeightDeclaration.Flags,
                    shorthands: LineHeightDeclaration.Shorthands)
            },
            {
                LetterSpacingDeclaration.Name, new DeclarationInfo(
                    name: LetterSpacingDeclaration.Name,
                    converter: LetterSpacingDeclaration.Converter,
                    flags: LetterSpacingDeclaration.Flags)
            },
            {
                FontSizeAdjustDeclaration.Name, new DeclarationInfo(
                    name: FontSizeAdjustDeclaration.Name,
                    converter: FontSizeAdjustDeclaration.Converter,
                    flags: FontSizeAdjustDeclaration.Flags)
            },
            {
                BreakAfterDeclaration.Name, new DeclarationInfo(
                    name: BreakAfterDeclaration.Name,
                    converter: BreakAfterDeclaration.Converter,
                    flags: BreakAfterDeclaration.Flags)
            },
            {
                BreakBeforeDeclaration.Name, new DeclarationInfo(
                    name: BreakBeforeDeclaration.Name,
                    converter: BreakBeforeDeclaration.Converter,
                    flags: BreakBeforeDeclaration.Flags)
            },
            {
                BreakInsideDeclaration.Name, new DeclarationInfo(
                    name: BreakInsideDeclaration.Name,
                    converter: BreakInsideDeclaration.Converter,
                    flags: BreakInsideDeclaration.Flags)
            },
            {
                PageBreakAfterDeclaration.Name, new DeclarationInfo(
                    name: PageBreakAfterDeclaration.Name,
                    converter: PageBreakAfterDeclaration.Converter,
                    flags: PageBreakAfterDeclaration.Flags)
            },
            {
                PageBreakBeforeDeclaration.Name, new DeclarationInfo(
                    name: PageBreakBeforeDeclaration.Name,
                    converter: PageBreakBeforeDeclaration.Converter,
                    flags: PageBreakBeforeDeclaration.Flags)
            },
            {
                PageBreakInsideDeclaration.Name, new DeclarationInfo(
                    name: PageBreakInsideDeclaration.Name,
                    converter: PageBreakInsideDeclaration.Converter,
                    flags: PageBreakInsideDeclaration.Flags)
            },
            {
                TransformOriginDeclaration.Name, new DeclarationInfo(
                    name: TransformOriginDeclaration.Name,
                    converter: TransformOriginDeclaration.Converter,
                    flags: TransformOriginDeclaration.Flags)
            },
            {
                TransformDeclaration.Name, new DeclarationInfo(
                    name: TransformDeclaration.Name,
                    converter: TransformDeclaration.Converter,
                    flags: TransformDeclaration.Flags)
            },
            {
                TransformStyleDeclaration.Name, new DeclarationInfo(
                    name: TransformStyleDeclaration.Name,
                    converter: TransformStyleDeclaration.Converter,
                    flags: TransformStyleDeclaration.Flags)
            },
            {
                ColumnCountDeclaration.Name, new DeclarationInfo(
                    name: ColumnCountDeclaration.Name,
                    converter: ColumnCountDeclaration.Converter,
                    flags: ColumnCountDeclaration.Flags,
                    shorthands: ColumnCountDeclaration.Shorthands)
            },
            {
                ColumnFillDeclaration.Name, new DeclarationInfo(
                    name: ColumnFillDeclaration.Name,
                    converter: ColumnFillDeclaration.Converter,
                    flags: ColumnFillDeclaration.Flags)
            },
            {
                ColumnGapDeclaration.Name, new DeclarationInfo(
                    name: ColumnGapDeclaration.Name,
                    converter: ColumnGapDeclaration.Converter,
                    shorthands: ColumnGapDeclaration.Shorthands,
                    flags: ColumnGapDeclaration.Flags)
            },
            {
                RowGapDeclaration.Name, new DeclarationInfo(
                    name: RowGapDeclaration.Name,
                    converter: RowGapDeclaration.Converter,
                    shorthands: RowGapDeclaration.Shorthands,
                    flags: RowGapDeclaration.Flags)
            },
            {
                GapDeclaration.Name, new DeclarationInfo(
                    name: GapDeclaration.Name,
                    converter: GapDeclaration.Converter,
                    longhands: GapDeclaration.Longhands,
                    flags: GapDeclaration.Flags)
            },
            {
                ColumnSpanDeclaration.Name, new DeclarationInfo(
                    name: ColumnSpanDeclaration.Name,
                    converter: ColumnSpanDeclaration.Converter,
                    flags: ColumnSpanDeclaration.Flags)
            },
            {
                ColumnWidthDeclaration.Name, new DeclarationInfo(
                    name: ColumnWidthDeclaration.Name,
                    converter: ColumnWidthDeclaration.Converter,
                    flags: ColumnWidthDeclaration.Flags,
                    shorthands: ColumnWidthDeclaration.Shorthands)
            },
            {
                BorderCollapseDeclaration.Name, new DeclarationInfo(
                    name: BorderCollapseDeclaration.Name,
                    converter: BorderCollapseDeclaration.Converter,
                    flags: BorderCollapseDeclaration.Flags)
            },
            {
                BorderSpacingDeclaration.Name, new DeclarationInfo(
                    name: BorderSpacingDeclaration.Name,
                    converter: BorderSpacingDeclaration.Converter,
                    flags: BorderSpacingDeclaration.Flags)
            },
            {
                WordBreakDeclaration.Name, new DeclarationInfo(
                    name: WordBreakDeclaration.Name,
                    converter: WordBreakDeclaration.Converter,
                    flags: WordBreakDeclaration.Flags)
            },
            {
                WhiteSpaceDeclaration.Name, new DeclarationInfo(
                    name: WhiteSpaceDeclaration.Name,
                    converter: WhiteSpaceDeclaration.Converter,
                    flags: WhiteSpaceDeclaration.Flags)
            },
            {
                VerticalAlignDeclaration.Name, new DeclarationInfo(
                    name: VerticalAlignDeclaration.Name,
                    converter: VerticalAlignDeclaration.Converter,
                    flags: VerticalAlignDeclaration.Flags)
            },
            {
                TextShadowDeclaration.Name, new DeclarationInfo(
                    name: TextShadowDeclaration.Name,
                    converter: TextShadowDeclaration.Converter,
                    flags: TextShadowDeclaration.Flags)
            },
            {
                TextJustifyDeclaration.Name, new DeclarationInfo(
                    name: TextJustifyDeclaration.Name,
                    converter: TextJustifyDeclaration.Converter,
                    flags: TextJustifyDeclaration.Flags)
            },
            {
                TextIndentDeclaration.Name, new DeclarationInfo(
                    name: TextIndentDeclaration.Name,
                    converter: TextIndentDeclaration.Converter,
                    flags: TextIndentDeclaration.Flags)
            },
            {
                TextAnchorDeclaration.Name, new DeclarationInfo(
                    name: TextAnchorDeclaration.Name,
                    converter: TextAnchorDeclaration.Converter,
                    flags: TextAnchorDeclaration.Flags)
            },
            {
                TextAlignDeclaration.Name, new DeclarationInfo(
                    name: TextAlignDeclaration.Name,
                    converter: TextAlignDeclaration.Converter,
                    flags: TextAlignDeclaration.Flags)
            },
            {
                TextAlignLastDeclaration.Name, new DeclarationInfo(
                    name: TextAlignLastDeclaration.Name,
                    converter: TextAlignLastDeclaration.Converter,
                    flags: TextAlignLastDeclaration.Flags)
            },
            {
                TextTransformDeclaration.Name, new DeclarationInfo(
                    name: TextTransformDeclaration.Name,
                    converter: TextTransformDeclaration.Converter,
                    flags: TextTransformDeclaration.Flags)
            },
            {
                ListStyleImageDeclaration.Name, new DeclarationInfo(
                    name: ListStyleImageDeclaration.Name,
                    converter: ListStyleImageDeclaration.Converter,
                    flags: ListStyleImageDeclaration.Flags,
                    shorthands: ListStyleImageDeclaration.Shorthands)
            },
            {
                ListStylePositionDeclaration.Name, new DeclarationInfo(
                    name: ListStylePositionDeclaration.Name,
                    converter: ListStylePositionDeclaration.Converter,
                    flags: ListStylePositionDeclaration.Flags,
                    shorthands: ListStylePositionDeclaration.Shorthands)
            },
            {
                ListStyleTypeDeclaration.Name, new DeclarationInfo(
                    name: ListStyleTypeDeclaration.Name,
                    converter: ListStyleTypeDeclaration.Converter,
                    flags: ListStyleTypeDeclaration.Flags,
                    shorthands: ListStyleTypeDeclaration.Shorthands)
            },
            {
                FontFamilyDeclaration.Name, new DeclarationInfo(
                    name: FontFamilyDeclaration.Name,
                    converter: FontFamilyDeclaration.Converter,
                    flags: FontFamilyDeclaration.Flags,
                    shorthands: FontFamilyDeclaration.Shorthands)
            },
            {
                FontSizeDeclaration.Name, new DeclarationInfo(
                    name: FontSizeDeclaration.Name,
                    converter: FontSizeDeclaration.Converter,
                    flags: FontSizeDeclaration.Flags,
                    shorthands: FontSizeDeclaration.Shorthands)
            },
            {
                FontStyleDeclaration.Name, new DeclarationInfo(
                    name: FontStyleDeclaration.Name,
                    converter: FontStyleDeclaration.Converter,
                    flags: FontStyleDeclaration.Flags,
                    shorthands: FontStyleDeclaration.Shorthands)
            },
            {
                FontStretchDeclaration.Name, new DeclarationInfo(
                    name: FontStretchDeclaration.Name,
                    converter: FontStretchDeclaration.Converter,
                    flags: FontStretchDeclaration.Flags)
            },
            {
                FontVariantDeclaration.Name, new DeclarationInfo(
                    name: FontVariantDeclaration.Name,
                    converter: FontVariantDeclaration.Converter,
                    flags: FontVariantDeclaration.Flags,
                    shorthands: FontVariantDeclaration.Shorthands)
            },
            {
                FontWeightDeclaration.Name, new DeclarationInfo(
                    name: FontWeightDeclaration.Name,
                    converter: FontWeightDeclaration.Converter,
                    flags: FontWeightDeclaration.Flags,
                    shorthands: FontWeightDeclaration.Shorthands)
            },
            {
                UnicodeRangeDeclaration.Name, new DeclarationInfo(
                    name: UnicodeRangeDeclaration.Name,
                    converter: UnicodeRangeDeclaration.Converter,
                    flags: UnicodeRangeDeclaration.Flags)
            },
            {
                SrcDeclaration.Name, new DeclarationInfo(
                    name: SrcDeclaration.Name,
                    converter: SrcDeclaration.Converter,
                    flags: SrcDeclaration.Flags)
            },
            {
                ColumnRuleWidthDeclaration.Name, new DeclarationInfo(
                    name: ColumnRuleWidthDeclaration.Name,
                    converter: ColumnRuleWidthDeclaration.Converter,
                    flags: ColumnRuleWidthDeclaration.Flags,
                    shorthands: ColumnRuleWidthDeclaration.Shorthands)
            },
            {
                ColumnRuleStyleDeclaration.Name, new DeclarationInfo(
                    name: ColumnRuleStyleDeclaration.Name,
                    converter: ColumnRuleStyleDeclaration.Converter,
                    flags: ColumnRuleStyleDeclaration.Flags,
                    shorthands: ColumnRuleStyleDeclaration.Shorthands)
            },
            {
                ColumnRuleColorDeclaration.Name, new DeclarationInfo(
                    name: ColumnRuleColorDeclaration.Name,
                    converter: ColumnRuleColorDeclaration.Converter,
                    flags: ColumnRuleColorDeclaration.Flags,
                    shorthands: ColumnRuleColorDeclaration.Shorthands)
            },
            {
                PaddingTopDeclaration.Name, new DeclarationInfo(
                    name: PaddingTopDeclaration.Name,
                    converter: PaddingTopDeclaration.Converter,
                    flags: PaddingTopDeclaration.Flags,
                    shorthands: PaddingTopDeclaration.Shorthands)
            },
            {
                PaddingRightDeclaration.Name, new DeclarationInfo(
                    name: PaddingRightDeclaration.Name,
                    converter: PaddingRightDeclaration.Converter,
                    flags: PaddingRightDeclaration.Flags,
                    shorthands: PaddingRightDeclaration.Shorthands)
            },
            {
                PaddingLeftDeclaration.Name, new DeclarationInfo(
                    name: PaddingLeftDeclaration.Name,
                    converter: PaddingLeftDeclaration.Converter,
                    flags: PaddingLeftDeclaration.Flags,
                    shorthands: PaddingLeftDeclaration.Shorthands)
            },
            {
                PaddingBottomDeclaration.Name, new DeclarationInfo(
                    name: PaddingBottomDeclaration.Name,
                    converter: PaddingBottomDeclaration.Converter,
                    flags: PaddingBottomDeclaration.Flags,
                    shorthands: PaddingBottomDeclaration.Shorthands)
            },
            {
                MarginTopDeclaration.Name, new DeclarationInfo(
                    name: MarginTopDeclaration.Name,
                    converter: MarginTopDeclaration.Converter,
                    flags: MarginTopDeclaration.Flags,
                    shorthands: MarginTopDeclaration.Shorthands)
            },
            {
                MarginRightDeclaration.Name, new DeclarationInfo(
                    name: MarginRightDeclaration.Name,
                    converter: MarginRightDeclaration.Converter,
                    flags: MarginRightDeclaration.Flags,
                    shorthands: MarginRightDeclaration.Shorthands)
            },
            {
                MarginLeftDeclaration.Name, new DeclarationInfo(
                    name: MarginLeftDeclaration.Name,
                    converter: MarginLeftDeclaration.Converter,
                    flags: MarginLeftDeclaration.Flags,
                    shorthands: MarginLeftDeclaration.Shorthands)
            },
            {
                MarginBottomDeclaration.Name, new DeclarationInfo(
                    name: MarginBottomDeclaration.Name,
                    converter: MarginBottomDeclaration.Converter,
                    flags: MarginBottomDeclaration.Flags,
                    shorthands: MarginBottomDeclaration.Shorthands)
            },
            {
                BorderTopRightRadiusDeclaration.Name, new DeclarationInfo(
                    name: BorderTopRightRadiusDeclaration.Name,
                    converter: BorderTopRightRadiusDeclaration.Converter,
                    flags: BorderTopRightRadiusDeclaration.Flags,
                    shorthands: BorderTopRightRadiusDeclaration.Shorthands)
            },
            {
                BorderTopLeftRadiusDeclaration.Name, new DeclarationInfo(
                    name: BorderTopLeftRadiusDeclaration.Name,
                    converter: BorderTopLeftRadiusDeclaration.Converter,
                    flags: BorderTopLeftRadiusDeclaration.Flags,
                    shorthands: BorderTopLeftRadiusDeclaration.Shorthands)
            },
            {
                BorderBottomRightRadiusDeclaration.Name, new DeclarationInfo(
                    name: BorderBottomRightRadiusDeclaration.Name,
                    converter: BorderBottomRightRadiusDeclaration.Converter,
                    flags: BorderBottomRightRadiusDeclaration.Flags,
                    shorthands: BorderBottomRightRadiusDeclaration.Shorthands)
            },
            {
                BorderBottomLeftRadiusDeclaration.Name, new DeclarationInfo(
                    name: BorderBottomLeftRadiusDeclaration.Name,
                    converter: BorderBottomLeftRadiusDeclaration.Converter,
                    flags: BorderBottomLeftRadiusDeclaration.Flags,
                    shorthands: BorderBottomLeftRadiusDeclaration.Shorthands)
            },
            {
                OutlineWidthDeclaration.Name, new DeclarationInfo(
                    name: OutlineWidthDeclaration.Name,
                    converter: OutlineWidthDeclaration.Converter,
                    flags: OutlineWidthDeclaration.Flags,
                    shorthands: OutlineWidthDeclaration.Shorthands)
            },
            {
                OutlineStyleDeclaration.Name, new DeclarationInfo(
                    name: OutlineStyleDeclaration.Name,
                    converter: OutlineStyleDeclaration.Converter,
                    flags: OutlineStyleDeclaration.Flags,
                    shorthands: OutlineStyleDeclaration.Shorthands)
            },
            {
                OutlineColorDeclaration.Name, new DeclarationInfo(
                    name: OutlineColorDeclaration.Name,
                    converter: OutlineColorDeclaration.Converter,
                    flags: OutlineColorDeclaration.Flags,
                    shorthands: OutlineColorDeclaration.Shorthands)
            },
            {
                TextDecorationStyleDeclaration.Name, new DeclarationInfo(
                    name: TextDecorationStyleDeclaration.Name,
                    converter: TextDecorationStyleDeclaration.Converter,
                    flags: TextDecorationStyleDeclaration.Flags,
                    shorthands: TextDecorationStyleDeclaration.Shorthands)
            },
            {
                TextDecorationLineDeclaration.Name, new DeclarationInfo(
                    name: TextDecorationLineDeclaration.Name,
                    converter: TextDecorationLineDeclaration.Converter,
                    flags: TextDecorationLineDeclaration.Flags,
                    shorthands: TextDecorationLineDeclaration.Shorthands)
            },
            {
                TextDecorationColorDeclaration.Name, new DeclarationInfo(
                    name: TextDecorationColorDeclaration.Name,
                    converter: TextDecorationColorDeclaration.Converter,
                    flags: TextDecorationColorDeclaration.Flags,
                    shorthands: TextDecorationColorDeclaration.Shorthands)
            },
            {
                TransitionTimingFunctionDeclaration.Name, new DeclarationInfo(
                    name: TransitionTimingFunctionDeclaration.Name,
                    converter: TransitionTimingFunctionDeclaration.Converter,
                    flags: TransitionTimingFunctionDeclaration.Flags,
                    shorthands: TransitionTimingFunctionDeclaration.Shorthands)
            },
            {
                TransitionPropertyDeclaration.Name, new DeclarationInfo(
                    name: TransitionPropertyDeclaration.Name,
                    converter: TransitionPropertyDeclaration.Converter,
                    flags: TransitionPropertyDeclaration.Flags,
                    shorthands: TransitionPropertyDeclaration.Shorthands)
            },
            {
                TransitionDurationDeclaration.Name, new DeclarationInfo(
                    name: TransitionDurationDeclaration.Name,
                    converter: TransitionDurationDeclaration.Converter,
                    flags: TransitionDurationDeclaration.Flags,
                    shorthands: TransitionDurationDeclaration.Shorthands)
            },
            {
                TransitionDelayDeclaration.Name, new DeclarationInfo(
                    name: TransitionDelayDeclaration.Name,
                    converter: TransitionDelayDeclaration.Converter,
                    flags: TransitionDelayDeclaration.Flags,
                    shorthands: TransitionDelayDeclaration.Shorthands)
            },
            {
                BorderImageWidthDeclaration.Name, new DeclarationInfo(
                    name: BorderImageWidthDeclaration.Name,
                    converter: BorderImageWidthDeclaration.Converter,
                    flags: BorderImageWidthDeclaration.Flags,
                    shorthands: BorderImageWidthDeclaration.Shorthands)
            },
            {
                BorderImageSourceDeclaration.Name, new DeclarationInfo(
                    name: BorderImageSourceDeclaration.Name,
                    converter: BorderImageSourceDeclaration.Converter,
                    flags: BorderImageSourceDeclaration.Flags,
                    shorthands: BorderImageSourceDeclaration.Shorthands)
            },
            {
                BorderImageSliceDeclaration.Name, new DeclarationInfo(
                    name: BorderImageSliceDeclaration.Name,
                    converter: BorderImageSliceDeclaration.Converter,
                    flags: BorderImageSliceDeclaration.Flags,
                    shorthands: BorderImageSliceDeclaration.Shorthands)
            },
            {
                BorderImageRepeatDeclaration.Name, new DeclarationInfo(
                    name: BorderImageRepeatDeclaration.Name,
                    converter: BorderImageRepeatDeclaration.Converter,
                    flags: BorderImageRepeatDeclaration.Flags,
                    shorthands: BorderImageRepeatDeclaration.Shorthands)
            },
            {
                BorderImageOutsetDeclaration.Name, new DeclarationInfo(
                    name: BorderImageOutsetDeclaration.Name,
                    converter: BorderImageOutsetDeclaration.Converter,
                    flags: BorderImageOutsetDeclaration.Flags,
                    shorthands: BorderImageOutsetDeclaration.Shorthands)
            },
            {
                AnimationTimingFunctionDeclaration.Name, new DeclarationInfo(
                    name: AnimationTimingFunctionDeclaration.Name,
                    converter: AnimationTimingFunctionDeclaration.Converter,
                    flags: AnimationTimingFunctionDeclaration.Flags,
                    shorthands: AnimationTimingFunctionDeclaration.Shorthands)
            },
            {
                AnimationPlayStateDeclaration.Name, new DeclarationInfo(
                    name: AnimationPlayStateDeclaration.Name,
                    converter: AnimationPlayStateDeclaration.Converter,
                    flags: AnimationPlayStateDeclaration.Flags,
                    shorthands: AnimationPlayStateDeclaration.Shorthands)
            },
            {
                AnimationNameDeclaration.Name, new DeclarationInfo(
                    name: AnimationNameDeclaration.Name,
                    converter: AnimationNameDeclaration.Converter,
                    flags: AnimationNameDeclaration.Flags,
                    shorthands: AnimationNameDeclaration.Shorthands)
            },
            {
                AnimationIterationCountDeclaration.Name, new DeclarationInfo(
                    name: AnimationIterationCountDeclaration.Name,
                    converter: AnimationIterationCountDeclaration.Converter,
                    flags: AnimationIterationCountDeclaration.Flags,
                    shorthands: AnimationIterationCountDeclaration.Shorthands)
            },
            {
                AnimationFillModeDeclaration.Name, new DeclarationInfo(
                    name: AnimationFillModeDeclaration.Name,
                    converter: AnimationFillModeDeclaration.Converter,
                    flags: AnimationFillModeDeclaration.Flags,
                    shorthands: AnimationFillModeDeclaration.Shorthands)
            },
            {
                AnimationDurationDeclaration.Name, new DeclarationInfo(
                    name: AnimationDurationDeclaration.Name,
                    converter: AnimationDurationDeclaration.Converter,
                    flags: AnimationDurationDeclaration.Flags,
                    shorthands: AnimationDurationDeclaration.Shorthands)
            },
            {
                AnimationDirectionDeclaration.Name, new DeclarationInfo(
                    name: AnimationDirectionDeclaration.Name,
                    converter: AnimationDirectionDeclaration.Converter,
                    flags: AnimationDirectionDeclaration.Flags,
                    shorthands: AnimationDirectionDeclaration.Shorthands)
            },
            {
                AnimationDelayDeclaration.Name, new DeclarationInfo(
                    name: AnimationDelayDeclaration.Name,
                    converter: AnimationDelayDeclaration.Converter,
                    flags: AnimationDelayDeclaration.Flags,
                    shorthands: AnimationDelayDeclaration.Shorthands)
            },
            {
                BackgroundSizeDeclaration.Name, new DeclarationInfo(
                    name: BackgroundSizeDeclaration.Name,
                    converter: BackgroundSizeDeclaration.Converter,
                    flags: BackgroundSizeDeclaration.Flags,
                    shorthands: BackgroundSizeDeclaration.Shorthands)
            },
            {
                BackgroundRepeatDeclaration.Name, new DeclarationInfo(
                    name: BackgroundRepeatDeclaration.Name,
                    converter: BackgroundRepeatDeclaration.Converter,
                    flags: BackgroundRepeatDeclaration.Flags,
                    longhands: BackgroundRepeatDeclaration.Longhands,
                    shorthands: BackgroundRepeatDeclaration.Shorthands)
            },
            {
                BackgroundRepeatXDeclaration.Name, new DeclarationInfo(
                    name: BackgroundRepeatXDeclaration.Name,
                    converter: BackgroundRepeatXDeclaration.Converter,
                    flags: BackgroundRepeatXDeclaration.Flags,
                    shorthands: BackgroundRepeatXDeclaration.Shorthands)
            },
            {
                BackgroundRepeatYDeclaration.Name, new DeclarationInfo(
                    name: BackgroundRepeatYDeclaration.Name,
                    converter: BackgroundRepeatYDeclaration.Converter,
                    flags: BackgroundRepeatYDeclaration.Flags,
                    shorthands: BackgroundRepeatYDeclaration.Shorthands)
            },
            {
                BackgroundPositionDeclaration.Name, new DeclarationInfo(
                    name: BackgroundPositionDeclaration.Name,
                    converter: BackgroundPositionDeclaration.Converter,
                    flags: BackgroundPositionDeclaration.Flags,
                    shorthands: BackgroundPositionDeclaration.Shorthands,
                    longhands: BackgroundPositionDeclaration.Longhands)
            },
            {
                BackgroundPositionXDeclaration.Name, new DeclarationInfo(
                    name: BackgroundPositionXDeclaration.Name,
                    converter: BackgroundPositionXDeclaration.Converter,
                    flags: BackgroundPositionXDeclaration.Flags,
                    shorthands: BackgroundPositionXDeclaration.Shorthands)
            },
            {
                BackgroundPositionYDeclaration.Name, new DeclarationInfo(
                    name: BackgroundPositionYDeclaration.Name,
                    converter: BackgroundPositionYDeclaration.Converter,
                    flags: BackgroundPositionYDeclaration.Flags,
                    shorthands: BackgroundPositionYDeclaration.Shorthands)
            },
            {
                BackgroundOriginDeclaration.Name, new DeclarationInfo(
                    name: BackgroundOriginDeclaration.Name,
                    converter: BackgroundOriginDeclaration.Converter,
                    flags: BackgroundOriginDeclaration.Flags,
                    shorthands: BackgroundOriginDeclaration.Shorthands)
            },
            {
                BackgroundImageDeclaration.Name, new DeclarationInfo(
                    name: BackgroundImageDeclaration.Name,
                    converter: BackgroundImageDeclaration.Converter,
                    flags: BackgroundImageDeclaration.Flags,
                    shorthands: BackgroundImageDeclaration.Shorthands)
            },
            {
                BackgroundColorDeclaration.Name, new DeclarationInfo(
                    name: BackgroundColorDeclaration.Name,
                    converter: BackgroundColorDeclaration.Converter,
                    flags: BackgroundColorDeclaration.Flags,
                    shorthands: BackgroundColorDeclaration.Shorthands)
            },
            {
                BackgroundClipDeclaration.Name, new DeclarationInfo(
                    name: BackgroundClipDeclaration.Name,
                    converter: BackgroundClipDeclaration.Converter,
                    flags: BackgroundClipDeclaration.Flags,
                    shorthands: BackgroundClipDeclaration.Shorthands)
            },
            {
                BackgroundAttachmentDeclaration.Name, new DeclarationInfo(
                    name: BackgroundAttachmentDeclaration.Name,
                    converter: BackgroundAttachmentDeclaration.Converter,
                    flags: BackgroundAttachmentDeclaration.Flags,
                    shorthands: BackgroundAttachmentDeclaration.Shorthands)
            },
            {
                BorderTopWidthDeclaration.Name, new DeclarationInfo(
                    name: BorderTopWidthDeclaration.Name,
                    converter: BorderTopWidthDeclaration.Converter,
                    flags: BorderTopWidthDeclaration.Flags,
                    shorthands: BorderTopWidthDeclaration.Shorthands)
            },
            {
                BorderTopStyleDeclaration.Name, new DeclarationInfo(
                    name: BorderTopStyleDeclaration.Name,
                    converter: BorderTopStyleDeclaration.Converter,
                    flags: BorderTopStyleDeclaration.Flags,
                    shorthands: BorderTopStyleDeclaration.Shorthands)
            },
            {
                BorderTopColorDeclaration.Name, new DeclarationInfo(
                    name: BorderTopColorDeclaration.Name,
                    converter: BorderTopColorDeclaration.Converter,
                    flags: BorderTopColorDeclaration.Flags,
                    shorthands: BorderTopColorDeclaration.Shorthands)
            },
            {
                BorderRightWidthDeclaration.Name, new DeclarationInfo(
                    name: BorderRightWidthDeclaration.Name,
                    converter: BorderRightWidthDeclaration.Converter,
                    flags: BorderRightWidthDeclaration.Flags,
                    shorthands: BorderRightWidthDeclaration.Shorthands)
            },
            {
                BorderRightStyleDeclaration.Name, new DeclarationInfo(
                    name: BorderRightStyleDeclaration.Name,
                    converter: BorderRightStyleDeclaration.Converter,
                    flags: BorderRightStyleDeclaration.Flags,
                    shorthands: BorderRightStyleDeclaration.Shorthands)
            },
            {
                BorderRightColorDeclaration.Name, new DeclarationInfo(
                    name: BorderRightColorDeclaration.Name,
                    converter: BorderRightColorDeclaration.Converter,
                    flags: BorderRightColorDeclaration.Flags,
                    shorthands: BorderRightColorDeclaration.Shorthands)
            },
            {
                BorderLeftWidthDeclaration.Name, new DeclarationInfo(
                    name: BorderLeftWidthDeclaration.Name,
                    converter: BorderLeftWidthDeclaration.Converter,
                    flags: BorderLeftWidthDeclaration.Flags,
                    shorthands: BorderLeftWidthDeclaration.Shorthands)
            },
            {
                BorderLeftStyleDeclaration.Name, new DeclarationInfo(
                    name: BorderLeftStyleDeclaration.Name,
                    converter: BorderLeftStyleDeclaration.Converter,
                    flags: BorderLeftStyleDeclaration.Flags,
                    shorthands: BorderLeftStyleDeclaration.Shorthands)
            },
            {
                BorderLeftColorDeclaration.Name, new DeclarationInfo(
                    name: BorderLeftColorDeclaration.Name,
                    converter: BorderLeftColorDeclaration.Converter,
                    flags: BorderLeftColorDeclaration.Flags,
                    shorthands: BorderLeftColorDeclaration.Shorthands)
            },
            {
                BorderBottomWidthDeclaration.Name, new DeclarationInfo(
                    name: BorderBottomWidthDeclaration.Name,
                    converter: BorderBottomWidthDeclaration.Converter,
                    flags: BorderBottomWidthDeclaration.Flags,
                    shorthands: BorderBottomWidthDeclaration.Shorthands)
            },
            {
                BorderBottomStyleDeclaration.Name, new DeclarationInfo(
                    name: BorderBottomStyleDeclaration.Name,
                    converter: BorderBottomStyleDeclaration.Converter,
                    flags: BorderBottomStyleDeclaration.Flags,
                    shorthands: BorderBottomStyleDeclaration.Shorthands)
            },
            {
                BorderBottomColorDeclaration.Name, new DeclarationInfo(
                    name: BorderBottomColorDeclaration.Name,
                    converter: BorderBottomColorDeclaration.Converter,
                    flags: BorderBottomColorDeclaration.Flags,
                    shorthands: BorderBottomColorDeclaration.Shorthands)
            },
            {
                AnimationDeclaration.Name, new DeclarationInfo(
                    name: AnimationDeclaration.Name,
                    converter: AnimationDeclaration.Converter,
                    flags: AnimationDeclaration.Flags,
                    longhands: AnimationDeclaration.Longhands)
            },
            {
                BackgroundDeclaration.Name, new DeclarationInfo(
                    name: BackgroundDeclaration.Name,
                    converter: BackgroundDeclaration.Converter,
                    flags: BackgroundDeclaration.Flags,
                    longhands: BackgroundDeclaration.Longhands)
            },
            {
                TransitionDeclaration.Name, new DeclarationInfo(
                    name: TransitionDeclaration.Name,
                    converter: TransitionDeclaration.Converter,
                    flags: TransitionDeclaration.Flags,
                    longhands: TransitionDeclaration.Longhands)
            },
            {
                TextDecorationDeclaration.Name, new DeclarationInfo(
                    name: TextDecorationDeclaration.Name,
                    converter: TextDecorationDeclaration.Converter,
                    flags: TextDecorationDeclaration.Flags,
                    longhands: TextDecorationDeclaration.Longhands)
            },
            {
                OutlineDeclaration.Name, new DeclarationInfo(
                    name: OutlineDeclaration.Name,
                    converter: OutlineDeclaration.Converter,
                    flags: OutlineDeclaration.Flags,
                    longhands: OutlineDeclaration.Longhands)
            },
            {
                ListStyleDeclaration.Name, new DeclarationInfo(
                    name: ListStyleDeclaration.Name,
                    converter: ListStyleDeclaration.Converter,
                    flags: ListStyleDeclaration.Flags,
                    longhands: ListStyleDeclaration.Longhands)
            },
            {
                FontDeclaration.Name, new DeclarationInfo(
                    name: FontDeclaration.Name,
                    converter: FontDeclaration.Converter,
                    flags: FontDeclaration.Flags,
                    longhands: FontDeclaration.Longhands)
            },
            {
                ColumnsDeclaration.Name, new DeclarationInfo(
                    name: ColumnsDeclaration.Name,
                    converter: ColumnsDeclaration.Converter,
                    flags: ColumnsDeclaration.Flags,
                    longhands: ColumnsDeclaration.Longhands)
            },
            {
                ColumnRuleDeclaration.Name, new DeclarationInfo(
                    name: ColumnRuleDeclaration.Name,
                    converter: ColumnRuleDeclaration.Converter,
                    flags: ColumnRuleDeclaration.Flags,
                    longhands: ColumnRuleDeclaration.Longhands)
            },
            {
                PaddingDeclaration.Name, new DeclarationInfo(
                    name: PaddingDeclaration.Name,
                    converter: PaddingDeclaration.Converter,
                    flags: PaddingDeclaration.Flags,
                    longhands: PaddingDeclaration.Longhands)
            },
            {
                MarginDeclaration.Name, new DeclarationInfo(
                    name: MarginDeclaration.Name,
                    converter: MarginDeclaration.Converter,
                    flags: MarginDeclaration.Flags,
                    longhands: MarginDeclaration.Longhands)
            },
            {
                BorderRadiusDeclaration.Name, new DeclarationInfo(
                    name: BorderRadiusDeclaration.Name,
                    converter: BorderRadiusDeclaration.Converter,
                    flags: BorderRadiusDeclaration.Flags,
                    longhands: BorderRadiusDeclaration.Longhands)
            },
            {
                BorderImageDeclaration.Name, new DeclarationInfo(
                    name: BorderImageDeclaration.Name,
                    converter: BorderImageDeclaration.Converter,
                    flags: BorderImageDeclaration.Flags,
                    longhands: BorderImageDeclaration.Longhands)
            },
            {
                BorderWidthDeclaration.Name, new DeclarationInfo(
                    name: BorderWidthDeclaration.Name,
                    converter: BorderWidthDeclaration.Converter,
                    flags: BorderWidthDeclaration.Flags,
                    shorthands: BorderWidthDeclaration.Shorthands,
                    longhands: BorderWidthDeclaration.Longhands)
            },
            {
                BorderTopDeclaration.Name, new DeclarationInfo(
                    name: BorderTopDeclaration.Name,
                    converter: BorderTopDeclaration.Converter,
                    flags: BorderTopDeclaration.Flags,
                    shorthands: BorderTopDeclaration.Shorthands,
                    longhands: BorderTopDeclaration.Longhands)
            },
            {
                BorderStyleDeclaration.Name, new DeclarationInfo(
                    name: BorderStyleDeclaration.Name,
                    converter: BorderStyleDeclaration.Converter,
                    flags: BorderStyleDeclaration.Flags,
                    shorthands: BorderStyleDeclaration.Shorthands,
                    longhands: BorderStyleDeclaration.Longhands)
            },
            {
                BorderRightDeclaration.Name, new DeclarationInfo(
                    name: BorderRightDeclaration.Name,
                    converter: BorderRightDeclaration.Converter,
                    flags: BorderRightDeclaration.Flags,
                    shorthands: BorderRightDeclaration.Shorthands,
                    longhands: BorderRightDeclaration.Longhands)
            },
            {
                BorderLeftDeclaration.Name, new DeclarationInfo(
                    name: BorderLeftDeclaration.Name,
                    converter: BorderLeftDeclaration.Converter,
                    flags: BorderLeftDeclaration.Flags,
                    shorthands: BorderLeftDeclaration.Shorthands,
                    longhands: BorderLeftDeclaration.Longhands)
            },
            {
                BorderColorDeclaration.Name, new DeclarationInfo(
                    name: BorderColorDeclaration.Name,
                    converter: BorderColorDeclaration.Converter,
                    flags: BorderColorDeclaration.Flags,
                    shorthands: BorderColorDeclaration.Shorthands,
                    longhands: BorderColorDeclaration.Longhands)
            },
            {
                BorderBottomDeclaration.Name, new DeclarationInfo(
                    name: BorderBottomDeclaration.Name,
                    converter: BorderBottomDeclaration.Converter,
                    flags: BorderBottomDeclaration.Flags,
                    shorthands: BorderBottomDeclaration.Shorthands,
                    longhands: BorderBottomDeclaration.Longhands)
            },
            {
                BorderDeclaration.Name, new DeclarationInfo(
                    name: BorderDeclaration.Name,
                    converter: BorderDeclaration.Converter,
                    flags: BorderDeclaration.Flags,
                    longhands: BorderDeclaration.Longhands)
            },
            {
                RubyAlignDeclaration.Name, new DeclarationInfo(
                    name: RubyAlignDeclaration.Name,
                    converter: RubyAlignDeclaration.Converter,
                    flags: RubyAlignDeclaration.Flags)
            },
            {
                RubyOverhangDeclaration.Name, new DeclarationInfo(
                    name: RubyOverhangDeclaration.Name,
                    converter: RubyOverhangDeclaration.Converter,
                    flags: RubyOverhangDeclaration.Flags)
            },
            {
                RubyPositionDeclaration.Name, new DeclarationInfo(
                    name: RubyPositionDeclaration.Name,
                    converter: RubyPositionDeclaration.Converter,
                    flags: RubyPositionDeclaration.Flags)
            },
            {
                Scrollbar3dLightColorDeclaration.Name, new DeclarationInfo(
                    name: Scrollbar3dLightColorDeclaration.Name,
                    converter: Scrollbar3dLightColorDeclaration.Converter,
                    flags: Scrollbar3dLightColorDeclaration.Flags)
            },
            {
                ScrollbarArrowColorDeclaration.Name, new DeclarationInfo(
                    name: ScrollbarArrowColorDeclaration.Name,
                    converter: ScrollbarArrowColorDeclaration.Converter,
                    flags: ScrollbarArrowColorDeclaration.Flags)
            },
            {
                ScrollbarBaseColorDeclaration.Name, new DeclarationInfo(
                    name: ScrollbarBaseColorDeclaration.Name,
                    converter: ScrollbarBaseColorDeclaration.Converter,
                    flags: ScrollbarBaseColorDeclaration.Flags)
            },
            {
                ScrollbarDarkshadowColorDeclaration.Name, new DeclarationInfo(
                    name: ScrollbarDarkshadowColorDeclaration.Name,
                    converter: ScrollbarDarkshadowColorDeclaration.Converter,
                    flags: ScrollbarDarkshadowColorDeclaration.Flags)
            },
            {
                ScrollbarFaceColorDeclaration.Name, new DeclarationInfo(
                    name: ScrollbarFaceColorDeclaration.Name,
                    converter: ScrollbarFaceColorDeclaration.Converter,
                    flags: ScrollbarFaceColorDeclaration.Flags)
            },
            {
                ScrollbarHighlightColorDeclaration.Name, new DeclarationInfo(
                    name: ScrollbarHighlightColorDeclaration.Name,
                    converter: ScrollbarHighlightColorDeclaration.Converter,
                    flags: ScrollbarHighlightColorDeclaration.Flags)
            },
            {
                ScrollbarShadowColorDeclaration.Name, new DeclarationInfo(
                    name: ScrollbarShadowColorDeclaration.Name,
                    converter: ScrollbarShadowColorDeclaration.Converter,
                    flags: ScrollbarShadowColorDeclaration.Flags)
            },
            {
                ScrollbarTrackColorDeclaration.Name, new DeclarationInfo(
                    name: ScrollbarTrackColorDeclaration.Name,
                    converter: ScrollbarTrackColorDeclaration.Converter,
                    flags: ScrollbarTrackColorDeclaration.Flags)
            },
            {
                OrderDeclaration.Name, new DeclarationInfo(
                    name: OrderDeclaration.Name,
                    converter: OrderDeclaration.Converter,
                    flags: OrderDeclaration.Flags)
            },
            {
                FlexShrinkDeclaration.Name, new DeclarationInfo(
                    name: FlexShrinkDeclaration.Name,
                    converter: FlexShrinkDeclaration.Converter,
                    shorthands: FlexShrinkDeclaration.Shorthands,
                    flags: FlexShrinkDeclaration.Flags)
            },
            {
                FlexGrowDeclaration.Name, new DeclarationInfo(
                    name: FlexGrowDeclaration.Name,
                    converter: FlexGrowDeclaration.Converter,
                    shorthands: FlexGrowDeclaration.Shorthands,
                    flags: FlexGrowDeclaration.Flags)
            },
            {
                FlexBasisDeclaration.Name, new DeclarationInfo(
                    name: FlexBasisDeclaration.Name,
                    converter: FlexBasisDeclaration.Converter,
                    shorthands: FlexBasisDeclaration.Shorthands,
                    flags: FlexBasisDeclaration.Flags)
            },
            {
                JustifyContentDeclaration.Name, new DeclarationInfo(
                    name: JustifyContentDeclaration.Name,
                    converter: JustifyContentDeclaration.Converter,
                    flags: JustifyContentDeclaration.Flags)
            },
            {
                AlignContentDeclaration.Name, new DeclarationInfo(
                    name: AlignContentDeclaration.Name,
                    converter: AlignContentDeclaration.Converter,
                    flags: AlignContentDeclaration.Flags)
            },
            {
                AlignSelfDeclaration.Name, new DeclarationInfo(
                    name: AlignSelfDeclaration.Name,
                    converter: AlignSelfDeclaration.Converter,
                    flags: AlignSelfDeclaration.Flags)
            },
            {
                AlignItemsDeclaration.Name, new DeclarationInfo(
                    name: AlignItemsDeclaration.Name,
                    converter: AlignItemsDeclaration.Converter,
                    flags: AlignItemsDeclaration.Flags)
            },
            {
                FlexDirectionDeclaration.Name, new DeclarationInfo(
                    name: FlexDirectionDeclaration.Name,
                    converter: FlexDirectionDeclaration.Converter,
                    shorthands: FlexDirectionDeclaration.Shorthands,
                    flags: FlexDirectionDeclaration.Flags)
            },
            {
                FlexWrapDeclaration.Name, new DeclarationInfo(
                    name: FlexWrapDeclaration.Name,
                    converter: FlexWrapDeclaration.Converter,
                    shorthands: FlexWrapDeclaration.Shorthands,
                    flags: FlexWrapDeclaration.Flags)
            },
            {
                FlexDeclaration.Name, new DeclarationInfo(
                    name: FlexDeclaration.Name,
                    converter: FlexDeclaration.Converter,
                    longhands: FlexDeclaration.Longhands,
                    flags: FlexDeclaration.Flags)
            },
            {
                FlexFlowDeclaration.Name, new DeclarationInfo(
                    name: FlexFlowDeclaration.Name,
                    converter: FlexFlowDeclaration.Converter,
                    longhands: FlexFlowDeclaration.Longhands,
                    flags: FlexFlowDeclaration.Flags)
            },
            {
                GridTemplateColumnsDeclaration.Name, new DeclarationInfo(
                    name: GridTemplateColumnsDeclaration.Name,
                    converter: GridTemplateColumnsDeclaration.Converter,
                    shorthands: GridTemplateAreasDeclaration.Shorthands,
                    flags: GridTemplateColumnsDeclaration.Flags)
            },
            {
                GridTemplateRowsDeclaration.Name, new DeclarationInfo(
                    name: GridTemplateRowsDeclaration.Name,
                    converter: GridTemplateRowsDeclaration.Converter,
                    shorthands: GridTemplateAreasDeclaration.Shorthands,
                    flags: GridTemplateRowsDeclaration.Flags)
            },
            {
                GridTemplateAreasDeclaration.Name, new DeclarationInfo(
                    name: GridTemplateAreasDeclaration.Name,
                    converter: GridTemplateAreasDeclaration.Converter,
                    shorthands: GridTemplateAreasDeclaration.Shorthands,
                    flags: GridTemplateAreasDeclaration.Flags)
            },
            {
                GridTemplateDeclaration.Name, new DeclarationInfo(
                    name: GridTemplateDeclaration.Name,
                    converter: GridTemplateDeclaration.Converter,
                    longhands: GridTemplateDeclaration.Longhands,
                    flags: GridTemplateDeclaration.Flags)
            },
            {
                GridAutoColumnsDeclaration.Name, new DeclarationInfo(
                    name: GridAutoColumnsDeclaration.Name,
                    converter: GridAutoColumnsDeclaration.Converter,
                    flags: GridAutoColumnsDeclaration.Flags)
            },
            {
                GridAutoRowsDeclaration.Name, new DeclarationInfo(
                    name: GridAutoRowsDeclaration.Name,
                    converter: GridAutoRowsDeclaration.Converter,
                    flags: GridAutoRowsDeclaration.Flags)
            },
            {
                GridAutoFlowDeclaration.Name, new DeclarationInfo(
                    name: GridAutoFlowDeclaration.Name,
                    converter: GridAutoFlowDeclaration.Converter,
                    flags: GridAutoFlowDeclaration.Flags)
            },
            {
                GridDeclaration.Name, new DeclarationInfo(
                    name: GridDeclaration.Name,
                    converter: GridDeclaration.Converter,
                    longhands: GridDeclaration.Longhands,
                    flags: GridDeclaration.Flags)
            },
            {
                GridRowStartDeclaration.Name, new DeclarationInfo(
                    name: GridRowStartDeclaration.Name,
                    converter: GridRowStartDeclaration.Converter,
                    shorthands: GridRowStartDeclaration.Shorthands,
                    flags: GridRowStartDeclaration.Flags)
            },
            {
                GridColumnStartDeclaration.Name, new DeclarationInfo(
                    name: GridColumnStartDeclaration.Name,
                    converter: GridColumnStartDeclaration.Converter,
                    shorthands: GridColumnStartDeclaration.Shorthands,
                    flags: GridColumnStartDeclaration.Flags)
            },
            {
                GridRowEndDeclaration.Name, new DeclarationInfo(
                    name: GridRowEndDeclaration.Name,
                    converter: GridRowEndDeclaration.Converter,
                    shorthands: GridRowEndDeclaration.Shorthands,
                    flags: GridRowEndDeclaration.Flags)
            },
            {
                GridColumnEndDeclaration.Name, new DeclarationInfo(
                    name: GridColumnEndDeclaration.Name,
                    converter: GridColumnEndDeclaration.Converter,
                    shorthands: GridColumnEndDeclaration.Shorthands,
                    flags: GridColumnEndDeclaration.Flags)
            },
            {
                GridRowDeclaration.Name, new DeclarationInfo(
                    name: GridRowDeclaration.Name,
                    converter: GridRowDeclaration.Converter,
                    longhands: GridRowDeclaration.Longhands,
                    flags: GridRowDeclaration.Flags)
            },
            {
                GridColumnDeclaration.Name, new DeclarationInfo(
                    name: GridColumnDeclaration.Name,
                    converter: GridColumnDeclaration.Converter,
                    longhands: GridColumnDeclaration.Longhands,
                    flags: GridColumnDeclaration.Flags)
            },
            {
                GridAreaDeclaration.Name, new DeclarationInfo(
                    name: GridAreaDeclaration.Name,
                    converter: GridAreaDeclaration.Converter,
                    longhands: GridAreaDeclaration.Longhands,
                    flags: GridAreaDeclaration.Flags)
            },
            {
                GridRowGapDeclaration.Name, new DeclarationInfo(
                    name: GridRowGapDeclaration.Name,
                    converter: GridRowGapDeclaration.Converter,
                    shorthands: GridRowGapDeclaration.Shorthands,
                    flags: GridRowGapDeclaration.Flags)
            },
            {
                GridColumnGapDeclaration.Name, new DeclarationInfo(
                    name: GridColumnGapDeclaration.Name,
                    converter: GridColumnGapDeclaration.Converter,
                    shorthands: GridColumnGapDeclaration.Shorthands,
                    flags: GridColumnGapDeclaration.Flags)
            },
            {
                GridGapDeclaration.Name, new DeclarationInfo(
                    name: GridGapDeclaration.Name,
                    converter: GridGapDeclaration.Converter,
                    longhands: GridGapDeclaration.Longhands,
                    flags: GridGapDeclaration.Flags)
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
            if (propertyName.StartsWith("--"))
            {
                return new DeclarationInfo(propertyName, ValueConverters.Any, flags: PropertyFlags.Inherited);
            }

            return new DeclarationInfo(propertyName, ValueConverters.Any, flags: PropertyFlags.Unknown);
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
