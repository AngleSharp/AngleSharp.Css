#nullable disable
namespace AngleSharp.Css.Tests.Declarations
{
    using AngleSharp.Css.Dom;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static CssConstructionFunctions;

    /// <summary>
    /// Setting a declaration builds a fresh property and assigns its value, which always
    /// replaces the raw value it starts with. These cases pin down that re-declaring a
    /// property - longhand over shorthand, shorthand over longhand, important over
    /// normal, or an invalid value over a valid one - does not depend on the new property
    /// being seeded from the old one first.
    ///
    /// The expectations are a captured baseline of the behaviour, not a statement about
    /// what the spec requires.
    /// </summary>
    [TestFixture]
    public class SetPropertyOverwriteTests
    {
        private static readonly (String Input, String Expected)[] Cases =
        {
            ("color:red;color:blue", "color: rgba(0, 0, 255, 1)"),
            ("color:blue;color:red", "color: rgba(255, 0, 0, 1)"),
            ("color:red;color:notacolor", "color: rgba(255, 0, 0, 1)"),
            ("color:red !important;color:blue", "color: rgba(255, 0, 0, 1) !important"),
            ("color:red;color:blue !important", "color: rgba(0, 0, 255, 1) !important"),
            ("color:red !important;color:blue !important", "color: rgba(0, 0, 255, 1) !important"),
            ("--x:1;--x:2", "--x: 2"),
            ("--x:1;color:red", "--x: 1; color: rgba(255, 0, 0, 1)"),
            ("margin:1px;margin-top:2px", "margin-bottom: 1px; margin-left: 1px; margin-right: 1px; margin-top: 2px"),
            ("margin-top:2px;margin:1px", "margin-bottom: 1px; margin-left: 1px; margin-right: 1px; margin-top: 1px"),
            ("margin:1px;margin:notalength", "margin-bottom: 1px; margin-left: 1px; margin-right: 1px; margin-top: 1px"),
            ("margin:1px !important;margin-top:2px", "margin-bottom: 1px !important; margin-left: 1px !important; margin-right: 1px !important; margin-top: 1px !important"),
            ("padding:1px 2px;padding-left:9px;padding:3px", "padding-bottom: 3px; padding-left: 3px; padding-right: 3px; padding-top: 3px"),
            ("border-radius:4px;border-top-left-radius:9px", "border-bottom-left-radius: 4px; border-bottom-right-radius: 4px; border-top-left-radius: 9px; border-top-right-radius: 4px"),
            ("flex:1 1 auto;flex-grow:5", "flex-basis: auto; flex-grow: 5; flex-shrink: 1"),
            ("overflow:hidden;overflow-x:scroll", "overflow-x: scroll; overflow: hidden"),
            ("grid-area:a;grid-row:2", "grid-column-end: a; grid-column-start: a; grid-row-end: auto; grid-row-start: 2"),
            ("grid-row:2;grid-area:a", "grid-column-end: a; grid-column-start: a; grid-row-end: a; grid-row-start: a"),
            ("transition:all .3s;transition-duration:1s", "transition-delay: initial; transition-duration: 1s; transition-property: all; transition-timing-function: initial"),
            ("font:bold 14px/1.2 Arial;font-size:20px", "font-family: Arial; font-size: 20px; font-stretch: ; font-style: ; font-variant: ; font-weight: bold; line-height: 1.2"),
            ("font-size:20px;font:bold 14px/1.2 Arial", "font-family: Arial; font-size: 14px; font-stretch: ; font-style: ; font-variant: ; font-weight: bold; line-height: 1.2"),
            ("border:1px solid red;border-width:3px", "border-bottom-color: rgba(255, 0, 0, 1); border-bottom-style: solid; border-bottom-width: 3px; border-left-color: rgba(255, 0, 0, 1); border-left-style: solid; border-left-width: 3px; border-right-color: rgba(255, 0, 0, 1); border-right-style: solid; border-right-width: 3px; border-top-color: rgba(255, 0, 0, 1); border-top-style: solid; border-top-width: 3px"),
            ("border-width:3px;border:1px solid red", "border-bottom-color: rgba(255, 0, 0, 1); border-bottom-style: solid; border-bottom-width: 1px; border-left-color: rgba(255, 0, 0, 1); border-left-style: solid; border-left-width: 1px; border-right-color: rgba(255, 0, 0, 1); border-right-style: solid; border-right-width: 1px; border-top-color: rgba(255, 0, 0, 1); border-top-style: solid; border-top-width: 1px"),
            ("background:red;background:blue", "background-attachment: initial; background-clip: initial; background-color: rgba(0, 0, 255, 1); background-image: initial; background-origin: initial; background-position-x: initial; background-position-y: initial; background-repeat-x: initial; background-repeat-y: initial; background-size: initial"),
            ("background:red;background-color:blue", "background-attachment: initial; background-clip: initial; background-color: rgba(0, 0, 255, 1); background-image: initial; background-origin: initial; background-position-x: initial; background-position-y: initial; background-repeat-x: initial; background-repeat-y: initial; background-size: initial"),
            ("background-color:blue;background:red", "background-attachment: initial; background-clip: initial; background-color: rgba(255, 0, 0, 1); background-image: initial; background-origin: initial; background-position-x: initial; background-position-y: initial; background-repeat-x: initial; background-repeat-y: initial; background-size: initial"),
            ("background:red;background:", "background-attachment: initial; background-clip: initial; background-color: rgba(255, 0, 0, 1); background-image: initial; background-origin: initial; background-position-x: initial; background-position-y: initial; background-repeat-x: initial; background-repeat-y: initial; background-size: initial"),
            ("background:linear-gradient(red,blue);background:none", "background-attachment: initial; background-clip: initial; background-color: initial; background-image: none; background-origin: initial; background-position-x: initial; background-position-y: initial; background-repeat-x: initial; background-repeat-y: initial; background-size: initial"),
        };

        [Test]
        public void RedeclaringAPropertyProducesTheExpectedBlock()
        {
            var failures = new List<String>();

            foreach (var (input, expected) in Cases)
            {
                var actual = Describe(ParseDeclarations(input));

                if (!String.Equals(actual, expected, StringComparison.Ordinal))
                {
                    failures.Add($"{input}{Environment.NewLine}  expected: {expected}{Environment.NewLine}  actual:   {actual}");
                }
            }

            Assert.That(failures, Is.Empty, String.Join(Environment.NewLine, failures));
        }

        private static String Describe(ICssStyleDeclaration style)
        {
            var parts = style.Select(p => $"{p.Name}: {p.Value}{(p.IsImportant ? " !important" : String.Empty)}").ToList();
            parts.Sort(StringComparer.Ordinal);
            return String.Join("; ", parts);
        }
    }
}
