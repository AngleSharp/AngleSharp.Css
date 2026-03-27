namespace AngleSharp.Css.Tests.Declarations
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
	public class CssStrokePropertyTests
	{
		[Test]
		public void CssStrokeColorRedLegal()
		{
			var snippet = "stroke: red";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("rgba(255, 0, 0, 1)"));
		}

		[Test]
		public void CssStrokeColorHexLegal()
		{
			var snippet = "stroke: #0F0";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("rgba(0, 255, 0, 1)"));
		}

		[Test]
		public void CssStrokeColorRgbaLegal()
		{
			var snippet = "stroke: rgba(1, 1, 1, 0)";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("rgba(1, 1, 1, 0)"));
		}

		[Test]
		public void CssStrokeColorRgbLegal()
		{
			var snippet = "stroke: rgb(1, 255, 100)";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("rgba(1, 255, 100, 1)"));
		}

		[Test]
		public void CssStrokeNoneLegal()
		{
			var snippet = "stroke: none";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("none"));
		}

		[Test]
		public void CssStrokeColorRedRedIllegal()
		{
			var snippet = "stroke: red red";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.False);
		}

		[Test]
		public void CssStrokeUrlLegal()
		{
			var snippet = "stroke: url(#linear)";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("url(\"#linear\")"));
		}


		[Test]
		public void CssStrokeDasharrayNumberNumberLegal()
		{
			var snippet = "stroke-dasharray: 5 5";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke-dasharray"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("5 5"));
		}

		[Test]
		public void CssStrokeDasharrayLengthLengthLegal()
		{
			var snippet = "stroke-dasharray: 5px 5em";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke-dasharray"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("5px 5em"));
		}

		[Test]
		public void CssStrokeDasharrayManyLegal()
		{
			var snippet = "stroke-dasharray: 1px 2em 3vh 4vw 5 6";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke-dasharray"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("1px 2em 3vh 4vw 5 6"));
		}

		[Test]
		public void CssStrokeDasharrayNoneLegal()
		{
			var snippet = "stroke-dasharray: none";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke-dasharray"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("none"));
		}

		[Test]
		public void CssStrokeDashoffsetLengthLegal()
		{
			var snippet = "stroke-dashoffset: 5px";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke-dashoffset"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("5px"));
		}

		[Test]
		public void CssStrokeDashoffsetLengthLengthIllegal()
		{
			var snippet = "stroke-dashoffset: 5px 5px";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke-dashoffset"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.False);
		}

		[Test]
		public void CssStrokeDashoffsetPercentLegal()
		{
			var snippet = "stroke-dashoffset: 50%";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke-dashoffset"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("50%"));
		}

		[Test]
		public void CssStrokeDashoffsetPercentPercentIllegal()
		{
			var snippet = "stroke-dashoffset: 50% 25%";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke-dashoffset"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.False);
		}

		[Test]
		public void CssStrokeLinecapButtLegal()
		{
			var snippet = "stroke-linecap: butt";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke-linecap"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("butt"));
		}

		[Test]
		public void CssStrokeLinecapRoundLegal()
		{
			var snippet = "stroke-linecap: round";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke-linecap"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("round"));
		}

		[Test]
		public void CssStrokeLinecapSquareLegal()
		{
			var snippet = "stroke-linecap: square";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke-linecap"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("square"));
		}

		[Test]
		public void CssStrokeLinecapNoneIllegal()
		{
			var snippet = "stroke-linecap: none";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke-linecap"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.False);
		}

		[Test]
		public void CssStrokeLinejoinMiterLegal()
		{
			var snippet = "stroke-linejoin: miter";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke-linejoin"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("miter"));
		}

		[Test]
		public void CssStrokeLinejoinRoundLegal()
		{
			var snippet = "stroke-linejoin: round";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke-linejoin"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("round"));
		}

		[Test]
		public void CssStrokeLinejoinBevelLegal()
		{
			var snippet = "stroke-linejoin: bevel";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke-linejoin"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("bevel"));
		}

		[Test]
		public void CssStrokeLinejoinNoneIllegal()
        {
			var snippet = "stroke-linejoin: none";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke-linejoin"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.False);
		}

		[Test]
		public void CssStrokeMiterlimitNumberLegal()
		{
			var snippet = "stroke-miterlimit: 2";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke-miterlimit"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("2"));
		}

		[Test]
		public void CssStrokeMiterlimitNumberIlegal()
		{
			var snippet = "stroke-miterlimit: 0.5";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke-miterlimit"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.False);
		}

		[Test]
		public void CssStrokeMiterlimitNumberNumberIlegal()
		{
			var snippet = "stroke-miterlimit: 2 0.5";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke-miterlimit"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.False);
		}

		[Test]
		public void CssStrokeOpacitytNumberLegal()
		{
			var snippet = "stroke-opacity: 0.5";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke-opacity"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("0.5"));
		}

		[Test]
		public void CssStrokeOpacityNumberNumberIllegal()
		{
			var snippet = "stroke-opacity: 0.5 0.5";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke-opacity"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.False);
		}
		
		[Test]
		public void CssStrokeWidthLengthLegal()
		{
			var snippet = "stroke-width: 5px";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke-width"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("5px"));
		}


		[Test]
		public void CssStrokeWidthPercentLegal()
		{
			var snippet = "stroke-width: 5%";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke-width"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.True);
			Assert.That(property.Value, Is.EqualTo("5%"));
		}

		[Test]
		public void CssStrokeWidthNoneIllegal()
		{
			var snippet = "stroke-width: none";
			var property = ParseDeclaration(snippet);
			Assert.That(property.Name, Is.EqualTo("stroke-width"));
			Assert.That(property.IsImportant, Is.False);
			Assert.That(property.IsInherited, Is.False);
			Assert.That(property.HasValue, Is.False);
		}

        [Test]
        public void CssStrokeWithoutUnit_Issue18()
        {
            var snippet = "stroke-width: 3";
            var property = ParseDeclaration(snippet);
            Assert.That(property.Name, Is.EqualTo("stroke-width"));
            Assert.That(property.IsImportant, Is.False);
            Assert.That(property.IsInherited, Is.False);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.Value, Is.EqualTo("3"));
        }
	}
}
