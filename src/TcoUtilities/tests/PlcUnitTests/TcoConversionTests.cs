using NUnit.Framework;
using NUnit.Framework.Constraints;
using TcoUtilitiesTests;
using System;
using TcoUtilities;

namespace TcoConversion
{
    public class Units
    {
        private const float RealMaxValue = 3.402823e+38f;
        private const float RealMinValue = -3.402823e+38f;
        TcoUtilitiesTests.TcoConversionTetsContext sut;

        [OneTimeSetUp]
        public void Setup()
        {
            TcoUtilitiesTests.Entry.TcoUtilitiesTests.Connector.BuildAndStart().ReadWriteCycleDelay = 100;
            sut = TcoUtilitiesTests.Entry.TcoUtilitiesTests.MAIN._testConversionContext;
        }

        [Test]
        [TestCase(0, 0, 0, 0, 0, (int)eTcoDataFormat.LittleEndian)]
        [TestCase(0, 0, 0, 0, 0, (int)eTcoDataFormat.BigEndian)]
        [TestCase(1, 63, 128, 0, 0, (int)eTcoDataFormat.BigEndian)]
        [TestCase(1, 0, 0, 128, 63, (int)eTcoDataFormat.LittleEndian)]
        public void ConvertBytesToReal(float reqResult, byte byte0, byte byte1, byte byte2, byte byte3, short format)
        {

            //-- Arrange  

            sut.ExecuteProbeRun(1, (int)eTcoConversionTests.Init);
            sut._Byte0.Synchron = byte0;
            sut._Byte1.Synchron = byte1;
            sut._Byte2.Synchron = byte2;
            sut._Byte3.Synchron = byte3;
            sut._format.Synchron = format;

            //-- Act
            sut.ExecuteProbeRun(1, (int)eTcoConversionTests.TcoBytesToReal);

            //-- Assert
            Assert.AreEqual(reqResult, sut._resultRealValue.Synchron);

        }
        [Test]
        [TestCase(0, 0, 0, 0, 0, (int)eTcoDataFormat.LittleEndian)]
        [TestCase(0, 0, 0, 0, 0, (int)eTcoDataFormat.BigEndian)]
        [TestCase(1, 0, 0, 128, 63, (int)eTcoDataFormat.LittleEndian)]
        [TestCase(1, 63, 128, 0, 0, (int)eTcoDataFormat.BigEndian)]

        public void ConvertRealToBytes(float realValue, byte byte0, byte byte1, byte byte2, byte byte3, short format)
        {

            //-- Arrange  
            sut.ExecuteProbeRun(1, (int)eTcoConversionTests.Init);
            sut._realValue.Synchron = realValue;
            sut._format.Synchron = format;





            //-- Act
            sut.ExecuteProbeRun(1, (int)eTcoConversionTests.TcoRealToBytes);

            //-- Assert
            Assert.AreEqual(byte0, sut._resultByte0.Synchron);
            Assert.AreEqual(byte1, sut._resultByte1.Synchron);
            Assert.AreEqual(byte2, sut._resultByte2.Synchron);
            Assert.AreEqual(byte3, sut._resultByte3.Synchron);


        }



        [TestCase(0, (int)eTcoDataFormat.LittleEndian)]
        [TestCase(0, (int)eTcoDataFormat.BigEndian)]
        [TestCase(1, (int)eTcoDataFormat.BigEndian)]
        [TestCase(1, (int)eTcoDataFormat.LittleEndian)]
        [TestCase(192.75f, (int)eTcoDataFormat.BigEndian)]
        [TestCase(192.78f, (int)eTcoDataFormat.LittleEndian)]
        [TestCase(1.7014118346046923173168730371588e+37f, (int)eTcoDataFormat.BigEndian)]
        [TestCase(-1.7014118346046923173168730371588e+37f, (int)eTcoDataFormat.LittleEndian)]
        [TestCase(RealMaxValue, (int)eTcoDataFormat.BigEndian)]
        [TestCase(RealMinValue, (int)eTcoDataFormat.LittleEndian)]
        public void ConvertBytesToRealPlcArrange(float reqResult, short format)
        {

            //-- Arrange  
            sut.ExecuteProbeRun(1, (int)eTcoConversionTests.Init);

            sut._realValue.Synchron = reqResult;
            sut._format.Synchron = format;

            //-- Act
            sut.ExecuteProbeRun(1, (int)eTcoConversionTests.TcoBytesToRealPlcArrange);

            //-- Assert
            Assert.AreEqual(reqResult, sut._resultRealValue.Synchron);

        }
     


        [TestCase(0, (int)eTcoDataFormat.LittleEndian)]
        [TestCase(0, (int)eTcoDataFormat.BigEndian)]
        [TestCase(1, (int)eTcoDataFormat.BigEndian)]
        [TestCase(1, (int)eTcoDataFormat.LittleEndian)]
        [TestCase(192.75f, (int)eTcoDataFormat.BigEndian)]
        [TestCase(192.78f, (int)eTcoDataFormat.LittleEndian)]
        [TestCase(1.7014118346046923173168730371588e+37f, (int)eTcoDataFormat.BigEndian)]
        [TestCase(-1.7014118346046923173168730371588e+37f, (int)eTcoDataFormat.LittleEndian)]
        [TestCase(RealMaxValue, (int)eTcoDataFormat.BigEndian)]
        [TestCase(-RealMinValue, (int)eTcoDataFormat.LittleEndian)]
        public void ConvertRealToBytesPlcArrange(float reqResult, short format)
        {

            //-- Arrange  
            sut.ExecuteProbeRun(1, (int)eTcoConversionTests.Init);

            sut._realValue.Synchron = reqResult;
            sut._format.Synchron = format;

            //-- Act
            sut.ExecuteProbeRun(1, (int)eTcoConversionTests.TcoRealToBytesPlcArrange);

            //-- Assert
            Assert.AreEqual( sut._union.bytes[0].Synchron,sut._resultByte0.Synchron);
            Assert.AreEqual( sut._union.bytes[1].Synchron,sut._resultByte1.Synchron);
            Assert.AreEqual( sut._union.bytes[2].Synchron,sut._resultByte2.Synchron);
            Assert.AreEqual( sut._union.bytes[3].Synchron, sut._resultByte3.Synchron);

        }
    }
}
