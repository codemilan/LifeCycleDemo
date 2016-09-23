// HIT_N_TRAIL: I think interface should be protable libraries, since this has
// act as an mediater between data service in application specfic code with the orm library.
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