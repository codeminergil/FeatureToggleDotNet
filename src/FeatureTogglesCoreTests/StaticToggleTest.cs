
namespace FeatureTogglesCoreTests
{
    using NUnit.Framework;
    using System.Diagnostics.CodeAnalysis;
    using FeatureTogglesCoreTests.TestModels;

    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class CoreStaticToggleTest
    {
        [Test]
        public void StaticToggleTest()
        {
            bool flag = StaticToggle.IsEnabled;

            Assert.IsTrue(flag, "Static flag should be enabled");
        }
    }
}
