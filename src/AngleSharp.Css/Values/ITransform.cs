namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;

    /// <summary>
    /// Functionality for computing transformation.
    /// </summary>
    interface ITransform : ICssValue
    {
        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        TransformMatrix ComputeMatrix();
    }
}
