namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// DOM Extensions for the CSSStyleDeclaration.
    /// </summary>
    [DomExposed("CSSStyleDeclaration")]
    public static class StyleDeclarationExtensions
    {
        /// <summary>
        /// Gets how a flex item's lines align within the flex
        /// container when there is extra space along the axis that is
        /// perpendicular to the axis defined by the flex-direction property.
        /// </summary>
        [DomName("alignContent")]
        [DomName("align-content")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAlignContent(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.AlignContent);

        /// <summary>
        /// Sets how a flex item's lines align within the flex
        /// container when there is extra space along the axis that is
        /// perpendicular to the axis defined by the flex-direction property.
        /// </summary>
        [DomName("alignContent")]
        [DomName("align-content")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAlignContent(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.AlignContent, value);

        /// <summary>
        /// Gets the alignment value (perpendicular to the layout axis
        /// defined by the flex-direction property) of flex items of the flex
        /// container.
        /// </summary>
        [DomName("alignItems")]
        [DomName("align-items")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAlignItems(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.AlignItems);

        /// <summary>
        /// Sets the alignment value (perpendicular to the layout axis
        /// defined by the flex-direction property) of flex items of the flex
        /// container.
        /// </summary>
        [DomName("alignItems")]
        [DomName("align-items")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAlignItems(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.AlignItems, value);

        /// <summary>
        /// Gets the alignment value (perpendicular to the layout axis
        /// defined by the flex-direction property) of flex items of the flex
        /// container.
        /// </summary>
        [DomName("alignSelf")]
        [DomName("align-self")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAlignSelf(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.AlignSelf);

        /// <summary>
        /// Sets the alignment value (perpendicular to the layout axis
        /// defined by the flex-direction property) of flex items of the flex
        /// container.
        /// </summary>
        [DomName("alignSelf")]
        [DomName("align-self")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAlignSelf(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.AlignSelf, value);

        /// <summary>
        /// Gets a string that indicates whether the object represents
        /// a keyboard shortcut.
        /// </summary>
        [DomName("accelerator")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAccelerator(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Accelerator);

        /// <summary>
        /// Sets a string that indicates whether the object represents
        /// a keyboard shortcut.
        /// </summary>
        [DomName("accelerator")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAccelerator(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Accelerator, value);

        /// <summary>
        /// Gets which baseline of this element is to be aligned with
        /// the corresponding baseline of the parent.
        /// </summary>
        [DomName("alignmentBaseline")]
        [DomName("alignment-baseline")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAlignmentBaseline(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.AlignBaseline);

        /// <summary>
        /// Sets which baseline of this element is to be aligned with
        /// the corresponding baseline of the parent.
        /// </summary>
        [DomName("alignmentBaseline")]
        [DomName("alignment-baseline")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAlignmentBaseline(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.AlignBaseline, value);

        /// <summary>
        /// Gets one or more shorthand values that define all animation
        /// properties (except animation-play-state) for a set of corresponding
        /// object properties identified in the CSS @keyframes at-rule
        /// specified by the animations-name property.
        /// </summary>
        [DomName("animation")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAnimation(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Animation);

        /// <summary>
        /// Sets one or more shorthand values that define all animation
        /// properties (except animation-play-state) for a set of corresponding
        /// object properties identified in the CSS @keyframes at-rule
        /// specified by the animations-name property.
        /// </summary>
        [DomName("animation")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAnimation(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Animation, value);

        /// <summary>
        /// Gets the offset within an animation cycle (the amount of
        /// time from the start of a cycle) before the animation is displayed
        /// for a set of corresponding object properties identified in the CSS
        /// @keyframes at-rule specified by the animation-name property.
        /// </summary>
        [DomName("animationDelay")]
        [DomName("animation-delay")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAnimationDelay(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.AnimationDelay);

        /// <summary>
        /// Sets the offset within an animation cycle (the amount of
        /// time from the start of a cycle) before the animation is displayed
        /// for a set of corresponding object properties identified in the CSS
        /// @keyframes at-rule specified by the animation-name property.
        /// </summary>
        [DomName("animationDelay")]
        [DomName("animation-delay")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAnimationDelay(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.AnimationDelay, value);

        /// <summary>
        /// Gets the direction of play for an animation cycle.
        /// </summary>
        [DomName("animationDirection")]
        [DomName("animation-direction")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAnimationDirection(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.AnimationDirection);

        /// <summary>
        /// Sets the direction of play for an animation cycle.
        /// </summary>
        [DomName("animationDirection")]
        [DomName("animation-direction")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAnimationDirection(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.AnimationDirection, value);

        /// <summary>
        /// Gets the length of time to complete one cycle of the
        /// animation.
        /// </summary>
        [DomName("animationDuration")]
        [DomName("animation-duration")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAnimationDuration(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.AnimationDuration);

        /// <summary>
        /// Sets the length of time to complete one cycle of the
        /// animation.
        /// </summary>
        [DomName("animationDuration")]
        [DomName("animation-duration")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAnimationDuration(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.AnimationDuration, value);

        /// <summary>
        /// Gets whether the effects of an animation are visible before
        /// or after it plays.
        /// </summary>
        [DomName("animationFillMode")]
        [DomName("animation-fill-mode")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAnimationFillMode(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.AnimationFillMode);

        /// <summary>
        /// Sets whether the effects of an animation are visible before
        /// or after it plays.
        /// </summary>
        [DomName("animationFillMode")]
        [DomName("animation-fill-mode")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAnimationFillMode(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.AnimationFillMode, value);

        /// <summary>
        /// Gets the number of times an animation cycle is played.
        /// </summary>
        [DomName("animationIterationCount")]
        [DomName("animation-iteration-count")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAnimationIterationCount(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.AnimationIterationCount);

        /// <summary>
        /// Sets the number of times an animation cycle is played.
        /// </summary>
        [DomName("animationIterationCount")]
        [DomName("animation-iteration-count")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAnimationIterationCount(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.AnimationIterationCount, value);

        /// <summary>
        /// Gets one or more animation names. An animation name selects
        /// a CSS @keyframes at-rule.
        /// </summary>
        [DomName("animationName")]
        [DomName("animation-name")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAnimationName(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.AnimationName);

        /// <summary>
        /// Sets one or more animation names. An animation name selects
        /// a CSS @keyframes at-rule.
        /// </summary>
        [DomName("animationName")]
        [DomName("animation-name")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAnimationName(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.AnimationName, value);

        /// <summary>
        /// Gets whether an animation is playing or paused.
        /// </summary>
        [DomName("animationPlayState")]
        [DomName("animation-play-state")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAnimationPlayState(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.AnimationPlayState);

        /// <summary>
        /// Sets whether an animation is playing or paused.
        /// </summary>
        [DomName("animationPlayState")]
        [DomName("animation-play-state")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAnimationPlayState(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.AnimationPlayState, value);

        /// <summary>
        /// Gets the intermediate property values to be used during a
        /// single cycle of an animation on a set of corresponding object
        /// properties identified in the CSS @keyframes at-rule specified by
        /// the animation-name property.
        /// </summary>
        [DomName("animationTimingFunction")]
        [DomName("animation-timing-function")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAnimationTimingFunction(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.AnimationTimingFunction);

        /// <summary>
        /// Sets the intermediate property values to be used during a
        /// single cycle of an animation on a set of corresponding object
        /// properties identified in the CSS @keyframes at-rule specified by
        /// the animation-name property.
        /// </summary>
        [DomName("animationTimingFunction")]
        [DomName("animation-timing-function")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAnimationTimingFunction(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.AnimationTimingFunction, value);

        /// <summary>
        /// Gets a value that specifies whether the back face (reverse
        /// side) of an object is visible.
        /// </summary>
        [DomName("backfaceVisibility")]
        [DomName("backface-visibility")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackfaceVisibility(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BackfaceVisibility);

        /// <summary>
        /// Sets a value that specifies whether the back face (reverse
        /// side) of an object is visible.
        /// </summary>
        [DomName("backfaceVisibility")]
        [DomName("backface-visibility")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackfaceVisibility(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BackfaceVisibility, value);

        /// <summary>
        /// Gets up to five separate background properties of an object.
        /// </summary>
        [DomName("background")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackground(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Background);

        /// <summary>
        /// Sets up to five separate background properties of an object.
        /// </summary>
        [DomName("background")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackground(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Background, value);

        /// <summary>
        /// Gets how the background image (or images) is attached to
        /// the object within the document.
        /// </summary>
        [DomName("backgroundAttachment")]
        [DomName("background-attachment")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackgroundAttachment(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BackgroundAttachment);

        /// <summary>
        /// Sets how the background image (or images) is attached to
        /// the object within the document.
        /// </summary>
        [DomName("backgroundAttachment")]
        [DomName("background-attachment")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackgroundAttachment(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BackgroundAttachment, value);

        /// <summary>
        /// Gets the background painting area or areas relative to the
        /// element's bounding boxes.
        /// </summary>
        [DomName("backgroundClip")]
        [DomName("background-clip")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackgroundClip(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BackgroundClip);

        /// <summary>
        /// Sets the background painting area or areas relative to the
        /// element's bounding boxes.
        /// </summary>
        [DomName("backgroundClip")]
        [DomName("background-clip")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackgroundClip(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BackgroundClip, value);

        /// <summary>
        /// Gets the color behind the content of the object.
        /// </summary>
        [DomName("backgroundColor")]
        [DomName("background-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackgroundColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BackgroundColor);

        /// <summary>
        /// Sets the color behind the content of the object.
        /// </summary>
        [DomName("backgroundColor")]
        [DomName("background-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackgroundColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BackgroundColor, value);

        /// <summary>
        /// Gets the background image or images of the object.
        /// </summary>
        [DomName("backgroundImage")]
        [DomName("background-image")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackgroundImage(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BackgroundImage);

        /// <summary>
        /// Sets the background image or images of the object.
        /// </summary>
        [DomName("backgroundImage")]
        [DomName("background-image")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackgroundImage(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BackgroundImage, value);

        /// <summary>
        /// Gets the positioning area of an element or multiple
        /// elements.
        /// </summary>
        [DomName("backgroundOrigin")]
        [DomName("background-origin")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackgroundOrigin(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BackgroundOrigin);

        /// <summary>
        /// Sets the positioning area of an element or multiple
        /// elements.
        /// </summary>
        [DomName("backgroundOrigin")]
        [DomName("background-origin")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackgroundOrigin(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BackgroundOrigin, value);

        /// <summary>
        /// Gets the position of the background of the object.
        /// </summary>
        [DomName("backgroundPosition")]
        [DomName("background-position")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackgroundPosition(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BackgroundPosition);

        /// <summary>
        /// Sets the position of the background of the object.
        /// </summary>
        [DomName("backgroundPosition")]
        [DomName("background-position")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackgroundPosition(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BackgroundPosition, value);

        /// <summary>
        /// Gets the x-coordinate of the background-position property.
        /// </summary>
        [DomName("backgroundPositionX")]
        [DomName("background-position-x")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackgroundPositionX(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BackgroundPositionX);

        /// <summary>
        /// Sets the x-coordinate of the background-position property.
        /// </summary>
        [DomName("backgroundPositionX")]
        [DomName("background-position-x")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackgroundPositionX(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BackgroundPositionX, value);

        /// <summary>
        /// Gets the y-coordinate of the background-position property.
        /// </summary>
        [DomName("backgroundPositionY")]
        [DomName("background-position-y")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackgroundPositionY(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BackgroundPositionY);

        /// <summary>
        /// Sets the y-coordinate of the background-position property.
        /// </summary>
        [DomName("backgroundPositionY")]
        [DomName("background-position-y")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackgroundPositionY(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BackgroundPositionY, value);

        /// <summary>
        /// Gets whether and how the background image (or images) is
        /// tiled.
        /// </summary>
        [DomName("backgroundRepeat")]
        [DomName("background-repeat")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackgroundRepeat(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BackgroundRepeat);

        /// <summary>
        /// Sets whether and how the background image (or images) is
        /// tiled.
        /// </summary>
        [DomName("backgroundRepeat")]
        [DomName("background-repeat")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackgroundRepeat(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BackgroundRepeat, value);

        /// <summary>
        /// Gets the size of the background images.
        /// </summary>
        [DomName("backgroundSize")]
        [DomName("background-size")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackgroundSize(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BackgroundSize);

        /// <summary>
        /// Sets the size of the background images.
        /// </summary>
        [DomName("backgroundSize")]
        [DomName("background-size")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackgroundSize(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BackgroundSize, value);

        /// <summary>
        /// Gets a value that indicates how the dominant baseline
        /// should be repositioned relative to the dominant baseline of the
        /// parent text content element.
        /// </summary>
        [DomName("baselineShift")]
        [DomName("baseline-shift")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBaselineShift(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BaselineShift);

        /// <summary>
        /// Sets a value that indicates how the dominant baseline
        /// should be repositioned relative to the dominant baseline of the
        /// parent text content element.
        /// </summary>
        [DomName("baselineShift")]
        [DomName("baseline-shift")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBaselineShift(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BaselineShift, value);

        /// <summary>
        /// Gets the location of the Dynamic HTML (DHTML) behavior 
        /// DHTML Behaviors.
        /// </summary>
        [DomName("behavior")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBehavior(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Behavior);

        /// <summary>
        /// Sets the location of the Dynamic HTML (DHTML) behavior 
        /// DHTML Behaviors.
        /// </summary>
        [DomName("behavior")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBehavior(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Behavior, value);

        /// <summary>
        /// Gets the position of the object relative to the top of
        /// the next positioned object in the document hierarchy.
        /// </summary>
        [DomName("bottom")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBottom(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Bottom);

        /// <summary>
        /// Sets the position of the object relative to the top of
        /// the next positioned object in the document hierarchy.
        /// </summary>
        [DomName("bottom")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBottom(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Bottom, value);

        /// <summary>
        /// Gets the properties of a border drawn around an object.
        /// </summary>
        [DomName("border")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorder(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Border);

        /// <summary>
        /// Sets the properties of a border drawn around an object.
        /// </summary>
        [DomName("border")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorder(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Border, value);

        /// <summary>
        /// Gets the properties of the bottom border of the object.
        /// </summary>
        [DomName("borderBottom")]
        [DomName("border-bottom")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderBottom(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderBottom);

        /// <summary>
        /// Sets the properties of the bottom border of the object.
        /// </summary>
        [DomName("borderBottom")]
        [DomName("border-bottom")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderBottom(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderBottom, value);

        /// <summary>
        /// Gets the foreground color of the bottom border of an
        /// object.
        /// </summary>
        [DomName("borderBottomColor")]
        [DomName("border-bottom-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderBottomColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderBottomColor);

        /// <summary>
        /// Sets the foreground color of the bottom border of an
        /// object.
        /// </summary>
        [DomName("borderBottomColor")]
        [DomName("border-bottom-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderBottomColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderBottomColor, value);

        /// <summary>
        /// Gets the radii of the quarter ellipse that defines the
        /// shape of the lower-left corner for the outer border edge of the
        /// current box.
        /// </summary>
        [DomName("borderBottomLeftRadius")]
        [DomName("border-bottom-left-radius")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderBottomLeftRadius(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderBottomLeftRadius);

        /// <summary>
        /// Sets the radii of the quarter ellipse that defines the
        /// shape of the lower-left corner for the outer border edge of the
        /// current box.
        /// </summary>
        [DomName("borderBottomLeftRadius")]
        [DomName("border-bottom-left-radius")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderBottomLeftRadius(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderBottomLeftRadius, value);

        /// <summary>
        /// Gets one or two values that define the radii of the quarter
        /// ellipse that defines the shape of the lower-right corner for the
        /// outer border edge of the current box.
        /// </summary>
        [DomName("borderBottomRightRadius")]
        [DomName("border-bottom-right-radius")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderBottomRightRadius(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderBottomRightRadius);

        /// <summary>
        /// Sets one or two values that define the radii of the quarter
        /// ellipse that defines the shape of the lower-right corner for the
        /// outer border edge of the current box.
        /// </summary>
        [DomName("borderBottomRightRadius")]
        [DomName("border-bottom-right-radius")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderBottomRightRadius(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderBottomRightRadius, value);

        /// <summary>
        /// Gets the style of the bottom border of the object.
        /// </summary>
        [DomName("borderBottomStyle")]
        [DomName("border-bottom-style")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderBottomStyle(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderBottomStyle);

        /// <summary>
        /// Sets the style of the bottom border of the object.
        /// </summary>
        [DomName("borderBottomStyle")]
        [DomName("border-bottom-style")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderBottomStyle(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderBottomStyle, value);

        /// <summary>
        /// Gets the thickness of the bottom border of the object.
        /// </summary>
        [DomName("borderBottomWidth")]
        [DomName("border-bottom-width")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderBottomWidth(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderBottomWidth);

        /// <summary>
        /// Sets the thickness of the bottom border of the object.
        /// </summary>
        [DomName("borderBottomWidth")]
        [DomName("border-bottom-width")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderBottomWidth(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderBottomWidth, value);

        /// <summary>
        /// Gets whether the row and cell borders of a table are joined
        /// in a single border or detached as in standard HTML.
        /// </summary>
        [DomName("borderCollapse")]
        [DomName("border-collapse")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderCollapse(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderCollapse);

        /// <summary>
        /// Sets whether the row and cell borders of a table are joined
        /// in a single border or detached as in standard HTML.
        /// </summary>
        [DomName("borderCollapse")]
        [DomName("border-collapse")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderCollapse(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderCollapse, value);

        /// <summary>
        /// Gets the border color of the object.
        /// </summary>
        [DomName("borderColor")]
        [DomName("border-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderColor);

        /// <summary>
        /// Sets the border color of the object.
        /// </summary>
        [DomName("borderColor")]
        [DomName("border-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderColor, value);

        /// <summary>
        /// Gets an image to be used in place of the border styles.
        /// </summary>
        [DomName("borderImage")]
        [DomName("border-image")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderImage(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderImage);

        /// <summary>
        /// Sets an image to be used in place of the border styles.
        /// </summary>
        [DomName("borderImage")]
        [DomName("border-image")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderImage(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderImage, value);

        /// <summary>
        /// Gets the amount by which the border image area extends
        /// beyond the border box.
        /// </summary>
        [DomName("borderImageOutset")]
        [DomName("border-image-outset")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderImageOutset(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderImageOutset);

        /// <summary>
        /// Sets the amount by which the border image area extends
        /// beyond the border box.
        /// </summary>
        [DomName("borderImageOutset")]
        [DomName("border-image-outset")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderImageOutset(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderImageOutset, value);

        /// <summary>
        /// Gets how the image is scaled and tiled.
        /// </summary>
        [DomName("borderImageRepeat")]
        [DomName("border-image-repeat")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderImageRepeat(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderImageRepeat);

        /// <summary>
        /// Sets how the image is scaled and tiled.
        /// </summary>
        [DomName("borderImageRepeat")]
        [DomName("border-image-repeat")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderImageRepeat(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderImageRepeat, value);

        /// <summary>
        /// Gets four inward offsets, this property slices the
        /// specified border image into a three by three grid: four corners,
        /// four edges, and a central region.
        /// </summary>
        [DomName("borderImageSlice")]
        [DomName("border-image-slice")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderImageSlice(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderImageSlice);

        /// <summary>
        /// Sets four inward offsets, this property slices the
        /// specified border image into a three by three grid: four corners,
        /// four edges, and a central region.
        /// </summary>
        [DomName("borderImageSlice")]
        [DomName("border-image-slice")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderImageSlice(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderImageSlice, value);

        /// <summary>
        /// Gets the path of the image to be used for the border.
        /// </summary>
        [DomName("borderImageSource")]
        [DomName("border-image-source")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderImageSource(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderImageSource);

        /// <summary>
        /// Sets the path of the image to be used for the border.
        /// </summary>
        [DomName("borderImageSource")]
        [DomName("border-image-source")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderImageSource(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderImageSource, value);

        /// <summary>
        /// Gets the inward offsets from the outer border edge.
        /// </summary>
        [DomName("borderImageWidth")]
        [DomName("border-image-width")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderImageWidth(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderImageWidth);

        /// <summary>
        /// Sets the inward offsets from the outer border edge.
        /// </summary>
        [DomName("borderImageWidth")]
        [DomName("border-image-width")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderImageWidth(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderImageWidth, value);

        /// <summary>
        /// Gets the properties of the left border of the object.
        /// </summary>
        [DomName("borderLeft")]
        [DomName("border-left")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderLeft(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderLeft);

        /// <summary>
        /// Sets the properties of the left border of the object.
        /// </summary>
        [DomName("borderLeft")]
        [DomName("border-left")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderLeft(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderLeft, value);

        /// <summary>
        /// Gets the foreground color of the left border of an object.
        /// </summary>
        [DomName("borderLeftColor")]
        [DomName("border-left-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderLeftColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderLeftColor);

        /// <summary>
        /// Sets the foreground color of the left border of an object.
        /// </summary>
        [DomName("borderLeftColor")]
        [DomName("border-left-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderLeftColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderLeftColor, value);

        /// <summary>
        /// Gets the style of the left border of the object.
        /// </summary>
        [DomName("borderLeftStyle")]
        [DomName("border-left-style")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderLeftStyle(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderLeftStyle);

        /// <summary>
        /// Sets the style of the left border of the object.
        /// </summary>
        [DomName("borderLeftStyle")]
        [DomName("border-left-style")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderLeftStyle(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderLeftStyle, value);

        /// <summary>
        /// Gets the thickness of the left border of the object.
        /// </summary>
        [DomName("borderLeftWidth")]
        [DomName("border-left-width")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderLeftWidth(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderLeftWidth);

        /// <summary>
        /// Sets the thickness of the left border of the object.
        /// </summary>
        [DomName("borderLeftWidth")]
        [DomName("border-left-width")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderLeftWidth(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderLeftWidth, value);

        /// <summary>
        /// Gets the radii of a quarter ellipse that defines the shape
        /// of the corners for the outer border edge of the current box.
        /// </summary>
        [DomName("borderRadius")]
        [DomName("border-radius")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderRadius(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderRadius);

        /// <summary>
        /// Sets the radii of a quarter ellipse that defines the shape
        /// of the corners for the outer border edge of the current box.
        /// </summary>
        [DomName("borderRadius")]
        [DomName("border-radius")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderRadius(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderRadius, value);

        /// <summary>
        /// Gets the properties of the right border of the object.
        /// </summary>
        [DomName("borderRight")]
        [DomName("border-right")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderRight(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderRight);

        /// <summary>
        /// Sets the properties of the right border of the object.
        /// </summary>
        [DomName("borderRight")]
        [DomName("border-right")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderRight(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderRight, value);

        /// <summary>
        /// Gets the foreground color of the right border of an object.
        /// </summary>
        [DomName("borderRightColor")]
        [DomName("border-right-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderRightColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderRightColor);

        /// <summary>
        /// Sets the foreground color of the right border of an object.
        /// </summary>
        [DomName("borderRightColor")]
        [DomName("border-right-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderRightColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderRightColor, value);

        /// <summary>
        /// Gets the style of the right border of the object.
        /// </summary>
        [DomName("borderRightStyle")]
        [DomName("border-right-style")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderRightStyle(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderRightStyle);

        /// <summary>
        /// Sets the style of the right border of the object.
        /// </summary>
        [DomName("borderRightStyle")]
        [DomName("border-right-style")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderRightStyle(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderRightStyle, value);

        /// <summary>
        /// Gets the thickness of the right border of the object.
        /// </summary>
        [DomName("borderRightWidth")]
        [DomName("border-right-width")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderRightWidth(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderRightWidth);

        /// <summary>
        /// Sets the thickness of the right border of the object.
        /// </summary>
        [DomName("borderRightWidth")]
        [DomName("border-right-width")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderRightWidth(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderRightWidth, value);

        /// <summary>
        /// Gets the distance between the borders of adjoining cells in
        /// a table.
        /// </summary>
        [DomName("borderSpacing")]
        [DomName("border-spacing")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderSpacing(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderSpacing);

        /// <summary>
        /// Sets the distance between the borders of adjoining cells in
        /// a table.
        /// </summary>
        [DomName("borderSpacing")]
        [DomName("border-spacing")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderSpacing(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderSpacing, value);

        /// <summary>
        /// Gets the style of the left, right, top, and bottom borders
        /// of the object.
        /// </summary>
        [DomName("borderStyle")]
        [DomName("border-style")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderStyle(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderStyle);

        /// <summary>
        /// Sets the style of the left, right, top, and bottom borders
        /// of the object.
        /// </summary>
        [DomName("borderStyle")]
        [DomName("border-style")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderStyle(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderStyle, value);

        /// <summary>
        /// Gets the properties of the top border of the object.
        /// </summary>
        [DomName("borderTop")]
        [DomName("border-top")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderTop(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderTop);

        /// <summary>
        /// Sets the properties of the top border of the object.
        /// </summary>
        [DomName("borderTop")]
        [DomName("border-top")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderTop(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderTop, value);

        /// <summary>
        /// Gets the foreground color of the top border of an object.
        /// </summary>
        [DomName("borderTopColor")]
        [DomName("border-top-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderTopColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderTopColor);

        /// <summary>
        /// Sets the foreground color of the top border of an object.
        /// </summary>
        [DomName("borderTopColor")]
        [DomName("border-top-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderTopColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderTopColor, value);

        /// <summary>
        /// Gets one or two values that define the radii of the
        /// quarter ellipse that defines the shape of the upper-left corner for
        /// the outer border edge of the current box.
        /// </summary>
        [DomName("borderTopLeftRadius")]
        [DomName("border-top-left-radius")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderTopLeftRadius(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderTopLeftRadius);

        /// <summary>
        /// Sets one or two values that define the radii of the
        /// quarter ellipse that defines the shape of the upper-left corner for
        /// the outer border edge of the current box.
        /// </summary>
        [DomName("borderTopLeftRadius")]
        [DomName("border-top-left-radius")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderTopLeftRadius(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderTopLeftRadius, value);

        /// <summary>
        /// Gets one or two values that define the radii of the quarter
        /// ellipse that defines the shape of the upper-right corner for the
        /// outer border edge of the current box.
        /// </summary>
        [DomName("borderTopRightRadius")]
        [DomName("border-top-right-radius")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderTopRightRadius(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderTopRightRadius);

        /// <summary>
        /// Sets one or two values that define the radii of the quarter
        /// ellipse that defines the shape of the upper-right corner for the
        /// outer border edge of the current box.
        /// </summary>
        [DomName("borderTopRightRadius")]
        [DomName("border-top-right-radius")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderTopRightRadius(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderTopRightRadius, value);

        /// <summary>
        /// Gets the style of the top border of the object.
        /// </summary>
        [DomName("borderTopStyle")]
        [DomName("border-top-style")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderTopStyle(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderTopStyle);

        /// <summary>
        /// Sets the style of the top border of the object.
        /// </summary>
        [DomName("borderTopStyle")]
        [DomName("border-top-style")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderTopStyle(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderTopStyle, value);

        /// <summary>
        /// Gets the thickness of the top border of the object.
        /// </summary>
        [DomName("borderTopWidth")]
        [DomName("border-top-width")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderTopWidth(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderTopWidth);

        /// <summary>
        /// Sets the thickness of the top border of the object.
        /// </summary>
        [DomName("borderTopWidth")]
        [DomName("border-top-width")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderTopWidth(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderTopWidth, value);

        /// <summary>
        /// Gets the thicknesses of the left, right, top, and bottom
        /// borders of an object.
        /// </summary>
        [DomName("borderWidth")]
        [DomName("border-width")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderWidth(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderWidth);

        /// <summary>
        /// Sets the thicknesses of the left, right, top, and bottom
        /// borders of an object.
        /// </summary>
        [DomName("borderWidth")]
        [DomName("border-width")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderWidth(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderWidth, value);

        /// <summary>
        /// Gets one or more set of shadow values that attaches one or
        /// more drop shadows to the current box.
        /// </summary>
        [DomName("boxShadow")]
        [DomName("box-shadow")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBoxShadow(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BoxShadow);

        /// <summary>
        /// Sets one or more set of shadow values that attaches one or
        /// more drop shadows to the current box.
        /// </summary>
        [DomName("boxShadow")]
        [DomName("box-shadow")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBoxShadow(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BoxShadow, value);

        /// <summary>
        /// Gets the box model to use for object sizing.
        /// </summary>
        [DomName("boxSizing")]
        [DomName("box-sizing")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBoxSizing(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BoxSizing);

        /// <summary>
        /// Sets the box model to use for object sizing.
        /// </summary>
        [DomName("boxSizing")]
        [DomName("box-sizing")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBoxSizing(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BoxSizing, value);

        /// <summary>
        /// Gets the column-break behavior that follows a content block
        /// in a multi-column element.
        /// </summary>
        [DomName("breakAfter")]
        [DomName("break-after")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBreakAfter(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BreakAfter);

        /// <summary>
        /// Sets the column-break behavior that follows a content block
        /// in a multi-column element.
        /// </summary>
        [DomName("breakAfter")]
        [DomName("break-after")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBreakAfter(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BreakAfter, value);

        /// <summary>
        /// Gets the column-break behavior that precedes a content
        /// block in a multi-column element.
        /// </summary>
        [DomName("breakBefore")]
        [DomName("break-before")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBreakBefore(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BreakBefore);

        /// <summary>
        /// Sets the column-break behavior that precedes a content
        /// block in a multi-column element.
        /// </summary>
        [DomName("breakBefore")]
        [DomName("break-before")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBreakBefore(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BreakBefore, value);

        /// <summary>
        /// Gets the column-break behavior that occurs within a
        /// content block in a multi-column element.
        /// </summary>
        [DomName("breakInside")]
        [DomName("break-inside")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBreakInside(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BreakInside);

        /// <summary>
        /// Sets the column-break behavior that occurs within a
        /// content block in a multi-column element.
        /// </summary>
        [DomName("breakInside")]
        [DomName("break-inside")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBreakInside(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BreakInside, value);

        /// <summary>
        /// Gets where the caption of a table is located.
        /// </summary>
        [DomName("captionSide")]
        [DomName("caption-side")]
        [DomAccessor(Accessors.Getter)]
        public static String GetCaptionSide(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.CaptionSide);

        /// <summary>
        /// Sets where the caption of a table is located.
        /// </summary>
        [DomName("captionSide")]
        [DomName("caption-side")]
        [DomAccessor(Accessors.Setter)]
        public static void SetCaptionSide(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.CaptionSide, value);

        /// <summary>
        /// Gets whether the object allows floating objects on its left
        /// side, right side, or both, so that the next text displays past the
        /// floating objects.
        /// </summary>
        [DomName("clear")]
        [DomAccessor(Accessors.Getter)]
        public static String GetClear(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Clear);

        /// <summary>
        /// Sets whether the object allows floating objects on its left
        /// side, right side, or both, so that the next text displays past the
        /// floating objects.
        /// </summary>
        [DomName("clear")]
        [DomAccessor(Accessors.Setter)]
        public static void SetClear(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Clear, value);

        /// <summary>
        /// Gets which part of a positioned object is visible.
        /// </summary>
        [DomName("clip")]
        [DomAccessor(Accessors.Getter)]
        public static String GetClip(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Clip);

        /// <summary>
        /// Sets which part of a positioned object is visible.
        /// </summary>
        [DomName("clip")]
        [DomAccessor(Accessors.Setter)]
        public static void SetClip(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Clip, value);

        /// <summary>
        /// Gets the bottom coordinate of the object clipping region.
        /// </summary>
        [DomName("clipBottom")]
        [DomName("clip-bottom")]
        [DomAccessor(Accessors.Getter)]
        public static String GetClipBottom(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ClipBottom);

        /// <summary>
        /// Sets the bottom coordinate of the object clipping region.
        /// </summary>
        [DomName("clipBottom")]
        [DomName("clip-bottom")]
        [DomAccessor(Accessors.Setter)]
        public static void SetClipBottom(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ClipBottom, value);

        /// <summary>
        /// Gets the left coordinate of the object clipping region.
        /// </summary>
        [DomName("clipLeft")]
        [DomName("clip-left")]
        [DomAccessor(Accessors.Getter)]
        public static String GetClipLeft(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ClipLeft);

        /// <summary>
        /// Sets the left coordinate of the object clipping region.
        /// </summary>
        [DomName("clipLeft")]
        [DomName("clip-left")]
        [DomAccessor(Accessors.Setter)]
        public static void SetClipLeft(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ClipLeft, value);

        /// <summary>
        /// Gets a reference to the SVG graphical object
        /// that will be used as the clipping path.
        /// </summary>
        [DomName("clipPath")]
        [DomName("clip-path")]
        [DomAccessor(Accessors.Getter)]
        public static String GetClipPath(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ClipPath);

        /// <summary>
        /// Sets a reference to the SVG graphical object
        /// that will be used as the clipping path.
        /// </summary>
        [DomName("clipPath")]
        [DomName("clip-path")]
        [DomAccessor(Accessors.Setter)]
        public static void SetClipPath(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ClipPath, value);

        /// <summary>
        /// Gets the right coordinate of the object clipping region.
        /// </summary>
        [DomName("clipRight")]
        [DomName("clip-right")]
        [DomAccessor(Accessors.Getter)]
        public static String GetClipRight(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ClipRight);

        /// <summary>
        /// Sets the right coordinate of the object clipping region.
        /// </summary>
        [DomName("clipRight")]
        [DomName("clip-right")]
        [DomAccessor(Accessors.Setter)]
        public static void SetClipRight(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ClipRight, value);

        /// <summary>
        /// Gets the algorithm used to determine what parts of the
        /// canvas are affected by the fill operation.
        /// </summary>
        [DomName("clipRule")]
        [DomName("clip-rule")]
        [DomAccessor(Accessors.Getter)]
        public static String GetClipRule(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ClipRule);

        /// <summary>
        /// Sets the algorithm used to determine what parts of the
        /// canvas are affected by the fill operation.
        /// </summary>
        [DomName("clipRule")]
        [DomName("clip-rule")]
        [DomAccessor(Accessors.Setter)]
        public static void SetClipRule(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ClipRule, value);

        /// <summary>
        /// Gets the top coordinate of the object clipping region.
        /// </summary>
        [DomName("clipTop")]
        [DomName("clip-top")]
        [DomAccessor(Accessors.Getter)]
        public static String GetClipTop(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ClipTop);

        /// <summary>
        /// Sets the top coordinate of the object clipping region.
        /// </summary>
        [DomName("clipTop")]
        [DomName("clip-top")]
        [DomAccessor(Accessors.Setter)]
        public static void SetClipTop(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ClipTop, value);

        /// <summary>
        /// Gets the foreground color of the text of an object.
        /// </summary>
        [DomName("color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Color);

        /// <summary>
        /// Sets the foreground color of the text of an object.
        /// </summary>
        [DomName("color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Color, value);

        /// <summary>
        /// Gets which color space to use for filter effects.
        /// </summary>
        [DomName("colorInterpolationFilters")]
        [DomName("color-interpolation-filters")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColorInterpolationFilters(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ColorInterpolationFilters);

        /// <summary>
        /// Sets which color space to use for filter effects.
        /// </summary>
        [DomName("colorInterpolationFilters")]
        [DomName("color-interpolation-filters")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColorInterpolationFilters(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ColorInterpolationFilters, value);

        /// <summary>
        /// Gets the optimal number of columns in a multi-column
        /// element.
        /// </summary>
        [DomName("columnCount")]
        [DomName("column-count")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColumnCount(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ColumnCount);

        /// <summary>
        /// Sets the optimal number of columns in a multi-column
        /// element.
        /// </summary>
        [DomName("columnCount")]
        [DomName("column-count")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColumnCount(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ColumnCount, value);

        /// <summary>
        /// Gets a value that indicates how the column lengths in a
        /// multi-column element are affected by the content flow.
        /// </summary>
        [DomName("columnFill")]
        [DomName("column-fill")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColumnFill(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ColumnFill);

        /// <summary>
        /// Sets a value that indicates how the column lengths in a
        /// multi-column element are affected by the content flow.
        /// </summary>
        [DomName("columnFill")]
        [DomName("column-fill")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColumnFill(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ColumnFill, value);

        /// <summary>
        /// Gets the width of the gap between columns in a multi-column
        /// element.
        /// </summary>
        [DomName("columnGap")]
        [DomName("column-gap")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColumnGap(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ColumnGap);

        /// <summary>
        /// Sets the width of the gap between columns in a multi-column
        /// element.
        /// </summary>
        [DomName("columnGap")]
        [DomName("column-gap")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColumnGap(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ColumnGap, value);

        /// <summary>
        /// Gets a shorthand value  that specifies values for the
        /// columnRuleWidth, columnRuleStyle, and the columnRuleColor of a
        /// multi-column element.
        /// </summary>
        [DomName("columnRule")]
        [DomName("column-rule")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColumnRule(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ColumnRule);

        /// <summary>
        /// Sets a shorthand value  that specifies values for the
        /// columnRuleWidth, columnRuleStyle, and the columnRuleColor of a
        /// multi-column element.
        /// </summary>
        [DomName("columnRule")]
        [DomName("column-rule")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColumnRule(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ColumnRule, value);

        /// <summary>
        /// Gets the color for all column rules in a multi-column
        /// element.
        /// </summary>
        [DomName("columnRuleColor")]
        [DomName("column-rule-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColumnRuleColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ColumnRuleColor);

        /// <summary>
        /// Sets the color for all column rules in a multi-column
        /// element.
        /// </summary>
        [DomName("columnRuleColor")]
        [DomName("column-rule-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColumnRuleColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ColumnRuleColor, value);

        /// <summary>
        /// Gets the style for all column rules in a multi-column
        /// element.
        /// </summary>
        [DomName("columnRuleStyle")]
        [DomName("column-rule-style")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColumnRuleStyle(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ColumnRuleStyle);

        /// <summary>
        /// Sets the style for all column rules in a multi-column
        /// element.
        /// </summary>
        [DomName("columnRuleStyle")]
        [DomName("column-rule-style")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColumnRuleStyle(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ColumnRuleStyle, value);

        /// <summary>
        /// Gets the width of all column rules in a multi-column
        /// element.
        /// </summary>
        [DomName("columnRuleWidth")]
        [DomName("column-rule-width")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColumnRuleWidth(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ColumnRuleWidth);

        /// <summary>
        /// Sets the width of all column rules in a multi-column
        /// element.
        /// </summary>
        [DomName("columnRuleWidth")]
        [DomName("column-rule-width")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColumnRuleWidth(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ColumnRuleWidth, value);

        /// <summary>
        /// Gets a shorthand value that specifies values for the
        /// column-width and the column-count of a multi-column element.
        /// </summary>
        [DomName("columns")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColumns(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Columns);

        /// <summary>
        /// Sets a shorthand value that specifies values for the
        /// column-width and the column-count of a multi-column element.
        /// </summary>
        [DomName("columns")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColumns(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Columns, value);

        /// <summary>
        /// Gets the number of columns that a content block
        /// spans in a multi-column element.
        /// </summary>
        [DomName("columnSpan")]
        [DomName("column-span")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColumnSpan(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ColumnSpan);

        /// <summary>
        /// Sets the number of columns that a content block
        /// spans in a multi-column element.
        /// </summary>
        [DomName("columnSpan")]
        [DomName("column-span")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColumnSpan(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ColumnSpan, value);

        /// <summary>
        /// Gets the optimal width of the columns in a multi-column
        /// element.
        /// </summary>
        [DomName("columnWidth")]
        [DomName("column-width")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColumnWidth(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ColumnWidth);

        /// <summary>
        /// Sets the optimal width of the columns in a multi-column
        /// element.
        /// </summary>
        [DomName("columnWidth")]
        [DomName("column-width")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColumnWidth(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ColumnWidth, value);

        /// <summary>
        /// Gets generated content to insert before or after an
        /// element.
        /// </summary>
        [DomName("content")]
        [DomAccessor(Accessors.Getter)]
        public static String GetContent(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Content);

        /// <summary>
        /// Sets generated content to insert before or after an
        /// element.
        /// </summary>
        [DomName("content")]
        [DomAccessor(Accessors.Setter)]
        public static void SetContent(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Content, value);

        /// <summary>
        /// Gets a list of counters to increment.
        /// </summary>
        [DomName("counterIncrement")]
        [DomName("counter-increment")]
        [DomAccessor(Accessors.Getter)]
        public static String GetCounterIncrement(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.CounterIncrement);

        /// <summary>
        /// Sets a list of counters to increment.
        /// </summary>
        [DomName("counterIncrement")]
        [DomName("counter-increment")]
        [DomAccessor(Accessors.Setter)]
        public static void SetCounterIncrement(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.CounterIncrement, value);

        /// <summary>
        /// Gets a list of counters to create or reset to zero.
        /// </summary>
        [DomName("counterReset")]
        [DomName("counter-reset")]
        [DomAccessor(Accessors.Getter)]
        public static String GetCounterReset(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.CounterReset);

        /// <summary>
        /// Sets a list of counters to create or reset to zero.
        /// </summary>
        [DomName("counterReset")]
        [DomName("counter-reset")]
        [DomAccessor(Accessors.Setter)]
        public static void SetCounterReset(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.CounterReset, value);

        /// <summary>
        /// Gets a value that specifies whether a box should float to
        /// the left, right, or not at all.
        /// </summary>
        [DomName("cssFloat")]
        [DomName("float")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFloat(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Float);

        /// <summary>
        /// Sets a value that specifies whether a box should float to
        /// the left, right, or not at all.
        /// </summary>
        [DomName("cssFloat")]
        [DomName("float")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFloat(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Float, value);

        /// <summary>
        /// Gets the type of cursor to display as the mouse pointer
        /// moves over the object.
        /// </summary>
        [DomName("cursor")]
        [DomAccessor(Accessors.Getter)]
        public static String GetCursor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Cursor);

        /// <summary>
        /// Sets the type of cursor to display as the mouse pointer
        /// moves over the object.
        /// </summary>
        [DomName("cursor")]
        [DomAccessor(Accessors.Setter)]
        public static void SetCursor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Cursor, value);

        /// <summary>
        /// Gets the reading order of the object.
        /// </summary>
        [DomName("direction")]
        [DomAccessor(Accessors.Getter)]
        public static String GetDirection(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Direction);

        /// <summary>
        /// Sets the reading order of the object.
        /// </summary>
        [DomName("direction")]
        [DomAccessor(Accessors.Setter)]
        public static void SetDirection(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Direction, value);

        /// <summary>
        /// Gets a value that indicates whether and how the object is
        /// rendered.
        /// </summary>
        [DomName("display")]
        [DomAccessor(Accessors.Getter)]
        public static String GetDisplay(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Display);

        /// <summary>
        /// Sets a value that indicates whether and how the object is
        /// rendered.
        /// </summary>
        [DomName("display")]
        [DomAccessor(Accessors.Setter)]
        public static void SetDisplay(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Display, value);

        /// <summary>
        /// Gets a value that determines or redetermines a
        /// scaled-baseline table.
        /// </summary>
        [DomName("dominantBaseline")]
        [DomName("dominant-baseline")]
        [DomAccessor(Accessors.Getter)]
        public static String GetDominantBaseline(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.DominantBaseline);

        /// <summary>
        /// Sets a value that determines or redetermines a
        /// scaled-baseline table.
        /// </summary>
        [DomName("dominantBaseline")]
        [DomName("dominant-baseline")]
        [DomAccessor(Accessors.Setter)]
        public static void SetDominantBaseline(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.DominantBaseline, value);

        /// <summary>
        /// Gets whether to show or hide a cell without content.
        /// </summary>
        [DomName("emptyCells")]
        [DomName("empty-cells")]
        [DomAccessor(Accessors.Getter)]
        public static String GetEmptyCells(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.EmptyCells);

        /// <summary>
        /// Sets whether to show or hide a cell without content.
        /// </summary>
        [DomName("emptyCells")]
        [DomName("empty-cells")]
        [DomAccessor(Accessors.Setter)]
        public static void SetEmptyCells(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.EmptyCells, value);

        /// <summary>
        /// Gets a shared background image all graphic elements within a
        /// container.
        /// </summary>
        [DomName("enableBackground")]
        [DomName("enable-background")]
        [DomAccessor(Accessors.Getter)]
        public static String GetEnableBackground(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.EnableBackground);

        /// <summary>
        /// Sets a shared background image all graphic elements within a
        /// container.
        /// </summary>
        [DomName("enableBackground")]
        [DomName("enable-background")]
        [DomAccessor(Accessors.Setter)]
        public static void SetEnableBackground(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.EnableBackground, value);

        /// <summary>
        /// Gets a value that indicates the color to paint the
        /// interior of the given graphical element.
        /// </summary>
        [DomName("fill")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFill(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Fill);

        /// <summary>
        /// Sets a value that indicates the color to paint the
        /// interior of the given graphical element.
        /// </summary>
        [DomName("fill")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFill(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Fill, value);

        /// <summary>
        /// Gets a value that specifies the opacity of the painting
        /// operation that is used to paint the interior of the current object.
        /// </summary>
        [DomName("fillOpacity")]
        [DomName("fill-opacity")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFillOpacity(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FillOpacity);

        /// <summary>
        /// Sets a value that specifies the opacity of the painting
        /// operation that is used to paint the interior of the current object.
        /// </summary>
        [DomName("fillOpacity")]
        [DomName("fill-opacity")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFillOpacity(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FillOpacity, value);

        /// <summary>
        /// Gets a value that indicates the algorithm that is to be
        /// used to determine what parts of the canvas are included inside the
        /// shape.
        /// </summary>
        [DomName("fillRule")]
        [DomName("fill-rule")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFillRule(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FillRule);

        /// <summary>
        /// Sets a value that indicates the algorithm that is to be
        /// used to determine what parts of the canvas are included inside the
        /// shape.
        /// </summary>
        [DomName("fillRule")]
        [DomName("fill-rule")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFillRule(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FillRule, value);

        /// <summary>
        /// Gets the filter property is generally used to apply a previously
        /// define filter to an applicable element.
        /// </summary>
        [DomName("filter")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFilter(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Filter);

        /// <summary>
        /// Sets the filter property is generally used to apply a previously
        /// define filter to an applicable element.
        /// </summary>
        [DomName("filter")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFilter(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Filter, value);

        /// <summary>
        /// Gets the parameter values of a flexible length, the
        /// positive and negative flexibility, and the preferred size.
        /// </summary>
        [DomName("flex")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFlex(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Flex);

        /// <summary>
        /// Sets the parameter values of a flexible length, the
        /// positive and negative flexibility, and the preferred size.
        /// </summary>
        [DomName("flex")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFlex(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Flex, value);

        /// <summary>
        /// Gets the initial main size of the flex item.
        /// </summary>
        [DomName("flexBasis")]
        [DomName("flex-basis")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFlexBasis(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FlexBasis);

        /// <summary>
        /// Sets the initial main size of the flex item.
        /// </summary>
        [DomName("flexBasis")]
        [DomName("flex-basis")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFlexBasis(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FlexBasis, value);

        /// <summary>
        /// Gets the direction of the main axis which specifies how the
        /// flex items are displayed in the flex container.
        /// </summary>
        [DomName("flexDirection")]
        [DomName("flex-direction")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFlexDirection(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FlexDirection);

        /// <summary>
        /// Sets the direction of the main axis which specifies how the
        /// flex items are displayed in the flex container.
        /// </summary>
        [DomName("flexDirection")]
        [DomName("flex-direction")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFlexDirection(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FlexDirection, value);

        /// <summary>
        /// Gets the shorthand property to set both the flex-direction
        /// and flex-wrap properties of a flex container.
        /// </summary>
        [DomName("flexFlow")]
        [DomName("flex-flow")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFlexFlow(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FlexFlow);

        /// <summary>
        /// Sets the shorthand property to set both the flex-direction
        /// and flex-wrap properties of a flex container.
        /// </summary>
        [DomName("flexFlow")]
        [DomName("flex-flow")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFlexFlow(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FlexFlow, value);

        /// <summary>
        /// Gets the flex grow factor for the flex item.
        /// </summary>
        [DomName("flexGrow")]
        [DomName("flex-grow")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFlexGrow(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FlexGrow);

        /// <summary>
        /// Sets the flex grow factor for the flex item.
        /// </summary>
        [DomName("flexGrow")]
        [DomName("flex-grow")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFlexGrow(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FlexGrow, value);

        /// <summary>
        /// Gets the flex shrink factor for the flex item.
        /// </summary>
        [DomName("flexShrink")]
        [DomName("flex-shrink")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFlexShrink(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FlexShrink);

        /// <summary>
        /// Sets the flex shrink factor for the flex item.
        /// </summary>
        [DomName("flexShrink")]
        [DomName("flex-shrink")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFlexShrink(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FlexShrink, value);

        /// <summary>
        /// Gets whether flex items wrap and the direction they wrap
        /// onto multiple lines or columns based on the space available in the
        /// flex container. 
        /// </summary>
        [DomName("flexWrap")]
        [DomName("flex-wrap")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFlexWrap(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FlexWrap);

        /// <summary>
        /// Sets whether flex items wrap and the direction they wrap
        /// onto multiple lines or columns based on the space available in the
        /// flex container. 
        /// </summary>
        [DomName("flexWrap")]
        [DomName("flex-wrap")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFlexWrap(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FlexWrap, value);

        /// <summary>
        /// Gets a combination of separate font properties of the
        /// object. Alternatively, sets or retrieves one or more of six
        /// user-preference fonts.
        /// </summary>
        [DomName("font")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFont(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Font);

        /// <summary>
        /// Sets a combination of separate font properties of the
        /// object. Alternatively, sets or retrieves one or more of six
        /// user-preference fonts.
        /// </summary>
        [DomName("font")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFont(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Font, value);

        /// <summary>
        /// Gets the name of the font used for text in the object.
        /// </summary>
        [DomName("fontFamily")]
        [DomName("font-family")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontFamily(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FontFamily);

        /// <summary>
        /// Sets the name of the font used for text in the object.
        /// </summary>
        [DomName("fontFamily")]
        [DomName("font-family")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontFamily(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FontFamily, value);

        /// <summary>
        /// Gets one or more values that specify glyph substitution and
        /// positioning in fonts that include OpenType layout features.
        /// </summary>
        [DomName("fontFeatureSettings")]
        [DomName("font-feature-settings")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontFeatureSettings(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FontFeatureSettings);

        /// <summary>
        /// Sets one or more values that specify glyph substitution and
        /// positioning in fonts that include OpenType layout features.
        /// </summary>
        [DomName("fontFeatureSettings")]
        [DomName("font-feature-settings")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontFeatureSettings(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FontFeatureSettings, value);

        /// <summary>
        /// Gets a value that indicates the font size used for text in
        /// the object.
        /// </summary>
        [DomName("fontSize")]
        [DomName("font-size")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontSize(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FontSize);

        /// <summary>
        /// Sets a value that indicates the font size used for text in
        /// the object.
        /// </summary>
        [DomName("fontSize")]
        [DomName("font-size")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontSize(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FontSize, value);

        /// <summary>
        /// Gets a value that specifies an aspect value for an element
        /// that will effectively preserve the x-height of the first choice
        /// font, whether it is substituted or not.
        /// </summary>
        [DomName("fontSizeAdjust")]
        [DomName("font-size-adjust")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontSizeAdjust(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FontSizeAdjust);

        /// <summary>
        /// Sets a value that specifies an aspect value for an element
        /// that will effectively preserve the x-height of the first choice
        /// font, whether it is substituted or not.
        /// </summary>
        [DomName("fontSizeAdjust")]
        [DomName("font-size-adjust")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontSizeAdjust(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FontSizeAdjust, value);

        /// <summary>
        /// Gets a value that indicates a normal, condensed, or
        /// expanded face of a font family.
        /// </summary>
        [DomName("fontStretch")]
        [DomName("font-stretch")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontStretch(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FontStretch);

        /// <summary>
        /// Sets a value that indicates a normal, condensed, or
        /// expanded face of a font family.
        /// </summary>
        [DomName("fontStretch")]
        [DomName("font-stretch")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontStretch(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FontStretch, value);

        /// <summary>
        /// Gets the font style of the object as italic, normal, or
        /// oblique.
        /// </summary>
        [DomName("fontStyle")]
        [DomName("font-style")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontStyle(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FontStyle);

        /// <summary>
        /// Sets the font style of the object as italic, normal, or
        /// oblique.
        /// </summary>
        [DomName("fontStyle")]
        [DomName("font-style")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontStyle(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FontStyle, value);

        /// <summary>
        /// Gets whether the text of the object is in small capital
        /// letters.
        /// </summary>
        [DomName("fontVariant")]
        [DomName("font-variant")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontVariant(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FontVariant);

        /// <summary>
        /// Sets whether the text of the object is in small capital
        /// letters.
        /// </summary>
        [DomName("fontVariant")]
        [DomName("font-variant")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontVariant(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FontVariant, value);

        /// <summary>
        /// Gets of sets the weight of the font of the object.
        /// </summary>
        [DomName("fontWeight")]
        [DomName("font-weight")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontWeight(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FontWeight);

        /// <summary>
        /// Sets of sets the weight of the font of the object.
        /// </summary>
        [DomName("fontWeight")]
        [DomName("font-weight")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontWeight(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FontWeight, value);

        /// <summary>
        /// Gets a value that alters the orientation of a sequence of
        /// characters relative to an inline-progression-direction of
        /// horizontal.
        /// </summary>
        [DomName("glyphOrientationHorizontal")]
        [DomName("glyph-orientation-horizontal")]
        [DomAccessor(Accessors.Getter)]
        public static String GetGlyphOrientationHorizontal(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.GlyphOrientationHorizontal);

        /// <summary>
        /// Sets a value that alters the orientation of a sequence of
        /// characters relative to an inline-progression-direction of
        /// horizontal.
        /// </summary>
        [DomName("glyphOrientationHorizontal")]
        [DomName("glyph-orientation-horizontal")]
        [DomAccessor(Accessors.Setter)]
        public static void SetGlyphOrientationHorizontal(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.GlyphOrientationHorizontal, value);

        /// <summary>
        /// Gets a value that alters the orientation of a sequence
        /// of characters relative to an inline-progression-direction of
        /// vertical.
        /// </summary>
        [DomName("glyphOrientationVertical")]
        [DomName("glyph-orientation-vertical")]
        [DomAccessor(Accessors.Getter)]
        public static String GetGlyphOrientationVertical(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.GlyphOrientationVertical);

        /// <summary>
        /// Sets a value that alters the orientation of a sequence
        /// of characters relative to an inline-progression-direction of
        /// vertical.
        /// </summary>
        [DomName("glyphOrientationVertical")]
        [DomName("glyph-orientation-vertical")]
        [DomAccessor(Accessors.Setter)]
        public static void SetGlyphOrientationVertical(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.GlyphOrientationVertical, value);

        /// <summary>
        /// Gets the height of the object.
        /// </summary>
        [DomName("height")]
        [DomAccessor(Accessors.Getter)]
        public static String GetHeight(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Height);

        /// <summary>
        /// Sets the height of the object.
        /// </summary>
        [DomName("height")]
        [DomAccessor(Accessors.Setter)]
        public static void SetHeight(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Height, value);

        /// <summary>
        /// Gets the state of an IME.
        /// </summary>
        [DomName("imeMode")]
        [DomName("ime-mode")]
        [DomAccessor(Accessors.Getter)]
        public static String GetImeMode(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ImeMode);

        /// <summary>
        /// Sets the state of an IME.
        /// </summary>
        [DomName("imeMode")]
        [DomName("ime-mode")]
        [DomAccessor(Accessors.Setter)]
        public static void SetImeMode(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ImeMode, value);

        /// <summary>
        /// Gets a how flex items are aligned along the main axis of
        /// the flex container after any flexible lengths and auto margins are
        /// resolved.
        /// </summary>
        [DomName("justifyContent")]
        [DomName("justify-content")]
        [DomAccessor(Accessors.Getter)]
        public static String GetJustifyContent(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.JustifyContent);

        /// <summary>
        /// Sets a how flex items are aligned along the main axis of
        /// the flex container after any flexible lengths and auto margins are
        /// resolved.
        /// </summary>
        [DomName("justifyContent")]
        [DomName("justify-content")]
        [DomAccessor(Accessors.Setter)]
        public static void SetJustifyContent(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.JustifyContent, value);

        /// <summary>
        /// Gets the composite document grid properties that specify
        /// the layout of text characters.
        /// </summary>
        [DomName("layoutGrid")]
        [DomName("layout-grid")]
        [DomAccessor(Accessors.Getter)]
        public static String GetLayoutGrid(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.LayoutGrid);

        /// <summary>
        /// Sets the composite document grid properties that specify
        /// the layout of text characters.
        /// </summary>
        [DomName("layoutGrid")]
        [DomName("layout-grid")]
        [DomAccessor(Accessors.Setter)]
        public static void SetLayoutGrid(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.LayoutGrid, value);

        /// <summary>
        /// Gets the size of the character grid used for rendering
        /// the text content of an element.
        /// </summary>
        [DomName("layoutGridChar")]
        [DomName("layout-grid-char")]
        [DomAccessor(Accessors.Getter)]
        public static String GetLayoutGridChar(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.LayoutGridChar);

        /// <summary>
        /// Sets the size of the character grid used for rendering
        /// the text content of an element.
        /// </summary>
        [DomName("layoutGridChar")]
        [DomName("layout-grid-char")]
        [DomAccessor(Accessors.Setter)]
        public static void SetLayoutGridChar(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.LayoutGridChar, value);

        /// <summary>
        /// Gets the gridline value used for rendering the text content
        /// of an element.
        /// </summary>
        [DomName("layoutGridLine")]
        [DomName("layout-grid-line")]
        [DomAccessor(Accessors.Getter)]
        public static String GetLayoutGridLine(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.LayoutGridLine);

        /// <summary>
        /// Sets the gridline value used for rendering the text content
        /// of an element.
        /// </summary>
        [DomName("layoutGridLine")]
        [DomName("layout-grid-line")]
        [DomAccessor(Accessors.Setter)]
        public static void SetLayoutGridLine(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.LayoutGridLine, value);

        /// <summary>
        /// Gets whether the text layout grid uses two dimensions.
        /// </summary>
        [DomName("layoutGridMode")]
        [DomName("layout-grid-mode")]
        [DomAccessor(Accessors.Getter)]
        public static String GetLayoutGridMode(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.LayoutGridMode);

        /// <summary>
        /// Sets whether the text layout grid uses two dimensions.
        /// </summary>
        [DomName("layoutGridMode")]
        [DomName("layout-grid-mode")]
        [DomAccessor(Accessors.Setter)]
        public static void SetLayoutGridMode(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.LayoutGridMode, value);

        /// <summary>
        /// Gets the type of grid used for rendering the text content
        /// of an element.
        /// </summary>
        [DomName("layoutGridType")]
        [DomName("layout-grid-type")]
        [DomAccessor(Accessors.Getter)]
        public static String GetLayoutGridType(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.LayoutGridType);

        /// <summary>
        /// Sets the type of grid used for rendering the text content
        /// of an element.
        /// </summary>
        [DomName("layoutGridType")]
        [DomName("layout-grid-type")]
        [DomAccessor(Accessors.Setter)]
        public static void SetLayoutGridType(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.LayoutGridType, value);

        /// <summary>
        /// Gets the position of the object relative to the left edge
        /// of the next positioned object in the document hierarchy.
        /// </summary>
        [DomName("left")]
        [DomAccessor(Accessors.Getter)]
        public static String GetLeft(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Left);

        /// <summary>
        /// Sets the position of the object relative to the left edge
        /// of the next positioned object in the document hierarchy.
        /// </summary>
        [DomName("left")]
        [DomAccessor(Accessors.Setter)]
        public static void SetLeft(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Left, value);

        /// <summary>
        /// Gets the amount of additional space between letters in the
        /// object.
        /// </summary>
        [DomName("letterSpacing")]
        [DomName("letter-spacing")]
        [DomAccessor(Accessors.Getter)]
        public static String GetLetterSpacing(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.LetterSpacing);

        /// <summary>
        /// Sets the amount of additional space between letters in the
        /// object.
        /// </summary>
        [DomName("letterSpacing")]
        [DomName("letter-spacing")]
        [DomAccessor(Accessors.Setter)]
        public static void SetLetterSpacing(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.LetterSpacing, value);

        /// <summary>
        /// Gets the distance between lines in the object.
        /// </summary>
        [DomName("lineHeight")]
        [DomName("line-height")]
        [DomAccessor(Accessors.Getter)]
        public static String GetLineHeight(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.LineHeight);

        /// <summary>
        /// Sets the distance between lines in the object.
        /// </summary>
        [DomName("lineHeight")]
        [DomName("line-height")]
        [DomAccessor(Accessors.Setter)]
        public static void SetLineHeight(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.LineHeight, value);

        /// <summary>
        /// Gets up to three separate list-style properties of the
        /// object.
        /// </summary>
        [DomName("listStyle")]
        [DomName("list-style")]
        [DomAccessor(Accessors.Getter)]
        public static String GetListStyle(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ListStyle);

        /// <summary>
        /// Sets up to three separate list-style properties of the
        /// object.
        /// </summary>
        [DomName("listStyle")]
        [DomName("list-style")]
        [DomAccessor(Accessors.Setter)]
        public static void SetListStyle(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ListStyle, value);

        /// <summary>
        /// Gets a value that indicates which image to use as a
        /// list-item marker for the object.
        /// </summary>
        [DomName("listStyleImage")]
        [DomName("list-style-image")]
        [DomAccessor(Accessors.Getter)]
        public static String GetListStyleImage(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ListStyleImage);

        /// <summary>
        /// Sets a value that indicates which image to use as a
        /// list-item marker for the object.
        /// </summary>
        [DomName("listStyleImage")]
        [DomName("list-style-image")]
        [DomAccessor(Accessors.Setter)]
        public static void SetListStyleImage(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ListStyleImage, value);

        /// <summary>
        /// Gets a variable that indicates how the list-item marker is
        /// drawn relative to the content of the object.
        /// </summary>
        [DomName("listStylePosition")]
        [DomName("list-style-position")]
        [DomAccessor(Accessors.Getter)]
        public static String GetListStylePosition(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ListStylePosition);

        /// <summary>
        /// Sets a variable that indicates how the list-item marker is
        /// drawn relative to the content of the object.
        /// </summary>
        [DomName("listStylePosition")]
        [DomName("list-style-position")]
        [DomAccessor(Accessors.Setter)]
        public static void SetListStylePosition(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ListStylePosition, value);

        /// <summary>
        /// Gets the predefined type of the line-item marker for the
        /// object.
        /// </summary>
        [DomName("listStyleType")]
        [DomName("list-style-type")]
        [DomAccessor(Accessors.Getter)]
        public static String GetListStyleType(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ListStyleType);

        /// <summary>
        /// Sets the predefined type of the line-item marker for the
        /// object.
        /// </summary>
        [DomName("listStyleType")]
        [DomName("list-style-type")]
        [DomAccessor(Accessors.Setter)]
        public static void SetListStyleType(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ListStyleType, value);

        /// <summary>
        /// Gets the width of the top, right, bottom, and left margins
        /// of the object.
        /// </summary>
        [DomName("margin")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMargin(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Margin);

        /// <summary>
        /// Sets the width of the top, right, bottom, and left margins
        /// of the object.
        /// </summary>
        [DomName("margin")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMargin(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Margin, value);

        /// <summary>
        /// Gets the height of the bottom margin of the object.
        /// </summary>
        [DomName("marginBottom")]
        [DomName("margin-bottom")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMarginBottom(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MarginBottom);

        /// <summary>
        /// Sets the height of the bottom margin of the object.
        /// </summary>
        [DomName("marginBottom")]
        [DomName("margin-bottom")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMarginBottom(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MarginBottom, value);

        /// <summary>
        /// Gets the width of the left margin of the object.
        /// </summary>
        [DomName("marginLeft")]
        [DomName("margin-left")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMarginLeft(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MarginLeft);

        /// <summary>
        /// Sets the width of the left margin of the object.
        /// </summary>
        [DomName("marginLeft")]
        [DomName("margin-left")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMarginLeft(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MarginLeft, value);

        /// <summary>
        /// Gets the width of the right margin of the object.
        /// </summary>
        [DomName("marginRight")]
        [DomName("margin-right")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMarginRight(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MarginRight);

        /// <summary>
        /// Sets the width of the right margin of the object.
        /// </summary>
        [DomName("marginRight")]
        [DomName("margin-right")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMarginRight(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MarginRight, value);

        /// <summary>
        /// Gets the height of the top margin of the object.
        /// </summary>
        [DomName("marginTop")]
        [DomName("margin-top")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMarginTop(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MarginTop);

        /// <summary>
        /// Sets the height of the top margin of the object.
        /// </summary>
        [DomName("marginTop")]
        [DomName("margin-top")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMarginTop(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MarginTop, value);

        /// <summary>
        /// Gets a value that specifies the marker symbol that is
        /// used for all vertices on the given path element or basic shape.
        /// </summary>
        [DomName("marker")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMarker(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Marker);

        /// <summary>
        /// Sets a value that specifies the marker symbol that is
        /// used for all vertices on the given path element or basic shape.
        /// </summary>
        [DomName("marker")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMarker(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Marker, value);

        /// <summary>
        /// Gets a value that defines the arrowhead or polymarker that
        /// is drawn at the final vertex of a given path element or basic
        /// shape.
        /// </summary>
        [DomName("markerEnd")]
        [DomName("marker-end")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMarkerEnd(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MarkerEnd);

        /// <summary>
        /// Sets a value that defines the arrowhead or polymarker that
        /// is drawn at the final vertex of a given path element or basic
        /// shape.
        /// </summary>
        [DomName("markerEnd")]
        [DomName("marker-end")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMarkerEnd(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MarkerEnd, value);

        /// <summary>
        /// Gets a value that defines the arrowhead or polymarker that
        /// is drawn at every other vertex (that is, every vertex except the
        /// first and last) of a given path element or basic shape.
        /// </summary>
        [DomName("markerMid")]
        [DomName("marker-mid")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMarkerMid(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MarkerMid);

        /// <summary>
        /// Sets a value that defines the arrowhead or polymarker that
        /// is drawn at every other vertex (that is, every vertex except the
        /// first and last) of a given path element or basic shape.
        /// </summary>
        [DomName("markerMid")]
        [DomName("marker-mid")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMarkerMid(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MarkerMid, value);

        /// <summary>
        /// Gets a value that defines the arrowhead or polymarker that
        /// is drawn at the first vertex of a given path element or basic
        /// shape.
        /// </summary>
        [DomName("markerStart")]
        [DomName("marker-start")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMarkerStart(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MarkerStart);

        /// <summary>
        /// Sets a value that defines the arrowhead or polymarker that
        /// is drawn at the first vertex of a given path element or basic
        /// shape.
        /// </summary>
        [DomName("markerStart")]
        [DomName("marker-start")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMarkerStart(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MarkerStart, value);

        /// <summary>
        /// Gets a value that indicates a SVG mask.
        /// </summary>
        [DomName("mask")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMask(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Mask);

        /// <summary>
        /// Sets a value that indicates a SVG mask.
        /// </summary>
        [DomName("mask")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMask(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Mask, value);

        /// <summary>
        /// Gets the maximum height for an element.
        /// </summary>
        [DomName("maxHeight")]
        [DomName("max-height")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMaxHeight(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MaxHeight);

        /// <summary>
        /// Sets the maximum height for an element.
        /// </summary>
        [DomName("maxHeight")]
        [DomName("max-height")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMaxHeight(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MaxHeight, value);

        /// <summary>
        /// Gets the maximum width for an element.
        /// </summary>
        [DomName("maxWidth")]
        [DomName("max-width")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMaxWidth(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MaxWidth);

        /// <summary>
        /// Sets the maximum width for an element.
        /// </summary>
        [DomName("maxWidth")]
        [DomName("max-width")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMaxWidth(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MaxWidth, value);

        /// <summary>
        /// Gets the minimum height for an element.
        /// </summary>
        [DomName("minHeight")]
        [DomName("min-height")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMinHeight(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MinHeight);

        /// <summary>
        /// Sets the minimum height for an element.
        /// </summary>
        [DomName("minHeight")]
        [DomName("min-height")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMinHeight(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MinHeight, value);

        /// <summary>
        /// Gets the minimum width for an element.
        /// </summary>
        [DomName("minWidth")]
        [DomName("min-width")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMinWidth(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MinWidth);

        /// <summary>
        /// Sets the minimum width for an element.
        /// </summary>
        [DomName("minWidth")]
        [DomName("min-width")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMinWidth(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MinWidth, value);

        /// <summary>
        /// Gets a value that specifies object or group opacity in CSS
        /// or SVG.
        /// </summary>
        [DomName("opacity")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOpacity(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Opacity);

        /// <summary>
        /// Sets a value that specifies object or group opacity in CSS
        /// or SVG.
        /// </summary>
        [DomName("opacity")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOpacity(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Opacity, value);

        /// <summary>
        /// Gets the order, which property specifies the order used to
        /// lay out flex items in their flex container. Elements are laid out
        /// by ascending order of the order value. Elements with the same order
        /// value are laid out in the order they appear in the source code.
        /// </summary>
        [DomName("order")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOrder(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Order);

        /// <summary>
        /// Sets the order, which property specifies the order used to
        /// lay out flex items in their flex container. Elements are laid out
        /// by ascending order of the order value. Elements with the same order
        /// value are laid out in the order they appear in the source code.
        /// </summary>
        [DomName("order")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOrder(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Order, value);

        /// <summary>
        /// Gets the minimum number of lines of a paragraph that must
        /// appear at the bottom of a page.
        /// </summary>
        [DomName("orphans")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOrphans(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Orphans);

        /// <summary>
        /// Sets the minimum number of lines of a paragraph that must
        /// appear at the bottom of a page.
        /// </summary>
        [DomName("orphans")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOrphans(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Orphans, value);

        /// <summary>
        /// Gets the outline frame.
        /// </summary>
        [DomName("outline")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOutline(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Outline);

        /// <summary>
        /// Sets the outline frame.
        /// </summary>
        [DomName("outline")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOutline(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Outline, value);

        /// <summary>
        /// Gets the color of the outline frame.
        /// </summary>
        [DomName("outlineColor")]
        [DomName("outline-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOutlineColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.OutlineColor);

        /// <summary>
        /// Sets the color of the outline frame.
        /// </summary>
        [DomName("outlineColor")]
        [DomName("outline-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOutlineColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.OutlineColor, value);

        /// <summary>
        /// Gets the style of the outline frame.
        /// </summary>
        [DomName("outlineStyle")]
        [DomName("outline-style")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOutlineStyle(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.OutlineStyle);

        /// <summary>
        /// Sets the style of the outline frame.
        /// </summary>
        [DomName("outlineStyle")]
        [DomName("outline-style")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOutlineStyle(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.OutlineStyle, value);

        /// <summary>
        /// Gets the width of the outline frame.
        /// </summary>
        [DomName("outlineWidth")]
        [DomName("outline-width")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOutlineWidth(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.OutlineWidth);

        /// <summary>
        /// Sets the width of the outline frame.
        /// </summary>
        [DomName("outlineWidth")]
        [DomName("outline-width")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOutlineWidth(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.OutlineWidth, value);

        /// <summary>
        /// Gets a value indicating how to manage the content of the
        /// object when the content exceeds the height or width of the object.
        /// </summary>
        [DomName("overflow")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOverflow(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Overflow);

        /// <summary>
        /// Sets a value indicating how to manage the content of the
        /// object when the content exceeds the height or width of the object.
        /// </summary>
        [DomName("overflow")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOverflow(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Overflow, value);

        /// <summary>
        /// Gets how to manage the content of the object when the
        /// content exceeds the width of the object.
        /// </summary>
        [DomName("overflowX")]
        [DomName("overflow-x")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOverflowX(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.OverflowX);

        /// <summary>
        /// Sets how to manage the content of the object when the
        /// content exceeds the width of the object.
        /// </summary>
        [DomName("overflowX")]
        [DomName("overflow-x")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOverflowX(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.OverflowX, value);

        /// <summary>
        /// Gets how to manage the content of the object when the
        /// content exceeds the height of the object.
        /// </summary>
        [DomName("overflowY")]
        [DomName("overflow-y")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOverflowY(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.OverflowY);

        /// <summary>
        /// Sets how to manage the content of the object when the
        /// content exceeds the height of the object.
        /// </summary>
        [DomName("overflowY")]
        [DomName("overflow-y")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOverflowY(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.OverflowY, value);

        /// <summary>
        /// Gets the amount of space to insert between the object and
        /// its margin or, if there is a border, between the object and its
        /// border.
        /// </summary>
        [DomName("padding")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPadding(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Padding);

        /// <summary>
        /// Sets the amount of space to insert between the object and
        /// its margin or, if there is a border, between the object and its
        /// border.
        /// </summary>
        [DomName("padding")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPadding(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Padding, value);

        /// <summary>
        /// Gets the amount of space to insert between the bottom
        /// border of the object and the content.
        /// </summary>
        [DomName("paddingBottom")]
        [DomName("padding-bottom")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPaddingBottom(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.PaddingBottom);

        /// <summary>
        /// Sets the amount of space to insert between the bottom
        /// border of the object and the content.
        /// </summary>
        [DomName("paddingBottom")]
        [DomName("padding-bottom")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPaddingBottom(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.PaddingBottom, value);

        /// <summary>
        /// Gets the amount of space to insert between the left
        /// border of the object and the content.
        /// </summary>
        [DomName("paddingLeft")]
        [DomName("padding-left")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPaddingLeft(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.PaddingLeft);

        /// <summary>
        /// Sets the amount of space to insert between the left
        /// border of the object and the content.
        /// </summary>
        [DomName("paddingLeft")]
        [DomName("padding-left")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPaddingLeft(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.PaddingLeft, value);

        /// <summary>
        /// Gets the amount of space to insert between the right border
        /// of the object and the content.
        /// </summary>
        [DomName("paddingRight")]
        [DomName("padding-right")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPaddingRight(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.PaddingRight);

        /// <summary>
        /// Sets the amount of space to insert between the right border
        /// of the object and the content.
        /// </summary>
        [DomName("paddingRight")]
        [DomName("padding-right")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPaddingRight(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.PaddingRight, value);

        /// <summary>
        /// Gets the amount of space to insert between the top border
        /// of the object and the content.
        /// </summary>
        [DomName("paddingTop")]
        [DomName("padding-top")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPaddingTop(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.PaddingTop);

        /// <summary>
        /// Sets the amount of space to insert between the top border
        /// of the object and the content.
        /// </summary>
        [DomName("paddingTop")]
        [DomName("padding-top")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPaddingTop(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.PaddingTop, value);

        /// <summary>
        /// Gets a value indicating whether a page break occurs after
        /// the object.
        /// </summary>
        [DomName("pageBreakAfter")]
        [DomName("page-break-after")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPageBreakAfter(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.PageBreakAfter);

        /// <summary>
        /// Sets a value indicating whether a page break occurs after
        /// the object.
        /// </summary>
        [DomName("pageBreakAfter")]
        [DomName("page-break-after")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPageBreakAfter(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.PageBreakAfter, value);

        /// <summary>
        /// Gets a string indicating whether a page break occurs before
        /// the object.
        /// </summary>
        [DomName("pageBreakBefore")]
        [DomName("page-break-before")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPageBreakBefore(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.PageBreakBefore);

        /// <summary>
        /// Sets a string indicating whether a page break occurs before
        /// the object.
        /// </summary>
        [DomName("pageBreakBefore")]
        [DomName("page-break-before")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPageBreakBefore(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.PageBreakBefore, value);

        /// <summary>
        /// Gets a string indicating whether a page break is allowed to
        /// occur inside the object.
        /// </summary>
        [DomName("pageBreakInside")]
        [DomName("page-break-inside")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPageBreakInside(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.PageBreakInside);

        /// <summary>
        /// Sets a string indicating whether a page break is allowed to
        /// occur inside the object.
        /// </summary>
        [DomName("pageBreakInside")]
        [DomName("page-break-inside")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPageBreakInside(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.PageBreakInside, value);

        /// <summary>
        /// Gets a value that represents the perspective from which all
        /// child elements of the object are viewed.
        /// </summary>
        [DomName("perspective")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPerspective(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Perspective);

        /// <summary>
        /// Sets a value that represents the perspective from which all
        /// child elements of the object are viewed.
        /// </summary>
        [DomName("perspective")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPerspective(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Perspective, value);

        /// <summary>
        /// Gets one or two values that represent the origin (the
        /// vanishing point for the 3-D space) of an object with an perspective
        /// property declaration.
        /// </summary>
        [DomName("perspectiveOrigin")]
        [DomName("perspective-origin")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPerspectiveOrigin(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.PerspectiveOrigin);

        /// <summary>
        /// Sets one or two values that represent the origin (the
        /// vanishing point for the 3-D space) of an object with an perspective
        /// property declaration.
        /// </summary>
        [DomName("perspectiveOrigin")]
        [DomName("perspective-origin")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPerspectiveOrigin(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.PerspectiveOrigin, value);

        /// <summary>
        /// Gets a value that specifies under what circumstances a
        /// given graphics element can be the target element for a pointer
        /// event in SVG.
        /// </summary>
        [DomName("pointerEvents")]
        [DomName("pointer-events")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPointerEvents(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.PointerEvents);

        /// <summary>
        /// Sets a value that specifies under what circumstances a
        /// given graphics element can be the target element for a pointer
        /// event in SVG.
        /// </summary>
        [DomName("pointerEvents")]
        [DomName("pointer-events")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPointerEvents(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.PointerEvents, value);

        /// <summary>
        /// Gets the pairs of strings to be used as quotes in generated
        /// content.
        /// </summary>
        [DomName("quotes")]
        [DomAccessor(Accessors.Getter)]
        public static String GetQuotes(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Quotes);

        /// <summary>
        /// Sets the pairs of strings to be used as quotes in generated
        /// content.
        /// </summary>
        [DomName("quotes")]
        [DomAccessor(Accessors.Setter)]
        public static void SetQuotes(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Quotes, value);

        /// <summary>
        /// Gets the type of positioning used for the object.
        /// </summary>
        [DomName("position")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPosition(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Position);

        /// <summary>
        /// Sets the type of positioning used for the object.
        /// </summary>
        [DomName("position")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPosition(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Position, value);

        /// <summary>
        /// Gets the position of the object relative to the right edge
        /// of the next positioned object in the document hierarchy.
        /// </summary>
        [DomName("right")]
        [DomAccessor(Accessors.Getter)]
        public static String GetRight(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Right);

        /// <summary>
        /// Sets the position of the object relative to the right edge
        /// of the next positioned object in the document hierarchy.
        /// </summary>
        [DomName("right")]
        [DomAccessor(Accessors.Setter)]
        public static void SetRight(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Right, value);

        /// <summary>
        /// Gets a value that indicates how to align the ruby text
        /// content.
        /// </summary>
        [DomName("rubyAlign")]
        [DomName("ruby-align")]
        [DomAccessor(Accessors.Getter)]
        public static String GetRubyAlign(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.RubyAlign);

        /// <summary>
        /// Sets a value that indicates how to align the ruby text
        /// content.
        /// </summary>
        [DomName("rubyAlign")]
        [DomName("ruby-align")]
        [DomAccessor(Accessors.Setter)]
        public static void SetRubyAlign(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.RubyAlign, value);

        /// <summary>
        /// Gets a value that indicates whether, and on which side,
        /// ruby text is allowed to partially overhang any adjacent text in
        /// addition to its own base, when the ruby text is wider than the
        /// ruby base.
        /// </summary>
        [DomName("rubyOverhang")]
        [DomName("ruby-overhang")]
        [DomAccessor(Accessors.Getter)]
        public static String GetRubyOverhang(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.RubyOverhang);

        /// <summary>
        /// Sets a value that indicates whether, and on which side,
        /// ruby text is allowed to partially overhang any adjacent text in
        /// addition to its own base, when the ruby text is wider than the
        /// ruby base.
        /// </summary>
        [DomName("rubyOverhang")]
        [DomName("ruby-overhang")]
        [DomAccessor(Accessors.Setter)]
        public static void SetRubyOverhang(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.RubyOverhang, value);

        /// <summary>
        /// Gets a value that controls the position of the ruby text
        /// with respect to its base.
        /// </summary>
        [DomName("rubyPosition")]
        [DomName("ruby-position")]
        [DomAccessor(Accessors.Getter)]
        public static String GetRubyPosition(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.RubyPosition);

        /// <summary>
        /// Sets a value that controls the position of the ruby text
        /// with respect to its base.
        /// </summary>
        [DomName("rubyPosition")]
        [DomName("ruby-position")]
        [DomAccessor(Accessors.Setter)]
        public static void SetRubyPosition(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.RubyPosition, value);

        /// <summary>
        /// Gets the color of the top and left edges of the scroll
        /// box and scroll arrows of a scroll bar.
        /// </summary>
        [DomName("scrollbar3dLightColor")]
        [DomName("scrollbar3d-light-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollbar3dLightColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Scrollbar3dLightColor);

        /// <summary>
        /// Sets the color of the top and left edges of the scroll
        /// box and scroll arrows of a scroll bar.
        /// </summary>
        [DomName("scrollbar3dLightColor")]
        [DomName("scrollbar3d-light-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollbar3dLightColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Scrollbar3dLightColor, value);

        /// <summary>
        /// Gets the color of the arrow elements of a scroll arrow.
        /// </summary>
        [DomName("scrollbarArrowColor")]
        [DomName("scrollbar-arrow-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollbarArrowColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollbarArrowColor);

        /// <summary>
        /// Sets the color of the arrow elements of a scroll arrow.
        /// </summary>
        [DomName("scrollbarArrowColor")]
        [DomName("scrollbar-arrow-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollbarArrowColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollbarArrowColor, value);

        /// <summary>
        /// Gets the color of the gutter of a scroll bar.
        /// </summary>
        [DomName("scrollbarDarkShadowColor")]
        [DomName("scrollbar-dark-shadow-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollbarDarkShadowColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollbarDarkShadowColor);

        /// <summary>
        /// Sets the color of the gutter of a scroll bar.
        /// </summary>
        [DomName("scrollbarDarkShadowColor")]
        [DomName("scrollbar-dark-shadow-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollbarDarkShadowColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollbarDarkShadowColor, value);

        /// <summary>
        /// Gets the color of the scroll box and scroll arrows of a
        /// scroll bar.
        /// </summary>
        [DomName("scrollbarFaceColor")]
        [DomName("scrollbar-face-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollbarFaceColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollbarFaceColor);

        /// <summary>
        /// Sets the color of the scroll box and scroll arrows of a
        /// scroll bar.
        /// </summary>
        [DomName("scrollbarFaceColor")]
        [DomName("scrollbar-face-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollbarFaceColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollbarFaceColor, value);

        /// <summary>
        /// Gets the color of the top and left edges of the scroll box
        /// and scroll arrows of a scroll bar.
        /// </summary>
        [DomName("scrollbarHighlightColor")]
        [DomName("scrollbar-highlight-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollbarHighlightColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollbarHighlightColor);

        /// <summary>
        /// Sets the color of the top and left edges of the scroll box
        /// and scroll arrows of a scroll bar.
        /// </summary>
        [DomName("scrollbarHighlightColor")]
        [DomName("scrollbar-highlight-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollbarHighlightColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollbarHighlightColor, value);

        /// <summary>
        /// Gets the color of the bottom and right edges of the scroll
        /// box and scroll arrows of a scroll bar.
        /// </summary>
        [DomName("scrollbarShadowColor")]
        [DomName("scrollbar-shadow-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollbarShadowColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollbarShadowColor);

        /// <summary>
        /// Sets the color of the bottom and right edges of the scroll
        /// box and scroll arrows of a scroll bar.
        /// </summary>
        [DomName("scrollbarShadowColor")]
        [DomName("scrollbar-shadow-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollbarShadowColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollbarShadowColor, value);

        /// <summary>
        /// Gets the color of the track element of a scroll bar.
        /// </summary>
        [DomName("scrollbarTrackColor")]
        [DomName("scrollbar-track-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollbarTrackColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollbarTrackColor);

        /// <summary>
        /// Sets the color of the track element of a scroll bar.
        /// </summary>
        [DomName("scrollbarTrackColor")]
        [DomName("scrollbar-track-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollbarTrackColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollbarTrackColor, value);

        /// <summary>
        /// Gets a value that indicates the color to paint along the
        /// outline of a given graphical element.
        /// </summary>
        [DomName("stroke")]
        [DomAccessor(Accessors.Getter)]
        public static String GetStroke(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Stroke);

        /// <summary>
        /// Sets a value that indicates the color to paint along the
        /// outline of a given graphical element.
        /// </summary>
        [DomName("stroke")]
        [DomAccessor(Accessors.Setter)]
        public static void SetStroke(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Stroke, value);

        /// <summary>
        /// Gets one or more values that indicate the pattern of dashes
        /// and gaps used to stroke paths.
        /// </summary>
        [DomName("strokeDasharray")]
        [DomName("stroke-dasharray")]
        [DomAccessor(Accessors.Getter)]
        public static String GetStrokeDashArray(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.StrokeDasharray);

        /// <summary>
        /// Sets one or more values that indicate the pattern of dashes
        /// and gaps used to stroke paths.
        /// </summary>
        [DomName("strokeDasharray")]
        [DomName("stroke-dasharray")]
        [DomAccessor(Accessors.Setter)]
        public static void SetStrokeDashArray(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.StrokeDasharray, value);

        /// <summary>
        /// Gets a value that specifies the distance into the dash
        /// pattern to start the dash.
        /// </summary>
        [DomName("strokeDashoffset")]
        [DomName("stroke-dashoffset")]
        [DomAccessor(Accessors.Getter)]
        public static String GetStrokeDashOffset(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.StrokeDashoffset);

        /// <summary>
        /// Sets a value that specifies the distance into the dash
        /// pattern to start the dash.
        /// </summary>
        [DomName("strokeDashoffset")]
        [DomName("stroke-dashoffset")]
        [DomAccessor(Accessors.Setter)]
        public static void SetStrokeDashOffset(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.StrokeDashoffset, value);

        /// <summary>
        /// Gets a value that specifies the shape to be used at the end
        /// of open subpaths when they are stroked.
        /// </summary>
        [DomName("strokeLinecap")]
        [DomName("stroke-linecap")]
        [DomAccessor(Accessors.Getter)]
        public static String GetStrokeLineCap(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.StrokeLinecap);

        /// <summary>
        /// Sets a value that specifies the shape to be used at the end
        /// of open subpaths when they are stroked.
        /// </summary>
        [DomName("strokeLinecap")]
        [DomName("stroke-linecap")]
        [DomAccessor(Accessors.Setter)]
        public static void SetStrokeLineCap(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.StrokeLinecap, value);

        /// <summary>
        /// Gets a value that specifies the shape to be used at the
        /// corners of paths or basic shapes when they are stroked.
        /// </summary>
        [DomName("strokeLinejoin")]
        [DomName("stroke-linejoin")]
        [DomAccessor(Accessors.Getter)]
        public static String GetStrokeLineJoin(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.StrokeLinejoin);

        /// <summary>
        /// Sets a value that specifies the shape to be used at the
        /// corners of paths or basic shapes when they are stroked.
        /// </summary>
        [DomName("strokeLinejoin")]
        [DomName("stroke-linejoin")]
        [DomAccessor(Accessors.Setter)]
        public static void SetStrokeLineJoin(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.StrokeLinejoin, value);

        /// <summary>
        /// Gets a value that indicates the limit on the ratio of the
        /// length of miter joins (as specified in the StrokeLinejoin 
        /// property).
        /// </summary>
        [DomName("strokeMiterlimit")]
        [DomName("stroke-miterlimit")]
        [DomAccessor(Accessors.Getter)]
        public static String GetStrokeMiterLimit(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.StrokeMiterlimit);

        /// <summary>
        /// Sets a value that indicates the limit on the ratio of the
        /// length of miter joins (as specified in the StrokeLinejoin 
        /// property).
        /// </summary>
        [DomName("strokeMiterlimit")]
        [DomName("stroke-miterlimit")]
        [DomAccessor(Accessors.Setter)]
        public static void SetStrokeMiterLimit(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.StrokeMiterlimit, value);

        /// <summary>
        /// Gets a value that specifies the opacity of the painting 
        /// operation that is used to stroke the current object.
        /// </summary>
        [DomName("strokeOpacity")]
        [DomName("stroke-opacity")]
        [DomAccessor(Accessors.Getter)]
        public static String GetStrokeOpacity(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.StrokeOpacity);

        /// <summary>
        /// Sets a value that specifies the opacity of the painting 
        /// operation that is used to stroke the current object.
        /// </summary>
        [DomName("strokeOpacity")]
        [DomName("stroke-opacity")]
        [DomAccessor(Accessors.Setter)]
        public static void SetStrokeOpacity(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.StrokeOpacity, value);

        /// <summary>
        /// Gets a value that specifies the width of the stroke on the
        /// current object.
        /// </summary>
        [DomName("strokeWidth")]
        [DomName("stroke-width")]
        [DomAccessor(Accessors.Getter)]
        public static String GetStrokeWidth(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.StrokeWidth);

        /// <summary>
        /// Sets a value that specifies the width of the stroke on the
        /// current object.
        /// </summary>
        [DomName("strokeWidth")]
        [DomName("stroke-width")]
        [DomAccessor(Accessors.Setter)]
        public static void SetStrokeWidth(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.StrokeWidth, value);

        /// <summary>
        /// Gets a string that indicates whether the table layout is
        /// fixed.
        /// </summary>
        [DomName("tableLayout")]
        [DomName("table-layout")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTableLayout(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TableLayout);

        /// <summary>
        /// Sets a string that indicates whether the table layout is
        /// fixed.
        /// </summary>
        [DomName("tableLayout")]
        [DomName("table-layout")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTableLayout(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TableLayout, value);

        /// <summary>
        /// Gets whether the text in the object is left-aligned,
        /// right-aligned, centered, or justified.
        /// </summary>
        [DomName("textAlign")]
        [DomName("text-align")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextAlign(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TextAlign);

        /// <summary>
        /// Sets whether the text in the object is left-aligned,
        /// right-aligned, centered, or justified.
        /// </summary>
        [DomName("textAlign")]
        [DomName("text-align")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextAlign(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TextAlign, value);


        /// <summary>
        /// Gets a value that indicates how to align the last line or
        /// only line of text in the specified object.
        /// </summary>
        [DomName("textAlignLast")]
        [DomName("text-align-last")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextAlignLast(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TextAlignLast);


        /// <summary>
        /// Sets a value that indicates how to align the last line or
        /// only line of text in the specified object.
        /// </summary>
        [DomName("textAlignLast")]
        [DomName("text-align-last")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextAlignLast(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TextAlignLast, value);

        /// <summary>
        /// Gets aligns a string of text relative to the specified point.
        /// </summary>
        [DomName("textAnchor")]
        [DomName("text-anchor")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextAnchor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TextAnchor);

        /// <summary>
        /// Sets aligns a string of text relative to the specified point.
        /// </summary>
        [DomName("textAnchor")]
        [DomName("text-anchor")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextAnchor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TextAnchor, value);

        /// <summary>
        /// Gets the autospacing and narrow space width adjustment of
        /// text.
        /// </summary>
        [DomName("textAutospace")]
        [DomName("text-autospace")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextAutospace(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TextAutospace);

        /// <summary>
        /// Sets the autospacing and narrow space width adjustment of
        /// text.
        /// </summary>
        [DomName("textAutospace")]
        [DomName("text-autospace")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextAutospace(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TextAutospace, value);

        /// <summary>
        /// Gets a value that indicates whether the text in the object
        /// has blink, line-through, overline, or underline decorations.
        /// </summary>
        [DomName("textDecoration")]
        [DomName("text-decoration")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextDecoration(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TextDecoration);

        /// <summary>
        /// Sets a value that indicates whether the text in the object
        /// has blink, line-through, overline, or underline decorations.
        /// </summary>
        [DomName("textDecoration")]
        [DomName("text-decoration")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextDecoration(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TextDecoration, value);

        /// <summary>
        /// Gets a value that indicates the style of the text decoration.
        /// </summary>
        [DomName("textDecorationStyle")]
        [DomName("text-decoration-style")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextDecorationStyle(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TextDecorationStyle);

        /// <summary>
        /// Sets a value that indicates the style of the text decoration.
        /// </summary>
        [DomName("textDecorationStyle")]
        [DomName("text-decoration-style")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextDecorationStyle(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TextDecorationStyle, value);

        /// <summary>
        /// Gets a value that indicates the line of the text decoration.
        /// </summary>
        [DomName("textDecorationLine")]
        [DomName("text-decoration-line")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextDecorationLine(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TextDecorationLine);

        /// <summary>
        /// Sets a value that indicates the line of the text decoration.
        /// </summary>
        [DomName("textDecorationLine")]
        [DomName("text-decoration-line")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextDecorationLine(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TextDecorationLine, value);

        /// <summary>
        /// Gets a value that indicates the color of the text decoration.
        /// </summary>
        [DomName("textDecorationColor")]
        [DomName("text-decoration-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextDecorationColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TextDecorationColor);

        /// <summary>
        /// Sets a value that indicates the color of the text decoration.
        /// </summary>
        [DomName("textDecorationColor")]
        [DomName("text-decoration-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextDecorationColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TextDecorationColor, value);

        /// <summary>
        /// Gets the indentation of the first line of text in the
        /// object.
        /// </summary>
        [DomName("textIndent")]
        [DomName("text-indent")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextIndent(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TextIndent);

        /// <summary>
        /// Sets the indentation of the first line of text in the
        /// object.
        /// </summary>
        [DomName("textIndent")]
        [DomName("text-indent")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextIndent(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TextIndent, value);

        /// <summary>
        /// Gets the type of alignment used to justify text in the
        /// object.
        /// </summary>
        [DomName("textJustify")]
        [DomName("text-justify")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextJustify(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TextJustify);

        /// <summary>
        /// Sets the type of alignment used to justify text in the
        /// object.
        /// </summary>
        [DomName("textJustify")]
        [DomName("text-justify")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextJustify(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TextJustify, value);

        /// <summary>
        /// Gets a value that indicates whether to render ellipses
        /// (...) to indicate text overflow.
        /// </summary>
        [DomName("textOverflow")]
        [DomName("text-overflow")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextOverflow(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TextOverflow);

        /// <summary>
        /// Sets a value that indicates whether to render ellipses
        /// (...) to indicate text overflow.
        /// </summary>
        [DomName("textOverflow")]
        [DomName("text-overflow")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextOverflow(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TextOverflow, value);

        /// <summary>
        /// Gets a comma-separated list of shadows that attaches one or
        /// more drop shadows to the specified text.
        /// </summary>
        [DomName("textShadow")]
        [DomName("text-shadow")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextShadow(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TextShadow);

        /// <summary>
        /// Sets a comma-separated list of shadows that attaches one or
        /// more drop shadows to the specified text.
        /// </summary>
        [DomName("textShadow")]
        [DomName("text-shadow")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextShadow(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TextShadow, value);

        /// <summary>
        /// Gets the rendering of the text in the object.
        /// </summary>
        [DomName("textTransform")]
        [DomName("text-transform")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextTransform(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TextTransform);

        /// <summary>
        /// Sets the rendering of the text in the object.
        /// </summary>
        [DomName("textTransform")]
        [DomName("text-transform")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextTransform(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TextTransform, value);

        /// <summary>
        /// Gets the position of the underline decoration that is set
        /// through the text-decoration property of the object.
        /// </summary>
        [DomName("textUnderlinePosition")]
        [DomName("text-underline-position")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextUnderlinePosition(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TextUnderlinePosition);

        /// <summary>
        /// Sets the position of the underline decoration that is set
        /// through the text-decoration property of the object.
        /// </summary>
        [DomName("textUnderlinePosition")]
        [DomName("text-underline-position")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextUnderlinePosition(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TextUnderlinePosition, value);

        /// <summary>
        /// Gets the position of the object relative to the top of the
        /// next positioned object in the document hierarchy.
        /// </summary>
        [DomName("top")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTop(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Top);

        /// <summary>
        /// Sets the position of the object relative to the top of the
        /// next positioned object in the document hierarchy.
        /// </summary>
        [DomName("top")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTop(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Top, value);

        /// <summary>
        /// Gets a list of one or more transform functions that specify
        /// how to translate, rotate, or scale an element in 2-D or 3-D space.
        /// </summary>
        [DomName("transform")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTransform(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Transform);

        /// <summary>
        /// Sets a list of one or more transform functions that specify
        /// how to translate, rotate, or scale an element in 2-D or 3-D space.
        /// </summary>
        [DomName("transform")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTransform(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Transform, value);

        /// <summary>
        /// Gets one or two values that establish the origin of 
        /// transformation for an element.
        /// </summary>
        [DomName("transformOrigin")]
        [DomName("transform-origin")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTransformOrigin(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TransformOrigin);

        /// <summary>
        /// Sets one or two values that establish the origin of 
        /// transformation for an element.
        /// </summary>
        [DomName("transformOrigin")]
        [DomName("transform-origin")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTransformOrigin(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TransformOrigin, value);

        /// <summary>
        /// Gets a value that specifies how child elements of the
        /// object are rendered in 3-D space.
        /// </summary>
        [DomName("transformStyle")]
        [DomName("transform-style")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTransformStyle(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TransformStyle);

        /// <summary>
        /// Sets a value that specifies how child elements of the
        /// object are rendered in 3-D space.
        /// </summary>
        [DomName("transformStyle")]
        [DomName("transform-style")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTransformStyle(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TransformStyle, value);

        /// <summary>
        /// Gets one or more shorthand values that specify the 
        /// transition properties for a set of corresponding object properties 
        /// identified in the transition-property property.
        /// </summary>
        [DomName("transition")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTransition(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Transition);

        /// <summary>
        /// Sets one or more shorthand values that specify the 
        /// transition properties for a set of corresponding object properties 
        /// identified in the transition-property property.
        /// </summary>
        [DomName("transition")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTransition(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Transition, value);

        /// <summary>
        /// Gets one or more values that specify the offset within a
        /// transition (the amount of time from the start of a transition) 
        /// before the transition is displayed  for a set of corresponding 
        /// object properties identified in the transition property.
        /// </summary>
        [DomName("transitionDelay")]
        [DomName("transition-delay")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTransitionDelay(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TransitionDelay);

        /// <summary>
        /// Sets one or more values that specify the offset within a
        /// transition (the amount of time from the start of a transition) 
        /// before the transition is displayed  for a set of corresponding 
        /// object properties identified in the transition property.
        /// </summary>
        [DomName("transitionDelay")]
        [DomName("transition-delay")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTransitionDelay(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TransitionDelay, value);

        /// <summary>
        /// Gets one or more values that specify the durations of 
        /// transitions on a set of corresponding object properties identified 
        /// in the transition-property property.
        /// </summary>
        [DomName("transitionDuration")]
        [DomName("transition-duration")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTransitionDuration(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TransitionDuration);

        /// <summary>
        /// Sets one or more values that specify the durations of 
        /// transitions on a set of corresponding object properties identified 
        /// in the transition-property property.
        /// </summary>
        [DomName("transitionDuration")]
        [DomName("transition-duration")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTransitionDuration(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TransitionDuration, value);

        /// <summary>
        /// Gets a value that identifies the CSS property name or names
        /// to which the transition effect (defined by the transition-duration, 
        /// transition-timing-function, and transition-delay properties) is 
        /// applied when a new property value is specified.
        /// </summary>
        [DomName("transitionProperty")]
        [DomName("transition-property")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTransitionProperty(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TransitionProperty);

        /// <summary>
        /// Sets a value that identifies the CSS property name or names
        /// to which the transition effect (defined by the transition-duration, 
        /// transition-timing-function, and transition-delay properties) is 
        /// applied when a new property value is specified.
        /// </summary>
        [DomName("transitionProperty")]
        [DomName("transition-property")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTransitionProperty(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TransitionProperty, value);

        /// <summary>
        /// Gets one or more values that specify the intermediate 
        /// property values to be used during a transition on a set of 
        /// corresponding object properties identified in the 
        /// transition-property property.
        /// </summary>
        [DomName("transitionTimingFunction")]
        [DomName("transition-timing-function")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTransitionTimingFunction(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TransitionTimingFunction);

        /// <summary>
        /// Sets one or more values that specify the intermediate 
        /// property values to be used during a transition on a set of 
        /// corresponding object properties identified in the 
        /// transition-property property.
        /// </summary>
        [DomName("transitionTimingFunction")]
        [DomName("transition-timing-function")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTransitionTimingFunction(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TransitionTimingFunction, value);

        /// <summary>
        /// Gets the level of embedding with respect to the 
        /// bidirectional algorithm.
        /// </summary>
        [DomName("unicodeBidi")]
        [DomName("unicode-bidi")]
        [DomAccessor(Accessors.Getter)]
        public static String GetUnicodeBidi(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.UnicodeBidi);

        /// <summary>
        /// Sets the level of embedding with respect to the 
        /// bidirectional algorithm.
        /// </summary>
        [DomName("unicodeBidi")]
        [DomName("unicode-bidi")]
        [DomAccessor(Accessors.Setter)]
        public static void SetUnicodeBidi(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.UnicodeBidi, value);

        /// <summary>
        /// Gets the vertical alignment of the object.
        /// </summary>
        [DomName("verticalAlign")]
        [DomName("vertical-align")]
        [DomAccessor(Accessors.Getter)]
        public static String GetVerticalAlign(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.VerticalAlign);

        /// <summary>
        /// Sets the vertical alignment of the object.
        /// </summary>
        [DomName("verticalAlign")]
        [DomName("vertical-align")]
        [DomAccessor(Accessors.Setter)]
        public static void SetVerticalAlign(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.VerticalAlign, value);

        /// <summary>
        /// Gets whether the content of the object is displayed.
        /// </summary>
        [DomName("visibility")]
        [DomAccessor(Accessors.Getter)]
        public static String GetVisibility(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Visibility);

        /// <summary>
        /// Sets whether the content of the object is displayed.
        /// </summary>
        [DomName("visibility")]
        [DomAccessor(Accessors.Setter)]
        public static void SetVisibility(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Visibility, value);

        /// <summary>
        /// Gets a value that indicates whether lines are automatically
        /// broken inside the object.
        /// </summary>
        [DomName("whiteSpace")]
        [DomName("white-space")]
        [DomAccessor(Accessors.Getter)]
        public static String GetWhiteSpace(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.WhiteSpace);

        /// <summary>
        /// Sets a value that indicates whether lines are automatically
        /// broken inside the object.
        /// </summary>
        [DomName("whiteSpace")]
        [DomName("white-space")]
        [DomAccessor(Accessors.Setter)]
        public static void SetWhiteSpace(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.WhiteSpace, value);

        /// <summary>
        /// Gets the minimum number of lines of a paragraph that must
        /// appear at the top of a document.
        /// </summary>
        [DomName("widows")]
        [DomAccessor(Accessors.Getter)]
        public static String GetWidows(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Widows);

        /// <summary>
        /// Sets the minimum number of lines of a paragraph that must
        /// appear at the top of a document.
        /// </summary>
        [DomName("widows")]
        [DomAccessor(Accessors.Setter)]
        public static void SetWidows(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Widows, value);

        /// <summary>
        /// Gets the width of the object.
        /// </summary>
        [DomName("width")]
        [DomAccessor(Accessors.Getter)]
        public static String GetWidth(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Width);

        /// <summary>
        /// Sets the width of the object.
        /// </summary>
        [DomName("width")]
        [DomAccessor(Accessors.Setter)]
        public static void SetWidth(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Width, value);

        /// <summary>
        /// Gets line-breaking behavior within words, particularly 
        /// where multiple languages appear in the object.
        /// </summary>
        [DomName("wordBreak")]
        [DomName("word-break")]
        [DomAccessor(Accessors.Getter)]
        public static String GetWordBreak(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.WordBreak);

        /// <summary>
        /// Sets line-breaking behavior within words, particularly 
        /// where multiple languages appear in the object.
        /// </summary>
        [DomName("wordBreak")]
        [DomName("word-break")]
        [DomAccessor(Accessors.Setter)]
        public static void SetWordBreak(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.WordBreak, value);

        /// <summary>
        /// Gets the amount of additional space between words in the 
        /// object.
        /// </summary>
        [DomName("wordSpacing")]
        [DomName("word-spacing")]
        [DomAccessor(Accessors.Getter)]
        public static String GetWordSpacing(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.WordSpacing);

        /// <summary>
        /// Sets the amount of additional space between words in the 
        /// object.
        /// </summary>
        [DomName("wordSpacing")]
        [DomName("word-spacing")]
        [DomAccessor(Accessors.Setter)]
        public static void SetWordSpacing(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.WordSpacing, value);

        /// <summary>
        /// Gets whether to break words when the content exceeds the
        /// boundaries of its container.
        /// </summary>
        [DomName("wordWrap")]
        [DomName("word-wrap")]
        [DomAccessor(Accessors.Getter)]
        public static String GetWordWrap(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.WordWrap);

        /// <summary>
        /// Sets whether to break words when the content exceeds the
        /// boundaries of its container.
        /// </summary>
        [DomName("wordWrap")]
        [DomName("word-wrap")]
        [DomAccessor(Accessors.Setter)]
        public static void SetWordWrap(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.WordWrap, value);
        
        /// <summary>
        /// Gets the overflow-wrap value.
        /// </summary>
        [DomName("overflowWrap")]
        [DomName("overflow-wrap")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOverflowWrap(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.OverflowWrap);

        /// <summary>
        /// Sets the overflow-wrap value.
        /// </summary>
        [DomName("overflowWrap")]
        [DomName("overflow-wrap")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOverflowWrap(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.OverflowWrap, value);

        /// <summary>
        /// Gets the direction and flow of the content in the object.
        /// </summary>
        [DomName("writingMode")]
        [DomName("writing-mode")]
        [DomAccessor(Accessors.Getter)]
        public static String GetWritingMode(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.WritingMode);

        /// <summary>
        /// Sets the direction and flow of the content in the object.
        /// </summary>
        [DomName("writingMode")]
        [DomName("writing-mode")]
        [DomAccessor(Accessors.Setter)]
        public static void SetWritingMode(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.WritingMode, value);

        /// <summary>
        /// Gets the stacking order of positioned objects.
        /// </summary>
        [DomName("zIndex")]
        [DomName("z-index")]
        [DomAccessor(Accessors.Getter)]
        public static String GetZIndex(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ZIndex);

        /// <summary>
        /// Sets the stacking order of positioned objects.
        /// </summary>
        [DomName("zIndex")]
        [DomName("z-index")]
        [DomAccessor(Accessors.Setter)]
        public static void SetZIndex(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ZIndex, value);

        /// <summary>
        /// Gets the magnification scale of the object.
        /// </summary>
        [DomName("zoom")]
        [DomAccessor(Accessors.Getter)]
        public static String GetZoom(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Zoom);

        /// <summary>
        /// Sets the magnification scale of the object.
        /// </summary>
        [DomName("zoom")]
        [DomAccessor(Accessors.Setter)]
        public static void SetZoom(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Zoom, value);

        /// <summary>
        /// Gets the value of aspect-ratio.
        /// </summary>
        [DomName("aspectRatio")]
        [DomName("aspect-ratio")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAspectRatio(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.AspectRatio);

        /// <summary>
        /// Sets the value of aspect-ratio.
        /// </summary>
        [DomName("aspectRatio")]
        [DomName("aspect-ratio")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAspectRatio(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.AspectRatio, value);

        /// <summary>
        /// Gets the value of animation-composition.
        /// </summary>
        [DomName("animationComposition")]
        [DomName("animation-composition")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAnimationComposition(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.AnimationComposition);

        /// <summary>
        /// Sets the value of animation-composition.
        /// </summary>
        [DomName("animationComposition")]
        [DomName("animation-composition")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAnimationComposition(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.AnimationComposition, value);

        /// <summary>
        /// Gets the value of accent-color.
        /// </summary>
        [DomName("accentColor")]
        [DomName("accent-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAccentColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.AccentColor);

        /// <summary>
        /// Sets the value of accent-color.
        /// </summary>
        [DomName("accentColor")]
        [DomName("accent-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAccentColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.AccentColor, value);

        /// <summary>
        /// Gets the value of appearance.
        /// </summary>
        [DomName("appearance")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAppearance(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Appearance);

        /// <summary>
        /// Sets the value of appearance.
        /// </summary>
        [DomName("appearance")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAppearance(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Appearance, value);

        /// <summary>
        /// Gets the value of background-blend-mode.
        /// </summary>
        [DomName("backgroundBlendMode")]
        [DomName("background-blend-mode")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackgroundBlendMode(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BackgroundBlendMode);

        /// <summary>
        /// Sets the value of background-blend-mode.
        /// </summary>
        [DomName("backgroundBlendMode")]
        [DomName("background-blend-mode")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackgroundBlendMode(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BackgroundBlendMode, value);

        /// <summary>
        /// Gets the value of backdrop-filter.
        /// </summary>
        [DomName("backdropFilter")]
        [DomName("backdrop-filter")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackdropFilter(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BackdropFilter);

        /// <summary>
        /// Sets the value of backdrop-filter.
        /// </summary>
        [DomName("backdropFilter")]
        [DomName("backdrop-filter")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackdropFilter(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BackdropFilter, value);

        /// <summary>
        /// Gets the value of box-decoration-break.
        /// </summary>
        [DomName("boxDecorationBreak")]
        [DomName("box-decoration-break")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBoxDecorationBreak(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BoxDecorationBreak);

        /// <summary>
        /// Sets the value of box-decoration-break.
        /// </summary>
        [DomName("boxDecorationBreak")]
        [DomName("box-decoration-break")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBoxDecorationBreak(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BoxDecorationBreak, value);

        /// <summary>
        /// Gets the value of border-block.
        /// </summary>
        [DomName("borderBlock")]
        [DomName("border-block")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderBlock(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderBlock);

        /// <summary>
        /// Sets the value of border-block.
        /// </summary>
        [DomName("borderBlock")]
        [DomName("border-block")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderBlock(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderBlock, value);

        /// <summary>
        /// Gets the value of border-block-start.
        /// </summary>
        [DomName("borderBlockStart")]
        [DomName("border-block-start")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderBlockStart(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderBlockStart);

        /// <summary>
        /// Sets the value of border-block-start.
        /// </summary>
        [DomName("borderBlockStart")]
        [DomName("border-block-start")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderBlockStart(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderBlockStart, value);

        /// <summary>
        /// Gets the value of border-block-end.
        /// </summary>
        [DomName("borderBlockEnd")]
        [DomName("border-block-end")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderBlockEnd(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderBlockEnd);

        /// <summary>
        /// Sets the value of border-block-end.
        /// </summary>
        [DomName("borderBlockEnd")]
        [DomName("border-block-end")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderBlockEnd(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderBlockEnd, value);

        /// <summary>
        /// Gets the value of border-block-color.
        /// </summary>
        [DomName("borderBlockColor")]
        [DomName("border-block-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderBlockColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderBlockColor);

        /// <summary>
        /// Sets the value of border-block-color.
        /// </summary>
        [DomName("borderBlockColor")]
        [DomName("border-block-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderBlockColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderBlockColor, value);

        /// <summary>
        /// Gets the value of border-block-style.
        /// </summary>
        [DomName("borderBlockStyle")]
        [DomName("border-block-style")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderBlockStyle(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderBlockStyle);

        /// <summary>
        /// Sets the value of border-block-style.
        /// </summary>
        [DomName("borderBlockStyle")]
        [DomName("border-block-style")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderBlockStyle(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderBlockStyle, value);

        /// <summary>
        /// Gets the value of border-block-width.
        /// </summary>
        [DomName("borderBlockWidth")]
        [DomName("border-block-width")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderBlockWidth(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderBlockWidth);

        /// <summary>
        /// Sets the value of border-block-width.
        /// </summary>
        [DomName("borderBlockWidth")]
        [DomName("border-block-width")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderBlockWidth(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderBlockWidth, value);

        /// <summary>
        /// Gets the value of border-block-start-color.
        /// </summary>
        [DomName("borderBlockStartColor")]
        [DomName("border-block-start-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderBlockStartColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderBlockStartColor);

        /// <summary>
        /// Sets the value of border-block-start-color.
        /// </summary>
        [DomName("borderBlockStartColor")]
        [DomName("border-block-start-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderBlockStartColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderBlockStartColor, value);

        /// <summary>
        /// Gets the value of border-block-start-style.
        /// </summary>
        [DomName("borderBlockStartStyle")]
        [DomName("border-block-start-style")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderBlockStartStyle(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderBlockStartStyle);

        /// <summary>
        /// Sets the value of border-block-start-style.
        /// </summary>
        [DomName("borderBlockStartStyle")]
        [DomName("border-block-start-style")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderBlockStartStyle(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderBlockStartStyle, value);

        /// <summary>
        /// Gets the value of border-block-start-width.
        /// </summary>
        [DomName("borderBlockStartWidth")]
        [DomName("border-block-start-width")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderBlockStartWidth(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderBlockStartWidth);

        /// <summary>
        /// Sets the value of border-block-start-width.
        /// </summary>
        [DomName("borderBlockStartWidth")]
        [DomName("border-block-start-width")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderBlockStartWidth(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderBlockStartWidth, value);

        /// <summary>
        /// Gets the value of border-block-end-color.
        /// </summary>
        [DomName("borderBlockEndColor")]
        [DomName("border-block-end-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderBlockEndColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderBlockEndColor);

        /// <summary>
        /// Sets the value of border-block-end-color.
        /// </summary>
        [DomName("borderBlockEndColor")]
        [DomName("border-block-end-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderBlockEndColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderBlockEndColor, value);

        /// <summary>
        /// Gets the value of border-block-end-style.
        /// </summary>
        [DomName("borderBlockEndStyle")]
        [DomName("border-block-end-style")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderBlockEndStyle(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderBlockEndStyle);

        /// <summary>
        /// Sets the value of border-block-end-style.
        /// </summary>
        [DomName("borderBlockEndStyle")]
        [DomName("border-block-end-style")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderBlockEndStyle(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderBlockEndStyle, value);

        /// <summary>
        /// Gets the value of border-block-end-width.
        /// </summary>
        [DomName("borderBlockEndWidth")]
        [DomName("border-block-end-width")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderBlockEndWidth(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderBlockEndWidth);

        /// <summary>
        /// Sets the value of border-block-end-width.
        /// </summary>
        [DomName("borderBlockEndWidth")]
        [DomName("border-block-end-width")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderBlockEndWidth(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderBlockEndWidth, value);

        /// <summary>
        /// Gets the value of border-inline.
        /// </summary>
        [DomName("borderInline")]
        [DomName("border-inline")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderInline(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderInline);

        /// <summary>
        /// Sets the value of border-inline.
        /// </summary>
        [DomName("borderInline")]
        [DomName("border-inline")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderInline(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderInline, value);

        /// <summary>
        /// Gets the value of border-inline-start.
        /// </summary>
        [DomName("borderInlineStart")]
        [DomName("border-inline-start")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderInlineStart(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderInlineStart);

        /// <summary>
        /// Sets the value of border-inline-start.
        /// </summary>
        [DomName("borderInlineStart")]
        [DomName("border-inline-start")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderInlineStart(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderInlineStart, value);

        /// <summary>
        /// Gets the value of border-inline-end.
        /// </summary>
        [DomName("borderInlineEnd")]
        [DomName("border-inline-end")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderInlineEnd(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderInlineEnd);

        /// <summary>
        /// Sets the value of border-inline-end.
        /// </summary>
        [DomName("borderInlineEnd")]
        [DomName("border-inline-end")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderInlineEnd(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderInlineEnd, value);

        /// <summary>
        /// Gets the value of border-inline-color.
        /// </summary>
        [DomName("borderInlineColor")]
        [DomName("border-inline-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderInlineColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderInlineColor);

        /// <summary>
        /// Sets the value of border-inline-color.
        /// </summary>
        [DomName("borderInlineColor")]
        [DomName("border-inline-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderInlineColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderInlineColor, value);

        /// <summary>
        /// Gets the value of border-inline-style.
        /// </summary>
        [DomName("borderInlineStyle")]
        [DomName("border-inline-style")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderInlineStyle(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderInlineStyle);

        /// <summary>
        /// Sets the value of border-inline-style.
        /// </summary>
        [DomName("borderInlineStyle")]
        [DomName("border-inline-style")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderInlineStyle(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderInlineStyle, value);

        /// <summary>
        /// Gets the value of border-inline-width.
        /// </summary>
        [DomName("borderInlineWidth")]
        [DomName("border-inline-width")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderInlineWidth(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderInlineWidth);

        /// <summary>
        /// Sets the value of border-inline-width.
        /// </summary>
        [DomName("borderInlineWidth")]
        [DomName("border-inline-width")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderInlineWidth(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderInlineWidth, value);

        /// <summary>
        /// Gets the value of border-inline-start-color.
        /// </summary>
        [DomName("borderInlineStartColor")]
        [DomName("border-inline-start-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderInlineStartColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderInlineStartColor);

        /// <summary>
        /// Sets the value of border-inline-start-color.
        /// </summary>
        [DomName("borderInlineStartColor")]
        [DomName("border-inline-start-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderInlineStartColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderInlineStartColor, value);

        /// <summary>
        /// Gets the value of border-inline-start-style.
        /// </summary>
        [DomName("borderInlineStartStyle")]
        [DomName("border-inline-start-style")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderInlineStartStyle(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderInlineStartStyle);

        /// <summary>
        /// Sets the value of border-inline-start-style.
        /// </summary>
        [DomName("borderInlineStartStyle")]
        [DomName("border-inline-start-style")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderInlineStartStyle(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderInlineStartStyle, value);

        /// <summary>
        /// Gets the value of border-inline-start-width.
        /// </summary>
        [DomName("borderInlineStartWidth")]
        [DomName("border-inline-start-width")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderInlineStartWidth(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderInlineStartWidth);

        /// <summary>
        /// Sets the value of border-inline-start-width.
        /// </summary>
        [DomName("borderInlineStartWidth")]
        [DomName("border-inline-start-width")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderInlineStartWidth(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderInlineStartWidth, value);

        /// <summary>
        /// Gets the value of border-inline-end-color.
        /// </summary>
        [DomName("borderInlineEndColor")]
        [DomName("border-inline-end-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderInlineEndColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderInlineEndColor);

        /// <summary>
        /// Sets the value of border-inline-end-color.
        /// </summary>
        [DomName("borderInlineEndColor")]
        [DomName("border-inline-end-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderInlineEndColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderInlineEndColor, value);

        /// <summary>
        /// Gets the value of border-inline-end-style.
        /// </summary>
        [DomName("borderInlineEndStyle")]
        [DomName("border-inline-end-style")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderInlineEndStyle(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderInlineEndStyle);

        /// <summary>
        /// Sets the value of border-inline-end-style.
        /// </summary>
        [DomName("borderInlineEndStyle")]
        [DomName("border-inline-end-style")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderInlineEndStyle(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderInlineEndStyle, value);

        /// <summary>
        /// Gets the value of border-inline-end-width.
        /// </summary>
        [DomName("borderInlineEndWidth")]
        [DomName("border-inline-end-width")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderInlineEndWidth(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderInlineEndWidth);

        /// <summary>
        /// Sets the value of border-inline-end-width.
        /// </summary>
        [DomName("borderInlineEndWidth")]
        [DomName("border-inline-end-width")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderInlineEndWidth(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderInlineEndWidth, value);

        /// <summary>
        /// Gets the value of border-start-start-radius.
        /// </summary>
        [DomName("borderStartStartRadius")]
        [DomName("border-start-start-radius")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderStartStartRadius(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderStartStartRadius);

        /// <summary>
        /// Sets the value of border-start-start-radius.
        /// </summary>
        [DomName("borderStartStartRadius")]
        [DomName("border-start-start-radius")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderStartStartRadius(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderStartStartRadius, value);

        /// <summary>
        /// Gets the value of border-start-end-radius.
        /// </summary>
        [DomName("borderStartEndRadius")]
        [DomName("border-start-end-radius")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderStartEndRadius(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderStartEndRadius);

        /// <summary>
        /// Sets the value of border-start-end-radius.
        /// </summary>
        [DomName("borderStartEndRadius")]
        [DomName("border-start-end-radius")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderStartEndRadius(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderStartEndRadius, value);

        /// <summary>
        /// Gets the value of border-end-start-radius.
        /// </summary>
        [DomName("borderEndStartRadius")]
        [DomName("border-end-start-radius")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderEndStartRadius(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderEndStartRadius);

        /// <summary>
        /// Sets the value of border-end-start-radius.
        /// </summary>
        [DomName("borderEndStartRadius")]
        [DomName("border-end-start-radius")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderEndStartRadius(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderEndStartRadius, value);

        /// <summary>
        /// Gets the value of border-end-end-radius.
        /// </summary>
        [DomName("borderEndEndRadius")]
        [DomName("border-end-end-radius")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderEndEndRadius(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BorderEndEndRadius);

        /// <summary>
        /// Sets the value of border-end-end-radius.
        /// </summary>
        [DomName("borderEndEndRadius")]
        [DomName("border-end-end-radius")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderEndEndRadius(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BorderEndEndRadius, value);

        /// <summary>
        /// Gets the value of block-size.
        /// </summary>
        [DomName("blockSize")]
        [DomName("block-size")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBlockSize(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.BlockSize);

        /// <summary>
        /// Sets the value of block-size.
        /// </summary>
        [DomName("blockSize")]
        [DomName("block-size")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBlockSize(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.BlockSize, value);

        /// <summary>
        /// Gets the value of row-gap.
        /// </summary>
        [DomName("rowGap")]
        [DomName("row-gap")]
        [DomAccessor(Accessors.Getter)]
        public static String GetRowGap(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.RowGap);

        /// <summary>
        /// Sets the value of row-gap.
        /// </summary>
        [DomName("rowGap")]
        [DomName("row-gap")]
        [DomAccessor(Accessors.Setter)]
        public static void SetRowGap(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.RowGap, value);

        /// <summary>
        /// Gets the value of gap.
        /// </summary>
        [DomName("gap")]
        [DomAccessor(Accessors.Getter)]
        public static String GetGap(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Gap);

        /// <summary>
        /// Sets the value of gap.
        /// </summary>
        [DomName("gap")]
        [DomAccessor(Accessors.Setter)]
        public static void SetGap(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Gap, value);

        /// <summary>
        /// Gets the value of caret-color.
        /// </summary>
        [DomName("caretColor")]
        [DomName("caret-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetCaretColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.CaretColor);

        /// <summary>
        /// Sets the value of caret-color.
        /// </summary>
        [DomName("caretColor")]
        [DomName("caret-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetCaretColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.CaretColor, value);

        /// <summary>
        /// Gets the value of color-scheme.
        /// </summary>
        [DomName("colorScheme")]
        [DomName("color-scheme")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColorScheme(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ColorScheme);

        /// <summary>
        /// Sets the value of color-scheme.
        /// </summary>
        [DomName("colorScheme")]
        [DomName("color-scheme")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColorScheme(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ColorScheme, value);

        /// <summary>
        /// Gets the value of container.
        /// </summary>
        [DomName("container")]
        [DomAccessor(Accessors.Getter)]
        public static String GetContainer(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Container);

        /// <summary>
        /// Sets the value of container.
        /// </summary>
        [DomName("container")]
        [DomAccessor(Accessors.Setter)]
        public static void SetContainer(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Container, value);

        /// <summary>
        /// Gets the value of container-name.
        /// </summary>
        [DomName("containerName")]
        [DomName("container-name")]
        [DomAccessor(Accessors.Getter)]
        public static String GetContainerName(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ContainerName);

        /// <summary>
        /// Sets the value of container-name.
        /// </summary>
        [DomName("containerName")]
        [DomName("container-name")]
        [DomAccessor(Accessors.Setter)]
        public static void SetContainerName(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ContainerName, value);

        /// <summary>
        /// Gets the value of container-type.
        /// </summary>
        [DomName("containerType")]
        [DomName("container-type")]
        [DomAccessor(Accessors.Getter)]
        public static String GetContainerType(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ContainerType);

        /// <summary>
        /// Sets the value of container-type.
        /// </summary>
        [DomName("containerType")]
        [DomName("container-type")]
        [DomAccessor(Accessors.Setter)]
        public static void SetContainerType(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ContainerType, value);

        /// <summary>
        /// Gets the value of contain.
        /// </summary>
        [DomName("contain")]
        [DomAccessor(Accessors.Getter)]
        public static String GetContain(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Contain);

        /// <summary>
        /// Sets the value of contain.
        /// </summary>
        [DomName("contain")]
        [DomAccessor(Accessors.Setter)]
        public static void SetContain(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Contain, value);

        /// <summary>
        /// Gets the value of contain-intrinsic-size.
        /// </summary>
        [DomName("containIntrinsicSize")]
        [DomName("contain-intrinsic-size")]
        [DomAccessor(Accessors.Getter)]
        public static String GetContainIntrinsicSize(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ContainIntrinsicSize);

        /// <summary>
        /// Sets the value of contain-intrinsic-size.
        /// </summary>
        [DomName("containIntrinsicSize")]
        [DomName("contain-intrinsic-size")]
        [DomAccessor(Accessors.Setter)]
        public static void SetContainIntrinsicSize(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ContainIntrinsicSize, value);

        /// <summary>
        /// Gets the value of contain-intrinsic-width.
        /// </summary>
        [DomName("containIntrinsicWidth")]
        [DomName("contain-intrinsic-width")]
        [DomAccessor(Accessors.Getter)]
        public static String GetContainIntrinsicWidth(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ContainIntrinsicWidth);

        /// <summary>
        /// Sets the value of contain-intrinsic-width.
        /// </summary>
        [DomName("containIntrinsicWidth")]
        [DomName("contain-intrinsic-width")]
        [DomAccessor(Accessors.Setter)]
        public static void SetContainIntrinsicWidth(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ContainIntrinsicWidth, value);

        /// <summary>
        /// Gets the value of contain-intrinsic-height.
        /// </summary>
        [DomName("containIntrinsicHeight")]
        [DomName("contain-intrinsic-height")]
        [DomAccessor(Accessors.Getter)]
        public static String GetContainIntrinsicHeight(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ContainIntrinsicHeight);

        /// <summary>
        /// Sets the value of contain-intrinsic-height.
        /// </summary>
        [DomName("containIntrinsicHeight")]
        [DomName("contain-intrinsic-height")]
        [DomAccessor(Accessors.Setter)]
        public static void SetContainIntrinsicHeight(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ContainIntrinsicHeight, value);

        /// <summary>
        /// Gets the value of contain-intrinsic-block-size.
        /// </summary>
        [DomName("containIntrinsicBlockSize")]
        [DomName("contain-intrinsic-block-size")]
        [DomAccessor(Accessors.Getter)]
        public static String GetContainIntrinsicBlockSize(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ContainIntrinsicBlockSize);

        /// <summary>
        /// Sets the value of contain-intrinsic-block-size.
        /// </summary>
        [DomName("containIntrinsicBlockSize")]
        [DomName("contain-intrinsic-block-size")]
        [DomAccessor(Accessors.Setter)]
        public static void SetContainIntrinsicBlockSize(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ContainIntrinsicBlockSize, value);

        /// <summary>
        /// Gets the value of contain-intrinsic-inline-size.
        /// </summary>
        [DomName("containIntrinsicInlineSize")]
        [DomName("contain-intrinsic-inline-size")]
        [DomAccessor(Accessors.Getter)]
        public static String GetContainIntrinsicInlineSize(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ContainIntrinsicInlineSize);

        /// <summary>
        /// Sets the value of contain-intrinsic-inline-size.
        /// </summary>
        [DomName("containIntrinsicInlineSize")]
        [DomName("contain-intrinsic-inline-size")]
        [DomAccessor(Accessors.Setter)]
        public static void SetContainIntrinsicInlineSize(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ContainIntrinsicInlineSize, value);

        /// <summary>
        /// Gets the value of content-visibility.
        /// </summary>
        [DomName("contentVisibility")]
        [DomName("content-visibility")]
        [DomAccessor(Accessors.Getter)]
        public static String GetContentVisibility(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ContentVisibility);

        /// <summary>
        /// Sets the value of content-visibility.
        /// </summary>
        [DomName("contentVisibility")]
        [DomName("content-visibility")]
        [DomAccessor(Accessors.Setter)]
        public static void SetContentVisibility(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ContentVisibility, value);

        /// <summary>
        /// Gets the value of counter-set.
        /// </summary>
        [DomName("counterSet")]
        [DomName("counter-set")]
        [DomAccessor(Accessors.Getter)]
        public static String GetCounterSet(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.CounterSet);

        /// <summary>
        /// Sets the value of counter-set.
        /// </summary>
        [DomName("counterSet")]
        [DomName("counter-set")]
        [DomAccessor(Accessors.Setter)]
        public static void SetCounterSet(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.CounterSet, value);

        /// <summary>
        /// Gets the value of forced-color-adjust.
        /// </summary>
        [DomName("forcedColorAdjust")]
        [DomName("forced-color-adjust")]
        [DomAccessor(Accessors.Getter)]
        public static String GetForcedColorAdjust(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ForcedColorAdjust);

        /// <summary>
        /// Sets the value of forced-color-adjust.
        /// </summary>
        [DomName("forcedColorAdjust")]
        [DomName("forced-color-adjust")]
        [DomAccessor(Accessors.Setter)]
        public static void SetForcedColorAdjust(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ForcedColorAdjust, value);

        /// <summary>
        /// Gets the value of font-variant-caps.
        /// </summary>
        [DomName("fontVariantCaps")]
        [DomName("font-variant-caps")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontVariantCaps(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FontVariantCaps);

        /// <summary>
        /// Sets the value of font-variant-caps.
        /// </summary>
        [DomName("fontVariantCaps")]
        [DomName("font-variant-caps")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontVariantCaps(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FontVariantCaps, value);

        /// <summary>
        /// Gets the value of font-variant-numeric.
        /// </summary>
        [DomName("fontVariantNumeric")]
        [DomName("font-variant-numeric")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontVariantNumeric(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FontVariantNumeric);

        /// <summary>
        /// Sets the value of font-variant-numeric.
        /// </summary>
        [DomName("fontVariantNumeric")]
        [DomName("font-variant-numeric")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontVariantNumeric(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FontVariantNumeric, value);

        /// <summary>
        /// Gets the value of font-variant-ligatures.
        /// </summary>
        [DomName("fontVariantLigatures")]
        [DomName("font-variant-ligatures")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontVariantLigatures(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FontVariantLigatures);

        /// <summary>
        /// Sets the value of font-variant-ligatures.
        /// </summary>
        [DomName("fontVariantLigatures")]
        [DomName("font-variant-ligatures")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontVariantLigatures(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FontVariantLigatures, value);

        /// <summary>
        /// Gets the value of font-variant-east-asian.
        /// </summary>
        [DomName("fontVariantEastAsian")]
        [DomName("font-variant-east-asian")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontVariantEastAsian(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FontVariantEastAsian);

        /// <summary>
        /// Sets the value of font-variant-east-asian.
        /// </summary>
        [DomName("fontVariantEastAsian")]
        [DomName("font-variant-east-asian")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontVariantEastAsian(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FontVariantEastAsian, value);

        /// <summary>
        /// Gets the value of font-kerning.
        /// </summary>
        [DomName("fontKerning")]
        [DomName("font-kerning")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontKerning(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FontKerning);

        /// <summary>
        /// Sets the value of font-kerning.
        /// </summary>
        [DomName("fontKerning")]
        [DomName("font-kerning")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontKerning(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FontKerning, value);

        /// <summary>
        /// Gets the value of font-language-override.
        /// </summary>
        [DomName("fontLanguageOverride")]
        [DomName("font-language-override")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontLanguageOverride(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FontLanguageOverride);

        /// <summary>
        /// Sets the value of font-language-override.
        /// </summary>
        [DomName("fontLanguageOverride")]
        [DomName("font-language-override")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontLanguageOverride(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FontLanguageOverride, value);

        /// <summary>
        /// Gets the value of font-optical-sizing.
        /// </summary>
        [DomName("fontOpticalSizing")]
        [DomName("font-optical-sizing")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontOpticalSizing(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FontOpticalSizing);

        /// <summary>
        /// Sets the value of font-optical-sizing.
        /// </summary>
        [DomName("fontOpticalSizing")]
        [DomName("font-optical-sizing")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontOpticalSizing(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FontOpticalSizing, value);

        /// <summary>
        /// Gets the value of font-palette.
        /// </summary>
        [DomName("fontPalette")]
        [DomName("font-palette")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontPalette(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FontPalette);

        /// <summary>
        /// Sets the value of font-palette.
        /// </summary>
        [DomName("fontPalette")]
        [DomName("font-palette")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontPalette(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FontPalette, value);

        /// <summary>
        /// Gets the value of font-synthesis.
        /// </summary>
        [DomName("fontSynthesis")]
        [DomName("font-synthesis")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontSynthesis(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FontSynthesis);

        /// <summary>
        /// Sets the value of font-synthesis.
        /// </summary>
        [DomName("fontSynthesis")]
        [DomName("font-synthesis")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontSynthesis(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FontSynthesis, value);

        /// <summary>
        /// Gets the value of font-synthesis-weight.
        /// </summary>
        [DomName("fontSynthesisWeight")]
        [DomName("font-synthesis-weight")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontSynthesisWeight(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FontSynthesisWeight);

        /// <summary>
        /// Sets the value of font-synthesis-weight.
        /// </summary>
        [DomName("fontSynthesisWeight")]
        [DomName("font-synthesis-weight")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontSynthesisWeight(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FontSynthesisWeight, value);

        /// <summary>
        /// Gets the value of font-synthesis-style.
        /// </summary>
        [DomName("fontSynthesisStyle")]
        [DomName("font-synthesis-style")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontSynthesisStyle(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FontSynthesisStyle);

        /// <summary>
        /// Sets the value of font-synthesis-style.
        /// </summary>
        [DomName("fontSynthesisStyle")]
        [DomName("font-synthesis-style")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontSynthesisStyle(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FontSynthesisStyle, value);

        /// <summary>
        /// Gets the value of font-synthesis-small-caps.
        /// </summary>
        [DomName("fontSynthesisSmallCaps")]
        [DomName("font-synthesis-small-caps")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontSynthesisSmallCaps(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FontSynthesisSmallCaps);

        /// <summary>
        /// Sets the value of font-synthesis-small-caps.
        /// </summary>
        [DomName("fontSynthesisSmallCaps")]
        [DomName("font-synthesis-small-caps")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontSynthesisSmallCaps(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FontSynthesisSmallCaps, value);

        /// <summary>
        /// Gets the value of font-variation-settings.
        /// </summary>
        [DomName("fontVariationSettings")]
        [DomName("font-variation-settings")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontVariationSettings(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.FontVariationSettings);

        /// <summary>
        /// Sets the value of font-variation-settings.
        /// </summary>
        [DomName("fontVariationSettings")]
        [DomName("font-variation-settings")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontVariationSettings(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.FontVariationSettings, value);

        /// <summary>
        /// Gets the value of hanging-punctuation.
        /// </summary>
        [DomName("hangingPunctuation")]
        [DomName("hanging-punctuation")]
        [DomAccessor(Accessors.Getter)]
        public static String GetHangingPunctuation(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.HangingPunctuation);

        /// <summary>
        /// Sets the value of hanging-punctuation.
        /// </summary>
        [DomName("hangingPunctuation")]
        [DomName("hanging-punctuation")]
        [DomAccessor(Accessors.Setter)]
        public static void SetHangingPunctuation(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.HangingPunctuation, value);

        /// <summary>
        /// Gets the value of hyphenate-character.
        /// </summary>
        [DomName("hyphenateCharacter")]
        [DomName("hyphenate-character")]
        [DomAccessor(Accessors.Getter)]
        public static String GetHyphenateCharacter(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.HyphenateCharacter);

        /// <summary>
        /// Sets the value of hyphenate-character.
        /// </summary>
        [DomName("hyphenateCharacter")]
        [DomName("hyphenate-character")]
        [DomAccessor(Accessors.Setter)]
        public static void SetHyphenateCharacter(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.HyphenateCharacter, value);

        /// <summary>
        /// Gets the value of hyphenate-limit-chars.
        /// </summary>
        [DomName("hyphenateLimitChars")]
        [DomName("hyphenate-limit-chars")]
        [DomAccessor(Accessors.Getter)]
        public static String GetHypenatateLimitChars(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.HypenatateLimitChars);

        /// <summary>
        /// Sets the value of hyphenate-limit-chars.
        /// </summary>
        [DomName("hyphenateLimitChars")]
        [DomName("hyphenate-limit-chars")]
        [DomAccessor(Accessors.Setter)]
        public static void SetHypenatateLimitChars(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.HypenatateLimitChars, value);

        /// <summary>
        /// Gets the value of hyphens.
        /// </summary>
        [DomName("hyphens")]
        [DomAccessor(Accessors.Getter)]
        public static String GetHyphens(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Hyphens);

        /// <summary>
        /// Sets the value of hyphens.
        /// </summary>
        [DomName("hyphens")]
        [DomAccessor(Accessors.Setter)]
        public static void SetHyphens(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Hyphens, value);

        /// <summary>
        /// Gets the value of initial-letter.
        /// </summary>
        [DomName("initialLetter")]
        [DomName("initial-letter")]
        [DomAccessor(Accessors.Getter)]
        public static String GetInitialLetter(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.InitialLetter);

        /// <summary>
        /// Sets the value of initial-letter.
        /// </summary>
        [DomName("initialLetter")]
        [DomName("initial-letter")]
        [DomAccessor(Accessors.Setter)]
        public static void SetInitialLetter(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.InitialLetter, value);

        /// <summary>
        /// Gets the value of initial-letter-align.
        /// </summary>
        [DomName("initialLetterAlign")]
        [DomName("initial-letter-align")]
        [DomAccessor(Accessors.Getter)]
        public static String GetInitialLetterAlign(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.InitialLetterAlign);

        /// <summary>
        /// Sets the value of initial-letter-align.
        /// </summary>
        [DomName("initialLetterAlign")]
        [DomName("initial-letter-align")]
        [DomAccessor(Accessors.Setter)]
        public static void SetInitialLetterAlign(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.InitialLetterAlign, value);

        /// <summary>
        /// Gets the value of image-orientation.
        /// </summary>
        [DomName("imageOrientation")]
        [DomName("image-orientation")]
        [DomAccessor(Accessors.Getter)]
        public static String GetImageOrientation(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ImageOrientation);

        /// <summary>
        /// Sets the value of image-orientation.
        /// </summary>
        [DomName("imageOrientation")]
        [DomName("image-orientation")]
        [DomAccessor(Accessors.Setter)]
        public static void SetImageOrientation(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ImageOrientation, value);

        /// <summary>
        /// Gets the value of image-rendering.
        /// </summary>
        [DomName("imageRendering")]
        [DomName("image-rendering")]
        [DomAccessor(Accessors.Getter)]
        public static String GetImageRendering(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ImageRendering);

        /// <summary>
        /// Sets the value of image-rendering.
        /// </summary>
        [DomName("imageRendering")]
        [DomName("image-rendering")]
        [DomAccessor(Accessors.Setter)]
        public static void SetImageRendering(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ImageRendering, value);

        /// <summary>
        /// Gets the value of inline-size.
        /// </summary>
        [DomName("inlineSize")]
        [DomName("inline-size")]
        [DomAccessor(Accessors.Getter)]
        public static String GetInlineSize(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.InlineSize);

        /// <summary>
        /// Sets the value of inline-size.
        /// </summary>
        [DomName("inlineSize")]
        [DomName("inline-size")]
        [DomAccessor(Accessors.Setter)]
        public static void SetInlineSize(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.InlineSize, value);

        /// <summary>
        /// Gets the value of isolation.
        /// </summary>
        [DomName("isolation")]
        [DomAccessor(Accessors.Getter)]
        public static String GetIsolation(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Isolation);

        /// <summary>
        /// Sets the value of isolation.
        /// </summary>
        [DomName("isolation")]
        [DomAccessor(Accessors.Setter)]
        public static void SetIsolation(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Isolation, value);

        /// <summary>
        /// Gets the value of inset.
        /// </summary>
        [DomName("inset")]
        [DomAccessor(Accessors.Getter)]
        public static String GetInset(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Inset);

        /// <summary>
        /// Sets the value of inset.
        /// </summary>
        [DomName("inset")]
        [DomAccessor(Accessors.Setter)]
        public static void SetInset(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Inset, value);

        /// <summary>
        /// Gets the value of inset-block.
        /// </summary>
        [DomName("insetBlock")]
        [DomName("inset-block")]
        [DomAccessor(Accessors.Getter)]
        public static String GetInsetBlock(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.InsetBlock);

        /// <summary>
        /// Sets the value of inset-block.
        /// </summary>
        [DomName("insetBlock")]
        [DomName("inset-block")]
        [DomAccessor(Accessors.Setter)]
        public static void SetInsetBlock(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.InsetBlock, value);

        /// <summary>
        /// Gets the value of inset-block-start.
        /// </summary>
        [DomName("insetBlockStart")]
        [DomName("inset-block-start")]
        [DomAccessor(Accessors.Getter)]
        public static String GetInsetBlockStart(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.InsetBlockStart);

        /// <summary>
        /// Sets the value of inset-block-start.
        /// </summary>
        [DomName("insetBlockStart")]
        [DomName("inset-block-start")]
        [DomAccessor(Accessors.Setter)]
        public static void SetInsetBlockStart(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.InsetBlockStart, value);

        /// <summary>
        /// Gets the value of inset-block-end.
        /// </summary>
        [DomName("insetBlockEnd")]
        [DomName("inset-block-end")]
        [DomAccessor(Accessors.Getter)]
        public static String GetInsetBlockEnd(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.InsetBlockEnd);

        /// <summary>
        /// Sets the value of inset-block-end.
        /// </summary>
        [DomName("insetBlockEnd")]
        [DomName("inset-block-end")]
        [DomAccessor(Accessors.Setter)]
        public static void SetInsetBlockEnd(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.InsetBlockEnd, value);

        /// <summary>
        /// Gets the value of inset-inline.
        /// </summary>
        [DomName("insetInline")]
        [DomName("inset-inline")]
        [DomAccessor(Accessors.Getter)]
        public static String GetInsetInline(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.InsetInline);

        /// <summary>
        /// Sets the value of inset-inline.
        /// </summary>
        [DomName("insetInline")]
        [DomName("inset-inline")]
        [DomAccessor(Accessors.Setter)]
        public static void SetInsetInline(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.InsetInline, value);

        /// <summary>
        /// Gets the value of inset-inline-start.
        /// </summary>
        [DomName("insetInlineStart")]
        [DomName("inset-inline-start")]
        [DomAccessor(Accessors.Getter)]
        public static String GetInsetInlineStart(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.InsetInlineStart);

        /// <summary>
        /// Sets the value of inset-inline-start.
        /// </summary>
        [DomName("insetInlineStart")]
        [DomName("inset-inline-start")]
        [DomAccessor(Accessors.Setter)]
        public static void SetInsetInlineStart(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.InsetInlineStart, value);

        /// <summary>
        /// Gets the value of inset-inline-end.
        /// </summary>
        [DomName("insetInlineEnd")]
        [DomName("inset-inline-end")]
        [DomAccessor(Accessors.Getter)]
        public static String GetInsetInlineEnd(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.InsetInlineEnd);

        /// <summary>
        /// Sets the value of inset-inline-end.
        /// </summary>
        [DomName("insetInlineEnd")]
        [DomName("inset-inline-end")]
        [DomAccessor(Accessors.Setter)]
        public static void SetInsetInlineEnd(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.InsetInlineEnd, value);

        /// <summary>
        /// Gets the value of justify-items.
        /// </summary>
        [DomName("justifyItems")]
        [DomName("justify-items")]
        [DomAccessor(Accessors.Getter)]
        public static String GetJustifyItems(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.JustifyItems);

        /// <summary>
        /// Sets the value of justify-items.
        /// </summary>
        [DomName("justifyItems")]
        [DomName("justify-items")]
        [DomAccessor(Accessors.Setter)]
        public static void SetJustifyItems(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.JustifyItems, value);

        /// <summary>
        /// Gets the value of justify-self.
        /// </summary>
        [DomName("justifySelf")]
        [DomName("justify-self")]
        [DomAccessor(Accessors.Getter)]
        public static String GetJustifySelf(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.JustifySelf);

        /// <summary>
        /// Sets the value of justify-self.
        /// </summary>
        [DomName("justifySelf")]
        [DomName("justify-self")]
        [DomAccessor(Accessors.Setter)]
        public static void SetJustifySelf(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.JustifySelf, value);

        /// <summary>
        /// Gets the value of grid-template-rows.
        /// </summary>
        [DomName("gridTemplateRows")]
        [DomName("grid-template-rows")]
        [DomAccessor(Accessors.Getter)]
        public static String GetGridTemplateRows(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.GridTemplateRows);

        /// <summary>
        /// Sets the value of grid-template-rows.
        /// </summary>
        [DomName("gridTemplateRows")]
        [DomName("grid-template-rows")]
        [DomAccessor(Accessors.Setter)]
        public static void SetGridTemplateRows(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.GridTemplateRows, value);

        /// <summary>
        /// Gets the value of grid-template-columns.
        /// </summary>
        [DomName("gridTemplateColumns")]
        [DomName("grid-template-columns")]
        [DomAccessor(Accessors.Getter)]
        public static String GetGridTemplateColumns(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.GridTemplateColumns);

        /// <summary>
        /// Sets the value of grid-template-columns.
        /// </summary>
        [DomName("gridTemplateColumns")]
        [DomName("grid-template-columns")]
        [DomAccessor(Accessors.Setter)]
        public static void SetGridTemplateColumns(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.GridTemplateColumns, value);

        /// <summary>
        /// Gets the value of grid-template-areas.
        /// </summary>
        [DomName("gridTemplateAreas")]
        [DomName("grid-template-areas")]
        [DomAccessor(Accessors.Getter)]
        public static String GetGridTemplateAreas(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.GridTemplateAreas);

        /// <summary>
        /// Sets the value of grid-template-areas.
        /// </summary>
        [DomName("gridTemplateAreas")]
        [DomName("grid-template-areas")]
        [DomAccessor(Accessors.Setter)]
        public static void SetGridTemplateAreas(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.GridTemplateAreas, value);

        /// <summary>
        /// Gets the value of grid-template.
        /// </summary>
        [DomName("gridTemplate")]
        [DomName("grid-template")]
        [DomAccessor(Accessors.Getter)]
        public static String GetGridTemplate(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.GridTemplate);

        /// <summary>
        /// Sets the value of grid-template.
        /// </summary>
        [DomName("gridTemplate")]
        [DomName("grid-template")]
        [DomAccessor(Accessors.Setter)]
        public static void SetGridTemplate(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.GridTemplate, value);

        /// <summary>
        /// Gets the value of grid-auto-columns.
        /// </summary>
        [DomName("gridAutoColumns")]
        [DomName("grid-auto-columns")]
        [DomAccessor(Accessors.Getter)]
        public static String GetGridAutoColumns(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.GridAutoColumns);

        /// <summary>
        /// Sets the value of grid-auto-columns.
        /// </summary>
        [DomName("gridAutoColumns")]
        [DomName("grid-auto-columns")]
        [DomAccessor(Accessors.Setter)]
        public static void SetGridAutoColumns(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.GridAutoColumns, value);

        /// <summary>
        /// Gets the value of grid-auto-rows.
        /// </summary>
        [DomName("gridAutoRows")]
        [DomName("grid-auto-rows")]
        [DomAccessor(Accessors.Getter)]
        public static String GetGridAutoRows(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.GridAutoRows);

        /// <summary>
        /// Sets the value of grid-auto-rows.
        /// </summary>
        [DomName("gridAutoRows")]
        [DomName("grid-auto-rows")]
        [DomAccessor(Accessors.Setter)]
        public static void SetGridAutoRows(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.GridAutoRows, value);

        /// <summary>
        /// Gets the value of grid-auto-flow.
        /// </summary>
        [DomName("gridAutoFlow")]
        [DomName("grid-auto-flow")]
        [DomAccessor(Accessors.Getter)]
        public static String GetGridAutoFlow(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.GridAutoFlow);

        /// <summary>
        /// Sets the value of grid-auto-flow.
        /// </summary>
        [DomName("gridAutoFlow")]
        [DomName("grid-auto-flow")]
        [DomAccessor(Accessors.Setter)]
        public static void SetGridAutoFlow(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.GridAutoFlow, value);

        /// <summary>
        /// Gets the value of grid.
        /// </summary>
        [DomName("grid")]
        [DomAccessor(Accessors.Getter)]
        public static String GetGrid(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Grid);

        /// <summary>
        /// Sets the value of grid.
        /// </summary>
        [DomName("grid")]
        [DomAccessor(Accessors.Setter)]
        public static void SetGrid(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Grid, value);

        /// <summary>
        /// Gets the value of grid-row-start.
        /// </summary>
        [DomName("gridRowStart")]
        [DomName("grid-row-start")]
        [DomAccessor(Accessors.Getter)]
        public static String GetGridRowStart(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.GridRowStart);

        /// <summary>
        /// Sets the value of grid-row-start.
        /// </summary>
        [DomName("gridRowStart")]
        [DomName("grid-row-start")]
        [DomAccessor(Accessors.Setter)]
        public static void SetGridRowStart(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.GridRowStart, value);

        /// <summary>
        /// Gets the value of grid-column-start.
        /// </summary>
        [DomName("gridColumnStart")]
        [DomName("grid-column-start")]
        [DomAccessor(Accessors.Getter)]
        public static String GetGridColumnStart(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.GridColumnStart);

        /// <summary>
        /// Sets the value of grid-column-start.
        /// </summary>
        [DomName("gridColumnStart")]
        [DomName("grid-column-start")]
        [DomAccessor(Accessors.Setter)]
        public static void SetGridColumnStart(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.GridColumnStart, value);

        /// <summary>
        /// Gets the value of grid-row-end.
        /// </summary>
        [DomName("gridRowEnd")]
        [DomName("grid-row-end")]
        [DomAccessor(Accessors.Getter)]
        public static String GetGridRowEnd(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.GridRowEnd);

        /// <summary>
        /// Sets the value of grid-row-end.
        /// </summary>
        [DomName("gridRowEnd")]
        [DomName("grid-row-end")]
        [DomAccessor(Accessors.Setter)]
        public static void SetGridRowEnd(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.GridRowEnd, value);

        /// <summary>
        /// Gets the value of grid-column-end.
        /// </summary>
        [DomName("gridColumnEnd")]
        [DomName("grid-column-end")]
        [DomAccessor(Accessors.Getter)]
        public static String GetGridColumnEnd(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.GridColumnEnd);

        /// <summary>
        /// Sets the value of grid-column-end.
        /// </summary>
        [DomName("gridColumnEnd")]
        [DomName("grid-column-end")]
        [DomAccessor(Accessors.Setter)]
        public static void SetGridColumnEnd(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.GridColumnEnd, value);

        /// <summary>
        /// Gets the value of grid-row.
        /// </summary>
        [DomName("gridRow")]
        [DomName("grid-row")]
        [DomAccessor(Accessors.Getter)]
        public static String GetGridRow(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.GridRow);

        /// <summary>
        /// Sets the value of grid-row.
        /// </summary>
        [DomName("gridRow")]
        [DomName("grid-row")]
        [DomAccessor(Accessors.Setter)]
        public static void SetGridRow(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.GridRow, value);

        /// <summary>
        /// Gets the value of grid-column.
        /// </summary>
        [DomName("gridColumn")]
        [DomName("grid-column")]
        [DomAccessor(Accessors.Getter)]
        public static String GetGridColumn(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.GridColumn);

        /// <summary>
        /// Sets the value of grid-column.
        /// </summary>
        [DomName("gridColumn")]
        [DomName("grid-column")]
        [DomAccessor(Accessors.Setter)]
        public static void SetGridColumn(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.GridColumn, value);

        /// <summary>
        /// Gets the value of grid-area.
        /// </summary>
        [DomName("gridArea")]
        [DomName("grid-area")]
        [DomAccessor(Accessors.Getter)]
        public static String GetGridArea(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.GridArea);

        /// <summary>
        /// Sets the value of grid-area.
        /// </summary>
        [DomName("gridArea")]
        [DomName("grid-area")]
        [DomAccessor(Accessors.Setter)]
        public static void SetGridArea(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.GridArea, value);

        /// <summary>
        /// Gets the value of grid-row-gap.
        /// </summary>
        [DomName("gridRowGap")]
        [DomName("grid-row-gap")]
        [DomAccessor(Accessors.Getter)]
        public static String GetGridRowGap(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.GridRowGap);

        /// <summary>
        /// Sets the value of grid-row-gap.
        /// </summary>
        [DomName("gridRowGap")]
        [DomName("grid-row-gap")]
        [DomAccessor(Accessors.Setter)]
        public static void SetGridRowGap(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.GridRowGap, value);

        /// <summary>
        /// Gets the value of grid-column-gap.
        /// </summary>
        [DomName("gridColumnGap")]
        [DomName("grid-column-gap")]
        [DomAccessor(Accessors.Getter)]
        public static String GetGridColumnGap(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.GridColumnGap);

        /// <summary>
        /// Sets the value of grid-column-gap.
        /// </summary>
        [DomName("gridColumnGap")]
        [DomName("grid-column-gap")]
        [DomAccessor(Accessors.Setter)]
        public static void SetGridColumnGap(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.GridColumnGap, value);

        /// <summary>
        /// Gets the value of grid-gap.
        /// </summary>
        [DomName("gridGap")]
        [DomName("grid-gap")]
        [DomAccessor(Accessors.Getter)]
        public static String GetGridGap(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.GridGap);

        /// <summary>
        /// Sets the value of grid-gap.
        /// </summary>
        [DomName("gridGap")]
        [DomName("grid-gap")]
        [DomAccessor(Accessors.Setter)]
        public static void SetGridGap(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.GridGap, value);

        /// <summary>
        /// Gets the value of line-break.
        /// </summary>
        [DomName("lineBreak")]
        [DomName("line-break")]
        [DomAccessor(Accessors.Getter)]
        public static String GetLineBreak(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.LineBreak);

        /// <summary>
        /// Sets the value of line-break.
        /// </summary>
        [DomName("lineBreak")]
        [DomName("line-break")]
        [DomAccessor(Accessors.Setter)]
        public static void SetLineBreak(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.LineBreak, value);

        /// <summary>
        /// Gets the value of margin-block.
        /// </summary>
        [DomName("marginBlock")]
        [DomName("margin-block")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMarginBlock(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MarginBlock);

        /// <summary>
        /// Sets the value of margin-block.
        /// </summary>
        [DomName("marginBlock")]
        [DomName("margin-block")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMarginBlock(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MarginBlock, value);

        /// <summary>
        /// Gets the value of margin-block-end.
        /// </summary>
        [DomName("marginBlockEnd")]
        [DomName("margin-block-end")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMarginBlockEnd(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MarginBlockEnd);

        /// <summary>
        /// Sets the value of margin-block-end.
        /// </summary>
        [DomName("marginBlockEnd")]
        [DomName("margin-block-end")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMarginBlockEnd(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MarginBlockEnd, value);

        /// <summary>
        /// Gets the value of margin-block-start.
        /// </summary>
        [DomName("marginBlockStart")]
        [DomName("margin-block-start")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMarginBlockStart(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MarginBlockStart);

        /// <summary>
        /// Sets the value of margin-block-start.
        /// </summary>
        [DomName("marginBlockStart")]
        [DomName("margin-block-start")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMarginBlockStart(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MarginBlockStart, value);

        /// <summary>
        /// Gets the value of margin-inline.
        /// </summary>
        [DomName("marginInline")]
        [DomName("margin-inline")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMarginInline(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MarginInline);

        /// <summary>
        /// Sets the value of margin-inline.
        /// </summary>
        [DomName("marginInline")]
        [DomName("margin-inline")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMarginInline(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MarginInline, value);

        /// <summary>
        /// Gets the value of margin-inline-end.
        /// </summary>
        [DomName("marginInlineEnd")]
        [DomName("margin-inline-end")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMarginInlineEnd(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MarginInlineEnd);

        /// <summary>
        /// Sets the value of margin-inline-end.
        /// </summary>
        [DomName("marginInlineEnd")]
        [DomName("margin-inline-end")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMarginInlineEnd(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MarginInlineEnd, value);

        /// <summary>
        /// Gets the value of margin-inline-start.
        /// </summary>
        [DomName("marginInlineStart")]
        [DomName("margin-inline-start")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMarginInlineStart(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MarginInlineStart);

        /// <summary>
        /// Sets the value of margin-inline-start.
        /// </summary>
        [DomName("marginInlineStart")]
        [DomName("margin-inline-start")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMarginInlineStart(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MarginInlineStart, value);

        /// <summary>
        /// Gets the value of mix-blend-mode.
        /// </summary>
        [DomName("mixBlendMode")]
        [DomName("mix-blend-mode")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMixBlendMode(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MixBlendMode);

        /// <summary>
        /// Sets the value of mix-blend-mode.
        /// </summary>
        [DomName("mixBlendMode")]
        [DomName("mix-blend-mode")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMixBlendMode(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MixBlendMode, value);

        /// <summary>
        /// Gets the value of mask-border.
        /// </summary>
        [DomName("maskBorder")]
        [DomName("mask-border")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMaskBorder(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MaskBorder);

        /// <summary>
        /// Sets the value of mask-border.
        /// </summary>
        [DomName("maskBorder")]
        [DomName("mask-border")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMaskBorder(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MaskBorder, value);

        /// <summary>
        /// Gets the value of mask-border-mode.
        /// </summary>
        [DomName("maskBorderMode")]
        [DomName("mask-border-mode")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMaskBorderMode(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MaskBorderMode);

        /// <summary>
        /// Sets the value of mask-border-mode.
        /// </summary>
        [DomName("maskBorderMode")]
        [DomName("mask-border-mode")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMaskBorderMode(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MaskBorderMode, value);

        /// <summary>
        /// Gets the value of mask-border-outset.
        /// </summary>
        [DomName("maskBorderOutset")]
        [DomName("mask-border-outset")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMaskBorderOutset(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MaskBorderOutset);

        /// <summary>
        /// Sets the value of mask-border-outset.
        /// </summary>
        [DomName("maskBorderOutset")]
        [DomName("mask-border-outset")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMaskBorderOutset(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MaskBorderOutset, value);

        /// <summary>
        /// Gets the value of mask-border-repeat.
        /// </summary>
        [DomName("maskBorderRepeat")]
        [DomName("mask-border-repeat")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMaskBorderRepeat(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MaskBorderRepeat);

        /// <summary>
        /// Sets the value of mask-border-repeat.
        /// </summary>
        [DomName("maskBorderRepeat")]
        [DomName("mask-border-repeat")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMaskBorderRepeat(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MaskBorderRepeat, value);

        /// <summary>
        /// Gets the value of mask-border-slice.
        /// </summary>
        [DomName("maskBorderSlice")]
        [DomName("mask-border-slice")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMaskBorderSlice(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MaskBorderSlice);

        /// <summary>
        /// Sets the value of mask-border-slice.
        /// </summary>
        [DomName("maskBorderSlice")]
        [DomName("mask-border-slice")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMaskBorderSlice(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MaskBorderSlice, value);

        /// <summary>
        /// Gets the value of mask-border-source.
        /// </summary>
        [DomName("maskBorderSource")]
        [DomName("mask-border-source")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMaskBorderSource(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MaskBorderSource);

        /// <summary>
        /// Sets the value of mask-border-source.
        /// </summary>
        [DomName("maskBorderSource")]
        [DomName("mask-border-source")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMaskBorderSource(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MaskBorderSource, value);

        /// <summary>
        /// Gets the value of mask-border-width.
        /// </summary>
        [DomName("maskBorderWidth")]
        [DomName("mask-border-width")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMaskBorderWidth(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MaskBorderWidth);

        /// <summary>
        /// Sets the value of mask-border-width.
        /// </summary>
        [DomName("maskBorderWidth")]
        [DomName("mask-border-width")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMaskBorderWidth(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MaskBorderWidth, value);

        /// <summary>
        /// Gets the value of mask-clip.
        /// </summary>
        [DomName("maskClip")]
        [DomName("mask-clip")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMaskClip(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MaskClip);

        /// <summary>
        /// Sets the value of mask-clip.
        /// </summary>
        [DomName("maskClip")]
        [DomName("mask-clip")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMaskClip(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MaskClip, value);

        /// <summary>
        /// Gets the value of mask-composite.
        /// </summary>
        [DomName("maskComposite")]
        [DomName("mask-composite")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMaskComposite(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MaskComposite);

        /// <summary>
        /// Sets the value of mask-composite.
        /// </summary>
        [DomName("maskComposite")]
        [DomName("mask-composite")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMaskComposite(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MaskComposite, value);

        /// <summary>
        /// Gets the value of mask-image.
        /// </summary>
        [DomName("maskImage")]
        [DomName("mask-image")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMaskImage(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MaskImage);

        /// <summary>
        /// Sets the value of mask-image.
        /// </summary>
        [DomName("maskImage")]
        [DomName("mask-image")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMaskImage(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MaskImage, value);

        /// <summary>
        /// Gets the value of mask-mode.
        /// </summary>
        [DomName("maskMode")]
        [DomName("mask-mode")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMaskMode(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MaskMode);

        /// <summary>
        /// Sets the value of mask-mode.
        /// </summary>
        [DomName("maskMode")]
        [DomName("mask-mode")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMaskMode(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MaskMode, value);

        /// <summary>
        /// Gets the value of mask-origin.
        /// </summary>
        [DomName("maskOrigin")]
        [DomName("mask-origin")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMaskOrigin(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MaskOrigin);

        /// <summary>
        /// Sets the value of mask-origin.
        /// </summary>
        [DomName("maskOrigin")]
        [DomName("mask-origin")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMaskOrigin(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MaskOrigin, value);

        /// <summary>
        /// Gets the value of mask-position.
        /// </summary>
        [DomName("maskPosition")]
        [DomName("mask-position")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMaskPosition(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MaskPosition);

        /// <summary>
        /// Sets the value of mask-position.
        /// </summary>
        [DomName("maskPosition")]
        [DomName("mask-position")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMaskPosition(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MaskPosition, value);

        /// <summary>
        /// Gets the value of mask-repeat.
        /// </summary>
        [DomName("maskRepeat")]
        [DomName("mask-repeat")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMaskRepeat(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MaskRepeat);

        /// <summary>
        /// Sets the value of mask-repeat.
        /// </summary>
        [DomName("maskRepeat")]
        [DomName("mask-repeat")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMaskRepeat(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MaskRepeat, value);

        /// <summary>
        /// Gets the value of mask-size.
        /// </summary>
        [DomName("maskSize")]
        [DomName("mask-size")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMaskSize(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MaskSize);

        /// <summary>
        /// Sets the value of mask-size.
        /// </summary>
        [DomName("maskSize")]
        [DomName("mask-size")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMaskSize(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MaskSize, value);

        /// <summary>
        /// Gets the value of mask-type.
        /// </summary>
        [DomName("maskType")]
        [DomName("mask-type")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMaskType(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MaskType);

        /// <summary>
        /// Sets the value of mask-type.
        /// </summary>
        [DomName("maskType")]
        [DomName("mask-type")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMaskType(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MaskType, value);

        /// <summary>
        /// Gets the value of max-block-size.
        /// </summary>
        [DomName("maxBlockSize")]
        [DomName("max-block-size")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMaxBlockSize(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MaxBlockSize);

        /// <summary>
        /// Sets the value of max-block-size.
        /// </summary>
        [DomName("maxBlockSize")]
        [DomName("max-block-size")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMaxBlockSize(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MaxBlockSize, value);

        /// <summary>
        /// Gets the value of max-inline-size.
        /// </summary>
        [DomName("maxInlineSize")]
        [DomName("max-inline-size")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMaxInlineSize(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MaxInlineSize);

        /// <summary>
        /// Sets the value of max-inline-size.
        /// </summary>
        [DomName("maxInlineSize")]
        [DomName("max-inline-size")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMaxInlineSize(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MaxInlineSize, value);

        /// <summary>
        /// Gets the value of min-block-size.
        /// </summary>
        [DomName("minBlockSize")]
        [DomName("min-block-size")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMinBlockSize(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MinBlockSize);

        /// <summary>
        /// Sets the value of min-block-size.
        /// </summary>
        [DomName("minBlockSize")]
        [DomName("min-block-size")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMinBlockSize(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MinBlockSize, value);

        /// <summary>
        /// Gets the value of min-inline-size.
        /// </summary>
        [DomName("minInlineSize")]
        [DomName("min-inline-size")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMinInlineSize(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.MinInlineSize);

        /// <summary>
        /// Sets the value of min-inline-size.
        /// </summary>
        [DomName("minInlineSize")]
        [DomName("min-inline-size")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMinInlineSize(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.MinInlineSize, value);

        /// <summary>
        /// Gets the value of outline-offset.
        /// </summary>
        [DomName("outlineOffset")]
        [DomName("outline-offset")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOutlineOffset(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.OutlineOffset);

        /// <summary>
        /// Sets the value of outline-offset.
        /// </summary>
        [DomName("outlineOffset")]
        [DomName("outline-offset")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOutlineOffset(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.OutlineOffset, value);

        /// <summary>
        /// Gets the value of overflow-clip-margin.
        /// </summary>
        [DomName("overflowClipMargin")]
        [DomName("overflow-clip-margin")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOverflowClipMargin(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.OverflowClipMargin);

        /// <summary>
        /// Sets the value of overflow-clip-margin.
        /// </summary>
        [DomName("overflowClipMargin")]
        [DomName("overflow-clip-margin")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOverflowClipMargin(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.OverflowClipMargin, value);

        /// <summary>
        /// Gets the value of overflow-anchor.
        /// </summary>
        [DomName("overflowAnchor")]
        [DomName("overflow-anchor")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOverflowAnchor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.OverflowAnchor);

        /// <summary>
        /// Sets the value of overflow-anchor.
        /// </summary>
        [DomName("overflowAnchor")]
        [DomName("overflow-anchor")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOverflowAnchor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.OverflowAnchor, value);

        /// <summary>
        /// Gets the value of padding-block.
        /// </summary>
        [DomName("paddingBlock")]
        [DomName("padding-block")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPaddingBlock(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.PaddingBlock);

        /// <summary>
        /// Sets the value of padding-block.
        /// </summary>
        [DomName("paddingBlock")]
        [DomName("padding-block")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPaddingBlock(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.PaddingBlock, value);

        /// <summary>
        /// Gets the value of padding-block-end.
        /// </summary>
        [DomName("paddingBlockEnd")]
        [DomName("padding-block-end")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPaddingBlockEnd(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.PaddingBlockEnd);

        /// <summary>
        /// Sets the value of padding-block-end.
        /// </summary>
        [DomName("paddingBlockEnd")]
        [DomName("padding-block-end")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPaddingBlockEnd(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.PaddingBlockEnd, value);

        /// <summary>
        /// Gets the value of padding-block-start.
        /// </summary>
        [DomName("paddingBlockStart")]
        [DomName("padding-block-start")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPaddingBlockStart(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.PaddingBlockStart);

        /// <summary>
        /// Sets the value of padding-block-start.
        /// </summary>
        [DomName("paddingBlockStart")]
        [DomName("padding-block-start")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPaddingBlockStart(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.PaddingBlockStart, value);

        /// <summary>
        /// Gets the value of padding-inline.
        /// </summary>
        [DomName("paddingInline")]
        [DomName("padding-inline")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPaddingInline(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.PaddingInline);

        /// <summary>
        /// Sets the value of padding-inline.
        /// </summary>
        [DomName("paddingInline")]
        [DomName("padding-inline")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPaddingInline(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.PaddingInline, value);

        /// <summary>
        /// Gets the value of padding-inline-end.
        /// </summary>
        [DomName("paddingInlineEnd")]
        [DomName("padding-inline-end")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPaddingInlineEnd(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.PaddingInlineEnd);

        /// <summary>
        /// Sets the value of padding-inline-end.
        /// </summary>
        [DomName("paddingInlineEnd")]
        [DomName("padding-inline-end")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPaddingInlineEnd(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.PaddingInlineEnd, value);

        /// <summary>
        /// Gets the value of padding-inline-start.
        /// </summary>
        [DomName("paddingInlineStart")]
        [DomName("padding-inline-start")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPaddingInlineStart(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.PaddingInlineStart);

        /// <summary>
        /// Sets the value of padding-inline-start.
        /// </summary>
        [DomName("paddingInlineStart")]
        [DomName("padding-inline-start")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPaddingInlineStart(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.PaddingInlineStart, value);

        /// <summary>
        /// Gets the value of place-content.
        /// </summary>
        [DomName("placeContent")]
        [DomName("place-content")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPlaceContent(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.PlaceContent);

        /// <summary>
        /// Sets the value of place-content.
        /// </summary>
        [DomName("placeContent")]
        [DomName("place-content")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPlaceContent(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.PlaceContent, value);

        /// <summary>
        /// Gets the value of place-items.
        /// </summary>
        [DomName("placeItems")]
        [DomName("place-items")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPlaceItems(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.PlaceItems);

        /// <summary>
        /// Sets the value of place-items.
        /// </summary>
        [DomName("placeItems")]
        [DomName("place-items")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPlaceItems(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.PlaceItems, value);

        /// <summary>
        /// Gets the value of place-self.
        /// </summary>
        [DomName("placeSelf")]
        [DomName("place-self")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPlaceSelf(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.PlaceSelf);

        /// <summary>
        /// Sets the value of place-self.
        /// </summary>
        [DomName("placeSelf")]
        [DomName("place-self")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPlaceSelf(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.PlaceSelf, value);

        /// <summary>
        /// Gets the value of print-color-adjust.
        /// </summary>
        [DomName("printColorAdjust")]
        [DomName("print-color-adjust")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPrintColorAdjust(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.PrintColorAdjust);

        /// <summary>
        /// Sets the value of print-color-adjust.
        /// </summary>
        [DomName("printColorAdjust")]
        [DomName("print-color-adjust")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPrintColorAdjust(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.PrintColorAdjust, value);

        /// <summary>
        /// Gets the value of resize.
        /// </summary>
        [DomName("resize")]
        [DomAccessor(Accessors.Getter)]
        public static String GetResize(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Resize);

        /// <summary>
        /// Sets the value of resize.
        /// </summary>
        [DomName("resize")]
        [DomAccessor(Accessors.Setter)]
        public static void SetResize(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Resize, value);

        /// <summary>
        /// Gets the value of rotate.
        /// </summary>
        [DomName("rotate")]
        [DomAccessor(Accessors.Getter)]
        public static String GetRotate(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Rotate);

        /// <summary>
        /// Sets the value of rotate.
        /// </summary>
        [DomName("rotate")]
        [DomAccessor(Accessors.Setter)]
        public static void SetRotate(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Rotate, value);

        /// <summary>
        /// Gets the value of scale.
        /// </summary>
        [DomName("scale")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScale(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Scale);

        /// <summary>
        /// Sets the value of scale.
        /// </summary>
        [DomName("scale")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScale(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Scale, value);

        /// <summary>
        /// Gets the value of scrollbar-width.
        /// </summary>
        [DomName("scrollbarWidth")]
        [DomName("scrollbar-width")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollbarWidth(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollbarWidth);

        /// <summary>
        /// Sets the value of scrollbar-width.
        /// </summary>
        [DomName("scrollbarWidth")]
        [DomName("scrollbar-width")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollbarWidth(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollbarWidth, value);

        /// <summary>
        /// Gets the value of scrollbar-color.
        /// </summary>
        [DomName("scrollbarColor")]
        [DomName("scrollbar-color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollbarColor(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollbarColor);

        /// <summary>
        /// Sets the value of scrollbar-color.
        /// </summary>
        [DomName("scrollbarColor")]
        [DomName("scrollbar-color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollbarColor(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollbarColor, value);

        /// <summary>
        /// Gets the value of scrollbar-gutter.
        /// </summary>
        [DomName("scrollbarGutter")]
        [DomName("scrollbar-gutter")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollbarGutter(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollbarGutter);

        /// <summary>
        /// Sets the value of scrollbar-gutter.
        /// </summary>
        [DomName("scrollbarGutter")]
        [DomName("scrollbar-gutter")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollbarGutter(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollbarGutter, value);

        /// <summary>
        /// Gets the value of scroll-behavior.
        /// </summary>
        [DomName("scrollBehavior")]
        [DomName("scroll-behavior")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollBehavior(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollBehavior);

        /// <summary>
        /// Sets the value of scroll-behavior.
        /// </summary>
        [DomName("scrollBehavior")]
        [DomName("scroll-behavior")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollBehavior(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollBehavior, value);

        /// <summary>
        /// Gets the value of scroll-snap-stop.
        /// </summary>
        [DomName("scrollSnapStop")]
        [DomName("scroll-snap-stop")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollSnapStop(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollSnapStop);

        /// <summary>
        /// Sets the value of scroll-snap-stop.
        /// </summary>
        [DomName("scrollSnapStop")]
        [DomName("scroll-snap-stop")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollSnapStop(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollSnapStop, value);

        /// <summary>
        /// Gets the value of scroll-snap-type.
        /// </summary>
        [DomName("scrollSnapType")]
        [DomName("scroll-snap-type")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollSnapType(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollSnapType);

        /// <summary>
        /// Sets the value of scroll-snap-type.
        /// </summary>
        [DomName("scrollSnapType")]
        [DomName("scroll-snap-type")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollSnapType(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollSnapType, value);

        /// <summary>
        /// Gets the value of scroll-margin.
        /// </summary>
        [DomName("scrollMargin")]
        [DomName("scroll-margin")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollMargin(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollMargin);

        /// <summary>
        /// Sets the value of scroll-margin.
        /// </summary>
        [DomName("scrollMargin")]
        [DomName("scroll-margin")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollMargin(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollMargin, value);

        /// <summary>
        /// Gets the value of scroll-margin-top.
        /// </summary>
        [DomName("scrollMarginTop")]
        [DomName("scroll-margin-top")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollMarginTop(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollMarginTop);

        /// <summary>
        /// Sets the value of scroll-margin-top.
        /// </summary>
        [DomName("scrollMarginTop")]
        [DomName("scroll-margin-top")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollMarginTop(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollMarginTop, value);

        /// <summary>
        /// Gets the value of scroll-margin-right.
        /// </summary>
        [DomName("scrollMarginRight")]
        [DomName("scroll-margin-right")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollMarginRight(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollMarginRight);

        /// <summary>
        /// Sets the value of scroll-margin-right.
        /// </summary>
        [DomName("scrollMarginRight")]
        [DomName("scroll-margin-right")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollMarginRight(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollMarginRight, value);

        /// <summary>
        /// Gets the value of scroll-margin-bottom.
        /// </summary>
        [DomName("scrollMarginBottom")]
        [DomName("scroll-margin-bottom")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollMarginBottom(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollMarginBottom);

        /// <summary>
        /// Sets the value of scroll-margin-bottom.
        /// </summary>
        [DomName("scrollMarginBottom")]
        [DomName("scroll-margin-bottom")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollMarginBottom(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollMarginBottom, value);

        /// <summary>
        /// Gets the value of scroll-margin-left.
        /// </summary>
        [DomName("scrollMarginLeft")]
        [DomName("scroll-margin-left")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollMarginLeft(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollMarginLeft);

        /// <summary>
        /// Sets the value of scroll-margin-left.
        /// </summary>
        [DomName("scrollMarginLeft")]
        [DomName("scroll-margin-left")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollMarginLeft(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollMarginLeft, value);

        /// <summary>
        /// Gets the value of scroll-margin-block.
        /// </summary>
        [DomName("scrollMarginBlock")]
        [DomName("scroll-margin-block")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollMarginBlock(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollMarginBlock);

        /// <summary>
        /// Sets the value of scroll-margin-block.
        /// </summary>
        [DomName("scrollMarginBlock")]
        [DomName("scroll-margin-block")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollMarginBlock(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollMarginBlock, value);

        /// <summary>
        /// Gets the value of scroll-margin-block-start.
        /// </summary>
        [DomName("scrollMarginBlockStart")]
        [DomName("scroll-margin-block-start")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollMarginBlockStart(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollMarginBlockStart);

        /// <summary>
        /// Sets the value of scroll-margin-block-start.
        /// </summary>
        [DomName("scrollMarginBlockStart")]
        [DomName("scroll-margin-block-start")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollMarginBlockStart(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollMarginBlockStart, value);

        /// <summary>
        /// Gets the value of scroll-margin-block-end.
        /// </summary>
        [DomName("scrollMarginBlockEnd")]
        [DomName("scroll-margin-block-end")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollMarginBlockEnd(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollMarginBlockEnd);

        /// <summary>
        /// Sets the value of scroll-margin-block-end.
        /// </summary>
        [DomName("scrollMarginBlockEnd")]
        [DomName("scroll-margin-block-end")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollMarginBlockEnd(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollMarginBlockEnd, value);

        /// <summary>
        /// Gets the value of scroll-margin-inline.
        /// </summary>
        [DomName("scrollMarginInline")]
        [DomName("scroll-margin-inline")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollMarginInline(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollMarginInline);

        /// <summary>
        /// Sets the value of scroll-margin-inline.
        /// </summary>
        [DomName("scrollMarginInline")]
        [DomName("scroll-margin-inline")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollMarginInline(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollMarginInline, value);

        /// <summary>
        /// Gets the value of scroll-margin-inline-start.
        /// </summary>
        [DomName("scrollMarginInlineStart")]
        [DomName("scroll-margin-inline-start")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollMarginInlineStart(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollMarginInlineStart);

        /// <summary>
        /// Sets the value of scroll-margin-inline-start.
        /// </summary>
        [DomName("scrollMarginInlineStart")]
        [DomName("scroll-margin-inline-start")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollMarginInlineStart(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollMarginInlineStart, value);

        /// <summary>
        /// Gets the value of scroll-margin-inline-end.
        /// </summary>
        [DomName("scrollMarginInlineEnd")]
        [DomName("scroll-margin-inline-end")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollMarginInlineEnd(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollMarginInlineEnd);

        /// <summary>
        /// Sets the value of scroll-margin-inline-end.
        /// </summary>
        [DomName("scrollMarginInlineEnd")]
        [DomName("scroll-margin-inline-end")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollMarginInlineEnd(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollMarginInlineEnd, value);

        /// <summary>
        /// Gets the value of shape-outside.
        /// </summary>
        [DomName("shapeOutside")]
        [DomName("shape-outside")]
        [DomAccessor(Accessors.Getter)]
        public static String GetShapeOutside(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ShapeOutside);

        /// <summary>
        /// Sets the value of shape-outside.
        /// </summary>
        [DomName("shapeOutside")]
        [DomName("shape-outside")]
        [DomAccessor(Accessors.Setter)]
        public static void SetShapeOutside(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ShapeOutside, value);

        /// <summary>
        /// Gets the value of shape-margin.
        /// </summary>
        [DomName("shapeMargin")]
        [DomName("shape-margin")]
        [DomAccessor(Accessors.Getter)]
        public static String GetShapeMargin(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ShapeMargin);

        /// <summary>
        /// Sets the value of shape-margin.
        /// </summary>
        [DomName("shapeMargin")]
        [DomName("shape-margin")]
        [DomAccessor(Accessors.Setter)]
        public static void SetShapeMargin(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ShapeMargin, value);

        /// <summary>
        /// Gets the value of shape-image-threshold.
        /// </summary>
        [DomName("shapeImageThreshold")]
        [DomName("shape-image-threshold")]
        [DomAccessor(Accessors.Getter)]
        public static String GetShapeImageThreshold(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ShapeImageThreshold);

        /// <summary>
        /// Sets the value of shape-image-threshold.
        /// </summary>
        [DomName("shapeImageThreshold")]
        [DomName("shape-image-threshold")]
        [DomAccessor(Accessors.Setter)]
        public static void SetShapeImageThreshold(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ShapeImageThreshold, value);

        /// <summary>
        /// Gets the value of shape-rendering.
        /// </summary>
        [DomName("shapeRendering")]
        [DomName("shape-rendering")]
        [DomAccessor(Accessors.Getter)]
        public static String GetShapeRendering(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ShapeRendering);

        /// <summary>
        /// Sets the value of shape-rendering.
        /// </summary>
        [DomName("shapeRendering")]
        [DomName("shape-rendering")]
        [DomAccessor(Accessors.Setter)]
        public static void SetShapeRendering(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ShapeRendering, value);

        /// <summary>
        /// Gets the value of scroll-padding.
        /// </summary>
        [DomName("scrollPadding")]
        [DomName("scroll-padding")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollPadding(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollPadding);

        /// <summary>
        /// Sets the value of scroll-padding.
        /// </summary>
        [DomName("scrollPadding")]
        [DomName("scroll-padding")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollPadding(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollPadding, value);

        /// <summary>
        /// Gets the value of scroll-padding-top.
        /// </summary>
        [DomName("scrollPaddingTop")]
        [DomName("scroll-padding-top")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollPaddingTop(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollPaddingTop);

        /// <summary>
        /// Sets the value of scroll-padding-top.
        /// </summary>
        [DomName("scrollPaddingTop")]
        [DomName("scroll-padding-top")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollPaddingTop(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollPaddingTop, value);

        /// <summary>
        /// Gets the value of scroll-padding-right.
        /// </summary>
        [DomName("scrollPaddingRight")]
        [DomName("scroll-padding-right")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollPaddingRight(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollPaddingRight);

        /// <summary>
        /// Sets the value of scroll-padding-right.
        /// </summary>
        [DomName("scrollPaddingRight")]
        [DomName("scroll-padding-right")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollPaddingRight(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollPaddingRight, value);

        /// <summary>
        /// Gets the value of scroll-padding-bottom.
        /// </summary>
        [DomName("scrollPaddingBottom")]
        [DomName("scroll-padding-bottom")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollPaddingBottom(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollPaddingBottom);

        /// <summary>
        /// Sets the value of scroll-padding-bottom.
        /// </summary>
        [DomName("scrollPaddingBottom")]
        [DomName("scroll-padding-bottom")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollPaddingBottom(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollPaddingBottom, value);

        /// <summary>
        /// Gets the value of scroll-padding-left.
        /// </summary>
        [DomName("scrollPaddingLeft")]
        [DomName("scroll-padding-left")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollPaddingLeft(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollPaddingLeft);

        /// <summary>
        /// Sets the value of scroll-padding-left.
        /// </summary>
        [DomName("scrollPaddingLeft")]
        [DomName("scroll-padding-left")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollPaddingLeft(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollPaddingLeft, value);

        /// <summary>
        /// Gets the value of scroll-padding-block.
        /// </summary>
        [DomName("scrollPaddingBlock")]
        [DomName("scroll-padding-block")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollPaddingBlock(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollPaddingBlock);

        /// <summary>
        /// Sets the value of scroll-padding-block.
        /// </summary>
        [DomName("scrollPaddingBlock")]
        [DomName("scroll-padding-block")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollPaddingBlock(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollPaddingBlock, value);

        /// <summary>
        /// Gets the value of scroll-padding-block-start.
        /// </summary>
        [DomName("scrollPaddingBlockStart")]
        [DomName("scroll-padding-block-start")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollPaddingBlockStart(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollPaddingBlockStart);

        /// <summary>
        /// Sets the value of scroll-padding-block-start.
        /// </summary>
        [DomName("scrollPaddingBlockStart")]
        [DomName("scroll-padding-block-start")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollPaddingBlockStart(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollPaddingBlockStart, value);

        /// <summary>
        /// Gets the value of scroll-padding-block-end.
        /// </summary>
        [DomName("scrollPaddingBlockEnd")]
        [DomName("scroll-padding-block-end")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollPaddingBlockEnd(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollPaddingBlockEnd);

        /// <summary>
        /// Sets the value of scroll-padding-block-end.
        /// </summary>
        [DomName("scrollPaddingBlockEnd")]
        [DomName("scroll-padding-block-end")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollPaddingBlockEnd(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollPaddingBlockEnd, value);

        /// <summary>
        /// Gets the value of scroll-padding-inline.
        /// </summary>
        [DomName("scrollPaddingInline")]
        [DomName("scroll-padding-inline")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollPaddingInline(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollPaddingInline);

        /// <summary>
        /// Sets the value of scroll-padding-inline.
        /// </summary>
        [DomName("scrollPaddingInline")]
        [DomName("scroll-padding-inline")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollPaddingInline(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollPaddingInline, value);

        /// <summary>
        /// Gets the value of scroll-padding-inline-start.
        /// </summary>
        [DomName("scrollPaddingInlineStart")]
        [DomName("scroll-padding-inline-start")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollPaddingInlineStart(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollPaddingInlineStart);

        /// <summary>
        /// Sets the value of scroll-padding-inline-start.
        /// </summary>
        [DomName("scrollPaddingInlineStart")]
        [DomName("scroll-padding-inline-start")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollPaddingInlineStart(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollPaddingInlineStart, value);

        /// <summary>
        /// Gets the value of scroll-padding-inline-end.
        /// </summary>
        [DomName("scrollPaddingInlineEnd")]
        [DomName("scroll-padding-inline-end")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollPaddingInlineEnd(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollPaddingInlineEnd);

        /// <summary>
        /// Sets the value of scroll-padding-inline-end.
        /// </summary>
        [DomName("scrollPaddingInlineEnd")]
        [DomName("scroll-padding-inline-end")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollPaddingInlineEnd(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollPaddingInlineEnd, value);

        /// <summary>
        /// Gets the value of overscroll-behavior.
        /// </summary>
        [DomName("overscrollBehavior")]
        [DomName("overscroll-behavior")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOverscrollBehavior(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.OverscrollBehavior);

        /// <summary>
        /// Sets the value of overscroll-behavior.
        /// </summary>
        [DomName("overscrollBehavior")]
        [DomName("overscroll-behavior")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOverscrollBehavior(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.OverscrollBehavior, value);

        /// <summary>
        /// Gets the value of overscroll-behavior-x.
        /// </summary>
        [DomName("overscrollBehaviorX")]
        [DomName("overscroll-behavior-x")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOverscrollBehaviorX(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.OverscrollBehaviorX);

        /// <summary>
        /// Sets the value of overscroll-behavior-x.
        /// </summary>
        [DomName("overscrollBehaviorX")]
        [DomName("overscroll-behavior-x")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOverscrollBehaviorX(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.OverscrollBehaviorX, value);

        /// <summary>
        /// Gets the value of overscroll-behavior-y.
        /// </summary>
        [DomName("overscrollBehaviorY")]
        [DomName("overscroll-behavior-y")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOverscrollBehaviorY(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.OverscrollBehaviorY);

        /// <summary>
        /// Sets the value of overscroll-behavior-y.
        /// </summary>
        [DomName("overscrollBehaviorY")]
        [DomName("overscroll-behavior-y")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOverscrollBehaviorY(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.OverscrollBehaviorY, value);

        /// <summary>
        /// Gets the value of overscroll-behavior-block.
        /// </summary>
        [DomName("overscrollBehaviorBlock")]
        [DomName("overscroll-behavior-block")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOverscrollBehaviorBlock(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.OverscrollBehaviorBlock);

        /// <summary>
        /// Sets the value of overscroll-behavior-block.
        /// </summary>
        [DomName("overscrollBehaviorBlock")]
        [DomName("overscroll-behavior-block")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOverscrollBehaviorBlock(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.OverscrollBehaviorBlock, value);

        /// <summary>
        /// Gets the value of overscroll-behavior-inline.
        /// </summary>
        [DomName("overscrollBehaviorInline")]
        [DomName("overscroll-behavior-inline")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOverscrollBehaviorInline(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.OverscrollBehaviorInline);

        /// <summary>
        /// Sets the value of overscroll-behavior-inline.
        /// </summary>
        [DomName("overscrollBehaviorInline")]
        [DomName("overscroll-behavior-inline")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOverscrollBehaviorInline(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.OverscrollBehaviorInline, value);

        /// <summary>
        /// Gets the value of scroll-snap-align.
        /// </summary>
        [DomName("scrollSnapAlign")]
        [DomName("scroll-snap-align")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollSnapAlign(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ScrollSnapAlign);

        /// <summary>
        /// Sets the value of scroll-snap-align.
        /// </summary>
        [DomName("scrollSnapAlign")]
        [DomName("scroll-snap-align")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollSnapAlign(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ScrollSnapAlign, value);

        /// <summary>
        /// Gets the value of tab-size.
        /// </summary>
        [DomName("tabSize")]
        [DomName("tab-size")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTabSize(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TabSize);

        /// <summary>
        /// Sets the value of tab-size.
        /// </summary>
        [DomName("tabSize")]
        [DomName("tab-size")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTabSize(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TabSize, value);

        /// <summary>
        /// Gets the value of text-decoration-skip-ink.
        /// </summary>
        [DomName("textDecorationSkipInk")]
        [DomName("text-decoration-skip-ink")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextDecorationSkipInk(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TextDecorationSkipInk);

        /// <summary>
        /// Sets the value of text-decoration-skip-ink.
        /// </summary>
        [DomName("textDecorationSkipInk")]
        [DomName("text-decoration-skip-ink")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextDecorationSkipInk(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TextDecorationSkipInk, value);

        /// <summary>
        /// Gets the value of text-decoration-thickness.
        /// </summary>
        [DomName("textDecorationThickness")]
        [DomName("text-decoration-thickness")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextDecorationThickness(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TextDecorationThickness);

        /// <summary>
        /// Sets the value of text-decoration-thickness.
        /// </summary>
        [DomName("textDecorationThickness")]
        [DomName("text-decoration-thickness")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextDecorationThickness(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TextDecorationThickness, value);

        /// <summary>
        /// Gets the value of text-orientation.
        /// </summary>
        [DomName("textOrientation")]
        [DomName("text-orientation")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextOrientation(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TextOrientation);

        /// <summary>
        /// Sets the value of text-orientation.
        /// </summary>
        [DomName("textOrientation")]
        [DomName("text-orientation")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextOrientation(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TextOrientation, value);

        /// <summary>
        /// Gets the value of text-underline-offset.
        /// </summary>
        [DomName("textUnderlineOffset")]
        [DomName("text-underline-offset")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextUnderlineOffset(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TextUnderlineOffset);

        /// <summary>
        /// Sets the value of text-underline-offset.
        /// </summary>
        [DomName("textUnderlineOffset")]
        [DomName("text-underline-offset")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextUnderlineOffset(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TextUnderlineOffset, value);

        /// <summary>
        /// Gets the value of text-wrap.
        /// </summary>
        [DomName("textWrap")]
        [DomName("text-wrap")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextWrap(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TextWrap);

        /// <summary>
        /// Sets the value of text-wrap.
        /// </summary>
        [DomName("textWrap")]
        [DomName("text-wrap")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextWrap(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TextWrap, value);

        /// <summary>
        /// Gets the value of text-wrap-mode.
        /// </summary>
        [DomName("textWrapMode")]
        [DomName("text-wrap-mode")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextWrapMode(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TextWrapMode);

        /// <summary>
        /// Sets the value of text-wrap-mode.
        /// </summary>
        [DomName("textWrapMode")]
        [DomName("text-wrap-mode")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextWrapMode(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TextWrapMode, value);

        /// <summary>
        /// Gets the value of text-wrap-style.
        /// </summary>
        [DomName("textWrapStyle")]
        [DomName("text-wrap-style")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextWrapStyle(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TextWrapStyle);

        /// <summary>
        /// Sets the value of text-wrap-style.
        /// </summary>
        [DomName("textWrapStyle")]
        [DomName("text-wrap-style")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextWrapStyle(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TextWrapStyle, value);

        /// <summary>
        /// Gets the value of transform-box.
        /// </summary>
        [DomName("transformBox")]
        [DomName("transform-box")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTransformBox(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TransformBox);

        /// <summary>
        /// Sets the value of transform-box.
        /// </summary>
        [DomName("transformBox")]
        [DomName("transform-box")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTransformBox(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TransformBox, value);

        /// <summary>
        /// Gets the value of translate.
        /// </summary>
        [DomName("translate")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTranslate(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.Translate);

        /// <summary>
        /// Sets the value of translate.
        /// </summary>
        [DomName("translate")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTranslate(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.Translate, value);

        /// <summary>
        /// Gets the value of transition-behavior.
        /// </summary>
        [DomName("transitionBehavior")]
        [DomName("transition-behavior")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTransitionBehavior(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TransitionBehavior);

        /// <summary>
        /// Sets the value of transition-behavior.
        /// </summary>
        [DomName("transitionBehavior")]
        [DomName("transition-behavior")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTransitionBehavior(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TransitionBehavior, value);

        /// <summary>
        /// Gets the value of touch-action.
        /// </summary>
        [DomName("touchAction")]
        [DomName("touch-action")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTouchAction(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.TouchAction);

        /// <summary>
        /// Sets the value of touch-action.
        /// </summary>
        [DomName("touchAction")]
        [DomName("touch-action")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTouchAction(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.TouchAction, value);

        /// <summary>
        /// Gets the value of user-select.
        /// </summary>
        [DomName("userSelect")]
        [DomName("user-select")]
        [DomAccessor(Accessors.Getter)]
        public static String GetUserSelect(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.UserSelect);

        /// <summary>
        /// Sets the value of user-select.
        /// </summary>
        [DomName("userSelect")]
        [DomName("user-select")]
        [DomAccessor(Accessors.Setter)]
        public static void SetUserSelect(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.UserSelect, value);

        /// <summary>
        /// Gets the value of white-space-collapse.
        /// </summary>
        [DomName("whiteSpaceCollapse")]
        [DomName("white-space-collapse")]
        [DomAccessor(Accessors.Getter)]
        public static String GetWhiteSpaceCollapse(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.WhiteSpaceCollapse);

        /// <summary>
        /// Sets the value of white-space-collapse.
        /// </summary>
        [DomName("whiteSpaceCollapse")]
        [DomName("white-space-collapse")]
        [DomAccessor(Accessors.Setter)]
        public static void SetWhiteSpaceCollapse(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.WhiteSpaceCollapse, value);

        /// <summary>
        /// Gets the value of will-change.
        /// </summary>
        [DomName("willChange")]
        [DomName("will-change")]
        [DomAccessor(Accessors.Getter)]
        public static String GetWillChange(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.WillChange);

        /// <summary>
        /// Sets the value of will-change.
        /// </summary>
        [DomName("willChange")]
        [DomName("will-change")]
        [DomAccessor(Accessors.Setter)]
        public static void SetWillChange(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.WillChange, value);

        /// <summary>
        /// Gets the value of object-fit.
        /// </summary>
        [DomName("objectFit")]
        [DomName("object-fit")]
        [DomAccessor(Accessors.Getter)]
        public static String GetObjectFit(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ObjectFit);

        /// <summary>
        /// Sets the value of object-fit.
        /// </summary>
        [DomName("objectFit")]
        [DomName("object-fit")]
        [DomAccessor(Accessors.Setter)]
        public static void SetObjectFit(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ObjectFit, value);

        /// <summary>
        /// Gets the value of object-position.
        /// </summary>
        [DomName("objectPosition")]
        [DomName("object-position")]
        [DomAccessor(Accessors.Getter)]
        public static String GetObjectPosition(this ICssStyleDeclaration style) =>
            style.GetPropertyValue(PropertyNames.ObjectPosition);

        /// <summary>
        /// Sets the value of object-position.
        /// </summary>
        [DomName("objectPosition")]
        [DomName("object-position")]
        [DomAccessor(Accessors.Setter)]
        public static void SetObjectPosition(this ICssStyleDeclaration style, String value) =>
            style.SetProperty(PropertyNames.ObjectPosition, value);
    }
}
