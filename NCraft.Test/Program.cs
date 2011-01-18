using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Core;
using System.Reflection;
using System.IO;
using NUnit.Core.Builders;
using log4net;
using NUnit.Util;
using NUnit.Framework;
using System.Diagnostics;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace NCraft.Test
{
    class Program
    {
        private static ILog Logger = LogManager.GetLogger(typeof(Program));

        static void Main(String[] args)
        {
            Debug.WriteLine("TestRunner starting...");

            var path = Assembly.GetExecutingAssembly().Location;

            CoreExtensions.Host.InitializeService();
            using (var testRunner = new RemoteTestRunner())
            {
                var package = new TestPackage("NCraft.Test.exe", new List<string>() { path });
                testRunner.Load(package);

                testRunner.Run(new LoggingEventListener(), TestFilter.Empty);
            }

            Debug.WriteLine("TestRunner complete!");        
        }
    }

    public class LoggingEventListener : EventListener
    {
        private static ILog Logger = LogManager.GetLogger(typeof(LoggingEventListener));

        private int runCount = 0;
        private int runSuccessCount = 0;

        private int suiteCount = 0;
        private int suiteSuccessCount = 0;

        private int testCount = 0;
        private int testSuccessCount = 0;

        private int unhandledExceptionCount = 0;

        private bool verbose = false;

        public void RunFinished(Exception exception)
        {
            if (verbose)
            {
                Debug.WriteLine("Run finished with Exception: " + exception);
            }

            WriteResults();
        }

        public void RunFinished(TestResult result)
        {
            if (verbose)
            {
                Debug.WriteLine("Run finished with result: " + result.ResultState.ToString());
            }

            if (result.IsSuccess)
            {
                runSuccessCount++;
            }

            WriteResults();
        }

        public void RunStarted(string name, int testCount)
        {
            if (verbose)
            {
                Debug.WriteLine("Run started: {0} ({1} tests)", name, testCount);
            }
            runCount++;
        }

        public void SuiteFinished(TestResult result)
        {
            if (verbose)
            {
                Debug.WriteLine("Suite finished with result: " + result.ResultState.ToString());
            }

            if (result.IsSuccess)
            {
                suiteSuccessCount++;
            }
        }

        public void SuiteStarted(TestName testName)
        {
            if (verbose)
            {
                Debug.WriteLine("Suite starting: " + testName.Name);
            }
            suiteCount++;
        }

        public void TestFinished(TestResult result)
        {
            if (verbose)
            {
                Debug.WriteLine("Test finished with result: " + result.ResultState.ToString());
            }

            if (result.IsSuccess)
            {
                testSuccessCount++;
            }
            else
            {
                Debug.WriteLine("    Test failed: " + result.FullName);
                Debug.WriteLine("      " + result.Message);
            }
        }

        public void TestOutput(TestOutput testOutput)
        {
            //Debug.WriteLine("TestOutput: {0}", testOutput);
        }

        public void TestStarted(TestName testName)
        {
            //if (verbose)
            {
                Debug.WriteLine("Test starting: " + testName.FullName);
            }
            testCount++;
        }

        public void UnhandledException(Exception exception)
        {
            Debug.WriteLine("Unhandled Exception: " + exception);
            
            unhandledExceptionCount++;
        }

        private void WriteResults()
        {
            Debug.WriteLine("----------------------------------------------------------------");
            Debug.WriteLine("{0} out of {1} tests passed.", testSuccessCount, testCount);
            Debug.WriteLine("{0} out of {1} suites passed.", suiteSuccessCount, suiteCount);
            Debug.WriteLine("{0} out of {1} runs passed.", runSuccessCount, runCount);
            Debug.WriteLine("{0} unhandled exceptions", unhandledExceptionCount);
            Debug.WriteLine("----------------------------------------------------------------");
        }
    }
}
