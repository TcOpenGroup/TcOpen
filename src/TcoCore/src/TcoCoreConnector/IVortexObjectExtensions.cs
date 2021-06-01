using System;
using System.Collections.Generic;
using System.Linq;
using Vortex.Connector;

namespace TcoCore
{
    /// <summary>
    /// Provides extension methods for <see cref="IVortexObject"/>.
    /// </summary>
    public static class IVortexObjectExtensions
    {
        /// <summary>
        /// Searches recursively the parents of this <see cref="IVortexObject"/> until encounters object of given
        /// type. When the root object <see cref="Vortex.Connector.IConnector"/> is hit climbing up the hierarchy the method returns pre-existing parent.
        /// </summary>
        /// <remarks> 
        /// Take into consideration possible performance degradation due to use of reflections in this method.
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">Searched object</param>
        /// <param name="parent">[optional] Pre-existing parent. </param>
        /// <returns>Parent object of given type.</returns>
        public static T GetParent<T>(this IVortexObject obj, T parent = null) where T : class
        {
            if (obj is Vortex.Connector.RootVortexerObject || obj is Vortex.Connector.IConnector || obj == null)
                return parent;

            if (obj is T)
            {
                parent = (T)obj;
                return parent;
            }

            return GetParent<T>(obj.GetParent());
        }

        /// <summary>
        /// Searches recursively the children of this <see cref="IVortexObject"/>         
        /// </summary>
        /// <remarks> 
        /// Take into consideration possible performance degradation due to use of reflections in this method.
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">Searched object</param>
        /// <param name="children">[optional] Pre-existing children. </param>
        /// <returns>Children of this object.</returns>
        public static IEnumerable<T> GetDescendants<T>(this IVortexObject obj, IList<T> children = null) where T : class
        {
            children = children != null ? children : new List<T>();

            if (obj != null)
            {              
                foreach (var child in obj.GetChildren())
                {
                    var ch = child as T;
                    if (ch != null)
                    {
                        children.Add(ch);
                    }

                    GetDescendants<T>(child, children);
                }
            }

            return children;
        }


        /// <summary>
        /// Get the children of given type of this <see cref="IVortexObject"/>         
        /// </summary>
        /// <remarks> 
        /// Take into consideration possible performance degradation due to use of reflections in this method.
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">Searched object</param>       
        /// <returns>Children of this object.</returns>
        public static IEnumerable<T> GetChildren<T>(this IVortexObject obj) where T : IVortexObject
        {
            var children = obj.GetChildren().Where(p => p is T).Select(p => (T)p);
            return children;
        }

        public static IEnumerable<T> GetChildren<T>(this IVortexObject obj, IEnumerable<object> excluding) where T : IVortexObject
        {
            if (excluding == null)
                excluding = new List<Type>();

            var children = obj.GetChildren().Where(p => p is T && !excluding.Any(e => e != p)).Select(p => (T)p);
            return children;
        }
    }
}
