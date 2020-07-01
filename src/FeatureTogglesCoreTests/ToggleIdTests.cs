using NUnit.Framework;
using System;

namespace FeatureTogglesCoreTests
{
    using FeatureToggles;
    using NUnit.Framework;

    [TestFixture]
    public class ToggleIdTests
    {
        [Test]
        public void EqualityTest()
        {
            ToggleId id = new ToggleId("test");
            ToggleId id2 = new ToggleId("test");

            Assert.AreEqual(id, id2, "Toggle ids have the same name so should be equal");
        }

        [Test]
        public void InequalityTest()
        {
            ToggleId id = new ToggleId("test");
            ToggleId id2 = new ToggleId("test2");

            Assert.AreNotEqual(id, id2, "Toggle ids have the different names so should be different");
        }
    }
}