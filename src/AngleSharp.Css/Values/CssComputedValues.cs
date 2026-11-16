#nullable enable
namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Computes the items of multiple values, see ICssComputedValue.
    /// </summary>
    static class CssComputedValues
    {
        /// <summary>
        /// Gets if computing the value gives the value itself.
        /// </summary>
        public static Boolean IsComputed(ICssValue? value) => value is ICssComputedValue { IsComputed: true };

        /// <summary>
        /// Gets if all items (null ones are left out) are computed already.
        /// </summary>
        public static Boolean AreComputed<T>(T[] items)
            where T : ICssValue
        {
            for (var i = 0; i < items.Length; i++)
            {
                var item = items[i];

                if (item is not null && !IsComputed(item))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Computes the items (null ones stay null), or gives null if they are all computed
        /// already, i.e., the value is its own computed value.
        /// </summary>
        public static T?[]? Compute<T>(T?[] items, ICssComputeContext context)
            where T : ICssValue
        {
            var computed = default(T?[]);

            for (var i = 0; i < items.Length; i++)
            {
                var item = items[i];

                if (item is null || IsComputed(item))
                {
                    if (computed is not null)
                    {
                        computed[i] = item;
                    }

                    continue;
                }

                if (computed is null)
                {
                    computed = new T?[items.Length];
                    Array.Copy(items, computed, i);
                }

                computed[i] = (T?)item.Compute(context);
            }

            return computed;
        }
    }
}
