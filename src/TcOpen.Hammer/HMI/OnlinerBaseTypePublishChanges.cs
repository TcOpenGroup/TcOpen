using System;
using System.Linq;
using System.Threading.Tasks;
using PlcHammer;
using TcOpen.Inxton.Data;
using Vortex.Connector.ValueTypes;

namespace HMI
{
    public static class OnlinerBaseTypePublishChanges
    {
        /// <summary>
        /// A function which will subscribe to an Onliner in a PLC.
        /// It will publish every change it detectes to an IRepository.
        /// A new value is passed to the Creator function, which should be used to create and object of IObservedValue<T> which will be stored in the database
        ///
        /// </summary>
        /// <code>
        ///     var repoSettings = new MongoDbRepositorySettings<enumModesObservedValue>(Constants.CONNECTION_STRING_DB, Constants.DB_NAME, "observedModes");
        ///     var observedModes = new MongoDbRepository<enumModesObservedValue>(repoSettings);
        ///
        ///     Connectors.Instance.MainPlc.TECH_MAIN._app._tech.Cu1.SL._currentState
        ///         .Uid
        ///         .PublishChanges(observedModes, (a) => new enumModesObservedValue(a));
        /// </code>
        /// <typeparam name="T"></typeparam>
        /// <param name="onliner">The variable in PLC we're intereseted in</param>
        /// <param name="repository">The destionation repository where all the changes will be published to</param>
        /// <param name="Creator">Function which creates a IObservedValue<T> and also IBrowsableDataObject for the Repository</param>
        public static OnlinerBaseType<T> PublishChanges<T>(
            this OnlinerBaseType<T> onliner,
            IRepository repository,
            Func<T, IObservedValue<T>> Creator
        )
        {
            onliner.Subscribe(
                (tag, newValue) =>
                {
                    var observedEntity = Creator((T)newValue.NewValue);
                    if (observedEntity.ValueDescription == null)
                    {
                        observedEntity.ValueDescription = onliner.Symbol;
                    }
                    if (observedEntity.Name == null)
                    {
                        observedEntity.Name = onliner.AttributeName;
                    }
                    _ = Task.Run(() =>
                    {
                        try
                        {
                            repository?.Create(observedEntity._recordId, observedEntity);
                        }
                        catch (Exception e)
                        {
                            TcOpen.Inxton.TcoAppDomain.Current.Logger.Error(
                                $"Error {e} while publishing changes for {0}",
                                onliner
                            );
                        }
                    });
                }
            );
            return onliner;
        }
    }

    public interface IObservedValue<T> : IBrowsableDataObject
    {
#pragma warning disable IDE1006 // Naming Styles
        dynamic _recordId { get; set; }
        string _EntityId { get; set; }
        DateTime Timestamp { get; set; }
        string Name { get; set; }
        T Value { get; set; }
        string ValueDescription { get; set; }
    }

    public class ObservedValue<T> : IObservedValue<T>
    {
        public dynamic _recordId { get; set; }
        public string _EntityId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Name { get; set; }
        public T Value { get; set; }
        public string ValueDescription { get; set; }

        public ObservedValue(T value)
        {
            Timestamp = DateTime.Now;
            Value = value;
        }
    }

    public class enumModesObservedValue : IObservedValue<short>
    {
        public dynamic _recordId { get; set; }
        public string _EntityId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Name { get; set; }
        public short Value { get; set; }
        public string ValueDescription { get; set; }

        public enumModesObservedValue(short value)
        {
            Timestamp = DateTime.Now;
            Value = value;
            ValueDescription = Enum.GetValues(typeof(enumModes))
                .Cast<enumModes>()
                .FirstOrDefault(x => (int)x == value)
                .ToString();
            _recordId = Guid.NewGuid().ToString();
        }
    }

#pragma warning restore IDE1006 // Naming Styles
}
