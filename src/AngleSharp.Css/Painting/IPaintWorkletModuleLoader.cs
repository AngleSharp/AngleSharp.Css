namespace AngleSharp.Css.Painting
{
    using AngleSharp.Dom;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Loads the modules of paint worklets, i.e., runs a script in the global scope of a
    /// paint worklet, which registers its paint definitions with registerPaint(). This is
    /// the extension point for scripting engines (e.g., AngleSharp.Js): register an
    /// implementation as a service of the configuration. Without one, addModule() fails
    /// with a NotSupportedError, while paint definitions can still be registered directly.
    /// https://www.w3.org/TR/css-paint-api-1/#paint-worklet
    /// </summary>
    public interface IPaintWorkletModuleLoader
    {
        /// <summary>
        /// Fetches and runs the module in the given global scope.
        /// </summary>
        /// <param name="scope">
        /// The global scope to run the module in, which gives registerPaint(), the
        /// devicePixelRatio, and the document the worklet belongs to.
        /// </param>
        /// <param name="moduleUrl">The (absolute) URL of the module.</param>
        /// <param name="cancellationToken">The token to cancel loading.</param>
        /// <returns>The task completing once the module has been run.</returns>
        Task LoadAsync(PaintWorkletGlobalScope scope, Url moduleUrl, CancellationToken cancellationToken);
    }
}
