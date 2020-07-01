
namespace FeatureTogglesCoreTests.TestModels
{
    using Microsoft.Extensions.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using FeatureToggles;
    using FeatureToggles.Configuration.AppSettings.Providers;
    using FeatureToggles.Providers.AppSettings;

    [ExcludeFromCodeCoverage]
    public static class StaticToggle
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return config;
        }

        public static IConfiguration configuration = InitConfiguration();
        private static readonly ToggleFactory Factory = new ToggleFactory(new AppSettingsConfigurationProvider(configuration), new AppSettingsDataProvider(configuration));

        public static bool IsEnabled
        {
            get
            {
                Toggle toggle = Factory.Get<StrongToggleId>();

                // OR:
                // Toggle toggle = Factory.Get("StaticToggle");

                return toggle.IsEnabled;
            }
        }
    }
}
