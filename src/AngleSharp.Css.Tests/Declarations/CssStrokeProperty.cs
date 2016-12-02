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
			Assert.AreEqual("stroke", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("rgba(255, 0, 0, 1)", property.Value);
		}

		[Test]
		public void CssStrokeColorHexLegal()
		{
			var snippet = "stroke: #0F0";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("rgba(0, 255, 0, 1)", property.Value);
		}

		[Test]
		public void CssStrokeColorRgbaLegal()
		{
			var snippet = "stroke: rgba(1, 1, 1, 0)";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("rgba(1, 1, 1, 0)", property.Value);
		}

		[Test]
		public void CssStrokeColorRgbLegal()
		{
			var snippet = "stroke: rgb(1, 255, 100)";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("rgba(1, 255, 100, 1)", property.Value);
		}

		[Test]
		public void CssStrokeNoneLegal()
		{
			var snippet = "stroke: none";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("none", property.Value);
		}

		[Test]
		public void CssStrokeColorRedRedIllegal()
		{
			var snippet = "stroke: red red";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.HasValue);
		}

		[Test]
		public void CssStrokeUrlLegal()
		{
			var snippet = "stroke: url(#linear)";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("url(\"#linear\")", property.Value);
		}


		[Test]
		public void CssStrokeDasharrayNumberNumberLegal()
		{
			var snippet = "stroke-dasharray: 5 5";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke-dasharray", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("5 5", property.Value);
		}

		[Test]
		public void CssStrokeDasharrayLengthLengthLegal()
		{
			var snippet = "stroke-dasharray: 5px 5em";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke-dasharray", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("5px 5em", property.Value);
		}

		[Test]
		public void CssStrokeDasharrayManyLegal()
		{
			var snippet = "stroke-dasharray: 1px 2em 3vh 4vw 5 6";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke-dasharray", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("1px 2em 3vh 4vw 5 6", property.Value);
		}

		[Test]
		public void CssStrokeDasharrayNoneLegal()
		{
			var snippet = "stroke-dasharray: none";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke-dasharray", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("none", property.Value);
		}

		[Test]
		public void CssStrokeDashoffsetLengthLegal()
		{
			var snippet = "stroke-dashoffset: 5px";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke-dashoffset", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("5px", property.Value);
		}

		[Test]
		public void CssStrokeDashoffsetLengthLengthIllegal()
		{
			var snippet = "stroke-dashoffset: 5px 5px";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke-dashoffset", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.HasValue);
		}

		[Test]
		public void CssStrokeDashoffsetPercentLegal()
		{
			var snippet = "stroke-dashoffset: 50%";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke-dashoffset", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("50%", property.Value);
		}

		[Test]
		public void CssStrokeDashoffsetPercentPercentIllegal()
		{
			var snippet = "stroke-dashoffset: 50% 25%";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke-dashoffset", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.HasValue);
		}

		[Test]
		public void CssStrokeLinecapButtLegal()
		{
			var snippet = "stroke-linecap: butt";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke-linecap", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("butt", property.Value);
		}

		[Test]
		public void CssStrokeLinecapRoundLegal()
		{
			var snippet = "stroke-linecap: round";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke-linecap", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("round", property.Value);
		}

		[Test]
		public void CssStrokeLinecapSquareLegal()
		{
			var snippet = "stroke-linecap: square";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke-linecap", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("square", property.Value);
		}

		[Test]
		public void CssStrokeLinecapNoneIllegal()
		{
			var snippet = "stroke-linecap: none";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke-linecap", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.HasValue);
		}

		[Test]
		public void CssStrokeLinejoinMiterLegal()
		{
			var snippet = "stroke-linejoin: miter";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke-linejoin", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("miter", property.Value);
		}

		[Test]
		public void CssStrokeLinejoinRoundLegal()
		{
			var snippet = "stroke-linejoin: round";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke-linejoin", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("round", property.Value);
		}

		[Test]
		public void CssStrokeLinejoinBevelLegal()
		{
			var snippet = "stroke-linejoin: bevel";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke-linejoin", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("bevel", property.Value);
		}

		[Test]
		public void CssStrokeLinejoinNoneIllegal()
        {
			var snippet = "stroke-linejoin: none";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke-linejoin", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.HasValue);
		}

		[Test]
		public void CssStrokeMiterlimitNumberLegal()
		{
			var snippet = "stroke-miterlimit: 2";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke-miterlimit", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("2", property.Value);
		}

		[Test]
		public void CssStrokeMiterlimitNumberIlegal()
		{
			var snippet = "stroke-miterlimit: 0.5";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke-miterlimit", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.HasValue);
		}

		[Test]
		public void CssStrokeMiterlimitNumberNumberIlegal()
		{
			var snippet = "stroke-miterlimit: 2 0.5";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke-miterlimit", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.HasValue);
		}

		[Test]
		public void CssStrokeOpacitytNumberLegal()
		{
			var snippet = "stroke-opacity: 0.5";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke-opacity", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("0.5", property.Value);
		}

		[Test]
		public void CssStrokeOpacityNumberNumberIllegal()
		{
			var snippet = "stroke-opacity: 0.5 0.5";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke-opacity", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.HasValue);
		}
		
		[Test]
		public void CssStrokeWidthLengthLegal()
		{
			var snippet = "stroke-width: 5px";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke-width", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("5px", property.Value);
		}


		[Test]
		public void CssStrokeWidthPercentLegal()
		{
			var snippet = "stroke-width: 5%";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke-width", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsTrue(property.HasValue);
			Assert.AreEqual("5%", property.Value);
		}

		[Test]
		public void CssStrokeWidthNoneIllegal()
		{
			var snippet = "stroke-width: none";
			var property = ParseDeclaration(snippet);
			Assert.AreEqual("stroke-width", property.Name);
			Assert.IsFalse(property.IsImportant);
			Assert.IsFalse(property.IsInherited);
			Assert.IsFalse(property.HasValue);
		}
	}
}
