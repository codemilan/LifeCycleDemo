using System;
using NUnit.Framework;
using POIApp;

namespace POITestApp
{
    [TestFixture]
    public class POITestFixture
    {
        IPOIDataService _poiService;

        [SetUp]
        public void Setup()
        {
            _poiService = new POIJsonService();
        }

        [TearDown]
        public void Tear() { }

        [Test]
        public void Pass()
        {
            Console.WriteLine("test1");
            Assert.True(true);
        }

        [Test]
        public void Fail()
        {
            Assert.False(true);
        }

        [Test]
        [Ignore("another time")]
        public void Ignore()
        {
            Assert.True(false);
        }

        [Test]
        public void Inconclusive()
        {
            Assert.Inconclusive("Inconclusive");
        }

        [Test]
        public void CreatePOI()
        {
            PointOfInterest newPOI = new PointOfInterest();
            newPOI.Name = "New POI";
            newPOI.Description = "POI to test creation of new poi.";
            newPOI.Address = "100 Main Street\nAnywhere, TX 75069";
            _poiService.SavePOI(newPOI);

            int testId = newPOI.Id.Value;
            //refresh the cache to to be sure the data was saved properly.
            _poiService.RefreshCache();
            //verify that the newly created poi exists.
            PointOfInterest poi = _poiService.GetPOI(testId);
            Assert.NotNull(poi);
            Assert.AreEqual(poi.Name, "New POI");
        }
    }
}