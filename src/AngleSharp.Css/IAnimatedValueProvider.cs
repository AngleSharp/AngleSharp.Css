#nullable enable
namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Gives the values the running animations and transitions of elements have now, which a host
    /// following them (e.g. a renderer advancing its clock) knows. They are the animation origin of
    /// the cascade (CSS Cascade 5, 6.2): an animated value wins over the declarations of the element,
    /// unless one is important, so what depends on it follows, e.g. a var() of an animated custom
    /// property or the input properties of a paint worklet.
    /// </summary>
    public interface IAnimatedValueProvider
    {
        /// <summary>
        /// Gets the values the animations and transitions of the element give it now, by property
        /// name (e.g. --angle: 37deg), or null if none does.
        /// </summary>
        /// <param name="element">The element whose style is computed.</param>
        /// <param name="declared">
        /// What the cascade gave the element without the animated values, e.g. the value a
        /// transition goes to or the animation-name of the element.
        /// </param>
        /// <returns>The animated values, or null.</returns>
        IReadOnlyDictionary<String, String>? GetAnimatedValues(IElement element, ICssStyleDeclaration declared);
    }
}
