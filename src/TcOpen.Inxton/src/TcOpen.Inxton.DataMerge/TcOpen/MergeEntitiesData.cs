using System;
using System.Collections.Generic;
using System.Linq;
using TcOpen.Inxton.Data.Merge.Base;
using Vortex.Connector;

namespace TcOpen.Inxton.Data.Merge
{
    public class MergeEntitiesData<T> : MergeEntityDataBase<T>, IMergeEntitiesData<T>
        where T : IBrowsableDataObject, new()
    {
        private static IRepository<T> _sourceRepository;
        private static IRepository<T> _targetRepository;

        public MergeEntitiesData(
            IRepository<T> sourceRepository,
            IRepository<T> targetRepository,
            IEnumerable<Type> requiredTypes,
            IEnumerable<string> requiredProperties,
            Func<object, bool> exclusion,
            Func<object, bool> inclusion
        )
        {
            SourceRepository = sourceRepository;
            TargetRepository = targetRepository;
            RequiredTypes = requiredTypes;
            RequiredProperties = requiredProperties;
            Exclusion = exclusion;
            Inclusion = inclusion;
        }

        public MergeEntitiesData(IRepository<T> sourceRepository, IRepository<T> targetRepository)
        {
            SourceRepository = sourceRepository;
            TargetRepository = targetRepository;
            RequiredTypes = new List<Type>();
            RequiredProperties = new List<string>();
        }

        public T Source { get; set; }
        public T Target { get; set; }
        public IRepository<T> SourceRepository
        {
            get => _sourceRepository;
            set => _sourceRepository = value;
        }
        public IRepository<T> TargetRepository
        {
            get => _targetRepository;
            set => _targetRepository = value;
        }

        public override T GetEntityData(string id, IRepository<T> repository)
        {
            var entity = repository.Queryable.FirstOrDefault<T>(p => p._EntityId == id);
            return entity;
        }

        public IQueryable<T> GetEntities(IRepository<T> repository)
        {
            return repository.Queryable.Where(p => true);
        }

        public override void UpdateEntityData(T data, IRepository<T> repository)
        {
            repository.Update(data._EntityId, data);
        }

        /// <summary>
        /// Merge specified data from source entity to all entities in target repository
        /// </summary>
        /// <param name="sourceId"></param>
        /// <returns></returns>
        public bool Merge(string sourceId)
        {
            if (string.IsNullOrEmpty(sourceId))
            {
                return false;
            }

            Source = GetEntityData(sourceId, SourceRepository);

            foreach (var entity in GetEntities(TargetRepository))
            {
                Target = GetEntityData(entity._EntityId, TargetRepository);
                MergeEntities((IPlain)Target, (IPlain)Source);

                UpdateEntityData(Target, TargetRepository);
            }

            return true;
        }

        /// <summary>
        /// Merge specified data from source entity to target entity
        /// </summary>
        /// <param name="sourceId"></param>
        /// <param name="targetId"></param>
        /// <returns></returns>
        public bool Merge(string sourceId, string targetId)
        {
            if (string.IsNullOrEmpty(sourceId) || string.IsNullOrEmpty(targetId))
            {
                return false;
            }

            Source = GetEntityData(sourceId, SourceRepository);
            Target = GetEntityData(targetId, TargetRepository);

            MergeEntities((IPlain)Target, (IPlain)Source);

            UpdateEntityData(Target, TargetRepository);
            return true;
        }

        /// <summary>
        /// Merge specified data from source entity to all entities in target repository
        /// </summary>
        /// <param name="sourceId"></param>
        /// <param name="exclude"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public bool Merge(
            string sourceId,
            Func<object, bool> exclude = null,
            Func<object, bool> include = null,
            Func<object, IEnumerable<string>> includeProperties = null
        )
        {
            if (string.IsNullOrEmpty(sourceId))
            {
                return false;
            }

            Source = GetEntityData(sourceId, SourceRepository);

            foreach (var entity in GetEntities(TargetRepository))
            {
                Target = GetEntityData(entity._EntityId, TargetRepository);
                MergeEntities((IPlain)Target, (IPlain)Source, exclude, include, includeProperties);

                UpdateEntityData(Target, TargetRepository);
            }

            return true;
        }

        /// <summary>
        /// Merge specified data from source entity to target entity
        /// </summary>
        /// <param name="sourceId"></param>
        /// <param name="targetId"></param>
        /// <param name="exclude"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public bool Merge(
            string sourceId,
            string targetId,
            Func<object, bool> exclude = null,
            Func<object, bool> include = null,
            Func<object, IEnumerable<string>> includeProperties = null
        )
        {
            if (string.IsNullOrEmpty(sourceId) || string.IsNullOrEmpty(targetId))
            {
                return false;
            }

            Source = GetEntityData(sourceId, SourceRepository);
            Target = GetEntityData(targetId, TargetRepository);

            MergeEntities((IPlain)Target, (IPlain)Source, exclude, include, includeProperties);

            UpdateEntityData(Target, TargetRepository);
            return true;
        }

        private bool IsRequiredType(object obj)
        {
            return RequiredTypes.Any(p => p == obj.GetType());
        }

        public IEnumerable<Type> RequiredTypes { get; }

        public IEnumerable<string> RequiredProperties { get; }

        public Func<object, bool> Exclusion { get; }
        public Func<object, bool> Inclusion { get; }

        private void MergeEntities(IPlain childObjTarget, IPlain childObjSource)
        {
            //foreach (var childTarget in childObjTarget.GetType().GetProperties().Where(p => true).Select(p => p.GetValue(childObjTarget)))

            foreach (var child in childObjTarget.GetType().GetProperties().Where(p => true))
            {
                var childTarget = child.GetValue(childObjTarget);
                var childSource = childObjSource
                    .GetType()
                    .GetProperties()
                    .FirstOrDefault(p => p.Name == child.Name)
                    .GetValue(childObjSource);

                if (childTarget != null)
                {
                    if (IsRequiredType(childTarget))
                    {
                        CopyValues(childTarget, childSource, RequiredProperties, Exclusion);
                    }
                    else if (Inclusion != null)
                    {
                        if (Inclusion(childTarget))
                        {
                            CopyValues(childTarget, childSource, RequiredProperties, Exclusion);
                        }
                    }

                    if (childTarget.GetType().IsArray)
                    {
                        var arrayTarget = childTarget as Array;
                        var arraySource = childTarget as Array;

                        if (arrayTarget != null || arraySource != null)
                            for (int i = 0; i < arrayTarget.Length; i++)
                            {
                                MergeEntities(
                                    (IPlain)arrayTarget.GetValue(i),
                                    (IPlain)arraySource.GetValue(i)
                                );
                            }
                    }

                    if (childTarget is IPlain)
                    {
                        MergeEntities((IPlain)childTarget, (IPlain)childSource);
                    }
                }
            }
        }

        private void MergeEntities(
            IPlain childObjTarget,
            IPlain childObjSource,
            Func<object, bool> exclude = null,
            Func<object, bool> include = null,
            Func<object, IEnumerable<string>> properties = null
        )
        {
            foreach (var child in childObjTarget.GetType().GetProperties().Where(p => true))
            {
                var childTarget = child.GetValue(childObjTarget);
                var childSource = childObjSource
                    .GetType()
                    .GetProperties()
                    .FirstOrDefault(p => p.Name == child.Name)
                    .GetValue(childObjSource);

                if (childTarget != null)
                {
                    if (include != null)
                    {
                        if (include(childTarget) || IsRequiredType(childTarget))
                        {
                            CopyValues(childTarget, childSource, properties(childTarget), exclude);
                        }

                        if (childTarget.GetType().IsArray)
                        {
                            var arrayTarget = childTarget as Array;
                            var arraySource = childTarget as Array;

                            if (arrayTarget != null || arraySource != null)
                                for (int i = 0; i < arrayTarget.Length; i++)
                                {
                                    MergeEntities(
                                        (IPlain)arrayTarget.GetValue(i),
                                        (IPlain)arraySource.GetValue(i),
                                        exclude,
                                        include,
                                        properties
                                    );
                                }
                        }

                        if (childTarget is IPlain)
                        {
                            MergeEntities(
                                (IPlain)childTarget,
                                (IPlain)childSource,
                                exclude,
                                include,
                                properties
                            );
                        }
                    }
                }
            }
        }

        private void CopyValues(
            object target,
            object source,
            IEnumerable<string> requiredProperties,
            Func<object, bool> exclude = null
        )
        {
            Type targetType = target.GetType();
            var _properties = targetType.GetProperties();

            if (exclude != null)
            {
                if (exclude(target))
                    return;
            }

            foreach (var prop in _properties)
            {
                if (requiredProperties.Count() > 0)
                {
                    if (requiredProperties.Any(p => p == prop.Name))
                    {
                        var value = prop.GetValue(source, null);
                        prop.SetValue(target, value, null);
                    }
                }
                else
                {
                    var value = prop.GetValue(source, null);
                    prop.SetValue(target, value, null);
                }
            }
        }
    }
}
