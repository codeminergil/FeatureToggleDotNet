//-----------------------------------------------------------------------
// <copyright file="ToggleTests.cs" company="Code Miners Limited">
//  Copyright (c) 2019 Code Miners Limited
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.If not, see<https://www.gnu.org/licenses/>.
// </copyright>
//-----------------------------------------------------------------------

namespace ToggleTestsAppSettings.json
{
    using Microsoft.Extensions.Configuration;
    using Moq;
    using NUnit.Framework;
    using System.Diagnostics.CodeAnalysis;
    using FeatureToggles;
    using FeatureToggles.Configuration;
    using FeatureToggles.Configuration.AppSettings.Providers;
    using FeatureToggles.Providers;
    using FeatureToggles.Providers.AppSettings;

    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class ToggleTests
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
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
            IToggleConfiguration provider = new AppSettingsConfigurationProvider(InitConfiguration());

            Assert.IsTrue(provider.SystemEnabled);
        }

        [Test]
        public void DefaultValueTest()
        {
            IToggleConfiguration provider = new AppSettingsConfigurationProvider(InitConfiguration());

            Assert.IsFalse(provider.DefaultValue);
        }

        [Test]
        public void EnabledFlagTest()
        {
            Mock<IToggleDataProvider> dataProvider = new Mock<IToggleDataProvider>(MockBehavior.Strict);
            dataProvider.Setup(x => x.GetFlag("enabledFeature"))
                .Returns(new Toggle("enabledFeature", true));
            dataProvider.Setup(x => x.GetFlag("disabledFeature"))
                .Returns(new Toggle("disabledFeature", false));

            ToggleFactory factory = new ToggleFactory(GetEnabledConfiguration(), dataProvider.Object);

            Toggle t = factory.Get("enabledFeature");

            Assert.IsFalse(t == Toggle.Empty);
            Assert.IsFalse(Toggle.IsNullOrEmpty(t));
            Assert.AreEqual("enabledFeature", t.Name);
            Assert.IsTrue(t.IsEnabled);
        }

        [Test]
        public void DisabledFlagTest()
        {
            Mock<IToggleDataProvider> dataProvider = new Mock<IToggleDataProvider>(MockBehavior.Strict);
            dataProvider.Setup(x => x.GetFlag("enabledFeature"))
                .Returns(new Toggle("enabledFeature", true));
            dataProvider.Setup(x => x.GetFlag("disabledFeature"))
                .Returns(new Toggle("disabledFeature", false));

            ToggleFactory factory = new ToggleFactory(GetEnabledConfiguration(), dataProvider.Object);

            Toggle t = factory.Get("disabledFeature");

            Assert.IsFalse(t == Toggle.Empty);
            Assert.IsFalse(Toggle.IsNullOrEmpty(t));
            Assert.AreEqual("disabledFeature", t.Name);
            Assert.IsFalse(t.IsEnabled);
        }

        [Test]
        public void ToggleEqualityTest()
        {
            Mock<IToggleDataProvider> dataProvider = new Mock<IToggleDataProvider>(MockBehavior.Strict);
            dataProvider.Setup(x => x.GetFlag("enabledFeature"))
                .Returns(new Toggle("enabledFeature", true));
            dataProvider.Setup(x => x.GetFlag("disabledFeature"))
                .Returns(new Toggle("disabledFeature", false));

            ToggleFactory factory = new ToggleFactory(GetEnabledConfiguration(), dataProvider.Object);

            Toggle t1 = factory.Get("enabledFeature");
            Toggle t2 = factory.Get("disabledFeature");

            Assert.IsFalse(t1 == t2);
        }

        [Test]
        public void LoadToggleFromConfigTest()
        {
            ToggleFactory factory = new ToggleFactory(GetEnabledConfiguration(), new AppSettingsDataProvider(InitConfiguration()));

            Toggle t1 = factory.Get("CacheInheritableDatasource");

            Assert.IsTrue(t1 != Toggle.Empty);
        }
    }
}
