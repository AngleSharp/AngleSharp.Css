namespace AngleSharp.Css.Dom
{
    /// <summary>
    /// The values for pointer-event.
    /// </summary>
    public enum PointerEvent
    {
        /// <summary>
        /// The element is never the target of pointer events; however, pointer events may target its descendant elements if those descendants have pointer-events set to some other value. In these circumstances, pointer events will trigger event listeners on this parent element as appropriate on their way to/from the descendant during the event capture/bubble phases.
        /// </summary>
        None,
        /// <summary>
        /// The element behaves as it would if the pointer-events property were not specified. In SVG content, this value and the value visiblePainted have the same effect.
        /// </summary>
        Auto,
        /// <summary>
        /// SVG only. The element can only be the target of a pointer event when the visibility property is set to visible and e.g. when a mouse cursor is over the interior (i.e., 'fill') of the element and the fill property is set to a value other than none, or when a mouse cursor is over the perimeter (i.e., 'stroke') of the element and the stroke property is set to a value other than none.
        /// </summary>
        VisiblePainted,
        /// <summary>
        /// SVG only. The element can only be the target of a pointer event when the visibility property is set to visible and when e.g. a mouse cursor is over the interior (i.e., fill) of the element. The value of the fill property does not affect event processing.
        /// </summary>
        VisibleFill,
        /// <summary>
        /// SVG only. The element can only be the target of a pointer event when the visibility property is set to visible and e.g. when the mouse cursor is over the perimeter (i.e., stroke) of the element. The value of the stroke property does not affect event processing.
        /// </summary>
        VisibleStroke,
        /// <summary>
        /// SVG only. The element can be the target of a pointer event when the visibility property is set to visible and e.g. the mouse cursor is over either the interior (i.e., fill) or the perimeter (i.e., stroke) of the element. The values of the fill and stroke do not affect event processing.
        /// </summary>
        Visible,
        /// <summary>
        /// SVG only. The element can only be the target of a pointer event when e.g. the mouse cursor is over the interior (i.e., 'fill') of the element and the fill property is set to a value other than none, or when the mouse cursor is over the perimeter (i.e., 'stroke') of the element and the stroke property is set to a value other than none. The value of the visibility property does not affect event processing.
        /// </summary>
        Painted,
        /// <summary>
        /// SVG only. The element can only be the target of a pointer event when the pointer is over the interior (i.e., fill) of the element. The values of the fill and visibility properties do not affect event processing.
        /// </summary>
        Fill,
        /// <summary>
        /// SVG only. The element can only be the target of a pointer event when the pointer is over the perimeter (i.e., stroke) of the element. The values of the stroke and visibility properties do not affect event processing.
        /// </summary>
        Stroke,
        /// <summary>
        /// SVG only. The element can only be the target of a pointer event when the pointer is over the interior (i.e., fill) or the perimeter (i.e., stroke) of the element. The values of the fill, stroke, and visibility properties do not affect event processing.
        /// </summary>
        All,
    }
}
