#nullable disable
namespace AngleSharp.Css.Tests.TypedOm
{
    using AngleSharp.Css.TypedOm;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class UnparsedValueTests
    {
        [Test]
        public void SplitsTextAndReferences()
        {
            var value = (CssUnparsedValue)CssStyleValue.Parse("--foo", "1px var(--a) solid");
            Assert.AreEqual(3, value.Length);
            Assert.AreEqual("1px ", value[0]);
            Assert.AreEqual("--a", ((CssVariableReferenceValue)value[1]).Variable);
            Assert.IsNull(((CssVariableReferenceValue)value[1]).Fallback);
            Assert.AreEqual(" solid", value[2]);
            Assert.AreEqual("1px var(--a) solid", value.ToString());
        }

        [Test]
        public void ParsesNestedFallbacks()
        {
            var value = (CssUnparsedValue)CssStyleValue.Parse("--foo", "var(--a, var(--b, 1px))");
            var outer = (CssVariableReferenceValue)value[0];
            Assert.AreEqual("--a", outer.Variable);
            var inner = (CssVariableReferenceValue)outer.Fallback[1];
            Assert.AreEqual("--b", inner.Variable);
            Assert.AreEqual(" 1px", inner.Fallback.ToString());
            Assert.AreEqual("var(--a, var(--b, 1px))", value.ToString());
        }

        [Test]
        public void IgnoresReferencesInStrings()
        {
            var value = (CssUnparsedValue)CssStyleValue.Parse("--foo", "'var(--x)' var(--y)");
            Assert.AreEqual(2, value.Length);
            Assert.AreEqual("'var(--x)' ", value[0]);
            Assert.AreEqual("--y", ((CssVariableReferenceValue)value[1]).Variable);
        }

        [Test]
        public void IgnoresOtherFunctionsEndingInVar()
        {
            var value = (CssUnparsedValue)CssStyleValue.Parse("--foo", "myvar(--x)");
            Assert.AreEqual(1, value.Length);
            Assert.AreEqual("myvar(--x)", value[0]);
        }

        [Test]
        public void CanBeConstructedFromSegments()
        {
            var value = new CssUnparsedValue(new Object[] { "1px ", new CssVariableReferenceValue("--a", new CssUnparsedValue(new Object[] { "red" })) });
            Assert.AreEqual("1px var(--a,red)", value.ToString());
        }

        [Test]
        public void SegmentsCanBeReplacedAndAppended()
        {
            var value = new CssUnparsedValue(new Object[] { "a" });
            value[0] = "b";
            value[1] = new CssVariableReferenceValue("--c");
            Assert.AreEqual("bvar(--c)", value.ToString());
        }

        [Test]
        public void RejectsInvalidSegments()
        {
            Assert.Throws<ArgumentException>(() => new CssUnparsedValue(new Object[] { 5 }));
            Assert.Throws<ArgumentException>(() => new CssVariableReferenceValue("foo"));
        }
    }
}
