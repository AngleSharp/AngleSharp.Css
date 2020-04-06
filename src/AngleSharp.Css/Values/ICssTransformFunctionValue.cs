namespace AngleSharp.Css.Values
{
    /// <summary>
    /// Functionality for computing transformation.
    /// </summary>
    public interface ICssTransformFunctionValue : ICssFunctionValue
    {
        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        TransformMatrix ComputeMatrix(IRenderDimensions renderDimensions);
    }
}
