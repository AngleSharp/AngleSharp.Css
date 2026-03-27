namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class CssOutlinePropertyTests
    {
        [Test]
        public void CssOutlineStyleDottedLegal()
        {
            var snippet = "outline-style   :  dotTED";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("outline-style"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("dotted"));
        }

        [Test]
        public void CssOutlineStyleSolidLegal()
        {
            var snippet = "outline-style   :  solid";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("outline-style"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("solid"));
        }

        [Test]
        public void CssOutlineStyleNoIllegal()
        {
            var snippet = "outline-style   :  no";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("outline-style"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssOutlineColorInvertLegal()
        {
            var snippet = "outline-color :  invert ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("outline-color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("invert"));
        }

        [Test]
        public void CssOutlineColorHslLegal()
        {
            var snippet = "outline-color :  hsl(320, 80%, 50%) ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("outline-color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(230, 25, 162, 1)"));
        }

        [Test]
        public void CssOutlineColorHexLegal()
        {
            var snippet = "outline-color :  #0000FF ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("outline-color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(0, 0, 255, 1)"));
        }

        [Test]
        public void CssOutlineColorRedLegal()
        {
            var snippet = "outline-color :  red ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("outline-color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("rgba(255, 0, 0, 1)"));
        }

        [Test]
        public void CssOutlineColorIllegal()
        {
            var snippet = "outline-color :  blau ";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("outline-color"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssOutlineWidthThinImportantLegal()
        {
            var snippet = "outline-width :  thin !important";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("outline-width"));
            Assert.That(property.IsImportant, Is.True);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("1px"));
        }

        [Test]
        public void CssOutlineWidthNumberIllegal()
        {
            var snippet = "outline-width :  3";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("outline-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssOutlineWidthLengthLegal()
        {
            var snippet = "outline-width :  0.1em";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("outline-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0.1em"));
        }

        [Test]
        public void CssOutlineSingleLegal()
        {
            var snippet = "outline :  thin";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("outline"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("1px"));
        }

        [Test]
        public void CssOutlineDualLegal()
        {
            var snippet = "outline :  thin   invert";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("outline"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("1px invert"));
        }

        [Test]
        public void CssOutlineAllDottedLegal()
        {
            var snippet = "outline :  dotted 0.3em rgb(255, 255, 255)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("outline"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("0.3em dotted rgba(255, 255, 255, 1)"));
        }

        [Test]
        public void CssOutlineDoubleColorIllegal()
        {
            var snippet = "outline :  dotted #123456 rgb(255, 255, 255)";
            var property = ParseDeclaration(snippet);
            Assert.That(property.HasValue, Is.False);
        }

        [Test]
        public void CssOutlineAllSolidLegal()
        {
            var snippet = "outline :  1px solid #000";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("outline"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("1px solid rgba(0, 0, 0, 1)"));
        }

        [Test]
        public void CssOutlineAllColorNamedLegal()
        {
            var snippet = "outline :  solid black 1px";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("outline"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("1px solid rgba(0, 0, 0, 1)"));
        }
    }
}
