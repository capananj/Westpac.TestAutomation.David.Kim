using System.Configuration;

namespace TestAutomation.Framework.Common
{
    public class TestConfigLoader
    {
        public static T Load<T>(string sectionName) where T : ConfigurationSection
        {
            return (T)ConfigurationManager.GetSection(sectionName);
        }
    }
}
