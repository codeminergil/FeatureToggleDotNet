
namespace FeatureTogglesCoreTests
{
    using FeatureToggles.Configuration.AppSettings.ToggleSettings;
    using Microsoft.Extensions.Configuration;
    using NUnit.Framework;
    using System.Diagnostics.CodeAnalysis;

    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class ToggleSettingsTest
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return config;
        }

        [Test]
        public void TestLoadToggleSettings()
        {
            IConfiguration configuration = InitConfiguration();

            ToggleSettings appSettings = configuration.GetSection("appSettings").Get<ToggleSettings>();

            Assert.IsNotNull(appSettings);
        }
    }
}
