using NUnit.Framework;
using System.Text;

namespace TcoCoreUnitTests
{
    public class StringBuilderTests
    {

        TcoCoreTests.StringBuilderTests stringBuilderTests => ConnectorFixture.Connector.MAIN._stringBuilderTest;

        [Test, Order(000)]
        public void StringBuilderWillAppendText()
        {
            //arrange
            stringBuilderTests.Clear();
            var wordsToAppend = "Hello,how,are,you,doing,?,Fine,Thank,You";
            var listOfWordsToAppend = wordsToAppend.Split(',');
            var csharpSb = new StringBuilder();
            //act
            foreach (var word in listOfWordsToAppend)
            {
                stringBuilderTests.Append(word+",");
                csharpSb.Append(word + ",");
            }

            //assert
            var expected = csharpSb.ToString();
            var plcResult = stringBuilderTests.StringBuilderResult.Synchron;
            Assert.IsNotEmpty(plcResult);
            Assert.IsNotEmpty(expected);
            Assert.AreEqual(expected, plcResult);
        }

        [Test, Order(100)]
        public void FluentApiWorks()
        {
            //arrange
            stringBuilderTests.Clear();

            var csharpSb = new StringBuilder();

            //act
            var plcResult = stringBuilderTests.FluentApi();
            csharpSb.
                Clear()
                .Append("1")
                .Append("2")
                .Append("3")
                .Append("4")
                .Clear()
                .Append("1")
                .Append("2")
                .Append("3")
                .Append("4");
            //assert

            var expected = csharpSb.ToString();
            Assert.IsNotEmpty(plcResult);
            Assert.IsNotEmpty(expected);
            Assert.AreEqual(expected, plcResult);
        }

        [Test, Order(200)]
        public void ClearMethodWillAbandonWorkingString()
        {
            //arrange
            stringBuilderTests.Clear();
            //act
            var plcResult = stringBuilderTests.FluentApi();
            Assert.AreEqual(plcResult,"1234");
            stringBuilderTests.Clear();
            //assert
            Assert.IsEmpty(stringBuilderTests.StringBuilderResult.Synchron);
        }


        [Test, Order(300)]
        public void WorksAsCSharpSb()
        {
            //arrange
            stringBuilderTests.Clear();
            var csharpSb = new StringBuilder();

            //act

            csharpSb.Append("One");
            csharpSb.Clear();
            csharpSb.Append("Two");
            csharpSb.Append("Three");

            stringBuilderTests.Append("One");
            stringBuilderTests.Clear();
            stringBuilderTests.Append("Two");
            stringBuilderTests.Append("Three");
            //assert
            Assert.AreEqual(stringBuilderTests.ToString(),csharpSb.ToString());
            Assert.IsNotEmpty(stringBuilderTests.ToString());
            Assert.IsNotEmpty(csharpSb.ToString());
        }


    }
}