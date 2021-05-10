using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.IO;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;

namespace TestAutomation.Framework.Common
{
    public class ExtentReporter
    {
        public ExtentReports Extent { get; set; }
        public ExtentHtmlReporter Reporter { get; set; }
        public Dictionary<string, ExtentTest> Features { get; set; }
        public Dictionary<string, ExtentTest> Scenarios { get; set; }
        public string ReportFileName { get; set; }
        public string ReportFolder { get; set; }
        public ExtentReporter(string reportName, TargetEnvironment environment, string reportFileName = "")
        {
            Extent = new ExtentReports();
            Features = new Dictionary<string, ExtentTest>();
            Scenarios = new Dictionary<string, ExtentTest>();

            ReportFolder = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName, "TestResults");

            if (!Directory.Exists(ReportFolder))
            {
                Directory.CreateDirectory(ReportFolder);
            }

            Reporter = new ExtentHtmlReporter($@"{ReportFolder}\");
            Reporter.Config.DocumentTitle = "Automated Test Report";
            Reporter.Config.ReportName = $"{reportName}";
            Reporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            Extent.AttachReporter(Reporter);
            Extent.AddSystemInfo("Report Name", reportName);
            Extent.AddSystemInfo("Environment", environment.ToString());
            Extent.AddSystemInfo("Machine", Environment.MachineName);
            Extent.AddSystemInfo("OS", Environment.OSVersion.VersionString);

            ReportFileName = reportFileName;
        }
        public void CreateFeature(string featureTitle, string description = "")
        {
            var feature = Extent.CreateTest<Feature>(featureTitle, description);
            Features.Add(featureTitle, feature);
        }

        public void CreateScenario(string featureTitle, string scenarioTitle, string description = "")
        {
            var scenario = Features[featureTitle].CreateNode<Scenario>(scenarioTitle, description);
            Scenarios[scenarioTitle] = scenario;
        }

        public ExtentTest LogStep(ScenarioContext scenarioContext, string screenshotBase64)
        {
            ExtentTest step = null;

            var scenarioTitle = scenarioContext.ScenarioInfo.Title.Trim();
            var testStatus = scenarioContext.ScenarioExecutionStatus;
            var stepInfo = scenarioContext.StepContext.StepInfo;
            var testError = scenarioContext.TestError;

            switch (stepInfo.StepDefinitionType)
            {
                case StepDefinitionType.Given:
                    step = Scenarios[scenarioTitle].CreateNode<Given>(stepInfo.Text);
                    break;
                case StepDefinitionType.When:
                    step = Scenarios[scenarioTitle].CreateNode<When>(stepInfo.Text);
                    break;
                case StepDefinitionType.Then:
                    step = Scenarios[scenarioTitle].CreateNode<Then>(stepInfo.Text);
                    break;
            }

            if (testStatus != ScenarioExecutionStatus.OK && !screenshotBase64.IsNullOrEmpty())
            {
                step.AddScreenCaptureFromBase64String(screenshotBase64, scenarioTitle);
            }

            if (testStatus == ScenarioExecutionStatus.TestError)
            {
                var errorMessage = testError.InnerException != null ? testError.InnerException.Message : testError.Message;
                step.Fail(errorMessage);
            }
            else if (testStatus != ScenarioExecutionStatus.OK)
            {
                step.Skip($"Step execution failed. ScenarioExecutionStatus: {testStatus}");
            }

            return step;
        }

        public void FinalizeTest(string scenarioTitle, ExtentTest lastStep, string[,] tableData = null, string screenshotBase64 = null)
        {
            if (screenshotBase64 != null)
            {
                lastStep
                    .AddScreenCaptureFromBase64String(screenshotBase64, scenarioTitle);
            }

            if (tableData != null)
            {
                var markupTable = MarkupHelper.CreateTable(tableData);

                lastStep
                    .Log(Status.Info, markupTable.GetMarkup());
            }
        }

        public void Close()
        {
            Extent.Flush();
            RenameReport();
        }

        public void RenameReport()
        {
            if (ReportFileName == "")
            {
                ReportFileName = "TestResult";
            }
            else
            {
                ReportFileName = ReportFileName.Replace(".html", "");
            }
            var currentFilePath = Path.Combine(ReportFolder, "index.html");
            var newFilePath = Path.Combine(ReportFolder, $"{ReportFileName}_{DateTime.Now:yyyyMMdd-HHmmss}.html");
            File.Move(currentFilePath, newFilePath);
        }
    }
}
