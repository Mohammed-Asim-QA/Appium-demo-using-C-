using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AppiumDemo
{
    [TestFixture]
    public class Extent
    {
        public ExtentReports extent = new ExtentReports();
        public ExtentTest test;
        [OneTimeSetUp]
        public void StartReport()
        {
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            string reportPath = projectPath + "Reports\\MyOwnReport.html";
            //Console.WriteLine("the reports path is {0}", reportPath);

            extent = new ExtentReports();
            // extent = new ExtentReports(reportPath);
            /* extent
             .AddSystemInfo("Host Name", "Mohammed")
             .AddSystemInfo("Environment", "QA")
             .AddSystemInfo("User Name", "Test User");
             extent.LoadConfig(projectPath + "extent-config.xml");*/
            var dir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\Test_Execution_Reports", "dddd");
            DirectoryInfo di = Directory.CreateDirectory(dir + "\\Test_Execution_Reports");
            var htmlReporter = new ExtentHtmlReporter(dir + "\\Test_Execution_Reports" + "\\Automation_Report" + "MyOwnReport.html");
            extent.AddSystemInfo("Environment", "Leeds Digital Festival");
            extent.AddSystemInfo("User Name", "Mohammed Asim");
            extent.AddSystemInfo("AppName", "Appium Demo");
            extent.AttachReporter(htmlReporter);
           


        }


        //[Test]
        //public void DemoReportPass()
        //{
        //    //test = extent.StartTest("DemoReportPass");
        //    test = extent.CreateTest("DemoReportsPass");
        //    Assert.IsTrue(true);
        //    test.Log(Status.Pass, "Assertion Pass as condition is true");
        //   // test.Log(LogStatus.Pass, "Assert Pass as condition is True");
        //}

        //[Test]
        //public void DemoReportFail()
        //{
        //    test = extent.CreateTest("DemoReportFail");
        //    Assert.IsTrue(false);
        //    test.Log(Status.Pass, "Assert Fail as condition is False");
        //}

        [TearDown]
        public void GetResult()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";
            var errorMessage = TestContext.CurrentContext.Result.Message;

            if (status == TestStatus.Failed)
            {
                test.Log(Status.Fail, stackTrace + errorMessage);
            }
            
            //extent.EndTest(test);
        }

        [OneTimeTearDown]
        public void EndReport()
        {
            extent.Flush();
            // extent.Close(); not working
        }

    }
}
