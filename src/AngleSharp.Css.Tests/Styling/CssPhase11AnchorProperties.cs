namespace AngleSharp.Css.Tests.Styling
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssPhase11AnchorPropertiesTests
    {
        [Test]
        public void CssAnchorNameInitial()
        {
            var snippet = "anchor-name: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("anchor-name", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssAnchorNameNone()
        {
            var snippet = "anchor-name: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("anchor-name", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssAnchorNameCustom()
        {
            var snippet = "anchor-name: my-anchor;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("anchor-name", property.Name);
            Assert.AreEqual("my-anchor", property.Value);
        }

        [Test]
        public void CssAnchorNameMultiple()
        {
            var snippet = "anchor-name: anchor-a, anchor-b;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("anchor-name", property.Name);
            Assert.AreEqual("anchor-a, anchor-b", property.Value);
        }

        [Test]
        public void CssAnchorNameInvalid()
        {
            var snippet = "anchor-name: 123;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("anchor-name", property.Name);
            Assert.AreEqual("", property.Value);
        }

        [Test]
        public void CssAnchorScopeInitial()
        {
            var snippet = "anchor-scope: all;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("anchor-scope", property.Name);
            Assert.AreEqual("all", property.Value);
        }

        [Test]
        public void CssAnchorScopeAll()
        {
            var snippet = "anchor-scope: all;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("anchor-scope", property.Name);
            Assert.AreEqual("all", property.Value);
        }

        [Test]
        public void CssAnchorScopeOwn()
        {
            var snippet = "anchor-scope: own;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("anchor-scope", property.Name);
            Assert.AreEqual("own", property.Value);
        }

        [Test]
        public void CssAnchorScopeIdentifier()
        {
            var snippet = "anchor-scope: my-scope;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("anchor-scope", property.Name);
            Assert.AreEqual("my-scope", property.Value);
        }

        [Test]
        public void CssAnchorScopeInvalid()
        {
            var snippet = "anchor-scope: invalid-value;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("anchor-scope", property.Name);
            Assert.AreEqual("invalid-value", property.Value);
        }

        [Test]
        public void CssPositionAnchorInitial()
        {
            var snippet = "position-anchor: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-anchor", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssPositionAnchorAuto()
        {
            var snippet = "position-anchor: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-anchor", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssPositionAnchorIdentifier()
        {
            var snippet = "position-anchor: my-anchor;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-anchor", property.Name);
            Assert.AreEqual("my-anchor", property.Value);
        }

        [Test]
        public void CssPositionAnchorInvalid()
        {
            var snippet = "position-anchor: 123;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-anchor", property.Name);
            Assert.AreEqual("", property.Value);
        }

        [Test]
        public void CssPositionAreaInitial()
        {
            var snippet = "position-area: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-area", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssPositionAreaNone()
        {
            var snippet = "position-area: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-area", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssPositionAreaTop()
        {
            var snippet = "position-area: top;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-area", property.Name);
            Assert.AreEqual("top", property.Value);
        }

        [Test]
        public void CssPositionAreaBottom()
        {
            var snippet = "position-area: bottom;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-area", property.Name);
            Assert.AreEqual("bottom", property.Value);
        }

        [Test]
        public void CssPositionAreaStart()
        {
            var snippet = "position-area: start;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-area", property.Name);
            Assert.AreEqual("start", property.Value);
        }

        [Test]
        public void CssPositionAreaEnd()
        {
            var snippet = "position-area: end;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-area", property.Name);
            Assert.AreEqual("end", property.Value);
        }

        [Test]
        public void CssPositionAreaCenter()
        {
            var snippet = "position-area: center;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-area", property.Name);
            Assert.AreEqual("center", property.Value);
        }

        [Test]
        public void CssPositionAreaSpanAll()
        {
            var snippet = "position-area: span-all;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-area", property.Name);
            Assert.AreEqual("span-all", property.Value);
        }

        [Test]
        public void CssPositionAreaSelfStart()
        {
            var snippet = "position-area: self-start;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-area", property.Name);
            Assert.AreEqual("self-start", property.Value);
        }

        [Test]
        public void CssPositionAreaSelfEnd()
        {
            var snippet = "position-area: self-end;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-area", property.Name);
            Assert.AreEqual("self-end", property.Value);
        }

        [Test]
        public void CssPositionAreaInvalid()
        {
            var snippet = "position-area: invalid;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-area", property.Name);
            Assert.AreEqual("", property.Value);
        }

        [Test]
        public void CssPositionTryFallbacksInitial()
        {
            var snippet = "position-try-fallbacks: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-try-fallbacks", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssPositionTryFallbacksNone()
        {
            var snippet = "position-try-fallbacks: none;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-try-fallbacks", property.Name);
            Assert.AreEqual("none", property.Value);
        }

        [Test]
        public void CssPositionTryOrderInitial()
        {
            var snippet = "position-try-order: normal;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-try-order", property.Name);
            Assert.AreEqual("normal", property.Value);
        }

        [Test]
        public void CssPositionTryOrderNormal()
        {
            var snippet = "position-try-order: normal;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-try-order", property.Name);
            Assert.AreEqual("normal", property.Value);
        }

        [Test]
        public void CssPositionTryOrderFlipBlock()
        {
            var snippet = "position-try-order: flip-block;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-try-order", property.Name);
            Assert.AreEqual("flip-block", property.Value);
        }

        [Test]
        public void CssPositionTryOrderFlipInline()
        {
            var snippet = "position-try-order: flip-inline;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-try-order", property.Name);
            Assert.AreEqual("flip-inline", property.Value);
        }

        [Test]
        public void CssPositionTryOrderFlipStart()
        {
            var snippet = "position-try-order: flip-start;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-try-order", property.Name);
            Assert.AreEqual("flip-start", property.Value);
        }

        [Test]
        public void CssPositionTryOrderFlipEnd()
        {
            var snippet = "position-try-order: flip-end;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-try-order", property.Name);
            Assert.AreEqual("flip-end", property.Value);
        }

        [Test]
        public void CssPositionTryOrderInvalid()
        {
            var snippet = "position-try-order: invalid;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-try-order", property.Name);
            Assert.AreEqual("", property.Value);
        }

        [Test]
        public void CssPositionVisibilityInitial()
        {
            var snippet = "position-visibility: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-visibility", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssPositionVisibilityAuto()
        {
            var snippet = "position-visibility: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-visibility", property.Name);
            Assert.AreEqual("auto", property.Value);
        }

        [Test]
        public void CssPositionVisibilityAlways()
        {
            var snippet = "position-visibility: always;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-visibility", property.Name);
            Assert.AreEqual("always", property.Value);
        }

        [Test]
        public void CssPositionVisibilityPreferHidden()
        {
            var snippet = "position-visibility: prefer-hidden;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-visibility", property.Name);
            Assert.AreEqual("prefer-hidden", property.Value);
        }

        [Test]
        public void CssPositionVisibilityPreferNoOverflow()
        {
            var snippet = "position-visibility: prefer-no-overflow;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-visibility", property.Name);
            Assert.AreEqual("prefer-no-overflow", property.Value);
        }

        [Test]
        public void CssPositionVisibilityInvalid()
        {
            var snippet = "position-visibility: invalid;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position-visibility", property.Name);
            Assert.AreEqual("", property.Value);
        }
    }
}
