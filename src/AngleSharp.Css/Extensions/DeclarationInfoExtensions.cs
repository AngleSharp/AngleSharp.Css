namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    static class DeclarationInfoExtensions
    {
        public static IEnumerable<String> GetMappings(this DeclarationInfo info) =>
            info.Longhands.Length > 0 ? info.Longhands : Enumerable.Repeat(info.Name, 1);

        public static ICssValue Combine(this DeclarationInfo info, ICssValue[] longhands)
        {
            var initial = true;
            var unset = true;
            var child = true;

            foreach (var longhand in longhands)
            {
                initial = initial && longhand is CssInitialValue;
                unset = unset && longhand is CssUnsetValue;
                child = child && longhand is CssChildValue;
            }

            if (initial)
            {
                return new CssInitialValue(info.InitialValue);
            }
            else if (unset)
            {
                return new CssUnsetValue(info.InitialValue);
            }
            else if (child)
            {
                return ((CssChildValue)longhands[0]).Parent;
            }

            return info.Aggregator?.Merge(longhands);
        }

        public static ICssValue[] Seperate(this DeclarationInfo info, IDeclarationFactory factory, ICssValue value)
        {
            var longhands = info.Longhands;

            if (value is ICssRawValue || value is CssChildValue)
            {
                var child = new CssChildValue(value);
                return Enumerable.Repeat(child, longhands.Length).ToArray();
            }
            else if (value is CssInitialValue)
            {
                return longhands.Select(name => factory.Create(name)?.InitialValue).ToArray();
            }

            return info.Aggregator?.Split(value);
        }
    }
}
