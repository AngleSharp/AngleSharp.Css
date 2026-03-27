namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssListPropertyTests
    {
        [Test]
        public void CssListStylePositionOutsideLegal()
        {
            var snippet = "list-style-position: outside ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("list-style-position"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("outside"));
        }

        [Test]
        public void CssListStylePositionOutsideIllegal()
        {
            var snippet = "list-style-position: out-side ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("list-style-position"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssListStylePositionNoneIllegal()
        {
            var snippet = "list-style-position: none ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("list-style-position"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.True);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssListStylePositionInsideLegal()
        {
            var snippet = "list-style-position: insiDe ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("list-style-position"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("inside"));
        }

        [Test]
        public void CssListStyleImageNoneLegal()
        {
            var snippet = "list-style-image: none ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("list-style-image"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssListStyleImageUrlLegal()
        {
            var snippet = "list-style-image: url(http://www.example.com/images/list.png)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("list-style-image"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("url(\"http://www.example.com/images/list.png\")"));
        }

        [Test]
        public void CssListStyleTypeDiscLegal()
        {
            var snippet = "list-style-type: disc ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("list-style-type"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("disc"));
        }

        [Test]
        public void CssListStyleTypeLowerAlphaLegal()
        {
            var snippet = "list-style-type: lower-ALPHA ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("list-style-type"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("lower-alpha"));
        }

        [Test]
        public void CssListStyleTypeGeorgianLegal()
        {
            var snippet = "list-style-type: georgian ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("list-style-type"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("georgian"));
        }

        [Test]
        public void CssListStyleTypeDecimalLeadingZeroLegal()
        {
            var snippet = "list-style-type: decimal-leading-zerO ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("list-style-type"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("decimal-leading-zero"));
        }

        [Test]
        public void CssListStyleTypeSomeValueLegal()
        {
            var snippet = "list-style-type: number ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("list-style-type"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("number"));
        }

        [Test]
        public void CssListStyleCircleLegal()
        {
            var snippet = "list-style: circle ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("list-style"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("circle"));
        }

        [Test]
        public void CssListStyleNone()
        {
            var snippet = "list-style: none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("list-style"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssListStyleSquareInsideLegal()
        {
            var snippet = "list-style: square inside ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("list-style"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("square inside"));
        }

        [Test]
        public void CssListStyleSquareImageInsideLegal()
        {
            var snippet = "list-style: square url('image.png') inside ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("list-style"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("square inside url(\"image.png\")"));
        }

        [Test]
        public void CssCounterResetLegal()
        {
            var snippet = "counter-reset: chapter section 1 page;";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("counter-reset"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("chapter 0 section 1 page 0"));
        }

        [Test]
        public void CssCounterResetSingleLegal()
        {
            var snippet = "counter-reset: counter-name";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("counter-reset"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("counter-name 0"));
        }

        [Test]
        public void CssCounterResetNoneLegal()
        {
            var snippet = "counter-reset: none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("counter-reset"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssCounterResetNumberIllegal()
        {
            var snippet = "counter-reset: 3";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("counter-reset"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssCounterResetNegativeLegal()
        {
            var snippet = "counter-reset  :  counter-name   -1";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("counter-reset"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("counter-name -1"));
        }

        [Test]
        public void CssCounterResetTwoCountersExplicitLegal()
        {
            var snippet = "counter-reset  :  counter1   1   counter2   4  ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("counter-reset"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("counter1 1 counter2 4"));
        }

        [Test]
        public void CssCounterIncrementNoneLegal()
        {
            var snippet = "counter-increment: none";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("counter-increment"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("none"));
        }

        [Test]
        public void CssCounterIncrementLegal()
        {
            var snippet = "counter-increment: chapter section 2 page";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("counter-increment"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("chapter 1 section 2 page 1"));
        }

        [Test]
        public void CssListStyleStringValue_Issue152()
        {
            var snippet = "list-style-type: \"-\"";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("list-style-type"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("\"-\""));
        }

        [Test]
        public void CssListStyleKannada_Issue152()
        {
            var snippet = "list-style-type: kannada";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("list-style-type"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("kannada"));
        }

        [Test]
        public void CssListStyleTradChineseInformal_Issue152()
        {
            var snippet = "list-style-type: trad-chinese-informal;";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("list-style-type"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("trad-chinese-informal"));
        }

        [Test]
        public void CssListStyleGeorgian_Issue152()
        {
            var snippet = "list-style-type: georgian";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("list-style-type"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("georgian"));
        }

        [Test]
        public void CssListStyleDecimal_Issue152()
        {
            var snippet = "list-style-type: decimal";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("list-style-type"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("decimal"));
        }

        [Test]
        public void CssListStyleSquare_Issue152()
        {
            var snippet = "list-style-type: square";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("list-style-type"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("square"));
        }

        [Test]
        public void CssListStyleCircle_Issue152()
        {
            var snippet = "list-style-type: circle";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("list-style-type"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("circle"));
        }

        [Test]
        public void CssListStyleSymbolsFunction_Issue152()
        {
            var snippet = "list-style-type: symbols(cyclic \"*\" \"†\" \"‡\")";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("list-style-type"));
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("symbols(cyclic \"*\" \"†\" \"‡\")"));
        }

        [Test]
        public void CssListStyleSymbolsFailingForWrongType_Issue152()
        {
            var snippet = "list-style-type: symbols(foo \"*\" \"†\" \"‡\")";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("list-style-type"));
            Assert.That(property.HasValue, Is.False);
        }
    }
}
