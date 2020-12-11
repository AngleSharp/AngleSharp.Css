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
                BookmarkLabelDeclaration.Name, new DeclarationInfo(
                    name: BookmarkLabelDeclaration.Name,
                    converter: BookmarkLabelDeclaration.Converter,
                    initialValue: BookmarkLabelDeclaration.InitialValue,
                    flags: BookmarkLabelDeclaration.Flags)
            },
            {
                BookmarkLevelDeclaration.Name, new DeclarationInfo(
                    name: BookmarkLevelDeclaration.Name,
                    converter: BookmarkLevelDeclaration.Converter,
                    initialValue: BookmarkLevelDeclaration.InitialValue,
                    flags: BookmarkLevelDeclaration.Flags)
            },
            {
                BookmarkStateDeclaration.Name, new DeclarationInfo(
                    name: BookmarkStateDeclaration.Name,
                    converter: BookmarkStateDeclaration.Converter,
                    initialValue: BookmarkStateDeclaration.InitialValue,
                    flags: BookmarkStateDeclaration.Flags)
            },
            {
                FootnoteDisplayDeclaration.Name, new DeclarationInfo(
                    name: FootnoteDisplayDeclaration.Name,
                    converter: FootnoteDisplayDeclaration.Converter,
                    initialValue: FootnoteDisplayDeclaration.InitialValue,
                    flags: FootnoteDisplayDeclaration.Flags)
            },
            {
                FootnotePolicyDeclaration.Name, new DeclarationInfo(
                    name: FootnotePolicyDeclaration.Name,
                    converter: FootnotePolicyDeclaration.Converter,
                    initialValue: FootnotePolicyDeclaration.InitialValue,
                    flags: FootnotePolicyDeclaration.Flags)
            },
            {
                RunningDeclaration.Name, new DeclarationInfo(
                    name: RunningDeclaration.Name,
                    converter: RunningDeclaration.Converter,
                    initialValue: RunningDeclaration.InitialValue,
                    flags: RunningDeclaration.Flags)
            },
            {
                StringSetDeclaration.Name, new DeclarationInfo(
                    name: StringSetDeclaration.Name,
                    converter: StringSetDeclaration.Converter,
                    initialValue: StringSetDeclaration.InitialValue,
                    flags: StringSetDeclaration.Flags)
            },
            {
                CaptionSideDeclaration.Name, new DeclarationInfo(
                    name: CaptionSideDeclaration.Name,
                    converter: CaptionSideDeclaration.Converter,
                    initialValue: CaptionSideDeclaration.InitialValue,
                    flags: CaptionSideDeclaration.Flags)
            },
            {
                CursorDeclaration.Name, new DeclarationInfo(
                    name: CursorDeclaration.Name,
                    converter: CursorDeclaration.Converter,
                    initialValue: CursorDeclaration.InitialValue,
                    flags: CursorDeclaration.Flags)
            },
            {
                EmptyCellsDeclaration.Name, new DeclarationInfo(
                    name: EmptyCellsDeclaration.Name,
                    converter: EmptyCellsDeclaration.Converter,
                    initialValue: EmptyCellsDeclaration.InitialValue,
                    flags: EmptyCellsDeclaration.Flags)
            },
            {
                OrphansDeclaration.Name, new DeclarationInfo(
                    name: OrphansDeclaration.Name,
                    converter: OrphansDeclaration.Converter,
                    initialValue: OrphansDeclaration.InitialValue,
                    flags: OrphansDeclaration.Flags)
            },
            {
                QuotesDeclaration.Name, new DeclarationInfo(
                    name: QuotesDeclaration.Name,
                    converter: QuotesDeclaration.Converter,
                    initialValue: QuotesDeclaration.InitialValue,
                    flags: QuotesDeclaration.Flags)
            },
            {
                TableLayoutDeclaration.Name, new DeclarationInfo(
                    name: TableLayoutDeclaration.Name,
                    converter: TableLayoutDeclaration.Converter,
                    initialValue: TableLayoutDeclaration.InitialValue,
                    flags: TableLayoutDeclaration.Flags)
            },
            {
                UnicodeBidiDeclaration.Name, new DeclarationInfo(
                    name: UnicodeBidiDeclaration.Name,
                    converter: UnicodeBidiDeclaration.Converter,
                    initialValue: UnicodeBidiDeclaration.InitialValue,
                    flags: UnicodeBidiDeclaration.Flags)
            },
            {
                WidowsDeclaration.Name, new DeclarationInfo(
                    name: WidowsDeclaration.Name,
                    converter: WidowsDeclaration.Converter,
                    initialValue: WidowsDeclaration.InitialValue,
                    flags: WidowsDeclaration.Flags)
            },
            {
                ContentDeclaration.Name, new DeclarationInfo(
                    name: ContentDeclaration.Name,
                    converter: ContentDeclaration.Converter,
                    initialValue: ContentDeclaration.InitialValue,
                    flags: ContentDeclaration.Flags)
            },
            {
                BoxDecorationBreakDeclaration.Name, new DeclarationInfo(
                    name: BoxDecorationBreakDeclaration.Name,
                    converter: BoxDecorationBreakDeclaration.Converter,
                    initialValue: BoxDecorationBreakDeclaration.InitialValue,
                    flags: BoxDecorationBreakDeclaration.Flags)
            },
            {
                BoxShadowDeclaration.Name, new DeclarationInfo(
                    name: BoxShadowDeclaration.Name,
                    converter: BoxShadowDeclaration.Converter,
                    initialValue: BoxShadowDeclaration.InitialValue,
                    flags: BoxShadowDeclaration.Flags)
            },
            {
                ClearDeclaration.Name, new DeclarationInfo(
                    name: ClearDeclaration.Name,
                    converter: ClearDeclaration.Converter,
                    initialValue: ClearDeclaration.InitialValue,
                    flags: ClearDeclaration.Flags)
            },
            {
                DisplayDeclaration.Name, new DeclarationInfo(
                    name: DisplayDeclaration.Name,
                    converter: DisplayDeclaration.Converter,
                    initialValue: DisplayDeclaration.InitialValue,
                    flags: DisplayDeclaration.Flags)
            },
            {
                FloatDeclaration.Name, new DeclarationInfo(
                    name: FloatDeclaration.Name,
                    converter: FloatDeclaration.Converter,
                    initialValue: FloatDeclaration.InitialValue,
                    flags: FloatDeclaration.Flags)
            },
            {
                OverflowDeclaration.Name, new DeclarationInfo(
                    name: OverflowDeclaration.Name,
                    converter: OverflowDeclaration.Converter,
                    initialValue: OverflowDeclaration.InitialValue,
                    flags: OverflowDeclaration.Flags)
            },
            {
                OverflowXDeclaration.Name, new DeclarationInfo(
                    name: OverflowXDeclaration.Name,
                    converter: OverflowXDeclaration.Converter,
                    initialValue: OverflowXDeclaration.InitialValue,
                    flags: OverflowXDeclaration.Flags)
            },
            {
                OverflowYDeclaration.Name, new DeclarationInfo(
                    name: OverflowYDeclaration.Name,
                    converter: OverflowYDeclaration.Converter,
                    initialValue: OverflowYDeclaration.InitialValue,
                    flags: OverflowYDeclaration.Flags)
            },
            {
                PositionDeclaration.Name, new DeclarationInfo(
                    name: PositionDeclaration.Name,
                    converter: PositionDeclaration.Converter,
                    initialValue: PositionDeclaration.InitialValue,
                    flags: PositionDeclaration.Flags)
            },
            {
                ZIndexDeclaration.Name, new DeclarationInfo(
                    name: ZIndexDeclaration.Name,
                    converter: ZIndexDeclaration.Converter,
                    initialValue: ZIndexDeclaration.InitialValue,
                    flags: ZIndexDeclaration.Flags)
            },
            {
                CounterResetDeclaration.Name, new DeclarationInfo(
                    name: CounterResetDeclaration.Name,
                    converter: CounterResetDeclaration.Converter,
                    initialValue: CounterResetDeclaration.InitialValue,
                    flags: CounterResetDeclaration.Flags)
            },
            {
                CounterIncrementDeclaration.Name, new DeclarationInfo(
                    name: CounterIncrementDeclaration.Name,
                    converter: CounterIncrementDeclaration.Converter,
                    initialValue: CounterIncrementDeclaration.InitialValue,
                    flags: CounterIncrementDeclaration.Flags)
            },
            {
                ObjectFitDeclaration.Name, new DeclarationInfo(
                    name: ObjectFitDeclaration.Name,
                    converter: ObjectFitDeclaration.Converter,
                    initialValue: ObjectFitDeclaration.InitialValue,
                    flags: ObjectFitDeclaration.Flags)
            },
            {
                ObjectPositionDeclaration.Name, new DeclarationInfo(
                    name: ObjectPositionDeclaration.Name,
                    converter: ObjectPositionDeclaration.Converter,
                    initialValue: ObjectPositionDeclaration.InitialValue,
                    flags: ObjectPositionDeclaration.Flags)
            },
            {
                StrokeDasharrayDeclaration.Name, new DeclarationInfo(
                    name: StrokeDasharrayDeclaration.Name,
                    converter: StrokeDasharrayDeclaration.Converter,
                    initialValue: StrokeDasharrayDeclaration.InitialValue,
                    flags: StrokeDasharrayDeclaration.Flags)
            },
            {
                StrokeDashoffsetDeclaration.Name, new DeclarationInfo(
                    name: StrokeDashoffsetDeclaration.Name,
                    converter: StrokeDashoffsetDeclaration.Converter,
                    initialValue: StrokeDashoffsetDeclaration.InitialValue,
                    flags: StrokeDashoffsetDeclaration.Flags)
            },
            {
                StrokeLinecapDeclaration.Name, new DeclarationInfo(
                    name: StrokeLinecapDeclaration.Name,
                    converter: StrokeLinecapDeclaration.Converter,
                    initialValue: StrokeLinecapDeclaration.InitialValue,
                    flags: StrokeLinecapDeclaration.Flags)
            },
            {
                StrokeLinejoinDeclaration.Name, new DeclarationInfo(
                    name: StrokeLinejoinDeclaration.Name,
                    converter: StrokeLinejoinDeclaration.Converter,
                    initialValue: StrokeLinejoinDeclaration.InitialValue,
                    flags: StrokeLinejoinDeclaration.Flags)
            },
            {
                StrokeMiterlimitDeclaration.Name, new DeclarationInfo(
                    name: StrokeMiterlimitDeclaration.Name,
                    converter: StrokeMiterlimitDeclaration.Converter,
                    initialValue: StrokeMiterlimitDeclaration.InitialValue,
                    flags: StrokeMiterlimitDeclaration.Flags)
            },
            {
                StrokeOpacityDeclaration.Name, new DeclarationInfo(
                    name: StrokeOpacityDeclaration.Name,
                    converter: StrokeOpacityDeclaration.Converter,
                    initialValue: StrokeOpacityDeclaration.InitialValue,
                    flags: StrokeOpacityDeclaration.Flags)
            },
            {
                StrokeDeclaration.Name, new DeclarationInfo(
                    name: StrokeDeclaration.Name,
                    converter: StrokeDeclaration.Converter,
                    initialValue: StrokeDeclaration.InitialValue,
                    flags: StrokeDeclaration.Flags)
            },
            {
                StrokeWidthDeclaration.Name, new DeclarationInfo(
                    name: StrokeWidthDeclaration.Name,
                    converter: StrokeWidthDeclaration.Converter,
                    initialValue: StrokeWidthDeclaration.InitialValue,
                    flags: StrokeWidthDeclaration.Flags)
            },
            {
                DirectionDeclaration.Name, new DeclarationInfo(
                    name: DirectionDeclaration.Name,
                    converter: DirectionDeclaration.Converter,
                    initialValue: DirectionDeclaration.InitialValue,
                    flags: DirectionDeclaration.Flags)
            },
            {
                OverflowWrapDeclaration.Name, new DeclarationInfo(
                    name: OverflowWrapDeclaration.Name,
                    converter: OverflowWrapDeclaration.Converter,
                    initialValue: OverflowWrapDeclaration.InitialValue,
                    flags: OverflowWrapDeclaration.Flags)
            },
            {
                WordWrapDeclaration.Name, new DeclarationInfo(
                    name: WordWrapDeclaration.Name,
                    converter: WordWrapDeclaration.Converter,
                    initialValue: WordWrapDeclaration.InitialValue,
                    flags: WordWrapDeclaration.Flags)
            },
            {
                PerspectiveOriginDeclaration.Name, new DeclarationInfo(
                    name: PerspectiveOriginDeclaration.Name,
                    converter: PerspectiveOriginDeclaration.Converter,
                    initialValue: PerspectiveOriginDeclaration.InitialValue,
                    flags: PerspectiveOriginDeclaration.Flags)
            },
            {
                PerspectiveDeclaration.Name, new DeclarationInfo(
                    name: PerspectiveDeclaration.Name,
                    converter: PerspectiveDeclaration.Converter,
                    initialValue: PerspectiveDeclaration.InitialValue,
                    flags: PerspectiveDeclaration.Flags)
            },
            {
                BackfaceVisibilityDeclaration.Name, new DeclarationInfo(
                    name: BackfaceVisibilityDeclaration.Name,
                    converter: BackfaceVisibilityDeclaration.Converter,
                    initialValue: BackfaceVisibilityDeclaration.InitialValue,
                    flags: BackfaceVisibilityDeclaration.Flags)
            },
            {
                ClipDeclaration.Name, new DeclarationInfo(
                    name: ClipDeclaration.Name,
                    converter: ClipDeclaration.Converter,
                    initialValue: ClipDeclaration.InitialValue,
                    flags: ClipDeclaration.Flags)
            },
            {
                OpacityDeclaration.Name, new DeclarationInfo(
                    name: OpacityDeclaration.Name,
                    converter: OpacityDeclaration.Converter,
                    initialValue: OpacityDeclaration.InitialValue,
                    flags: OpacityDeclaration.Flags)
            },
            {
                VisibilityDeclaration.Name, new DeclarationInfo(
                    name: VisibilityDeclaration.Name,
                    converter: VisibilityDeclaration.Converter,
                    initialValue: VisibilityDeclaration.InitialValue,
                    flags: VisibilityDeclaration.Flags)
            },
            {
                BottomDeclaration.Name, new DeclarationInfo(
                    name: BottomDeclaration.Name,
                    converter: BottomDeclaration.Converter,
                    initialValue: BottomDeclaration.InitialValue,
                    flags: BottomDeclaration.Flags)
            },
            {
                HeightDeclaration.Name, new DeclarationInfo(
                    name: HeightDeclaration.Name,
                    converter: HeightDeclaration.Converter,
                    initialValue: HeightDeclaration.InitialValue,
                    flags: HeightDeclaration.Flags)
            },
            {
                LeftDeclaration.Name, new DeclarationInfo(
                    name: LeftDeclaration.Name,
                    converter: LeftDeclaration.Converter,
                    initialValue: LeftDeclaration.InitialValue,
                    flags: LeftDeclaration.Flags)
            },
            {
                MaxHeightDeclaration.Name, new DeclarationInfo(
                    name: MaxHeightDeclaration.Name,
                    converter: MaxHeightDeclaration.Converter,
                    initialValue: MaxHeightDeclaration.InitialValue,
                    flags: MaxHeightDeclaration.Flags)
            },
            {
                MaxWidthDeclaration.Name, new DeclarationInfo(
                    name: MaxWidthDeclaration.Name,
                    converter: MaxWidthDeclaration.Converter,
                    initialValue: MaxWidthDeclaration.InitialValue,
                    flags: MaxWidthDeclaration.Flags)
            },
            {
                MinHeightDeclaration.Name, new DeclarationInfo(
                    name: MinHeightDeclaration.Name,
                    converter: MinHeightDeclaration.Converter,
                    initialValue: MinHeightDeclaration.InitialValue,
                    flags: MinHeightDeclaration.Flags)
            },
            {
                MinWidthDeclaration.Name, new DeclarationInfo(
                    name: MinWidthDeclaration.Name,
                    converter: MinWidthDeclaration.Converter,
                    initialValue: MinWidthDeclaration.InitialValue,
                    flags: MinWidthDeclaration.Flags)
            },
            {
                RightDeclaration.Name, new DeclarationInfo(
                    name: RightDeclaration.Name,
                    converter: RightDeclaration.Converter,
                    initialValue: RightDeclaration.InitialValue,
                    flags: RightDeclaration.Flags)
            },
            {
                TopDeclaration.Name, new DeclarationInfo(
                    name: TopDeclaration.Name,
                    converter: TopDeclaration.Converter,
                    initialValue: TopDeclaration.InitialValue,
                    flags: TopDeclaration.Flags)
            },
            {
                WidthDeclaration.Name, new DeclarationInfo(
                    name: WidthDeclaration.Name,
                    converter: WidthDeclaration.Converter,
                    initialValue: WidthDeclaration.InitialValue,
                    flags: WidthDeclaration.Flags)
            },
            {
                ColorDeclaration.Name, new DeclarationInfo(
                    name: ColorDeclaration.Name,
                    converter: ColorDeclaration.Converter,
                    initialValue: ColorDeclaration.InitialValue,
                    flags: ColorDeclaration.Flags)
            },
            {
                WordSpacingDeclaration.Name, new DeclarationInfo(
                    name: WordSpacingDeclaration.Name,
                    converter: WordSpacingDeclaration.Converter,
                    initialValue: WordSpacingDeclaration.InitialValue,
                    flags: WordSpacingDeclaration.Flags)
            },
            {
                LineHeightDeclaration.Name, new DeclarationInfo(
                    name: LineHeightDeclaration.Name,
                    converter: LineHeightDeclaration.Converter,
                    initialValue: LineHeightDeclaration.InitialValue,
                    flags: LineHeightDeclaration.Flags,
                    shorthands: LineHeightDeclaration.Shorthands)
            },
            {
                LetterSpacingDeclaration.Name, new DeclarationInfo(
                    name: LetterSpacingDeclaration.Name,
                    converter: LetterSpacingDeclaration.Converter,
                    initialValue: LetterSpacingDeclaration.InitialValue,
                    flags: LetterSpacingDeclaration.Flags)
            },
            {
                FontSizeAdjustDeclaration.Name, new DeclarationInfo(
                    name: FontSizeAdjustDeclaration.Name,
                    converter: FontSizeAdjustDeclaration.Converter,
                    initialValue: FontSizeAdjustDeclaration.InitialValue,
                    flags: FontSizeAdjustDeclaration.Flags)
            },
            {
                BreakAfterDeclaration.Name, new DeclarationInfo(
                    name: BreakAfterDeclaration.Name,
                    converter: BreakAfterDeclaration.Converter,
                    initialValue: BreakAfterDeclaration.InitialValue,
                    flags: BreakAfterDeclaration.Flags)
            },
            {
                BreakBeforeDeclaration.Name, new DeclarationInfo(
                    name: BreakBeforeDeclaration.Name,
                    converter: BreakBeforeDeclaration.Converter,
                    initialValue: BreakBeforeDeclaration.InitialValue,
                    flags: BreakBeforeDeclaration.Flags)
            },
            {
                BreakInsideDeclaration.Name, new DeclarationInfo(
                    name: BreakInsideDeclaration.Name,
                    converter: BreakInsideDeclaration.Converter,
                    initialValue: BreakInsideDeclaration.InitialValue,
                    flags: BreakInsideDeclaration.Flags)
            },
            {
                PageBreakAfterDeclaration.Name, new DeclarationInfo(
                    name: PageBreakAfterDeclaration.Name,
                    converter: PageBreakAfterDeclaration.Converter,
                    initialValue: PageBreakAfterDeclaration.InitialValue,
                    flags: PageBreakAfterDeclaration.Flags)
            },
            {
                PageBreakBeforeDeclaration.Name, new DeclarationInfo(
                    name: PageBreakBeforeDeclaration.Name,
                    converter: PageBreakBeforeDeclaration.Converter,
                    initialValue: PageBreakBeforeDeclaration.InitialValue,
                    flags: PageBreakBeforeDeclaration.Flags)
            },
            {
                PageBreakInsideDeclaration.Name, new DeclarationInfo(
                    name: PageBreakInsideDeclaration.Name,
                    converter: PageBreakInsideDeclaration.Converter,
                    initialValue: PageBreakInsideDeclaration.InitialValue,
                    flags: PageBreakInsideDeclaration.Flags)
            },
            {
                TransformOriginDeclaration.Name, new DeclarationInfo(
                    name: TransformOriginDeclaration.Name,
                    converter: TransformOriginDeclaration.Converter,
                    initialValue: TransformOriginDeclaration.InitialValue,
                    flags: TransformOriginDeclaration.Flags)
            },
            {
                TransformDeclaration.Name, new DeclarationInfo(
                    name: TransformDeclaration.Name,
                    converter: TransformDeclaration.Converter,
                    initialValue: TransformDeclaration.InitialValue,
                    flags: TransformDeclaration.Flags)
            },
            {
                TransformStyleDeclaration.Name, new DeclarationInfo(
                    name: TransformStyleDeclaration.Name,
                    converter: TransformStyleDeclaration.Converter,
                    initialValue: TransformStyleDeclaration.InitialValue,
                    flags: TransformStyleDeclaration.Flags)
            },
            {
                ColumnCountDeclaration.Name, new DeclarationInfo(
                    name: ColumnCountDeclaration.Name,
                    converter: ColumnCountDeclaration.Converter,
                    initialValue: ColumnCountDeclaration.InitialValue,
                    flags: ColumnCountDeclaration.Flags,
                    shorthands: ColumnCountDeclaration.Shorthands)
            },
            {
                ColumnFillDeclaration.Name, new DeclarationInfo(
                    name: ColumnFillDeclaration.Name,
                    converter: ColumnFillDeclaration.Converter,
                    initialValue: ColumnFillDeclaration.InitialValue,
                    flags: ColumnFillDeclaration.Flags)
            },
            {
                ColumnGapDeclaration.Name, new DeclarationInfo(
                    name: ColumnGapDeclaration.Name,
                    converter: ColumnGapDeclaration.Converter,
                    initialValue: ColumnGapDeclaration.InitialValue,
                    shorthands: ColumnGapDeclaration.Shorthands,
                    flags: ColumnGapDeclaration.Flags)
            },
            {
                RowGapDeclaration.Name, new DeclarationInfo(
                    name: RowGapDeclaration.Name,
                    converter: RowGapDeclaration.Converter,
                    initialValue: RowGapDeclaration.InitialValue,
                    shorthands: RowGapDeclaration.Shorthands,
                    flags: RowGapDeclaration.Flags)
            },
            {
                GapDeclaration.Name, new DeclarationInfo(
                    name: GapDeclaration.Name,
                    converter: GapDeclaration.Converter,
                    initialValue: GapDeclaration.InitialValue,
                    longhands: GapDeclaration.Longhands,
                    flags: GapDeclaration.Flags)
            },
            {
                ColumnSpanDeclaration.Name, new DeclarationInfo(
                    name: ColumnSpanDeclaration.Name,
                    converter: ColumnSpanDeclaration.Converter,
                    initialValue: ColumnSpanDeclaration.InitialValue,
                    flags: ColumnSpanDeclaration.Flags)
            },
            {
                ColumnWidthDeclaration.Name, new DeclarationInfo(
                    name: ColumnWidthDeclaration.Name,
                    converter: ColumnWidthDeclaration.Converter,
                    initialValue: ColumnWidthDeclaration.InitialValue,
                    flags: ColumnWidthDeclaration.Flags,
                    shorthands: ColumnWidthDeclaration.Shorthands)
            },
            {
                BorderCollapseDeclaration.Name, new DeclarationInfo(
                    name: BorderCollapseDeclaration.Name,
                    converter: BorderCollapseDeclaration.Converter,
                    initialValue: BorderCollapseDeclaration.InitialValue,
                    flags: BorderCollapseDeclaration.Flags)
            },
            {
                BorderSpacingDeclaration.Name, new DeclarationInfo(
                    name: BorderSpacingDeclaration.Name,
                    converter: BorderSpacingDeclaration.Converter,
                    initialValue: BorderSpacingDeclaration.InitialValue,
                    flags: BorderSpacingDeclaration.Flags)
            },
            {
                WordBreakDeclaration.Name, new DeclarationInfo(
                    name: WordBreakDeclaration.Name,
                    converter: WordBreakDeclaration.Converter,
                    initialValue: WordBreakDeclaration.InitialValue,
                    flags: WordBreakDeclaration.Flags)
            },
            {
                WhiteSpaceDeclaration.Name, new DeclarationInfo(
                    name: WhiteSpaceDeclaration.Name,
                    converter: WhiteSpaceDeclaration.Converter,
                    initialValue: WhiteSpaceDeclaration.InitialValue,
                    flags: WhiteSpaceDeclaration.Flags)
            },
            {
                VerticalAlignDeclaration.Name, new DeclarationInfo(
                    name: VerticalAlignDeclaration.Name,
                    converter: VerticalAlignDeclaration.Converter,
                    initialValue: VerticalAlignDeclaration.InitialValue,
                    flags: VerticalAlignDeclaration.Flags)
            },
            {
                TextShadowDeclaration.Name, new DeclarationInfo(
                    name: TextShadowDeclaration.Name,
                    converter: TextShadowDeclaration.Converter,
                    initialValue: TextShadowDeclaration.InitialValue,
                    flags: TextShadowDeclaration.Flags)
            },
            {
                TextJustifyDeclaration.Name, new DeclarationInfo(
                    name: TextJustifyDeclaration.Name,
                    converter: TextJustifyDeclaration.Converter,
                    initialValue: TextJustifyDeclaration.InitialValue,
                    flags: TextJustifyDeclaration.Flags)
            },
            {
                TextIndentDeclaration.Name, new DeclarationInfo(
                    name: TextIndentDeclaration.Name,
                    converter: TextIndentDeclaration.Converter,
                    initialValue: TextIndentDeclaration.InitialValue,
                    flags: TextIndentDeclaration.Flags)
            },
            {
                TextAnchorDeclaration.Name, new DeclarationInfo(
                    name: TextAnchorDeclaration.Name,
                    converter: TextAnchorDeclaration.Converter,
                    initialValue: TextAnchorDeclaration.InitialValue,
                    flags: TextAnchorDeclaration.Flags)
            },
            {
                TextAlignDeclaration.Name, new DeclarationInfo(
                    name: TextAlignDeclaration.Name,
                    converter: TextAlignDeclaration.Converter,
                    initialValue: TextAlignDeclaration.InitialValue,
                    flags: TextAlignDeclaration.Flags)
            },
            {
                TextAlignLastDeclaration.Name, new DeclarationInfo(
                    name: TextAlignLastDeclaration.Name,
                    converter: TextAlignLastDeclaration.Converter,
                    initialValue: TextAlignLastDeclaration.InitialValue,
                    flags: TextAlignLastDeclaration.Flags)
            },
            {
                TextTransformDeclaration.Name, new DeclarationInfo(
                    name: TextTransformDeclaration.Name,
                    converter: TextTransformDeclaration.Converter,
                    initialValue: TextTransformDeclaration.InitialValue,
                    flags: TextTransformDeclaration.Flags)
            },
            {
                ListStyleImageDeclaration.Name, new DeclarationInfo(
                    name: ListStyleImageDeclaration.Name,
                    converter: ListStyleImageDeclaration.Converter,
                    initialValue: ListStyleImageDeclaration.InitialValue,
                    flags: ListStyleImageDeclaration.Flags,
                    shorthands: ListStyleImageDeclaration.Shorthands)
            },
            {
                ListStylePositionDeclaration.Name, new DeclarationInfo(
                    name: ListStylePositionDeclaration.Name,
                    converter: ListStylePositionDeclaration.Converter,
                    initialValue: ListStylePositionDeclaration.InitialValue,
                    flags: ListStylePositionDeclaration.Flags,
                    shorthands: ListStylePositionDeclaration.Shorthands)
            },
            {
                ListStyleTypeDeclaration.Name, new DeclarationInfo(
                    name: ListStyleTypeDeclaration.Name,
                    converter: ListStyleTypeDeclaration.Converter,
                    initialValue: ListStyleTypeDeclaration.InitialValue,
                    flags: ListStyleTypeDeclaration.Flags,
                    shorthands: ListStyleTypeDeclaration.Shorthands)
            },
            {
                FontFamilyDeclaration.Name, new DeclarationInfo(
                    name: FontFamilyDeclaration.Name,
                    converter: FontFamilyDeclaration.Converter,
                    initialValue: FontFamilyDeclaration.InitialValue,
                    flags: FontFamilyDeclaration.Flags,
                    shorthands: FontFamilyDeclaration.Shorthands)
            },
            {
                FontSizeDeclaration.Name, new DeclarationInfo(
                    name: FontSizeDeclaration.Name,
                    converter: FontSizeDeclaration.Converter,
                    initialValue: FontSizeDeclaration.InitialValue,
                    flags: FontSizeDeclaration.Flags,
                    shorthands: FontSizeDeclaration.Shorthands)
            },
            {
                FontStyleDeclaration.Name, new DeclarationInfo(
                    name: FontStyleDeclaration.Name,
                    converter: FontStyleDeclaration.Converter,
                    initialValue: FontStyleDeclaration.InitialValue,
                    flags: FontStyleDeclaration.Flags,
                    shorthands: FontStyleDeclaration.Shorthands)
            },
            {
                FontStretchDeclaration.Name, new DeclarationInfo(
                    name: FontStretchDeclaration.Name,
                    converter: FontStretchDeclaration.Converter,
                    initialValue: FontStretchDeclaration.InitialValue,
                    flags: FontStretchDeclaration.Flags)
            },
            {
                FontVariantDeclaration.Name, new DeclarationInfo(
                    name: FontVariantDeclaration.Name,
                    converter: FontVariantDeclaration.Converter,
                    initialValue: FontVariantDeclaration.InitialValue,
                    flags: FontVariantDeclaration.Flags,
                    shorthands: FontVariantDeclaration.Shorthands)
            },
            {
                FontWeightDeclaration.Name, new DeclarationInfo(
                    name: FontWeightDeclaration.Name,
                    converter: FontWeightDeclaration.Converter,
                    initialValue: FontWeightDeclaration.InitialValue,
                    flags: FontWeightDeclaration.Flags,
                    shorthands: FontWeightDeclaration.Shorthands)
            },
            {
                UnicodeRangeDeclaration.Name, new DeclarationInfo(
                    name: UnicodeRangeDeclaration.Name,
                    converter: UnicodeRangeDeclaration.Converter,
                    initialValue: UnicodeRangeDeclaration.InitialValue,
                    flags: UnicodeRangeDeclaration.Flags)
            },
            {
                SrcDeclaration.Name, new DeclarationInfo(
                    name: SrcDeclaration.Name,
                    converter: SrcDeclaration.Converter,
                    initialValue: SrcDeclaration.InitialValue,
                    flags: SrcDeclaration.Flags)
            },
            {
                ColumnRuleWidthDeclaration.Name, new DeclarationInfo(
                    name: ColumnRuleWidthDeclaration.Name,
                    converter: ColumnRuleWidthDeclaration.Converter,
                    initialValue: ColumnRuleWidthDeclaration.InitialValue,
                    flags: ColumnRuleWidthDeclaration.Flags,
                    shorthands: ColumnRuleWidthDeclaration.Shorthands)
            },
            {
                ColumnRuleStyleDeclaration.Name, new DeclarationInfo(
                    name: ColumnRuleStyleDeclaration.Name,
                    converter: ColumnRuleStyleDeclaration.Converter,
                    initialValue: ColumnRuleStyleDeclaration.InitialValue,
                    flags: ColumnRuleStyleDeclaration.Flags,
                    shorthands: ColumnRuleStyleDeclaration.Shorthands)
            },
            {
                ColumnRuleColorDeclaration.Name, new DeclarationInfo(
                    name: ColumnRuleColorDeclaration.Name,
                    converter: ColumnRuleColorDeclaration.Converter,
                    initialValue: ColumnRuleColorDeclaration.InitialValue,
                    flags: ColumnRuleColorDeclaration.Flags,
                    shorthands: ColumnRuleColorDeclaration.Shorthands)
            },
            {
                PaddingTopDeclaration.Name, new DeclarationInfo(
                    name: PaddingTopDeclaration.Name,
                    converter: PaddingTopDeclaration.Converter,
                    initialValue: PaddingTopDeclaration.InitialValue,
                    flags: PaddingTopDeclaration.Flags,
                    shorthands: PaddingTopDeclaration.Shorthands)
            },
            {
                PaddingRightDeclaration.Name, new DeclarationInfo(
                    name: PaddingRightDeclaration.Name,
                    converter: PaddingRightDeclaration.Converter,
                    initialValue: PaddingRightDeclaration.InitialValue,
                    flags: PaddingRightDeclaration.Flags,
                    shorthands: PaddingRightDeclaration.Shorthands)
            },
            {
                PaddingLeftDeclaration.Name, new DeclarationInfo(
                    name: PaddingLeftDeclaration.Name,
                    converter: PaddingLeftDeclaration.Converter,
                    initialValue: PaddingLeftDeclaration.InitialValue,
                    flags: PaddingLeftDeclaration.Flags,
                    shorthands: PaddingLeftDeclaration.Shorthands)
            },
            {
                PaddingBottomDeclaration.Name, new DeclarationInfo(
                    name: PaddingBottomDeclaration.Name,
                    converter: PaddingBottomDeclaration.Converter,
                    initialValue: PaddingBottomDeclaration.InitialValue,
                    flags: PaddingBottomDeclaration.Flags,
                    shorthands: PaddingBottomDeclaration.Shorthands)
            },
            {
                MarginTopDeclaration.Name, new DeclarationInfo(
                    name: MarginTopDeclaration.Name,
                    converter: MarginTopDeclaration.Converter,
                    initialValue: MarginTopDeclaration.InitialValue,
                    flags: MarginTopDeclaration.Flags,
                    shorthands: MarginTopDeclaration.Shorthands)
            },
            {
                MarginRightDeclaration.Name, new DeclarationInfo(
                    name: MarginRightDeclaration.Name,
                    converter: MarginRightDeclaration.Converter,
                    initialValue: MarginRightDeclaration.InitialValue,
                    flags: MarginRightDeclaration.Flags,
                    shorthands: MarginRightDeclaration.Shorthands)
            },
            {
                MarginLeftDeclaration.Name, new DeclarationInfo(
                    name: MarginLeftDeclaration.Name,
                    converter: MarginLeftDeclaration.Converter,
                    initialValue: MarginLeftDeclaration.InitialValue,
                    flags: MarginLeftDeclaration.Flags,
                    shorthands: MarginLeftDeclaration.Shorthands)
            },
            {
                MarginBottomDeclaration.Name, new DeclarationInfo(
                    name: MarginBottomDeclaration.Name,
                    converter: MarginBottomDeclaration.Converter,
                    initialValue: MarginBottomDeclaration.InitialValue,
                    flags: MarginBottomDeclaration.Flags,
                    shorthands: MarginBottomDeclaration.Shorthands)
            },
            {
                BorderTopRightRadiusDeclaration.Name, new DeclarationInfo(
                    name: BorderTopRightRadiusDeclaration.Name,
                    converter: BorderTopRightRadiusDeclaration.Converter,
                    initialValue: BorderTopRightRadiusDeclaration.InitialValue,
                    flags: BorderTopRightRadiusDeclaration.Flags,
                    shorthands: BorderTopRightRadiusDeclaration.Shorthands)
            },
            {
                BorderTopLeftRadiusDeclaration.Name, new DeclarationInfo(
                    name: BorderTopLeftRadiusDeclaration.Name,
                    converter: BorderTopLeftRadiusDeclaration.Converter,
                    initialValue: BorderTopLeftRadiusDeclaration.InitialValue,
                    flags: BorderTopLeftRadiusDeclaration.Flags,
                    shorthands: BorderTopLeftRadiusDeclaration.Shorthands)
            },
            {
                BorderBottomRightRadiusDeclaration.Name, new DeclarationInfo(
                    name: BorderBottomRightRadiusDeclaration.Name,
                    converter: BorderBottomRightRadiusDeclaration.Converter,
                    initialValue: BorderBottomRightRadiusDeclaration.InitialValue,
                    flags: BorderBottomRightRadiusDeclaration.Flags,
                    shorthands: BorderBottomRightRadiusDeclaration.Shorthands)
            },
            {
                BorderBottomLeftRadiusDeclaration.Name, new DeclarationInfo(
                    name: BorderBottomLeftRadiusDeclaration.Name,
                    converter: BorderBottomLeftRadiusDeclaration.Converter,
                    initialValue: BorderBottomLeftRadiusDeclaration.InitialValue,
                    flags: BorderBottomLeftRadiusDeclaration.Flags,
                    shorthands: BorderBottomLeftRadiusDeclaration.Shorthands)
            },
            {
                OutlineWidthDeclaration.Name, new DeclarationInfo(
                    name: OutlineWidthDeclaration.Name,
                    converter: OutlineWidthDeclaration.Converter,
                    initialValue: OutlineWidthDeclaration.InitialValue,
                    flags: OutlineWidthDeclaration.Flags,
                    shorthands: OutlineWidthDeclaration.Shorthands)
            },
            {
                OutlineStyleDeclaration.Name, new DeclarationInfo(
                    name: OutlineStyleDeclaration.Name,
                    converter: OutlineStyleDeclaration.Converter,
                    initialValue: OutlineStyleDeclaration.InitialValue,
                    flags: OutlineStyleDeclaration.Flags,
                    shorthands: OutlineStyleDeclaration.Shorthands)
            },
            {
                OutlineColorDeclaration.Name, new DeclarationInfo(
                    name: OutlineColorDeclaration.Name,
                    converter: OutlineColorDeclaration.Converter,
                    initialValue: OutlineColorDeclaration.InitialValue,
                    flags: OutlineColorDeclaration.Flags,
                    shorthands: OutlineColorDeclaration.Shorthands)
            },
            {
                TextDecorationStyleDeclaration.Name, new DeclarationInfo(
                    name: TextDecorationStyleDeclaration.Name,
                    converter: TextDecorationStyleDeclaration.Converter,
                    initialValue: TextDecorationStyleDeclaration.InitialValue,
                    flags: TextDecorationStyleDeclaration.Flags,
                    shorthands: TextDecorationStyleDeclaration.Shorthands)
            },
            {
                TextDecorationLineDeclaration.Name, new DeclarationInfo(
                    name: TextDecorationLineDeclaration.Name,
                    converter: TextDecorationLineDeclaration.Converter,
                    initialValue: TextDecorationLineDeclaration.InitialValue,
                    flags: TextDecorationLineDeclaration.Flags,
                    shorthands: TextDecorationLineDeclaration.Shorthands)
            },
            {
                TextDecorationColorDeclaration.Name, new DeclarationInfo(
                    name: TextDecorationColorDeclaration.Name,
                    converter: TextDecorationColorDeclaration.Converter,
                    initialValue: TextDecorationColorDeclaration.InitialValue,
                    flags: TextDecorationColorDeclaration.Flags,
                    shorthands: TextDecorationColorDeclaration.Shorthands)
            },
            {
                TransitionTimingFunctionDeclaration.Name, new DeclarationInfo(
                    name: TransitionTimingFunctionDeclaration.Name,
                    converter: TransitionTimingFunctionDeclaration.Converter,
                    initialValue: TransitionTimingFunctionDeclaration.InitialValue,
                    flags: TransitionTimingFunctionDeclaration.Flags,
                    shorthands: TransitionTimingFunctionDeclaration.Shorthands)
            },
            {
                TransitionPropertyDeclaration.Name, new DeclarationInfo(
                    name: TransitionPropertyDeclaration.Name,
                    converter: TransitionPropertyDeclaration.Converter,
                    initialValue: TransitionPropertyDeclaration.InitialValue,
                    flags: TransitionPropertyDeclaration.Flags,
                    shorthands: TransitionPropertyDeclaration.Shorthands)
            },
            {
                TransitionDurationDeclaration.Name, new DeclarationInfo(
                    name: TransitionDurationDeclaration.Name,
                    converter: TransitionDurationDeclaration.Converter,
                    initialValue: TransitionDurationDeclaration.InitialValue,
                    flags: TransitionDurationDeclaration.Flags,
                    shorthands: TransitionDurationDeclaration.Shorthands)
            },
            {
                TransitionDelayDeclaration.Name, new DeclarationInfo(
                    name: TransitionDelayDeclaration.Name,
                    converter: TransitionDelayDeclaration.Converter,
                    initialValue: TransitionDelayDeclaration.InitialValue,
                    flags: TransitionDelayDeclaration.Flags,
                    shorthands: TransitionDelayDeclaration.Shorthands)
            },
            {
                BorderImageWidthDeclaration.Name, new DeclarationInfo(
                    name: BorderImageWidthDeclaration.Name,
                    converter: BorderImageWidthDeclaration.Converter,
                    initialValue: BorderImageWidthDeclaration.InitialValue,
                    flags: BorderImageWidthDeclaration.Flags,
                    shorthands: BorderImageWidthDeclaration.Shorthands)
            },
            {
                BorderImageSourceDeclaration.Name, new DeclarationInfo(
                    name: BorderImageSourceDeclaration.Name,
                    converter: BorderImageSourceDeclaration.Converter,
                    initialValue: BorderImageSourceDeclaration.InitialValue,
                    flags: BorderImageSourceDeclaration.Flags,
                    shorthands: BorderImageSourceDeclaration.Shorthands)
            },
            {
                BorderImageSliceDeclaration.Name, new DeclarationInfo(
                    name: BorderImageSliceDeclaration.Name,
                    converter: BorderImageSliceDeclaration.Converter,
                    initialValue: BorderImageSliceDeclaration.InitialValue,
                    flags: BorderImageSliceDeclaration.Flags,
                    shorthands: BorderImageSliceDeclaration.Shorthands)
            },
            {
                BorderImageRepeatDeclaration.Name, new DeclarationInfo(
                    name: BorderImageRepeatDeclaration.Name,
                    converter: BorderImageRepeatDeclaration.Converter,
                    initialValue: BorderImageRepeatDeclaration.InitialValue,
                    flags: BorderImageRepeatDeclaration.Flags,
                    shorthands: BorderImageRepeatDeclaration.Shorthands)
            },
            {
                BorderImageOutsetDeclaration.Name, new DeclarationInfo(
                    name: BorderImageOutsetDeclaration.Name,
                    converter: BorderImageOutsetDeclaration.Converter,
                    initialValue: BorderImageOutsetDeclaration.InitialValue,
                    flags: BorderImageOutsetDeclaration.Flags,
                    shorthands: BorderImageOutsetDeclaration.Shorthands)
            },
            {
                AnimationTimingFunctionDeclaration.Name, new DeclarationInfo(
                    name: AnimationTimingFunctionDeclaration.Name,
                    converter: AnimationTimingFunctionDeclaration.Converter,
                    initialValue: AnimationTimingFunctionDeclaration.InitialValue,
                    flags: AnimationTimingFunctionDeclaration.Flags,
                    shorthands: AnimationTimingFunctionDeclaration.Shorthands)
            },
            {
                AnimationPlayStateDeclaration.Name, new DeclarationInfo(
                    name: AnimationPlayStateDeclaration.Name,
                    converter: AnimationPlayStateDeclaration.Converter,
                    initialValue: AnimationPlayStateDeclaration.InitialValue,
                    flags: AnimationPlayStateDeclaration.Flags,
                    shorthands: AnimationPlayStateDeclaration.Shorthands)
            },
            {
                AnimationNameDeclaration.Name, new DeclarationInfo(
                    name: AnimationNameDeclaration.Name,
                    converter: AnimationNameDeclaration.Converter,
                    initialValue: AnimationNameDeclaration.InitialValue,
                    flags: AnimationNameDeclaration.Flags,
                    shorthands: AnimationNameDeclaration.Shorthands)
            },
            {
                AnimationIterationCountDeclaration.Name, new DeclarationInfo(
                    name: AnimationIterationCountDeclaration.Name,
                    converter: AnimationIterationCountDeclaration.Converter,
                    initialValue: AnimationIterationCountDeclaration.InitialValue,
                    flags: AnimationIterationCountDeclaration.Flags,
                    shorthands: AnimationIterationCountDeclaration.Shorthands)
            },
            {
                AnimationFillModeDeclaration.Name, new DeclarationInfo(
                    name: AnimationFillModeDeclaration.Name,
                    converter: AnimationFillModeDeclaration.Converter,
                    initialValue: AnimationFillModeDeclaration.InitialValue,
                    flags: AnimationFillModeDeclaration.Flags,
                    shorthands: AnimationFillModeDeclaration.Shorthands)
            },
            {
                AnimationDurationDeclaration.Name, new DeclarationInfo(
                    name: AnimationDurationDeclaration.Name,
                    converter: AnimationDurationDeclaration.Converter,
                    initialValue: AnimationDurationDeclaration.InitialValue,
                    flags: AnimationDurationDeclaration.Flags,
                    shorthands: AnimationDurationDeclaration.Shorthands)
            },
            {
                AnimationDirectionDeclaration.Name, new DeclarationInfo(
                    name: AnimationDirectionDeclaration.Name,
                    converter: AnimationDirectionDeclaration.Converter,
                    initialValue: AnimationDirectionDeclaration.InitialValue,
                    flags: AnimationDirectionDeclaration.Flags,
                    shorthands: AnimationDirectionDeclaration.Shorthands)
            },
            {
                AnimationDelayDeclaration.Name, new DeclarationInfo(
                    name: AnimationDelayDeclaration.Name,
                    converter: AnimationDelayDeclaration.Converter,
                    initialValue: AnimationDelayDeclaration.InitialValue,
                    flags: AnimationDelayDeclaration.Flags,
                    shorthands: AnimationDelayDeclaration.Shorthands)
            },
            {
                BackgroundSizeDeclaration.Name, new DeclarationInfo(
                    name: BackgroundSizeDeclaration.Name,
                    converter: BackgroundSizeDeclaration.Converter,
                    initialValue: BackgroundSizeDeclaration.InitialValue,
                    flags: BackgroundSizeDeclaration.Flags,
                    shorthands: BackgroundSizeDeclaration.Shorthands)
            },
            {
                BackgroundRepeatDeclaration.Name, new DeclarationInfo(
                    name: BackgroundRepeatDeclaration.Name,
                    converter: BackgroundRepeatDeclaration.Converter,
                    initialValue: BackgroundRepeatDeclaration.InitialValue,
                    flags: BackgroundRepeatDeclaration.Flags,
                    longhands: BackgroundRepeatDeclaration.Longhands,
                    shorthands: BackgroundRepeatDeclaration.Shorthands)
            },
            {
                BackgroundRepeatXDeclaration.Name, new DeclarationInfo(
                    name: BackgroundRepeatXDeclaration.Name,
                    converter: BackgroundRepeatXDeclaration.Converter,
                    initialValue: BackgroundRepeatXDeclaration.InitialValue,
                    flags: BackgroundRepeatXDeclaration.Flags,
                    shorthands: BackgroundRepeatXDeclaration.Shorthands)
            },
            {
                BackgroundRepeatYDeclaration.Name, new DeclarationInfo(
                    name: BackgroundRepeatYDeclaration.Name,
                    converter: BackgroundRepeatYDeclaration.Converter,
                    initialValue: BackgroundRepeatYDeclaration.InitialValue,
                    flags: BackgroundRepeatYDeclaration.Flags,
                    shorthands: BackgroundRepeatYDeclaration.Shorthands)
            },
            {
                BackgroundPositionDeclaration.Name, new DeclarationInfo(
                    name: BackgroundPositionDeclaration.Name,
                    converter: BackgroundPositionDeclaration.Converter,
                    initialValue: BackgroundPositionDeclaration.InitialValue,
                    flags: BackgroundPositionDeclaration.Flags,
                    shorthands: BackgroundPositionDeclaration.Shorthands,
                    longhands: BackgroundPositionDeclaration.Longhands)
            },
            {
                BackgroundPositionXDeclaration.Name, new DeclarationInfo(
                    name: BackgroundPositionXDeclaration.Name,
                    converter: BackgroundPositionXDeclaration.Converter,
                    initialValue: BackgroundPositionXDeclaration.InitialValue,
                    flags: BackgroundPositionXDeclaration.Flags,
                    shorthands: BackgroundPositionXDeclaration.Shorthands)
            },
            {
                BackgroundPositionYDeclaration.Name, new DeclarationInfo(
                    name: BackgroundPositionYDeclaration.Name,
                    converter: BackgroundPositionYDeclaration.Converter,
                    initialValue: BackgroundPositionYDeclaration.InitialValue,
                    flags: BackgroundPositionYDeclaration.Flags,
                    shorthands: BackgroundPositionYDeclaration.Shorthands)
            },
            {
                BackgroundOriginDeclaration.Name, new DeclarationInfo(
                    name: BackgroundOriginDeclaration.Name,
                    converter: BackgroundOriginDeclaration.Converter,
                    initialValue: BackgroundOriginDeclaration.InitialValue,
                    flags: BackgroundOriginDeclaration.Flags,
                    shorthands: BackgroundOriginDeclaration.Shorthands)
            },
            {
                BackgroundImageDeclaration.Name, new DeclarationInfo(
                    name: BackgroundImageDeclaration.Name,
                    converter: BackgroundImageDeclaration.Converter,
                    initialValue: BackgroundImageDeclaration.InitialValue,
                    flags: BackgroundImageDeclaration.Flags,
                    shorthands: BackgroundImageDeclaration.Shorthands)
            },
            {
                BackgroundColorDeclaration.Name, new DeclarationInfo(
                    name: BackgroundColorDeclaration.Name,
                    converter: BackgroundColorDeclaration.Converter,
                    initialValue: BackgroundColorDeclaration.InitialValue,
                    flags: BackgroundColorDeclaration.Flags,
                    shorthands: BackgroundColorDeclaration.Shorthands)
            },
            {
                BackgroundClipDeclaration.Name, new DeclarationInfo(
                    name: BackgroundClipDeclaration.Name,
                    converter: BackgroundClipDeclaration.Converter,
                    initialValue: BackgroundClipDeclaration.InitialValue,
                    flags: BackgroundClipDeclaration.Flags,
                    shorthands: BackgroundClipDeclaration.Shorthands)
            },
            {
                BackgroundAttachmentDeclaration.Name, new DeclarationInfo(
                    name: BackgroundAttachmentDeclaration.Name,
                    converter: BackgroundAttachmentDeclaration.Converter,
                    initialValue: BackgroundAttachmentDeclaration.InitialValue,
                    flags: BackgroundAttachmentDeclaration.Flags,
                    shorthands: BackgroundAttachmentDeclaration.Shorthands)
            },
            {
                BorderTopWidthDeclaration.Name, new DeclarationInfo(
                    name: BorderTopWidthDeclaration.Name,
                    converter: BorderTopWidthDeclaration.Converter,
                    initialValue: BorderTopWidthDeclaration.InitialValue,
                    flags: BorderTopWidthDeclaration.Flags,
                    shorthands: BorderTopWidthDeclaration.Shorthands)
            },
            {
                BorderTopStyleDeclaration.Name, new DeclarationInfo(
                    name: BorderTopStyleDeclaration.Name,
                    converter: BorderTopStyleDeclaration.Converter,
                    initialValue: BorderTopStyleDeclaration.InitialValue,
                    flags: BorderTopStyleDeclaration.Flags,
                    shorthands: BorderTopStyleDeclaration.Shorthands)
            },
            {
                BorderTopColorDeclaration.Name, new DeclarationInfo(
                    name: BorderTopColorDeclaration.Name,
                    converter: BorderTopColorDeclaration.Converter,
                    initialValue: BorderTopColorDeclaration.InitialValue,
                    flags: BorderTopColorDeclaration.Flags,
                    shorthands: BorderTopColorDeclaration.Shorthands)
            },
            {
                BorderRightWidthDeclaration.Name, new DeclarationInfo(
                    name: BorderRightWidthDeclaration.Name,
                    converter: BorderRightWidthDeclaration.Converter,
                    initialValue: BorderRightWidthDeclaration.InitialValue,
                    flags: BorderRightWidthDeclaration.Flags,
                    shorthands: BorderRightWidthDeclaration.Shorthands)
            },
            {
                BorderRightStyleDeclaration.Name, new DeclarationInfo(
                    name: BorderRightStyleDeclaration.Name,
                    converter: BorderRightStyleDeclaration.Converter,
                    initialValue: BorderRightStyleDeclaration.InitialValue,
                    flags: BorderRightStyleDeclaration.Flags,
                    shorthands: BorderRightStyleDeclaration.Shorthands)
            },
            {
                BorderRightColorDeclaration.Name, new DeclarationInfo(
                    name: BorderRightColorDeclaration.Name,
                    converter: BorderRightColorDeclaration.Converter,
                    initialValue: BorderRightColorDeclaration.InitialValue,
                    flags: BorderRightColorDeclaration.Flags,
                    shorthands: BorderRightColorDeclaration.Shorthands)
            },
            {
                BorderLeftWidthDeclaration.Name, new DeclarationInfo(
                    name: BorderLeftWidthDeclaration.Name,
                    converter: BorderLeftWidthDeclaration.Converter,
                    initialValue: BorderLeftWidthDeclaration.InitialValue,
                    flags: BorderLeftWidthDeclaration.Flags,
                    shorthands: BorderLeftWidthDeclaration.Shorthands)
            },
            {
                BorderLeftStyleDeclaration.Name, new DeclarationInfo(
                    name: BorderLeftStyleDeclaration.Name,
                    converter: BorderLeftStyleDeclaration.Converter,
                    initialValue: BorderLeftStyleDeclaration.InitialValue,
                    flags: BorderLeftStyleDeclaration.Flags,
                    shorthands: BorderLeftStyleDeclaration.Shorthands)
            },
            {
                BorderLeftColorDeclaration.Name, new DeclarationInfo(
                    name: BorderLeftColorDeclaration.Name,
                    converter: BorderLeftColorDeclaration.Converter,
                    initialValue: BorderLeftColorDeclaration.InitialValue,
                    flags: BorderLeftColorDeclaration.Flags,
                    shorthands: BorderLeftColorDeclaration.Shorthands)
            },
            {
                BorderBottomWidthDeclaration.Name, new DeclarationInfo(
                    name: BorderBottomWidthDeclaration.Name,
                    converter: BorderBottomWidthDeclaration.Converter,
                    initialValue: BorderBottomWidthDeclaration.InitialValue,
                    flags: BorderBottomWidthDeclaration.Flags,
                    shorthands: BorderBottomWidthDeclaration.Shorthands)
            },
            {
                BorderBottomStyleDeclaration.Name, new DeclarationInfo(
                    name: BorderBottomStyleDeclaration.Name,
                    converter: BorderBottomStyleDeclaration.Converter,
                    initialValue: BorderBottomStyleDeclaration.InitialValue,
                    flags: BorderBottomStyleDeclaration.Flags,
                    shorthands: BorderBottomStyleDeclaration.Shorthands)
            },
            {
                BorderBottomColorDeclaration.Name, new DeclarationInfo(
                    name: BorderBottomColorDeclaration.Name,
                    converter: BorderBottomColorDeclaration.Converter,
                    initialValue: BorderBottomColorDeclaration.InitialValue,
                    flags: BorderBottomColorDeclaration.Flags,
                    shorthands: BorderBottomColorDeclaration.Shorthands)
            },
            {
                AnimationDeclaration.Name, new DeclarationInfo(
                    name: AnimationDeclaration.Name,
                    converter: AnimationDeclaration.Converter,
                    initialValue: AnimationDeclaration.InitialValue,
                    flags: AnimationDeclaration.Flags,
                    longhands: AnimationDeclaration.Longhands)
            },
            {
                BackgroundDeclaration.Name, new DeclarationInfo(
                    name: BackgroundDeclaration.Name,
                    converter: BackgroundDeclaration.Converter,
                    initialValue: BackgroundDeclaration.InitialValue,
                    flags: BackgroundDeclaration.Flags,
                    longhands: BackgroundDeclaration.Longhands)
            },
            {
                TransitionDeclaration.Name, new DeclarationInfo(
                    name: TransitionDeclaration.Name,
                    converter: TransitionDeclaration.Converter,
                    initialValue: TransitionDeclaration.InitialValue,
                    flags: TransitionDeclaration.Flags,
                    longhands: TransitionDeclaration.Longhands)
            },
            {
                TextDecorationDeclaration.Name, new DeclarationInfo(
                    name: TextDecorationDeclaration.Name,
                    converter: TextDecorationDeclaration.Converter,
                    initialValue: TextDecorationDeclaration.InitialValue,
                    flags: TextDecorationDeclaration.Flags,
                    longhands: TextDecorationDeclaration.Longhands)
            },
            {
                OutlineDeclaration.Name, new DeclarationInfo(
                    name: OutlineDeclaration.Name,
                    converter: OutlineDeclaration.Converter,
                    initialValue: OutlineDeclaration.InitialValue,
                    flags: OutlineDeclaration.Flags,
                    longhands: OutlineDeclaration.Longhands)
            },
            {
                ListStyleDeclaration.Name, new DeclarationInfo(
                    name: ListStyleDeclaration.Name,
                    converter: ListStyleDeclaration.Converter,
                    initialValue: ListStyleDeclaration.InitialValue,
                    flags: ListStyleDeclaration.Flags,
                    longhands: ListStyleDeclaration.Longhands)
            },
            {
                FontDeclaration.Name, new DeclarationInfo(
                    name: FontDeclaration.Name,
                    converter: FontDeclaration.Converter,
                    initialValue: FontDeclaration.InitialValue,
                    flags: FontDeclaration.Flags,
                    longhands: FontDeclaration.Longhands)
            },
            {
                ColumnsDeclaration.Name, new DeclarationInfo(
                    name: ColumnsDeclaration.Name,
                    converter: ColumnsDeclaration.Converter,
                    initialValue: ColumnsDeclaration.InitialValue,
                    flags: ColumnsDeclaration.Flags,
                    longhands: ColumnsDeclaration.Longhands)
            },
            {
                ColumnRuleDeclaration.Name, new DeclarationInfo(
                    name: ColumnRuleDeclaration.Name,
                    converter: ColumnRuleDeclaration.Converter,
                    initialValue: ColumnRuleDeclaration.InitialValue,
                    flags: ColumnRuleDeclaration.Flags,
                    longhands: ColumnRuleDeclaration.Longhands)
            },
            {
                PaddingDeclaration.Name, new DeclarationInfo(
                    name: PaddingDeclaration.Name,
                    converter: PaddingDeclaration.Converter,
                    initialValue: PaddingDeclaration.InitialValue,
                    flags: PaddingDeclaration.Flags,
                    longhands: PaddingDeclaration.Longhands)
            },
            {
                MarginDeclaration.Name, new DeclarationInfo(
                    name: MarginDeclaration.Name,
                    converter: MarginDeclaration.Converter,
                    initialValue: MarginDeclaration.InitialValue,
                    flags: MarginDeclaration.Flags,
                    longhands: MarginDeclaration.Longhands)
            },
            {
                BorderRadiusDeclaration.Name, new DeclarationInfo(
                    name: BorderRadiusDeclaration.Name,
                    converter: BorderRadiusDeclaration.Converter,
                    initialValue: BorderRadiusDeclaration.InitialValue,
                    flags: BorderRadiusDeclaration.Flags,
                    longhands: BorderRadiusDeclaration.Longhands)
            },
            {
                BorderImageDeclaration.Name, new DeclarationInfo(
                    name: BorderImageDeclaration.Name,
                    converter: BorderImageDeclaration.Converter,
                    initialValue: BorderImageDeclaration.InitialValue,
                    flags: BorderImageDeclaration.Flags,
                    longhands: BorderImageDeclaration.Longhands)
            },
            {
                BorderWidthDeclaration.Name, new DeclarationInfo(
                    name: BorderWidthDeclaration.Name,
                    converter: BorderWidthDeclaration.Converter,
                    initialValue: BorderWidthDeclaration.InitialValue,
                    flags: BorderWidthDeclaration.Flags,
                    shorthands: BorderWidthDeclaration.Shorthands,
                    longhands: BorderWidthDeclaration.Longhands)
            },
            {
                BorderTopDeclaration.Name, new DeclarationInfo(
                    name: BorderTopDeclaration.Name,
                    converter: BorderTopDeclaration.Converter,
                    initialValue: BorderTopDeclaration.InitialValue,
                    flags: BorderTopDeclaration.Flags,
                    shorthands: BorderTopDeclaration.Shorthands,
                    longhands: BorderTopDeclaration.Longhands)
            },
            {
                BorderStyleDeclaration.Name, new DeclarationInfo(
                    name: BorderStyleDeclaration.Name,
                    converter: BorderStyleDeclaration.Converter,
                    initialValue: BorderStyleDeclaration.InitialValue,
                    flags: BorderStyleDeclaration.Flags,
                    shorthands: BorderStyleDeclaration.Shorthands,
                    longhands: BorderStyleDeclaration.Longhands)
            },
            {
                BorderRightDeclaration.Name, new DeclarationInfo(
                    name: BorderRightDeclaration.Name,
                    converter: BorderRightDeclaration.Converter,
                    initialValue: BorderRightDeclaration.InitialValue,
                    flags: BorderRightDeclaration.Flags,
                    shorthands: BorderRightDeclaration.Shorthands,
                    longhands: BorderRightDeclaration.Longhands)
            },
            {
                BorderLeftDeclaration.Name, new DeclarationInfo(
                    name: BorderLeftDeclaration.Name,
                    converter: BorderLeftDeclaration.Converter,
                    initialValue: BorderLeftDeclaration.InitialValue,
                    flags: BorderLeftDeclaration.Flags,
                    shorthands: BorderLeftDeclaration.Shorthands,
                    longhands: BorderLeftDeclaration.Longhands)
            },
            {
                BorderColorDeclaration.Name, new DeclarationInfo(
                    name: BorderColorDeclaration.Name,
                    converter: BorderColorDeclaration.Converter,
                    initialValue: BorderColorDeclaration.InitialValue,
                    flags: BorderColorDeclaration.Flags,
                    shorthands: BorderColorDeclaration.Shorthands,
                    longhands: BorderColorDeclaration.Longhands)
            },
            {
                BorderBottomDeclaration.Name, new DeclarationInfo(
                    name: BorderBottomDeclaration.Name,
                    converter: BorderBottomDeclaration.Converter,
                    initialValue: BorderBottomDeclaration.InitialValue,
                    flags: BorderBottomDeclaration.Flags,
                    shorthands: BorderBottomDeclaration.Shorthands,
                    longhands: BorderBottomDeclaration.Longhands)
            },
            {
                BorderDeclaration.Name, new DeclarationInfo(
                    name: BorderDeclaration.Name,
                    converter: BorderDeclaration.Converter,
                    initialValue: BorderDeclaration.InitialValue,
                    flags: BorderDeclaration.Flags,
                    longhands: BorderDeclaration.Longhands)
            },
            {
                ResizeDeclaration.Name, new DeclarationInfo(
                    name: ResizeDeclaration.Name,
                    converter: ResizeDeclaration.Converter,
                    initialValue: ResizeDeclaration.InitialValue,
                    flags: ResizeDeclaration.Flags)
            },
            {
                RubyAlignDeclaration.Name, new DeclarationInfo(
                    name: RubyAlignDeclaration.Name,
                    converter: RubyAlignDeclaration.Converter,
                    initialValue: RubyAlignDeclaration.InitialValue,
                    flags: RubyAlignDeclaration.Flags)
            },
            {
                RubyOverhangDeclaration.Name, new DeclarationInfo(
                    name: RubyOverhangDeclaration.Name,
                    converter: RubyOverhangDeclaration.Converter,
                    initialValue: RubyOverhangDeclaration.InitialValue,
                    flags: RubyOverhangDeclaration.Flags)
            },
            {
                RubyPositionDeclaration.Name, new DeclarationInfo(
                    name: RubyPositionDeclaration.Name,
                    converter: RubyPositionDeclaration.Converter,
                    initialValue: RubyPositionDeclaration.InitialValue,
                    flags: RubyPositionDeclaration.Flags)
            },
            {
                Scrollbar3dLightColorDeclaration.Name, new DeclarationInfo(
                    name: Scrollbar3dLightColorDeclaration.Name,
                    converter: Scrollbar3dLightColorDeclaration.Converter,
                    initialValue: Scrollbar3dLightColorDeclaration.InitialValue,
                    flags: Scrollbar3dLightColorDeclaration.Flags)
            },
            {
                ScrollbarArrowColorDeclaration.Name, new DeclarationInfo(
                    name: ScrollbarArrowColorDeclaration.Name,
                    converter: ScrollbarArrowColorDeclaration.Converter,
                    initialValue: ScrollbarArrowColorDeclaration.InitialValue,
                    flags: ScrollbarArrowColorDeclaration.Flags)
            },
            {
                ScrollbarBaseColorDeclaration.Name, new DeclarationInfo(
                    name: ScrollbarBaseColorDeclaration.Name,
                    converter: ScrollbarBaseColorDeclaration.Converter,
                    initialValue: ScrollbarBaseColorDeclaration.InitialValue,
                    flags: ScrollbarBaseColorDeclaration.Flags)
            },
            {
                ScrollbarDarkshadowColorDeclaration.Name, new DeclarationInfo(
                    name: ScrollbarDarkshadowColorDeclaration.Name,
                    converter: ScrollbarDarkshadowColorDeclaration.Converter,
                    initialValue: ScrollbarDarkshadowColorDeclaration.InitialValue,
                    flags: ScrollbarDarkshadowColorDeclaration.Flags)
            },
            {
                ScrollbarFaceColorDeclaration.Name, new DeclarationInfo(
                    name: ScrollbarFaceColorDeclaration.Name,
                    converter: ScrollbarFaceColorDeclaration.Converter,
                    initialValue: ScrollbarFaceColorDeclaration.InitialValue,
                    flags: ScrollbarFaceColorDeclaration.Flags)
            },
            {
                ScrollbarHighlightColorDeclaration.Name, new DeclarationInfo(
                    name: ScrollbarHighlightColorDeclaration.Name,
                    converter: ScrollbarHighlightColorDeclaration.Converter,
                    initialValue: ScrollbarHighlightColorDeclaration.InitialValue,
                    flags: ScrollbarHighlightColorDeclaration.Flags)
            },
            {
                ScrollbarShadowColorDeclaration.Name, new DeclarationInfo(
                    name: ScrollbarShadowColorDeclaration.Name,
                    converter: ScrollbarShadowColorDeclaration.Converter,
                    initialValue: ScrollbarShadowColorDeclaration.InitialValue,
                    flags: ScrollbarShadowColorDeclaration.Flags)
            },
            {
                ScrollbarTrackColorDeclaration.Name, new DeclarationInfo(
                    name: ScrollbarTrackColorDeclaration.Name,
                    converter: ScrollbarTrackColorDeclaration.Converter,
                    initialValue: ScrollbarTrackColorDeclaration.InitialValue,
                    flags: ScrollbarTrackColorDeclaration.Flags)
            },
            {
                PointerEventsDeclaration.Name, new DeclarationInfo(
                    name: PointerEventsDeclaration.Name,
                    converter: PointerEventsDeclaration.Converter,
                    initialValue: PointerEventsDeclaration.InitialValue,
                    flags: PointerEventsDeclaration.Flags)
            },
            {
                OrderDeclaration.Name, new DeclarationInfo(
                    name: OrderDeclaration.Name,
                    converter: OrderDeclaration.Converter,
                    initialValue: OrderDeclaration.InitialValue,
                    flags: OrderDeclaration.Flags)
            },
            {
                FlexShrinkDeclaration.Name, new DeclarationInfo(
                    name: FlexShrinkDeclaration.Name,
                    converter: FlexShrinkDeclaration.Converter,
                    initialValue: FlexShrinkDeclaration.InitialValue,
                    shorthands: FlexShrinkDeclaration.Shorthands,
                    flags: FlexShrinkDeclaration.Flags)
            },
            {
                FlexGrowDeclaration.Name, new DeclarationInfo(
                    name: FlexGrowDeclaration.Name,
                    converter: FlexGrowDeclaration.Converter,
                    initialValue: FlexGrowDeclaration.InitialValue,
                    shorthands: FlexGrowDeclaration.Shorthands,
                    flags: FlexGrowDeclaration.Flags)
            },
            {
                FlexBasisDeclaration.Name, new DeclarationInfo(
                    name: FlexBasisDeclaration.Name,
                    converter: FlexBasisDeclaration.Converter,
                    initialValue: FlexBasisDeclaration.InitialValue,
                    shorthands: FlexBasisDeclaration.Shorthands,
                    flags: FlexBasisDeclaration.Flags)
            },
            {
                JustifyContentDeclaration.Name, new DeclarationInfo(
                    name: JustifyContentDeclaration.Name,
                    converter: JustifyContentDeclaration.Converter,
                    initialValue: JustifyContentDeclaration.InitialValue,
                    flags: JustifyContentDeclaration.Flags)
            },
            {
                AlignContentDeclaration.Name, new DeclarationInfo(
                    name: AlignContentDeclaration.Name,
                    converter: AlignContentDeclaration.Converter,
                    initialValue: AlignContentDeclaration.InitialValue,
                    flags: AlignContentDeclaration.Flags)
            },
            {
                AlignSelfDeclaration.Name, new DeclarationInfo(
                    name: AlignSelfDeclaration.Name,
                    converter: AlignSelfDeclaration.Converter,
                    initialValue: AlignSelfDeclaration.InitialValue,
                    flags: AlignSelfDeclaration.Flags)
            },
            {
                AlignItemsDeclaration.Name, new DeclarationInfo(
                    name: AlignItemsDeclaration.Name,
                    converter: AlignItemsDeclaration.Converter,
                    initialValue: AlignItemsDeclaration.InitialValue,
                    flags: AlignItemsDeclaration.Flags)
            },
            {
                FlexDirectionDeclaration.Name, new DeclarationInfo(
                    name: FlexDirectionDeclaration.Name,
                    converter: FlexDirectionDeclaration.Converter,
                    initialValue: FlexDirectionDeclaration.InitialValue,
                    shorthands: FlexDirectionDeclaration.Shorthands,
                    flags: FlexDirectionDeclaration.Flags)
            },
            {
                FlexWrapDeclaration.Name, new DeclarationInfo(
                    name: FlexWrapDeclaration.Name,
                    converter: FlexWrapDeclaration.Converter,
                    initialValue: FlexWrapDeclaration.InitialValue,
                    shorthands: FlexWrapDeclaration.Shorthands,
                    flags: FlexWrapDeclaration.Flags)
            },
            {
                FlexDeclaration.Name, new DeclarationInfo(
                    name: FlexDeclaration.Name,
                    converter: FlexDeclaration.Converter,
                    initialValue: FlexDeclaration.InitialValue,
                    longhands: FlexDeclaration.Longhands,
                    flags: FlexDeclaration.Flags)
            },
            {
                FlexFlowDeclaration.Name, new DeclarationInfo(
                    name: FlexFlowDeclaration.Name,
                    converter: FlexFlowDeclaration.Converter,
                    initialValue: FlexFlowDeclaration.InitialValue,
                    longhands: FlexFlowDeclaration.Longhands,
                    flags: FlexFlowDeclaration.Flags)
            },
            {
                GridTemplateColumnsDeclaration.Name, new DeclarationInfo(
                    name: GridTemplateColumnsDeclaration.Name,
                    converter: GridTemplateColumnsDeclaration.Converter,
                    initialValue: GridTemplateColumnsDeclaration.InitialValue,
                    shorthands: GridTemplateAreasDeclaration.Shorthands,
                    flags: GridTemplateColumnsDeclaration.Flags)
            },
            {
                GridTemplateRowsDeclaration.Name, new DeclarationInfo(
                    name: GridTemplateRowsDeclaration.Name,
                    converter: GridTemplateRowsDeclaration.Converter,
                    initialValue: GridTemplateRowsDeclaration.InitialValue,
                    shorthands: GridTemplateAreasDeclaration.Shorthands,
                    flags: GridTemplateRowsDeclaration.Flags)
            },
            {
                GridTemplateAreasDeclaration.Name, new DeclarationInfo(
                    name: GridTemplateAreasDeclaration.Name,
                    converter: GridTemplateAreasDeclaration.Converter,
                    initialValue: GridTemplateAreasDeclaration.InitialValue,
                    shorthands: GridTemplateAreasDeclaration.Shorthands,
                    flags: GridTemplateAreasDeclaration.Flags)
            },
            {
                GridTemplateDeclaration.Name, new DeclarationInfo(
                    name: GridTemplateDeclaration.Name,
                    converter: GridTemplateDeclaration.Converter,
                    initialValue: GridTemplateDeclaration.InitialValue,
                    longhands: GridTemplateDeclaration.Longhands,
                    flags: GridTemplateDeclaration.Flags)
            },
            {
                GridAutoColumnsDeclaration.Name, new DeclarationInfo(
                    name: GridAutoColumnsDeclaration.Name,
                    converter: GridAutoColumnsDeclaration.Converter,
                    initialValue: GridAutoColumnsDeclaration.InitialValue,
                    flags: GridAutoColumnsDeclaration.Flags)
            },
            {
                GridAutoRowsDeclaration.Name, new DeclarationInfo(
                    name: GridAutoRowsDeclaration.Name,
                    converter: GridAutoRowsDeclaration.Converter,
                    initialValue: GridAutoRowsDeclaration.InitialValue,
                    flags: GridAutoRowsDeclaration.Flags)
            },
            {
                GridAutoFlowDeclaration.Name, new DeclarationInfo(
                    name: GridAutoFlowDeclaration.Name,
                    converter: GridAutoFlowDeclaration.Converter,
                    initialValue: GridAutoFlowDeclaration.InitialValue,
                    flags: GridAutoFlowDeclaration.Flags)
            },
            {
                GridDeclaration.Name, new DeclarationInfo(
                    name: GridDeclaration.Name,
                    converter: GridDeclaration.Converter,
                    initialValue: GridDeclaration.InitialValue,
                    longhands: GridDeclaration.Longhands,
                    flags: GridDeclaration.Flags)
            },
            {
                GridRowStartDeclaration.Name, new DeclarationInfo(
                    name: GridRowStartDeclaration.Name,
                    converter: GridRowStartDeclaration.Converter,
                    initialValue: GridRowStartDeclaration.InitialValue,
                    shorthands: GridRowStartDeclaration.Shorthands,
                    flags: GridRowStartDeclaration.Flags)
            },
            {
                GridColumnStartDeclaration.Name, new DeclarationInfo(
                    name: GridColumnStartDeclaration.Name,
                    converter: GridColumnStartDeclaration.Converter,
                    initialValue: GridColumnStartDeclaration.InitialValue,
                    shorthands: GridColumnStartDeclaration.Shorthands,
                    flags: GridColumnStartDeclaration.Flags)
            },
            {
                GridRowEndDeclaration.Name, new DeclarationInfo(
                    name: GridRowEndDeclaration.Name,
                    converter: GridRowEndDeclaration.Converter,
                    initialValue: GridRowEndDeclaration.InitialValue,
                    shorthands: GridRowEndDeclaration.Shorthands,
                    flags: GridRowEndDeclaration.Flags)
            },
            {
                GridColumnEndDeclaration.Name, new DeclarationInfo(
                    name: GridColumnEndDeclaration.Name,
                    converter: GridColumnEndDeclaration.Converter,
                    initialValue: GridColumnEndDeclaration.InitialValue,
                    shorthands: GridColumnEndDeclaration.Shorthands,
                    flags: GridColumnEndDeclaration.Flags)
            },
            {
                GridRowDeclaration.Name, new DeclarationInfo(
                    name: GridRowDeclaration.Name,
                    converter: GridRowDeclaration.Converter,
                    initialValue: GridRowDeclaration.InitialValue,
                    longhands: GridRowDeclaration.Longhands,
                    flags: GridRowDeclaration.Flags)
            },
            {
                GridColumnDeclaration.Name, new DeclarationInfo(
                    name: GridColumnDeclaration.Name,
                    converter: GridColumnDeclaration.Converter,
                    initialValue: GridColumnDeclaration.InitialValue,
                    longhands: GridColumnDeclaration.Longhands,
                    flags: GridColumnDeclaration.Flags)
            },
            {
                GridAreaDeclaration.Name, new DeclarationInfo(
                    name: GridAreaDeclaration.Name,
                    converter: GridAreaDeclaration.Converter,
                    initialValue: GridAreaDeclaration.InitialValue,
                    longhands: GridAreaDeclaration.Longhands,
                    flags: GridAreaDeclaration.Flags)
            },
            {
                GridRowGapDeclaration.Name, new DeclarationInfo(
                    name: GridRowGapDeclaration.Name,
                    converter: GridRowGapDeclaration.Converter,
                    initialValue: GridRowGapDeclaration.InitialValue,
                    shorthands: GridRowGapDeclaration.Shorthands,
                    flags: GridRowGapDeclaration.Flags)
            },
            {
                GridColumnGapDeclaration.Name, new DeclarationInfo(
                    name: GridColumnGapDeclaration.Name,
                    converter: GridColumnGapDeclaration.Converter,
                    initialValue: GridColumnGapDeclaration.InitialValue,
                    shorthands: GridColumnGapDeclaration.Shorthands,
                    flags: GridColumnGapDeclaration.Flags)
            },
            {
                GridGapDeclaration.Name, new DeclarationInfo(
                    name: GridGapDeclaration.Name,
                    converter: GridGapDeclaration.Converter,
                    initialValue: GridGapDeclaration.InitialValue,
                    longhands: GridGapDeclaration.Longhands,
                    flags: GridGapDeclaration.Flags)
            },
            {
                FillDeclaration.Name, new DeclarationInfo(
                    name: FillDeclaration.Name,
                    converter: FillDeclaration.Converter,
                    initialValue: FillDeclaration.InitialValue,
                    flags: FillDeclaration.Flags)
            },
        };

        /// <summary>
        /// Registers a new declaration for the given name. Throws an exception if another
        /// declaration for the given property name is already added.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="converter">The converter to use.</param>
        public void Register(String propertyName, DeclarationInfo converter) => _declarations.Add(propertyName, converter);

        /// <summary>
        /// Unregisters an existing declaration.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The registered declaration, if any.</returns>
        public DeclarationInfo Unregister(String propertyName)
        {
            if (_declarations.TryGetValue(propertyName, out DeclarationInfo info))
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
