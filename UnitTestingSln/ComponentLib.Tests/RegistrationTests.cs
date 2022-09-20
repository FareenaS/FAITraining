using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Complete the method provided which accepts a collection of registration numbers and returns a collection of registration numbers which are sorted on RTO-Zonal area where the vehicles were registered. Note: If vehicles are from same area then they should be sorted based on registration sequence number
namespace ComponentLib.Tests
{
    [TestFixture]
    class RegistrationTests
    {
        [SetUp]
        public void CreateSetup()
        {

        }
        [Test]
        public void When_Vehicles_SortedByArea()
        {
            var inputs = new string[] { "KA-55-AB-4555", "KA-01-EF-4444", "KA-04-AB-9000", "KA-56-200", "KA-50-T-3111", "KA-02-AG-9243" };
            var expected = new string[] { "KA-56-200", "KA-01-EF-4444", "KA-55-AB-4555", "KA-02-AG-9243", "KA-50-T-3111", "KA-04-AB-9000" };
        }

        [Test]
        public void When_Vehicles_SortedByAreaAndNumbers()
        {
            var inputs = new string[] {"KA-57-DE-111", "KA-51-A-9", "KA-04-500", "KA-02-L-41"};
            var expected = new string[] { "KA-51-A-9", "KA-02-L-41", "KA-57-DE-111", "KA-04-500"};
        }
    }
}
