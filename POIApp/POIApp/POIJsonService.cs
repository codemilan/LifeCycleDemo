using System;
using System.Collections.Generic;

namespace POIApp
{
    public class POIJsonService : IPOIDataService
    {
        public POIJsonService()
        {
        }

        #region IPOIDataService implementation
        public IReadOnlyList<PointOfInterest> POIs
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void DeletePOI(PointOfInterest poi)
        {
            throw new NotImplementedException();
        }

        public PointOfInterest GetPOI(int id)
        {
            throw new NotImplementedException();
        }

        public void RefreshCache()
        {
            throw new NotImplementedException();
        }

        public void SavePOI(PointOfInterest poi)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}