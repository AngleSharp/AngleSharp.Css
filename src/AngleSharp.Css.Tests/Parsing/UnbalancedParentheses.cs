namespace AngleSharp.Css.Tests.Parsing
{
    using NUnit.Framework;
    using static CssConstructionFunctions;

    [TestFixture]
    public class UnbalancedParentheses
    {
        [Test]
        public void ParsingUnbalancedParenthesesShouldNotExceedMemoryIssue_11()
        {
            var source = @"@media (-ms-high-contrast:white-on-black { } @media (-ms-high-contrast:white-on-black { }";
            var sheet = ParseStyleSheet(source);
            Assert.IsNotNull(sheet);
        }
    }
}
