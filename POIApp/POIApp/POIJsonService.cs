// HIT_N_TRAIL: i think this class should be platform specific means should contain in each
// platform specific project
// since this implements the interface that can be a abstration to orm in portable project.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace POIApp
{
    public class POIJsonService : IPOIDataService
    {
        private string _storagePath;
        private List<PointOfInterest> _pois = new List<PointOfInterest>();

        public POIJsonService(string storagePath)
        {
            _storagePath = storagePath;
            //create storage location if it doesn't exists.
            if (!Directory.Exists(_storagePath))
                Directory.CreateDirectory(_storagePath);
            RefreshCache();
        }

        #region IPOIDataService implementation
        public IReadOnlyList<PointOfInterest> POIs
        {
            get { return _pois; }
        }

        public void DeletePOI(PointOfInterest poi)
        {
            File.Delete(GetFilename(poi.Id.Value));
            _pois.Remove(poi);
        }

        public PointOfInterest GetPOI(int id)
        {
            PointOfInterest poi = _pois.Find(p => p.Id == id);
            return poi;
        }

        public void RefreshCache()
        {
            _pois.Clear();
            string[] filenames = Directory.GetFiles(_storagePath, "*.json");

            foreach(string filename in filenames)
            {
                string poiString = File.ReadAllText(filename);
                PointOfInterest poi = JsonConvert.DeserializeObject<PointOfInterest>(poiString);
                _pois.Add(poi);
            }
        }

        public void SavePOI(PointOfInterest poi)
        {
            Boolean newPOI = false;
            if (!poi.Id.HasValue)
            {
                poi.Id = GetNextId();
                newPOI = true;
            }
            // serialize POI
            string poiString = JsonConvert.SerializeObject (poi);
            // write new file or overwrite existing file
            File.WriteAllText (GetFilename (poi.Id.Value), poiString);
            // update cache if file save was successful
            if (newPOI)
                _pois.Add (poi);
        }
        #endregion

        private int GetNextId()
        {
            if (_pois.Count == 0)
                return 1;
            else
                return _pois.Max(p => p.Id.Value) + 1;
        }

        private string GetFilename(int id)
        {
            return Path.Combine(_storagePath, "poi" + id.ToString() + ".json");
        }
    }
}