#nullable disable
namespace AngleSharp.Css.Tests.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using NUnit.Framework;
    using System;
    using static ValueConverters;

    [TestFixture]
    public class AnyValueComputationTests
    {
        [TestCase(false)]
        [TestCase(true)]
        public void AnyResultIsNotRecomputedThroughNestedConverters(Boolean isResolved)
        {
            var converter = new SingleUseConverter(Or(None, Or(Auto, Any)));
            var context = new TestComputeContext { Converter = converter };
            ICssValue value = new CssAnyValue("opaque tokens", isResolved);

            Assert.AreEqual("opaque tokens", value.Compute(context).CssText);
            Assert.AreEqual(1, converter.Calls);
        }

        [TestCase("none", "none")]
        [TestCase("2em", "32px")]
        [TestCase("calc(1px + 2px)", "3px")]
        [TestCase(" /*before*/ 2em /*after*/ ", "32px")]
        [TestCase("invalid", null)]
        [TestCase("2px trailing", null)]
        [TestCase("none trailing", null)]
        public void ConcreteCompositeResultsStillComputeAndValidate(String text, String expected)
        {
            var context = new TestComputeContext { Converter = Or(None, LengthConverter) };

            foreach (var isResolved in new[] { false, true })
            {
                ICssValue value = new CssAnyValue(text, isResolved);
                Assert.AreEqual(expected, value.Compute(context)?.CssText);
            }
        }

        [TestCase("none", "none")]
        [TestCase("2em", "32px")]
        [TestCase("calc(1px + 2px)", "3px")]
        public void ConcreteBranchesBeforeAnyStillCompute(String text, String expected)
        {
            var context = new TestComputeContext { Converter = Or(None, LengthConverter, Any) };
            ICssValue value = new CssAnyValue(text);

            Assert.AreEqual(expected, value.Compute(context).CssText);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void AnyResultStillRequiresCompleteInputConsumption(Boolean isResolved)
        {
            var context = new TestComputeContext
            {
                Converter = new ClassValueConverter<ICssValue>(source =>
                {
                    source.Next();
                    return new CssAnyValue("accepted", isResolved);
                }),
            };
            ICssValue value = new CssAnyValue("xy", isResolved);

            Assert.IsNull(value.Compute(context));
        }

        [TestCase(false, false)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(true, true)]
        public void DirectAnyAndMissingConvertersRetainResolutionSemantics(Boolean isResolved, Boolean hasConverter)
        {
            var context = new TestComputeContext { Converter = hasConverter ? Any : null };
            ICssValue value = new CssAnyValue("opaque tokens", isResolved);

            Assert.AreSame(isResolved ? value : null, value.Compute(context));
        }

        [Test]
        public void ConverterExceptionsAreNotSuppressed()
        {
            var context = new TestComputeContext
            {
                Converter = new ClassValueConverter<ICssValue>(_ => throw new InvalidOperationException("Test exception")),
            };
            ICssValue value = new CssAnyValue("opaque tokens");

            Assert.Throws<InvalidOperationException>(() => value.Compute(context));
        }

        private sealed class SingleUseConverter : IValueConverter
        {
            private readonly IValueConverter _converter;

            public SingleUseConverter(IValueConverter converter)
            {
                _converter = converter;
            }

            public Int32 Calls { get; private set; }

            public ICssValue Convert(StringSource source)
            {
                if (++Calls > 1)
                {
                    throw new InvalidOperationException("The converter was re-entered for its own any result.");
                }

                return _converter.Convert(source);
            }
        }

        private sealed class TestComputeContext : ICssComputeContext
        {
            public IRenderDevice Device { get; } = new DefaultRenderDevice { FontSize = 16 };
            public IBrowsingContext Context => null;
            public IValueConverter Converter { get; set; }
            public ICssValue Resolve(String name) => null;
        }
    }
}
