#nullable disable
namespace AngleSharp.Css.Tests.Values
{
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using NUnit.Framework;
    using static CssConstructionFunctions;

    /// <summary>
    /// Regression tests for three confirmed bugs in the CSS `transform` function value pipeline
    /// (`ICssTransformFunctionValue`/`TransformMatrix`), found while integrating this library's
    /// transform support into a downstream renderer. All three currently fail (that is the point -
    /// they pin down the exact defect for a fix); see each test's own comments for the root cause
    /// traced in the corresponding source file.
    /// </summary>
    [TestFixture]
    public class TransformFunctionsTests
    {
        [Test]
        public void TranslateWithTwoArgumentsDoesNotThrowWhenComputed()
        {
            // Root cause: CssTranslateValue.Compute() (Values/Functions/CssTranslateValue.cs)
            // unconditionally calls _x.Compute(context), _y.Compute(context), _z.Compute(context)
            // with no null check on any of the three. For the 2-argument `translate(x, y)` form,
            // the constructor is only ever given a non-null z when parsing translate3d - here _z is
            // null, so _z.Compute(context) throws NullReferenceException. ComputeMatrix (a few
            // lines below Compute in the same file) does not have this bug, because it reads each
            // component through the null-tolerant AsPx(...) extension method instead of calling an
            // interface method directly on a value that may be null.
            //
            // This is not a synthetic edge case: it reproduces via the most ordinary possible
            // route - building a render tree for a document with a plain inline
            // `style="transform: translate(10px, 5px)"` declaration. RenderTreeBuilder computes an
            // element's entire style declaration eagerly while constructing the tree, so the crash
            // happens even if nothing downstream ever reads the `transform` property specifically.
            //
            // Written as "does not throw" (the correct, expected behavior) rather than "throws
            // NullReferenceException" (today's actual, buggy behavior) so this test fails now and
            // starts passing automatically once the bug is fixed, instead of needing to be flipped.
            var document = "<div style=\"transform: translate(10px, 5px)\"></div>".ToHtmlDocument(Configuration.Default.WithRenderDevice().WithCss());
            var window = document.DefaultView;

            Assert.DoesNotThrow(() => window.Render(new PlainRenderDevice()));
        }

        [Test]
        public void TranslateXWithOneArgumentDoesNotThrowWhenComputed()
        {
            // Same root cause as TranslateWithTwoArgumentsDoesNotThrowWhenComputed, via
            // translateX() instead of the 2-argument translate() - here both _y and _z are null,
            // so Compute() throws on the first of the two (_y.Compute(context)).
            var document = "<div style=\"transform: translateX(10px)\"></div>".ToHtmlDocument(Configuration.Default.WithRenderDevice().WithCss());
            var window = document.DefaultView;

            Assert.DoesNotThrow(() => window.Render(new PlainRenderDevice()));
        }

        [Test]
        public void RotateComputeMatrixReturnsNaNForThePlain2DForm()
        {
            // Root cause: CssRotateValue.ComputeMatrix() (Values/Functions/CssRotateValue.cs) has
            // two distinct bugs that compound here:
            //
            // 1. A copy-paste typo - the `y` and `z` locals are both assigned `_x.AsDouble()`
            //    instead of `_y.AsDouble()`/`_z.AsDouble()` respectively. This alone would corrupt
            //    rotate3d(x, y, z, angle) results, but does not explain this test's NaN, since for
            //    plain 2D rotate() _x/_y/_z are all null anyway.
            //
            // 2. The real cause of the NaN: for the plain 2D `rotate(angle)` form, _x/_y/_z are all
            //    null (per this class's own Name property, which treats "all three null" as the
            //    signal for plain `rotate`, distinct from rotateX/Y/Z/rotate3d). Per the CSS
            //    Transforms spec, `rotate(angle)` is shorthand for `rotate3d(0, 0, 1, angle)` - the
            //    rotation axis defaults to the Z axis, not the zero vector. ComputeMatrix does not
            //    special-case this: it reads x/y/z as 0 (via AsDouble() on a null value) and
            //    proceeds to normalize (x, y, z) as if it were a real axis vector - normalizing a
            //    zero-length vector divides by Math.Sqrt(0) = 0, producing Infinity, and
            //    0 * Infinity is NaN in IEEE 754, propagating through every matrix entry.
            var source = new StringSource("rotate(45deg)");
            var value = TransformParser.ParseTransform(source);
            Assert.IsNotNull(value);

            var matrix = value.ComputeMatrix(new PlainRenderDevice());

            Assert.IsFalse(double.IsNaN(matrix.M11), "M11 should not be NaN");
            Assert.IsFalse(double.IsNaN(matrix.M12), "M12 should not be NaN");
            Assert.IsFalse(double.IsNaN(matrix.M21), "M21 should not be NaN");
            Assert.IsFalse(double.IsNaN(matrix.M22), "M22 should not be NaN");
        }

        [Test]
        public void PlainSixValueMatrixFunctionDoesNotThrowWhenComputed()
        {
            // Original root cause (now fixed): CssMatrixValue.ComputeMatrix() used to pad the
            // ordinary 6-value 2D matrix(a, b, c, d, e, f) form up to a 4x4 matrix by appending
            // only 8 more values before constructing a TransformMatrix from the flat array - but
            // TransformMatrix's array constructor requires exactly 16 values (4x4) and 6 + 8 = 14,
            // not 16, so it threw "You need to provide 16 (4x4) values." See
            // PlainSixValueMatrixFunctionPreservesAllSixComponents below for a second, distinct bug
            // the fix for this one introduced.
            var source = new StringSource("matrix(1, 0, 0, 1, 5, 9)");
            var value = TransformParser.ParseTransform(source);
            Assert.IsNotNull(value);

            Assert.DoesNotThrow(() => value.ComputeMatrix(new PlainRenderDevice()));
        }

        [Test]
        public void PlainSixValueMatrixFunctionPreservesAllSixComponents()
        {
            // Root cause: TransformMatrix's array constructor (Values/TransformMatrix.cs) is
            // column-major - `for (i = 0..4) for (j = 0..4, k++) _matrix[j, i] = values[k];` with
            // `i` as the outer/column index and `j` as the inner/row index, so array indices
            // 0-3 fill *column* 0 (not row 0), indices 4-7 fill column 1, and so on; Tx/Ty/Tz live
            // at indices 12/13/14 (the start of column 3), not scattered through the middle of the
            // array.
            //
            // CssMatrixValue.ComputeMatrix()'s current 6-to-16 padding
            // (Values/Functions/CssMatrixValue.cs) builds
            // [a, c, 0, e,  b, d, 0, f,  0, 0, 1, 0,  0, 0, 0, 1] - grouping by CSS *row*
            // (matching how a reader would naturally transcribe matrix(a,b,c,d,e,f) into the 2D
            // homogeneous matrix [[a,c,0,e],[b,d,0,f],[0,0,1,0],[0,0,0,1]]), which would be correct
            // for a *row-major* array constructor but is wrong for this one, which is column-major.
            // Reading that array back through the constructor's actual column-major loop lands
            // `e` and `f` in column 0/1's *last* row (an unused perspective cell, values[3] and
            // values[7]) rather than in column 3 (Tx/Ty) - so the translation is silently dropped
            // to 0 - and it also transposes b/c into the wrong of M12/M21.
            //
            // Confirmed with distinguishable, non-symmetric coefficients (a=2, b=3, c=4, d=5, e=6,
            // f=7) specifically so a coincidental symmetric identity (e.g. a=d=1, b=c=0) cannot mask
            // the corruption - which is exactly what happened with the identical-looking
            // PlainSixValueMatrixFunctionDoesNotThrowWhenComputed test above: for matrix(1,0,0,1,5,9)
            // this bug happens to zero everything down to the identity matrix, silently discarding
            // the translation without a symptom that "does not throw" alone would ever catch.
            //
            // The correct column-major 16-value embedding of matrix(a, b, c, d, e, f) is
            // [a, b, 0, 0,  c, d, 0, 0,  0, 0, 1, 0,  e, f, 0, 1].
            var source = new StringSource("matrix(2, 3, 4, 5, 6, 7)");
            var value = TransformParser.ParseTransform(source);
            Assert.IsNotNull(value);

            var matrix = value.ComputeMatrix(new PlainRenderDevice());

            Assert.AreEqual(2.0, matrix.M11, "M11 should be a (2)");
            Assert.AreEqual(4.0, matrix.M12, "M12 should be c (4)");
            Assert.AreEqual(3.0, matrix.M21, "M21 should be b (3)");
            Assert.AreEqual(5.0, matrix.M22, "M22 should be d (5)");
            Assert.AreEqual(6.0, matrix.Tx, "Tx should be e (6)");
            Assert.AreEqual(7.0, matrix.Ty, "Ty should be f (7)");
        }
    }
}
