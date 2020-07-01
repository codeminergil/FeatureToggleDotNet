
namespace FeatureTogglesCoreTests.JsonTests
{
    using Microsoft.Extensions.Configuration;
    using Moq;
    using NUnit.Framework;
    using System.Diagnostics.CodeAnalysis;
    using FeatureTogglesIConfiguration;
    using FeatureTogglesIConfiguration.JsonConfiguration;

    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class ToggleTests
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("app.config.json")
                .Build();
            return config;
        }

        private IToggleConfiguration GetEnabledConfiguration()
        {
            Mock<IToggleConfiguration> config = new Mock<IToggleConfiguration>(MockBehavior.Strict);
            config.SetupGet(x => x.SystemEnabled)
                .Returns(true);

            config.SetupGet(x => x.DefaultValue)
                .Returns(false);

            return config.Object;
        }

        [Test]
        public void SystemEnabledTest()
        {
            IToggleConfiguration provider = new AppConfigurationProvider(InitConfiguration());

            Assert.IsTrue(provider.SystemEnabled);
        }

        [Test]
        public void DefaultValueTest()
        {
            IToggleConfiguration provider = new AppConfigurationProvider(InitConfiguration());

            Assert.IsFalse(provider.DefaultValue);
        }

        //[Test]
        //public void EnabledFlagTest()
        //{
        //    Mock<IToggleDataProvider> dataProvider = new Mock<IToggleDataProvider>(MockBehavior.Strict);
        //    dataProvider.Setup(x => x.GetFlag("enabledFeature"))
        //        .Returns(new Toggle("enabledFeature", true));
        //    dataProvider.Setup(x => x.GetFlag("disabledFeature"))
        //        .Returns(new Toggle("disabledFeature", false));

        //    ToggleFactory factory = new ToggleFactory(GetEnabledConfiguration(), dataProvider.Object);

        //    Toggle t = factory.Get("enabledFeature");

        //    Assert.IsFalse(t == Toggle.Empty);
        //    Assert.IsFalse(Toggle.IsNullOrEmpty(t));
        //    Assert.AreEqual("enabledFeature", t.Name);
        //    Assert.IsTrue(t.IsEnabled);
        //}
    }
}
