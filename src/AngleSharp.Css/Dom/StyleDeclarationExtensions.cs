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
        [DomAccessor(Accessors.Getter)]
        public static String GetAlignContent(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.AlignContent);
        }

        /// <summary>
        /// Sets how a flex item's lines align within the flex
        /// container when there is extra space along the axis that is
        /// perpendicular to the axis defined by the flex-direction property.
        /// </summary>
        [DomName("alignContent")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAlignContent(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.AlignContent, value);
        }

        /// <summary>
        /// Gets the alignment value (perpendicular to the layout axis
        /// defined by the flex-direction property) of flex items of the flex
        /// container.
        /// </summary>
        [DomName("alignItems")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAlignItems(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.AlignItems);
        }

        /// <summary>
        /// Sets the alignment value (perpendicular to the layout axis
        /// defined by the flex-direction property) of flex items of the flex
        /// container.
        /// </summary>
        [DomName("alignItems")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAlignItems(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.AlignItems, value);
        }

        /// <summary>
        /// Gets the alignment value (perpendicular to the layout axis
        /// defined by the flex-direction property) of flex items of the flex
        /// container.
        /// </summary>
        [DomName("alignSelf")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAlignSelf(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.AlignSelf);
        }

        /// <summary>
        /// Sets the alignment value (perpendicular to the layout axis
        /// defined by the flex-direction property) of flex items of the flex
        /// container.
        /// </summary>
        [DomName("alignSelf")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAlignSelf(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.AlignSelf, value);
        }

        /// <summary>
        /// Gets a string that indicates whether the object represents
        /// a keyboard shortcut.
        /// </summary>
        [DomName("accelerator")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAccelerator(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Accelerator);
        }

        /// <summary>
        /// Sets a string that indicates whether the object represents
        /// a keyboard shortcut.
        /// </summary>
        [DomName("accelerator")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAccelerator(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Accelerator, value);
        }

        /// <summary>
        /// Gets which baseline of this element is to be aligned with
        /// the corresponding baseline of the parent.
        /// </summary>
        [DomName("alignmentBaseline")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAlignmentBaseline(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.AlignBaseline);
        }

        /// <summary>
        /// Sets which baseline of this element is to be aligned with
        /// the corresponding baseline of the parent.
        /// </summary>
        [DomName("alignmentBaseline")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAlignmentBaseline(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.AlignBaseline, value);
        }

        /// <summary>
        /// Gets one or more shorthand values that define all animation
        /// properties (except animation-play-state) for a set of corresponding
        /// object properties identified in the CSS @keyframes at-rule
        /// specified by the animations-name property.
        /// </summary>
        [DomName("animation")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAnimation(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Animation);
        }

        /// <summary>
        /// Sets one or more shorthand values that define all animation
        /// properties (except animation-play-state) for a set of corresponding
        /// object properties identified in the CSS @keyframes at-rule
        /// specified by the animations-name property.
        /// </summary>
        [DomName("animation")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAnimation(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Animation, value);
        }

        /// <summary>
        /// Gets the offset within an animation cycle (the amount of
        /// time from the start of a cycle) before the animation is displayed
        /// for a set of corresponding object properties identified in the CSS
        /// @keyframes at-rule specified by the animation-name property.
        /// </summary>
        [DomName("animationDelay")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAnimationDelay(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.AnimationDelay);
        }

        /// <summary>
        /// Sets the offset within an animation cycle (the amount of
        /// time from the start of a cycle) before the animation is displayed
        /// for a set of corresponding object properties identified in the CSS
        /// @keyframes at-rule specified by the animation-name property.
        /// </summary>
        [DomName("animationDelay")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAnimationDelay(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.AnimationDelay, value);
        }

        /// <summary>
        /// Gets the direction of play for an animation cycle.
        /// </summary>
        [DomName("animationDirection")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAnimationDirection(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.AnimationDirection);
        }

        /// <summary>
        /// Sets the direction of play for an animation cycle.
        /// </summary>
        [DomName("animationDirection")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAnimationDirection(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.AnimationDirection, value);
        }

        /// <summary>
        /// Gets the length of time to complete one cycle of the
        /// animation.
        /// </summary>
        [DomName("animationDuration")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAnimationDuration(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.AnimationDuration);
        }

        /// <summary>
        /// Sets the length of time to complete one cycle of the
        /// animation.
        /// </summary>
        [DomName("animationDuration")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAnimationDuration(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.AnimationDuration, value);
        }

        /// <summary>
        /// Gets whether the effects of an animation are visible before
        /// or after it plays.
        /// </summary>
        [DomName("animationFillMode")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAnimationFillMode(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.AnimationFillMode);
        }

        /// <summary>
        /// Sets whether the effects of an animation are visible before
        /// or after it plays.
        /// </summary>
        [DomName("animationFillMode")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAnimationFillMode(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.AnimationFillMode, value);
        }

        /// <summary>
        /// Gets the number of times an animation cycle is played.
        /// </summary>
        [DomName("animationIterationCount")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAnimationIterationCount(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.AnimationIterationCount);
        }

        /// <summary>
        /// Sets the number of times an animation cycle is played.
        /// </summary>
        [DomName("animationIterationCount")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAnimationIterationCount(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.AnimationIterationCount, value);
        }

        /// <summary>
        /// Gets one or more animation names. An animation name selects
        /// a CSS @keyframes at-rule.
        /// </summary>
        [DomName("animationName")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAnimationName(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.AnimationName);
        }

        /// <summary>
        /// Sets one or more animation names. An animation name selects
        /// a CSS @keyframes at-rule.
        /// </summary>
        [DomName("animationName")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAnimationName(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.AnimationName, value);
        }

        /// <summary>
        /// Gets whether an animation is playing or paused.
        /// </summary>
        [DomName("animationPlayState")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAnimationPlayState(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.AnimationPlayState);
        }

        /// <summary>
        /// Sets whether an animation is playing or paused.
        /// </summary>
        [DomName("animationPlayState")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAnimationPlayState(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.AnimationPlayState, value);
        }

        /// <summary>
        /// Gets the intermediate property values to be used during a
        /// single cycle of an animation on a set of corresponding object
        /// properties identified in the CSS @keyframes at-rule specified by
        /// the animation-name property.
        /// </summary>
        [DomName("animationTimingFunction")]
        [DomAccessor(Accessors.Getter)]
        public static String GetAnimationTimingFunction(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.AnimationTimingFunction);
        }

        /// <summary>
        /// Sets the intermediate property values to be used during a
        /// single cycle of an animation on a set of corresponding object
        /// properties identified in the CSS @keyframes at-rule specified by
        /// the animation-name property.
        /// </summary>
        [DomName("animationTimingFunction")]
        [DomAccessor(Accessors.Setter)]
        public static void SetAnimationTimingFunction(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.AnimationTimingFunction, value);
        }

        /// <summary>
        /// Gets a value that specifies whether the back face (reverse
        /// side) of an object is visible.
        /// </summary>
        [DomName("backfaceVisibility")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackfaceVisibility(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BackfaceVisibility);
        }

        /// <summary>
        /// Sets a value that specifies whether the back face (reverse
        /// side) of an object is visible.
        /// </summary>
        [DomName("backfaceVisibility")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackfaceVisibility(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BackfaceVisibility, value);
        }

        /// <summary>
        /// Gets up to five separate background properties of an object.
        /// </summary>
        [DomName("background")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackground(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Background);
        }

        /// <summary>
        /// Sets up to five separate background properties of an object.
        /// </summary>
        [DomName("background")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackground(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Background, value);
        }

        /// <summary>
        /// Gets how the background image (or images) is attached to
        /// the object within the document.
        /// </summary>
        [DomName("backgroundAttachment")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackgroundAttachment(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BackgroundAttachment);
        }

        /// <summary>
        /// Sets how the background image (or images) is attached to
        /// the object within the document.
        /// </summary>
        [DomName("backgroundAttachment")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackgroundAttachment(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BackgroundAttachment, value);
        }

        /// <summary>
        /// Gets the background painting area or areas relative to the
        /// element's bounding boxes.
        /// </summary>
        [DomName("backgroundClip")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackgroundClip(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BackgroundClip);
        }

        /// <summary>
        /// Sets the background painting area or areas relative to the
        /// element's bounding boxes.
        /// </summary>
        [DomName("backgroundClip")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackgroundClip(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BackgroundClip, value);
        }

        /// <summary>
        /// Gets the color behind the content of the object.
        /// </summary>
        [DomName("backgroundColor")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackgroundColor(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BackgroundColor);
        }

        /// <summary>
        /// Sets the color behind the content of the object.
        /// </summary>
        [DomName("backgroundColor")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackgroundColor(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BackgroundColor, value);
        }

        /// <summary>
        /// Gets the background image or images of the object.
        /// </summary>
        [DomName("backgroundImage")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackgroundImage(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BackgroundImage);
        }

        /// <summary>
        /// Sets the background image or images of the object.
        /// </summary>
        [DomName("backgroundImage")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackgroundImage(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BackgroundImage, value);
        }

        /// <summary>
        /// Gets the positioning area of an element or multiple
        /// elements.
        /// </summary>
        [DomName("backgroundOrigin")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackgroundOrigin(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BackgroundOrigin);
        }

        /// <summary>
        /// Sets the positioning area of an element or multiple
        /// elements.
        /// </summary>
        [DomName("backgroundOrigin")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackgroundOrigin(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BackgroundOrigin, value);
        }

        /// <summary>
        /// Gets the position of the background of the object.
        /// </summary>
        [DomName("backgroundPosition")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackgroundPosition(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BackgroundPosition);
        }

        /// <summary>
        /// Sets the position of the background of the object.
        /// </summary>
        [DomName("backgroundPosition")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackgroundPosition(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BackgroundPosition, value);
        }

        /// <summary>
        /// Gets the x-coordinate of the background-position property.
        /// </summary>
        [DomName("backgroundPositionX")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackgroundPositionX(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BackgroundPositionX);
        }

        /// <summary>
        /// Sets the x-coordinate of the background-position property.
        /// </summary>
        [DomName("backgroundPositionX")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackgroundPositionX(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BackgroundPositionX, value);
        }

        /// <summary>
        /// Gets the y-coordinate of the background-position property.
        /// </summary>
        [DomName("backgroundPositionY")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackgroundPositionY(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BackgroundPositionY);
        }

        /// <summary>
        /// Sets the y-coordinate of the background-position property.
        /// </summary>
        [DomName("backgroundPositionY")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackgroundPositionY(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BackgroundPositionY, value);
        }

        /// <summary>
        /// Gets whether and how the background image (or images) is
        /// tiled.
        /// </summary>
        [DomName("backgroundRepeat")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackgroundRepeat(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BackgroundRepeat);
        }

        /// <summary>
        /// Sets whether and how the background image (or images) is
        /// tiled.
        /// </summary>
        [DomName("backgroundRepeat")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackgroundRepeat(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BackgroundRepeat, value);
        }

        /// <summary>
        /// Gets the size of the background images.
        /// </summary>
        [DomName("backgroundSize")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBackgroundSize(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BackgroundSize);
        }

        /// <summary>
        /// Sets the size of the background images.
        /// </summary>
        [DomName("backgroundSize")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBackgroundSize(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BackgroundSize, value);
        }

        /// <summary>
        /// Gets a value that indicates how the dominant baseline
        /// should be repositioned relative to the dominant baseline of the
        /// parent text content element.
        /// </summary>
        [DomName("baselineShift")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBaselineShift(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BaselineShift);
        }

        /// <summary>
        /// Sets a value that indicates how the dominant baseline
        /// should be repositioned relative to the dominant baseline of the
        /// parent text content element.
        /// </summary>
        [DomName("baselineShift")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBaselineShift(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BaselineShift, value);
        }

        /// <summary>
        /// Gets the location of the Dynamic HTML (DHTML) behavior 
        /// DHTML Behaviors.
        /// </summary>
        [DomName("behavior")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBehavior(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Behavior);
        }

        /// <summary>
        /// Sets the location of the Dynamic HTML (DHTML) behavior 
        /// DHTML Behaviors.
        /// </summary>
        [DomName("behavior")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBehavior(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Behavior, value);
        }

        /// <summary>
        /// Gets the position of the object relative to the top of
        /// the next positioned object in the document hierarchy.
        /// </summary>
        [DomName("bottom")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBottom(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Bottom);
        }

        /// <summary>
        /// Sets the position of the object relative to the top of
        /// the next positioned object in the document hierarchy.
        /// </summary>
        [DomName("bottom")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBottom(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Bottom, value);
        }

        /// <summary>
        /// Gets the properties of a border drawn around an object.
        /// </summary>
        [DomName("border")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorder(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Border);
        }

        /// <summary>
        /// Sets the properties of a border drawn around an object.
        /// </summary>
        [DomName("border")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorder(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Border, value);
        }

        /// <summary>
        /// Gets the properties of the bottom border of the object.
        /// </summary>
        [DomName("borderBottom")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderBottom(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderBottom);
        }

        /// <summary>
        /// Sets the properties of the bottom border of the object.
        /// </summary>
        [DomName("borderBottom")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderBottom(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderBottom, value);
        }

        /// <summary>
        /// Gets the foreground color of the bottom border of an
        /// object.
        /// </summary>
        [DomName("borderBottomColor")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderBottomColor(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderBottomColor);
        }

        /// <summary>
        /// Sets the foreground color of the bottom border of an
        /// object.
        /// </summary>
        [DomName("borderBottomColor")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderBottomColor(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderBottomColor, value);
        }

        /// <summary>
        /// Gets the radii of the quarter ellipse that defines the
        /// shape of the lower-left corner for the outer border edge of the
        /// current box.
        /// </summary>
        [DomName("borderBottomLeftRadius")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderBottomLeftRadius(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderBottomLeftRadius);
        }

        /// <summary>
        /// Sets the radii of the quarter ellipse that defines the
        /// shape of the lower-left corner for the outer border edge of the
        /// current box.
        /// </summary>
        [DomName("borderBottomLeftRadius")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderBottomLeftRadius(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderBottomLeftRadius, value);
        }

        /// <summary>
        /// Gets one or two values that define the radii of the quarter
        /// ellipse that defines the shape of the lower-right corner for the
        /// outer border edge of the current box.
        /// </summary>
        [DomName("borderBottomRightRadius")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderBottomRightRadius(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderBottomRightRadius);
        }

        /// <summary>
        /// Sets one or two values that define the radii of the quarter
        /// ellipse that defines the shape of the lower-right corner for the
        /// outer border edge of the current box.
        /// </summary>
        [DomName("borderBottomRightRadius")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderBottomRightRadius(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderBottomRightRadius, value);
        }

        /// <summary>
        /// Gets the style of the bottom border of the object.
        /// </summary>
        [DomName("borderBottomStyle")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderBottomStyle(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderBottomStyle);
        }

        /// <summary>
        /// Sets the style of the bottom border of the object.
        /// </summary>
        [DomName("borderBottomStyle")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderBottomStyle(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderBottomStyle, value);
        }

        /// <summary>
        /// Gets the thickness of the bottom border of the object.
        /// </summary>
        [DomName("borderBottomWidth")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderBottomWidth(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderBottomWidth);
        }

        /// <summary>
        /// Sets the thickness of the bottom border of the object.
        /// </summary>
        [DomName("borderBottomWidth")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderBottomWidth(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderBottomWidth, value);
        }

        /// <summary>
        /// Gets whether the row and cell borders of a table are joined
        /// in a single border or detached as in standard HTML.
        /// </summary>
        [DomName("borderCollapse")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderCollapse(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderCollapse);
        }

        /// <summary>
        /// Sets whether the row and cell borders of a table are joined
        /// in a single border or detached as in standard HTML.
        /// </summary>
        [DomName("borderCollapse")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderCollapse(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderCollapse, value);
        }

        /// <summary>
        /// Gets the border color of the object.
        /// </summary>
        [DomName("borderColor")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderColor(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderColor);
        }

        /// <summary>
        /// Sets the border color of the object.
        /// </summary>
        [DomName("borderColor")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderColor(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderColor, value);
        }

        /// <summary>
        /// Gets an image to be used in place of the border styles.
        /// </summary>
        [DomName("borderImage")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderImage(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderImage);
        }

        /// <summary>
        /// Sets an image to be used in place of the border styles.
        /// </summary>
        [DomName("borderImage")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderImage(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderImage, value);
        }

        /// <summary>
        /// Gets the amount by which the border image area extends
        /// beyond the border box.
        /// </summary>
        [DomName("borderImageOutset")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderImageOutset(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderImageOutset);
        }

        /// <summary>
        /// Sets the amount by which the border image area extends
        /// beyond the border box.
        /// </summary>
        [DomName("borderImageOutset")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderImageOutset(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderImageOutset, value);
        }

        /// <summary>
        /// Gets how the image is scaled and tiled.
        /// </summary>
        [DomName("borderImageRepeat")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderImageRepeat(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderImageRepeat);
        }

        /// <summary>
        /// Sets how the image is scaled and tiled.
        /// </summary>
        [DomName("borderImageRepeat")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderImageRepeat(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderImageRepeat, value);
        }

        /// <summary>
        /// Gets four inward offsets, this property slices the
        /// specified border image into a three by three grid: four corners,
        /// four edges, and a central region.
        /// </summary>
        [DomName("borderImageSlice")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderImageSlice(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderImageSlice);
        }

        /// <summary>
        /// Sets four inward offsets, this property slices the
        /// specified border image into a three by three grid: four corners,
        /// four edges, and a central region.
        /// </summary>
        [DomName("borderImageSlice")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderImageSlice(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderImageSlice, value);
        }

        /// <summary>
        /// Gets the path of the image to be used for the border.
        /// </summary>
        [DomName("borderImageSource")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderImageSource(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderImageSource);
        }

        /// <summary>
        /// Sets the path of the image to be used for the border.
        /// </summary>
        [DomName("borderImageSource")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderImageSource(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderImageSource, value);
        }

        /// <summary>
        /// Gets the inward offsets from the outer border edge.
        /// </summary>
        [DomName("borderImageWidth")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderImageWidth(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderImageWidth);
        }

        /// <summary>
        /// Sets the inward offsets from the outer border edge.
        /// </summary>
        [DomName("borderImageWidth")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderImageWidth(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderImageWidth, value);
        }

        /// <summary>
        /// Gets the properties of the left border of the object.
        /// </summary>
        [DomName("borderLeft")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderLeft(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderLeft);
        }

        /// <summary>
        /// Sets the properties of the left border of the object.
        /// </summary>
        [DomName("borderLeft")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderLeft(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderLeft, value);
        }

        /// <summary>
        /// Gets the foreground color of the left border of an object.
        /// </summary>
        [DomName("borderLeftColor")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderLeftColor(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderLeftColor);
        }

        /// <summary>
        /// Sets the foreground color of the left border of an object.
        /// </summary>
        [DomName("borderLeftColor")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderLeftColor(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderLeftColor, value);
        }

        /// <summary>
        /// Gets the style of the left border of the object.
        /// </summary>
        [DomName("borderLeftStyle")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderLeftStyle(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderLeftStyle);
        }

        /// <summary>
        /// Sets the style of the left border of the object.
        /// </summary>
        [DomName("borderLeftStyle")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderLeftStyle(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderLeftStyle, value);
        }

        /// <summary>
        /// Gets the thickness of the left border of the object.
        /// </summary>
        [DomName("borderLeftWidth")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderLeftWidth(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderLeftWidth);
        }

        /// <summary>
        /// Sets the thickness of the left border of the object.
        /// </summary>
        [DomName("borderLeftWidth")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderLeftWidth(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderLeftWidth, value);
        }

        /// <summary>
        /// Gets the radii of a quarter ellipse that defines the shape
        /// of the corners for the outer border edge of the current box.
        /// </summary>
        [DomName("borderRadius")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderRadius(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderRadius);
        }

        /// <summary>
        /// Sets the radii of a quarter ellipse that defines the shape
        /// of the corners for the outer border edge of the current box.
        /// </summary>
        [DomName("borderRadius")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderRadius(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderRadius, value);
        }

        /// <summary>
        /// Gets the properties of the right border of the object.
        /// </summary>
        [DomName("borderRight")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderRight(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderRight);
        }

        /// <summary>
        /// Sets the properties of the right border of the object.
        /// </summary>
        [DomName("borderRight")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderRight(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderRight, value);
        }

        /// <summary>
        /// Gets the foreground color of the right border of an object.
        /// </summary>
        [DomName("borderRightColor")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderRightColor(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderRightColor);
        }

        /// <summary>
        /// Sets the foreground color of the right border of an object.
        /// </summary>
        [DomName("borderRightColor")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderRightColor(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderRightColor, value);
        }

        /// <summary>
        /// Gets the style of the right border of the object.
        /// </summary>
        [DomName("borderRightStyle")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderRightStyle(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderRightStyle);
        }

        /// <summary>
        /// Sets the style of the right border of the object.
        /// </summary>
        [DomName("borderRightStyle")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderRightStyle(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderRightStyle, value);
        }

        /// <summary>
        /// Gets the thickness of the right border of the object.
        /// </summary>
        [DomName("borderRightWidth")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderRightWidth(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderRightWidth);
        }

        /// <summary>
        /// Sets the thickness of the right border of the object.
        /// </summary>
        [DomName("borderRightWidth")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderRightWidth(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderRightWidth, value);
        }

        /// <summary>
        /// Gets the distance between the borders of adjoining cells in
        /// a table.
        /// </summary>
        [DomName("borderSpacing")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderSpacing(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderSpacing);
        }

        /// <summary>
        /// Sets the distance between the borders of adjoining cells in
        /// a table.
        /// </summary>
        [DomName("borderSpacing")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderSpacing(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderSpacing, value);
        }

        /// <summary>
        /// Gets the style of the left, right, top, and bottom borders
        /// of the object.
        /// </summary>
        [DomName("borderStyle")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderStyle(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderStyle);
        }

        /// <summary>
        /// Sets the style of the left, right, top, and bottom borders
        /// of the object.
        /// </summary>
        [DomName("borderStyle")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderStyle(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderStyle, value);
        }

        /// <summary>
        /// Gets the properties of the top border of the object.
        /// </summary>
        [DomName("borderTop")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderTop(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderTop);
        }

        /// <summary>
        /// Sets the properties of the top border of the object.
        /// </summary>
        [DomName("borderTop")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderTop(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderTop, value);
        }

        /// <summary>
        /// Gets the foreground color of the top border of an object.
        /// </summary>
        [DomName("borderTopColor")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderTopColor(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderTopColor);
        }

        /// <summary>
        /// Sets the foreground color of the top border of an object.
        /// </summary>
        [DomName("borderTopColor")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderTopColor(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderTopColor, value);
        }

        /// <summary>
        /// Gets one or two values that define the radii of the
        /// quarter ellipse that defines the shape of the upper-left corner for
        /// the outer border edge of the current box.
        /// </summary>
        [DomName("borderTopLeftRadius")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderTopLeftRadius(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderTopLeftRadius);
        }

        /// <summary>
        /// Sets one or two values that define the radii of the
        /// quarter ellipse that defines the shape of the upper-left corner for
        /// the outer border edge of the current box.
        /// </summary>
        [DomName("borderTopLeftRadius")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderTopLeftRadius(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderTopLeftRadius, value);
        }

        /// <summary>
        /// Gets one or two values that define the radii of the quarter
        /// ellipse that defines the shape of the upper-right corner for the
        /// outer border edge of the current box.
        /// </summary>
        [DomName("borderTopRightRadius")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderTopRightRadius(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderTopRightRadius);
        }

        /// <summary>
        /// Sets one or two values that define the radii of the quarter
        /// ellipse that defines the shape of the upper-right corner for the
        /// outer border edge of the current box.
        /// </summary>
        [DomName("borderTopRightRadius")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderTopRightRadius(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderTopRightRadius, value);
        }

        /// <summary>
        /// Gets the style of the top border of the object.
        /// </summary>
        [DomName("borderTopStyle")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderTopStyle(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderTopStyle);
        }

        /// <summary>
        /// Sets the style of the top border of the object.
        /// </summary>
        [DomName("borderTopStyle")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderTopStyle(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderTopStyle, value);
        }

        /// <summary>
        /// Gets the thickness of the top border of the object.
        /// </summary>
        [DomName("borderTopWidth")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderTopWidth(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderTopWidth);
        }

        /// <summary>
        /// Sets the thickness of the top border of the object.
        /// </summary>
        [DomName("borderTopWidth")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderTopWidth(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderTopWidth, value);
        }

        /// <summary>
        /// Gets the thicknesses of the left, right, top, and bottom
        /// borders of an object.
        /// </summary>
        [DomName("borderWidth")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBorderWidth(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BorderWidth);
        }

        /// <summary>
        /// Sets the thicknesses of the left, right, top, and bottom
        /// borders of an object.
        /// </summary>
        [DomName("borderWidth")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBorderWidth(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BorderWidth, value);
        }

        /// <summary>
        /// Gets one or more set of shadow values that attaches one or
        /// more drop shadows to the current box.
        /// </summary>
        [DomName("boxShadow")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBoxShadow(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BoxShadow);
        }

        /// <summary>
        /// Sets one or more set of shadow values that attaches one or
        /// more drop shadows to the current box.
        /// </summary>
        [DomName("boxShadow")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBoxShadow(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BoxShadow, value);
        }

        /// <summary>
        /// Gets the box model to use for object sizing.
        /// </summary>
        [DomName("boxSizing")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBoxSizing(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BoxSizing);
        }

        /// <summary>
        /// Sets the box model to use for object sizing.
        /// </summary>
        [DomName("boxSizing")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBoxSizing(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BoxSizing, value);
        }

        /// <summary>
        /// Gets the column-break behavior that follows a content block
        /// in a multi-column element.
        /// </summary>
        [DomName("breakAfter")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBreakAfter(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BreakAfter);
        }

        /// <summary>
        /// Sets the column-break behavior that follows a content block
        /// in a multi-column element.
        /// </summary>
        [DomName("breakAfter")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBreakAfter(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BreakAfter, value);
        }

        /// <summary>
        /// Gets the column-break behavior that precedes a content
        /// block in a multi-column element.
        /// </summary>
        [DomName("breakBefore")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBreakBefore(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BreakBefore);
        }

        /// <summary>
        /// Sets the column-break behavior that precedes a content
        /// block in a multi-column element.
        /// </summary>
        [DomName("breakBefore")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBreakBefore(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BreakBefore, value);
        }

        /// <summary>
        /// Gets the column-break behavior that occurs within a
        /// content block in a multi-column element.
        /// </summary>
        [DomName("breakInside")]
        [DomAccessor(Accessors.Getter)]
        public static String GetBreakInside(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.BreakInside);
        }

        /// <summary>
        /// Sets the column-break behavior that occurs within a
        /// content block in a multi-column element.
        /// </summary>
        [DomName("breakInside")]
        [DomAccessor(Accessors.Setter)]
        public static void SetBreakInside(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.BreakInside, value);
        }

        /// <summary>
        /// Gets where the caption of a table is located.
        /// </summary>
        [DomName("captionSide")]
        [DomAccessor(Accessors.Getter)]
        public static String GetCaptionSide(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.CaptionSide);
        }

        /// <summary>
        /// Sets where the caption of a table is located.
        /// </summary>
        [DomName("captionSide")]
        [DomAccessor(Accessors.Setter)]
        public static void SetCaptionSide(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.CaptionSide, value);
        }

        /// <summary>
        /// Gets whether the object allows floating objects on its left
        /// side, right side, or both, so that the next text displays past the
        /// floating objects.
        /// </summary>
        [DomName("clear")]
        [DomAccessor(Accessors.Getter)]
        public static String GetClear(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Clear);
        }

        /// <summary>
        /// Sets whether the object allows floating objects on its left
        /// side, right side, or both, so that the next text displays past the
        /// floating objects.
        /// </summary>
        [DomName("clear")]
        [DomAccessor(Accessors.Setter)]
        public static void SetClear(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Clear, value);
        }

        /// <summary>
        /// Gets which part of a positioned object is visible.
        /// </summary>
        [DomName("clip")]
        [DomAccessor(Accessors.Getter)]
        public static String GetClip(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Clip);
        }

        /// <summary>
        /// Sets which part of a positioned object is visible.
        /// </summary>
        [DomName("clip")]
        [DomAccessor(Accessors.Setter)]
        public static void SetClip(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Clip, value);
        }

        /// <summary>
        /// Gets the bottom coordinate of the object clipping region.
        /// </summary>
        [DomName("clipBottom")]
        [DomAccessor(Accessors.Getter)]
        public static String GetClipBottom(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ClipBottom);
        }

        /// <summary>
        /// Sets the bottom coordinate of the object clipping region.
        /// </summary>
        [DomName("clipBottom")]
        [DomAccessor(Accessors.Setter)]
        public static void SetClipBottom(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ClipBottom, value);
        }

        /// <summary>
        /// Gets the left coordinate of the object clipping region.
        /// </summary>
        [DomName("clipLeft")]
        [DomAccessor(Accessors.Getter)]
        public static String GetClipLeft(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ClipLeft);
        }

        /// <summary>
        /// Sets the left coordinate of the object clipping region.
        /// </summary>
        [DomName("clipLeft")]
        [DomAccessor(Accessors.Setter)]
        public static void SetClipLeft(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ClipLeft, value);
        }

        /// <summary>
        /// Gets a reference to the SVG graphical object
        /// that will be used as the clipping path.
        /// </summary>
        [DomName("clipPath")]
        [DomAccessor(Accessors.Getter)]
        public static String GetClipPath(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ClipPath);
        }

        /// <summary>
        /// Sets a reference to the SVG graphical object
        /// that will be used as the clipping path.
        /// </summary>
        [DomName("clipPath")]
        [DomAccessor(Accessors.Setter)]
        public static void SetClipPath(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ClipPath, value);
        }

        /// <summary>
        /// Gets the right coordinate of the object clipping region.
        /// </summary>
        [DomName("clipRight")]
        [DomAccessor(Accessors.Getter)]
        public static String GetClipRight(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ClipRight);
        }

        /// <summary>
        /// Sets the right coordinate of the object clipping region.
        /// </summary>
        [DomName("clipRight")]
        [DomAccessor(Accessors.Setter)]
        public static void SetClipRight(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ClipRight, value);
        }

        /// <summary>
        /// Gets the algorithm used to determine what parts of the
        /// canvas are affected by the fill operation.
        /// </summary>
        [DomName("clipRule")]
        [DomAccessor(Accessors.Getter)]
        public static String GetClipRule(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ClipRule);
        }

        /// <summary>
        /// Sets the algorithm used to determine what parts of the
        /// canvas are affected by the fill operation.
        /// </summary>
        [DomName("clipRule")]
        [DomAccessor(Accessors.Setter)]
        public static void SetClipRule(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ClipRule, value);
        }

        /// <summary>
        /// Gets the top coordinate of the object clipping region.
        /// </summary>
        [DomName("clipTop")]
        [DomAccessor(Accessors.Getter)]
        public static String GetClipTop(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ClipTop);
        }

        /// <summary>
        /// Sets the top coordinate of the object clipping region.
        /// </summary>
        [DomName("clipTop")]
        [DomAccessor(Accessors.Setter)]
        public static void SetClipTop(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ClipTop, value);
        }

        /// <summary>
        /// Gets the foreground color of the text of an object.
        /// </summary>
        [DomName("color")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColor(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Color);
        }

        /// <summary>
        /// Sets the foreground color of the text of an object.
        /// </summary>
        [DomName("color")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColor(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Color, value);
        }

        /// <summary>
        /// Gets which color space to use for filter effects.
        /// </summary>
        [DomName("colorInterpolationFilters")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColorInterpolationFilters(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ColorInterpolationFilters);
        }

        /// <summary>
        /// Sets which color space to use for filter effects.
        /// </summary>
        [DomName("colorInterpolationFilters")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColorInterpolationFilters(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ColorInterpolationFilters, value);
        }

        /// <summary>
        /// Gets the optimal number of columns in a multi-column
        /// element.
        /// </summary>
        [DomName("columnCount")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColumnCount(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ColumnCount);
        }

        /// <summary>
        /// Sets the optimal number of columns in a multi-column
        /// element.
        /// </summary>
        [DomName("columnCount")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColumnCount(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ColumnCount, value);
        }

        /// <summary>
        /// Gets a value that indicates how the column lengths in a
        /// multi-column element are affected by the content flow.
        /// </summary>
        [DomName("columnFill")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColumnFill(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ColumnFill);
        }

        /// <summary>
        /// Sets a value that indicates how the column lengths in a
        /// multi-column element are affected by the content flow.
        /// </summary>
        [DomName("columnFill")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColumnFill(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ColumnFill, value);
        }

        /// <summary>
        /// Gets the width of the gap between columns in a multi-column
        /// element.
        /// </summary>
        [DomName("columnGap")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColumnGap(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ColumnGap);
        }

        /// <summary>
        /// Sets the width of the gap between columns in a multi-column
        /// element.
        /// </summary>
        [DomName("columnGap")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColumnGap(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ColumnGap, value);
        }

        /// <summary>
        /// Gets a shorthand value  that specifies values for the
        /// columnRuleWidth, columnRuleStyle, and the columnRuleColor of a
        /// multi-column element.
        /// </summary>
        [DomName("columnRule")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColumnRule(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ColumnRule);
        }

        /// <summary>
        /// Sets a shorthand value  that specifies values for the
        /// columnRuleWidth, columnRuleStyle, and the columnRuleColor of a
        /// multi-column element.
        /// </summary>
        [DomName("columnRule")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColumnRule(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ColumnRule, value);
        }

        /// <summary>
        /// Gets the color for all column rules in a multi-column
        /// element.
        /// </summary>
        [DomName("columnRuleColor")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColumnRuleColor(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ColumnRuleColor);
        }

        /// <summary>
        /// Sets the color for all column rules in a multi-column
        /// element.
        /// </summary>
        [DomName("columnRuleColor")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColumnRuleColor(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ColumnRuleColor, value);
        }

        /// <summary>
        /// Gets the style for all column rules in a multi-column
        /// element.
        /// </summary>
        [DomName("columnRuleStyle")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColumnRuleStyle(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ColumnRuleStyle);
        }

        /// <summary>
        /// Sets the style for all column rules in a multi-column
        /// element.
        /// </summary>
        [DomName("columnRuleStyle")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColumnRuleStyle(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ColumnRuleStyle, value);
        }

        /// <summary>
        /// Gets the width of all column rules in a multi-column
        /// element.
        /// </summary>
        [DomName("columnRuleWidth")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColumnRuleWidth(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ColumnRuleWidth);
        }

        /// <summary>
        /// Sets the width of all column rules in a multi-column
        /// element.
        /// </summary>
        [DomName("columnRuleWidth")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColumnRuleWidth(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ColumnRuleWidth, value);
        }

        /// <summary>
        /// Gets a shorthand value that specifies values for the
        /// column-width and the column-count of a multi-column element.
        /// </summary>
        [DomName("columns")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColumns(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Columns);
        }

        /// <summary>
        /// Sets a shorthand value that specifies values for the
        /// column-width and the column-count of a multi-column element.
        /// </summary>
        [DomName("columns")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColumns(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Columns, value);
        }

        /// <summary>
        /// Gets the number of columns that a content block
        /// spans in a multi-column element.
        /// </summary>
        [DomName("columnSpan")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColumnSpan(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ColumnSpan);
        }

        /// <summary>
        /// Sets the number of columns that a content block
        /// spans in a multi-column element.
        /// </summary>
        [DomName("columnSpan")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColumnSpan(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ColumnSpan, value);
        }

        /// <summary>
        /// Gets the optimal width of the columns in a multi-column
        /// element.
        /// </summary>
        [DomName("columnWidth")]
        [DomAccessor(Accessors.Getter)]
        public static String GetColumnWidth(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ColumnWidth);
        }

        /// <summary>
        /// Sets the optimal width of the columns in a multi-column
        /// element.
        /// </summary>
        [DomName("columnWidth")]
        [DomAccessor(Accessors.Setter)]
        public static void SetColumnWidth(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ColumnWidth, value);
        }

        /// <summary>
        /// Gets generated content to insert before or after an
        /// element.
        /// </summary>
        [DomName("content")]
        [DomAccessor(Accessors.Getter)]
        public static String GetContent(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Content);
        }

        /// <summary>
        /// Sets generated content to insert before or after an
        /// element.
        /// </summary>
        [DomName("content")]
        [DomAccessor(Accessors.Setter)]
        public static void SetContent(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Content, value);
        }

        /// <summary>
        /// Gets a list of counters to increment.
        /// </summary>
        [DomName("counterIncrement")]
        [DomAccessor(Accessors.Getter)]
        public static String GetCounterIncrement(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.CounterIncrement);
        }

        /// <summary>
        /// Sets a list of counters to increment.
        /// </summary>
        [DomName("counterIncrement")]
        [DomAccessor(Accessors.Setter)]
        public static void SetCounterIncrement(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.CounterIncrement, value);
        }

        /// <summary>
        /// Gets a list of counters to create or reset to zero.
        /// </summary>
        [DomName("counterReset")]
        [DomAccessor(Accessors.Getter)]
        public static String GetCounterReset(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.CounterReset);
        }

        /// <summary>
        /// Sets a list of counters to create or reset to zero.
        /// </summary>
        [DomName("counterReset")]
        [DomAccessor(Accessors.Setter)]
        public static void SetCounterReset(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.CounterReset, value);
        }

        /// <summary>
        /// Gets a value that specifies whether a box should float to
        /// the left, right, or not at all.
        /// </summary>
        [DomName("cssFloat")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFloat(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Float);
        }

        /// <summary>
        /// Sets a value that specifies whether a box should float to
        /// the left, right, or not at all.
        /// </summary>
        [DomName("cssFloat")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFloat(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Float, value);
        }

        /// <summary>
        /// Gets the type of cursor to display as the mouse pointer
        /// moves over the object.
        /// </summary>
        [DomName("cursor")]
        [DomAccessor(Accessors.Getter)]
        public static String GetCursor(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Cursor);
        }

        /// <summary>
        /// Sets the type of cursor to display as the mouse pointer
        /// moves over the object.
        /// </summary>
        [DomName("cursor")]
        [DomAccessor(Accessors.Setter)]
        public static void SetCursor(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Cursor, value);
        }

        /// <summary>
        /// Gets the reading order of the object.
        /// </summary>
        [DomName("direction")]
        [DomAccessor(Accessors.Getter)]
        public static String GetDirection(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Direction);
        }

        /// <summary>
        /// Sets the reading order of the object.
        /// </summary>
        [DomName("direction")]
        [DomAccessor(Accessors.Setter)]
        public static void SetDirection(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Direction, value);
        }

        /// <summary>
        /// Gets a value that indicates whether and how the object is
        /// rendered.
        /// </summary>
        [DomName("display")]
        [DomAccessor(Accessors.Getter)]
        public static String GetDisplay(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Display);
        }

        /// <summary>
        /// Sets a value that indicates whether and how the object is
        /// rendered.
        /// </summary>
        [DomName("display")]
        [DomAccessor(Accessors.Setter)]
        public static void SetDisplay(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Display, value);
        }

        /// <summary>
        /// Gets a value that determines or redetermines a
        /// scaled-baseline table.
        /// </summary>
        [DomName("dominantBaseline")]
        [DomAccessor(Accessors.Getter)]
        public static String GetDominantBaseline(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.DominantBaseline);
        }

        /// <summary>
        /// Sets a value that determines or redetermines a
        /// scaled-baseline table.
        /// </summary>
        [DomName("dominantBaseline")]
        [DomAccessor(Accessors.Setter)]
        public static void SetDominantBaseline(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.DominantBaseline, value);
        }

        /// <summary>
        /// Gets whether to show or hide a cell without content.
        /// </summary>
        [DomName("emptyCells")]
        [DomAccessor(Accessors.Getter)]
        public static String GetEmptyCells(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.EmptyCells);
        }

        /// <summary>
        /// Sets whether to show or hide a cell without content.
        /// </summary>
        [DomName("emptyCells")]
        [DomAccessor(Accessors.Setter)]
        public static void SetEmptyCells(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.EmptyCells, value);
        }

        /// <summary>
        /// Gets a shared background image all graphic elements within a
        /// container.
        /// </summary>
        [DomName("enableBackground")]
        [DomAccessor(Accessors.Getter)]
        public static String GetEnableBackground(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.EnableBackground);
        }

        /// <summary>
        /// Sets a shared background image all graphic elements within a
        /// container.
        /// </summary>
        [DomName("enableBackground")]
        [DomAccessor(Accessors.Setter)]
        public static void SetEnableBackground(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.EnableBackground, value);
        }

        /// <summary>
        /// Gets a value that indicates the color to paint the
        /// interior of the given graphical element.
        /// </summary>
        [DomName("fill")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFill(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Fill);
        }

        /// <summary>
        /// Sets a value that indicates the color to paint the
        /// interior of the given graphical element.
        /// </summary>
        [DomName("fill")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFill(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Fill, value);
        }

        /// <summary>
        /// Gets a value that specifies the opacity of the painting
        /// operation that is used to paint the interior of the current object.
        /// </summary>
        [DomName("fillOpacity")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFillOpacity(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.FillOpacity);
        }

        /// <summary>
        /// Sets a value that specifies the opacity of the painting
        /// operation that is used to paint the interior of the current object.
        /// </summary>
        [DomName("fillOpacity")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFillOpacity(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.FillOpacity, value);
        }

        /// <summary>
        /// Gets a value that indicates the algorithm that is to be
        /// used to determine what parts of the canvas are included inside the
        /// shape.
        /// </summary>
        [DomName("fillRule")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFillRule(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.FillRule);
        }

        /// <summary>
        /// Sets a value that indicates the algorithm that is to be
        /// used to determine what parts of the canvas are included inside the
        /// shape.
        /// </summary>
        [DomName("fillRule")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFillRule(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.FillRule, value);
        }

        /// <summary>
        /// Gets the filter property is generally used to apply a previously
        /// define filter to an applicable element.
        /// </summary>
        [DomName("filter")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFilter(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Filter);
        }

        /// <summary>
        /// Sets the filter property is generally used to apply a previously
        /// define filter to an applicable element.
        /// </summary>
        [DomName("filter")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFilter(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Filter, value);
        }

        /// <summary>
        /// Gets the parameter values of a flexible length, the
        /// positive and negative flexibility, and the preferred size.
        /// </summary>
        [DomName("flex")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFlex(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Flex);
        }

        /// <summary>
        /// Sets the parameter values of a flexible length, the
        /// positive and negative flexibility, and the preferred size.
        /// </summary>
        [DomName("flex")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFlex(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Flex, value);
        }

        /// <summary>
        /// Gets the initial main size of the flex item.
        /// </summary>
        [DomName("flexBasis")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFlexBasis(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.FlexBasis);
        }

        /// <summary>
        /// Sets the initial main size of the flex item.
        /// </summary>
        [DomName("flexBasis")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFlexBasis(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.FlexBasis, value);
        }

        /// <summary>
        /// Gets the direction of the main axis which specifies how the
        /// flex items are displayed in the flex container.
        /// </summary>
        [DomName("flexDirection")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFlexDirection(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.FlexDirection);
        }

        /// <summary>
        /// Sets the direction of the main axis which specifies how the
        /// flex items are displayed in the flex container.
        /// </summary>
        [DomName("flexDirection")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFlexDirection(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.FlexDirection, value);
        }

        /// <summary>
        /// Gets the shorthand property to set both the flex-direction
        /// and flex-wrap properties of a flex container.
        /// </summary>
        [DomName("flexFlow")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFlexFlow(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.FlexFlow);
        }

        /// <summary>
        /// Sets the shorthand property to set both the flex-direction
        /// and flex-wrap properties of a flex container.
        /// </summary>
        [DomName("flexFlow")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFlexFlow(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.FlexFlow, value);
        }

        /// <summary>
        /// Gets the flex grow factor for the flex item.
        /// </summary>
        [DomName("flexGrow")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFlexGrow(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.FlexGrow);
        }

        /// <summary>
        /// Sets the flex grow factor for the flex item.
        /// </summary>
        [DomName("flexGrow")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFlexGrow(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.FlexGrow, value);
        }

        /// <summary>
        /// Gets the flex shrink factor for the flex item.
        /// </summary>
        [DomName("flexShrink")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFlexShrink(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.FlexShrink);
        }

        /// <summary>
        /// Sets the flex shrink factor for the flex item.
        /// </summary>
        [DomName("flexShrink")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFlexShrink(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.FlexShrink, value);
        }

        /// <summary>
        /// Gets whether flex items wrap and the direction they wrap
        /// onto multiple lines or columns based on the space available in the
        /// flex container. 
        /// </summary>
        [DomName("flexWrap")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFlexWrap(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.FlexWrap);
        }

        /// <summary>
        /// Sets whether flex items wrap and the direction they wrap
        /// onto multiple lines or columns based on the space available in the
        /// flex container. 
        /// </summary>
        [DomName("flexWrap")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFlexWrap(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.FlexWrap, value);
        }

        /// <summary>
        /// Gets a combination of separate font properties of the
        /// object. Alternatively, sets or retrieves one or more of six
        /// user-preference fonts.
        /// </summary>
        [DomName("font")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFont(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Font);
        }

        /// <summary>
        /// Sets a combination of separate font properties of the
        /// object. Alternatively, sets or retrieves one or more of six
        /// user-preference fonts.
        /// </summary>
        [DomName("font")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFont(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Font, value);
        }

        /// <summary>
        /// Gets the name of the font used for text in the object.
        /// </summary>
        [DomName("fontFamily")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontFamily(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.FontFamily);
        }

        /// <summary>
        /// Sets the name of the font used for text in the object.
        /// </summary>
        [DomName("fontFamily")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontFamily(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.FontFamily, value);
        }

        /// <summary>
        /// Gets one or more values that specify glyph substitution and
        /// positioning in fonts that include OpenType layout features.
        /// </summary>
        [DomName("fontFeatureSettings")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontFeatureSettings(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.FontFeatureSettings);
        }

        /// <summary>
        /// Sets one or more values that specify glyph substitution and
        /// positioning in fonts that include OpenType layout features.
        /// </summary>
        [DomName("fontFeatureSettings")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontFeatureSettings(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.FontFeatureSettings, value);
        }

        /// <summary>
        /// Gets a value that indicates the font size used for text in
        /// the object.
        /// </summary>
        [DomName("fontSize")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontSize(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.FontSize);
        }

        /// <summary>
        /// Sets a value that indicates the font size used for text in
        /// the object.
        /// </summary>
        [DomName("fontSize")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontSize(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.FontSize, value);
        }

        /// <summary>
        /// Gets a value that specifies an aspect value for an element
        /// that will effectively preserve the x-height of the first choice
        /// font, whether it is substituted or not.
        /// </summary>
        [DomName("fontSizeAdjust")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontSizeAdjust(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.FontSizeAdjust);
        }

        /// <summary>
        /// Sets a value that specifies an aspect value for an element
        /// that will effectively preserve the x-height of the first choice
        /// font, whether it is substituted or not.
        /// </summary>
        [DomName("fontSizeAdjust")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontSizeAdjust(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.FontSizeAdjust, value);
        }

        /// <summary>
        /// Gets a value that indicates a normal, condensed, or
        /// expanded face of a font family.
        /// </summary>
        [DomName("fontStretch")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontStretch(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.FontStretch);
        }

        /// <summary>
        /// Sets a value that indicates a normal, condensed, or
        /// expanded face of a font family.
        /// </summary>
        [DomName("fontStretch")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontStretch(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.FontStretch, value);
        }

        /// <summary>
        /// Gets the font style of the object as italic, normal, or
        /// oblique.
        /// </summary>
        [DomName("fontStyle")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontStyle(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.FontStyle);
        }

        /// <summary>
        /// Sets the font style of the object as italic, normal, or
        /// oblique.
        /// </summary>
        [DomName("fontStyle")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontStyle(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.FontStyle, value);
        }

        /// <summary>
        /// Gets whether the text of the object is in small capital
        /// letters.
        /// </summary>
        [DomName("fontVariant")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontVariant(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.FontVariant);
        }

        /// <summary>
        /// Sets whether the text of the object is in small capital
        /// letters.
        /// </summary>
        [DomName("fontVariant")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontVariant(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.FontVariant, value);
        }

        /// <summary>
        /// Gets of sets the weight of the font of the object.
        /// </summary>
        [DomName("fontWeight")]
        [DomAccessor(Accessors.Getter)]
        public static String GetFontWeight(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.FontWeight);
        }

        /// <summary>
        /// Sets of sets the weight of the font of the object.
        /// </summary>
        [DomName("fontWeight")]
        [DomAccessor(Accessors.Setter)]
        public static void SetFontWeight(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.FontWeight, value);
        }

        /// <summary>
        /// Gets a value that alters the orientation of a sequence of
        /// characters relative to an inline-progression-direction of
        /// horizontal.
        /// </summary>
        [DomName("glyphOrientationHorizontal")]
        [DomAccessor(Accessors.Getter)]
        public static String GetGlyphOrientationHorizontal(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.GlyphOrientationHorizontal);
        }

        /// <summary>
        /// Sets a value that alters the orientation of a sequence of
        /// characters relative to an inline-progression-direction of
        /// horizontal.
        /// </summary>
        [DomName("glyphOrientationHorizontal")]
        [DomAccessor(Accessors.Setter)]
        public static void SetGlyphOrientationHorizontal(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.GlyphOrientationHorizontal, value);
        }

        /// <summary>
        /// Gets a value that alters the orientation of a sequence
        /// of characters relative to an inline-progression-direction of
        /// vertical.
        /// </summary>
        [DomName("glyphOrientationVertical")]
        [DomAccessor(Accessors.Getter)]
        public static String GetGlyphOrientationVertical(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.GlyphOrientationVertical);
        }

        /// <summary>
        /// Sets a value that alters the orientation of a sequence
        /// of characters relative to an inline-progression-direction of
        /// vertical.
        /// </summary>
        [DomName("glyphOrientationVertical")]
        [DomAccessor(Accessors.Setter)]
        public static void SetGlyphOrientationVertical(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.GlyphOrientationVertical, value);
        }

        /// <summary>
        /// Gets the height of the object.
        /// </summary>
        [DomName("height")]
        [DomAccessor(Accessors.Getter)]
        public static String GetHeight(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Height);
        }

        /// <summary>
        /// Sets the height of the object.
        /// </summary>
        [DomName("height")]
        [DomAccessor(Accessors.Setter)]
        public static void SetHeight(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Height, value);
        }

        /// <summary>
        /// Gets the state of an IME.
        /// </summary>
        [DomName("imeMode")]
        [DomAccessor(Accessors.Getter)]
        public static String GetImeMode(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ImeMode);
        }

        /// <summary>
        /// Sets the state of an IME.
        /// </summary>
        [DomName("imeMode")]
        [DomAccessor(Accessors.Setter)]
        public static void SetImeMode(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ImeMode, value);
        }

        /// <summary>
        /// Gets a how flex items are aligned along the main axis of
        /// the flex container after any flexible lengths and auto margins are
        /// resolved.
        /// </summary>
        [DomName("justifyContent")]
        [DomAccessor(Accessors.Getter)]
        public static String GetJustifyContent(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.JustifyContent);
        }

        /// <summary>
        /// Sets a how flex items are aligned along the main axis of
        /// the flex container after any flexible lengths and auto margins are
        /// resolved.
        /// </summary>
        [DomName("justifyContent")]
        [DomAccessor(Accessors.Setter)]
        public static void SetJustifyContent(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.JustifyContent, value);
        }

        /// <summary>
        /// Gets the composite document grid properties that specify
        /// the layout of text characters.
        /// </summary>
        [DomName("layoutGrid")]
        [DomAccessor(Accessors.Getter)]
        public static String GetLayoutGrid(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.LayoutGrid);
        }

        /// <summary>
        /// Sets the composite document grid properties that specify
        /// the layout of text characters.
        /// </summary>
        [DomName("layoutGrid")]
        [DomAccessor(Accessors.Setter)]
        public static void SetLayoutGrid(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.LayoutGrid, value);
        }

        /// <summary>
        /// Gets the size of the character grid used for rendering
        /// the text content of an element.
        /// </summary>
        [DomName("layoutGridChar")]
        [DomAccessor(Accessors.Getter)]
        public static String GetLayoutGridChar(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.LayoutGridChar);
        }

        /// <summary>
        /// Sets the size of the character grid used for rendering
        /// the text content of an element.
        /// </summary>
        [DomName("layoutGridChar")]
        [DomAccessor(Accessors.Setter)]
        public static void SetLayoutGridChar(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.LayoutGridChar, value);
        }

        /// <summary>
        /// Gets the gridline value used for rendering the text content
        /// of an element.
        /// </summary>
        [DomName("layoutGridLine")]
        [DomAccessor(Accessors.Getter)]
        public static String GetLayoutGridLine(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.LayoutGridLine);
        }

        /// <summary>
        /// Sets the gridline value used for rendering the text content
        /// of an element.
        /// </summary>
        [DomName("layoutGridLine")]
        [DomAccessor(Accessors.Setter)]
        public static void SetLayoutGridLine(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.LayoutGridLine, value);
        }

        /// <summary>
        /// Gets whether the text layout grid uses two dimensions.
        /// </summary>
        [DomName("layoutGridMode")]
        [DomAccessor(Accessors.Getter)]
        public static String GetLayoutGridMode(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.LayoutGridMode);
        }

        /// <summary>
        /// Sets whether the text layout grid uses two dimensions.
        /// </summary>
        [DomName("layoutGridMode")]
        [DomAccessor(Accessors.Setter)]
        public static void SetLayoutGridMode(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.LayoutGridMode, value);
        }

        /// <summary>
        /// Gets the type of grid used for rendering the text content
        /// of an element.
        /// </summary>
        [DomName("layoutGridType")]
        [DomAccessor(Accessors.Getter)]
        public static String GetLayoutGridType(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.LayoutGridType);
        }

        /// <summary>
        /// Sets the type of grid used for rendering the text content
        /// of an element.
        /// </summary>
        [DomName("layoutGridType")]
        [DomAccessor(Accessors.Setter)]
        public static void SetLayoutGridType(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.LayoutGridType, value);
        }

        /// <summary>
        /// Gets the position of the object relative to the left edge
        /// of the next positioned object in the document hierarchy.
        /// </summary>
        [DomName("left")]
        [DomAccessor(Accessors.Getter)]
        public static String GetLeft(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Left);
        }

        /// <summary>
        /// Sets the position of the object relative to the left edge
        /// of the next positioned object in the document hierarchy.
        /// </summary>
        [DomName("left")]
        [DomAccessor(Accessors.Setter)]
        public static void SetLeft(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Left, value);
        }

        /// <summary>
        /// Gets the amount of additional space between letters in the
        /// object.
        /// </summary>
        [DomName("letterSpacing")]
        [DomAccessor(Accessors.Getter)]
        public static String GetLetterSpacing(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.LetterSpacing);
        }

        /// <summary>
        /// Sets the amount of additional space between letters in the
        /// object.
        /// </summary>
        [DomName("letterSpacing")]
        [DomAccessor(Accessors.Setter)]
        public static void SetLetterSpacing(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.LetterSpacing, value);
        }

        /// <summary>
        /// Gets the distance between lines in the object.
        /// </summary>
        [DomName("lineHeight")]
        [DomAccessor(Accessors.Getter)]
        public static String GetLineHeight(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.LineHeight);
        }

        /// <summary>
        /// Sets the distance between lines in the object.
        /// </summary>
        [DomName("lineHeight")]
        [DomAccessor(Accessors.Setter)]
        public static void SetLineHeight(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.LineHeight, value);
        }

        /// <summary>
        /// Gets up to three separate list-style properties of the
        /// object.
        /// </summary>
        [DomName("listStyle")]
        [DomAccessor(Accessors.Getter)]
        public static String GetListStyle(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ListStyle);
        }

        /// <summary>
        /// Sets up to three separate list-style properties of the
        /// object.
        /// </summary>
        [DomName("listStyle")]
        [DomAccessor(Accessors.Setter)]
        public static void SetListStyle(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ListStyle, value);
        }

        /// <summary>
        /// Gets a value that indicates which image to use as a
        /// list-item marker for the object.
        /// </summary>
        [DomName("listStyleImage")]
        [DomAccessor(Accessors.Getter)]
        public static String GetListStyleImage(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ListStyleImage);
        }

        /// <summary>
        /// Sets a value that indicates which image to use as a
        /// list-item marker for the object.
        /// </summary>
        [DomName("listStyleImage")]
        [DomAccessor(Accessors.Setter)]
        public static void SetListStyleImage(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ListStyleImage, value);
        }

        /// <summary>
        /// Gets a variable that indicates how the list-item marker is
        /// drawn relative to the content of the object.
        /// </summary>
        [DomName("listStylePosition")]
        [DomAccessor(Accessors.Getter)]
        public static String GetListStylePosition(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ListStylePosition);
        }

        /// <summary>
        /// Sets a variable that indicates how the list-item marker is
        /// drawn relative to the content of the object.
        /// </summary>
        [DomName("listStylePosition")]
        [DomAccessor(Accessors.Setter)]
        public static void SetListStylePosition(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ListStylePosition, value);
        }

        /// <summary>
        /// Gets the predefined type of the line-item marker for the
        /// object.
        /// </summary>
        [DomName("listStyleType")]
        [DomAccessor(Accessors.Getter)]
        public static String GetListStyleType(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ListStyleType);
        }

        /// <summary>
        /// Sets the predefined type of the line-item marker for the
        /// object.
        /// </summary>
        [DomName("listStyleType")]
        [DomAccessor(Accessors.Setter)]
        public static void SetListStyleType(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ListStyleType, value);
        }

        /// <summary>
        /// Gets the width of the top, right, bottom, and left margins
        /// of the object.
        /// </summary>
        [DomName("margin")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMargin(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Margin);
        }

        /// <summary>
        /// Sets the width of the top, right, bottom, and left margins
        /// of the object.
        /// </summary>
        [DomName("margin")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMargin(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Margin, value);
        }

        /// <summary>
        /// Gets the height of the bottom margin of the object.
        /// </summary>
        [DomName("marginBottom")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMarginBottom(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.MarginBottom);
        }

        /// <summary>
        /// Sets the height of the bottom margin of the object.
        /// </summary>
        [DomName("marginBottom")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMarginBottom(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.MarginBottom, value);
        }

        /// <summary>
        /// Gets the width of the left margin of the object.
        /// </summary>
        [DomName("marginLeft")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMarginLeft(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.MarginLeft);
        }

        /// <summary>
        /// Sets the width of the left margin of the object.
        /// </summary>
        [DomName("marginLeft")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMarginLeft(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.MarginLeft, value);
        }

        /// <summary>
        /// Gets the width of the right margin of the object.
        /// </summary>
        [DomName("marginRight")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMarginRight(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.MarginRight);
        }

        /// <summary>
        /// Sets the width of the right margin of the object.
        /// </summary>
        [DomName("marginRight")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMarginRight(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.MarginRight, value);
        }

        /// <summary>
        /// Gets the height of the top margin of the object.
        /// </summary>
        [DomName("marginTop")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMarginTop(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.MarginTop);
        }

        /// <summary>
        /// Sets the height of the top margin of the object.
        /// </summary>
        [DomName("marginTop")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMarginTop(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.MarginTop, value);
        }

        /// <summary>
        /// Gets a value that specifies the marker symbol that is
        /// used for all vertices on the given path element or basic shape.
        /// </summary>
        [DomName("marker")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMarker(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Marker);
        }

        /// <summary>
        /// Sets a value that specifies the marker symbol that is
        /// used for all vertices on the given path element or basic shape.
        /// </summary>
        [DomName("marker")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMarker(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Marker, value);
        }

        /// <summary>
        /// Gets a value that defines the arrowhead or polymarker that
        /// is drawn at the final vertex of a given path element or basic
        /// shape.
        /// </summary>
        [DomName("markerEnd")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMarkerEnd(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.MarkerEnd);
        }

        /// <summary>
        /// Sets a value that defines the arrowhead or polymarker that
        /// is drawn at the final vertex of a given path element or basic
        /// shape.
        /// </summary>
        [DomName("markerEnd")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMarkerEnd(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.MarkerEnd, value);
        }

        /// <summary>
        /// Gets a value that defines the arrowhead or polymarker that
        /// is drawn at every other vertex (that is, every vertex except the
        /// first and last) of a given path element or basic shape.
        /// </summary>
        [DomName("markerMid")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMarkerMid(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.MarkerMid);
        }

        /// <summary>
        /// Sets a value that defines the arrowhead or polymarker that
        /// is drawn at every other vertex (that is, every vertex except the
        /// first and last) of a given path element or basic shape.
        /// </summary>
        [DomName("markerMid")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMarkerMid(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.MarkerMid, value);
        }

        /// <summary>
        /// Gets a value that defines the arrowhead or polymarker that
        /// is drawn at the first vertex of a given path element or basic
        /// shape.
        /// </summary>
        [DomName("markerStart")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMarkerStart(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.MarkerStart);
        }

        /// <summary>
        /// Sets a value that defines the arrowhead or polymarker that
        /// is drawn at the first vertex of a given path element or basic
        /// shape.
        /// </summary>
        [DomName("markerStart")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMarkerStart(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.MarkerStart, value);
        }

        /// <summary>
        /// Gets a value that indicates a SVG mask.
        /// </summary>
        [DomName("mask")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMask(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Mask);
        }

        /// <summary>
        /// Sets a value that indicates a SVG mask.
        /// </summary>
        [DomName("mask")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMask(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Mask, value);
        }

        /// <summary>
        /// Gets the maximum height for an element.
        /// </summary>
        [DomName("maxHeight")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMaxHeight(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.MaxHeight);
        }

        /// <summary>
        /// Sets the maximum height for an element.
        /// </summary>
        [DomName("maxHeight")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMaxHeight(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.MaxHeight, value);
        }

        /// <summary>
        /// Gets the maximum width for an element.
        /// </summary>
        [DomName("maxWidth")]
        public static String GetMaxWidth(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.MaxWidth);
        }

        /// <summary>
        /// Sets the maximum width for an element.
        /// </summary>
        [DomName("maxWidth")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMaxWidth(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.MaxWidth, value);
        }

        /// <summary>
        /// Gets the minimum height for an element.
        /// </summary>
        [DomName("minHeight")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMinHeight(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.MinHeight);
        }

        /// <summary>
        /// Sets the minimum height for an element.
        /// </summary>
        [DomName("minHeight")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMinHeight(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.MinHeight, value);
        }

        /// <summary>
        /// Gets the minimum width for an element.
        /// </summary>
        [DomName("minWidth")]
        [DomAccessor(Accessors.Getter)]
        public static String GetMinWidth(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.MinWidth);
        }

        /// <summary>
        /// Sets the minimum width for an element.
        /// </summary>
        [DomName("minWidth")]
        [DomAccessor(Accessors.Setter)]
        public static void SetMinWidth(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.MinWidth, value);
        }

        /// <summary>
        /// Gets a value that specifies object or group opacity in CSS
        /// or SVG.
        /// </summary>
        [DomName("opacity")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOpacity(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Opacity);
        }

        /// <summary>
        /// Sets a value that specifies object or group opacity in CSS
        /// or SVG.
        /// </summary>
        [DomName("opacity")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOpacity(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Opacity, value);
        }

        /// <summary>
        /// Gets the order, which property specifies the order used to
        /// lay out flex items in their flex container. Elements are laid out
        /// by ascending order of the order value. Elements with the same order
        /// value are laid out in the order they appear in the source code.
        /// </summary>
        [DomName("order")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOrder(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Order);
        }

        /// <summary>
        /// Sets the order, which property specifies the order used to
        /// lay out flex items in their flex container. Elements are laid out
        /// by ascending order of the order value. Elements with the same order
        /// value are laid out in the order they appear in the source code.
        /// </summary>
        [DomName("order")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOrder(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Order, value);
        }

        /// <summary>
        /// Gets the minimum number of lines of a paragraph that must
        /// appear at the bottom of a page.
        /// </summary>
        [DomName("orphans")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOrphans(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Orphans);
        }

        /// <summary>
        /// Sets the minimum number of lines of a paragraph that must
        /// appear at the bottom of a page.
        /// </summary>
        [DomName("orphans")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOrphans(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Orphans, value);
        }

        /// <summary>
        /// Gets the outline frame.
        /// </summary>
        [DomName("outline")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOutline(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Outline);
        }

        /// <summary>
        /// Sets the outline frame.
        /// </summary>
        [DomName("outline")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOutline(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Outline, value);
        }

        /// <summary>
        /// Gets the color of the outline frame.
        /// </summary>
        [DomName("outlineColor")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOutlineColor(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.OutlineColor);
        }

        /// <summary>
        /// Sets the color of the outline frame.
        /// </summary>
        [DomName("outlineColor")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOutlineColor(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.OutlineColor, value);
        }

        /// <summary>
        /// Gets the style of the outline frame.
        /// </summary>
        [DomName("outlineStyle")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOutlineStyle(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.OutlineStyle);
        }

        /// <summary>
        /// Sets the style of the outline frame.
        /// </summary>
        [DomName("outlineStyle")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOutlineStyle(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.OutlineStyle, value);
        }

        /// <summary>
        /// Gets the width of the outline frame.
        /// </summary>
        [DomName("outlineWidth")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOutlineWidth(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.OutlineWidth);
        }

        /// <summary>
        /// Sets the width of the outline frame.
        /// </summary>
        [DomName("outlineWidth")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOutlineWidth(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.OutlineWidth, value);
        }

        /// <summary>
        /// Gets a value indicating how to manage the content of the
        /// object when the content exceeds the height or width of the object.
        /// </summary>
        [DomName("overflow")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOverflow(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Overflow);
        }

        /// <summary>
        /// Sets a value indicating how to manage the content of the
        /// object when the content exceeds the height or width of the object.
        /// </summary>
        [DomName("overflow")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOverflow(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Overflow, value);
        }

        /// <summary>
        /// Gets how to manage the content of the object when the
        /// content exceeds the width of the object.
        /// </summary>
        [DomName("overflowX")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOverflowX(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.OverflowX);
        }

        /// <summary>
        /// Sets how to manage the content of the object when the
        /// content exceeds the width of the object.
        /// </summary>
        [DomName("overflowX")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOverflowX(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.OverflowX, value);
        }

        /// <summary>
        /// Gets how to manage the content of the object when the
        /// content exceeds the height of the object.
        /// </summary>
        [DomName("overflowY")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOverflowY(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.OverflowY);
        }

        /// <summary>
        /// Sets how to manage the content of the object when the
        /// content exceeds the height of the object.
        /// </summary>
        [DomName("overflowY")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOverflowY(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.OverflowY, value);
        }

        /// <summary>
        /// Gets the amount of space to insert between the object and
        /// its margin or, if there is a border, between the object and its
        /// border.
        /// </summary>
        [DomName("padding")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPadding(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Padding);
        }

        /// <summary>
        /// Sets the amount of space to insert between the object and
        /// its margin or, if there is a border, between the object and its
        /// border.
        /// </summary>
        [DomName("padding")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPadding(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Padding, value);
        }

        /// <summary>
        /// Gets the amount of space to insert between the bottom
        /// border of the object and the content.
        /// </summary>
        [DomName("paddingBottom")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPaddingBottom(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.PaddingBottom);
        }

        /// <summary>
        /// Sets the amount of space to insert between the bottom
        /// border of the object and the content.
        /// </summary>
        [DomName("paddingBottom")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPaddingBottom(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.PaddingBottom, value);
        }

        /// <summary>
        /// Gets the amount of space to insert between the left
        /// border of the object and the content.
        /// </summary>
        [DomName("paddingLeft")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPaddingLeft(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.PaddingLeft);
        }

        /// <summary>
        /// Sets the amount of space to insert between the left
        /// border of the object and the content.
        /// </summary>
        [DomName("paddingLeft")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPaddingLeft(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.PaddingLeft, value);
        }

        /// <summary>
        /// Gets the amount of space to insert between the right border
        /// of the object and the content.
        /// </summary>
        [DomName("paddingRight")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPaddingRight(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.PaddingRight);
        }

        /// <summary>
        /// Sets the amount of space to insert between the right border
        /// of the object and the content.
        /// </summary>
        [DomName("paddingRight")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPaddingRight(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.PaddingRight, value);
        }

        /// <summary>
        /// Gets the amount of space to insert between the top border
        /// of the object and the content.
        /// </summary>
        [DomName("paddingTop")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPaddingTop(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.PaddingTop);
        }

        /// <summary>
        /// Sets the amount of space to insert between the top border
        /// of the object and the content.
        /// </summary>
        [DomName("paddingTop")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPaddingTop(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.PaddingTop, value);
        }

        /// <summary>
        /// Gets a value indicating whether a page break occurs after
        /// the object.
        /// </summary>
        [DomName("pageBreakAfter")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPageBreakAfter(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.PageBreakAfter);
        }

        /// <summary>
        /// Sets a value indicating whether a page break occurs after
        /// the object.
        /// </summary>
        [DomName("pageBreakAfter")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPageBreakAfter(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.PageBreakAfter, value);
        }

        /// <summary>
        /// Gets a string indicating whether a page break occurs before
        /// the object.
        /// </summary>
        [DomName("pageBreakBefore")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPageBreakBefore(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.PageBreakBefore);
        }

        /// <summary>
        /// Sets a string indicating whether a page break occurs before
        /// the object.
        /// </summary>
        [DomName("pageBreakBefore")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPageBreakBefore(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.PageBreakBefore, value);
        }

        /// <summary>
        /// Gets a string indicating whether a page break is allowed to
        /// occur inside the object.
        /// </summary>
        [DomName("pageBreakInside")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPageBreakInside(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.PageBreakInside);
        }

        /// <summary>
        /// Sets a string indicating whether a page break is allowed to
        /// occur inside the object.
        /// </summary>
        [DomName("pageBreakInside")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPageBreakInside(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.PageBreakInside, value);
        }

        /// <summary>
        /// Gets a value that represents the perspective from which all
        /// child elements of the object are viewed.
        /// </summary>
        [DomName("perspective")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPerspective(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Perspective);
        }

        /// <summary>
        /// Sets a value that represents the perspective from which all
        /// child elements of the object are viewed.
        /// </summary>
        [DomName("perspective")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPerspective(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Perspective, value);
        }

        /// <summary>
        /// Gets one or two values that represent the origin (the
        /// vanishing point for the 3-D space) of an object with an perspective
        /// property declaration.
        /// </summary>
        [DomName("perspectiveOrigin")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPerspectiveOrigin(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.PerspectiveOrigin);
        }

        /// <summary>
        /// Sets one or two values that represent the origin (the
        /// vanishing point for the 3-D space) of an object with an perspective
        /// property declaration.
        /// </summary>
        [DomName("perspectiveOrigin")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPerspectiveOrigin(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.PerspectiveOrigin, value);
        }

        /// <summary>
        /// Gets a value that specifies under what circumstances a
        /// given graphics element can be the target element for a pointer
        /// event in SVG.
        /// </summary>
        [DomName("pointerEvents")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPointerEvents(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.PointerEvents);
        }

        /// <summary>
        /// Sets a value that specifies under what circumstances a
        /// given graphics element can be the target element for a pointer
        /// event in SVG.
        /// </summary>
        [DomName("pointerEvents")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPointerEvents(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.PointerEvents, value);
        }

        /// <summary>
        /// Gets the pairs of strings to be used as quotes in generated
        /// content.
        /// </summary>
        [DomName("quotes")]
        [DomAccessor(Accessors.Getter)]
        public static String GetQuotes(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Quotes);
        }

        /// <summary>
        /// Sets the pairs of strings to be used as quotes in generated
        /// content.
        /// </summary>
        [DomName("quotes")]
        [DomAccessor(Accessors.Setter)]
        public static void SetQuotes(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Quotes, value);
        }

        /// <summary>
        /// Gets the type of positioning used for the object.
        /// </summary>
        [DomName("position")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPosition(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Position);
        }

        /// <summary>
        /// Sets the type of positioning used for the object.
        /// </summary>
        [DomName("position")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPosition(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Position, value);
        }

        /// <summary>
        /// Gets the position of the object relative to the right edge
        /// of the next positioned object in the document hierarchy.
        /// </summary>
        [DomName("right")]
        [DomAccessor(Accessors.Getter)]
        public static String GetRight(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Right);
        }

        /// <summary>
        /// Sets the position of the object relative to the right edge
        /// of the next positioned object in the document hierarchy.
        /// </summary>
        [DomName("right")]
        [DomAccessor(Accessors.Setter)]
        public static void SetRight(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Right, value);
        }

        /// <summary>
        /// Gets a value that indicates how to align the ruby text
        /// content.
        /// </summary>
        [DomName("rubyAlign")]
        [DomAccessor(Accessors.Getter)]
        public static String GetRubyAlign(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.RubyAlign);
        }

        /// <summary>
        /// Sets a value that indicates how to align the ruby text
        /// content.
        /// </summary>
        [DomName("rubyAlign")]
        [DomAccessor(Accessors.Setter)]
        public static void SetRubyAlign(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.RubyAlign, value);
        }

        /// <summary>
        /// Gets a value that indicates whether, and on which side,
        /// ruby text is allowed to partially overhang any adjacent text in
        /// addition to its own base, when the ruby text is wider than the
        /// ruby base.
        /// </summary>
        [DomName("rubyOverhang")]
        [DomAccessor(Accessors.Getter)]
        public static String GetRubyOverhang(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.RubyOverhang);
        }

        /// <summary>
        /// Sets a value that indicates whether, and on which side,
        /// ruby text is allowed to partially overhang any adjacent text in
        /// addition to its own base, when the ruby text is wider than the
        /// ruby base.
        /// </summary>
        [DomName("rubyOverhang")]
        [DomAccessor(Accessors.Setter)]
        public static void SetRubyOverhang(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.RubyOverhang, value);
        }

        /// <summary>
        /// Gets a value that controls the position of the ruby text
        /// with respect to its base.
        /// </summary>
        [DomName("rubyPosition")]
        [DomAccessor(Accessors.Getter)]
        public static String GetRubyPosition(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.RubyPosition);
        }

        /// <summary>
        /// Sets a value that controls the position of the ruby text
        /// with respect to its base.
        /// </summary>
        [DomName("rubyPosition")]
        [DomAccessor(Accessors.Setter)]
        public static void SetRubyPosition(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.RubyPosition, value);
        }

        /// <summary>
        /// Gets the color of the top and left edges of the scroll
        /// box and scroll arrows of a scroll bar.
        /// </summary>
        [DomName("scrollbar3dLightColor")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollbar3dLightColor(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Scrollbar3dLightColor);
        }

        /// <summary>
        /// Sets the color of the top and left edges of the scroll
        /// box and scroll arrows of a scroll bar.
        /// </summary>
        [DomName("scrollbar3dLightColor")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollbar3dLightColor(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Scrollbar3dLightColor, value);
        }

        /// <summary>
        /// Gets the color of the arrow elements of a scroll arrow.
        /// </summary>
        [DomName("scrollbarArrowColor")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollbarArrowColor(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ScrollbarArrowColor);
        }

        /// <summary>
        /// Sets the color of the arrow elements of a scroll arrow.
        /// </summary>
        [DomName("scrollbarArrowColor")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollbarArrowColor(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ScrollbarArrowColor, value);
        }

        /// <summary>
        /// Gets the color of the gutter of a scroll bar.
        /// </summary>
        [DomName("scrollbarDarkShadowColor")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollbarDarkShadowColor(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ScrollbarDarkShadowColor);
        }

        /// <summary>
        /// Sets the color of the gutter of a scroll bar.
        /// </summary>
        [DomName("scrollbarDarkShadowColor")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollbarDarkShadowColor(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ScrollbarDarkShadowColor, value);
        }

        /// <summary>
        /// Gets the color of the scroll box and scroll arrows of a
        /// scroll bar.
        /// </summary>
        [DomName("scrollbarFaceColor")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollbarFaceColor(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ScrollbarFaceColor);
        }

        /// <summary>
        /// Sets the color of the scroll box and scroll arrows of a
        /// scroll bar.
        /// </summary>
        [DomName("scrollbarFaceColor")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollbarFaceColor(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ScrollbarFaceColor, value);
        }

        /// <summary>
        /// Gets the color of the top and left edges of the scroll box
        /// and scroll arrows of a scroll bar.
        /// </summary>
        [DomName("scrollbarHighlightColor")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollbarHighlightColor(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ScrollbarHighlightColor);
        }

        /// <summary>
        /// Sets the color of the top and left edges of the scroll box
        /// and scroll arrows of a scroll bar.
        /// </summary>
        [DomName("scrollbarHighlightColor")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollbarHighlightColor(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ScrollbarHighlightColor, value);
        }

        /// <summary>
        /// Gets the color of the bottom and right edges of the scroll
        /// box and scroll arrows of a scroll bar.
        /// </summary>
        [DomName("scrollbarShadowColor")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollbarShadowColor(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ScrollbarShadowColor);
        }

        /// <summary>
        /// Sets the color of the bottom and right edges of the scroll
        /// box and scroll arrows of a scroll bar.
        /// </summary>
        [DomName("scrollbarShadowColor")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollbarShadowColor(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ScrollbarShadowColor, value);
        }

        /// <summary>
        /// Gets the color of the track element of a scroll bar.
        /// </summary>
        [DomName("scrollbarTrackColor")]
        [DomAccessor(Accessors.Getter)]
        public static String GetScrollbarTrackColor(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ScrollbarTrackColor);
        }

        /// <summary>
        /// Sets the color of the track element of a scroll bar.
        /// </summary>
        [DomName("scrollbarTrackColor")]
        [DomAccessor(Accessors.Setter)]
        public static void SetScrollbarTrackColor(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ScrollbarTrackColor, value);
        }

        /// <summary>
        /// Gets a value that indicates the color to paint along the
        /// outline of a given graphical element.
        /// </summary>
        [DomName("stroke")]
        [DomAccessor(Accessors.Getter)]
        public static String GetStroke(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Stroke);
        }

        /// <summary>
        /// Sets a value that indicates the color to paint along the
        /// outline of a given graphical element.
        /// </summary>
        [DomName("stroke")]
        [DomAccessor(Accessors.Setter)]
        public static void SetStroke(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Stroke, value);
        }

        /// <summary>
        /// Gets one or more values that indicate the pattern of dashes
        /// and gaps used to stroke paths.
        /// </summary>
        [DomName("strokeDasharray")]
        [DomAccessor(Accessors.Getter)]
        public static String GetStrokeDashArray(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.StrokeDasharray);
        }

        /// <summary>
        /// Sets one or more values that indicate the pattern of dashes
        /// and gaps used to stroke paths.
        /// </summary>
        [DomName("strokeDasharray")]
        [DomAccessor(Accessors.Setter)]
        public static void SetStrokeDashArray(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.StrokeDasharray, value);
        }

        /// <summary>
        /// Gets a value that specifies the distance into the dash
        /// pattern to start the dash.
        /// </summary>
        [DomName("strokeDashoffset")]
        [DomAccessor(Accessors.Getter)]
        public static String GetStrokeDashOffset(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.StrokeDashoffset);
        }

        /// <summary>
        /// Sets a value that specifies the distance into the dash
        /// pattern to start the dash.
        /// </summary>
        [DomName("strokeDashoffset")]
        [DomAccessor(Accessors.Setter)]
        public static void SetStrokeDashOffset(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.StrokeDashoffset, value);
        }

        /// <summary>
        /// Gets a value that specifies the shape to be used at the end
        /// of open subpaths when they are stroked.
        /// </summary>
        [DomName("strokeLinecap")]
        [DomAccessor(Accessors.Getter)]
        public static String GetStrokeLineCap(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.StrokeLinecap);
        }

        /// <summary>
        /// Sets a value that specifies the shape to be used at the end
        /// of open subpaths when they are stroked.
        /// </summary>
        [DomName("strokeLinecap")]
        [DomAccessor(Accessors.Setter)]
        public static void SetStrokeLineCap(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.StrokeLinecap, value);
        }

        /// <summary>
        /// Gets a value that specifies the shape to be used at the
        /// corners of paths or basic shapes when they are stroked.
        /// </summary>
        [DomName("strokeLinejoin")]
        [DomAccessor(Accessors.Getter)]
        public static String GetStrokeLineJoin(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.StrokeLinejoin);
        }

        /// <summary>
        /// Sets a value that specifies the shape to be used at the
        /// corners of paths or basic shapes when they are stroked.
        /// </summary>
        [DomName("strokeLinejoin")]
        [DomAccessor(Accessors.Setter)]
        public static void SetStrokeLineJoin(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.StrokeLinejoin, value);
        }

        /// <summary>
        /// Gets a value that indicates the limit on the ratio of the
        /// length of miter joins (as specified in the StrokeLinejoin 
        /// property).
        /// </summary>
        [DomName("strokeMiterlimit")]
        [DomAccessor(Accessors.Getter)]
        public static String GetStrokeMiterLimit(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.StrokeMiterlimit);
        }

        /// <summary>
        /// Sets a value that indicates the limit on the ratio of the
        /// length of miter joins (as specified in the StrokeLinejoin 
        /// property).
        /// </summary>
        [DomName("strokeMiterlimit")]
        [DomAccessor(Accessors.Setter)]
        public static void SetStrokeMiterLimit(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.StrokeMiterlimit, value);
        }

        /// <summary>
        /// Gets a value that specifies the opacity of the painting 
        /// operation that is used to stroke the current object.
        /// </summary>
        [DomName("strokeOpacity")]
        [DomAccessor(Accessors.Getter)]
        public static String GetStrokeOpacity(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.StrokeOpacity);
        }

        /// <summary>
        /// Sets a value that specifies the opacity of the painting 
        /// operation that is used to stroke the current object.
        /// </summary>
        [DomName("strokeOpacity")]
        [DomAccessor(Accessors.Setter)]
        public static void SetStrokeOpacity(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.StrokeOpacity, value);
        }

        /// <summary>
        /// Gets a value that specifies the width of the stroke on the
        /// current object.
        /// </summary>
        [DomName("strokeWidth")]
        [DomAccessor(Accessors.Getter)]
        public static String GetStrokeWidth(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.StrokeWidth);
        }

        /// <summary>
        /// Sets a value that specifies the width of the stroke on the
        /// current object.
        /// </summary>
        [DomName("strokeWidth")]
        [DomAccessor(Accessors.Setter)]
        public static void SetStrokeWidth(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.StrokeWidth, value);
        }

        /// <summary>
        /// Gets a string that indicates whether the table layout is
        /// fixed.
        /// </summary>
        [DomName("tableLayout")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTableLayout(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.TableLayout);
        }

        /// <summary>
        /// Sets a string that indicates whether the table layout is
        /// fixed.
        /// </summary>
        [DomName("tableLayout")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTableLayout(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.TableLayout, value);
        }

        /// <summary>
        /// Gets whether the text in the object is left-aligned,
        /// right-aligned, centered, or justified.
        /// </summary>
        [DomName("textAlign")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextAlign(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.TextAlign);
        }

        /// <summary>
        /// Sets whether the text in the object is left-aligned,
        /// right-aligned, centered, or justified.
        /// </summary>
        [DomName("textAlign")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextAlign(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.TextAlign, value);
        }


        /// <summary>
        /// Gets a value that indicates how to align the last line or
        /// only line of text in the specified object.
        /// </summary>
        [DomName("textAlignLast")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextAlignLast(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.TextAlignLast);
        }


        /// <summary>
        /// Sets a value that indicates how to align the last line or
        /// only line of text in the specified object.
        /// </summary>
        [DomName("textAlignLast")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextAlignLast(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.TextAlignLast, value);
        }

        /// <summary>
        /// Gets aligns a string of text relative to the specified point.
        /// </summary>
        [DomName("textAnchor")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextAnchor(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.TextAnchor);
        }

        /// <summary>
        /// Sets aligns a string of text relative to the specified point.
        /// </summary>
        [DomName("textAnchor")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextAnchor(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.TextAnchor, value);
        }

        /// <summary>
        /// Gets the autospacing and narrow space width adjustment of
        /// text.
        /// </summary>
        [DomName("textAutospace")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextAutospace(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.TextAutospace);
        }

        /// <summary>
        /// Sets the autospacing and narrow space width adjustment of
        /// text.
        /// </summary>
        [DomName("textAutospace")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextAutospace(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.TextAutospace, value);
        }

        /// <summary>
        /// Gets a value that indicates whether the text in the object
        /// has blink, line-through, overline, or underline decorations.
        /// </summary>
        [DomName("textDecoration")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextDecoration(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.TextDecoration);
        }

        /// <summary>
        /// Sets a value that indicates whether the text in the object
        /// has blink, line-through, overline, or underline decorations.
        /// </summary>
        [DomName("textDecoration")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextDecoration(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.TextDecoration, value);
        }

        /// <summary>
        /// Gets the indentation of the first line of text in the
        /// object.
        /// </summary>
        [DomName("textIndent")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextIndent(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.TextIndent);
        }

        /// <summary>
        /// Sets the indentation of the first line of text in the
        /// object.
        /// </summary>
        [DomName("textIndent")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextIndent(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.TextIndent, value);
        }

        /// <summary>
        /// Gets the type of alignment used to justify text in the
        /// object.
        /// </summary>
        [DomName("textJustify")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextJustify(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.TextJustify);
        }

        /// <summary>
        /// Sets the type of alignment used to justify text in the
        /// object.
        /// </summary>
        [DomName("textJustify")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextJustify(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.TextJustify, value);
        }

        /// <summary>
        /// Gets a value that indicates whether to render ellipses
        /// (...) to indicate text overflow.
        /// </summary>
        [DomName("textOverflow")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextOverflow(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.TextOverflow);
        }

        /// <summary>
        /// Sets a value that indicates whether to render ellipses
        /// (...) to indicate text overflow.
        /// </summary>
        [DomName("textOverflow")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextOverflow(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.TextOverflow, value);
        }

        /// <summary>
        /// Gets a comma-separated list of shadows that attaches one or
        /// more drop shadows to the specified text.
        /// </summary>
        [DomName("textShadow")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextShadow(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.TextShadow);
        }

        /// <summary>
        /// Sets a comma-separated list of shadows that attaches one or
        /// more drop shadows to the specified text.
        /// </summary>
        [DomName("textShadow")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextShadow(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.TextShadow, value);
        }

        /// <summary>
        /// Gets the rendering of the text in the object.
        /// </summary>
        [DomName("textTransform")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextTransform(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.TextTransform);
        }

        /// <summary>
        /// Sets the rendering of the text in the object.
        /// </summary>
        [DomName("textTransform")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextTransform(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.TextTransform, value);
        }

        /// <summary>
        /// Gets the position of the underline decoration that is set
        /// through the text-decoration property of the object.
        /// </summary>
        [DomName("textUnderlinePosition")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTextUnderlinePosition(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.TextUnderlinePosition);
        }

        /// <summary>
        /// Sets the position of the underline decoration that is set
        /// through the text-decoration property of the object.
        /// </summary>
        [DomName("textUnderlinePosition")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTextUnderlinePosition(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.TextUnderlinePosition, value);
        }

        /// <summary>
        /// Gets the position of the object relative to the top of the
        /// next positioned object in the document hierarchy.
        /// </summary>
        [DomName("top")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTop(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Top);
        }

        /// <summary>
        /// Sets the position of the object relative to the top of the
        /// next positioned object in the document hierarchy.
        /// </summary>
        [DomName("top")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTop(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Top, value);
        }

        /// <summary>
        /// Gets a list of one or more transform functions that specify
        /// how to translate, rotate, or scale an element in 2-D or 3-D space.
        /// </summary>
        [DomName("transform")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTransform(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Transform);
        }

        /// <summary>
        /// Sets a list of one or more transform functions that specify
        /// how to translate, rotate, or scale an element in 2-D or 3-D space.
        /// </summary>
        [DomName("transform")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTransform(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Transform, value);
        }

        /// <summary>
        /// Gets one or two values that establish the origin of 
        /// transformation for an element.
        /// </summary>
        [DomName("transformOrigin")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTransformOrigin(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.TransformOrigin);
        }

        /// <summary>
        /// Sets one or two values that establish the origin of 
        /// transformation for an element.
        /// </summary>
        [DomName("transformOrigin")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTransformOrigin(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.TransformOrigin, value);
        }

        /// <summary>
        /// Gets a value that specifies how child elements of the
        /// object are rendered in 3-D space.
        /// </summary>
        [DomName("transformStyle")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTransformStyle(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.TransformStyle);
        }

        /// <summary>
        /// Sets a value that specifies how child elements of the
        /// object are rendered in 3-D space.
        /// </summary>
        [DomName("transformStyle")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTransformStyle(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.TransformStyle, value);
        }

        /// <summary>
        /// Gets one or more shorthand values that specify the 
        /// transition properties for a set of corresponding object properties 
        /// identified in the transition-property property.
        /// </summary>
        [DomName("transition")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTransition(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Transition);
        }

        /// <summary>
        /// Sets one or more shorthand values that specify the 
        /// transition properties for a set of corresponding object properties 
        /// identified in the transition-property property.
        /// </summary>
        [DomName("transition")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTransition(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Transition, value);
        }

        /// <summary>
        /// Gets one or more values that specify the offset within a
        /// transition (the amount of time from the start of a transition) 
        /// before the transition is displayed  for a set of corresponding 
        /// object properties identified in the transition property.
        /// </summary>
        [DomName("transitionDelay")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTransitionDelay(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.TransitionDelay);
        }

        /// <summary>
        /// Sets one or more values that specify the offset within a
        /// transition (the amount of time from the start of a transition) 
        /// before the transition is displayed  for a set of corresponding 
        /// object properties identified in the transition property.
        /// </summary>
        [DomName("transitionDelay")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTransitionDelay(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.TransitionDelay, value);
        }

        /// <summary>
        /// Gets one or more values that specify the durations of 
        /// transitions on a set of corresponding object properties identified 
        /// in the transition-property property.
        /// </summary>
        [DomName("transitionDuration")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTransitionDuration(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.TransitionDuration);
        }

        /// <summary>
        /// Sets one or more values that specify the durations of 
        /// transitions on a set of corresponding object properties identified 
        /// in the transition-property property.
        /// </summary>
        [DomName("transitionDuration")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTransitionDuration(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.TransitionDuration, value);
        }

        /// <summary>
        /// Gets a value that identifies the CSS property name or names
        /// to which the transition effect (defined by the transition-duration, 
        /// transition-timing-function, and transition-delay properties) is 
        /// applied when a new property value is specified.
        /// </summary>
        [DomName("transitionProperty")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTransitionProperty(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.TransitionProperty);
        }

        /// <summary>
        /// Sets a value that identifies the CSS property name or names
        /// to which the transition effect (defined by the transition-duration, 
        /// transition-timing-function, and transition-delay properties) is 
        /// applied when a new property value is specified.
        /// </summary>
        [DomName("transitionProperty")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTransitionProperty(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.TransitionProperty, value);
        }

        /// <summary>
        /// Gets one or more values that specify the intermediate 
        /// property values to be used during a transition on a set of 
        /// corresponding object properties identified in the 
        /// transition-property property.
        /// </summary>
        [DomName("transitionTimingFunction")]
        [DomAccessor(Accessors.Getter)]
        public static String GetTransitionTimingFunction(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.TransitionTimingFunction);
        }

        /// <summary>
        /// Sets one or more values that specify the intermediate 
        /// property values to be used during a transition on a set of 
        /// corresponding object properties identified in the 
        /// transition-property property.
        /// </summary>
        [DomName("transitionTimingFunction")]
        [DomAccessor(Accessors.Setter)]
        public static void SetTransitionTimingFunction(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.TransitionTimingFunction, value);
        }

        /// <summary>
        /// Gets the level of embedding with respect to the 
        /// bidirectional algorithm.
        /// </summary>
        [DomName("unicodeBidi")]
        [DomAccessor(Accessors.Getter)]
        public static String GetUnicodeBidi(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.UnicodeBidi);
        }

        /// <summary>
        /// Sets the level of embedding with respect to the 
        /// bidirectional algorithm.
        /// </summary>
        [DomName("unicodeBidi")]
        [DomAccessor(Accessors.Setter)]
        public static void SetUnicodeBidi(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.UnicodeBidi, value);
        }

        /// <summary>
        /// Gets the vertical alignment of the object.
        /// </summary>
        [DomName("verticalAlign")]
        [DomAccessor(Accessors.Getter)]
        public static String GetVerticalAlign(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.VerticalAlign);
        }

        /// <summary>
        /// Sets the vertical alignment of the object.
        /// </summary>
        [DomName("verticalAlign")]
        [DomAccessor(Accessors.Setter)]
        public static void SetVerticalAlign(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.VerticalAlign, value);
        }

        /// <summary>
        /// Gets whether the content of the object is displayed.
        /// </summary>
        [DomName("visibility")]
        [DomAccessor(Accessors.Getter)]
        public static String GetVisibility(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Visibility);
        }

        /// <summary>
        /// Sets whether the content of the object is displayed.
        /// </summary>
        [DomName("visibility")]
        [DomAccessor(Accessors.Setter)]
        public static void SetVisibility(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Visibility, value);
        }

        /// <summary>
        /// Gets a value that indicates whether lines are automatically
        /// broken inside the object.
        /// </summary>
        [DomName("whiteSpace")]
        [DomAccessor(Accessors.Getter)]
        public static String GetWhiteSpace(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.WhiteSpace);
        }

        /// <summary>
        /// Sets a value that indicates whether lines are automatically
        /// broken inside the object.
        /// </summary>
        [DomName("whiteSpace")]
        [DomAccessor(Accessors.Setter)]
        public static void SetWhiteSpace(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.WhiteSpace, value);
        }

        /// <summary>
        /// Gets the minimum number of lines of a paragraph that must
        /// appear at the top of a document.
        /// </summary>
        [DomName("widows")]
        [DomAccessor(Accessors.Getter)]
        public static String GetWidows(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Widows);
        }

        /// <summary>
        /// Sets the minimum number of lines of a paragraph that must
        /// appear at the top of a document.
        /// </summary>
        [DomName("widows")]
        [DomAccessor(Accessors.Setter)]
        public static void SetWidows(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Widows, value);
        }

        /// <summary>
        /// Gets the width of the object.
        /// </summary>
        [DomName("width")]
        [DomAccessor(Accessors.Getter)]
        public static String GetWidth(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Width);
        }

        /// <summary>
        /// Sets the width of the object.
        /// </summary>
        [DomName("width")]
        [DomAccessor(Accessors.Setter)]
        public static void SetWidth(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Width, value);
        }

        /// <summary>
        /// Gets line-breaking behavior within words, particularly 
        /// where multiple languages appear in the object.
        /// </summary>
        [DomName("wordBreak")]
        [DomAccessor(Accessors.Getter)]
        public static String GetWordBreak(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.WordBreak);
        }

        /// <summary>
        /// Sets line-breaking behavior within words, particularly 
        /// where multiple languages appear in the object.
        /// </summary>
        [DomName("wordBreak")]
        [DomAccessor(Accessors.Setter)]
        public static void SetWordBreak(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.WordBreak, value);
        }

        /// <summary>
        /// Gets the amount of additional space between words in the 
        /// object.
        /// </summary>
        [DomName("wordSpacing")]
        [DomAccessor(Accessors.Getter)]
        public static String GetWordSpacing(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.WordSpacing);
        }

        /// <summary>
        /// Sets the amount of additional space between words in the 
        /// object.
        /// </summary>
        [DomName("wordSpacing")]
        [DomAccessor(Accessors.Setter)]
        public static void SetWordSpacing(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.WordSpacing, value);
        }

        /// <summary>
        /// Gets whether to break words when the content exceeds the
        /// boundaries of its container.
        /// </summary>
        [DomName("wordWrap")]
        [DomAccessor(Accessors.Getter)]
        public static String GetWordWrap(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.WordWrap);
        }

        /// <summary>
        /// Sets whether to break words when the content exceeds the
        /// boundaries of its container.
        /// </summary>
        [DomName("wordWrap")]
        [DomAccessor(Accessors.Setter)]
        public static void SetWordWrap(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.WordWrap, value);
        }
        
        /// <summary>
        /// Gets the overflow-wrap value.
        /// </summary>
        [DomName("overflowWrap")]
        [DomAccessor(Accessors.Getter)]
        public static String GetOverflowWrap(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.OverflowWrap);
        }

        /// <summary>
        /// Sets the overflow-wrap value.
        /// </summary>
        [DomName("overflowWrap")]
        [DomAccessor(Accessors.Setter)]
        public static void SetOverflowWrap(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.OverflowWrap, value);
        }

        /// <summary>
        /// Gets the direction and flow of the content in the object.
        /// </summary>
        [DomName("writingMode")]
        [DomAccessor(Accessors.Getter)]
        public static String GetWritingMode(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.WritingMode);
        }

        /// <summary>
        /// Sets the direction and flow of the content in the object.
        /// </summary>
        [DomName("writingMode")]
        [DomAccessor(Accessors.Setter)]
        public static void SetWritingMode(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.WritingMode, value);
        }

        /// <summary>
        /// Gets the stacking order of positioned objects.
        /// </summary>
        [DomName("zIndex")]
        [DomAccessor(Accessors.Getter)]
        public static String GetZIndex(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.ZIndex);
        }

        /// <summary>
        /// Sets the stacking order of positioned objects.
        /// </summary>
        [DomName("zIndex")]
        [DomAccessor(Accessors.Setter)]
        public static void SetZIndex(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.ZIndex, value);
        }

        /// <summary>
        /// Gets the magnification scale of the object.
        /// </summary>
        [DomName("zoom")]
        [DomAccessor(Accessors.Getter)]
        public static String GetZoom(this ICssStyleDeclaration style)
        {
            return style.GetPropertyValue(PropertyNames.Zoom);
        }

        /// <summary>
        /// Sets the magnification scale of the object.
        /// </summary>
        [DomName("zoom")]
        [DomAccessor(Accessors.Setter)]
        public static void SetZoom(this ICssStyleDeclaration style, String value)
        {
            style.SetProperty(PropertyNames.Zoom, value);
        }
    }
}
