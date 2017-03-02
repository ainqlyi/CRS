using System.Collections.Generic;

namespace RestfulGeoCoder.GISObjects.Response
{
    /// <summary>
    /// RootResponseObject to include in json response REST call to ArcGIS Server
    /// </summary>
    public class RootResponseObject
    {
        public SpatialReference spatialReference { get; set; }
        public List<Location> locations { get; set; }
    }
}