using NUnit.Framework;
using System;

namespace FeatureTogglesCoreTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using FeatureToggles.Models;
    using NUnit.Framework;

    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class IPAddressRangeTests
    {
        [Test]
        public void ParseCidrBlockTest()
        {
            string ip = "127.10.0.0/28";
            IPAddressRange range = IPAddressRange.FromCidrAddress(ip);

            IPAddress.TryParse("127.10.0.0", out IPAddress lower);
            IPAddress.TryParse("127.10.0.15", out IPAddress higher);

            Assert.IsFalse(IPAddressRange.IsNullOrEmpty(range));
            Assert.AreEqual(lower, range.Lower);
            Assert.AreEqual(higher, range.Upper);
            Console.Error.WriteLine(range.ToString());
        }

        [Test]
        public void Foo()
        {
            string ip = "127.0.0.1/28";
            IPAddressRange range = IPAddressRange.FromCidrAddress(ip);

            Assert.IsFalse(IPAddressRange.IsNullOrEmpty(range));
            Console.Error.WriteLine(range.ToString());
        }

        [Test]
        public void IPAddressInRangeTest()
        {
            string ip = "127.10.0.0/28";
            IPAddressRange range = IPAddressRange.FromCidrAddress(ip);

            IPAddress.TryParse("127.10.0.1", out IPAddress candidate);

            Assert.IsTrue(range.IPInRange(candidate));
        }

        [Test]
        public void IPAddressNotInRangeTest()
        {
            string ip = "127.10.0.0/28";
            IPAddressRange range = IPAddressRange.FromCidrAddress(ip);

            IPAddress.TryParse("192.168.0.1", out IPAddress candidate);

            Assert.IsFalse(range.IPInRange(candidate));
        }
    }
}
