using AventStack.ExtentReports;
using System.Collections;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using TestAutomation.Framework.Common;
using TestAutomation.Framework.UI;

namespace TestAutomation.Tests.UI.Hooks
{
    [Binding]
    public class AfterHooks
    {
        private static ExtentReporter _reporter;
        
        [AfterStep(Order = 0)]
        public void LogStepContext(ScenarioContext scenarioContext)
        {
            var testContext = scenarioContext.ScenarioContainer.Resolve<UITestContext>();
            var reporter = testContext.Reporter;
            var screenshot = testContext.Driver.TakeScreenshotAsBase64();
            var step = reporter.LogStep(scenarioContext, screenshot);

            testContext.LastStep = step;
            testContext.LastStepScreenshot = screenshot;
        }

        [AfterScenario(Order = 0)]
        public void CloseTest(ScenarioContext scenarioContext)
        {
            var testContext = scenarioContext.ScenarioContainer.Resolve<UITestContext>();
            var scenarioTitle = scenarioContext.ScenarioInfo.Title;
            var reporter = testContext.Reporter;
            var lastStep = testContext.LastStep;
            var arguments = scenarioContext.ScenarioInfo.Arguments;
            string lastStepScreenshot = null;
            string[,] tableData = null;

            if (arguments != null && arguments.Count > 0)
            {
                var rowSize = 2;
                var keys = new List<string>();

                foreach (DictionaryEntry row in arguments)
                {
                    keys.Add(row.Key.ToString());
                }

                tableData = new string[rowSize, keys.Count];

                for (int i = 0; i < keys.Count; i++)
                {
                    tableData[0, i] = keys[i];
                    tableData[1, i] = arguments[keys[i]].ToString();
                }
            }

            if (lastStep.Status == Status.Pass)
            {
                lastStepScreenshot = testContext.LastStepScreenshot;
            }

            reporter.FinalizeTest(scenarioTitle, lastStep, tableData, lastStepScreenshot);
        }

        [AfterFeature]
        public static void DisposeFeatureWebDriver(FeatureContext featureContext)
        {
            var testContext = featureContext.FeatureContainer.Resolve<UITestContext>();
            var driver = testContext.Driver;
            driver.Quit();

            if (_reporter == null)
            {
                _reporter = testContext.Reporter;
            }
        }

        [AfterTestRun]
        public static void CloseReport()
        {
            _reporter.Close();
        }
    }
}
