// HIT_N_TRAIL: i think should be in protable projects so that each platform specific projects can use this as a entity.
namespace POIApp
{
    public class PointOfInterest
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}