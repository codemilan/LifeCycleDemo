using System.Collections.Generic;

namespace POIApp
{
    public interface IPOIDataService
    {
        IReadOnlyList<PointOfInterest> POIs { get; } // this will used for caching based on crud.
        void RefreshCache(); // this will be used to refresh cash.
        PointOfInterest GetPOI(int id); // Read operation for single PointOfInterest object based on id.
        void SavePOI(PointOfInterest poi);
        void DeletePOI(PointOfInterest poi);
    }
}