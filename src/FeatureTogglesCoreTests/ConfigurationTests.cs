//-----------------------------------------------------------------------
// <copyright file="ConfigurationTests.cs" company="Code Miners Limited">
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
    using NUnit.Framework;
    using FeatureToggles.Configuration.AppSettings;

    [TestFixture]
    public class ConfigurationTests
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return config;
        }

        [Test]
        public void TestLoadConfig()
        {
            IConfiguration configuration = InitConfiguration();

            ToggleConfigurationSection config = configuration.GetSection("ToggleConfiguration").Get<ToggleConfigurationSection>();

            Assert.NotNull(config);
        }

        [Test]
        public void ValidateSettingsTest()
        {
            IConfiguration configuration = InitConfiguration();

            ToggleConfigurationSection config = configuration.GetSection("ToggleConfiguration").Get<ToggleConfigurationSection>();

            ToggleElement toggle = config.toggles[0];

            Assert.IsNotNull(toggle);

            Assert.AreEqual("CacheInheritableDatasource", toggle.name);
            Assert.IsTrue(toggle.users.Count > 0);
            Assert.IsTrue(toggle.roles.Count > 0);
            Assert.IsTrue(toggle.ipaddresses.Count > 0);
        }

        [Test]
        public void ValidateUsersTest()
        {
            IConfiguration configuration = InitConfiguration();

            ToggleConfigurationSection config = configuration.GetSection("ToggleConfiguration").Get<ToggleConfigurationSection>();

            ToggleElement toggle = config.toggles[0];

            Assert.IsNotNull(toggle);

            Assert.AreEqual("CacheInheritableDatasource", toggle.name);
            Assert.IsTrue(toggle.users.Count > 0);

            Assert.AreEqual("abcd", toggle.users[0].user.name);
        }

        [Test]
        public void ValidateRolesTest()
        {
            IConfiguration configuration = InitConfiguration();

            ToggleConfigurationSection config = configuration.GetSection("ToggleConfiguration").Get<ToggleConfigurationSection>();

            ToggleElement toggle = config.toggles[0];

            Assert.IsNotNull(toggle);

            Assert.AreEqual("CacheInheritableDatasource", toggle.name);
            Assert.IsTrue(toggle.roles.Count > 0);

            Assert.AreEqual("Staff", toggle.roles[0].role.name);
        }

        [Test]
        public void ValidateIpAddressesTest()
        {
            IConfiguration configuration = InitConfiguration();

            ToggleConfigurationSection config = configuration.GetSection("ToggleConfiguration").Get<ToggleConfigurationSection>();

            ToggleElement toggle = config.toggles[0];

            Assert.IsNotNull(toggle);

            Assert.AreEqual("CacheInheritableDatasource", toggle.name);
            Assert.IsTrue(toggle.ipaddresses.Count > 0);

            Assert.AreEqual("127.0.0.1/28", toggle.ipaddresses[0].ipaddress.value);
        }
    }
}
