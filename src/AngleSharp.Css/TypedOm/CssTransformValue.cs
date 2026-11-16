namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Values;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a list of transformations, i.e., the value of transform.
    /// https://www.w3.org/TR/css-typed-om-1/#csstransformvalue
    /// </summary>
    [DomName("CSSTransformValue")]
    public sealed class CssTransformValue : CssStyleValue, IReadOnlyList<CssTransformComponent>
    {
        private readonly List<CssTransformComponent> _transforms;

        /// <summary>
        /// Creates a new list of transformations.
        /// </summary>
        /// <param name="transforms">The transformations, at least one.</param>
        /// <exception cref="ArgumentException">No transformation is given.</exception>
        [DomConstructor]
        public CssTransformValue(IEnumerable<CssTransformComponent> transforms)
        {
            _transforms = (transforms ?? throw new ArgumentNullException(nameof(transforms))).ToList();

            if (_transforms.Count == 0)
                throw new ArgumentException("At least one transformation is required.", nameof(transforms));

            if (_transforms.Any(m => m is null))
                throw new ArgumentNullException(nameof(transforms));
        }

        /// <summary>
        /// Gets or sets the transformation at the given index. Setting the index right
        /// after the last transformation appends one.
        /// </summary>
        [DomName("item")]
        public CssTransformComponent this[Int32 index]
        {
            get => _transforms[index];
            set
            {
                if (value is null)
                    throw new ArgumentNullException(nameof(value));

                if (index == _transforms.Count)
                {
                    _transforms.Add(value);
                }
                else
                {
                    _transforms[index] = value;
                }
            }
        }

        /// <summary>
        /// Gets the number of transformations.
        /// </summary>
        [DomName("length")]
        public Int32 Length => _transforms.Count;

        Int32 IReadOnlyCollection<CssTransformComponent>.Count => _transforms.Count;

        /// <summary>
        /// Gets if all the transformations are 2D.
        /// </summary>
        [DomName("is2D")]
        public Boolean Is2D => _transforms.All(m => m.Is2D);

        /// <summary>
        /// Computes the matrix of all transformations, applied in their order.
        /// </summary>
        /// <returns>The resulting transformation matrix.</returns>
        /// <exception cref="ArgumentException">A value cannot be converted to an absolute unit, e.g., em or %.</exception>
        [DomName("toMatrix")]
        public TransformMatrix ToMatrix() => _transforms
            .Select(m => m.ToMatrix())
            .Aggregate(CssTransformComponent.Multiply);

        /// <summary>
        /// Gets an enumerator over the transformations.
        /// </summary>
        public IEnumerator<CssTransformComponent> GetEnumerator() => _transforms.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Serializes the transformations, e.g., translate(10px, 20px) rotate(45deg).
        /// </summary>
        public override String ToString() => String.Join(" ", _transforms.Select(m => m.ToString()));
    }
}
