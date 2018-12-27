namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A collection of mappings for CSS (keywords to constants).
    /// </summary>
    static class Map
    {
        /// <summary>
        /// Contains the string-Whitespace mapping.
        /// </summary>
        public static readonly Dictionary<String, Whitespace> Whitespaces = new Dictionary<String, Whitespace>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Normal, Whitespace.Normal },
            { CssKeywords.Pre, Whitespace.Pre },
            { CssKeywords.Nowrap, Whitespace.NoWrap },
            { CssKeywords.PreWrap, Whitespace.PreWrap },
            { CssKeywords.PreLine, Whitespace.PreLine },
        };

        /// <summary>
        /// Contains the string-Angle mapping for linear-gradients.s
        /// </summary>
        public static readonly Dictionary<String, Angle> GradientAngles = new Dictionary<String, Angle>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Left, new Angle(270.0, Angle.Unit.Deg) },
            { CssKeywords.Top, new Angle(0.0, Angle.Unit.Deg) },
            { CssKeywords.Right, new Angle(90.0, Angle.Unit.Deg) },
            { CssKeywords.Bottom, new Angle(180.0, Angle.Unit.Deg) },
            { CssKeywords.LeftTop, new Angle(315.0, Angle.Unit.Deg) },
            { CssKeywords.LeftBottom, new Angle(225.0, Angle.Unit.Deg) },
            { CssKeywords.RightTop, new Angle(45.0, Angle.Unit.Deg) },
            { CssKeywords.RightBottom, new Angle(135.0, Angle.Unit.Deg) },
        };

        /// <summary>
        /// Contains the string-TextTransform mapping.
        /// </summary>
        public static readonly Dictionary<String, TextTransform> TextTransforms = new Dictionary<String, TextTransform>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.None, TextTransform.None },
            { CssKeywords.Capitalize, TextTransform.Capitalize },
            { CssKeywords.Uppercase, TextTransform.Uppercase },
            { CssKeywords.Lowercase, TextTransform.Lowercase },
            { CssKeywords.FullWidth, TextTransform.FullWidth },
        };

		/// <summary>
		/// Contains the string-TextAlignLast mapping.
		/// </summary>
		public static readonly Dictionary<String, TextAlignLast> TextAlignLasts = new Dictionary<String, TextAlignLast>(StringComparer.OrdinalIgnoreCase)
		{
			{ CssKeywords.Auto, TextAlignLast.Auto },
			{ CssKeywords.Start, TextAlignLast.Start },
			{ CssKeywords.End, TextAlignLast.End },
			{ CssKeywords.Right, TextAlignLast.Right },
			{ CssKeywords.Left, TextAlignLast.Left },
			{ CssKeywords.Center, TextAlignLast.Center },
			{ CssKeywords.Justify, TextAlignLast.Justify }
		};

		/// <summary>
		/// Contains the string-TextAnchor mapping.
		/// </summary>
		public static readonly Dictionary<String, TextAnchor> TextAnchors = new Dictionary<String, TextAnchor>(StringComparer.OrdinalIgnoreCase)
		{
			{ CssKeywords.Start, TextAnchor.Start },
			{ CssKeywords.Middle, TextAnchor.Middle },
			{ CssKeywords.End, TextAnchor.End }
		};

		/// <summary>
		/// Contains the string-TextJustify mapping.
		/// </summary>
		public static readonly Dictionary<String, TextJustify> TextJustifies = new Dictionary<String, TextJustify>(StringComparer.OrdinalIgnoreCase)
		{
			{ CssKeywords.Auto, TextJustify.Auto },
			{ CssKeywords.Distribute, TextJustify.Distribute },
			{ CssKeywords.DistributeAllLines, TextJustify.DistributeAllLines },
			{ CssKeywords.DistributeCenterLast, TextJustify.DistributeCenterLast },
			{ CssKeywords.InterCluster, TextJustify.InterCluster },
			{ CssKeywords.InterIdeograph, TextJustify.InterIdeograph },
			{ CssKeywords.InterWord, TextJustify.InterWord },
			{ CssKeywords.Kashida, TextJustify.Kashida },
			{ CssKeywords.Newspaper, TextJustify.Newspaper }
		};

		/// <summary>
		/// Contains the string-HorizontalAlignment mapping.
		/// </summary>
		public static readonly Dictionary<String, HorizontalAlignment> HorizontalAlignments = new Dictionary<String, HorizontalAlignment>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Left, HorizontalAlignment.Left },
            { CssKeywords.Right, HorizontalAlignment.Right },
            { CssKeywords.Center, HorizontalAlignment.Center },
            { CssKeywords.Justify, HorizontalAlignment.Justify },
        };

        /// <summary>
        /// Contains the string-VerticalAlignment mapping.
        /// </summary>
        public static readonly Dictionary<String, VerticalAlignment> VerticalAlignments = new Dictionary<String, VerticalAlignment>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Baseline, VerticalAlignment.Baseline },
            { CssKeywords.Sub, VerticalAlignment.Sub },
            { CssKeywords.Super, VerticalAlignment.Super },
            { CssKeywords.TextTop, VerticalAlignment.TextTop },
            { CssKeywords.TextBottom, VerticalAlignment.TextBottom },
            { CssKeywords.Middle, VerticalAlignment.Middle },
            { CssKeywords.Top, VerticalAlignment.Top },
            { CssKeywords.Bottom, VerticalAlignment.Bottom },
        };

        /// <summary>
        /// Contains the string-LineStyle mapping.
        /// </summary>
        public static readonly Dictionary<String, LineStyle> LineStyles = new Dictionary<String, LineStyle>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.None, LineStyle.None },
            { CssKeywords.Solid, LineStyle.Solid },
            { CssKeywords.Double, LineStyle.Double },
            { CssKeywords.Dotted, LineStyle.Dotted },
            { CssKeywords.Dashed, LineStyle.Dashed },
            { CssKeywords.Inset, LineStyle.Inset },
            { CssKeywords.Outset, LineStyle.Outset },
            { CssKeywords.Ridge, LineStyle.Ridge },
            { CssKeywords.Groove, LineStyle.Groove },
            { CssKeywords.Hidden, LineStyle.Hidden },
        };

        /// <summary>
        /// Contains the string-BoxModel mapping.
        /// </summary>
        public static readonly Dictionary<String, BoxModel> BoxModels = new Dictionary<String, BoxModel>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.BorderBox, BoxModel.BorderBox },
            { CssKeywords.PaddingBox, BoxModel.PaddingBox },
            { CssKeywords.ContentBox, BoxModel.ContentBox },
        };

        /// <summary>
        /// Contains the string-TimingFunction mapping.
        /// </summary>
        public static readonly Dictionary<String, ITimingFunction> TimingFunctions = new Dictionary<String, ITimingFunction>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Ease, new CubicBezierTimingFunction(0.25, 0.1, 0.25, 1.0) },
            { CssKeywords.EaseIn, new CubicBezierTimingFunction(0.42, 0.0, 1.0, 1.0) },
            { CssKeywords.EaseOut, new CubicBezierTimingFunction(0.0, 0.0, 0.58, 1.0) },
            { CssKeywords.EaseInOut, new CubicBezierTimingFunction(0.42, 0.0, 0.58, 1.0) },
            { CssKeywords.Linear, new CubicBezierTimingFunction(0.0, 0.0, 1.0, 1.0) },
            { CssKeywords.StepStart, new StepsTimingFunction(1, true) },
            { CssKeywords.StepEnd, new StepsTimingFunction(1, false) },
        };

        /// <summary>
        /// Contains the string-AnimationFillStyle mapping.
        /// </summary>
        public static readonly Dictionary<String, AnimationFillStyle> AnimationFillStyles = new Dictionary<String, AnimationFillStyle>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.None, AnimationFillStyle.None },
            { CssKeywords.Forwards, AnimationFillStyle.Forwards },
            { CssKeywords.Backwards, AnimationFillStyle.Backwards },
            { CssKeywords.Both, AnimationFillStyle.Both },
        };

        /// <summary>
        /// Contains the string-AnimationDirection mapping.
        /// </summary>
        public static readonly Dictionary<String, AnimationDirection> AnimationDirections = new Dictionary<String, AnimationDirection>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Normal, AnimationDirection.Normal },
            { CssKeywords.Reverse, AnimationDirection.Reverse },
            { CssKeywords.Alternate, AnimationDirection.Alternate },
            { CssKeywords.AlternateReverse, AnimationDirection.AlternateReverse },
        };

        /// <summary>
        /// Contains the string-Visibility mapping.
        /// </summary>
        public static readonly Dictionary<String, Visibility> Visibilities = new Dictionary<String, Visibility>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Visible, Visibility.Visible },
            { CssKeywords.Hidden, Visibility.Hidden },
            { CssKeywords.Collapse, Visibility.Collapse },
        };

        /// <summary>
        /// Contains the string-PlayState mapping.
        /// </summary>
        public static readonly Dictionary<String, PlayState> PlayStates = new Dictionary<String, PlayState>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Running, PlayState.Running },
            { CssKeywords.Paused, PlayState.Paused },
        };

        /// <summary>
        /// Contains the string-FontVariant mapping.
        /// </summary>
        public static readonly Dictionary<String, FontVariant> FontVariants = new Dictionary<String, FontVariant>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Normal, FontVariant.Normal },
            { CssKeywords.SmallCaps, FontVariant.SmallCaps },
        };

        /// <summary>
        /// Contains the string-DirectionMode mapping.
        /// </summary>
        public static readonly Dictionary<String, DirectionMode> DirectionModes = new Dictionary<String, DirectionMode>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Ltr, DirectionMode.Ltr },
            { CssKeywords.Rtl, DirectionMode.Rtl },
        };

        /// <summary>
        /// Contains the string-ListStyle mapping.
        /// </summary>
        public static readonly Dictionary<String, ListStyle> ListStyles = new Dictionary<String, ListStyle>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Disc, ListStyle.Disc },
            { CssKeywords.Circle, ListStyle.Circle },
            { CssKeywords.Square, ListStyle.Square },
            { CssKeywords.Decimal, ListStyle.Decimal },
            { CssKeywords.DecimalLeadingZero, ListStyle.DecimalLeadingZero },
            { CssKeywords.LowerRoman, ListStyle.LowerRoman },
            { CssKeywords.UpperRoman, ListStyle.UpperRoman },
            { CssKeywords.LowerGreek, ListStyle.LowerGreek },
            { CssKeywords.LowerLatin, ListStyle.LowerLatin },
            { CssKeywords.UpperLatin, ListStyle.UpperLatin },
            { CssKeywords.Armenian, ListStyle.Armenian },
            { CssKeywords.Georgian, ListStyle.Georgian },
            { CssKeywords.LowerAlpha, ListStyle.LowerLatin },
            { CssKeywords.UpperAlpha, ListStyle.UpperLatin },
            { CssKeywords.None, ListStyle.None },
        };

        /// <summary>
        /// Contains the string-ListPosition mapping.
        /// </summary>
        public static readonly Dictionary<String, ListPosition> ListPositions = new Dictionary<String, ListPosition>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Inside, ListPosition.Inside },
            { CssKeywords.Outside, ListPosition.Outside },
        };

        /// <summary>
        /// Contains the string-length mapping.
        /// </summary>
        public static readonly Dictionary<String, Length> FontSizes = new Dictionary<String, Length>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.XxSmall, new Length(0.6, Length.Unit.Em) },
            { CssKeywords.XSmall, new Length(0.75, Length.Unit.Em) },
            { CssKeywords.Small, new Length(8.0 / 9.0, Length.Unit.Em) },
            { CssKeywords.Medium, new Length(1.0, Length.Unit.Em) },
            { CssKeywords.Large, new Length(1.2, Length.Unit.Em) },
            { CssKeywords.XLarge, new Length(1.5, Length.Unit.Em) },
            { CssKeywords.XxLarge, new Length(2.0, Length.Unit.Em) },
            { CssKeywords.Larger, new Length(120.0, Length.Unit.Percent) },
            { CssKeywords.Smaller, new Length(80.0, Length.Unit.Percent) },
        };

        /// <summary>
        /// Contains the string-TextDecorationStyle mapping.
        /// </summary>
        public static readonly Dictionary<String, TextDecorationStyle> TextDecorationStyles = new Dictionary<String, TextDecorationStyle>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Solid, TextDecorationStyle.Solid },
            { CssKeywords.Double, TextDecorationStyle.Double },
            { CssKeywords.Dotted, TextDecorationStyle.Dotted },
            { CssKeywords.Dashed, TextDecorationStyle.Dashed },
            { CssKeywords.Wavy, TextDecorationStyle.Wavy },
        };

        /// <summary>
        /// Contains the string-TextDecorationLine mapping.
        /// </summary>
        public static readonly Dictionary<String, TextDecorationLine> TextDecorationLines = new Dictionary<String, TextDecorationLine>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Underline, TextDecorationLine.Underline },
            { CssKeywords.Overline, TextDecorationLine.Overline },
            { CssKeywords.LineThrough, TextDecorationLine.LineThrough },
            { CssKeywords.Blink, TextDecorationLine.Blink },
        };

        /// <summary>
        /// Contains the string-BorderRepeat mapping.
        /// </summary>
        public static readonly Dictionary<String, BorderRepeat> BorderRepeats = new Dictionary<String, BorderRepeat>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Stretch, BorderRepeat.Stretch },
            { CssKeywords.Repeat, BorderRepeat.Repeat },
            { CssKeywords.Round, BorderRepeat.Round },
        };

        /// <summary>
        /// Contains the string-border width mapping.
        /// </summary>
        public static readonly Dictionary<String, Length> BorderWidths = new Dictionary<String, Length>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Thin, Length.Thin },
            { CssKeywords.Medium, Length.Medium },
            { CssKeywords.Thick, Length.Thick },
        };

        /// <summary>
        /// Contains the string-font family mapping.
        /// </summary>
        public static readonly Dictionary<String, String> FontFamilies = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Serif, "Times New Roman" },
            { CssKeywords.SansSerif, "Arial" },
            { CssKeywords.Monospace, "Consolas" },
            { CssKeywords.Cursive, "Cursive" },
            { CssKeywords.Fantasy, "Comic Sans" },
        };

        /// <summary>
        /// Contains the string-BackgroundAttachment mapping.
        /// </summary>
        public static readonly Dictionary<String, BackgroundAttachment> BackgroundAttachments = new Dictionary<String, BackgroundAttachment>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Fixed, BackgroundAttachment.Fixed },
            { CssKeywords.Local, BackgroundAttachment.Local },
            { CssKeywords.Scroll, BackgroundAttachment.Scroll },
        };

        /// <summary>
        /// Contains the string-FontStyle mapping.
        /// </summary>
        public static readonly Dictionary<String, FontStyle> FontStyles = new Dictionary<String, FontStyle>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Normal, FontStyle.Normal },
            { CssKeywords.Italic, FontStyle.Italic },
            { CssKeywords.Oblique, FontStyle.Oblique },
        };

        /// <summary>
        /// Contains the string-FontStretch mapping.
        /// </summary>
        public static readonly Dictionary<String, FontStretch> FontStretches = new Dictionary<String, FontStretch>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Normal, FontStretch.Normal },
            { CssKeywords.UltraCondensed, FontStretch.UltraCondensed },
            { CssKeywords.ExtraCondensed, FontStretch.ExtraCondensed },
            { CssKeywords.Condensed, FontStretch.Condensed },
            { CssKeywords.SemiCondensed, FontStretch.SemiCondensed },
            { CssKeywords.SemiExpanded, FontStretch.SemiExpanded },
            { CssKeywords.Expanded, FontStretch.Expanded },
            { CssKeywords.ExtraExpanded, FontStretch.ExtraExpanded },
            { CssKeywords.UltraExpanded, FontStretch.UltraExpanded },
        };

        /// <summary>
        /// Contains the string-BreakMode (general) mapping.
        /// </summary>
        public static readonly Dictionary<String, BreakMode> BreakModes = new Dictionary<String, BreakMode>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Auto, BreakMode.Auto },
            { CssKeywords.Always, BreakMode.Always },
            { CssKeywords.Avoid, BreakMode.Avoid },
            { CssKeywords.Left, BreakMode.Left },
            { CssKeywords.Right, BreakMode.Right },
            { CssKeywords.Page, BreakMode.Page },
            { CssKeywords.Column, BreakMode.Column },
            { CssKeywords.AvoidPage, BreakMode.AvoidPage },
            { CssKeywords.AvoidColumn, BreakMode.AvoidColumn },
        };

        /// <summary>
        /// Contains the string-BreakMode (page) mapping.
        /// </summary>
        public static readonly Dictionary<String, BreakMode> PageBreakModes = new Dictionary<String, BreakMode>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Auto, BreakMode.Auto },
            { CssKeywords.Always, BreakMode.Always },
            { CssKeywords.Avoid, BreakMode.Avoid },
            { CssKeywords.Left, BreakMode.Left },
            { CssKeywords.Right, BreakMode.Right },
        };

        /// <summary>
        /// Contains the string-BreakMode (inside) mapping.
        /// </summary>
        public static readonly Dictionary<String, BreakMode> BreakInsideModes = new Dictionary<String, BreakMode>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Auto, BreakMode.Auto },
            { CssKeywords.Avoid, BreakMode.Avoid },
            { CssKeywords.AvoidPage, BreakMode.AvoidPage },
            { CssKeywords.AvoidColumn, BreakMode.AvoidColumn },
            { CssKeywords.AvoidRegion, BreakMode.AvoidRegion },
        };

        /// <summary>
        /// Contains the string-BreakMode (page/inside) mapping.
        /// </summary>
        public static readonly Dictionary<String, BreakMode> PageBreakInsideModes = new Dictionary<String, BreakMode>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Auto, BreakMode.Auto },
            { CssKeywords.Avoid, BreakMode.Avoid },
        };

        /// <summary>
        /// Contains the string-horizontal modes mapping.
        /// </summary>
        public static readonly Dictionary<String, Double> HorizontalModes = new Dictionary<String, Double>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Left, 0.0 },
            { CssKeywords.Center, 0.5 },
            { CssKeywords.Right, 1.0 },
        };

        /// <summary>
        /// Contains the string-vertical modes mapping.
        /// </summary>
        public static readonly Dictionary<String, Double> VerticalModes = new Dictionary<String, Double>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Top, 0.0 },
            { CssKeywords.Center, 0.5 },
            { CssKeywords.Bottom, 1.0 },
        };

        /// <summary>
        /// Contains the string-UnicodeMode mapping.
        /// </summary>
        public static readonly Dictionary<String, UnicodeMode> UnicodeModes = new Dictionary<String, UnicodeMode>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Normal, UnicodeMode.Normal },
            { CssKeywords.Embed, UnicodeMode.Embed },
            { CssKeywords.Isolate, UnicodeMode.Isolate },
            { CssKeywords.IsolateOverride, UnicodeMode.IsolateOverride },
            { CssKeywords.BidiOverride, UnicodeMode.BidiOverride },
            { CssKeywords.Plaintext, UnicodeMode.Plaintext },
        };

        /// <summary>
        /// Contains the string-whitespace mapping.
        /// </summary>
        public static readonly Dictionary<String, SystemCursor> SystemCursors = new Dictionary<String, SystemCursor>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Auto, SystemCursor.Auto },
            { CssKeywords.Default, SystemCursor.Default },
            { CssKeywords.None, SystemCursor.None },
            { CssKeywords.ContextMenu, SystemCursor.ContextMenu },
            { CssKeywords.Help, SystemCursor.Help },
            { CssKeywords.Pointer, SystemCursor.Pointer },
            { CssKeywords.Progress, SystemCursor.Progress },
            { CssKeywords.Wait, SystemCursor.Wait },
            { CssKeywords.Cell, SystemCursor.Cell },
            { CssKeywords.Crosshair, SystemCursor.Crosshair },
            { CssKeywords.Text, SystemCursor.Text },
            { CssKeywords.VerticalText, SystemCursor.VerticalText },
            { CssKeywords.Alias, SystemCursor.Alias },
            { CssKeywords.Copy, SystemCursor.Copy },
            { CssKeywords.Move, SystemCursor.Move },
            { CssKeywords.NoDrop, SystemCursor.NoDrop },
            { CssKeywords.NotAllowed, SystemCursor.NotAllowed },
            { CssKeywords.EastResize, SystemCursor.EResize },
            { CssKeywords.NorthResize, SystemCursor.NResize },
            { CssKeywords.NorthEastResize, SystemCursor.NeResize },
            { CssKeywords.NorthWestResize, SystemCursor.NwResize },
            { CssKeywords.SouthResize, SystemCursor.SResize },
            { CssKeywords.SouthEastResize, SystemCursor.SeResize },
            { CssKeywords.SouthWestResize, SystemCursor.WResize },
            { CssKeywords.WestResize, SystemCursor.WResize },
            { CssKeywords.EastWestResize, SystemCursor.EwResize },
            { CssKeywords.NorthSouthResize, SystemCursor.NsResize },
            { CssKeywords.NorthEastSouthWestResize, SystemCursor.NeswResize },
            { CssKeywords.NorthWestSouthEastResize, SystemCursor.NwseResize },
            { CssKeywords.ColResize, SystemCursor.ColResize },
            { CssKeywords.RowResize, SystemCursor.RowResize },
            { CssKeywords.AllScroll, SystemCursor.AllScroll },
            { CssKeywords.ZoomIn, SystemCursor.ZoomIn },
            { CssKeywords.ZoomOut, SystemCursor.ZoomOut },
            { CssKeywords.Grab, SystemCursor.Grab },
            { CssKeywords.Grabbing, SystemCursor.Grabbing },
        };

        /// <summary>
        /// Contains the string-PositionMode mapping.
        /// </summary>
        public static readonly Dictionary<String, PositionMode> PositionModes = new Dictionary<String, PositionMode>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Static, PositionMode.Static },
            { CssKeywords.Relative, PositionMode.Relative },
            { CssKeywords.Absolute, PositionMode.Absolute },
            { CssKeywords.Sticky, PositionMode.Sticky },
            { CssKeywords.Fixed, PositionMode.Fixed },
        };

        /// <summary>
        /// Contains the string-OverflowMode mapping.
        /// </summary>
        public static readonly Dictionary<String, OverflowMode> OverflowModes = new Dictionary<String, OverflowMode>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Visible, OverflowMode.Visible },
            { CssKeywords.Hidden, OverflowMode.Hidden },
            { CssKeywords.Scroll, OverflowMode.Scroll },
            { CssKeywords.Auto, OverflowMode.Auto },
        };

        /// <summary>
        /// Contains the extended string-OverflowMode mapping.
        /// </summary>
        public static readonly Dictionary<String, OverflowMode> OverflowExtendedModes = new Dictionary<String, OverflowMode>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Visible, OverflowMode.Visible },
            { CssKeywords.Hidden, OverflowMode.Hidden },
            { CssKeywords.Scroll, OverflowMode.Scroll },
            { CssKeywords.Clip, OverflowMode.Clip },
            { CssKeywords.Auto, OverflowMode.Auto },
        };

        /// <summary>
        /// Contains the string-Floating mapping.
        /// </summary>
        public static readonly Dictionary<String, Floating> Floatings = new Dictionary<String, Floating>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.None, Floating.None },
            { CssKeywords.Left, Floating.Left },
            { CssKeywords.Right, Floating.Right },
        };

        /// <summary>
        /// Contains the string-DisplayMode mapping.
        /// </summary>
        public static readonly Dictionary<String, DisplayMode> DisplayModes = new Dictionary<String, DisplayMode>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.None, DisplayMode.None },
            { CssKeywords.Inline, DisplayMode.Inline },
            { CssKeywords.Block, DisplayMode.Block },
            { CssKeywords.InlineBlock, DisplayMode.InlineBlock },
            { CssKeywords.ListItem, DisplayMode.ListItem },
            { CssKeywords.InlineTable, DisplayMode.InlineTable },
            { CssKeywords.Table, DisplayMode.Table },
            { CssKeywords.TableCaption, DisplayMode.TableCaption },
            { CssKeywords.TableCell, DisplayMode.TableCell },
            { CssKeywords.TableColumn, DisplayMode.TableColumn },
            { CssKeywords.TableColumnGroup, DisplayMode.TableColumnGroup },
            { CssKeywords.TableFooterGroup, DisplayMode.TableFooterGroup },
            { CssKeywords.TableHeaderGroup, DisplayMode.TableHeaderGroup },
            { CssKeywords.TableRow, DisplayMode.TableRow },
            { CssKeywords.TableRowGroup, DisplayMode.TableRowGroup },
            { CssKeywords.Flex, DisplayMode.Flex },
            { CssKeywords.InlineFlex, DisplayMode.InlineFlex },
            { CssKeywords.Grid, DisplayMode.Grid },
            { CssKeywords.InlineGrid, DisplayMode.InlineGrid },
        };

        /// <summary>
        /// Contains the string-ClearMode mapping.
        /// </summary>
        public static readonly Dictionary<String, ClearMode> ClearModes = new Dictionary<String, ClearMode>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.None, ClearMode.None },
            { CssKeywords.Left, ClearMode.Left },
            { CssKeywords.Right, ClearMode.Right },
            { CssKeywords.Both, ClearMode.Both },
        };

        /// <summary>
        /// Contains the string-BackgroundRepeat mapping.
        /// </summary>
        public static readonly Dictionary<String, BackgroundRepeat> BackgroundRepeats = new Dictionary<String, BackgroundRepeat>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.NoRepeat, BackgroundRepeat.NoRepeat },
            { CssKeywords.Repeat, BackgroundRepeat.Repeat },
            { CssKeywords.Round, BackgroundRepeat.Round },
            { CssKeywords.Space, BackgroundRepeat.Space },
        };

        /// <summary>
        /// Contains the string-BlendMode mapping.
        /// </summary>
        public static readonly Dictionary<String, BlendMode> BlendModes = new Dictionary<String, BlendMode>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Color, BlendMode.Color },
            { CssKeywords.ColorBurn, BlendMode.ColorBurn },
            { CssKeywords.ColorDodge, BlendMode.ColorDodge },
            { CssKeywords.Darken, BlendMode.Darken },
            { CssKeywords.Difference, BlendMode.Difference },
            { CssKeywords.Exclusion, BlendMode.Exclusion },
            { CssKeywords.HardLight, BlendMode.HardLight },
            { CssKeywords.Hue, BlendMode.Hue },
            { CssKeywords.Lighten, BlendMode.Lighten },
            { CssKeywords.Luminosity, BlendMode.Luminosity },
            { CssKeywords.Multiply, BlendMode.Multiply },
            { CssKeywords.Normal, BlendMode.Normal },
            { CssKeywords.Overlay, BlendMode.Overlay },
            { CssKeywords.Saturation, BlendMode.Saturation },
            { CssKeywords.Screen, BlendMode.Screen },
            { CssKeywords.SoftLight, BlendMode.SoftLight },
        };

        /// <summary>
        /// Contains the string-UpdateFrequency mapping.
        /// </summary>
        public static readonly Dictionary<String, UpdateFrequency> UpdateFrequencies = new Dictionary<String, UpdateFrequency>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.None, UpdateFrequency.None },
            { CssKeywords.Slow, UpdateFrequency.Slow },
            { CssKeywords.Normal, UpdateFrequency.Normal },
        };

        /// <summary>
        /// Contains the string-ScriptingState mapping.
        /// </summary>
        public static readonly Dictionary<String, ScriptingState> ScriptingStates = new Dictionary<String, ScriptingState>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.None, ScriptingState.None },
            { CssKeywords.InitialOnly, ScriptingState.InitialOnly },
            { CssKeywords.Enabled, ScriptingState.Enabled },
        };

        /// <summary>
        /// Contains the string-PointerAccuracy mapping.
        /// </summary>
        public static readonly Dictionary<String, PointerAccuracy> PointerAccuracies = new Dictionary<String, PointerAccuracy>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.None, PointerAccuracy.None },
            { CssKeywords.Coarse, PointerAccuracy.Coarse },
            { CssKeywords.Fine, PointerAccuracy.Fine },
        };

        /// <summary>
        /// Contains the string-HoverAbility mapping.
        /// </summary>
        public static readonly Dictionary<String, HoverAbility> HoverAbilities = new Dictionary<String, HoverAbility>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.None, HoverAbility.None },
            { CssKeywords.OnDemand, HoverAbility.OnDemand },
            { CssKeywords.Hover, HoverAbility.Hover },
        };

        /// <summary>
        /// Contains the string-SizeMode mapping.
        /// </summary>
        public static readonly Dictionary<String, RadialGradient.SizeMode> RadialGradientSizeModes = new Dictionary<String, RadialGradient.SizeMode>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.ClosestSide, RadialGradient.SizeMode.ClosestSide },
            { CssKeywords.FarthestSide, RadialGradient.SizeMode.FarthestSide },
            { CssKeywords.ClosestCorner, RadialGradient.SizeMode.ClosestCorner },
            { CssKeywords.FarthestCorner, RadialGradient.SizeMode.FarthestCorner },
        };

        /// <summary>
        /// Contains the string-ObjectFitting mapping.
        /// </summary>
        public static readonly Dictionary<String, ObjectFitting> ObjectFittings = new Dictionary<String, ObjectFitting>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.None, ObjectFitting.None },
            { CssKeywords.Cover, ObjectFitting.Cover },
            { CssKeywords.Contain, ObjectFitting.Contain },
            { CssKeywords.Fill, ObjectFitting.Fill },
            { CssKeywords.ScaleDown, ObjectFitting.ScaleDown },
        };

        /// <summary>
        /// Contains the string-FontWeight mapping.
        /// </summary>
        public static readonly Dictionary<String, FontWeight> FontWeights = new Dictionary<String, FontWeight>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Normal, FontWeight.Normal },
            { CssKeywords.Bold, FontWeight.Bold },
            { CssKeywords.Bolder, FontWeight.Bolder },
            { CssKeywords.Lighter, FontWeight.Lighter },
        };

        /// <summary>
        /// Contains the string-SystemFont mapping.
        /// </summary>
        public static readonly Dictionary<String, SystemFont> SystemFonts = new Dictionary<String, SystemFont>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Caption, SystemFont.Caption },
            { CssKeywords.Icon, SystemFont.Icon },
            { CssKeywords.Menu, SystemFont.Menu },
            { CssKeywords.MessageBox, SystemFont.MessageBox },
            { CssKeywords.SmallCaption, SystemFont.SmallCaption },
            { CssKeywords.StatusBar, SystemFont.StatusBar },
        };

		/// <summary>
		/// Contains the string-StrokeLinecap mapping.
		/// </summary>
		public static readonly Dictionary<String, StrokeLinecap> StrokeLinecaps = new Dictionary<String, StrokeLinecap>(StringComparer.OrdinalIgnoreCase)
		{
			{ CssKeywords.Butt, StrokeLinecap.Butt },
			{ CssKeywords.Round, StrokeLinecap.Round },
			{ CssKeywords.Square, StrokeLinecap.Square }
		};

		/// <summary>
		/// Contains the string-StrokeLinejoin mapping.
		/// </summary>
		public static readonly Dictionary<String, StrokeLinejoin> StrokeLinejoins = new Dictionary<String, StrokeLinejoin>(StringComparer.OrdinalIgnoreCase)
		{
			{ CssKeywords.Miter, StrokeLinejoin.Miter },
			{ CssKeywords.Round, StrokeLinejoin.Round },
			{ CssKeywords.Bevel, StrokeLinejoin.Bevel }
		};

		/// <summary>
		/// Contains the string-WordBreak mapping.
		/// </summary>
		public static readonly Dictionary<String, WordBreak> WordBreaks = new Dictionary<String, WordBreak>(StringComparer.OrdinalIgnoreCase)
		{
			{ CssKeywords.Normal, WordBreak.Normal },
			{ CssKeywords.BreakAll, WordBreak.BreakAll },
			{ CssKeywords.KeepAll, WordBreak.KeepAll }
		};

		/// <summary>
		/// Contains the string-WordBreak mapping.
		/// </summary>
		public static readonly Dictionary<String, OverflowWrap> OverflowWraps = new Dictionary<String, OverflowWrap>(StringComparer.OrdinalIgnoreCase)
		{
			{ CssKeywords.Normal, OverflowWrap.Normal },
			{ CssKeywords.BreakWord, OverflowWrap.BreakWord },
		};

        /// <summary>
        /// Contains the string-RubyAlignment mapping.
        /// </summary>
        public static readonly Dictionary<String, RubyAlignment> RubyAlignments = new Dictionary<String, RubyAlignment>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Center, RubyAlignment.Center },
            { CssKeywords.Start, RubyAlignment.Start },
            { CssKeywords.SpaceAround, RubyAlignment.SpaceAround },
            { CssKeywords.SpaceBetween, RubyAlignment.SpaceBetween },
        };

        /// <summary>
        /// Contains the string-RubyPosition mapping.
        /// </summary>
        public static readonly Dictionary<String, RubyPosition> RubyPositions = new Dictionary<String, RubyPosition>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.InterCharacter, RubyPosition.InterCharacter },
            { CssKeywords.Over, RubyPosition.Over },
            { CssKeywords.Under, RubyPosition.Under },
        };

        /// <summary>
        /// Contains the string-RubyOverhangMode mapping.
        /// </summary>
        public static readonly Dictionary<String, RubyOverhangMode> RubyOverhangModes = new Dictionary<String, RubyOverhangMode>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Auto, RubyOverhangMode.Auto },
            { CssKeywords.End, RubyOverhangMode.End },
            { CssKeywords.None, RubyOverhangMode.None },
            { CssKeywords.Start, RubyOverhangMode.Start },
        };

        /// <summary>
        /// Contains the string-FlexDirection mapping.
        /// </summary>
        public static readonly Dictionary<String, FlexDirection> FlexDirections = new Dictionary<String, FlexDirection>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Column, FlexDirection.Column },
            { CssKeywords.ColumnReverse, FlexDirection.ColumnReverse },
            { CssKeywords.Row, FlexDirection.Row },
            { CssKeywords.RowReverse, FlexDirection.RowReverse },
        };

        /// <summary>
        /// Contains the string-FlexWrapMode mapping.
        /// </summary>
        public static readonly Dictionary<String, FlexWrapMode> FlexWrapModes = new Dictionary<String, FlexWrapMode>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Nowrap, FlexWrapMode.NoWrap },
            { CssKeywords.Wrap, FlexWrapMode.Wrap },
            { CssKeywords.WrapReverse, FlexWrapMode.WrapReverse },
        };

        /// <summary>
        /// Contains the string-FlexContentMode mapping.
        /// </summary>
        public static readonly Dictionary<String, FlexContentMode> JustifyContentModes = new Dictionary<String, FlexContentMode>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.FlexStart, FlexContentMode.Start },
            { CssKeywords.FlexEnd, FlexContentMode.End },
            { CssKeywords.Center, FlexContentMode.Center },
            { CssKeywords.SpaceBetween, FlexContentMode.SpaceBetween },
            { CssKeywords.SpaceAround, FlexContentMode.SpaceAround },
        };

        /// <summary>
        /// Contains the string-FlexContentMode mapping.
        /// </summary>
        public static readonly Dictionary<String, FlexContentMode> AlignContentModes = new Dictionary<String, FlexContentMode>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.FlexStart, FlexContentMode.Start },
            { CssKeywords.FlexEnd, FlexContentMode.End },
            { CssKeywords.Center, FlexContentMode.Center },
            { CssKeywords.SpaceBetween, FlexContentMode.SpaceBetween },
            { CssKeywords.SpaceAround, FlexContentMode.SpaceAround },
            { CssKeywords.Stretch, FlexContentMode.Stretch },
        };

        /// <summary>
        /// Contains the string-FlexContentMode mapping.
        /// </summary>
        public static readonly Dictionary<String, FlexContentMode> AlignItemsModes = new Dictionary<String, FlexContentMode>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.FlexStart, FlexContentMode.Start },
            { CssKeywords.FlexEnd, FlexContentMode.End },
            { CssKeywords.Center, FlexContentMode.Center },
            { CssKeywords.Baseline, FlexContentMode.Baseline },
            { CssKeywords.Stretch, FlexContentMode.Stretch },
        };

        /// <summary>
        /// Contains the string-FlexContentMode mapping.
        /// </summary>
        public static readonly Dictionary<String, FlexContentMode> AlignSelfModes = new Dictionary<String, FlexContentMode>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.Auto, FlexContentMode.Auto },
            { CssKeywords.FlexStart, FlexContentMode.Start },
            { CssKeywords.FlexEnd, FlexContentMode.End },
            { CssKeywords.Center, FlexContentMode.Center },
            { CssKeywords.Baseline, FlexContentMode.Baseline },
            { CssKeywords.Stretch, FlexContentMode.Stretch },
        };
    }
}
