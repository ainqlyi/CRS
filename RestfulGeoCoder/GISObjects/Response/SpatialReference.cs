namespace RestfulGeoCoder.GISObjects.Response
{
    /// <summary>
    /// SpatialReference object to include in json response REST call to ArcGIS Server
    /// </summary>
    public class SpatialReference
    {
        public int wkid { get; set; }
        public int latestWkid { get; set; }
    }
}