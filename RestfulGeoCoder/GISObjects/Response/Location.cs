namespace RestfulGeoCoder.GISObjects.Response
{
    /// <summary>
    /// Location object to include in json response REST call to ArcGIS Server
    /// </summary>
    public class Location
    {
        public string address { get; set; }
        public Location2 location { get; set; }
        public float score { get; set; }
        public Attributes attributes { get; set; }
    }
}