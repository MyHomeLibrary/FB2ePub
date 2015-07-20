using ConverterContracts.Settings;
using ConverterTester.Tests;
using NUnit.Framework;

namespace ConverterTester
{
    [TestFixture]
    class Fb2EPubTester
    {
        //private const string TestFileName = "Test_001.fb2";

        [Test]
        public void TestConfig()
        {
            var test = new ConfigFileTester(); // configuration file tester
            test.Test();
        }


        [Test]
        public void TestV2()
        {
            var test = new EPubConversionTester(EPubVersion.V2); // V2 conversion test 
            test.Test();
        }

        [Test]
        public void TestV3()
        {
            var test = new EPubConversionTester(EPubVersion.V3); // V3 conversion test 
            test.Test();
        }
   }
}
