namespace AngleSharp.Css.Tests.Values
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class W3CSelectorTests
    {
        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-51.xml
        /// </summary>
        [Test]
        public void NegatedAttributeValueSelector()
        {
            var source = @"<style>@namespace a url(http://www.example.org/a);</style>
<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" test=""1"">
  <r test=""11"">This text should be</r>
  <r>in green characters</r>
</q>
<s xmlns=""http://www.example.org/a"">This text should be in green characters</s>
<p>This text should be in green characters</p>
</div>";
            var document = ParseDocument(source);

            var selector1 = document.QuerySelectorAll("div.stub p");
            Assert.That(selector1.Length, Is.EqualTo(1));
            var selector2 = document.QuerySelectorAll("div.stub > a|*");
            Assert.That(selector2.Length, Is.EqualTo(2));
            var selector3 = document.QuerySelectorAll("div.stub *|*:not([test='1'])");
            Assert.That(selector3.Length, Is.EqualTo(4));
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-50.xml
        /// </summary>
        [Test]
        public void NegatedAttributeExistenceSelector()
        {
            var source = @"<style>@namespace a url(http://www.example.org/a);</style>
<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" test=""1"">
  <r>This text should be in green characters</r>
</q>
<s xmlns=""http://www.example.org/a"">This text should be in green characters</s>
</div>";
            var document = ParseDocument(source);

            var selector1 = document.QuerySelectorAll("a|*");
            Assert.That(selector1.Length, Is.EqualTo(2));
            var selector2 = document.QuerySelectorAll("div.stub *|*:not([test])");
            Assert.That(selector2.Length, Is.EqualTo(2));
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-52.xml
        /// </summary>
        [Test]
        public void NegatedAttributeSpaceSeparatedValueSelector()
        {
            var source = @"<style>@namespace a url(http://www.example.org/a);@namespace b url(http://www.example.org/b);</style>
<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" test=""bar foo tut"">
  <r test=""tut foofoo bar"">This text should be</r>
  <r>in green characters</r>
</q>
<s xmlns=""http://www.example.org/a"" test=""bar tut"">This text should be in green characters</s>
<t xmlns=""http://www.example.org/a"">This text should be in green characters</t>
<p class=""tit foo1 tut"">This text should be in green characters</p>
<u xmlns=""http://www.example.org/b"" test=""tit foo2 tut"">This text should be in green characters</u>
</div>";
            var document = ParseDocument(source);

            var selector1 = document.QuerySelectorAll("div.stub p");
            Assert.That(selector1.Length, Is.EqualTo(1));
            var selector2 = document.QuerySelectorAll("div.stub > a|*, div.stub > b|*");
            Assert.That(selector2.Length, Is.EqualTo(4));
            var selector3 = document.QuerySelectorAll("div.stub *|*:not([test~='foo'])");
            Assert.That(selector3.Length, Is.EqualTo(6));
            var selector4 = document.QuerySelectorAll("div.stub *|p:not([class~='foo'])");
            Assert.That(selector4.Length, Is.EqualTo(1));
            var selector5 = document.QuerySelectorAll("div.stub b|*[test~='foo2']");
            Assert.That(selector5.Length, Is.EqualTo(1));
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-53.xml
        /// </summary>
        [Test]
        public void NegatedAttributeDashSeparatedValueSelector()
        {
            var source = @"<style>@namespace a url(http://www.example.org/a);@namespace b url(http://www.example.org/b);</style>
<div xmlns=""http://www.w3.org/1999/xhtml"" class=""stub"">
<q xmlns=""http://www.example.org/a"" test=""foo-bar"">
  <r test=""foo-bartut"">This text should be</r>
  <r>in green characters</r>
</q>
<s xmlns=""http://www.example.org/a"" test=""bar tut"">This text should be in green characters</s>
<t xmlns=""http://www.example.org/a"">This text should be in green characters</t>
<p class=""en-uk"">This text should be in green characters</p>
<u xmlns=""http://www.example.org/b"" test=""foo2-bar-lol"">This text should be in green characters</u>
</div>";
            var document = ParseDocument(source);

            var selector1 = document.QuerySelectorAll("div.stub p");
            Assert.That(selector1.Length, Is.EqualTo(1));
            var selector2 = document.QuerySelectorAll("div.stub > a|*, div.stub > b|*");
            Assert.That(selector2.Length, Is.EqualTo(4));
            var selector3 = document.QuerySelectorAll("div.stub *|*:not([test|='foo-bar'])");
            Assert.That(selector3.Length, Is.EqualTo(6));
            var selector4 = document.QuerySelectorAll("div.stub *|p:not([lang|='en-us'])");
            Assert.That(selector4.Length, Is.EqualTo(1));
            var selector5 = document.QuerySelectorAll("div.stub b|*[test|='foo2-bar']");
            Assert.That(selector5.Length, Is.EqualTo(1));
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-91.xml
        /// </summary>
        [Test]
        public void TypeElementSelectorWithDeclaredNamespace()
        {
            var source = @"<style>@namespace test url(http://www.example.org/a);</style>
<testa xmlns=""http://www.example.org/a"">This paragraph should have a green background</testa>";
            var document = ParseDocument(source);

            var selector1 = document.QuerySelectorAll("testa");
            Assert.That(selector1.Length, Is.EqualTo(1));
            var selector2 = document.QuerySelectorAll("test|testa");
            Assert.That(selector2.Length, Is.EqualTo(1));
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-94.xml
        /// </summary>
        [Test]
        public void UniversalSelectorWithDeclaredNamespaceA()
        {
            var source = @"<style>@namespace a url(http://www.example.org/a);@namespace b url(http://www.example.org/b);</style>
<p xmlns=""http://www.w3.org/1999/xhtml"">This line should be unstyled.</p>
<p xmlns=""http://www.example.org/b"">This line should have a green background.</p>
<q xmlns=""http://www.example.org/b"">This line should have a green background.</q>
<p xmlns=""http://www.example.org/a"">This line should be unstyleed.</p>
<p xmlns=""http://www.example.org/b"">This line should have a green background.</p>";
            var document = ParseDocument(source);

            var selector1 = document.QuerySelectorAll("p,q");
            Assert.That(selector1.Length, Is.EqualTo(5));
            var selector2 = document.QuerySelectorAll("b|*");
            Assert.That(selector2.Length, Is.EqualTo(3));
        }

        /// <summary>
        /// Test taken from http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/css3-modsel-94b.xml
        /// </summary>
        [Test]
        public void UniversalSelectorWithDeclaredNamespaceB()
        {
            var source = @"<style>@namespace a url(http://www.example.org/a);@namespace b url(http://www.example.org/b);</style>
<p xmlns=""http://www.w3.org/1999/xhtml"">This line should be unstyled.</p>
<p xmlns=""http://www.example.org/b"" test=""test"">This line should have a green background.</p>
<q xmlns=""http://www.example.org/b"" test=""test"">This line should have a green background.</q>
<p xmlns=""http://www.example.org/a"">This line should be unstyled.</p>
<p xmlns=""http://www.example.org/b"" test=""test"">This line should have a green background.</p>";
            var document = ParseDocument(source);

            var selector1 = document.QuerySelectorAll("p,q");
            Assert.That(selector1.Length, Is.EqualTo(5));
            var selector2 = document.QuerySelectorAll("b|*");
            Assert.That(selector2.Length, Is.EqualTo(3));
            var selector3 = document.QuerySelectorAll("[test]");
            Assert.That(selector3.Length, Is.EqualTo(3));
        }
    }
}
