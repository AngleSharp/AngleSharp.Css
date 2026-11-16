#nullable disable
namespace AngleSharp.Css.Tests.TypedOm
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.TypedOm;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using static CssConstructionFunctions;

    [TestFixture]
    public class StylePropertyMapTests
    {
        private static IElement CreateElement(String style)
        {
            var document = ParseDocument($"<div id=target style=\"{style}\"></div>");
            return document.GetElementById("target");
        }

        [Test]
        public void GetsTheTypedValuesOfTheInlineStyle()
        {
            var element = CreateElement("width: 10px; background-image: url(a.png), url(b.png); --foo: bar");
            var map = element.GetAttributeStyleMap();

            Assert.AreEqual("10px", ((CssUnitValue)map.Get("width")).ToString());
            Assert.AreEqual(2, map.GetAll("background-image").Count);
            Assert.IsInstanceOf<CssUnparsedValue>(map.Get("--foo"));
            Assert.AreEqual(3, map.Size);
        }

        [Test]
        public void MissingPropertiesHaveNoValue()
        {
            var map = CreateElement("width: 10px").GetAttributeStyleMap();

            Assert.IsFalse(map.Has("height"));
            Assert.IsNull(map.Get("height"));
            Assert.AreEqual(0, map.GetAll("height").Count);
            Assert.IsTrue(map.Has("WIDTH"));
        }

        [Test]
        public void UnknownPropertiesThrow()
        {
            var map = CreateElement("width: 10px").GetAttributeStyleMap();
            Assert.Throws<ArgumentException>(() => map.Get("foo-bar"));
            Assert.Throws<ArgumentException>(() => map.Set("foo-bar", CssHelpers.Px(1)));
        }

        [Test]
        public void IsTheSameObject()
        {
            var element = CreateElement("width: 10px");
            Assert.AreSame(element.GetAttributeStyleMap(), element.GetAttributeStyleMap());
        }

        [Test]
        public void ReflectsChangesOfTheStyle()
        {
            var element = CreateElement("width: 10px");
            var map = element.GetAttributeStyleMap();
            element.GetStyle().SetProperty("width", "20em");
            Assert.AreEqual("20em", map.Get("width").ToString());
        }

        [Test]
        public void SetUpdatesTheStyleAttribute()
        {
            var element = CreateElement("width: 10px");
            var map = element.GetAttributeStyleMap();
            map.Set("width", CssHelpers.Px(20).Add(CssHelpers.Em(1)));

            Assert.AreEqual("calc(20px + 1em)", element.GetStyle().GetPropertyValue("width"));
            Assert.AreEqual("width: calc(20px + 1em)", element.GetAttribute("style"));
        }

        [Test]
        public void SetAcceptsText()
        {
            var element = CreateElement("");
            var map = element.GetAttributeStyleMap();
            map.Set("height", "5vh");

            Assert.AreEqual("5vh", element.GetStyle().GetPropertyValue("height"));
            Assert.IsInstanceOf<CssUnitValue>(map.Get("height"));
        }

        [Test]
        public void SetOfAnInvalidValueThrowsAndKeepsTheStyle()
        {
            var element = CreateElement("width: 10px");
            var map = element.GetAttributeStyleMap();

            Assert.Throws<ArgumentException>(() => map.Set("width", CssHelpers.Deg(1)));
            Assert.Throws<ArgumentException>(() => map.Set("width", "red"));
            Assert.AreEqual("10px", element.GetStyle().GetPropertyValue("width"));
        }

        [Test]
        public void SetOfMultipleValuesRequiresAListValuedProperty()
        {
            var element = CreateElement("");
            var map = element.GetAttributeStyleMap();

            Assert.Throws<ArgumentException>(() => map.Set("width", CssHelpers.Px(1), CssHelpers.Px(2)));
            map.Set("transition-duration", CssHelpers.S(1), CssHelpers.Ms(200));
            Assert.AreEqual("1s, 200ms", element.GetStyle().GetPropertyValue("transition-duration"));
        }

        [Test]
        public void AppendAddsToAListValuedProperty()
        {
            var element = CreateElement("background-image: url(a.png)");
            var map = element.GetAttributeStyleMap();
            map.Append("background-image", "url(b.png)");
            map.Append("background-image", map.Get("background-image"));

            Assert.AreEqual(3, map.GetAll("background-image").Count);
            Assert.AreEqual("url(\"a.png\"), url(\"b.png\"), url(\"a.png\")", element.GetStyle().GetPropertyValue("background-image"));
        }

        [Test]
        public void AppendToAPropertyThatIsNotListValuedThrows()
        {
            var map = CreateElement("width: 10px").GetAttributeStyleMap();
            Assert.Throws<ArgumentException>(() => map.Append("width", CssHelpers.Px(1)));
        }

        [Test]
        public void ValuesWithoutATypedRepresentationOnlyFitTheirProperty()
        {
            var element = CreateElement("color: red");
            var map = element.GetAttributeStyleMap();
            var color = map.Get("color");

            Assert.Throws<ArgumentException>(() => map.Set("background-color", color));
            map.Set("color", color);
            Assert.AreEqual("rgba(255, 0, 0, 1)", element.GetStyle().GetPropertyValue("color"));
        }

        [Test]
        public void SetKeepsThePriority()
        {
            var element = CreateElement("width: 10px !important");
            var map = element.GetAttributeStyleMap();
            map.Set("width", CssHelpers.Px(20));

            Assert.AreEqual("important", element.GetStyle().GetPropertyPriority("width"));
            Assert.AreEqual("20px", element.GetStyle().GetPropertyValue("width"));
        }

        [Test]
        public void SetOfAShorthand()
        {
            var element = CreateElement("");
            var map = element.GetAttributeStyleMap();
            map.Set("margin", "1px 2px");

            Assert.AreEqual("2px", element.GetStyle().GetPropertyValue("margin-left"));
            Assert.AreEqual("1px 2px", map.Get("margin").ToString());
            Assert.AreEqual("2px", map.Get("margin-left").ToString());
        }

        [Test]
        public void SetOfACustomProperty()
        {
            var element = CreateElement("");
            var map = element.GetAttributeStyleMap();
            map.Set("--foo", new CssUnparsedValue(new Object[] { "1px ", new CssVariableReferenceValue("--bar") }));

            Assert.AreEqual("1px var(--bar)", element.GetStyle().GetPropertyValue("--foo"));
            Assert.Throws<ArgumentException>(() => map.Append("--foo", "2px"));
        }

        [Test]
        public void SetOfAReference()
        {
            var element = CreateElement("");
            var map = element.GetAttributeStyleMap();
            map.Set("width", new CssUnparsedValue(new Object[] { new CssVariableReferenceValue("--w") }));

            Assert.AreEqual("var(--w)", element.GetStyle().GetPropertyValue("width"));
        }

        [Test]
        public void DeleteAndClear()
        {
            var element = CreateElement("width: 10px; height: 20px");
            var map = element.GetAttributeStyleMap();

            map.Delete("width");
            Assert.IsFalse(map.Has("width"));
            Assert.AreEqual("20px", element.GetStyle().GetPropertyValue("height"));

            map.Clear();
            Assert.AreEqual(0, map.Size);
            Assert.AreEqual(0, element.GetStyle().Length);
        }

        [Test]
        public void IteratesTheDeclarationsInOrder()
        {
            var map = CreateElement("width: 10px; --foo: bar; height: auto").GetAttributeStyleMap();
            var names = map.Select(m => m.Key).ToArray();
            CollectionAssert.AreEqual(new[] { "width", "--foo", "height" }, names);
            Assert.IsInstanceOf<CssKeywordValue>(map.Single(m => m.Key == "height").Value[0]);
        }

        [Test]
        public void StyleMapOfARule()
        {
            var parser = new CssParser();
            var sheet = parser.ParseStyleSheet(".a { width: 10px; transform: rotate(45deg) }");
            var rule = (ICssStyleRule)sheet.Rules[0];
            var map = rule.GetStyleMap();

            Assert.IsInstanceOf<CssTransformValue>(map.Get("transform"));
            map.Set("width", CssHelpers.Percent(50));
            Assert.AreEqual("50%", rule.Style.GetPropertyValue("width"));
            Assert.AreSame(map, rule.GetStyleMap());
        }

        [Test]
        public void ComputedStyleMap()
        {
            var document = ParseDocument("<style>div { width: 50px; font-size: 10px; transform: rotate(45deg) }</style><div id=target style=\"--x: 7px; height: var(--x); padding-left: 2em\"></div>");
            var element = document.GetElementById("target");
            var map = element.ComputedStyleMap();

            Assert.IsNotInstanceOf<IStylePropertyMap>(map);
            Assert.AreEqual("50px", map.Get("width").ToString());
            Assert.AreEqual("7px", map.Get("height").ToString());
            Assert.AreEqual("20px", map.Get("padding-left").ToString());
            Assert.IsInstanceOf<CssUnparsedValue>(map.Get("--x"));
            Assert.IsInstanceOf<CssTransformValue>(map.Get("transform"));
            Assert.AreEqual("block", ((CssKeywordValue)map.Get("display")).Value);
        }

        [Test]
        public void ComputedStyleMapIsSortedWithCustomPropertiesLast()
        {
            var document = ParseDocument("<div id=target style=\"--b: 1; width: 1px; --a: 2; height: 2px\"></div>");
            var names = document.GetElementById("target").ComputedStyleMap().Select(m => m.Key).ToList();
            var custom = names.Where(m => m.StartsWith("--")).ToList();

            CollectionAssert.AreEqual(new[] { "--a", "--b" }, custom);
            CollectionAssert.AreEqual(names.Where(m => !m.StartsWith("--")).OrderBy(m => m, StringComparer.Ordinal), names.Take(names.Count - 2));
        }
    }
}
