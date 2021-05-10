using System.Configuration;

namespace TestAutomation.Framework.Common
{
    public class BaseTestConfig : ConfigurationSection
    {
        [ConfigurationProperty("Url")] public string Url => (string)this["Url"];
        [ConfigurationProperty("Environment")] public TargetEnvironment Environment => (TargetEnvironment)this["Environment"];
        [ConfigurationProperty("ReportName")] public string ReportName => (string)this["ReportName"];
        [ConfigurationProperty("ReportFileName")] public string ReportFileName => (string)this["ReportFileName"];
    }
}
