﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TcoDataTests;
using TcoDataUnitTests;
using TcOpen.Inxton.Data.MongoDb;
using TcOpen.Inxton.RavenDb;

namespace TcoDataUnitTests
{
    [TestFixture, Timeout(5000)]
    public class TcoDataExchangeRavenDbTest : TcoDataExchangeBaseTest
    {
        public override void Init()
        {
            var parameters = new RavenDbRepositorySettings<PlainstProcessData>(new string[]{"http://127.0.0.1:8080"}, "TestDataBase", "", "");
            Repository = new RavenDbRepository<PlainstProcessData>(parameters);
            foreach (var record in Repository.GetRecords())
            {
                Repository.Delete(record._EntityId);
            }
        }
    }
}
