﻿namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents the CSS style sheet for storing CSS styles.
    /// </summary>
    [DomName("CSSStyleSheet")]
    public interface ICssStyleSheet : IStyleSheet
    {
        /// <summary>
        /// Gets the @import rule if the stylesheet was importated otherwise it
        /// returns null.
        /// </summary>
        [DomName("ownerRule")]
        ICssRule OwnerRule { get; }

        /// <summary>
        /// Gets a CSSRuleList of the CSS rules in the style sheet.
        /// </summary>
        [DomName("cssRules")]
        [DomName("rules")]
        ICssRuleList Rules { get; }

        /// <summary>
        /// Gets the parent stylesheet for of the current sheet.
        /// </summary>
        [DomName("parentStyleSheet")]
        ICssStyleSheet Parent { get; }

        /// <summary>
        /// Inserts a new style rule into the current style sheet.
        /// </summary>
        /// <param name="rule">
        /// A string containing the rule to be inserted.
        /// </param>
        /// <param name="index">
        /// The index representing the position to be inserted.
        /// </param>
        /// <returns>The index of insertation.</returns>
        [DomName("insertRule")]
        Int32 Insert(String rule, Int32 index);

        /// <summary>
        /// Removes a style rule from the current style sheet object.
        /// </summary>
        /// <param name="index">
        /// The index representing the position to be removed.
        /// </param>
        [DomName("deleteRule")]
        void RemoveAt(Int32 index);

        /// <summary>
        /// Sets the owner of the sheet.
        /// </summary>
        /// <param name="rule">The owning rule.</param>
        void SetOwner(ICssRule rule);

        /// <summary>
        /// Sets the parent of the sheet.
        /// </summary>
        /// <param name="parent">The parent sheet.</param>
        void SetParent(ICssStyleSheet parent);
    }
}
