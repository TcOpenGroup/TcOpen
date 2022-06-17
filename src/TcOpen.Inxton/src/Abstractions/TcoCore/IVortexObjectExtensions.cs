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
#pragma warning disable CS0618 // Type or member is obsolete
            if (obj is Vortex.Connector.RootVortexerObject || obj is Vortex.Connector.IConnector || obj == null)
#pragma warning restore CS0618 // Type or member is obsolete
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
        /// Gets descendant objects of given type up to given tree depth.
        /// </summary>
        /// <remarks> 
        /// Take into consideration possible performance degradation due to use of reflections in this method.
        /// </remarks>
        /// <typeparam name="T">Descendant type</typeparam>
        /// <param name="obj">Root object</param>
        /// <param name="depth">Depth to search for descendant objects</param>
        /// <param name="children">[optional] Pre-existing descendants.</param>
        /// <param name="currentDepth">[optional] Current depth</param>
        /// <returns>Descendant of given type up to given depth.</returns>
        public static IEnumerable<T> GetDescendants<T>(this IVortexObject obj, int depth, IList<T> children = null, int currentDepth = 0) where T : class
        {
            children = children != null ? children : new List<T>();
           
            currentDepth++;
            
            if (obj != null && currentDepth < depth)
            {
                foreach (var child in obj.GetChildren())
                {
                    var ch = child as T;
                    if (ch != null)
                    {
                        children.Add(ch);
                    }

                    GetDescendants<T>(child, depth, children, currentDepth);
                }
            }

            currentDepth--;

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
        public static IEnumerable<T> GetChildren<T>(this IVortexObject obj) 
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

        /// <summary>
        /// Get the Plain (POCO) object populated with current online data.
        /// </summary>
        /// <remarks> 
        /// This method uses dynamic casting, which may impact the performance of the data exchange.
        /// </remarks>
        /// <param name="obj">Onliner from which the plain is created.</param>        
        /// <returns>Plain (POCO) object populated with current online data.</returns>
        public static T GetPlainFromOnline<T>(this IVortexObject obj)
        {
            dynamic o = obj;
            var plain = o.CreatePlainerType();
            o.FlushOnlineToPlain(plain);
            return plain;
        }


        /// <summary>
        /// Get the Plain (POCO) object populated with current online data.
        /// </summary>
        /// <remarks> 
        /// This method uses dynamic casting, which may impact the performance of the data exchange.
        /// </remarks>
        /// <param name="obj">Onliner from which the plain is created.</param>        
        /// <returns>Plain (POCO) object populated with current online data.</returns>
        public static object GetPlainFromOnline(this IVortexObject obj)
        {
            dynamic o = obj;
            var plain = o.CreatePlainerType();
            o.FlushOnlineToPlain(plain);
            return plain;
        }
    }   
}
