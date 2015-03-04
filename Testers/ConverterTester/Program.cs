using System.Collections.Generic;
using System.Linq;
using ConverterContracts.Settings;
using ConverterTester.Tests;
using EPubLibrary;

namespace ConverterTester
{
    class Program
    {
        //private const string TestFileName = "Test_001.fb2";



        static void Main(string[] args)
        {           
            foreach (var test in InitTesters().Where(test => !test.Test()))
            {
                Logger.Log.Error(string.Format("Test {0} failed",test.Name));
            }
        }

        private static IEnumerable<ITester> InitTesters()
        {
            var testList = new List<ITester>
            {
                new ConfigFileTester(), // configuration file tester
                new EPubConversionTester(EPubVersion.V2), // V2 conversion test 
                new EPubConversionTester(EPubVersion.V3), // V3 conversion test
            };


            return testList;
        }
    }
}
