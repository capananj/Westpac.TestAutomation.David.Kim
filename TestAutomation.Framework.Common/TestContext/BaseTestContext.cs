using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace TestAutomation.Framework.Common
{
    public class BaseTestContext : SpecFlowContext
    {
        public string BaseUrl { get; set; }
        public LoginUser LoginUser { get; set; }
        public List<LoginUser> UserCredentials { get; set; }
        public ExtentReporter Reporter { get; set; }
    }
}
