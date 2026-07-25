namespace AngleSharp.Css
{
    static class Factory
    {
        public static DefaultFeatureValidatorFactory FeatureValidator = new();

        public static DefaultPseudoElementFactory PseudoElement = new();

        public static DefaultDocumentFunctionFactory DocumentFunction = new();

        public static DefaultDeclarationFactory Declaration = new();

        public static StyleAttributeObserver Observer = new();
    }
}
