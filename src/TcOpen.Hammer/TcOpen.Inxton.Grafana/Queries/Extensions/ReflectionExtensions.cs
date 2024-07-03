using System;
using System.Collections.Generic;
using System.Linq;

namespace Grafana.Backend.Queries.Extensions
{
    internal static class ReflectionExtensions
    {
        private static IEnumerable<(string Path, T Object)> PlainChildrenOf<T>(
            object root,
            string parentName,
            List<T> acc
        )
        {
            if (!root.GetType().Name.Contains("Plain"))
                throw new ArgumentException("Supports only Inxton Plain objects");

            var props = root.GetType().GetProperties();

            var separator = string.IsNullOrEmpty(parentName) ? "" : ".";
            var name = parentName + separator;

            var currentLevel = props
                .Where(x => typeof(T).IsAssignableFrom(x.PropertyType))
                .Select(x => x.GetValue(root))
                .Cast<T>()
                .Select(x => (name + NameOfObject(root, x), x));

            var oneLevelDeeper = props
                .Where(x => x.PropertyType.Name.StartsWith("Plain"))
                .Select(x => x.GetValue(root))
                .SelectMany(x => PlainChildrenOf(x, name + NameOfObject(root, x), acc));

            return currentLevel.Concat(oneLevelDeeper);
        }

        // Based on the value of the @object it will check for a name of the property in a parent.
        private static string NameOfObject(object parent, object @object) =>
            parent.GetType().GetProperties().First(x => x.GetValue(parent) == @object).Name;
    }
}
