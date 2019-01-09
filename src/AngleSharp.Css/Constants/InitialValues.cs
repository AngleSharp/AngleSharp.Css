namespace AngleSharp.Css.Constants
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using System;

    static class InitialValues
    {
        public static readonly ICssValue ColorDeclaration = Color.Black;
        public static readonly ICssValue BackgroundColorDeclaration = Color.Transparent;
        public static readonly ICssValue BackgroundImageDeclaration = new Constant<Object>(CssKeywords.None, null);
        public static readonly ICssValue BackgroundRepeatDeclaration = new ImageRepeats(new Identifier(CssKeywords.Repeat), new Identifier(CssKeywords.Repeat));
        public static readonly ICssValue BackgroundPositionDeclaration = new CssTupleValue(new ICssValue[] { new Length(0, Length.Unit.Percent), new Length(0, Length.Unit.Percent) });
        public static readonly ICssValue BackgroundSizeDeclaration = new BackgroundSize(new Constant<Length>(CssKeywords.Auto, Length.Auto), new Constant<Length>(CssKeywords.Auto, Length.Auto));
        public static readonly ICssValue BackgroundOriginDeclaration = new Constant<BoxModel>(CssKeywords.BorderBox, BoxModel.PaddingBox);
        public static readonly ICssValue BackgroundClipDeclaration = new Constant<BoxModel>(CssKeywords.BorderBox, BoxModel.BorderBox);
        public static readonly ICssValue BackgroundAttachmentDeclaration = new Constant<BackgroundAttachment>(CssKeywords.Scroll, BackgroundAttachment.Scroll);
        public static readonly ICssValue FontStyleDeclaration = new Constant<FontStyle>(CssKeywords.Normal, FontStyle.Normal);
        public static readonly ICssValue FontVariantDeclaration = new Constant<FontVariant>(CssKeywords.Normal, FontVariant.Normal);
        public static readonly ICssValue FontWeightDeclaration = new Constant<FontWeight>(CssKeywords.Normal, FontWeight.Normal);
        public static readonly ICssValue FontStretchDeclaration = new Constant<FontStretch>(CssKeywords.Normal, FontStretch.Normal);
        public static readonly ICssValue FontSizeDeclaration = new Constant<Length>(CssKeywords.Medium, Length.Medium);
        public static readonly ICssValue FontFamilyDeclaration = new StringValue("Times New Roman");
        public static readonly ICssValue LineHeightDeclaration = new Constant<Length>(CssKeywords.Normal, Length.Normal);
        public static readonly ICssValue BorderTopWidthDeclaration = new Constant<Length>(CssKeywords.Medium, Length.Medium);
        public static readonly ICssValue BorderRightWidthDeclaration = new Constant<Length>(CssKeywords.Medium, Length.Medium);
        public static readonly ICssValue BorderBottomWidthDeclaration = new Constant<Length>(CssKeywords.Medium, Length.Medium);
        public static readonly ICssValue BorderLeftWidthDeclaration = new Constant<Length>(CssKeywords.Medium, Length.Medium);
        public static readonly ICssValue BorderTopStyleDeclaration = new Constant<Object>(CssKeywords.None, null);
        public static readonly ICssValue BorderRightStyleDeclaration = new Constant<Object>(CssKeywords.None, null);
        public static readonly ICssValue BorderBottomStyleDeclaration = new Constant<Object>(CssKeywords.None, null);
        public static readonly ICssValue BorderLeftStyleDeclaration = new Constant<Object>(CssKeywords.None, null);
        public static readonly ICssValue BorderTopColorDeclaration = new Constant<Color>(CssKeywords.CurrentColor, Color.CurrentColor);
        public static readonly ICssValue BorderRightColorDeclaration = new Constant<Color>(CssKeywords.CurrentColor, Color.CurrentColor);
        public static readonly ICssValue BorderBottomColorDeclaration = new Constant<Color>(CssKeywords.CurrentColor, Color.CurrentColor);
        public static readonly ICssValue BorderLeftColorDeclaration = new Constant<Color>(CssKeywords.CurrentColor, Color.CurrentColor);
        public static readonly ICssValue ColumnWidthDeclaration = new Constant<Length>(CssKeywords.Auto, Length.Auto);
        public static readonly ICssValue ColumnCountDeclaration = new Constant<Length>(CssKeywords.Auto, Length.Auto);
        public static readonly ICssValue ColumnRuleWidthDeclaration = new Constant<Length>(CssKeywords.Medium, Length.Medium);
        public static readonly ICssValue ColumnRuleStyleDeclaration = new Constant<Object>(CssKeywords.None, null);
        public static readonly ICssValue ColumnRuleColorDeclaration = new Constant<Color>(CssKeywords.CurrentColor, Color.CurrentColor);
        public static readonly ICssValue AnimationNameDeclaration = new Constant<Object>(CssKeywords.None, null);
        public static readonly ICssValue AnimationDurationDeclaration = Time.Zero;
        public static readonly ICssValue AnimationTimingFunctionDeclaration = CubicBezierTimingFunction.Ease;
        public static readonly ICssValue AnimationDelayDeclaration = Time.Zero;
        public static readonly ICssValue AnimationIterationCountDeclaration = new Length(1, Length.Unit.None);
        public static readonly ICssValue AnimationDirectionDeclaration = new Constant<AnimationDirection>(CssKeywords.Normal, AnimationDirection.Normal);
        public static readonly ICssValue AnimationFillModeDeclaration = new Constant<AnimationFillStyle>(CssKeywords.None, AnimationFillStyle.None);
        public static readonly ICssValue AnimationPlayStateDeclaration = new Constant<PlayState>(CssKeywords.Running, PlayState.Running);
        public static readonly ICssValue TransitionDelayDeclaration = Time.Zero;
        public static readonly ICssValue TransitionDurationDeclaration = Time.Zero;
        public static readonly ICssValue TransitionPropertyDeclaration = new Identifier(CssKeywords.All);
        public static readonly ICssValue TransitionTimingFunctionDeclaration = CubicBezierTimingFunction.Ease;
        public static readonly ICssValue DirectionDeclaration = new Constant<DirectionMode>(CssKeywords.Ltr, DirectionMode.Ltr);
        public static readonly ICssValue EmptyCellsDeclaration = new Constant<Boolean>(CssKeywords.Show, true);
        //flex-grow: 0
        //flex-shrink: 1
        //flex-basis: auto
        //float: none
        //border-spacing: 0
        //box-shadow: none
        //box-sizing: content-box
        //break-after: auto
        //bottom: auto
        //top: auto
        //right: auto
        //left: auto
        //caption-side: top
        //cursor: auto
        //margin-bottom: 0
        //margin-left: 0
        //margin-right: 0
        //margin-top: 0
        //max-height: none
        //max-width: none
        //min-height: auto
        //min-width: auto
        //overflow-wrap: normal
        //word-spacing: normal
        //word-break: normal
        //visibility: visible
        //vertical-align: baseline
        //opacity: 1.0
        //overflow: visible
        //outline-color: invert, for browsers supporting it, currentColor for the other
        //outline-style: none
        //outline-width: medium
        //padding-bottom: 0
        //padding-left: 0
        //padding-right: 0
        //padding-top: 0
        //text-transform: none
        //text-shadow: none
        //text-rendering: auto
        //text-overflow: clip
        //text-orientation: mixed
        //text-justify: auto
        //text-indent: 0
        //text-align: start, or a nameless value that acts as left if direction is ltr, right if direction is rtl if start is not supported by the browser.
        //text-align-last: auto
        //text-decoration-color: currentcolor
        //text-decoration-style: solid
        //text-decoration-line: none
        //list-style-type: disc
        //list-style-position: outside
        //list-style-image: none
        //line-break: auto
        //grid-template-rows: none
        //grid-template-columns: none
        //grid-template-areas: none
        //grid-auto-rows: auto
        //grid-auto-columns: auto
        //grid-auto-flow: row
        //grid-column-gap: 0
        //grid-row-gap: 0
        //column-gap: normal
        //row-gap: normal
        //page-break-after: auto
        //page-break-before: auto
        //page-break-inside: auto
        //perspective: none
        //position: static
        //transform: none
        //break-inside: auto
        //break-before: auto
        //clear: none
        //clip: auto
        //content: normal
        //counter-increment: none
        //counter-reset: none
        //display: inline
        //backface-visibility: visible
        //border-image-source: none
        //border-image-slice: 100%
        //border-image-width: 1
        //border-image-outset: 0
        //border-image-repeat: stretch
        //align-self: auto
        //align-items: normal
        //align-content: normal
        //justify-content: normal
        //justify-items: legacy
        //justify-self: auto
        //z-index: auto
    }
}
