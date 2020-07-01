
namespace FeatureTogglesCoreTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using FeatureToggles;
    using FeatureToggles.Configuration;
    using FeatureToggles.Models;
    using FeatureToggles.Providers;
    using FeatureToggles.Providers.AppSettings;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class ToggleFactoryTests
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return config;
        }

        [Test]
        public void GetMissingToggleTest()
        {
            Mock<IToggleConfiguration> config = new Mock<IToggleConfiguration>(MockBehavior.Strict);
            config.SetupGet(x => x.SystemEnabled)
                .Returns(true);

            config.SetupGet(x => x.DefaultValue)
                .Returns(false);

            Mock<IToggleDataProvider> dataProvider = new Mock<IToggleDataProvider>(MockBehavior.Strict);
            dataProvider.Setup(x => x.GetFlag(It.IsAny<string>()))
                .Returns((Toggle)null);

            ToggleFactory factory = new ToggleFactory(config.Object, dataProvider.Object);

            Toggle t = factory.Get("test");

            Assert.IsNotNull(t, "Toggle should never be null");
            Assert.IsTrue(Toggle.IsNullOrEmpty(t));
            Assert.IsFalse(t.IsEnabled, "Toggle should return the configured value of true");
        }

        [Test]
        public void GetToggleTest()
        {
            Mock<IToggleConfiguration> config = new Mock<IToggleConfiguration>(MockBehavior.Strict);
            config.SetupGet(x => x.SystemEnabled)
                .Returns(true);

            config.SetupGet(x => x.DefaultValue)
                .Returns(false);

            Mock<IToggleDataProvider> dataProvider = new Mock<IToggleDataProvider>(MockBehavior.Strict);
            dataProvider.Setup(x => x.GetFlag(It.IsAny<string>()))
                .Returns(new Toggle("test", true));

            ToggleFactory factory = new ToggleFactory(config.Object, dataProvider.Object);

            Toggle t = factory.Get("test");

            Assert.IsTrue(t.IsEnabled, "Toggle should return the configured value of true");
        }

        [Test]
        public void GetDefaultValueTest()
        {
            Mock<IToggleConfiguration> config = new Mock<IToggleConfiguration>(MockBehavior.Strict);
            config.SetupGet(x => x.SystemEnabled)
                .Returns(false);

            config.SetupGet(x => x.DefaultValue)
                .Returns(true);

            Mock<IToggleDataProvider> dataProvider = new Mock<IToggleDataProvider>(MockBehavior.Strict);
            dataProvider.Setup(x => x.GetFlag(It.IsAny<string>()))
                .Returns(new Toggle("test", false));

            ToggleFactory factory = new ToggleFactory(config.Object, dataProvider.Object);

            Toggle t = factory.Get("test");

            Assert.IsTrue(t.IsEnabled, "Toggle should return the default value of true");
        }

        [Test]
        public void GetValidUserFlagTest()
        {
            ToggleFactory factory = new ToggleFactory(GetEnabledConfiguration(), new AppSettingsDataProvider(InitConfiguration()));
            ToggleData data = new ToggleData("abcd", string.Empty);

            Toggle t1 = factory.Get("CacheInheritableDatasource", data);

            Assert.IsTrue(t1.IsEnabled);
        }

        [Test]
        public void GetValidRoleFlagTest()
        {
            ToggleFactory factory = new ToggleFactory(GetEnabledConfiguration(), new AppSettingsDataProvider(InitConfiguration()));
            ToggleData data = new ToggleData("abcd", string.Empty, "Staff");

            Toggle t1 = factory.Get("CacheInheritableDatasource", data);

            Assert.IsTrue(t1.IsEnabled);
        }

        [Test]
        public void GetValidToggleDataBasedFlagTest()
        {
            ToggleFactory factory = new ToggleFactory(GetEnabledConfiguration(), new AppSettingsDataProvider(InitConfiguration()));
            ToggleData data = new ToggleData("abcd", "127.0.0.2", "Staff");

            Toggle t1 = factory.Get("CacheInheritableDatasource", data);

            Assert.IsTrue(t1.IsEnabled);
        }

        [Test]
        public void GetInvalidToggleDataBasedFlagTest()
        {
            ToggleFactory factory = new ToggleFactory(GetEnabledConfiguration(), new AppSettingsDataProvider(InitConfiguration()));
            ToggleData data = new ToggleData("abcd", "128.0.0.2", "Staff");

            Toggle t1 = factory.Get("CacheInheritableDatasource", data);

            Assert.IsFalse(t1.IsEnabled);
        }

        [Test]
        public void GetInvalidUserFlagTest()
        {
            ToggleFactory factory = new ToggleFactory(GetEnabledConfiguration(), new AppSettingsDataProvider(InitConfiguration()));
            ToggleData data = new ToggleData("wibble", string.Empty);

            Toggle t1 = factory.Get("CacheInheritableDatasource", data);

            Assert.IsFalse(t1.IsEnabled);
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
    }
}
