using System;
using System.Collections.Generic;
using System.Linq;
using ConverterTester.Tests;

namespace ConverterTester
{
    class Program
    {
        //private const string TestFileName = "Test_001.fb2";



        static void Main(string[] args)
        {           
            foreach (var test in InitTesters().Where(test => !test.Test()))
            {
                throw new Exception(string.Format("Test {0} failed",test.Name));
            }
        }

        private static IEnumerable<ITester> InitTesters()
        {
            var testList = new List<ITester>
            {
                new ConfigFileTester(), // configuration file tester
            };


            return testList;
        }
    }
}
