namespace AngleSharp.Css.Tests.Values
{
    using AngleSharp.Css.Dom;
    using NUnit.Framework;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using static CssConstructionFunctions;

    [TestFixture]
    public class ConverterIntegrityTests
    {
        [Test]
        public void AllPublicValueConvertersAreNonNullAndContainNoNullChildren()
        {
            var type = typeof(ValueConverters);
            var failures = new List<String>();

            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                if (field.FieldType != typeof(IValueConverter))
                {
                    continue;
                }

                var converter = field.GetValue(null) as IValueConverter;
                
                if (converter is null)
                {
                    failures.Add($"Field {field.Name} is null");
                    continue;
                }

                failures.AddRange(FindNullReferences(field.Name, converter));
            }

            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Static))
            {
                if (property.PropertyType != typeof(IValueConverter) || property.GetIndexParameters().Length > 0)
                {
                    continue;
                }

                var converter = property.GetValue(null) as IValueConverter;
                
                if (converter is null)
                {
                    failures.Add($"Property {property.Name} is null");
                    continue;
                }

                failures.AddRange(FindNullReferences(property.Name, converter));
            }

            Assert.That(failures, Is.Empty, String.Join(Environment.NewLine, failures));
        }

        [Test]
        [TestCase("mask-image: linear-gradient(red, blue)")]
        [TestCase("mask-repeat: no-repeat")]
        [TestCase("mask-position: 10px 20px")]
        [TestCase("mask-size: 10px 20px")]
        [TestCase("mask-border-repeat: repeat")]
        public void MaskRelatedDeclarationsDoNotThrow(String declaration)
        {
            Assert.DoesNotThrow(() => ParseDeclaration(declaration));
            var property = ParseDeclaration(declaration);
            Assert.IsNotNull(property);
            Assert.IsTrue(property.HasValue);
        }

        private static IEnumerable<String> FindNullReferences(String rootName, IValueConverter root)
        {
            var visited = new HashSet<Object>(ReferenceEqualityComparer.Instance);
            var failures = new List<String>();
            Scan(root, rootName, visited, failures);
            return failures;
        }

        private static void Scan(Object instance, String path, HashSet<Object> visited, List<String> failures)
        {
            if (instance is null || !visited.Add(instance))
            {
                return;
            }

            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            foreach (var field in instance.GetType().GetFields(flags))
            {
                if (typeof(IValueConverter).IsAssignableFrom(field.FieldType))
                {
                    var child = field.GetValue(instance) as IValueConverter;

                    if (child is null)
                    {
                        failures.Add($"{path}.{field.Name} is null ({instance.GetType().Name})");
                    }
                    else
                    {
                        Scan(child, $"{path}.{field.Name}", visited, failures);
                    }
                }
                else if (field.FieldType.IsArray && typeof(IValueConverter).IsAssignableFrom(field.FieldType.GetElementType()))
                {
                    var array = field.GetValue(instance) as Array;

                    if (array is null)
                    {
                        failures.Add($"{path}.{field.Name} array is null ({instance.GetType().Name})");
                        continue;
                    }

                    for (var i = 0; i < array.Length; i++)
                    {
                        var item = array.GetValue(i) as IValueConverter;

                        if (item is null)
                        {
                            failures.Add($"{path}.{field.Name}[{i}] is null ({instance.GetType().Name})");
                        }
                        else
                        {
                            Scan(item, $"{path}.{field.Name}[{i}]", visited, failures);
                        }
                    }
                }
                else if (typeof(IEnumerable).IsAssignableFrom(field.FieldType) && field.FieldType != typeof(String))
                {
                    var value = field.GetValue(instance) as IEnumerable;

                    if (value is null)
                    {
                        continue;
                    }

                    var index = 0;

                    foreach (var item in value)
                    {
                        if (item is IValueConverter converter)
                        {
                            Scan(converter, $"{path}.{field.Name}[{index}]", visited, failures);
                        }

                        index++;
                    }
                }
            }
        }

        private sealed class ReferenceEqualityComparer : IEqualityComparer<Object>
        {
            public static readonly ReferenceEqualityComparer Instance = new ReferenceEqualityComparer();

            public new Boolean Equals(Object? x, Object? y) => ReferenceEquals(x, y);

            public Int32 GetHashCode(Object obj) => System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj);
        }
    }
}
