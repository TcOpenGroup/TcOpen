using System;
using System.Collections.Generic;
using TcOpen.Inxton.Data;
using Vortex.Connector;

namespace TcoData
{
    public class ValueChangeTracker
    {
        private IVortexObject VortexObject { get; set; }
        private ICrudDataObject DataObject { get; set; }
        public ValueChangeTracker(ICrudDataObject dataObject)
        {
            VortexObject = (IVortexObject)dataObject;
            DataObject = dataObject;
        }

        public void StartObservingChanges()
        {
            Changes = new List<ValueChangeItem>();
            VortexObject.SubscribeShadowValueChange(LogShadowChanges);
        }

        public void StopObservingChanges()
        {
            VortexObject.UnSubscribeShadowValueChange();
        }

        private void LogShadowChanges(IValueTag valueTag, object original, object newValue)
        {
            var userName = "no user";

            Changes.Add(new ValueChangeItem()
            {
                ValueTag = new ValueItemDescriptor(valueTag),
                OldValue = original,
                NewValue = newValue,
                DateTime = DateTime.Now,
                UserName = userName
            });
        }

        public void SaveObservedChanges(IBrowsableDataObject plainObject)
        {
            foreach (var change in Changes)
            {
                Vortex.Framework.Abstractions.Journal.Journaling.Journal.DataValueChangeTracking(VortexObject, plainObject._EntityId, change.ValueTag.HumanReadable, change.ValueTag.Symbol, change.OldValue, change.NewValue);
            }

            if (DataObject.Changes == null)
            {
                DataObject.Changes = new List<ValueChangeItem>();
            }

            Changes.AddRange(DataObject.Changes);
            ((IPlainCrudDataObject)plainObject).Changes.AddRange(Changes);

            Changes = new List<ValueChangeItem>();
        }

        private string GetUser()
        {
            var userName = "no user";

            return userName;
        }


        public void Import(IBrowsableDataObject plainObject)
        {

            var startImportTag = new ValueChangeItem()
            {
                DateTime = DateTime.Now,
                UserName = GetUser(),
                NewValue = "-Import start-",
                OldValue = "-Import start-"
            };

            var endImportTag = new ValueChangeItem()
            {
                DateTime = DateTime.Now,
                UserName = GetUser(),
                NewValue = "-Import end-",
                OldValue = "-Import end-"
            };

            ((IPlainCrudDataObject)plainObject).Changes.Add(startImportTag);
            SaveObservedChanges(plainObject);
            ((IPlainCrudDataObject)plainObject).Changes.Add(endImportTag);

        }


        private List<ValueChangeItem> Changes = new List<ValueChangeItem>();
    }
}
