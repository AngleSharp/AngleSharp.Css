namespace AngleSharp.Css
{
    static class Factory
    {
        public static DefaultFeatureValidatorFactory FeatureValidator = new DefaultFeatureValidatorFactory();

        public static DefaultPseudoElementFactory PseudoElement = new DefaultPseudoElementFactory();

        public static DefaultDocumentFunctionFactory DocumentFunction = new DefaultDocumentFunctionFactory();

        public static DefaultConverterFactory Converter = new DefaultConverterFactory();

        public static StyleAttributeObserver Observer = new StyleAttributeObserver();
    }
}
