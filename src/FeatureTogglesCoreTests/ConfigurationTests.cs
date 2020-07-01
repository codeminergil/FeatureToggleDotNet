
namespace FeatureTogglesCoreTests.JsonTests
{
    using Microsoft.Extensions.Configuration;
    using NUnit.Framework;
    using FeatureTogglesIConfiguration.JsonConfiguration;
    using NUnit.Framework.Internal;

    [TestFixture]
    public class ConfigurationTests
    {

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                //.AddJsonFile("app.config.json")
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
