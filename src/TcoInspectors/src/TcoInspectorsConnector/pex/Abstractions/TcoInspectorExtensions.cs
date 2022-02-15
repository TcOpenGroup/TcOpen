using System;
using System.Collections.Generic;
using TcoCore;

namespace TcoInspectors
{
    public static class TcoInspectorExtensions
    {
        public static IEnumerable<TcoInspector> ServeInspectors(this TcoObject obj, Action action)
        {
            var instpectors = obj.GetDescendants<TcoInspector>();

            foreach (var inspector in instpectors)
            {
                inspector._dialogueTask.Initialize(action);
            }

            return instpectors;
        }
    }
}
