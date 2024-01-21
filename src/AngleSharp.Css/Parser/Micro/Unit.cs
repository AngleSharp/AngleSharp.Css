namespace AngleSharp.Css.Parser
{
    using System;

    sealed class Unit(String value, String dimension)
    {
        public String Value
        {
            get;
        } = value;

        public String Dimension
        {
            get;
        } = dimension;
    }
}
