using System;
using System.Collections.Generic;
using NUnit.Framework;
using TcOpen.Inxton.Data;
using Vortex.Connector;

namespace TcoDataUnitTests
{
    public class DataTestObject : IBrowsableDataObject
    {
        public DataTestObject()
        {
            UlongMax = ulong.MaxValue;
        }

        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        public DateTime _Created { get; set; }

        public string _EntityId { get; set; }

        public DateTime _Modified { get; set; }

        public Guid Uid { get; set; }
        public object _recordId { get; set; }

        public ulong UlongMax { get; set; }

        AllTypes allTypes = new AllTypes();
        public AllTypes AllTypes
        {
            get { return allTypes; }
            set { allTypes = value; }
        }

        private List<string> _changes = new List<string>();
        public List<string> Changes
        {
            get { return this._changes; }
            set { this._changes = value; }
        }
    }

    public class AllTypes : IPlain
    {
        public AllTypes()
        {
            BoolMin = false;
            BoolMax = true;

            ByteMin = byte.MinValue;
            ByteMax = byte.MaxValue;

            SByteMin = sbyte.MinValue;
            SByteMax = sbyte.MaxValue;

            ShortMin = short.MinValue;
            ShortMax = short.MaxValue;

            IntMin = int.MinValue;
            IntMax = int.MaxValue;

            UintMin = uint.MinValue;
            UintMax = uint.MaxValue;

            LongMin = long.MinValue;
            LongMax = long.MaxValue;

            ULongMin = ulong.MinValue;
            ULongMax = ulong.MaxValue;

            CharMin = char.MinValue;
            CharMax = char.MaxValue;

            SingleMin = float.MinValue;
            SingleMax = float.MaxValue;

            DateTimeMin = DateTime.MinValue;
            DateTimeMax = DateTime.MaxValue;

            TimeSpanMin = TimeSpan.MinValue;
            TimeSpanMax = TimeSpan.MaxValue;

            DoubleMin = double.MinValue;
            DoubleMax = double.MaxValue;
        }

        public bool BoolMin { get; set; }
        public bool BoolMax { get; set; }

        public byte ByteMax { get; set; }
        public byte ByteMin { get; set; }

        public sbyte SByteMax { get; set; }
        public sbyte SByteMin { get; set; }

        public short ShortMax { get; set; }
        public short ShortMin { get; set; }

        public ushort UShortMax { get; set; }
        public ushort UShortMin { get; set; }

        public int IntMax { get; set; }
        public int IntMin { get; set; }

        public uint UintMax { get; set; }
        public uint UintMin { get; set; }

        public long LongMax { get; set; }
        public long LongMin { get; set; }

        public ulong ULongMax { get; set; }
        public ulong ULongMin { get; set; }

        public char CharMin { get; set; }

        public char CharMax { get; set; }

        public float SingleMax { get; set; }
        public float SingleMin { get; set; }

        public DateTime DateTimeMin { get; set; }
        public DateTime DateTimeMax { get; set; }

        public TimeSpan TimeSpanMin { get; set; }
        public TimeSpan TimeSpanMax { get; set; }

        public double DoubleMax { get; set; }
        public double DoubleMin { get; set; }

        public void AssertEquality(AllTypes c)
        {
            foreach (var property in this.GetType().GetProperties())
            {
                Assert.AreEqual(property.GetValue(this), property.GetValue(c));
            }
        }
    }

    public class DataTestObjectAlteredStructure : IBrowsableDataObject
    {
        public DataTestObjectAlteredStructure()
        {
            UlongMax = ulong.MaxValue;
        }

        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        public DateTime _Created { get; set; }

        public string _EntityId { get; set; }

        public DateTime _Modified { get; set; }

        public Guid Uid { get; set; }
        public object _recordId { get; set; }

        public ulong UlongMax { get; set; }

        public ulong ExtraElement { get; set; }

        AllTypesAlteredStructure allTypes = new AllTypesAlteredStructure();
        public AllTypesAlteredStructure AllTypes
        {
            get { return allTypes; }
            set { allTypes = value; }
        }

        private List<string> _changes = new List<string>();
        public List<string> Changes
        {
            get { return this._changes; }
            set { this._changes = value; }
        }
    }

    public class AllTypesAlteredStructure : IPlain
    {
        public AllTypesAlteredStructure()
        {
            BoolMin = false;
            BoolMax = true;

            ByteMin = byte.MinValue;
            ByteMax = byte.MaxValue;

            SByteMin = sbyte.MinValue;
            SByteMax = sbyte.MaxValue;

            ShortMin = short.MinValue;
            ShortMax = short.MaxValue;

            IntMin = int.MinValue;
            IntMax = int.MaxValue;

            UintMin = uint.MinValue;
            UintMax = uint.MaxValue;

            LongMin = long.MinValue;
            LongMax = long.MaxValue;

            ULongMin = ulong.MinValue;
            ULongMax = ulong.MaxValue;

            CharMin = char.MinValue;
            CharMax = char.MaxValue;

            SingleMin = float.MinValue;
            SingleMax = float.MaxValue;

            DateTimeMin = DateTime.MinValue;
            DateTimeMax = DateTime.MaxValue;

            TimeSpanMin = TimeSpan.MinValue;
            TimeSpanMax = TimeSpan.MaxValue;

            DoubleMin = double.MinValue;
            DoubleMax = double.MaxValue;
        }

        AllTypes extraElement1 = new AllTypes();
        public AllTypes extraElement
        {
            get { return extraElement1; }
            set { extraElement1 = value; }
        }

        public bool BoolMin { get; set; }
        public bool BoolMax { get; set; }

        public byte ByteMax { get; set; }
        public byte ByteMin { get; set; }

        public sbyte SByteMax { get; set; }
        public sbyte SByteMin { get; set; }

        public short ShortMax { get; set; }
        public short ShortMin { get; set; }

        public ushort UShortMax { get; set; }
        public ushort UShortMin { get; set; }

        public int IntMax { get; set; }
        public int IntMin { get; set; }

        public uint UintMax { get; set; }
        public uint UintMin { get; set; }

        public long LongMax { get; set; }
        public long LongMin { get; set; }

        public ulong ULongMax { get; set; }
        public ulong ULongMin { get; set; }

        public char CharMin { get; set; }

        public char CharMax { get; set; }

        public float SingleMax { get; set; }
        public float SingleMin { get; set; }

        public DateTime DateTimeMin { get; set; }
        public DateTime DateTimeMax { get; set; }

        public TimeSpan TimeSpanMin { get; set; }
        public TimeSpan TimeSpanMax { get; set; }

        public double DoubleMax { get; set; }
        public double DoubleMin { get; set; }
    }
}
