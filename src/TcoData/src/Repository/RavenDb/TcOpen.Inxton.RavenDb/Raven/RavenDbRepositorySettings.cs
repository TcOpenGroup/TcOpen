using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using TcOpen.Inxton.Data;

namespace TcOpen.Inxton.RavenDb
{
    public class RavenDbRepositorySettings<T> : RavenDbRepositorySettingsBase<T>
        where T : IBrowsableDataObject
    {
        public RavenDbRepositorySettings(
            string[] urls,
            string databaseName,
            string certPath,
            string certPass
        )
        {
            var store = new DocumentStore { Urls = urls, Database = databaseName };

            if (!string.IsNullOrWhiteSpace(certPath))
                store.Certificate = new X509Certificate2(certPath, certPass);

            store.Initialize();

            IndexCreation.CreateIndexes(typeof(RavenDbRepositorySettings<>).Assembly, store);

            Store = store;
        }
    }
}
