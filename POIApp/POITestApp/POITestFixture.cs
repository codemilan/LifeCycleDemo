using System;
using System.IO;
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
            string storagePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            _poiService = new POIJsonService(storagePath);

            foreach(string filename in Directory.EnumerateFiles(storagePath, "*.json"))
            {
                File.Delete(filename);
            }
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

        [Test]
        public void UpdatePOI()
        {
            PointOfInterest testpoi = new PointOfInterest();
            testpoi.Name = "Update POI";
            testpoi.Description = "POI being saved so we can test poi";
            testpoi.Address = "100 Main streed";
            _poiService.SavePOI(testpoi);
            int testId = testpoi.Id.Value;
            _poiService.RefreshCache();
            PointOfInterest poi = _poiService.GetPOI(testId);
            Assert.NotNull(poi);
            Assert.AreEqual(poi.Description, "POI being saved so we can test poi");
        }

        [Test]
        public void DeletePOI()
        {
            PointOfInterest testPOI = new PointOfInterest();
            testPOI.Name = "Delete POI";
            testPOI.Description = "POI being saved so we can test delete";
            testPOI.Address = "100 Main Street\nAnywhere, TX 75069";
            _poiService.SavePOI(testPOI);
            int testId = testPOI.Id.Value;
            // refresh the cache to be sure the data and   // poi was saved appropriately  _poiService.RefreshCache ();
            PointOfInterest deletePOI = _poiService.GetPOI(testId);
            Assert.IsNotNull(deletePOI);
            _poiService.DeletePOI(deletePOI);
            // refresh the cache to be sure the data was
            // deleted appropriately  _poiService.RefreshCache ();
            PointOfInterest poi = _poiService.GetPOI(testId);
            Assert.Null(poi);
        }
    }
}